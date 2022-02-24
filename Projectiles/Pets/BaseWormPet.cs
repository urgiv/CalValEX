using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace CalValEX.Projectiles.Pets
{
	public class WormPetSegment
    {
		public Vector2 position, oldPosition;
		public bool head;
	}

	public abstract class BaseWormPet : ModProjectile
	{
		public abstract string HeadTexture();
		public abstract string BodyTexture();
		public abstract string TailTexture();
		/// <summary>
		/// The "height" of a segment. Only counted for the body, the head and tail may be longer and it will be automatically adjusted for it
		/// </summary>
		public abstract int SegmentSize();
		/// <summary>
		/// The amount of segments in the worm
		/// </summary>
		public abstract int SegmentCount();
		/// <summary>
		/// Returns wether or not the pet should exist or not. This usually should be checking for the player bool
		/// </summary>
		/// <returns></returns>
		public abstract bool ExistenceCondition();
		/// <summary>
		/// Where the worm head would like to go, relative to the player's center
		/// </summary>
		public Vector2 RelativeIdealPosition;
		/// <summary>
		/// Where the worm head would like to go relative to the world
		/// </summary>
		public Vector2 IdealPosition => RelativeIdealPosition + Owner.Center;
		/// <summary>
		/// Has the projectile been initialized? This variable is automatically set so please don't use it yourself
		/// </summary>
		public ref float Initialized => ref projectile.ai[0];
		/// <summary>
		/// The steering angle of the worm head if its far away from the ideal position
		/// </summary>
		public virtual float MinimumSteerAngle => MathHelper.PiOver4 / 4f;
		/// <summary>
		/// The steering angle of the worm head if its close enough to the ideal position. Its good to have this steering angle high so the worm doesnt continually miss its target
		/// </summary>
		public virtual float MaximumSteerAngle => MathHelper.PiOver2;
		/// <summary>
		/// How many variants the worm body has. Variants are sheeted horizontally, so you may have multiple variants and an animated body at once. Of course you can always do the custom drawing yourself
		/// </summary>
		public virtual int BodyVariants => 1;
		/// <summary>
		/// How many iterations of the verlet chain simulation gets run every frame. More iterations = less gaps in the chain at higher speed
		/// </summary>
		public virtual int NumSimulationIterations => 15;
		/// <summary>
		/// How far away the worm should wander away from the players center. This is only useful if UpdateIdealPosition isn't overridden.
		/// </summary>
		public virtual float WanderDistance => Owner.velocity.Length() < 10 ? 200 : 100;
		/// <summary>
		/// The speed at which the worm head moves
		/// </summary>
		public virtual float GetSpeed() => MathHelper.Lerp(15, 40, MathHelper.Clamp(projectile.Distance(IdealPosition) / (WanderDistance * 2.2f) - 1f, 0, 1));

		public Player Owner => Main.player[projectile.owner];
		public CalValEXPlayer ModOwner => Owner.GetModPlayer<CalValEXPlayer>();

		public List<WormPetSegment> Segments;

		public ref float TimeTillReset => ref projectile.ai[1];

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Base Worm Head");
			ProjectileID.Sets.NeedsUUID[projectile.type] = true;
			Main.projPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = projectile.height = SegmentSize();
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.netImportant = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 300;
			projectile.tileCollide = false;
		}

		/// <summary>
		/// The full AI. If you don't need to entirely modify it, you can modify other behavior functions
		/// </summary>
		public virtual void WormAI()
		{
			bool shouldDoAI = CheckIfAlive();
			if (shouldDoAI)
			{
				UpdateIdealPosition();
				MoveTowardsIdealPosition();
				SimulateSegments();
				Animate();
				CustomAI();
			}
		}

		/// <summary>
		/// Initialize the worm. You shouldn't have to ever change this yourself
		/// </summary>
		public virtual void Initialize()
		{
			//Initialize the segments
			Segments = new List<WormPetSegment>(SegmentCount());
			for (int i = 0; i < SegmentCount(); i++)
            {
				WormPetSegment segment = new WormPetSegment();
				segment.head = false;
				segment.position = projectile.Center + Vector2.UnitY * SegmentSize() * i;
				segment.oldPosition = segment.position;
				Segments.Add(segment);
            }

			Segments[0].head = true;

			Initialized = 1f;
			return;
		}

		/// <summary>
		/// Checks if the worm should remain alive or if it should kill itself. Return false if the rest of the AI shouldn't even bother executing. This also kills the worm if its too far away from the player
		/// </summary>
		public virtual bool CheckIfAlive()
        {
			if (ExistenceCondition())
			{
				projectile.timeLeft = 2;
			}

			if (Owner.dead || !Owner.active || Owner.Distance(projectile.Center) > 4000)
			{
				projectile.timeLeft = 0;
				return false;
			}

			return true;
		}
		/// <summary>
		/// Updates the ideal position the worm head wants to reach
		/// </summary>
		public virtual void UpdateIdealPosition()
		{
			TimeTillReset++;
			if (TimeTillReset > 150)
			{
				RelativeIdealPosition = Vector2.Zero;
				TimeTillReset = 0;
			}

			//Reset the ideal position if the ideal position was reached
			if (projectile.Distance(IdealPosition) < GetSpeed())
				RelativeIdealPosition = Vector2.Zero;

			//Get a new ideal position
			if (RelativeIdealPosition == null || RelativeIdealPosition == Vector2.Zero)
			{
				RelativeIdealPosition = Main.rand.NextVector2CircularEdge(WanderDistance, WanderDistance);
				return;
			}
		}
		/// <summary>
		/// Makes the head move towards its ideal position.
		/// </summary>
		public virtual void MoveTowardsIdealPosition()
		{
			//Rotate towards its ideal position
			projectile.rotation = projectile.rotation.AngleTowards((IdealPosition - projectile.Center).ToRotation(), MathHelper.Lerp(MaximumSteerAngle, MinimumSteerAngle, MathHelper.Clamp(projectile.Distance(IdealPosition) / 80f, 0, 1)));
			projectile.velocity = projectile.rotation.ToRotationVector2() * GetSpeed();

			//Update its segment
			Segments[0].oldPosition = Segments[0].position;
			Segments[0].position = projectile.Center;
		}

		/// <summary>
		/// Makes the segments trail behind the worm
		/// </summary>
		public virtual void SimulateSegments()
        {
			//https://youtu.be/PGk0rnyTa1U?t=400 we use verlet integration chains here
			int i = 0;
			float movementLenght = projectile.velocity.Length();
			foreach(WormPetSegment segment in Segments)
            {
				if (!segment.head)
				{
					Vector2 positionBeforeUpdate = segment.position;

					//segment.position += (segment.position - segment.oldPosition); //=> This adds conservation of energy to the worm segments. This makes it super bouncy and shouldnt be used but it's really funny. Especially if you make the worm affected by gravity
					//segment.position += Vector2.UnitY * 0.2f; //=> This adds gravity to the worm segments. Works especially well when springy

					segment.position += Utils.SafeNormalize(Segments[i - 1].oldPosition - segment.position, Vector2.Zero) * movementLenght; //Makes the segment move towards the forward segment 
					segment.oldPosition = positionBeforeUpdate;
				}
				i++;
            }

			for (int k = 0; k < NumSimulationIterations; k++)
            {
				for (int j = 0; j < SegmentCount() - 1; j++)
				{
					WormPetSegment pointA = Segments[j];
					WormPetSegment pointB = Segments[j + 1];
					Vector2 segmentCenter = (pointA.position + pointB.position) / 2f;
					Vector2 segmentDirection = Utils.SafeNormalize(pointA.position - pointB.position, Vector2.UnitY);

					if (!pointA.head)
						pointA.position = segmentCenter + segmentDirection * SegmentSize() / 2f;

					if (!pointB.head)
						pointB.position = segmentCenter - segmentDirection * SegmentSize() / 2f;

					Segments[j] = pointA;
					Segments[j + 1] = pointB;
				}
			}
        }

		/// <summary>
		/// Mess with the frames here. Does nothing by default
		/// </summary>
		public virtual void Animate() { }
		/// <summary>
		/// Gets ran after everything else. Do whatever you want
		/// </summary>
		public virtual void CustomAI() { }


		public override void AI()
        {
			if (Initialized == 0)
				Initialize();
			WormAI();

            // Consistently update the worm
            if ((int)Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			Texture2D headTex = ModContent.GetTexture(HeadTexture());
			Texture2D bodyTex = ModContent.GetTexture(BodyTexture());
			Texture2D tailTex = ModContent.GetTexture(TailTexture());

			if (Initialized == 0f)
				return false;
			DrawWorm(spriteBatch, lightColor, headTex, bodyTex, tailTex);
			return false;
		}

		/// <summary>
		/// Draws the worm. Override this if you want to draw it yourself. 
		/// </summary>
		public virtual void DrawWorm(SpriteBatch spriteBatch, Color lightColor, Texture2D head, Texture2D body, Texture2D tail) 
		{
			for (int i = SegmentCount() - 1; i >= 0; i--)
			{
				bool bodySegment = i != 0 && i != SegmentCount() - 1;
				Texture2D sprite = bodySegment ? body : i == 0 ? head : tail;

				int frameStartX = bodySegment ? ((i % BodyVariants) * sprite.Width / BodyVariants) : 0;
				int frameStartY = sprite.Height / Main.projFrames[projectile.type] * projectile.frame;

				int frameWidth = sprite.Width / (bodySegment ? BodyVariants : 1);
				frameWidth -= (bodySegment && BodyVariants > 1) ? 2 : 0;

				int frameHeight = (sprite.Height / Main.projFrames[projectile.type]);
				frameHeight -= (Main.projFrames[projectile.type] > 1) ? 2 : 0;

				Rectangle frame = new Rectangle(frameStartX, frameStartY, frameWidth, frameHeight);
				Vector2 origin = bodySegment ? frame.Size() / 2f : i == 0 ? new Vector2(frame.Width / 2f, frame.Height - SegmentSize() / 2f) : new Vector2(frame.Width / 2f, SegmentSize() / 2f);

				float rotation = i == 0 ? projectile.rotation + MathHelper.PiOver2 : (Segments[i].position - Segments[i - 1].position).ToRotation() - MathHelper.PiOver2;

				Color segmentLight = Lighting.GetColor((int)Segments[i].position.X / 16, (int)Segments[i].position.Y / 16); //Lighting of the position of the segment

				spriteBatch.Draw(sprite, Segments[i].position - Main.screenPosition, frame, segmentLight, rotation, origin, projectile.scale, SpriteEffects.None, 0);
			}
		}
    }
}
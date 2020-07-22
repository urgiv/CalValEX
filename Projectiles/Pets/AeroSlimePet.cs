using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX;

namespace CalValEX.Projectiles.Pets {
	public class AeroSlimePet : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Aero Slime");
			Main.projFrames[projectile.type] = 6;
			
		}

		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.ZephyrFish);
			aiType = ProjectileID.ZephyrFish;
		}

		public override bool PreAI() {
			_ = Main.player[projectile.owner];
			return true;
		}

		public override void AI() {
			Player player = Main.player[projectile.owner];
			CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();
			if (player.dead) {
				modPlayer.aero = false;
			}
			if (modPlayer.aero) {
				projectile.timeLeft = 2;
			}
		}
	}
}
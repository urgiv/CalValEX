using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX;
using CalValEX.Items.Tiles;
using CalValEX.Items.Tiles.Blocks;
using CalamityMod.Tiles.FurnitureExo;
using Microsoft.Xna.Framework.Graphics;

namespace CalValEX.Tiles.Blocks {
	public class ThanatosPlatingPlaced : ModTile {
		public override void SetStaticDefaults()  {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileMerge[Type][TileType<ThanatosPlatingVentPlaced>()] = true;
			Main.tileMerge[Type][TileType<ExoPrismPanelTile>()] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			
			HitSound = SoundID.Tink;
			DustType = 226;
			ItemDrop = ItemType<ThanatosPlating>();
			
			AddMapEntry(new Color(51, 56, 63));
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) =>
			CalValEXGlobalTile.TileGlowmask(i, j, Request<Texture2D>("CalValEX/Tiles/Blocks/ThanatosPlatingPlaced_Glow").Value, spriteBatch);
	}

	public class ThanatosPlatingVentPlaced : ModTile {
		public override void SetStaticDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
			Main.tileMerge[Type][TileType<ThanatosPlatingPlaced>()] = true;
			Main.tileMerge[Type][TileType<ExoPrismPanelTile>()] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			
			HitSound = SoundID.Tink;
			DustType = 226;
			ItemDrop = ItemType<ThanatosPlatingVent>();
			
			AddMapEntry(new Color(51, 56, 63));
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch) =>
			CalValEXGlobalTile.TileGlowmask(i, j, Request<Texture2D>("CalValEX/Tiles/Blocks/ThanatosPlatingVentPlaced_Glow").Value, spriteBatch);
	}
}
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Blocks;

namespace CalValEX.Items.Tiles.Blocks
{
    public class ShadowBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Brick");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.rare = 0;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ShadowBrickPlaced>();
        }

        /*
        {
            Mod CalValEX = ModLoader.GetMod("CalamityMod");
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient((ModContent.ItemType<AuricBrick>()), 200);
            recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("ExoPrism"), 1);
            recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("CalamitousEssence"), 1);
            recipe.AddTile(ModLoader.GetMod("CalamityMod").TileType("DraedonsForge"));
            recipe.SetResult(this, 200);
            recipe.AddRecipe();
            ModRecipe recipe2 = new ModRecipe(mod);
            recipe2.AddIngredient(ModContent.ItemType<Items.Walls.ShadowBrickWall>(), 4);
            recipe2.AddTile(TileID.WorkBenches);
            recipe2.SetResult(this, 1);
            recipe2.AddRecipe();
        }*/
    }
}
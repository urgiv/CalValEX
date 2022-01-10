﻿using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.MiscFurniture;
using CalValEX.Items.Tiles.Blocks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CalValEX.Items.Tiles
{
    public class BrimstoneHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hanging Brimstone Heart");
            Tooltip.SetDefault("+Up!");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.createTile = ModContent.TileType<BrimstoneHeartPlaced>();
            item.width = 12;
            item.height = 12;
            item.rare = 11;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //rarity 12 (Turquoise) = new Color(0, 255, 200)
            //rarity 13 (Pure Green) = new Color(0, 255, 0)
            //rarity 14 (Dark Blue) = new Color(43, 96, 222)
            //rarity 15 (Violet) = new Color(108, 45, 199)
            //rarity 16 (Hot Pink/Developer) = new Color(255, 0, 255)
            //rarity rainbow (no expert tag on item) = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
            //rarity rare variant = new Color(255, 140, 0)
            //rarity dedicated(patron items) = new Color(139, 0, 0)
            //look at https://calamitymod.gamepedia.com/Rarity to know where to use the colors
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(108, 45, 199); //change the color accordingly to above
                }
            }
        }

        /*public override void AddRecipes()
        {
            Mod CalValEX = ModLoader.GetMod("CalamityMod");
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("BloodOrb"), 20);
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("CalamitousEssence"), 1);
                recipe.AddTile(ModLoader.GetMod("CalamityMod").TileType("DraedonsForge"));
                recipe.SetResult(this, 4);
                recipe.AddRecipe();
            }
        }*/
    }
}
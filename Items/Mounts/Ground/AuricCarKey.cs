﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Items.Mounts;

namespace CalValEX.Items.Mounts.Ground
{
    public class AuricCarKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auric Car Key");
            Tooltip.SetDefault("Yharim Car.\nMortally reduces health and damage if a boss is nearby");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item23;
            item.noMelee = true;
            item.mountType = mod.MountType("YharimCar");
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

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModContent.ItemType<ProfanedCycleItem>(), 1);
                recipe.AddIngredient(ModContent.ItemType<BloodstoneCarriageItem>(), 1);
                recipe.AddIngredient(ModContent.ItemType<GodRiderItem>(), 1);
                recipe.AddIngredient(ModContent.ItemType<SilvaJeepItem>(), 1);
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("AuricBar"), 10);
                recipe.AddTile(ModLoader.GetMod("CalamityMod").TileType("CosmicAnvil"));
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
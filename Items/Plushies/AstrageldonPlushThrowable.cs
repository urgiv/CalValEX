﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Projectiles.Plushies;
using CalValEX.Items.Tiles.Plushies;

namespace CalValEX.Items.Plushies
{
    public class AstrageldonPlushThrowable : ModItem
    {
        public override string Texture => "CalValEX/Items/Tiles/Plushies/AstrageldonPlush";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astrageldon Plushie (Throwable)");
            Tooltip.SetDefault("Can be thrown");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 44;
            item.height = 44;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.rare = 10;
            item.useAnimation = 20;
            item.useTime = 20;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = 20;
            item.shoot = mod.ProjectileType("AstrageldonPlush");
            item.shootSpeed = 6f;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            Mod Cata = ModLoader.GetMod("Catalyst");
            if (Cata != null)
            {
                Mod CalValEX = ModLoader.GetMod("CalamityMod");
                {
                    ModRecipe recipe = new ModRecipe(mod);
                    recipe.AddIngredient(ModContent.ItemType<Tiles.Plushies.AstrageldonPlush>());
                    recipe.SetResult(this);
                    recipe.AddRecipe();
                }
            }
        }
    }
}
﻿using Terraria.ID;
using CalValEX.Tiles.Paintings;
using Terraria.ModLoader;
using Terraria;

namespace CalValEX.Items.Tiles.Paintings
{
    public class AccidentalAbominationn : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Accidental Abomination");
            // Tooltip.SetDefault("'Maple'");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<AccidentalAbominationnPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Purple;
        }
    }
}
﻿using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Tiles.Blocks;

namespace CalValEX.Items.Tiles.Blocks
{
    public class DarklightMushroomCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<MushroomCap>();
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.White;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DarklightMushroomCapPlaced>();
        }
    }
}
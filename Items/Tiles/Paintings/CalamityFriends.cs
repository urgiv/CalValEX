using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID; 
using CalValEX.Tiles.Paintings;
using Terraria.ModLoader;
using CalValEX.CalamityID;

namespace CalValEX.Items.Tiles.Paintings
{
    public class CalamityFriends : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Little Buddies");
            // Tooltip.SetDefault("'Mochi'\nFriends!");
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<CalamityFriendsPlaced>();
            Item.width = 12;
            Item.height = 12;
            Item.rare = CalRarityID.HotPink;
        }
    }
}
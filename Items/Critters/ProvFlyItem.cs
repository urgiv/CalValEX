using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.NPCs.Critters;

namespace CalValEX.Items.Critters
{
    internal class ProvFlyItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Butterfly");
        }

        public override void SetDefaults()
        {
            //item.useStyle = 1;
            //item.autoReuse = true;
            //item.useTurn = true;
            //item.useAnimation = 15;
            //item.useTime = 10;
            //item.maxStack = 999;
            //item.consumable = true;
            //item.width = 12;
            //item.height = 12;
            //item.makeNPC = 360;
            //item.noUseGraphic = true;
            //item.bait = 15;

            item.CloneDefaults(ItemID.JuliaButterfly);
            Mod mod = ModLoader.GetMod("CalamityMod");
            if (mod == null)
            {
                return;
            }
            item.makeNPC = (short)NPCType<ProvFly>();
            item.bait = 45;
            item.rare = 11;
        }
    }
}
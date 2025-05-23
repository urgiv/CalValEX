using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalValEX.Items.Mounts
{
    public class ProfanedWheels : ModItem {
        public override void SetStaticDefaults() => ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ProfanedFrame>();

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 0, 33, 0);
            Item.rare = ItemRarityID.Purple;
        }
    }
}
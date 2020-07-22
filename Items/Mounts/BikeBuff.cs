﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Mounts
{
    public class BikeBuff : ModBuff
    {
        public override void SetDefaults() {
			DisplayName.SetDefault("Profaned Bike");
			Description.SetDefault("For when you really need to get somewhere, in style.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			player.mount.SetMount(ModContent.MountType<Mounts.ProfanedCycle>(), player);
			player.buffTime[buffIndex] = 10;
            player.thorns = 10f;
		}
	}
}
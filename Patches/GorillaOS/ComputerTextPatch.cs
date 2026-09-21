using BoolonxMenu.Mods;
using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoolonxMenu.Patches.GorillaOS
{
    [HarmonyPatch(typeof(GorillaComputer), "CreditsScreen")]
    internal class ComputerTextPatch
    {
        public static bool Prefix(GorillaComputer __instance)
        {
            string BoolonxTabText = "--- boolonx menu settings ---\n\n";
            if(SnowballFast.Enabled) BoolonxTabText += $"press 1 to change snowball speed ({SnowballFast.Multipliers[SnowballFast.MultiplierIndex]}x)\n";
            if(GSTBodyRotation.Enabled) BoolonxTabText += $"press 2 to change body rotation mode: {GSTBodyRotation.Modes[GSTBodyRotation.Mode]}\n";
            __instance.screenText.Set(BoolonxTabText);
            return false;
        }
    }
}

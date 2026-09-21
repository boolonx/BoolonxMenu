using BoolonxMenu.Mods;
using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Patches.GorillaOS
{
    [HarmonyPatch(typeof(GorillaComputer), "ProcessCreditsState", new Type[] { typeof(GorillaKeyboardBindings) })]
    internal class ComputerTabProcessPatch
    {
        public static bool Prefix(GorillaComputer __instance, GorillaKeyboardBindings buttonPressed)
        {
            switch (buttonPressed)
            {
                case GorillaKeyboardBindings.one:
                    SnowballFast.SwitchMultipliers();
                    break;                
                case GorillaKeyboardBindings.two:
                    GSTBodyRotation.SwitchMode();
                    break;
            }
            return false;
        }
    }
}

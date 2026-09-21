using System;
using System.Collections.Generic;
using System.Text;
using GorillaLocomotion.Climbing;
using HarmonyLib;
namespace BoolonxMenu.Patches
{
    [HarmonyPatch(typeof(GorillaPressableButton), nameof(GorillaPressableButton.Start))]
    internal class ButtonSoundPatch
    {
        public static bool Prefix(GorillaPressableButton __instance)
        {
            __instance.pressButtonSoundIndex = 336;
            return true;
        }
    }
}

using BoolonxMenu.Mods;
using UnityEngine;
using HarmonyLib;
using BepInEx;
using GorillaLocomotion;
using BoolonxMenu.Classes.Better_II_temp;

namespace BoolonxMenu.Patches
{
    [HarmonyPatch(typeof(VRRig), "PostTick")]
    public class GhostMonkey
    {
        private static bool Prefix(VRRig __instance)
        {
            if (SimpleInputs.LeftGrab && BoolonxMenu.Mods.GhostMonkey.Enabled && (__instance.isMyPlayer || __instance.isOfflineVRRig))
            {
                return false;
            }
            return true;
        }
    }
}

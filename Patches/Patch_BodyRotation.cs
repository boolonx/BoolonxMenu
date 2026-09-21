using GorillaLocomotion;
using HarmonyLib;
using BoolonxMenu.Mods;
using UnityEngine;

[HarmonyPatch(typeof(GTPlayerTransform), "BodyRotation", MethodType.Getter)]
public class Patch_BodyRotation
{
    private static bool Prefix(ref Quaternion __result)
    {
        if (GSTBodyRotation.Enabled)
        {
            __result = GSTBodyRotation.bodyRotation;
            return false;
        }
        return true;
    }
}
using BoolonxMenu.Mods;
using HarmonyLib;
using UnityEngine;

[HarmonyPatch(typeof(CrittersActor), nameof(CrittersActor.SetImpulseVelocity))]
public static class Patch_CrittersActor_SetImpulseVelocity
{

    [HarmonyPrefix]
    public static bool Prefix(ref Vector3 velocity, ref Vector3 angularVelocity)
    {
        // Multiply parameters before lastImpulseVelocity & lastImpulseAngularVelocity are assigned
        velocity *= SnowballFast.GetMultiplier();
        angularVelocity *= SnowballFast.GetMultiplier();

        return true; // Continue with original SetImpulseVelocity call
    }
}
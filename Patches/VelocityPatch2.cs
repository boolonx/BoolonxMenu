using GorillaLocomotion;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections.Generic;
using System.Text;
using static GorillaVelocityEstimator;
using UnityEngine;
using GorillaLocomotion.Climbing;
using HarmonyLib;
using BoolonxMenu.Mods;
using GorillaTagScripts;
using System.Collections;

namespace BoolonxMenu.Patches
{
    [HarmonyPatch(typeof(SnowballThrowable), "PerformSnowballThrowAuthority")]
    public static class VelocityPatch2
    {
        private static float originalMultiplier;

        [HarmonyPrefix]
        public static bool Prefix(ref float ___linSpeedMultiplier, ref float ___maxLinSpeed)
        {
            originalMultiplier = ___linSpeedMultiplier;

            ___linSpeedMultiplier *= SnowballFast.GetMultiplier();
            ___maxLinSpeed *= SnowballFast.GetMultiplier();

            return true;
        }

        [HarmonyPostfix]
        public static void Postfix(ref float ___linSpeedMultiplier, ref float ___maxLinSpeed)
        {
            ___linSpeedMultiplier = originalMultiplier;
            ___maxLinSpeed /= SnowballFast.GetMultiplier();
        }
    }
}

using BoolonxMenu.Mods;
using GorillaLocomotion.Climbing;
using HarmonyLib;
using UnityEngine;
using static GorillaLocomotion.Climbing.GorillaVelocityTracker;

// Replace target class name (e.g., VelocityTracker) with the actual class in Gorilla Tag
[HarmonyPatch(typeof(GorillaVelocityTracker), nameof(GorillaVelocityTracker.GetAverageVelocity))]
public static class Patch_GetAverageVelocity
{
    [HarmonyPrefix]
    public static bool Prefix(
        ref Vector3 __result,
        bool worldSpace,
        float maxTimeFromPast,
        bool doMagnitudeCheck,
        VelocityDataPoint[] ___localSpaceData,
        VelocityDataPoint[] ___worldSpaceData,
        int ___currentDataPointIndex,
        int ___maxDataPoints)
    {
        float num = maxTimeFromPast / 2f;
        VelocityDataPoint[] array = (!worldSpace) ? ___localSpaceData : ___worldSpaceData;

        if (array == null || array.Length <= 1)
        {
            __result = Vector3.zero;
            return false; // Skip original method
        }

        Vector3 total = Vector3.zero;
        float totalMag = 0f;
        int added = 0;
        float num2 = Time.time - maxTimeFromPast;
        float num3 = Time.time - num;
        int num4 = ___currentDataPointIndex;

        void AddPoint(VelocityDataPoint point)
        {
            total += point.delta;
            totalMag += point.delta.magnitude;
            added++;
        }

        for (int i = 0; i < ___maxDataPoints; i++)
        {
            VelocityDataPoint velocityDataPoint = array[num4];

            if (doMagnitudeCheck && added > 1 && velocityDataPoint.time >= num3)
            {
                if (velocityDataPoint.delta.magnitude >= totalMag / (float)added)
                {
                    AddPoint(velocityDataPoint);
                }
            }
            else if (velocityDataPoint.time >= num2)
            {
                AddPoint(velocityDataPoint);
            }

            num4++;
            if (num4 >= ___maxDataPoints)
            {
                num4 = 0;
            }
        }

        if (added > 0)
        {
            __result = (total / added) * SnowballFast.GetMultiplier();
        }
        else
        {
            __result = Vector3.zero;
        }

        return false;
    }
}
using GorillaLocomotion;
using HarmonyLib;
using Photon.Pun;
using BoolonxMenu.Mods;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace BoolonxMenu.Patches
{
    [HarmonyPatch(typeof(VRMap), nameof(VRMap.MapMine))]
    public class VRMapPatch
    {
        [HarmonyPrefix]
        private static bool Prefix(VRMap __instance, float ratio, Transform playerOffsetTransform)
        {
            if (__instance.rigTarget == null) return true;

            Transform physicsTarget = null;
            if (__instance.vrTargetNode == XRNode.Head && GTPlayer.Instance != null && GTPlayer.Instance.headCollider != null)
            {
                physicsTarget = GTPlayer.Instance.headCollider.transform;
            }
            else if (__instance.vrTargetNode == XRNode.LeftHand)
            {
                if (PhysicsGorilla.leftPhysicsTransform != null) physicsTarget = PhysicsGorilla.leftPhysicsTransform;
                else physicsTarget = GTPlayer.Instance.LeftHand.handFollower;
            }
            else if (__instance.vrTargetNode == XRNode.RightHand && PhysicsGorilla.rightPhysicsTransform != null)
            {
                if (PhysicsGorilla.rightPhysicsTransform != null) physicsTarget = PhysicsGorilla.rightPhysicsTransform;
                else physicsTarget = GTPlayer.Instance.RightHand.handFollower;
            }

            if (physicsTarget != null)
            {
                Quaternion targetRotation = physicsTarget.rotation * Quaternion.Euler(__instance.trackingRotationOffset);

                Vector3 targetPosition = physicsTarget.position + (targetRotation * (__instance.trackingPositionOffset * ratio));

                __instance.rigTarget.SetPositionAndRotation(targetPosition, targetRotation);

                return false;
            }

            return true;
        }
    }
}

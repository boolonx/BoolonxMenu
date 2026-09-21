using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Mods
{
    public class PunchMod : MonoBehaviour
    {
        public static float strength = 10f;

        public static Vector3 currentPosition;
        public static Vector3 lastPosition;
        public static float maxDistance = 0.4f;
        public static bool punch;

        public static void PunchUpdate()
        {
            foreach (VRRig rig in VRRigCache.ActiveRigs)
            {
                if (rig == null) continue;

                if (!(rig.isOfflineVRRig || rig.isMyPlayer))
                {
                    PunchTick(rig.leftHand.rigTarget.position, rig.scaleFactor);
                    PunchTick(rig.rightHand.rigTarget.position, rig.scaleFactor);
                }
            }
        }

        public static void PunchTick(Vector3 handPosition, float scale)
        {
            Rigidbody rb = GTPlayer.Instance.bodyCollider.attachedRigidbody;

            currentPosition = handPosition;

            bool touchingHead = Vector3.Distance(Camera.main.transform.position, handPosition) < maxDistance;
            bool punchAvailable = touchingHead && !(GTPlayer.Instance.LeftHand.wasColliding || GTPlayer.Instance.RightHand.wasColliding);
            bool readyToPunch = punchAvailable && !punch;

            if (readyToPunch)
            {
                punch = true;
                rb.linearVelocity = (currentPosition - lastPosition) * (strength * scale);
            }

            bool punched = !punchAvailable && punch;

            if (punched)
            {
                punch = false;
            }

            lastPosition = currentPosition;
        }
    }
}

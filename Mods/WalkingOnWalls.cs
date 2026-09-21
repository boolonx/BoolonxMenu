using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Mods
{
    public class WalkingOnWalls : MonoBehaviour
    {
        public static Vector3 currentNormal = Vector3.down;
        public static Vector3 normalLerp;
        public static float sdfslkdfj = 5f;
        public static Vector3 normalGravity = new Vector3(0f, -9.81f, 0f);
        public static Transform guy;

        // Update is called once per frame
        public static void Tick()
        {
            if (GTPlayer.Instance.LeftHand.wasColliding || GTPlayer.Instance.LeftHand.wasColliding)
            currentNormal = GTPlayer.Instance.GetTouchHitInfo(false).normal;
            normalLerp = Vector3.Lerp(normalLerp, currentNormal, sdfslkdfj * Time.deltaTime);
            Physics.gravity = normalLerp * -9.81f;
        }
    }
}

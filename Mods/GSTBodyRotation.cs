using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GorillaLocomotion;
using GorillaNetworking;

namespace BoolonxMenu.Mods
{
    internal class GSTBodyRotation
    {
        public static Quaternion bodyRotation;
        public static int Mode;
        public static bool Enabled;

        public static string[] Modes =
        {
            "Gold's Spooky Terror",
            "Stiff",
            "Spaz",
        };
        public static void Tick()
        {
            switch(Mode)
            {
                case 0:
                    bodyRotation = Quaternion.Slerp(bodyRotation, GTPlayer.Instance.headCollider.transform.rotation, 5f * Time.deltaTime);
                    break;            
                case 1:
                    bodyRotation = GTPlayer.Instance.headCollider.transform.rotation;
                    break;
                case 2:
                    bodyRotation = UnityEngine.Random.rotation;
                    break;
            }
        }

        public static void SwitchMode()
        {
            Mode++;
            if (Mode > Modes.Length - 1)
            {
                Mode = 0;
            }
            if (Mode < 0)
            {
                Mode = Mode - 1;
            }

            GorillaComputer.instance.UpdateScreen();
        }
        public static int Enable(int m) => Mode = m;
    }
}

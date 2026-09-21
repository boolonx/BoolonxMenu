using BoolonxMenu.Classes.Better_II_temp;
using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace BoolonxMenu.Mods
{
    internal class Noclip
    {
        public static bool clipping;

        public static bool Enabled;
        public static void Enable() => Enabled = true;
        public static void Disable() => Enabled = false;

        public static void Tick()
        {
            if(SimpleInputs.LeftTrigger)
            {
                clipping = true;
                GTPlayer.Instance.locomotionEnabledLayers = LayerMask.GetMask("Water");
                GTPlayer.Instance.headCollider.isTrigger = true;
                GTPlayer.Instance.bodyCollider.isTrigger = true;
            }
            else
            {
                clipping = false;
                GTPlayer.Instance.locomotionEnabledLayers = Plugin.defaultPlayerLayer;
                GTPlayer.Instance.headCollider.isTrigger = false;
                GTPlayer.Instance.bodyCollider.isTrigger = false;
            }
        }
    }
}

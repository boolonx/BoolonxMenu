using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Mods
{
    internal class GhostMonkey
    {
        public static bool Enabled;
        public static void Enable() => Enabled = true;
        public static void Disable() => Enabled = false;
    }
}

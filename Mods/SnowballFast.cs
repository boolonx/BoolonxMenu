using System;
using System.Collections.Generic;
using System.Text;

namespace BoolonxMenu.Mods
{
    internal class SnowballFast
    {
        public static float[] Multipliers = {
            1,
            2,
            5,
            10,
            25,
            50,
            75,
            100,
            1000
        };
        public static int MultiplierIndex = 1;

        public static bool Enabled;

        public static float GetMultiplier()
        {
            return Enabled ? Multipliers[MultiplierIndex] : 1f;
        }
        public static void SwitchMultipliers()
        {
            MultiplierIndex++;
            if (MultiplierIndex > Multipliers.Length - 1)
            {
                MultiplierIndex = 0;
            }
            if(MultiplierIndex < 0)
            {
                MultiplierIndex = MultiplierIndex - 1;
            }
        }
    }
}

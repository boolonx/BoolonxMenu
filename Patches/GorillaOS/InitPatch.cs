using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Patches.GorillaOS
{
    [HarmonyPatch(typeof(GorillaComputer), "Initialise")]
    internal class InitPatch
    {
        public static Material initmat;
        public static Material newmat;
        public static bool Prefix(GorillaComputer __instance)
        {

            __instance.OrderList[12] = new GorillaComputer.StateOrderItem(GorillaComputer.ComputerState.Credits, "boolonx");
            initmat = __instance.computerScreenRenderer.material;
            Material visualMat = new Material(Shader.Find("GorillaTag/UberShader"));
            visualMat.color = Settings.backgroundColor.colors[0].color;
            newmat = visualMat;
            return true;
        }
    }
}

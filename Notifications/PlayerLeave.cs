using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using BoolonxMenu.Notifications;
using UnityEngine;

namespace BoolonxMenu.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnPlayerLeftRoom")]
    public class LeavePatch : MonoBehaviour
    {
        private static void Prefix(Player otherPlayer)
        {
            if (otherPlayer != PhotonNetwork.LocalPlayer && otherPlayer != a)
            {
                NotifiLib.SendNotification($"<color=grey>[</color><color=red>LEAVE</color><color=grey>]</color> <color=white>Name: {otherPlayer.NickName} || {otherPlayer.UserId}</color>");
                a = otherPlayer;
            }
        }

        private static Player a;
    }
}
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Mods
{
    internal class RoomHax
    {
        public static float MaxPlayers = 20f;

        private static Room Writable()
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room == null) return null;
            if (!PhotonNetwork.IsMasterClient) return null;
            return room;
        }

        public static void ApplyMaxPlayers()
        {
            Room room = Writable();
            if (room == null) return;

            byte wanted = (byte)Mathf.Clamp(Mathf.RoundToInt(MaxPlayers), 2, 20);
            if (room.MaxPlayers == wanted) return;

            try { room.MaxPlayers = wanted;
            }
            catch (System.Exception e) { Notifications.NotifiLib.SendNotification($"Could not change max players: {e.Message}"); }
        }
    }
}

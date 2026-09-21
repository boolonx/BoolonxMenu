using GorillaLocomotion;
using Photon.Pun;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoolonxMenu.Mods
{
    public class Okay : MonoBehaviour
    {
        public static void TippyTappy(int index)
        {
            StiltID stiltID = StiltID.None;
            if (NetworkSystem.Instance.InRoom && GorillaTagger.Instance.myVRRig != null && GorillaTagger.Instance.myVRRig != null)
            {
                GorillaTagger.Instance.myVRRig.GetView.RPC("OnHandTapRPC", RpcTarget.All, index, true, true, stiltID, GorillaTagger.Instance.handTapSpeed, Utils.PackVector3ToLong(Vector3.zero));
            }
        }
    }
}

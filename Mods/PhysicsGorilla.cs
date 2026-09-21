using System.Collections;
using UnityEngine;
using BepInEx;
using GorillaLocomotion;
using HarmonyLib;
using static PlayerPrefFlags;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;

namespace BoolonxMenu.Mods
{
    public class PhysicsGorilla : MonoBehaviour
    {
        public static Transform leftPhysicsTransform;
        public static Transform rightPhysicsTransform;
        public static LayerMask initialLocoLayers;
        public static GameObject handsParent;
        public static bool PhysicsGrabEverywhere;
        public static void DisablePhysics()
        {
            GTPlayer.Instance.locomotionEnabledLayers = initialLocoLayers;
            Destroy(handsParent);
        }
        public static void InitializePhysics()
        {
            initialLocoLayers = GTPlayer.Instance.locomotionEnabledLayers;

            handsParent = Instantiate(new GameObject("Physics Hands"), GTPlayer.Instance.transform.parent);
            GameObject leftHand = Instantiate(new GameObject("Left"), handsParent.transform);
            GameObject rightHand = Instantiate(new GameObject("Right"), handsParent.transform);

            handsParent.transform.position = GTPlayer.Instance.headCollider.transform.position;

            SphereCollider leftCollider = leftHand.AddComponent<SphereCollider>();
            leftCollider.includeLayers = GTPlayer.Instance.locomotionEnabledLayers;
            leftCollider.excludeLayers = ~GTPlayer.Instance.locomotionEnabledLayers;

            SphereCollider rightCollider = rightHand.AddComponent<SphereCollider>();
            rightCollider.includeLayers = GTPlayer.Instance.locomotionEnabledLayers;
            rightCollider.excludeLayers = ~GTPlayer.Instance.locomotionEnabledLayers;

            PhysicsHand leftPhysics = leftHand.AddComponent<PhysicsHand>();
            PhysicsHand rightPhysics = rightHand.AddComponent<PhysicsHand>();

            leftPhysics.playerRigidbody = GTPlayer.Instance.playerRigidBody;
            leftPhysics.target = GTPlayer.Instance.LeftHand.handFollower;

            rightPhysics.playerRigidbody = GTPlayer.Instance.playerRigidBody;
            rightPhysics.target = GTPlayer.Instance.RightHand.handFollower;

            GTPlayer.Instance.locomotionEnabledLayers = 0;
            GorillaTagger.Instance.offlineVRRig.leftHandTransform = leftPhysics.transform;
            GorillaTagger.Instance.offlineVRRig.rightHandTransform = rightPhysics.transform;

            leftPhysicsTransform = leftPhysics.transform;
            rightPhysicsTransform = rightPhysics.transform;

            leftPhysics.leftController = true;
        }
    }
}
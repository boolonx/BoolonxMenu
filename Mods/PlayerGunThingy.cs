using BoolonxMenu.Classes.Better_II_temp;
using GorillaLocomotion;
using GorillaTagScripts;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace BoolonxMenu.Mods
{
    internal class PlayerGunThingy : MonoBehaviour
    {
        public static LineRenderer renderer;
        public static TextMeshPro tmp;
        public static Transform lastColliderTransform;
        public static VRRig selectedRig;
        public static VRRig selectingRig;
        public static void Enable()
        {
            renderer = UnityEngine.Object.Instantiate(new GameObject("Player Gun Visual"), Vector3.zero, Quaternion.identity).AddComponent<LineRenderer>();

            Material visualMat = new Material(Shader.Find("GorillaTag/UberShader"));
            visualMat.color = Color.blue;
            renderer.material = visualMat;

            Transform LeftHand = GTPlayer.Instance.LeftHand.handFollower;
            tmp = UnityEngine.Object.Instantiate(GorillaTagger.Instance.offlineVRRig.playerText1, LeftHand.position, LeftHand.rotation);
            tmp.text = "hold grip to use the gun, press trigger to select the player";

            GameObject GTPlayerParent = GTPlayer.Instance.transform.parent.gameObject;
            if (!GTPlayerParent.GetComponent<PlayerGunThingyTicker>()) GTPlayerParent.AddComponent<PlayerGunThingyTicker>();

        }
        public static void Disable()
        {
            GameObject.Destroy(renderer);
            GameObject.Destroy(tmp.gameObject);
            GameObject GTPlayerParent = GTPlayer.Instance.transform.parent.gameObject;
            if (GTPlayerParent.GetComponent<PlayerGunThingyTicker>()) Destroy(GTPlayerParent.GetComponent<PlayerGunThingyTicker>());
        }
        public static void Tick()
        {


        }
    }

    public class PlayerGunThingyTicker : MonoBehaviour
    {
        public string GoodyTime(string value)
        {
            if (double.TryParse(value, out double parsed)) return TimeSpan.FromSeconds(parsed).ToString(@"hh\:mm\:ss");
            return "error";
        }
        public static string Platform(Player player)
        {
            bool flag = player == null || player.CustomProperties == null;
            string result;
            if (flag)
            {
                result = "hidden";
            }
            else
            {
                foreach (string key in new string[] { "platform", "Platform", "plat", "cP" })
                {
                    bool flag2 = player.CustomProperties.ContainsKey(key);
                    if (flag2)
                    {
                        object obj = player.CustomProperties[key];
                        return ((obj != null) ? obj.ToString() : null) ?? "hidden";
                    }
                }
                result = "hidden";
            }
            return result;
        }
        public void LateUpdate()
        {
            float size = GorillaTagger.Instance.offlineVRRig.scaleFactor;
            Transform LeftHand = GTPlayer.Instance.LeftHand.handFollower;

            Vector3 lhandpos = LeftHand.position;
            lhandpos.y += 0.2f;
            PlayerGunThingy.tmp.transform.rotation = Quaternion.LookRotation(GTPlayer.Instance.headCollider.transform.forward);
            PlayerGunThingy.tmp.transform.position = lhandpos;

            Ray ray = new Ray(LeftHand.position, LeftHand.forward);
            RaycastHit hit;
            if (SimpleInputs.LeftGrab)
            {
                if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Gorilla Tag Collider")))
                {
                    if (hit.collider.gameObject.name == "BodyTrigger")
                    {
                        PlayerGunThingy.lastColliderTransform = hit.collider.transform;
                    }
                    else return;
                    VRRig sr = PlayerGunThingy.lastColliderTransform.parent.parent.parent.GetComponent<VRRig>();
                    if (sr != null)
                    {
                        if (PlayerGunThingy.selectingRig != sr)
                        {
                            GorillaTagger.Instance.StartVibration(true, 0.5f, 0.05f);
                        }
                        PlayerGunThingy.selectingRig = sr;

                    }

                }
            }
            if (SimpleInputs.LeftTrigger)
            {
                PlayerGunThingy.selectedRig = PlayerGunThingy.selectingRig;
            }
            if (PlayerGunThingy.selectedRig != null)
            {
                VRRig player = PlayerGunThingy.selectedRig;
                TextMeshPro tmp = PlayerGunThingy.tmp;
                tmp.fontStyle = TMPro.FontStyles.Normal;
                tmp.characterSpacing = 0f;
                tmp.lineSpacing = 0f;
                string tmptext = string.Empty;
                bool master = player.Creator.IsMasterClient;
                SubscriptionManager.SubscriptionDetails vimDetails =
                    SubscriptionManager.GetSubscriptionDetails(player);
                tmptext +=
                    $"<b><color=#{ColorUtility.ToHtmlStringRGB(player.playerColor)}>{player.Creator.NickName}</color></b>";
                tmptext +=
                    $"\n<size={tmp.fontSize / 3}>master client: {master}, actor number: {player.Creator.ActorNumber}, platform: {Platform(player.Creator.GetPlayerRef()).ToString().ToLower()}, user id: {player.Creator.UserId}, default name: {player.Creator.DefaultName}\ncolor code: r{player.playerColor.r * 9}, g{player.playerColor.g * 9}, b{player.playerColor.b * 9}, join time: {GoodyTime(player.Creator.JoinedTime.ToString())}, left time: {GoodyTime(player.Creator.LeftTime.ToString())}\nvim: active = {vimDetails.active}, days accrued = {vimDetails.daysAccrued}, subscription deadline = {vimDetails.subscriptionActiveUntilDate}</size>\n";
                tmp.text = tmptext ;

                foreach (VRRig rig in VRRigCache.ActiveRigs)
                {
                    AudioSource rigSource = rig.transform.Find("rig/head/SpeakerHeadCollider/HeadSpeaker").GetComponent<AudioSource>();
                    rigSource.spatialBlend = (rig == PlayerGunThingy.selectedRig) ? 0f : 0.9f;
                    rigSource.bypassEffects = rig == PlayerGunThingy.selectedRig;
                    rigSource.spatialize = rig != PlayerGunThingy.selectedRig;
                    // rig == PlayerGunThingy.selectedRig
                }
            }
            if(PlayerGunThingy.selectingRig != null)
            {

                PlayerGunThingy.renderer.material.color = PlayerGunThingy.selectingRig.playerColor;
            }


            if (PlayerGunThingy.renderer != null)
            {
                PlayerGunThingy.renderer.SetPosition(0, LeftHand.position);
                if (PlayerGunThingy.lastColliderTransform != null) PlayerGunThingy.renderer.SetPosition(1, PlayerGunThingy.lastColliderTransform.position);
                PlayerGunThingy.renderer.startWidth = 0.01f * size;
                PlayerGunThingy.renderer.endWidth = 0.01f * size;
                PlayerGunThingy.renderer.enabled = SimpleInputs.LeftGrab;
            }
        }
    }
}

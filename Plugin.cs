using BepInEx;
using BoolonxMenu.Classes.Better_II_temp;
using BoolonxMenu.Features;
using GorillaLocomotion;
using System;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR;

namespace BoolonxMenu
{
    [System.ComponentModel.Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static float initHandTapCooldown;
        public static LayerMask defaultPlayerLayer;
        public static AudioClip clickSound;
        readonly string menuBundleURL = "https://pub-b049168e106446d09ec3bf66304460a0.r2.dev/menu.bundle";
        public static AudioSource clickSource;
        public static Font menuFontAsset;
        public static AssetBundle menuBundle;
        public static GameObject brine;
        public static GameObject brinePlatforms;
        public static AudioSource menuSource;
        public static ManualAngularVelocity handTracker;
        public void Awake()
        {

            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);
            StartCoroutine(LoadFontFromWeb(menuBundleURL));
        }

        public void OnPlayerSpawned()
        {
            initHandTapCooldown = GorillaTagger.Instance.tapCoolDown;
            defaultPlayerLayer = GTPlayer.Instance.locomotionEnabledLayers;
            menuSource = Instantiate(new GameObject("Menu Source"), GorillaTagger.Instance.offlineVRRig.leftHandPlayer.transform).AddComponent<AudioSource>();
            
            handTracker = gameObject.AddComponent<ManualAngularVelocity>();
            handTracker.trackThis = GTPlayer.Instance.LeftHand.controllerTransform;

            Patches.PatchHandler.PatchAll();
        }

        public IEnumerator LoadFontFromWeb(string url)
        {
            using UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                menuBundle = DownloadHandlerAssetBundle.GetContent(www);
                if (menuBundle != null)
                {
                    menuFontAsset = menuBundle.LoadAsset<Font>("menufont");
                    brine = menuBundle.LoadAsset<GameObject>("BrianPrefab");
                    brinePlatforms = menuBundle.LoadAsset<GameObject>("BrianGriffinPlatforms");
                    clickSound = menuBundle.LoadAsset<AudioClip>("tick");
                }
            }
            else
            {
                Debug.LogError($"Failed to load AssetBundle: {www.error}");
            }
        }

        public static void MakeBrian(Vector3 position)
        {
            GameObject brineobject = Instantiate(brine, position, Quaternion.identity);
        }
    }
}

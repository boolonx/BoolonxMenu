using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using GorillaLocomotion;
using BoolonxMenu.Classes.Better_II_temp;
using UnityEngine.UIElements;

namespace BoolonxMenu.Mods
{
    public class GorillaTahFly : MonoBehaviour
    {
        public static LineRenderer renderer;
        public static void Enable()
        {
            renderer = UnityEngine.Object.Instantiate(new GameObject("GTAH Fly Visual"), Vector3.zero, Quaternion.identity).AddComponent<LineRenderer>();

            Material visualMat = new Material(Shader.Find("GorillaTag/UberShader"));
            visualMat.color = Color.orange;
            renderer.material = visualMat;

            GameObject GTPlayerParent = GTPlayer.Instance.transform.parent.gameObject;
            if(!GTPlayerParent.GetComponent<GTAHFlyTicker>()) GTPlayerParent.AddComponent<GTAHFlyTicker>();
            
        }
        public static void Disable()
        {
            GameObject.Destroy(renderer);

            GameObject GTPlayerParent = GTPlayer.Instance.transform.parent.gameObject;
            if (GTPlayerParent.GetComponent<GTAHFlyTicker>()) Destroy(GTPlayerParent.GetComponent<GTAHFlyTicker>());
        }
        public static void Tick()
        {

        }
    }

    public class GTAHFlyTicker : MonoBehaviour
    { 
        public void LateUpdate()
        {
            float size = GorillaTagger.Instance.offlineVRRig.scaleFactor;
            Transform RightHand = GTPlayer.Instance.RightHand.handFollower;
            if (SimpleInputs.RightTrigger && SimpleInputs.RightGrab) GTPlayer.Instance.GetComponent<Rigidbody>().AddForce(RightHand.forward * (10f * size), ForceMode.Impulse);
            if (GorillaTahFly.renderer != null)
            {
                GorillaTahFly.renderer.SetPosition(0, RightHand.position);
                GorillaTahFly.renderer.SetPosition(1, RightHand.position + GTPlayer.Instance.bodyCollider.attachedRigidbody.linearVelocity);
                GorillaTahFly.renderer.startWidth = 0.01f * size;
                GorillaTahFly.renderer.endWidth = 0.01f * size;
                GorillaTahFly.renderer.enabled = SimpleInputs.RightGrab;
            }
        }
    }
}

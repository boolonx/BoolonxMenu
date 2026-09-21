using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Player = GorillaLocomotion.GTPlayer;

namespace BoolonxMenu.Mods
{
    public class FlyGuy : MonoBehaviour
    {

        public static bool Enabled;
        public static void Enable()
        {
            GameObject GTPlayerParent = Player.Instance.transform.parent.gameObject;
            if(!GTPlayerParent.GetComponent<FlyGuyTicker>()) GTPlayerParent.AddComponent<FlyGuyTicker>();
        }
        public static void Disable()
        {
            GameObject GTPlayerParent = Player.Instance.transform.parent.gameObject;
            if (GTPlayerParent.GetComponent<FlyGuyTicker>()) Destroy(GTPlayerParent.GetComponent<FlyGuyTicker>());
        }
    }

    public class FlyGuyTicker : MonoBehaviour
    {
        public static float speedScale = 10;
        public static float acceleration = .1f;
        static Vector2 xz;
        static float y;
        public void FixedUpdate()
        {
					
			
			
            // nullify gravity by adding it's negative value to the player's velocity
            var rb = Player.Instance.bodyCollider.attachedRigidbody;
            rb.AddForce(-Physics.gravity * rb.mass);

            xz = ControllerInputPoller.Primary2DAxis(UnityEngine.XR.XRNode.LeftHand);
            y = ControllerInputPoller.Primary2DAxis(UnityEngine.XR.XRNode.RightHand).y;
			
			if (Input.GetKey(KeyCode.W)) xz.y += 1f;
			if (Input.GetKey(KeyCode.S)) xz.y -= 1f;
			if (Input.GetKey(KeyCode.A)) xz.x -= 1f;
			if (Input.GetKey(KeyCode.D)) xz.x += 1f;
			
			if (Input.GetKey(KeyCode.Q)) y -= 1f;
			if (Input.GetKey(KeyCode.E)) y += 1f;

            Vector3 inputDirection = new Vector3(xz.x, y, xz.y);

            // Get the direction the player is facing but nullify the y axis component
            var playerForward = Player.Instance.bodyCollider.transform.forward;
            playerForward.y = 0;

            // Get the right vector of the player but nullify the y axis component
            var playerRight = Player.Instance.bodyCollider.transform.right;
            playerRight.y = 0;

            var velocity =
                inputDirection.x * playerRight +
                y * Vector3.up +
                inputDirection.z * playerForward;
            velocity *= speedScale;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, velocity, acceleration);
        }
    }
}

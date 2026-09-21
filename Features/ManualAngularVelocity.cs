using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace BoolonxMenu.Features
{
    public class ManualAngularVelocity : MonoBehaviour
    {
        private Quaternion previousRotation;
        public Transform trackThis;

        void Start()
        {
            previousRotation = trackThis.rotation;
        }

        void Update()
        {
            if (Time.deltaTime <= 0) return;

            Quaternion deltaRotation = trackThis.rotation * Quaternion.Inverse(previousRotation);

            deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

            if (angle > 180f) angle -= 360f;

            Vector3 angularVelocity = axis * (angle / Time.deltaTime);

            float angularSpeed = Mathf.Abs(angle / Time.deltaTime);


            previousRotation = trackThis.rotation;
        }

        public Vector3 GetAngularVelocity()
        {
            return previousRotation.eulerAngles;
        }
    }
}

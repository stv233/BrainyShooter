using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectRotate : MonoBehaviour
    {
        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Rotate rigibody object by quaterion.
        /// </summary>
        /// <param name="rotation">Rotate quaterion</param>
        public void Rotate(Quaternion rotation)
        {
            Rotate(rotation.eulerAngles);
        }

        /// <summary>
        /// Rotate rigibody object by euler angles.
        /// </summary>
        /// <param name="eulerRotation">Rotate angles</param>
        public void Rotate(Vector3 eulerRotation)
        {
            Quaternion deltaRotation = Quaternion.Euler(rigidbody.rotation.eulerAngles + eulerRotation);
            rigidbody.MoveRotation(deltaRotation);
        }
    }
}

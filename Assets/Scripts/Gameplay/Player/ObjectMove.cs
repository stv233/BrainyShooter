using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.Gameplay.Player
{
    /// <summary>
    /// Move Rigibody object.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectMove : MonoBehaviour
    {
        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Move the object in the direction.
        /// </summary>
        /// <param name="directon">Direction</param>
        public void Move(Vector3 directon)
        {
            rigidbody.velocity = directon;
        }
    }
}

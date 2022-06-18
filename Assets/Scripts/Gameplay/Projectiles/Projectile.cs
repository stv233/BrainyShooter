using StvDEV.BS.Gameplay.Player;
using StvDEV.BS.Managment;
using StvDEV.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.Gameplay.Projectiles
{
    /// <summary>
    /// Projectile class.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool overrideGameData;
        [HideIf("overrideGameData")]
        [SerializeField] private float overrideForce;

        private float force;
        private new Rigidbody rigidbody;
        private Vector3 velocity;

        private void Awake()
        {
            if (overrideGameData)
            {
                force = overrideForce;
            }
            else
            {
                force = (float)GameManager.Instance.GameData.GetSettingByName("shotForce");
            }
        }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            velocity = transform.up * force;
            rigidbody.velocity = velocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Player.Player player))
            {
                player.Hit();
                Destroy(gameObject);
            }
            else
            {
                if (collision.collider.CompareTag("ProjectileWall"))
                {
                    Destroy(gameObject);
                }
                else
                {
                    Reflect(collision.contacts[0].normal);
                }
            }
    }

        /// <summary>
        /// Reflect projectile.
        /// </summary>
        /// <param name="reflectVector">Normal vector</param>
        private void Reflect(Vector3 reflectVector)
        {
            velocity = Vector3.Reflect(velocity, reflectVector);
            rigidbody.velocity = velocity;
        }
    }
}

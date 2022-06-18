using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.BS.Gameplay.Player
{
    /// <summary>
    /// Player object class.
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float fxLiveTime;

        [Header("FX")]
        [SerializeField] private GameObject hitFX;

        [Header("Events")]
        [SerializeField] private UnityEvent onHit;
        [SerializeField] private UnityEvent onRespawn;

        private Vector3 startPosition;
        private Quaternion startRotation;

        /// <summary>
        /// Called in case of hitting a player.
        /// </summary>
        public UnityEvent OnHit => onHit;

        /// <summary>
        /// Called when player respawned;
        /// </summary>
        public UnityEvent OnRespawn => onRespawn;

        private void Awake()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        /// <summary>
        /// Return the player to their original positions.
        /// </summary>
        public void Respawn()
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            onRespawn?.Invoke();
        }

        /// <summary>
        /// Hit the player.
        /// </summary>
        public void Hit()
        {
            onHit?.Invoke();
            SpawnFX();
        }

        private void SpawnFX()
        {
            if (hitFX)
            {
                Destroy(Instantiate(hitFX, transform.position, transform.rotation), fxLiveTime);
            }
        }
    }
}

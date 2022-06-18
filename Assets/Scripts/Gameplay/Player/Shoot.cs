using StvDEV.BS.Managment;
using StvDEV.Inspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.BS.Gameplay.Player
{
    /// <summary>
    /// Player shoot controller;
    /// </summary>
    public class Shoot : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform muzzle;
        [SerializeField] private bool overrideGameData;
        [HideIf("overrideGameData")]
        [SerializeField] private float overrideShotDelay;

        [Header("Prefabs")]
        [SerializeField] private Projectiles.Projectile projectile;

        [Header("Events")]
        [SerializeField] private UnityEvent onShot;


        private float shotDelay;
        private bool canShoot = true;

        /// <summary>
        /// Occurs when fired.
        /// </summary>
        public UnityEvent OnShot => onShot;

        /// <summary>
        /// Player gun muzzle.
        /// </summary>
        public Transform Muzzle => muzzle;

        private void Awake()
        {
            if (overrideGameData)
            {
                shotDelay = overrideShotDelay;
            }
            else
            {
                shotDelay = (float)GameManager.Instance.GameData.GetSettingByName("shotDelay");
            }
        }


        /// <summary>
        /// Make shot.
        /// </summary>
        public void Shot()
        {
            if (canShoot)
            {
                Instantiate(projectile, muzzle.position, muzzle.rotation);
                onShot.Invoke();
                canShoot = false;
                Observable.Timer(TimeSpan.FromSeconds(shotDelay)).TakeUntilDisable(gameObject).Subscribe(_ =>
                {
                    canShoot = true;
                });
            }
            
        }
    }
}

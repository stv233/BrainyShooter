using DG.Tweening;
using StvDEV.BS.Gameplay.Player;
using StvDEV.BS.Gameplay.Projectiles;
using StvDEV.BS.UI;
using StvDEV.StarterPack;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.BS.Managment
{
    /// <summary>
    /// Game Levels Manager.
    /// </summary>
    public class LevelManager : MonoBehaviourSingleton<LevelManager>
    {
        [Header("Lists")]
        [SerializeField] private List<GameObject> levels;
        [SerializeField] private List<Player> players;

        [Header("Links")]
        [SerializeField] private Fade fade;

        [Header("Settings")]
        [SerializeField] private float fadeTime;
        [SerializeField] private float levelEndDelay;

        [Header("Events")]
        [SerializeField] private UnityEvent onLevelEnd;
        [SerializeField] private UnityEvent onLevelStart;

        private int currentLevel = 0;
        private bool endLevel;

        /// <summary>
        /// Called when level ends.
        /// </summary>
        public UnityEvent OnLevelEnd => onLevelEnd;

        /// <summary>
        /// Called when level starts.
        /// </summary>
        public UnityEvent OnLevelStart => onLevelStart;

        protected override void Start()
        {
            players.ForEach(x =>
            {
                x.OnHit.AddListener(() =>
                {
                    x.gameObject.SetActive(false);
                    if (!endLevel)
                    {
                        endLevel = true;
                        Observable.Timer(TimeSpan.FromSeconds(levelEndDelay)).TakeUntilDisable(gameObject).Subscribe(_ =>
                        {
                            EndLevel();
                        });
                    }
                });
            });

            StartLevel(0);
            InputManager.Instance.LockCursor(true);
        }

        private void EndLevel()
        {
            InputManager.Instance.LockInput(true);
            fade.In(fadeTime).OnComplete(() =>
            {
                ClearAllProjectiles();
                levels[currentLevel].SetActive(false);
                currentLevel++;
                if (currentLevel >= levels.Count)
                {
                    currentLevel = 0;
                }
                StartLevel(currentLevel);
            });
            onLevelEnd?.Invoke();
        }

        private void StartLevel(int level)
        {
            endLevel = false;
            currentLevel = level;
            levels[level].SetActive(true);
            players.ForEach(x => { x.Respawn(); x.gameObject.SetActive(true); });
            fade.Out(fadeTime);
            InputManager.Instance.LockInput(false);
            onLevelStart?.Invoke();
        }

        private void ClearAllProjectiles()
        {
            foreach (Projectile projectile in FindObjectsOfType<Projectile>())
            {
                Destroy(projectile.gameObject);
            }
        }
    }
}

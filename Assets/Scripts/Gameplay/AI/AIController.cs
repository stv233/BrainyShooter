using StvDEV.BS.Gameplay.Player;
using StvDEV.BS.Managment;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace StvDEV.BS.Gameplay.AI
{
    /// <summary>
    /// Provides artificial player control.
    /// </summary>
    public class AIController : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Player.Player player;
        [SerializeField] private Shoot shoot;

        [Header("Settings")]
        [SerializeField] private float stoppingDistance;

        private Transform target;

        private void Start()
        {
            agent.stoppingDistance = stoppingDistance;
            player.OnHit.AddListener(() => { agent.enabled = false;});
            player.OnRespawn.AddListener(() => agent.enabled = true);
            LevelManager.Instance.OnLevelEnd.AddListener(() => agent.enabled = false);
            LevelManager.Instance.OnLevelStart.AddListener(() => agent.enabled = true);
        }

        private void FixedUpdate()
        {
            if (InputManager.Instance.InputLocked || !player.gameObject.activeInHierarchy)
            {
                return;
            }

            if (target == null)
            {
                target = FindTarget();
            }

            if (target != null)
            {
                agent.SetDestination(target.position);
                Shot();
            }

        }

        /// <summary>
        /// Make AI Shot.
        /// </summary>
        public void Shot()
        {
            if (Physics.Raycast(shoot.Muzzle.position, shoot.Muzzle.up, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out Player.Player player))
                {
                    if (target != player.transform)
                    {
                        target = player.transform;
                    }
                    shoot.Shot();
                }
            }
        }

        /// <summary>
        /// Finds another player in level.
        /// </summary>
        /// <returns>Another player or null</returns>
        public Transform FindTarget()
        {
            return FindObjectsOfType<Player.Player>().Where(x => x != player).FirstOrDefault()?.transform;
        }
    }
}

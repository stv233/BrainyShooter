using StvDEV.BS.Gameplay.Player;
using StvDEV.StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.BS.Managment
{
    /// <summary>
    /// Player score manager.
    /// </summary>
    public class ScoreManager : MonoBehaviourSingleton<ScoreManager>
    {
        [Header("Links")]
        [SerializeField] private List<Player> players;

        [Header("Events")]
        [SerializeField] private UnityEvent onScoreChanged;

        private Dictionary<Player, int> scores = new Dictionary<Player, int>();

        /// <summary>
        /// Calls when score changed.
        /// </summary>
        public UnityEvent OnScoreChanged => onScoreChanged;

        /// <summary>
        /// Player scores;
        /// </summary>
        public IReadOnlyDictionary<Player, int> Scores => scores;

        protected override void AwakeSingletone()
        {
            foreach (Player player in players)
            {
                scores.Add(player, 0);
                player.OnHit.AddListener(() =>
                {
                    scores[player]++;
                    onScoreChanged?.Invoke();
                });
            }
            base.AwakeSingletone();
        }
    }
}

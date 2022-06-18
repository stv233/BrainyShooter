using StvDEV.BS.Managment;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace StvDEV.BS.UI
{
    /// <summary>
    /// Display score in text.
    /// </summary>
    public class ScoreText : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private TMP_Text text;

        [Header("Settings")]
        [Multiline(3)]
        [SerializeField] private string pattern;

        private void Start()
        {
            ScoreManager.Instance.OnScoreChanged.AddListener(UpdateScore);
            UpdateScore();
        }

        /// <summary>
        /// Update score text.
        /// </summary>
        public void UpdateScore()
        {
            text.text = pattern.Replace("{Score}", GetScoreString());
        }

        /// <summary>
        /// Build score string.
        /// </summary>
        /// <returns>Score string</returns>
        private string GetScoreString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach ((int Score, int Index) score in ScoreManager.Instance.Scores.Values.Select((x,i) => ( Score:x, Index:i)))
            {
                stringBuilder.Append($"{(score.Index == 0 ? " " : " : ")}{score.Score}");
            }

            return stringBuilder.ToString();
        }
    }
}

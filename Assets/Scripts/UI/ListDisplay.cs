using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace StvDEV.BS.UI
{
    /// <summary>
    /// Display string list in text.
    /// </summary>
    public class ListDisplay : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// Display string list in text.
        /// </summary>
        /// <param name="stringList">String list</param>
        public void Display(List<string> stringList)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in stringList)
            {
                builder.Append($"{str}\n");
            }

            text.text = builder.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace StvDEV.BS.UI
{
    /// <summary>
    /// Fading image.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class Fade : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Color fadeColor;

        private Color baseColor;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            baseColor = image.color;
        }

        /// <summary>
        /// Fade in.
        /// </summary>
        /// <param name="fadeDuration">Duration</param>
        /// <returns>Tween</returns>
        public DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> In(float fadeDuration)
        {
            image.color = baseColor;
            return image.DOColor(fadeColor, fadeDuration);
        }

        /// <summary>
        /// Fade out.
        /// </summary>
        /// <param name="fadeDuration">Duration</param>
        /// <returns>Tween</returns>
        public DG.Tweening.Core.TweenerCore<Color, Color, DG.Tweening.Plugins.Options.ColorOptions> Out(float fadeDuration)
        {
            image.color = fadeColor;
            return image.DOColor(baseColor, fadeDuration);
        }

    }
}

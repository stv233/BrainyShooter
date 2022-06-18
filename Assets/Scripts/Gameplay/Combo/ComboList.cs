using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace StvDEV.BS.Gameplay.Combo
{
    /// <summary>
    /// Combo list.
    /// </summary>
    public class ComboList
    {
        private Queue<(string Combo, IDisposable Timer)> list = new Queue<(string Combo, IDisposable Timer)>();
        private float inListTime;

        /// <summary>
        /// Combo list.
        /// </summary>
        public List<string> List => list.Select(x => x.Combo).ToList();

        /// <summary>
        /// Combo list
        /// </summary>
        /// <param name="inListTime">How long combos been in list</param>
        public ComboList(float inListTime)
        {
            this.inListTime = inListTime;
        }

        /// <summary>
        /// Add combo to list.
        /// </summary>
        /// <param name="comboString">Combo string</param>
        public void AddCombo(string comboString)
        {
            (string Combo, IDisposable Timer) combo = (Combo: comboString, Timer: null);
            combo.Timer = Observable.Timer(TimeSpan.FromSeconds(inListTime)).Subscribe(_ =>
            {
                RemoveCombo(combo);
            });
            list.Enqueue(combo);
        }

        /// <summary>
        /// Remove combo from list.
        /// </summary>
        /// <param name="combo">Combo</param>
        public void RemoveCombo((string Combo, IDisposable Timer) combo)
        {
            List<(string Combo, IDisposable Timer)> tempList = list.ToList();
            tempList.Remove(combo);
            list = new Queue<(string Combo, IDisposable Timer)>(tempList);
        }
    }
}

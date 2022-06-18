using System;
using UniRx;

namespace StvDEV.BS.Gameplay.Combo
{
    /// <summary>
    /// Combo trigger.
    /// </summary>
    public class ComboTrigger<T>
    {
        private IDisposable timer;
        private T value;
        private T defaultValue;
        private float resetTime;
        private bool updated;

        /// <summary>
        /// Trigger value.
        /// </summary>
        public T Value
        {
            get
            {
                updated = false;
                return value;
            }
        }

        /// <summary>
        /// Trigger hav unreaded value update.
        /// </summary>
        public bool Updated => updated;

        /// <summary>
        /// Combo trigger.
        /// </summary>
        /// <param name="defaultValue">Default trigger value</param>
        /// <param name="resetTime">Reset value time</param>
        public ComboTrigger(T defaultValue, float resetTime)
        {
            this.defaultValue = defaultValue;
            this.resetTime = resetTime;
        }

        /// <summary>
        /// Trigger value change.
        /// </summary>
        /// <param name="valueFunction">Value function</param>
        public void Trigger(Func<T,T> valueFunction)
        {
            value = valueFunction.Invoke(value);
            updated = true;
            timer?.Dispose();
            timer = Observable.Timer(TimeSpan.FromSeconds(resetTime)).Subscribe(_ =>
            {
                value = defaultValue;
                updated = false;
            });
        }
    }
}

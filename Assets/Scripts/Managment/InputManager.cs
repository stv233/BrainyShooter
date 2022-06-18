using StvDEV.StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.Managment
{
    /// <summary>
    /// Player input manager.
    /// </summary>
    public class InputManager : MonoBehaviourSingleton<InputManager>
    {
        private bool inputLocked;

        /// <summary>
        /// Input is locked.
        /// </summary>
        public bool InputLocked => inputLocked;

        /// <summary>
        /// Lock input.
        /// </summary>
        /// <param name="locked">True - lock/False - Unlock</param>
        public void LockInput(bool locked)
        {
            inputLocked = locked;
        }

        /// <summary>
        /// Lock cursor.
        /// </summary>
        /// <param name="locked">True - lock/False - Unlock</param>
        public void LockCursor(bool locked)
        {
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        }

    }
}

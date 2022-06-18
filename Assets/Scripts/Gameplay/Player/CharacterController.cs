using StvDEV.BS.Managment;
using StvDEV.Inspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.BS.Gameplay.Player
{
    /// <summary>
    /// Implements the control of the character.
    /// </summary>
    public class CharacterController : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private ObjectMove objectMove;
        [SerializeField] private ObjectRotate objectRotate;

        [Header("Settings")]
        [SerializeField] private bool overrideGameData;
        [HideIf("overrideGameData")]
        [SerializeField] private float overrideMoveSpeed;
        [HideIf("overrideGameData")]
        [SerializeField] private float overrideRotationSpeed;

        [Header("Events")]
        [SerializeField] private UnityEvent onMove;
        [SerializeField] private UnityEvent onRotate;

        private float moveSpeed;
        private float rotationSpeed;

        /// <summary>
        /// Occurs when the player moves.
        /// </summary>
        public UnityEvent OnMove => onMove;

        /// <summary>
        /// Occurs when the player turns.
        /// </summary>
        public UnityEvent OnRotate => onRotate;

        private void Awake()
        {
            if (overrideGameData)
            {
                moveSpeed = overrideMoveSpeed;
                rotationSpeed = overrideRotationSpeed;
            }
            else
            {
                moveSpeed = (float)GameManager.Instance.GameData.GetSettingByName("moveSpeed");
                rotationSpeed = (float)GameManager.Instance.GameData.GetSettingByName("rotationSpeed");
            }
        }

        /// <summary>
        /// Move character.
        /// </summary>
        /// <param name="vertical">Vertical input</param>
        /// <param name="horizontal">Horizontal input</param>
        public void Move(float vertical, float horizontal)
        {
            objectMove.Move((objectMove.transform.forward * vertical * moveSpeed) + (objectMove.transform.right * horizontal * moveSpeed));

            if (vertical != 0 || horizontal != 0)
            {
                onMove?.Invoke();
            }
        }

        /// <summary>
        /// Rotatet character.
        /// </summary>
        /// <param name="vertical">Vertical input</param>
        /// <param name="horizontal">Horizontal input</param>
        public void Rotate(float vertical, float horizontal)
        {
            objectRotate.Rotate((objectRotate.transform.forward * vertical * rotationSpeed) + (objectRotate.transform.up * horizontal * rotationSpeed));

            if (vertical != 0 || horizontal != 0)
            {
                onRotate?.Invoke();
            }
        }

        /// <summary>
        /// Rotate character to position.
        /// </summary>
        /// <param name="position">Target position</param>
        public void RotateTo(Vector3 position)
        {
            Vector3 delta = position - transform.position;
            delta = new Vector3(delta.x, 0, delta.z);
            objectRotate.Rotate(Quaternion.LookRotation(delta));
            onRotate?.Invoke();
        }
    }
}

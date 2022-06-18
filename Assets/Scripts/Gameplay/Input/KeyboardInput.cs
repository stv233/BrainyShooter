using StvDEV.BS.Managment;
using UnityEngine;

namespace StvDEV.BS.Gameplay.Input
{
    /// <summary>
    /// Player keyboard Input;
    /// </summary>
    public class KeyboardInput : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private Player.CharacterController characterController;
        [SerializeField] private Player.Shoot shoot;

        private void Update()
        {
            if (InputManager.Instance.InputLocked)
            {
                return;
            }

            characterController.Move(UnityEngine.Input.GetAxis("Vertical"), 0);
            characterController.Rotate(0, UnityEngine.Input.GetAxis("Horizontal"));
            if (UnityEngine.Input.GetAxis("Fire1") > 0)
            {
                shoot.Shot();
            }
        }
    }
}

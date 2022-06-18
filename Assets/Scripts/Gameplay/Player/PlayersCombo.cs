using StvDEV.BS.Gameplay.Combo;
using StvDEV.BS.UI;
using UnityEngine;

namespace StvDEV.BS.Gameplay.Player
{
    /// <summary>
    /// Players combo catcher.
    /// </summary>
    public class PlayersCombo : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Shoot shoot;
        [SerializeField] private ListDisplay display;

        private ComboTrigger<int> shootCombo = new ComboTrigger<int>(0, 1);
        private ComboTrigger<bool> moveCombo = new ComboTrigger<bool>(false, 0.1f);
        private ComboTrigger<bool> rotationCombo = new ComboTrigger<bool> (false, 0.1f);

        private ComboList comboList = new ComboList(5);

        private void Start()
        {
            characterController.OnMove.AddListener(() =>
            {
                moveCombo.Trigger(_ => true);
            });

            characterController.OnRotate.AddListener(() =>
            {
                rotationCombo.Trigger(_ => true);
            });

            shoot.OnShot.AddListener(() =>
            {
                shootCombo.Trigger(x => x + 1);
            });
        }

        private void LateUpdate()
        {
            if (shootCombo.Updated)
            {
                if (shootCombo.Value > 0)
                {
                    if (moveCombo.Value)
                    {
                        comboList.AddCombo("Shot on the move!");
                    }

                    if (rotationCombo.Value)
                    {
                        comboList.AddCombo("Roundabout shot!");
                    }

                    if (shootCombo.Value > 1)
                    {
                        comboList.AddCombo($"Rapid Shot X{shootCombo.Value}!");
                    }
                }
            }

            display.Display(comboList.List);
        }
    }
}

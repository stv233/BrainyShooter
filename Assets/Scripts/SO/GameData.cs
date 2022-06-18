using StvDEV.StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.SO
{
    /// <summary>
    /// Game data storage.
    /// </summary>
    [CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 0)]
    public class GameData : Settings
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float shotDelay;
        [SerializeField] private float shotForce;

    }
}

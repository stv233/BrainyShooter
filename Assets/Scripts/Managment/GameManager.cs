using StvDEV.BS.SO;
using StvDEV.StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.BS.Managment
{
    /// <summary>
    /// Main manager.
    /// </summary>
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("Settings")]
        [SerializeField] private GameData gameData;

        public GameData GameData => gameData;
    }
}

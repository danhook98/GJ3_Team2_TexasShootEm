using System;
using UnityEngine;

namespace TexasShootEm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelLoadSO levelToLoad;

        private bool _gameCanRun = true; 

        private void Awake()
        {
            if (levelToLoad == null)
            {
                Debug.LogWarning("<color=red>Game Manager: </color>No level to load has been assigned.");
                _gameCanRun = false;
                return;
            }

            if (levelToLoad.loadedLevel == null)
            {
                Debug.LogWarning("<color=red>Game Manager: </color>A LevelLoadSO has been assigned, but it's " +
                                 "'loadedLevel' value is null.");
                _gameCanRun = false;
                return; 
            }
        }

        private void LoadLevel()
        {
            if (levelToLoad.loadedLevel.HasAccuracySlider)
            {
                
            }

            if (levelToLoad.loadedLevel.HasKeyPresses)
            {
                
            }
        }
    }
}

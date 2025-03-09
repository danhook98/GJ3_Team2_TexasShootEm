using System;
using TexasShootEm.EventSystem;
using UnityEngine;

namespace TexasShootEm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelLoadSO levelToLoad;

        [Header("Accuracy Slider Events")] 
        [SerializeField] private AccuracySliderDataSOEvent sendSliderData;

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
                sendSliderData.Invoke(levelToLoad.loadedLevel.AccuracySliderData);
            }

            if (levelToLoad.loadedLevel.HasKeyPresses)
            {
                
            }
        }
    }
}

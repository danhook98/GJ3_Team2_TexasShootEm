using System;
using System.Collections;
using TexasShootEm.EventSystem;
using UnityEngine;

namespace TexasShootEm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelLoadSO levelToLoad;

        [Header("Accuracy Slider Events")] 
        [SerializeField] private AccuracySliderDataSOEvent sendSliderData;
        [SerializeField] private BoolEvent showAccuracySliderEvent;

        [Header("Key Press QTE Events")] 
        [SerializeField] private IntEvent sendKeyPressesEvent;
        [SerializeField] private VoidEvent showKeyPressEvent;

        [Header("Timer Events")] 
        [SerializeField] private FloatEvent setTimerEvent;
        [SerializeField] private FloatEvent modifyTimerEvent;
        [SerializeField] private VoidEvent startTimerEvent; 
        [SerializeField] private VoidEvent pauseTimerEvent;

        [Header("Audio")] 
        [SerializeField] private AudioClipSOEvent playSfxEvent;
        [Space] 
        [SerializeField] private AudioClipSO sfxCountdown;

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

        private IEnumerator Start()
        {
            if (!_gameCanRun) yield return null; 
            
            LoadLevel();
            
            playSfxEvent.Invoke(sfxCountdown);
            yield return new WaitForSeconds(3f);
            
            startTimerEvent.Invoke(new Empty());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                showAccuracySliderEvent.Invoke(true);
                showKeyPressEvent.Invoke(new Empty());
            }
        }

        private void LoadLevel()
        {
            Debug.Log("<color=red>Game Manager: </color>Loading level...");
            
            if (levelToLoad.loadedLevel.HasAccuracySlider)
            {
                Debug.Log("Sending slider data");
                sendSliderData.Invoke(levelToLoad.loadedLevel.AccuracySliderData);
            }

            if (levelToLoad.loadedLevel.HasKeyPresses)
            {
                Debug.Log("Sending key press QTE data");
                sendKeyPressesEvent.Invoke(levelToLoad.loadedLevel.KeyPresses); 
            }
            
            setTimerEvent.Invoke(levelToLoad.loadedLevel.LevelTime);
        }

        public void TimeExpired()
        {
            Debug.Log("<color=red>Game Manager: </color>Time expired.");
            // Enemy shoot
            // Player death
        }
    }
}

using System;
using System.Collections;
using TexasShootEm.EventSystem;
using UnityEngine;

namespace TexasShootEm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelLoadSO levelToLoad;
        
        [Header("Core Events")]
        [SerializeField] private VoidEvent gameWonEvent;

        [Header("Canvas Events")] 
        [SerializeField] private VoidEvent showWinCanvasEvent;

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

        private bool _displaySlider = false;
        private bool _sliderWasDisplayed = false;
        private bool _displayQTE = false;
        private bool _QTEWasDisplayed = false;

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
            Debug.Log("<color=red>Game Manager: </color>Loading level...");
            
            if (levelToLoad.loadedLevel.HasAccuracySlider)
            {
                Debug.Log("Sending slider data");
                sendSliderData.Invoke(levelToLoad.loadedLevel.AccuracySliderData);
                _displaySlider = true;
            }

            if (levelToLoad.loadedLevel.HasKeyPresses)
            {
                Debug.Log("Sending key press QTE data");
                sendKeyPressesEvent.Invoke(levelToLoad.loadedLevel.KeyPresses);
                _displayQTE = true;
            }
            
            setTimerEvent.Invoke(levelToLoad.loadedLevel.LevelTime);
        }

        private IEnumerator Start()
        {
            if (!_gameCanRun) yield return null; 
            
            LoadLevel();
            
            playSfxEvent.Invoke(sfxCountdown);
            yield return new WaitForSeconds(3f);
            
            startTimerEvent.Invoke(new Empty());

            // Initial interactable display. 
            if ((_displaySlider && !_displayQTE) || (_displaySlider && _displayQTE))
            {
                DisplaySlider();
            }
            else if (!_displaySlider && _displayQTE)
            {
                DisplayQTE();
            }
        }

        private IEnumerator PlayerWon()
        {
            // Stop the timer.
            pauseTimerEvent.Invoke(new Empty());
            
            // Broadcast the game won event. 
            gameWonEvent.Invoke(new Empty());
            
            // Small delay so that the player can see the animations play before the canvas appears
            yield return new WaitForSeconds(3f);
            showWinCanvasEvent.Invoke(new Empty());
        }

        private void DisplaySlider()
        {
            showAccuracySliderEvent.Invoke(true);
            _sliderWasDisplayed = true;
        }

        private void DisplayQTE()
        {
            showKeyPressEvent.Invoke(new Empty());
            _QTEWasDisplayed = true;
        }

        // Slider score = 0f - 1f in 0.25f increments for each zone.
        public void PassSliderScore(float score)
        {
            // do whatever with the score.

            if (_displayQTE)
            {
                DisplayQTE();
                return; 
            }
            
            StartCoroutine(PlayerWon());
        }

        public void PassKeyPressScore(float score)
        {
            // do whatever with the score.
            StartCoroutine(PlayerWon());
        }

        public void TimeExpired()
        {
            Debug.Log("<color=red>Game Manager: </color>Time expired.");
            // Enemy shoot
            // Player death
        }
    }
}

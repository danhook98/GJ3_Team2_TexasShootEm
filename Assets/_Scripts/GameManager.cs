using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TexasShootEm.EventSystem;
using TexasShootEm.Level;

namespace TexasShootEm
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelLoadSO levelToLoad;
        
        [Header("Core Events")]
        [SerializeField] private VoidEvent gameWonEvent;

        [Header("Canvas Events")] 
        [SerializeField] private VoidEvent showWinCanvasEvent;
        [SerializeField] private IntEvent showMedalsEvent;

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

        private float _qteScore;
        private float _sliderScore;
        private int _scoreTrack = 1;

        private void Awake()
        {
            Time.timeScale = 1f;
            
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
            
            float finalScore = (_sliderScore + _qteScore)/_scoreTrack;
            int numMedals = 0;

            switch (finalScore) // Pass how many medals should be shown based on score.
            {
                case < 0.2f:
                    break;
                case < 0.4f:
                    numMedals = 1;
                    break;
                case < 0.85f:
                    numMedals = 2;
                    break;
                case <= 1f:
                    numMedals = 3;
                    break;
            }
            
            // < 0.2 = No Medals < 0.4 = Bronze, < 0.85 = Silver, < 1 = Gold
            showMedalsEvent.Invoke(numMedals);
            
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
            _sliderScore = score;
            
            if (_displayQTE)
            {
                _scoreTrack++;
                DisplayQTE();
                return; 
            }
            
            StartCoroutine(PlayerWon());
        }

        public void PassKeyPressScore(float score)
        {
            _qteScore = score;
            // do whatever with the score.
            StartCoroutine(PlayerWon());
        }

        public void TimeExpired()
        {
            Debug.Log("<color=red>Game Manager: </color>Time expired.");
            // Enemy shoot
            // Player death
        }

        private void UnlockNextLevel(ref LevelSO nextLevel)
        {
            nextLevel.Unlocked = true;
            
            // Set the level as '1' (true) in PlayerPrefs.
            PlayerPrefs.SetInt(nextLevel.name, 1);
        }

        public void LoadNextLevel()
        {
            ref LevelSO nextLevelSO = ref levelToLoad.loadedLevel.NextLevel;

            if (!nextLevelSO) return; 
            
            UnlockNextLevel(ref nextLevelSO);
            
            levelToLoad.loadedLevel = nextLevelSO;
            SceneManager.LoadScene("MainGame");
        }
    }
}

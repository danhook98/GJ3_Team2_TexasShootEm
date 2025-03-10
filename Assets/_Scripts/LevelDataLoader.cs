using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace TexasShootEm
{
    public class LevelDataLoader : MonoBehaviour
    {
        [SerializeField] private LevelSO levelSO;
        [SerializeField] private LevelLoadSO levelLoadSO;
        
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI difficultyText;
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI buttonText;

        private void Awake()
        {
            if (!levelSO) return;
            
            titleText.text = $"Level {levelSO.LevelNumber}";
            difficultyText.text = levelSO.Difficulty;

            // First level is always unlocked.
            if (levelSO.LevelNumber == 1) return;
            
            UpdatePlayButton();
        }   

        private void UpdatePlayButton()
        {
            levelSO.Unlocked = PlayerPrefs.GetInt(levelSO.name, 0) == 1;
            playButton.interactable = levelSO.Unlocked;
            buttonText.text = levelSO.Unlocked ? "Play" : "Locked";
        }

        public void Play()
        {
            levelLoadSO.loadedLevel = levelSO;
            Debug.Log($"Loading level {levelSO.LevelNumber}");
            SceneManager.LoadScene("MainGame");
        }
    }
}

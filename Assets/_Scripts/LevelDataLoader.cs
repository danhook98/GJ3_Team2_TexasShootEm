using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class LevelDataLoader : MonoBehaviour
    {
        [SerializeField] private LevelSO levelSO;
        [SerializeField] private LevelLoadSO levelLoadSO;
        [Space] 
        [SerializeField] private AudioClipSO clickSound;
        
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI difficultyText;
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI buttonText;

        private void Awake()
        {
            titleText.text = $"Level {levelSO.LevelNumber}";
            difficultyText.text = levelSO.Difficulty;

            UpdatePlayButton();
        }

        private void UpdatePlayButton()
        {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TexasShootEm
{
    public class PauseMenuUI : MonoBehaviour
    {
        public void RestartCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        public void PauseGame(bool isPaused) => Time.timeScale = isPaused ? 0 : 1;
    }
}

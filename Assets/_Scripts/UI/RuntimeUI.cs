using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TexasShootEm
{
    public class RuntimeUI : MonoBehaviour
    {
        public void RestartCurrentLevel()
        {
            Debug.Log("Restarting current level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void GoToMainMenu()
        {
            Debug.Log("Going to main menu");
            SceneManager.LoadScene("Main Menu");
        }
    }
}

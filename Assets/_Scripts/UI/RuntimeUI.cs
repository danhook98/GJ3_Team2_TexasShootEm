using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class RuntimeUI : MonoBehaviour
    {
        [SerializeField] private Sprite blankMedal;
        [SerializeField] private Image[] medals;
        
        public void RestartCurrentLevel()
        {
            Debug.Log("Restarting current level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void GoToMainMenu()
        {
            Debug.Log("Going to main menu");
            SceneManager.LoadScene("MainMenu");
        }

        public void SetToBlankMedal(int numMedals)
        {
            for (int i = medals.Length - 1; i >= 0; i--)
            {
                if (i >= numMedals)
                {
                    medals[i].sprite = blankMedal;
                }
                
            }
        }
    }
}

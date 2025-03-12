using UnityEngine;
using UnityEngine.SceneManagement;

namespace TexasShootEm.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private Canvas pauseCanvas;
        
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
        
        public void PauseGame(bool isPaused)
        {
            Debug.Log(isPaused);
            pauseCanvas.enabled = isPaused;
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}

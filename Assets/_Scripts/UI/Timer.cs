using UnityEngine;
using TexasShootEm.EventSystem;
using TMPro;

namespace TexasShootEm
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private VoidEvent onGameOverEvent;

        private float _timer = 30f; // TODO: Set this to 0 after testing.
        private TextMeshProUGUI _timerText;
        private bool _isCountingDown;
        private bool _hasTimerExpired;
        
        void Start()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
            _hasTimerExpired = false;
            _isCountingDown = false;
        }
        
        void Update()
        {
            // Display timer
            _timerText.text = _timer.ToString("00");
            if (_isCountingDown)
            {
                _timer -= Time.deltaTime;
            }
            
            // IF timer reaches 0, invoke GameOver game event.
            if (_timer <= 0)
            {
                _hasTimerExpired = true;
                if (_hasTimerExpired)
                {
                    onGameOverEvent.Invoke(new Empty());
                }
            }
            
            // TODO: Remove these, they only exist for testing purposes.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Starting Timer");
                StartTimer();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Pausing Timer");
                PauseTimer();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Increasing Timer by 2s");
                ModifyTimer(2f);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Decreasing Timer by 2s");
                ModifyTimer(-2f);
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                Debug.Log("Setting timer to 20s");
                SetTimeLimit(20f);
            }
        }

        public void SetTimeLimit(float time)
        {
            _timer = time;
        }

        public void StartTimer()
        {
            _isCountingDown = true;
        }

        public void PauseTimer()
        {
            _isCountingDown = false;
        }

        public void ModifyTimer(float time)
        {
            _timer += time;
        }
    }
}

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
        
        void Awake()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
            _hasTimerExpired = false;
            _isCountingDown = false;
        }
        
        void Update()
        {
            if (!_isCountingDown) return;
            
            // Display timer
            _timerText.text = _timer.ToString("00");
            if (_isCountingDown)
            {
                _timer -= Time.deltaTime;
            }
            
            // IF timer reaches 0, invoke GameOver game event.
            if (_timer <= 0)
            {
                _isCountingDown = false;
                if (_hasTimerExpired)
                {
                    onGameOverEvent.Invoke(new Empty());
                }
            }
            
            if (_timer > 0)
            {
                _hasTimerExpired = false;
            }
        }

        public void SetTimeLimit(float time) => _timer = time;

        public void StartTimer() => _isCountingDown = true;

        public void PauseTimer() => _isCountingDown = false;

        public void ModifyTimer(float time) => _timer += time;
    }
}

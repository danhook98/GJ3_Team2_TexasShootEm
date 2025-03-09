using UnityEngine;
using TexasShootEm.EventSystem;
using TMPro;
using UnityEngine.Serialization;

namespace TexasShootEm
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private VoidEvent timeExpiredEvent;

        private float _timer = 30f; // TODO: Set this to 0 after testing.
        private TextMeshProUGUI _timerText;
        private bool _isCountingDown;
        
        private void Awake()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
            _isCountingDown = false;
        }
        
        private void Update()
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
                
                timeExpiredEvent.Invoke(new Empty());
            }
        }

        public void SetTimeLimit(float time)
        {
            _timer = time;
            _timerText.text = _timer.ToString("00");
        }

        public void StartTimer() => _isCountingDown = true;

        public void PauseTimer() => _isCountingDown = false;

        public void ModifyTimer(float time) => _timer += time;
    }
}

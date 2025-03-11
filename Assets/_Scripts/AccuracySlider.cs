using TexasShootEm.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class AccuracySlider : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader; 
        [SerializeField] private Slider accuracySlider;
        [SerializeField] private AnimationCurve lerpCurve;
        
        [Header("Events")]
        [SerializeField] private FloatEvent sendScoreEvent;
        
        [Header("Slider Zones")]
        [SerializeField] private RectTransform[] sliderZones;
    
        [SerializeField] private float difficultyMultiplier = 0.25f;
        
        private AccuracySliderDataSO _accuracySliderData;
    
        private bool _sliderActive = false;
        private float _accuracyScore;
        private float _valueChange; 
        private bool _isSliderPaused;
        private float _sliderScore;
        private float[] _zoneScoring; 
        
        private void Awake()
        {
            _isSliderPaused = false;
            _zoneScoring = new float[4];
        }

        private void OnEnable() => inputReader.OnAimEvent += GetSliderValue;
        private void OnDisable() => inputReader.OnAimEvent -= GetSliderValue;

        private void Update()
        {
            if (_isSliderPaused || !_sliderActive) return;
            
            CalculateValueChange();
        }

        private void GetSliderValue()
        {
            // Prevent user input from doing anything if the slider isn't active. 
            if (!_sliderActive) return; 
            
            _isSliderPaused = true;
            
            float absoluteValue = Mathf.Abs(accuracySlider.value);
        
            _accuracyScore = 1 - absoluteValue;
            
            // 0 = Bad, 1 = Okay, 2 = Good, 3 = Perfect
            // Each of these hold their range value, if accuracy score is less than the range value of these, it's
            // the value of the one below
            for (int i = _zoneScoring.Length - 1; i >= 0; i--)
            {
                if (_accuracyScore > _zoneScoring[i])
                {
                    _sliderScore = 0.25f * (i + 1);
                    break;
                }
            }
            
            sendScoreEvent.Invoke(_sliderScore); 
            _sliderActive = false;
        }

        private void CalculateValueChange()
        {
            float pingPongValue = Mathf.PingPong(Time.time * difficultyMultiplier, accuracySlider.maxValue + 1) -1;
            _valueChange = lerpCurve.Evaluate(pingPongValue);
            accuracySlider.value = _valueChange;
        }
        
        private void SetSliderZones()
        {
            for (int i = 0; i < sliderZones.Length; i++)
            {
                float rangeStart = _accuracySliderData.sliderData[i].RangeStart;
                float xScale = 1 - rangeStart;
                sliderZones[i].localScale = new Vector3(xScale, 1f, 1f);
                
                _zoneScoring[i] = rangeStart;
            }
        }
        
        public void LoadData(AccuracySliderDataSO sliderData)
        {
            Debug.Log("Loading slider data");
            _accuracySliderData = sliderData;
            SetSliderZones();
            difficultyMultiplier = sliderData.PointerSpeed;
        }
        
        public void DisplaySlider(bool state) => _sliderActive = state;
    }
}
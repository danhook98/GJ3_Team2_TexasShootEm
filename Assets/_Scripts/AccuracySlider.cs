using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class AccuracySlider : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader; 
        [SerializeField] private Slider accuracySlider;
        [SerializeField] private AnimationCurve lerpCurve;
    
        [SerializeField] private float difficultyMultiplier = 0.25f;
    
        private float _accuracyScore;
        private float _accuracySliderValue;
        private float _valueChange; 
        private float _sliderSpeed;
        private bool _isSliderPaused;
    
        private Dictionary<string, float> scoreResults;
        private float _perfectScoreRange = 0.9f;
        private float _goodScoreRange = 0.6f;
        private float _okayScoreRange = 0.3f;
        private float _badScoreRange;
        private float _missScoreRange;

        private void Awake()
        {
            scoreResults = new Dictionary<string, float>();
            _isSliderPaused = false;
            AddNewScoreResults();
        }

        private void OnEnable() => inputReader.OnAimEvent += GetSliderValue;
        private void OnDisable() => inputReader.OnAimEvent -= GetSliderValue;

        private void Update()
        {
            if (!_isSliderPaused)
            {
                CalculateValueChange();
            }

            // For Testing
            if (Input.GetKeyDown(KeyCode.T))
            {
                ResetAccuracySlider();
            }
        }

        private void GetSliderValue()
        {
            // For scoring, score ranges from 0 to 1, closer to the middle of the bar closer to a value of 1.
            _isSliderPaused = true;
        
            // Calculate score when slider value is above or below the halfway mark.
            if (accuracySlider.value > 0.5f)
            {
                _accuracySliderValue = accuracySlider.value;
                _accuracySliderValue -= 0.5f;
                _accuracySliderValue = 1 - (2 * _accuracySliderValue);
            }

            if (accuracySlider.value < 0.5f)
            {
                _accuracySliderValue = accuracySlider.value;
                _accuracySliderValue = 2 * _accuracySliderValue;
            }
        
            _accuracyScore = _accuracySliderValue;
            Debug.Log(_accuracyScore);
        }

        private void CalculateValueChange()
        {
            float pingPongValue = Mathf.PingPong(Time.time * difficultyMultiplier, accuracySlider.maxValue);
            _valueChange = lerpCurve.Evaluate(pingPongValue);
            accuracySlider.value = _valueChange;
        }

        private void AddNewScoreResults()
        {
            scoreResults.Add("Perfect!", _perfectScoreRange);
            scoreResults.Add("Good!", _goodScoreRange);
            scoreResults.Add("Okay!", _okayScoreRange);
            scoreResults.Add("Bad!", _badScoreRange);
            scoreResults.Add("Miss!", _missScoreRange);
        }
        
        public void SetDifficulty(float newMultiplier) => difficultyMultiplier = newMultiplier;
        public void ResetAccuracySlider() => _isSliderPaused = false;
    }
}
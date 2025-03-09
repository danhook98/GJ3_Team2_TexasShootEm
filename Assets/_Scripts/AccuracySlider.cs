using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace TexasShootEm
{
    public class AccuracySlider : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader; 
        [SerializeField] private Slider accuracySlider;
        [SerializeField] private AnimationCurve lerpCurve;
        
        [Header("Slider Zones")]
        [SerializeField] private RectTransform[] sliderZones;
    
        [SerializeField] private float difficultyMultiplier = 0.25f;
        
        private AccuracySliderDataSO _accuracySliderData;
    
        private bool _sliderActive = false;
        
        private float _accuracyScore;
        private float _valueChange; 
        private bool _isSliderPaused;
    
        // These score ranges need to be overwritten in the LoadData method down below.
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
            if (_isSliderPaused || !_sliderActive) return;
            
            CalculateValueChange();
        }

        private void GetSliderValue()
        {
            // Prevent user input from doing anything if the slider isn't active. 
            if (!_sliderActive) return; 
            
            // For scoring, score ranges from 0 to 1, closer to the middle of the bar closer to a value of 1.
            _isSliderPaused = true;
            
            float absoluteValue = Mathf.Abs(accuracySlider.value);
        
            _accuracyScore = 1 - absoluteValue;
            Debug.Log(_accuracyScore);
        }

        private void CalculateValueChange()
        {
            float pingPongValue = Mathf.PingPong(Time.time * difficultyMultiplier, accuracySlider.maxValue + 1) -1;
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

        private void SetSliderZones()
        {
            for (int i = 0; i < sliderZones.Length; i++)
            {
                float xScale = 1 - _accuracySliderData.sliderData[i].RangeStart;
                sliderZones[i].localScale = new Vector3(xScale, 1f, 1f);
            }
        }

        public void LoadData(AccuracySliderDataSO sliderData)
        {
            _accuracySliderData = sliderData;
            Debug.Log("Loading slider data");
            SetSliderZones();
        }
        
        // Redundant for now, let's wait and see...
        // public void SetDifficulty(float newMultiplier) => difficultyMultiplier = newMultiplier;
        // public void ResetAccuracySlider() => _isSliderPaused = false;
    }
}
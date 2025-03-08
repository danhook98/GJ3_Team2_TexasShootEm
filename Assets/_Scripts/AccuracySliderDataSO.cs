using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Accuracy Slider Data", fileName = "AccuracySliderData")]
    public class AccuracySliderDataSO : ScriptableObject
    {
        public SliderData Bad;
        public SliderData Okay;
        public SliderData Good;
        public SliderData Perfect;
    }
    
    [System.Serializable]
    public struct SliderData
    {
        public int RangeStart; 
        public int TimePenalty;
    }
}

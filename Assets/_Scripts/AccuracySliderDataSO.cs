using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Accuracy Slider Data", fileName = "AccuracySliderData")]
    public class AccuracySliderDataSO : ScriptableObject
    {
        public SliderData[] sliderData = new SliderData[4];
        
        // public SliderData Bad;
        // public SliderData Okay;
        // public SliderData Good;
        // public SliderData Perfect;
    }
    
    [System.Serializable]
    public struct SliderData
    {
        public float RangeStart; 
        public int TimePenalty;
    }
}

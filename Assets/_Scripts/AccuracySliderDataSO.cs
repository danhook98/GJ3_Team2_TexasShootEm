using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Accuracy Slider Data", fileName = "AccuracySliderData")]
    public class AccuracySliderDataSO : ScriptableObject
    {
        [Header("0 = Bad, 1 = Okay, 2 = Good, 3 = Perfect")]
        public SliderData[] sliderData = new SliderData[4];

        [Range(0.25f, 6f)]
        public float PointerSpeed = 0.25f;
    }
    
    [System.Serializable]
    public struct SliderData
    {
        public float RangeStart; 
        public int TimePenalty;
    }
}

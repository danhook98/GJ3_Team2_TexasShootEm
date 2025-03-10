using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Level", fileName = "Level")]
    public class LevelSO : ScriptableObject
    {
        public int LevelNumber;

        [HideInInspector]
        public string Difficulty;
        
        public bool Unlocked = false;

        [Space] 
        public int LevelTime = 30;
        [Space]
        
        public bool HasAccuracySlider;
        public AccuracySliderDataSO AccuracySliderData;
        [Space]
        public bool HasKeyPresses;
        public int KeyPresses = 0;
        [Space]
        
        public LevelSO NextLevel = default;
    }
}

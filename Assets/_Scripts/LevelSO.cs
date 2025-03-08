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
    }
}

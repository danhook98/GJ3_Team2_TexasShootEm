using UnityEngine;

namespace TexasShootEm
{
    [CreateAssetMenu(menuName = "TexasShootEm/Level Load SO")]
    public class LevelLoadSO : ScriptableObject
    {
        [HideInInspector]
        public LevelSO loadedLevel; 
    }
}

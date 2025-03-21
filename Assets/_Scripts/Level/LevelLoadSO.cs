using UnityEngine;

namespace TexasShootEm.Level
{
    // Uncomment the below line if the instance of LevelLoadSO gets deleted. 
    //[CreateAssetMenu(menuName = "TexasShootEm/Level Load SO")]
    public class LevelLoadSO : ScriptableObject
    {
        [HideInInspector]
        public LevelSO loadedLevel; 
    }
}

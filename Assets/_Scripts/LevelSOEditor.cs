using System;
using UnityEditor;

namespace TexasShootEm
{
    [CustomEditor(typeof(LevelSO))]
    public class LevelSOEditor : Editor
    {        
        private SerializedProperty _difficultyProperty;
        private string[] difficultyList = { "Beginner", "Intermediate", "Expert" };
        private int selectedIndex = 0;
        
        private void OnEnable()
        {
            // Find the difficulty property from the LevelSO data.
            _difficultyProperty = serializedObject.FindProperty("Difficulty");
            
            // Ensure that the index of the dropdown is the same as what's saved in the LevelSO data.
            selectedIndex = Array.IndexOf(difficultyList, _difficultyProperty.stringValue);
        }

        public override void OnInspectorGUI()
        {
            // Display the base inspector GUI.
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            // Display the dropdown of difficulties.
            selectedIndex = EditorGUILayout.Popup("Difficulty", selectedIndex, difficultyList);
            
            // Update the difficulty property string text with what was selected.
            _difficultyProperty.stringValue = difficultyList[selectedIndex];

            serializedObject.ApplyModifiedProperties();
        }
    }
}
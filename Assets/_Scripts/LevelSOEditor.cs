using System;
using UnityEditor;
using UnityEngine;

namespace TexasShootEm
{
    [CustomEditor(typeof(LevelSO))]
    public class LevelSOEditor : Editor
    {
        private SerializedProperty _levelNumber;
        private SerializedProperty _levelUnlocked;
        private SerializedProperty _difficultyProperty;
        private SerializedProperty _accuracySliderData;
        private SerializedProperty _keyPresses;
        
        private string[] difficultyList = { "Beginner", "Intermediate", "Expert" };
        private int selectedIndex = 0;
        
        private void OnEnable()
        {
            _levelNumber = serializedObject.FindProperty("LevelNumber");
            _levelUnlocked = serializedObject.FindProperty("Unlocked");
            _difficultyProperty = serializedObject.FindProperty("Difficulty");
            _accuracySliderData = serializedObject.FindProperty("AccuracySliderData");
            _keyPresses = serializedObject.FindProperty("KeyPresses");
            
            // Ensure that the index of the dropdown is the same as what's saved in the LevelSO data.
            selectedIndex = Array.IndexOf(difficultyList, _difficultyProperty.stringValue);
            
            if (selectedIndex == -1) selectedIndex = 0;
        }

        public override void OnInspectorGUI()
        {
            // Display the base inspector GUI.
            //base.OnInspectorGUI();
            
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_levelNumber);
            EditorGUILayout.PropertyField(_levelUnlocked);
            
            // Display the dropdown of difficulties.
            selectedIndex = EditorGUILayout.Popup("Difficulty", selectedIndex, difficultyList);
            
            // Update the difficulty property string text with what was selected.
            _difficultyProperty.stringValue = difficultyList[selectedIndex];

            EditorGUILayout.PropertyField(_accuracySliderData);
            EditorGUILayout.PropertyField(_keyPresses);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
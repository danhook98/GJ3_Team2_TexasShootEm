using System;
using UnityEditor;

namespace TexasShootEm
{
    [CustomEditor(typeof(LevelSO))]
    public class LevelSOEditor : Editor
    {
        private SerializedProperty _levelNumber;
        private SerializedProperty _levelUnlocked;
        private SerializedProperty _difficultyProperty;
        private SerializedProperty _levelTimeProperty;
        private SerializedProperty _hasAccuracySliderProperty;
        private SerializedProperty _accuracySliderData;
        private SerializedProperty _hasKeyPressesProperty;
        private SerializedProperty _keyPresses;
        private SerializedProperty _nextLevelProperty;
        
        private string[] difficultyList = { "Beginner", "Intermediate", "Expert" };
        private int selectedIndex = 0;
        
        private void OnEnable()
        {
            _levelNumber = serializedObject.FindProperty("LevelNumber");
            _levelUnlocked = serializedObject.FindProperty("Unlocked");
            _difficultyProperty = serializedObject.FindProperty("Difficulty");
            _levelTimeProperty = serializedObject.FindProperty("LevelTime");
            _hasAccuracySliderProperty = serializedObject.FindProperty("HasAccuracySlider");
            _accuracySliderData = serializedObject.FindProperty("AccuracySliderData");
            _hasKeyPressesProperty = serializedObject.FindProperty("HasKeyPresses");
            _keyPresses = serializedObject.FindProperty("KeyPresses");
            _nextLevelProperty = serializedObject.FindProperty("NextLevel");
            
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

            EditorGUILayout.PropertyField(_levelTimeProperty);
            
            EditorGUILayout.PropertyField(_hasAccuracySliderProperty);
            EditorGUILayout.PropertyField(_accuracySliderData);
            
            EditorGUILayout.PropertyField(_hasKeyPressesProperty);
            EditorGUILayout.PropertyField(_keyPresses);
            
            EditorGUILayout.PropertyField(_nextLevelProperty);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
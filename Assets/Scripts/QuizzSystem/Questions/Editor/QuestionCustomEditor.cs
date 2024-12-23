using UnityEditor;
using UnityEngine;

namespace QuizzSystem.Questions.Editor
{
    [CustomEditor(typeof(Question), true)]
    public class QuestionCustomEditor : UnityEditor.Editor
    {
        private SerializedProperty _title;
        private SerializedProperty _answer;
        private SerializedProperty _choices;
        private SerializedProperty _background;
        private SerializedProperty _category;
        private SerializedProperty _buttonDisplay;
        private SerializedProperty _buttonSprite;
        
        protected virtual void OnEnable()
        {
            _title = serializedObject.FindProperty("title");
            _answer = serializedObject.FindProperty("answer");
            _choices = serializedObject.FindProperty("choices");
            _background = serializedObject.FindProperty("background");
            _category = serializedObject.FindProperty("category");
            _buttonDisplay = serializedObject.FindProperty("buttonDisplay");
            _buttonSprite = serializedObject.FindProperty("buttonSprite");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_title);
            EditorGUILayout.PropertyField(_category);
            EditorGUILayout.PropertyField(_choices);
            
            EditorGUILayout.LabelField("Button");
            EditorGUILayout.PropertyField(_buttonDisplay);
            if (_buttonDisplay.enumValueIndex == 1)
            {
                EditorGUILayout.PropertyField(_buttonSprite);
            }
            
            DisplayOtherParameters();
            
            EditorGUILayout.PropertyField(_answer);
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Optional", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_background);
            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DisplayOtherParameters()
        {
            
        }
    }
}
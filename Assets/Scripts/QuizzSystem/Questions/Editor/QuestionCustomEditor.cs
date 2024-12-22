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
        
        protected virtual void OnEnable()
        {
            _title = serializedObject.FindProperty("title");
            _answer = serializedObject.FindProperty("answer");
            _choices = serializedObject.FindProperty("choices");
            _background = serializedObject.FindProperty("background");
            _category = serializedObject.FindProperty("category");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_title);
            EditorGUILayout.PropertyField(_category);
            EditorGUILayout.PropertyField(_choices);
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
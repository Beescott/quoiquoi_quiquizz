using QuizzSystem.Answers;
using UnityEditor;
using UnityEngine;

namespace QuizzSystem.Questions.Editor
{
    [CustomPropertyDrawer(typeof(Answer))]
    public class AnswerEditorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var type = property.FindPropertyRelative(nameof(Answer.type));
            var singleTextAnswer = property.FindPropertyRelative(nameof(Answer.singleTextAnswer));
            var multipleChoiceIndexAnswer = property.FindPropertyRelative(nameof(Answer.multipleChoiceIndexAnswer));
            var spriteAnswer = property.FindPropertyRelative(nameof(Answer.spriteAnswer));
            
            EditorGUILayout.LabelField("Answer", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(type);
            AnswerType answerType = (AnswerType)type.enumValueFlag;
            if (answerType.HasFlag(AnswerType.SingleText))
                EditorGUILayout.PropertyField(singleTextAnswer);
            if (answerType.HasFlag(AnswerType.MultipleChoice))
                EditorGUILayout.PropertyField(multipleChoiceIndexAnswer);
            if (answerType.HasFlag(AnswerType.Image))
                EditorGUILayout.PropertyField(spriteAnswer);
        }
    }
}
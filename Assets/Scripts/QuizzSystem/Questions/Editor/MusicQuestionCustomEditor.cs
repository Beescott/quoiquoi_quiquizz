using UnityEditor;

namespace QuizzSystem.Questions.Editor
{
    [CustomEditor(typeof(MusicQuestion))]
    public class MusicQuestionCustomEditor : QuestionCustomEditor
    {
        private SerializedProperty _audioClip;

        protected override void OnEnable()
        {
            base.OnEnable();
            _audioClip = serializedObject.FindProperty("audioClip");
        }

        protected override void DisplayOtherParameters()
        {
            EditorGUILayout.PropertyField(_audioClip);
            
        }
    }
}
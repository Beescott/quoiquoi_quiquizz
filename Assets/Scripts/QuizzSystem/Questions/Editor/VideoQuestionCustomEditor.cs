using UnityEditor;

namespace QuizzSystem.Questions.Editor
{
    [CustomEditor(typeof(VideoQuestion))]
    public class VideoQuestionCustomEditor : QuestionCustomEditor
    {
        private SerializedProperty _videoClip;

        protected override void OnEnable()
        {
            base.OnEnable();
            _videoClip = serializedObject.FindProperty("videoClipName");
        }

        protected override void DisplayOtherParameters()
        {
            EditorGUILayout.PropertyField(_videoClip);
            
        }
    }
}
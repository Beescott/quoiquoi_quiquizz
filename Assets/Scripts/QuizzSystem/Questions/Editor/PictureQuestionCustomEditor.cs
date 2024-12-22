using UnityEditor;

namespace QuizzSystem.Questions.Editor
{
    [CustomEditor(typeof(PictureQuestion))]
    public class PictureQuestionCustomEditor : QuestionCustomEditor
    {
        private SerializedProperty _picture;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _picture = serializedObject.FindProperty("picture");
        }

        protected override void DisplayOtherParameters()
        {
            EditorGUILayout.PropertyField(_picture);
        }
    }
}
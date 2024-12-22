using QuizzSystem.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem.UI
{
    public class QuestionButton : MonoBehaviour
    {
        private const string PREFIX = "Question";
        
        [SerializeField] private Question question;
        [SerializeField, HideInInspector] private TMP_Text buttonIndex;
        [SerializeField, HideInInspector] private Button button;
        [SerializeField, HideInInspector] private Image image;

        public Question Question => question;
        
        public void AssignQuestion(Question q)
        {
            question = q;
        }
        
        private void OnButtonClick()
        {
            EventBus<QuestionClickedEvent>.Raise(new QuestionClickedEvent()
            {
                Index = transform.GetSiblingIndex(),
                Question = question
            });

            image.color = Color.grey;
            button.onClick.RemoveAllListeners();
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
            if (question == null)
                return;

            bool foundColor = CategoryColorAssigner.Instance.TryGetColorFromCategory(question.Category, out Color color);
            if (foundColor == false)
                return;

            image.color = color;
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnValidate()
        {
            buttonIndex ??= GetComponentInChildren<TMP_Text>();
            button ??= GetComponentInChildren<Button>();
            image ??= GetComponentInChildren<Image>();
            
            int siblingIndex = transform.GetSiblingIndex();
            buttonIndex.text = siblingIndex.ToString();
            
            string questionName = question == null ? "Empty" : question.name;
            gameObject.name = $"{PREFIX}_{siblingIndex.ToString()} ({questionName})";
        }
    }
}
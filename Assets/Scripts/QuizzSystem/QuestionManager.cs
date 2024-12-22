using DG.Tweening;
using QuizzSystem.Events;
using QuizzSystem.Questions;
using QuizzSystem.UI;
using QuizzSystem.UI.QuestionDisplayers;
using UnityEngine;

namespace QuizzSystem
{
    public class QuestionManager : MonoBehaviour
    {
        [SerializeField] private Transform questionsParent;
        [SerializeField] private TextQuestionDisplayer textQuestionDisplayer;
        [SerializeField] private PictureQuestionDisplayer pictureQuestionDisplayer;
        [SerializeField] private MusicQuestionDisplayer musicQuestionDisplayer;
        
        private EventBinding<QuestionClickedEvent> _questionClickedBinding;
        private EventBinding<QuestionQuitEvent> _questionQuitBinding;

        private IQuestionDisplayer _activeQuestionDisplayer;
        
        private void OnButtonClicked(QuestionClickedEvent questionClickedParam)
        {
            IQuestionDisplayer questionDisplayer;
            switch (questionClickedParam.Question)
            {
                case PictureQuestion:
                    questionDisplayer = pictureQuestionDisplayer;
                    break;
                case MusicQuestion:
                    questionDisplayer = musicQuestionDisplayer;
                    break;
                default:
                    questionDisplayer = textQuestionDisplayer;
                    break;
            }

            _activeQuestionDisplayer = questionDisplayer;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(questionsParent.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            sequence.AppendCallback(() => questionDisplayer.DisplayQuestion(questionClickedParam.Question));
        }

        private void OnQuestionQuit(QuestionQuitEvent quitEvent)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_activeQuestionDisplayer.GameObject.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            sequence.AppendCallback(() => _activeQuestionDisplayer.GameObject.SetActive(false));
            sequence.Append(questionsParent.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack));
        }

        private void OnEnable()
        {
            _questionClickedBinding = new(OnButtonClicked);
            EventBus<QuestionClickedEvent>.Register(_questionClickedBinding);

            _questionQuitBinding = new(OnQuestionQuit);
            EventBus<QuestionQuitEvent>.Register(_questionQuitBinding);
        }

        private void OnDisable()
        {
            EventBus<QuestionClickedEvent>.Deregister(_questionClickedBinding);
            EventBus<QuestionQuitEvent>.Deregister(_questionQuitBinding);
        }
    }
}
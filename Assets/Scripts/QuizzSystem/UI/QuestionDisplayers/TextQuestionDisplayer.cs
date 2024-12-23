using System.Collections.Generic;
using DG.Tweening;
using QuizzSystem.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem.UI.QuestionDisplayers
{
    public class TextQuestionDisplayer : MonoBehaviour, IQuestionDisplayer
    {
        [Header("Question params")]
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private GameObject choiceParent;
        [SerializeField] private List<ChoiceDisplayer> choiceTexts;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Button showAnswerButton;
        [SerializeField] protected RectTransform questionLayout;

        [Header("Answer params")]
        [SerializeField] private GameObject answerParent;
        [SerializeField] private Image answerImage;
        [SerializeField] private TMP_Text singleLineText;
        [SerializeField] private Button quitButton;
        
        public GameObject GameObject => gameObject;
        
        private Question _question;
        
        public virtual void DisplayQuestion(Question question)
        {
            _question = question;
            titleText.text = question.Title;

            if (question.Background != null)
                backgroundImage.sprite = question.Background;
            
            DisplayChoices(question);
            answerParent.SetActive(false);
            singleLineText.gameObject.SetActive(false);
            answerImage.gameObject.SetActive(false);
            gameObject.SetActive(true);
            AnimateOpening();
            
            quitButton.gameObject.SetActive(false);
            quitButton.transform.localScale = Vector3.zero;
            
            showAnswerButton.gameObject.SetActive(true);
            showAnswerButton.transform.localScale = Vector3.one;
        }

        private void AnimateOpening()
        {
            Sequence sequence = DOTween.Sequence();
            transform.localScale = Vector3.zero;
            sequence.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack));

            int count = 1;
            foreach (ChoiceDisplayer choice in choiceTexts)
            {
                choice.transform.localScale = Vector3.zero;
                sequence.Join(choice.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f * count));
            }

            sequence.OnComplete(() =>
            {
                transform.localScale = Vector3.one;
                choiceTexts.ForEach(choice => choice.transform.localScale = Vector3.one);
            });
        }

        private void DisplayChoices(Question question)
        {
            if (question.Choices.Count == 0)
            {
                choiceParent.SetActive(false);
                return;
            }
            
            choiceParent.SetActive(true);
            for (int i = 0; i < choiceTexts.Count; i++)
            {
                if (i >= question.Choices.Count)
                {
                    choiceTexts[i].Show(false);
                }
                else
                {
                    choiceTexts[i].Show(true);
                    choiceTexts[i].SetText(question.Choices[i]);
                }
            }
        }

        public void DisplayAnswer(Answer answer)
        {
            AnswerDisplayer answerDisplayer = new(new AnswerDisplayer.InitParam()
            {
                answerParent = answerParent,
                choices = choiceTexts,
                image = answerImage,
                singleLineText = singleLineText,
                layout = questionLayout
            });
            
            answerDisplayer.DisplayAnswer(answer);
            Sequence sequence = DOTween.Sequence();
            
            quitButton.gameObject.SetActive(true);
            sequence.Append(showAnswerButton.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            sequence.AppendCallback(() => showAnswerButton.gameObject.SetActive(false));
            sequence.Append(quitButton.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack));
        }

        protected virtual void OnClickShowAnswer()
        {
            DisplayAnswer(_question.Answer);
        }

        private void QuitQuestion()
        {
            EventBus<QuestionQuitEvent>.Raise(new QuestionQuitEvent());
        }

        protected virtual void OnEnable()
        {
            showAnswerButton.onClick.AddListener(OnClickShowAnswer);
            quitButton.onClick.AddListener(QuitQuestion);
        }

        protected virtual void OnDisable()
        {
            showAnswerButton.onClick.RemoveListener(OnClickShowAnswer);
            quitButton.onClick.RemoveListener(QuitQuestion);
        }
    }
}
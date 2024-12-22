using DG.Tweening;
using QuizzSystem.Questions;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace QuizzSystem.UI.QuestionDisplayers
{
    public class PictureQuestionDisplayer : TextQuestionDisplayer
    {
        [SerializeField] private Image picture;
        [SerializeField] private Button pictureButton;
        [SerializeField] private Button showPictureButton;
        [SerializeField] private Image fullScreenPicture;
        [SerializeField] private Button fullScreenButton;

        private bool _canZoom = true;
        
        public override void DisplayQuestion(Question question)
        {
            base.DisplayQuestion(question);
            picture.gameObject.SetActive(false);
            picture.transform.localScale = Vector3.zero;
            
            showPictureButton.gameObject.SetActive(true);
            showPictureButton.transform.localScale = Vector3.one;
            
            fullScreenPicture.transform.localScale = Vector3.zero;
            
            PictureQuestion pictureQuestion = question as PictureQuestion;
            if (pictureQuestion == null)
                return;

            picture.sprite = pictureQuestion.Picture;
            fullScreenPicture.sprite = pictureQuestion.Picture;
        }

        private void OnClickShowPicture()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(showPictureButton.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            sequence.AppendCallback(() =>
            {
                showPictureButton.gameObject.SetActive(false);
                picture.gameObject.SetActive(true);
            });
            sequence.Append(picture.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(questionLayout)));
        }

        private void OnClickPicture()
        {
            if (_canZoom == false)
                return;

            _canZoom = false;

            bool mustZoomOut = fullScreenPicture.transform.localScale == Vector3.one;
            Ease ease = mustZoomOut ? Ease.InBack : Ease.OutBack;
            Vector3 target = mustZoomOut ? Vector3.zero : Vector3.one;
            fullScreenPicture.transform.DOScale(target, 0.2f).SetEase(ease).OnComplete(() => _canZoom = true);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            showPictureButton.onClick.AddListener(OnClickShowPicture);
            pictureButton.onClick.AddListener(OnClickPicture);
            fullScreenButton.onClick.AddListener(OnClickPicture);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            showPictureButton.onClick.RemoveListener(OnClickShowPicture);
            pictureButton.onClick.RemoveListener(OnClickPicture);
            fullScreenButton.onClick.RemoveListener(OnClickPicture);
        }
    }
}

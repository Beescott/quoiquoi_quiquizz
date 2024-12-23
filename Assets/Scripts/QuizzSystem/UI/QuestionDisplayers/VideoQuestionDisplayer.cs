using DG.Tweening;
using QuizzSystem.Questions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace QuizzSystem.UI.QuestionDisplayers
{
    public class VideoQuestionDisplayer : TextQuestionDisplayer
    {
        [SerializeField] private RawImage picture;
        [SerializeField] private Button pictureButton;
        [SerializeField] private Button showPictureButton;
        
        [SerializeField] private RawImage fullScreenPicture;
        [SerializeField] private Button fullScreenButton;

        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private Button replayVideo;
        [SerializeField] private AudioSource mainMusicSource;
        
        private bool _canZoom = true;
        
        public override void DisplayQuestion(Question question)
        {
            base.DisplayQuestion(question);
            picture.gameObject.SetActive(false);
            picture.transform.localScale = Vector3.zero;
            
            showPictureButton.gameObject.SetActive(true);
            showPictureButton.transform.localScale = Vector3.one;
            
            fullScreenPicture.transform.localScale = Vector3.zero;
            replayVideo.transform.localScale = Vector3.one;
            VideoQuestion videoQuestion = question as VideoQuestion;
            if (videoQuestion == null)
                return;

            videoPlayer.url = System.IO.Path.Combine (Application.streamingAssetsPath, videoQuestion.VideoClip); 
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
            sequence.OnComplete(() => videoPlayer.Play());
            mainMusicSource.Pause();
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
            replayVideo.onClick.AddListener(OnReplay);
        }

        private void OnReplay()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            showPictureButton.onClick.RemoveListener(OnClickShowPicture);
            pictureButton.onClick.RemoveListener(OnClickPicture);
            fullScreenButton.onClick.RemoveListener(OnClickPicture);
            replayVideo.onClick.RemoveListener(OnReplay);
            
            videoPlayer.Stop();
            mainMusicSource.Play();
        }

        protected override void OnClickShowAnswer()
        {
            replayVideo.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack);
            base.OnClickShowAnswer();
        }
    }
}
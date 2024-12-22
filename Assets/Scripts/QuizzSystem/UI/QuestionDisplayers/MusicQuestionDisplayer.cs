using System.Collections;
using DG.Tweening;
using QuizzSystem.Questions;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem.UI.QuestionDisplayers
{
    public class MusicQuestionDisplayer : TextQuestionDisplayer
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button startMusicButton;
        [SerializeField] private GameObject musicGameObject;
        [SerializeField] private Image musicProgression;
        [SerializeField] private Button playAgain;
        [SerializeField] private AudioSource mainMusicSource;

        private IEnumerator _progressionCoroutine;
        
        public override void DisplayQuestion(Question question)
        {
            base.DisplayQuestion(question);
            musicGameObject.transform.localScale = Vector3.zero;

            startMusicButton.transform.localScale = Vector3.one;
            
            MusicQuestion musicQuestion = question as MusicQuestion;
            if (musicQuestion == null)
                return;

            audioSource.clip = musicQuestion.AudioClip;
        }

        private void OnClickPlayMusic()
        {
            mainMusicSource.Pause();
            Sequence sequence = DOTween.Sequence();
            sequence.Append(startMusicButton.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack));
            sequence.Append(musicGameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnUpdate(() => LayoutRebuilder.ForceRebuildLayoutImmediate(questionLayout)));
            sequence.AppendCallback(StartMusic);
        }

        private void StartMusic()
        {
            audioSource.Play();
            if (_progressionCoroutine != null)
                StopCoroutine(_progressionCoroutine);
            
            _progressionCoroutine = UpdateProgression();
            StartCoroutine(_progressionCoroutine);
        }

        private IEnumerator UpdateProgression()
        {
            while (audioSource.isPlaying)
            {
                float normalizedTime = audioSource.time / audioSource.clip.length;
                musicProgression.fillAmount = normalizedTime;
                yield return null;
            }
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            startMusicButton.onClick.AddListener(OnClickPlayMusic);
            playAgain.onClick.AddListener(PlayAgain);
        }

        private void PlayAgain()
        {
            audioSource.Stop();
            StopCoroutine(_progressionCoroutine);
            
            audioSource.Play();
            _progressionCoroutine = UpdateProgression();
            StartCoroutine(UpdateProgression());
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            audioSource.Stop();
            startMusicButton.onClick.RemoveListener(OnClickPlayMusic);
            musicProgression.fillAmount = 0;
            StopCoroutine(_progressionCoroutine);
            mainMusicSource.Play();
        }
    }
}
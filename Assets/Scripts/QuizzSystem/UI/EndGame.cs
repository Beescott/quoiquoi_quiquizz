using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using QuizzSystem.Events;
using UnityEngine;

namespace QuizzSystem.UI
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject endGameObject;
        [SerializeField] private Transform questionsParent;
        
        private EventBinding<QuestionQuitEvent> _questionQuitBinding;
        private List<QuestionButton> _questionButtons;
        private void OnEnable()
        {
            _questionButtons = questionsParent.GetComponentsInChildren<QuestionButton>().ToList();
            _questionQuitBinding = new(OnQuestionQuit);
            EventBus<QuestionQuitEvent>.Register(_questionQuitBinding);
        }

        private void OnDisable()
        {
            EventBus<QuestionQuitEvent>.Deregister(_questionQuitBinding);
        }

        private void OnQuestionQuit(QuestionQuitEvent questionQuitEvent)
        {
#if UNITY_EDITOR == false
            bool allAnswered = _questionButtons.TrueForAll(button => button.HasBeenAnswered);
            if (allAnswered == false)
                return;

            endGameObject.transform.localScale = Vector3.zero;
            endGameObject.SetActive(true);
            endGameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);      
#endif
        }
    }
}

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
            // si y'a un dev, j'veux faire une surprise avec ça, donc essaie de rien dire
            // une fois buildé y'aura cette image, et pas sur editor pour que quentin ait la surprise aussi
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

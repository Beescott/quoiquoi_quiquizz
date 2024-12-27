using System.Collections.Generic;
using System.Linq;
using QuizzSystem.UI;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem
{
    public class QuestionPager : MonoBehaviour
    {
        [SerializeField] private Button nextPage;
        [SerializeField] private Button previousPage;
        
        private List<QuestionButton> _questions;
        private int _currentPage = 0;
        
        private void OnEnable()
        {
            _questions = GetComponentsInChildren<QuestionButton>(includeInactive: true).ToList();
            _currentPage = 0;
            
            previousPage.onClick.AddListener(() => ChangePage(false));
            nextPage.onClick.AddListener(() => ChangePage(true));
            
            DisplayQuestions();
        }

        private void DisplayQuestions()
        {
            int numberOfQuestionPerPage = QuestionConfig.Instance.numberOfQuestionPerPage;
            int min = _currentPage * numberOfQuestionPerPage;
            int max = min + numberOfQuestionPerPage;
            
            for (int i = 0; i < _questions.Count; i++)
            {
                bool mustDisplay = i >= min && i < max;
                _questions[i].gameObject.SetActive(mustDisplay);
            }
            
            previousPage.gameObject.SetActive(_currentPage != 0);
            int maxPage = _questions.Count / numberOfQuestionPerPage;
            nextPage.gameObject.SetActive(_currentPage != maxPage);
        }

        private void OnDisable()
        {
            previousPage.onClick.RemoveAllListeners();
            nextPage.onClick.RemoveAllListeners();
        }

        private void ChangePage(bool isNextPage)
        {
            _currentPage += isNextPage ? 1 : -1;
            _currentPage = Mathf.Clamp(_currentPage, 0, _questions.Count / QuestionConfig.Instance.numberOfQuestionPerPage);
            DisplayQuestions();
        }
    }
}
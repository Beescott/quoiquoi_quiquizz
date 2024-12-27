using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace QuizzSystem.UI
{
    public class CategoryPage : MonoBehaviour
    {
        [SerializeField] private CategoryDisplayer categoryDisplayerPrefab;
        [SerializeField] private Transform categoryParent;
        [SerializeField] private Button quitButton;
        [SerializeField] private Transform questionParent;
        
        private List<CategoryDisplayer> _displayers;

        private void OnEnable()
        {
            _displayers = new();

            List<QuizzCategory> categories = new();
            foreach (Transform child in questionParent)
            {
                bool foundQuestionButton = child.TryGetComponent(out QuestionButton questionButton);
                if (foundQuestionButton == false)
                    continue;
                
                if (categories.Contains(questionButton.Question.Category) == false)
                    categories.Add(questionButton.Question.Category);
            }
            
            foreach (QuizzCategory category in categories)
            {
                bool foundColor = CategoryColorAssigner.Instance.TryGetColorFromCategory(category, out Color color);
                if (foundColor == false)
                    continue;
                
                CategoryDisplayer categoryDisplayer = Instantiate(categoryDisplayerPrefab, categoryParent);
                categoryDisplayer.SetCategory(category.ToFormattedText(), color);
                categoryDisplayer.transform.localScale = Vector3.zero;
                _displayers.Add(categoryDisplayer);
            }

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < _displayers.Count; i++)
            {
                sequence.Join(_displayers[i].transform.DOScale(Vector3.one, 0.2f).SetDelay(0.1f).SetEase(Ease.OutBack));
            }

            sequence.Play();
            
            quitButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void OnDisable()
        {
            for (int i = _displayers.Count - 1; i >= 0; i--)
            {
                Destroy(_displayers[i]);
            }

            _displayers = null;
        }
    }
}
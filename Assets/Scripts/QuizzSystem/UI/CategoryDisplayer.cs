using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QuizzSystem.UI
{
    public class CategoryDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text categoryText;
        [SerializeField] private Image categoryImage;

        public void SetCategory(string categoryName, Color color)
        {
            categoryText.text = categoryName;
            categoryImage.color = color;
        }

        private void OnValidate()
        {
            categoryText ??= GetComponentInChildren<TMP_Text>();
            categoryImage ??= GetComponentInChildren<Image>();
        }
    }
}

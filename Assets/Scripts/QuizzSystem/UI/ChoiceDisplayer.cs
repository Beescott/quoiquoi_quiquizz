using TMPro;
using UnityEngine;

namespace QuizzSystem.UI
{
    public class ChoiceDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text choiceText;
        
        public void SetText(string choice)
        {
            choiceText.text = choice;
        }

        public void Show(bool doShow)
        {
            gameObject.SetActive(doShow);
        }
    }
}
using System.Collections.Generic;
using QuizzSystem.UI;
using UnityEngine;

namespace QuizzSystem
{
    public abstract class Question : ScriptableObject
    {
        [SerializeField] protected string title;
        [SerializeField] protected Answer answer;
        [SerializeField] private List<string> choices;
        [SerializeField] private QuizzCategory category;
        [SerializeField] private Sprite background;
        
        [SerializeField] private Sprite buttonSprite;
        [SerializeField] private ButtonDisplay buttonDisplay;

        public string Title => title;
        public Answer Answer => answer;
        public List<string> Choices => choices;
        public Sprite Background => background;
        public QuizzCategory Category => category;
        public ButtonDisplay ButtonDisplay => buttonDisplay;
        public Sprite ButtonSprite => buttonSprite;
    }
}

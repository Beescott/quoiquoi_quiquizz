using System.Collections.Generic;
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

        public string Title => title;
        public Answer Answer => answer;
        public List<string> Choices => choices;
        public Sprite Background => background;
        public QuizzCategory Category => category;
    }
}

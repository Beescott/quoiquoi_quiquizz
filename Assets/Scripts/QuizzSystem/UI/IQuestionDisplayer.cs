using UnityEngine;

namespace QuizzSystem.UI
{
    public interface IQuestionDisplayer
    {
        public void DisplayQuestion(Question question);
        public void DisplayAnswer(Answer answer);

        public GameObject GameObject { get; }
    }
}
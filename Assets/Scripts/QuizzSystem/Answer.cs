using System;
using QuizzSystem.Answers;
using UnityEngine;

namespace QuizzSystem
{
    [Serializable]
    public struct Answer
    {
        public AnswerType type;
        public string singleTextAnswer;
        public int multipleChoiceIndexAnswer;
        public Sprite spriteAnswer;
    }
}
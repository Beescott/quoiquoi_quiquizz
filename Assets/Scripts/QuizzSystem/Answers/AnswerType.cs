using System;

namespace QuizzSystem.Answers
{
    [Flags]
    public enum AnswerType
    {
        MultipleChoice = 1 << 0,
        SingleText = 1 << 1,
        Image = 1 << 2
    }
}
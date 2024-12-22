namespace QuizzSystem.Events
{
    public struct QuestionClickedEvent : IEvent
    {
        public int Index;
        public Question Question;
    }
}
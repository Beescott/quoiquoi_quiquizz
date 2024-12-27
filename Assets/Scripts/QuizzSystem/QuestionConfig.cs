using Utility;

namespace QuizzSystem
{
    [AssetPathAttribute("Questions/Config")]
    public class QuestionConfig : ScriptableSingleton<QuestionConfig>
    {
        public int numberOfQuestionPerPage;
    }
}
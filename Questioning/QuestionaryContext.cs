using Questioning.DTO;

namespace Questioning
{
    public static class QuestionaryContext
    {
        public static int CurrentQuestion { get; set; }
        public static QuestionDTO[] Questions { get; set; }
    }
}

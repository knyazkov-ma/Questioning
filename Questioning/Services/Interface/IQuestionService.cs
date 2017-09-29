using System.Collections.Generic;
using Questioning.DTO;

namespace Questioning.Services.Interface
{
    public interface IQuestionService
    {
        QuestionDTO[] GetNewProfile();
        void SaveProfile(QuestionDTO[] questions);
        void ValidateQuestionAnswer(QuestionDTO[] questions, QuestionDTO question);
        void DeleteProfile(string fio);
        QuestionDTO[] GetProfile(string fio);
        IEnumerable<string> GetAllProfileNames();
        IEnumerable<string> GetAllTodayProfileNames();
        StatisticsDTO GetStatistics();
        void ZipProfile(string fio, string path);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IEBot.EntityFramework.Data;

namespace IEBot.EntityFramework
{
    public interface IQuestionsRepository
    {
        Task AddQuestionAsync(Question question);

        Task<Question> GetQuestionAsync(Guid guid);

        Task<IEnumerable<Question>> GetQuestionsAsync();

        IEnumerable<Question> GetQuestions(DateTime date);

        IEnumerable<Question> GetQuestions(ulong userId);

        void UpdateQuestion(Question question);

    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IEBot.Core.Models;

namespace IEBot.Core.Services
{
    public interface IQuestionsService
    {
        Task AddQuestionAsync(Question question);

        Task AddAnswerAsync(Answer answer);

        Task SetQuestionAnswered(Guid questionId, bool answered);

        Task<bool> IsQuestionAnswered(Guid questionId);

        Task<Question> GetQuestionAsync(Guid questionId);

        Task<IEnumerable<Question>> GetQuestionsAsync();

        IEnumerable<Question> GetQuestions(DateTime date);

        IEnumerable<Question> GetQuestions(ulong id);

        IEnumerable<Answer> GetAnswers(Guid questionId);

        
    }
}

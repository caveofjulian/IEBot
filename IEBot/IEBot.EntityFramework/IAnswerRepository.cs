using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IEBot.EntityFramework.Data;

namespace IEBot.EntityFramework
{
    public interface IAnswerRepository
    {

        IEnumerable<Answer> GetAnswers(Guid questionId);

        Task AddAnswerAsync(Answer answer);

        void DeleteAnswer(Answer answer);

    }
}

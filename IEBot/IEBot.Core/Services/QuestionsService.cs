using System;
using System.Collections.Generic;
using IEBot.Models;

namespace IEBot.Services
{
    public class QuestionsService : IQuestionsService
    {

        private IQuestionsRepository _questionsRepository;
        private IAnswerRepository _answerRepository;

        public QuestionsService()
        {
            //_questionsRepository = questionsRepository;
            //_answerRepository = answerRepository;
        }

        public void AddQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Answer> GetAnswers(Guid questionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetQuestions(DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetQuestions(ulong id)
        {
            throw new NotImplementedException();
        }

        public void SetQuestionAnswered(bool answered)
        {
            throw new NotImplementedException();
        }
    }
}

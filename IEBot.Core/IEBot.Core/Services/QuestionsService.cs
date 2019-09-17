using System;
using System.Collections.Generic;
using System.Text;

namespace IEBot.Core.Services
{
    public class QuestionsService
    {

        private IQuestionsRepository _questionsRepository;
        private IAnswerRepository _answerRepository;

        public QuestionsService(IQuestionsRepository questionsRepository, IAnswerRepository answerRepository)
        {
            _questionsRepository = questionsRepository;
            _answerRepository = answerRepository;
        }


    }
}

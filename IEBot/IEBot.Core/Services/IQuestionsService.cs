﻿using System;
using System.Collections.Generic;
using IEBot.Models;

namespace IEBot.Services
{
    public interface IQuestionsService
    {
        void AddQuestion(Question question);

        void SetQuestionAnswered(bool answered);

        IEnumerable<Question> GetQuestions(DateTime date);

        IEnumerable<Question> GetQuestions(ulong id);

        IEnumerable<Answer> GetAnswers(Guid questionId);

        
    }
}
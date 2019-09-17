using System;
using IEBot.Models;

namespace IEBot.Services
{
    public interface IQuestionsRepository
    {
        Question GetQuestions(DateTime date);


    }
}

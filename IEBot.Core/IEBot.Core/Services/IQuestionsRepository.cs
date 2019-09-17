using System;
using System.Collections.Generic;
using System.Text;
using IEBot.Core.Models;

namespace IEBot.Core.Services
{
    public interface IQuestionsRepository
    {
        Question GetQuestions(DateTime date);


    }
}

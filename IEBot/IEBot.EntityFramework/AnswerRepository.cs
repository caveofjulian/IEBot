﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEBot.EntityFramework.Data;

namespace IEBot.EntityFramework
{
    public class AnswerRepository : IAnswerRepository
    {
        private IEContext _context;

        public AnswerRepository(IEContext context)
        {
            _context = context;
        }

        public async Task AddAnswerAsync(Answer answer)
        {
            await _context.Answers.AddAsync(answer);
        }

        public void DeleteAnswer(Answer answer)
        {
            _context.Answers.Remove(answer);
        }

        public IEnumerable<Answer> GetAnswers(Guid questionId)
        {
            return _context.Answers.Where(x => x.QuestionId == questionId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IEBot.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace IEBot.EntityFramework
{
    public class QuestionsRepository : IQuestionsRepository
    {
        private IEContext _context;

        public QuestionsRepository(IEContext context)
        {
            _context = context;
        }

        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
        }

        public async Task<Question> GetQuestionAsync(Guid guid) =>
            await _context.Questions.FindAsync(guid);
        

        public async Task<IEnumerable<Question>> GetQuestionsAsync() =>
            await _context.Questions.ToListAsync();
        

        public IEnumerable<Question> GetQuestions(DateTime date) =>
            _context.Questions.Where(x => x.Time.CompareTo(date) == 0);


        public IEnumerable<Question> GetQuestions(ulong userId) =>
            _context.Questions.Where(x => x.UserId == userId);
        

        public void UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
        }
    }
}

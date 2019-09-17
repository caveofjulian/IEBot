using System;
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        public void DeleteAnswer(Answer answer)
        {
            _context.Answers.Remove(answer);
            _context.SaveChanges();
        }

        public IEnumerable<Answer> GetAnswers(Guid questionId)
        {
            return _context.Answers.Where(x => x.QuestionId == questionId);
        }
    }
}

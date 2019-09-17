using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IEBot.EntityFramework;
using IEBot.Models;

namespace IEBot.Services
{
    public class QuestionsService : IQuestionsService
    {

        private IQuestionsRepository _questionsRepository;
        private IAnswerRepository _answerRepository;

        public QuestionsService(IQuestionsRepository questionsRepository,IAnswerRepository answerRepository)
        {
            _questionsRepository = questionsRepository;
            _answerRepository = answerRepository;
        }

        public async Task AddQuestionAsync(Question question)
        {
            var questionEntity = new EntityFramework.Data.Question()
            {

                Id = question.GUID,
                Time = question.Time,
                Description = question.Description,
                Answered = question.Answered,
                UserId = question.UserId
            };

            await _questionsRepository.AddQuestionAsync(questionEntity);
        }

        public IEnumerable<Answer> GetAnswers(Guid questionId)
        {
            var answers = _answerRepository.GetAnswers(questionId).ToList();
            IList<Answer> modelAnswers = new List<Answer>();

            answers.ForEach(x => modelAnswers.Add(new Answer(x.QuestionId, x.Description, x.Time, x.UserId)));
            return modelAnswers;
        }

        public async Task<Question> GetQuestionAsync(Guid questionId)
        {
            var entity = await _questionsRepository.GetQuestionAsync(questionId);
            return new Question(entity.Description, entity.Time, entity.UserId);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var entities = (await _questionsRepository.GetQuestionsAsync()).ToList();
            IList<Question> questions = new List<Question>();

            entities.ForEach(x => questions.Add(new Question(x.Description, x.Time, x.UserId, x.Id, x.Answered)));
            return questions;
        }

        public IEnumerable<Question> GetQuestions(DateTime date)
        {
            var entities = _questionsRepository.GetQuestions(date).ToList();
            IList<Question> questions = new List<Question>();

            entities.ForEach(x => questions.Add(new Question(x.Description, x.Time, x.UserId, x.Id, x.Answered)));
            return questions;
        }

        public IEnumerable<Question> GetQuestions(ulong id)
        {
            var entities = _questionsRepository.GetQuestions(id).ToList();
            IList<Question> questions = new List<Question>();

            entities.ForEach(x => questions.Add(new Question(x.Description, x.Time, x.UserId, x.Id, x.Answered)));
            return questions;
        }

        public async Task<bool> IsQuestionAnswered(Guid questionId) =>(await _questionsRepository.GetQuestionAsync(questionId)).Answered;

        public async Task SetQuestionAnswered(Guid questionId, bool answered)
        {
            var question= await _questionsRepository.GetQuestionAsync(questionId);
            question.Answered = answered;
            _questionsRepository.UpdateQuestion(question); 
        }
    }
}

namespace IEBot.Services
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

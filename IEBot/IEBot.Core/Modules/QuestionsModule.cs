using System;
using System.Threading.Tasks;
using Discord.Commands;
using IEBot.Models;
using IEBot.Services;

namespace IEBot.Modules
{
    public class QuestionsModule : ModuleBase<SocketCommandContext>
    {
        private IQuestionsService _service;
        public QuestionsModule()
        {
            _service = new QuestionsService();
        }

        [Command("Ask")]
        public async Task Ask([Remainder] string description)
        {
            var question = new Question(description,DateTime.Now, Context.User.Id);
            //_service.AddQuestion(question);
            await ReplyAsync($"Oké deze vraag heeft ID nummer {question.GUID}, dus als je antwoord, begin met \"answer {question.GUID}\"");
        }

        [Command("Answer")]
        public async Task Answer([Remainder] string description)
        {
            var splitter = description.Split(' ');
            var guid = new Guid(splitter[0]);
            
            var answer = new Answer(guid,description,DateTime.Now, Context.User.Id);
            //_service.SetQuestionAnswered(true);
        }

        [Command("Questions")]
        public async Task ShowQuestions([Remainder] string userId)
        {

        }

        

    }
}

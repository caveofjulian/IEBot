using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using IEBot.Core.Models;
using IEBot.Core.Services;

namespace IEBot.Core.Modules
{
    public class QuestionsModule : ModuleBase<SocketCommandContext>
    {
        private IQuestionsService _service;
        public QuestionsModule(IQuestionsService service)
        {
            _service = service;
        }

        [Command("Ask")]
        public async Task Ask([Remainder] string description)
        {
            var question = new Question(description,DateTime.Now, Context.User.Id);
            _service.AddQuestion(question);
            await ReplyAsync($"Oké deze vraag heeft ID nummer {question.GUID}, dus als je antwoord, begin met \"answer {question.GUID}\"");
        }

        [Command("Answer")]
        public async Task Answer([Remainder] string description)
        {
            var splitter = description.Split(' ');
            var guid = new Guid(splitter[0]);
            
            var answer = new Answer(guid,description,DateTime.Now, Context.User.Id);
            _service.SetQuestionAnswered(true);
        }

        [Command("Questions")]
        public async Task ShowQuestions([Remainder] string userId)
        {

        }

        

    }
}

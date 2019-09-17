using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
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

        [Command("test")]
        public async Task Test()
        {
            await ReplyAsync("test");
        }

        [Command("Ask")]
        public async Task Ask([Remainder] string description)
        {
            var question = new Question(description,DateTime.Now, Context.User.Id);
            await _service.AddQuestionAsync(question);
            await ReplyAsync($"Oké deze vraag heeft ID nummer {question.GUID}, dus als je antwoord, begin met \"answer {question.GUID}\"");
        }

        [Command("Answer")]
        public async Task Answer([Remainder] string description)
        {
            var splitter = description.Split(' ');
            var guid = new Guid(splitter[0]);
            var answer = new Answer(guid,description,DateTime.Now, Context.User.Id);
            await _service.SetQuestionAnswered(guid,true);
        }

        [Command("Questions")]
        public async Task ShowQuestions([Remainder] string remainder)
        {
            if (remainder.ToLower().Contains("all"))
            {
                var questions = (await _service.GetQuestionsAsync()).ToList();

                if (!questions.Any()) await ReplyAsync("Er zijn gewoon nog geen vragen..");

                await SendQuestions(questions);

            }
        }

        public async Task ShowAnswers([Remainder] string questionId)
        {
            Guid guid;
            var succeeded = Guid.TryParse(questionId, out guid);

            if (!succeeded)
            {
                await ReplyAsync("Bruh, je guid klopt niet.");
            }
            else
            {
                var answers = _service.GetAnswers(new Guid(questionId)).ToList();

                if (!answers.Any()) await ReplyAsync("Er zijn hier helaas nog geen antwoorden op.");
                else await SendAnswers(answers);
            }
        }

        private async Task SendQuestions(IList<Question> questions)
        {
            foreach (var question in questions)
            {
                var embed = CreateEmbed(question);
                await ReplyAsync(embed: embed);
            }
        }

        private async Task SendAnswers(IList<Answer> answers)
        {
            foreach (var answer in answers)
            {
                var embed = await CreateEmbed(answer);
                await ReplyAsync(embed: embed);
            }
        }

        private Embed CreateEmbed(Question question)
        {
            var builder = new EmbedBuilder();
            builder.Author.Name = Context.Guild.GetUser(question.UserId).Nickname;

            string word = question.Answered ? "al" : "nog niet";

            builder.Title = $"Vraag {question.GUID} op {question.Time} is {question.Answered} beantwoord.";
            builder.Description = question.Description;
            return builder.Build();
        }

        private async Task<Embed> CreateEmbed(Answer answer)
        {
            var builder = new EmbedBuilder();
            builder.Author.Name = Context.Guild.GetUser(answer.UserId).Nickname;
            builder.Title = (await _service.GetQuestionAsync(answer.QuestionId)).Description;
            builder.Description = answer.Description;
            return builder.Build();
        }

        

    }
}

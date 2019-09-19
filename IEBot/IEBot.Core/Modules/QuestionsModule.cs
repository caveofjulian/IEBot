using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using IEBot.Core.Models;
using IEBot.Core.Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace IEBot.Core.Modules
{
    
    public class QuestionsModule : ModuleBase<SocketCommandContext>
    {
        private IQuestionsService _service;

        public QuestionsModule(IQuestionsService service)
        {
            _service = service;
            string s = "hello";
            for (int i = 0; i < s.Length; i++)
            {
                
            }
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
        public async Task Answer([Remainder] string args)
        {
            var splitter = args.Split(' ');
            StringBuilder builder = new StringBuilder();

            for (int i = 1; i < splitter.Length; i++)
            {
                builder.Append(splitter[i]);
            }

            Guid guid;
            var succeeded = Guid.TryParse(splitter[0].Trim(), out guid);

            if (!succeeded)
            {
                await ReplyAsync("Je hebt de guid niet goed ingetypt dus je mag niet antwoorden :(");
            }
            else
            {
                var answer = new Answer(guid, builder.ToString(), DateTime.Now, Context.User.Id);
                await _service.AddAnswerAsync(answer);
                await _service.SetQuestionAnswered(guid, true);
                await ReplyAsync("Goed dat je deze vraag hebt beantwoord gap.");
            }
        }

        [Command("Questions")]
        [Alias("ShowQuestions", "Show Questions", "Show Question", "ShowQuestion")]
        public async Task ShowQuestions([Remainder] string remainder)
        {
            if (remainder.ToLower().Contains("all"))
            {
                var q = await _service.GetQuestionsAsync();
                var questions = q.ToList();

                if (!questions.Any()) await ReplyAsync("Er zijn gewoon nog geen vragen..");

                await SendQuestions(questions);
            }
        }

        [Command("ShowAnswers")]
        [Alias("Answers all", "Answers", "ShowAnswer", "Show Answer", "Show Answers")]
        public async Task ShowAnswers([Remainder] string args)
        {
            Guid guid;
            var succeeded = Guid.TryParse(args.Trim(), out guid);

            if (!succeeded)
            {
                await ReplyAsync("Bruh, je guid klopt niet.");
            }
            else
            {
                var answers = _service.GetAnswers(guid).ToList();

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

            string word = question.Answered ? "al" : "nog niet";

            builder.Title = $"De vraag van {Context.Guild.GetUser(question.UserId).Username} met ID {question.GUID} op {question.Time} is {word} beantwoord.";
            builder.Description = question.Description;
            return builder.Build();
        }

        private async Task<Embed> CreateEmbed(Answer answer)
        {
            var builder = new EmbedBuilder();
            var question = await _service.GetQuestionAsync(answer.QuestionId);
            builder.Title = $"{question.Description}";
            builder.WithAuthor(new EmbedAuthorBuilder().WithName($"Gevraagd door: {Context.Guild.GetUser(question.UserId).Username}, Beantwoord door: {Context.Guild.GetUser(answer.UserId).Username}"));

            builder.Description = answer.Description;
            return builder.Build();
        }

        

    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using IEBot.Core;
using IEBot.Core.Services;
using IEBot.EntityFramework;

namespace IEBot.Container
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors
               .AddSingleton<IQuestionsService, QuestionsService>()
               .AddSingleton<IQuestionsRepository, QuestionsRepository>()
               .AddSingleton<IAnswerRepository, AnswerRepository>();

            Client Client = new Client(serviceDescriptors);

            Client.RunAsync().GetAwaiter().GetResult();
        }
    }
}

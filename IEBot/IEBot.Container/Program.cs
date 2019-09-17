using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using IEBot.Services;

namespace IEBot.Container
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            //serviceDescriptors
             //   .AddSingleton<IQuestionsService, QuestionsService>();

            Client Client = new Client(serviceDescriptors);

            await Client.RunAsync();
        }
    }
}

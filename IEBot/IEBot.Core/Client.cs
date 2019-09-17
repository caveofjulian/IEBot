using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IEBot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IEBot
{
    public class Client
    {
        public IServiceProvider Services { get; }
        public DiscordSocketClient SocketClient => new DiscordSocketClient();

        // locals
        private readonly CommandService _commands = new CommandService();
        public Client(ServiceCollection serviceDescriptors)
        {
            if (serviceDescriptors == null) serviceDescriptors = new ServiceCollection();

            Services = serviceDescriptors
                .AddSingleton(SocketClient)
                .AddSingleton(_commands)
                .AddTransient<ITokenService, TokenFileService>((x) => new TokenFileService(@"D:\workspace\token.txt"))
                .BuildServiceProvider();
        }

        /// <summary>
        /// Executes the startup sequence
        /// </summary>
        public async Task RunAsync()
        {
            try
            {
                SocketClient.Log += Log;
                await RegisterCommandsAsync();

                // get the token service
                ITokenService tokenService = Services.GetRequiredService<ITokenService>();
                await SocketClient.LoginAsync(Discord.TokenType.Bot, await tokenService.GetTokenAsync());
                await SocketClient.StartAsync();
                await Task.Delay(-1);
            }
            catch (Exception)
            {
                // log the error...
            }
        }

        private async Task RegisterCommandsAsync()
        {
            SocketClient.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            int argPos = 0;

            if (msg is null) return;

            if (!msg.Author.IsBot && msg.HasCharPrefix('*', ref argPos))
            {
                var context = new SocketCommandContext(SocketClient, msg);
                var result = await _commands.ExecuteAsync(context, argPos, Services);
                if (!result.IsSuccess) return;
            }

            ulong test = msg.Id;
            var options = new RequestOptions { RetryMode = RetryMode.AlwaysRetry };
            //await msg.DeleteAsync(options);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}

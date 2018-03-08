using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Bot
    {
        private DiscordSocketClient client;

        private CommandService commands;

        private IServiceProvider services;


        static void Main(string[] args) => new Bot().RunBotAsync().GetAwaiter().GetResult();



        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection().AddSingleton(client).AddSingleton(commands).BuildServiceProvider();

            string botToken = ""; ///TODO: token	

            client.Log += Log;


            await RegisterCommandsAsync();

            await client.LoginAsync(TokenType.Bot, botToken);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot)
                return;

            int argPos = 0;

            if (message.HasStringPrefix("!", ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(client, message);

                var result = await commands.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }

    }
}
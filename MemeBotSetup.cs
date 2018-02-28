using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MemeBotCode
{
    class MemeBotSetup
    {
        private IServiceProvider services;
        private CommandService commands;

        public static void Main(string[] args)
            => new MemeBotSetup().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            commands = new CommandService();

            string token = "no pls"; //This should be in a separate file for the final build on GitHub

            services = new ServiceCollection().BuildServiceProvider();

            client.Log += LogHandler;
            client.MessageReceived += (messageParameter) => MessageReceived(messageParameter, commands, client);

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task LogHandler(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage messageParameter, CommandService commands, DiscordSocketClient client)
        {
            SocketUserMessage message = messageParameter as SocketUserMessage;
            int cmdPos = 0;

            if (message != null && message.HasCharPrefix('!', ref cmdPos))
            {
                CommandContext context = new CommandContext(client, message);
                IntroModule.setCommandService(commands);
                var result = await commands.ExecuteAsync(context, cmdPos, services);

                if(!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}

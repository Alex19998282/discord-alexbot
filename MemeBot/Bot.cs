using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using MemeBot.Modules;
using System.Collections.Generic;
using System.Linq;

namespace MemeBot
{
    internal class Bot : IBot
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IBotConfiguration config;
        private IServiceProvider services;

        public Bot()
        {
            this.client = new DiscordSocketClient();
            this.commands = new CommandService();
            this.services = BotServiceLocator.Resolver;
            this.config = (IBotConfiguration)services.GetService(typeof(IBotConfiguration));
        }

        public async Task Run()
        {
            await commands.AddModuleAsync<IntroBot>();
            await commands.AddModuleAsync<ImageBot>();
            await commands.AddModuleAsync<MathsBot>();

            client.Log += LogHandler;
            client.MessageReceived += (message) => MessageReceived(message);

            await client.LoginAsync(TokenType.Bot, config.GetToken());
            await client.StartAsync();

            await Task.Delay(-1);
        }

        public IEnumerable<CommandInfo> GetCommands()
        {
            var commandList = new List<CommandInfo>();

            foreach (ModuleInfo module in commands.Modules)
            {
                foreach (CommandInfo command in module.Commands)
                {
                    commandList.Add(command);
                }
            }

            return commandList.AsEnumerable<CommandInfo>();
        }

        private Task LogHandler(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task MessageReceived(SocketMessage message)
        {
            SocketUserMessage userMessage = message as SocketUserMessage;
            CommandContext context = new CommandContext(client, userMessage);
            int cmdPos = 0;

            if (message != null && userMessage.HasCharPrefix('!', ref cmdPos))
            {

                var result = await commands.ExecuteAsync(context, cmdPos, services);

                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
            else if (message != null && message.Author.IsBot && message.Author.Id != client.CurrentUser.Id)
            {
                await context.Channel.SendMessageAsync($"H-h-h-hello {message.Author.Username}");
            }
        }
    }
}
using Discord.Commands;
using Discord.WebSocket;
using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeBot.Modules
{
    public class IntroBot : ModuleBase
    {
        private readonly IBot bot;

        public IntroBot(IBot bot)
        {
            this.bot = bot;
        }

        [Command("hello"), Summary("Says hello to the user")]
        public async Task Hello()
        {
            await ReplyAsync($"Hello {Context.Message.Author.Username}, I am a bot created by Alex19998282 and coded in C#. I can do many things, just use the !help command for more\n\n" +
                "My code can be found here: https://github.com/Alex19998282/discord-alexbot");
        }

        [Command("helloverbose"), Summary("Says hello to the user in a verbose manner")]
        public async Task HelloVerbose()
        {
            await ReplyAsync($"Hello {Context.Message.Author.Username}, I am a bot created by Alex19998282 and coded in C# using the Discord.Net API and .NET Core. I am currently using .NET 4.6.1. " +
                "I accept multiple commands which can be viewed by using the !help command.\n\n" +
                "My code can be found here: https://github.com/Alex19998282/discord-alexbot");
        }

        [Command("help"), Summary("Lists availiable commands to the user")]
        public async Task Help()
        {
            string replyString = "Sure, here are my availiable commands: \n\n```";

            foreach (CommandInfo command in bot.GetCommands())
            {
                string commandInfo = "";
                commandInfo += command.Aliases[0] + " - " + command.Summary;
                replyString += commandInfo + "\n";
            }

            replyString += "```";
            await ReplyAsync(replyString);
        }
    }
}

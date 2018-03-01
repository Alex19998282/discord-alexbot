using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MemeBot.Modules
{
    [Group("maths")]
    public class MathsBot : ModuleBase
    {
        [Command("multiply"), Summary("multiply")]
        public async Task Multiply(int a, int b)
        {
            await ReplyAsync("The result is: " + a * b);
        }
    }
}

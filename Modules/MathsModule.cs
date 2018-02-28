using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;


namespace MemeBotCode
{
    [Group("maths")]
    public class MathsModule : ModuleBase
    {
        [Command("multiply"), Summary("multiply")]
        public async Task Multiply(int a, int b)
        {
            await ReplyAsync("The result is: " + a * b);
        }
    }
}

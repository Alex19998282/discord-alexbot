using Discord.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemeBot
{
    public interface IBot
    {
        Task Run();

        IEnumerable<CommandInfo> GetCommands();
    }
}
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MemeBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection
                .AddSingleton<IBot, Bot>()
                .AddSingleton<IBotConfiguration, BotConfiguration>();

            BotServiceLocator.Resolver = serviceCollection.BuildServiceProvider();

            var bot = BotServiceLocator.Resolver.GetRequiredService<IBot>();
            bot.Run().GetAwaiter().GetResult();
        }
    }
}

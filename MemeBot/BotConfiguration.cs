using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MemeBot
{
    public class BotConfiguration : IBotConfiguration
    {
        private readonly IConfiguration config;

        public BotConfiguration()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            config = builder.Build();
        }

        public string GetToken()
        {
            return config["token"];
        }
    }
}

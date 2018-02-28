using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace MemeBotCode
{
    [Group("embed")]
    public class EmbedModule : ModuleBase
    {
        [Command("test"), Summary("Tests the embed abilities of the Discord API")]
        public async Task EmbedTest()
        {
            EmbedBuilder embed = new EmbedBuilder();
            EmbedFooterBuilder embedFooter = new EmbedFooterBuilder();
            embedFooter.Text = "This is a test";
            embedFooter.IconUrl = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/35/35d08b19c20453e316e4ce5fdc30d57425f7734f_full.jpg";

            embed.Author = new EmbedAuthorBuilder();
            embed.Title = "This is a test";
            embed.ThumbnailUrl = "http://cdn.akamai.steamstatic.com/steamcommunity/public/images/avatars/35/35d08b19c20453e316e4ce5fdc30d57425f7734f_full.jpg";
            embed.Footer = embedFooter;
            
            embed.AddInlineField("test", "123");

            await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}

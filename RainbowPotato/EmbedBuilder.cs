using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainbowPotato
{
    internal class EmbedBuilder
    {
        private DiscordEmbedBuilder discordEmbedBuilder;

        public EmbedBuilder()
        {
            discordEmbedBuilder = new DiscordEmbedBuilder();
            SetColor(131, 156, 206);
        }

        public void AddField(string name, string value, bool inline)
        {
            discordEmbedBuilder.AddField(name, value, inline);
        }

        public void SetColor(DiscordColor color)
        {
            discordEmbedBuilder.WithColor(color);
        }

        public void SetColor(byte r, byte g, byte b)
        {
            discordEmbedBuilder.WithColor(new DiscordColor(r, g, b));
        }

        public void SetThumbnail(string url)
        {
            discordEmbedBuilder.WithThumbnail(url);
        }

        public void AddImage(string url)
        {
            discordEmbedBuilder.WithImageUrl(url);
        }

        public void AddDescription(string content)
        {
            discordEmbedBuilder.WithDescription(content);
        }

        public DiscordEmbed Build()
        {
            return discordEmbedBuilder.Build();
        }
    }
}

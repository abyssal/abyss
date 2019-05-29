using System;
using System.Linq;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace Katbot.Extensions
{
    public static class DiscordExtensions
    {
        public static ITextChannel GetDefaultChannel(this SocketGuild guild)
        {
            if (guild.DefaultChannel != null) return guild.DefaultChannel;
            if (guild.TextChannels.Count == 0) return null;

            // find channel
            var firstOrDefault = guild.TextChannels.FirstOrDefault(c =>
                c.Name.Equals("general", StringComparison.OrdinalIgnoreCase)
                && guild.CurrentUser.GetPermissions(c).SendMessages);

            return firstOrDefault ?? guild.TextChannels.FirstOrDefault(c => guild.CurrentUser.GetPermissions(c).SendMessages);
        }

        public static LogLevel ToMicrosoftLogLevel(this LogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case LogSeverity.Critical:
                    return LogLevel.Critical;
                case LogSeverity.Error:
                    return LogLevel.Error;
                case LogSeverity.Warning:
                    return LogLevel.Warning;
                case LogSeverity.Info:
                    return LogLevel.Information;
                case LogSeverity.Verbose:
                    return LogLevel.Trace;
                case LogSeverity.Debug:
                    return LogLevel.Debug;
                default:
                    return LogLevel.Information;
            }
        }
    }
}
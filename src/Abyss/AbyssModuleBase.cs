﻿using Qmmands;
using Disqord;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Abyss
{
    /// <summary>
    ///     The base class for all Abyss modules.
    /// </summary>
    public abstract class AbyssModuleBase : ModuleBase<AbyssCommandContext>
    {
        #pragma warning disable 8618
        public ILoggerFactory LoggerFactory { get; set; }
        #pragma warning restore
        
        protected ILogger Logger => LoggerFactory.CreateLogger(GetType().Name);

        public Task ReplyAsync(string? content = null, LocalEmbedBuilder? embed = null,
            RestRequestOptions? options = null)
        {
            return Context.ReplyAsync(content, embed, options);
        }

        public static AbyssResult Attachment(params LocalAttachment[] attachments)
        {
            return new ContentResult((string?) null, attachments: attachments);
        }

        public static AbyssResult Image(string? title, string imageUrl)
        {
            return Ok(e =>
            {
                e.ImageUrl = imageUrl;
                e.Title = title;
            });
        }

        public static ReplySuccessResult SuccessReply() => new ReplySuccessResult();

        public static ReactSuccessResult SuccessReaction() => new ReactSuccessResult();

        public static AbyssResult Ok(string content, Action<ContentResultOptions>? resultOptions = null, params LocalAttachment[] attachments)
        {
            return new ContentResult(content, resultOptions, attachments);
        }

        /// <summary>
        ///     Returns an <see cref="AbyssResult"/> that represents replying to a context with the specified embed and attachments.
        /// </summary>
        /// <param name="builder">The embed builder to send.</param>
        /// <param name="attachments">The attachments to send.</param>
        /// <returns>An <see cref="AbyssResult"/> that represents replying to the specified context with the specified embed and attachments.</returns>
        public static AbyssResult Ok(LocalEmbedBuilder builder, Action<ContentResultOptions>? resultOptions = null, params LocalAttachment[] attachments)
        {
            return new ContentResult(builder, resultOptions, attachments);
        }

        /// <summary>
        ///     Returns an <see cref="AbyssResult"/> that represents replying to a context with the specified embed and attachments.
        /// </summary>
        /// <param name="actor">An actor which mutates the embed to send.</param>
        /// <param name="attachments">The attachments to send.</param>
        /// <returns>An <see cref="AbyssResult"/> that represents replying to the specified context with the specified embed and attachments.</returns>
        public static AbyssResult Ok(Action<LocalEmbedBuilder> actor, Action<ContentResultOptions>? resultOptions = null, params LocalAttachment[] attachments)
        {
            var eb = new LocalEmbedBuilder();
            actor(eb);
            return Ok(eb, resultOptions, attachments);
        }

        /// <summary>
        ///     Returns an <see cref="AbyssResult"/> that represents invalidity of the request parameters.
        /// </summary>
        /// <param name="reason">The reason as to why the request parameters are invalid.</param>
        /// <returns>A <see cref="BadRequestResult"/> that represents replying to the specified context with the specified error message.</returns>
        public static AbyssResult BadRequest(string reason)
        {
            return new BadRequestResult(reason);
        }

        /// <summary>
        ///     Returns an <see cref="AbyssResult"/> that does nothing.
        /// </summary>
        /// <returns>An <see cref="AbyssResult"/> that does nothing.</returns>
        public static AbyssResult Empty()
        {
            return new EmptyResult();
        }
    }
}
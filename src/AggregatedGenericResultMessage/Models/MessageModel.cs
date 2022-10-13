// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-06-30 10:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:43
// ***********************************************************************
//  <copyright file="MessageModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Text.Json.Serialization;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Helpers;
using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
#pragma warning disable 8632

#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc />
    public class MessageModel : IMessageModel
    {
        private const string ToStringFormat = "{0}: {1}";

        #region P R O P s

        /// <inheritdoc />
        [JsonPropertyName("key")]
        public string Key { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("messageType")]
        public MessageType MessageType { get; set; }

        #endregion

        #region C T O R s

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <remarks></remarks>
        public MessageModel()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(string). The default value is null.</param>
        /// <param name="messageType">Message type</param>
        /// <remarks></remarks>
        public MessageModel(string key, string message = null, MessageType messageType = MessageType.Error)
        {
            Key = key;
            MessageType = messageType;

            if (!message.IsNullOrEmpty())
                Message = message?.Trim();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="exception">Optional(exception). Current exception. The default value is null.</param>
        /// <remarks></remarks>
        public MessageModel(string key, Exception? exception)
        {
            Key = key;
            MessageType = MessageType.Exception;

            if (!exception.IsNull())
                Message = ExceptionHelper.CreateTraceExceptionString(exception);
        }

        #endregion
        
        /// <inheritdoc cref="IMessageModel" />
        public override string ToString()
        {
            return string.Format(ToStringFormat, Key, Message);
        }

        /// <summary>
        ///     Implicit operator
        /// </summary>
        /// <param name="formatted">Input message</param>
        /// <returns></returns>
        /// <remarks>formatted[0] => Message, formatted[1] => Message type</remarks>
        public static implicit operator MessageModel(string[] formatted)
        {
            var messageType = formatted.Length > 1
                ? formatted[1].ToEnum<MessageType>()
                : MessageType.Error;

            try
            {
                var parts = formatted[0].Split(':');

                if (parts.Length < 2)
                    return new MessageModel(ExceptionCodes.UnhandledException, formatted[0], messageType);

                if (parts.Length == 2)
                    return new MessageModel(parts[0], parts[1]);

                return new MessageModel(parts[0], formatted[0].Substring(parts[0].Length).TrimStart(':').Trim(),
                    messageType);
            }
            catch
            {
                return new MessageModel(ExceptionCodes.UnhandledException, formatted[0], messageType);
            }
        }
    }
}
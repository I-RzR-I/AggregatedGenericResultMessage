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

#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Helpers;
using AggregatedGenericResultMessage.Extensions.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

#pragma warning disable IDE0057
#pragma warning disable 8632
#pragma warning disable IDE0079
#pragma warning disable IDE0046


#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc />
    public class MessageModel : IMessageModel
    {
        private const string ToStringFormat = "{0}: {1}";

        #region P R O P s

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("key")]
#endif
        [XmlElement("Key")]
        public string Key { get; set; }

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("Message")]
#endif
        [XmlElement("Message")]
        public MessageDataModel Message { get; set; } = new MessageDataModel();

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("messageType")]
#endif
        [XmlElement("MessageType")]
        public MessageType MessageType { get; set; }

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("logTraceId")]
#endif
        [XmlElement("LogTraceId")]
        public string LogTraceId { get; set; } = GetTraceLogId();

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("relatedObjects")]
#endif
        [XmlArray("RelatedObjecs")]
        [XmlArrayItem("RelatedObject")]
        public List<RelatedObjectModel> RelatedObjects { get; set; } = new List<RelatedObjectModel>();

        #endregion

        #region C T O R s

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <remarks></remarks>
        public MessageModel() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(MessageDataModel). The default value is null.</param>
        /// <param name="messageType">Message type</param>
        /// <remarks></remarks>
        public MessageModel(string key, string message = null, MessageType messageType = MessageType.Error)
        {
            Key = key;
            MessageType = messageType;

            if (message.IsNotNull())
                Message = new MessageDataModel(message);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(MessageDataModel). The default value is null.</param>
        /// <param name="messageType">Message type</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public MessageModel(string key, string message, MessageType messageType, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            MessageType = messageType;

            if (message.IsNotNull())
                Message = new MessageDataModel(message);

            if (relatedObjects.IsNotNull())
                RelatedObjects?.AddRange(relatedObjects);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(MessageDataModel). The default value is null.</param>
        /// <param name="messageType">Message type</param>
        /// <remarks></remarks>
        public MessageModel(string key, MessageDataModel message = null, MessageType messageType = MessageType.Error)
        {
            Key = key;
            MessageType = messageType;

            if (message.IsNotNull())
                Message = message;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(MessageDataModel). The default value is null.</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public MessageModel(string key, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            Message = message;

            if (message.IsNotNull())
                Message = message;

            if (relatedObjects.IsNotNull())
                RelatedObjects?.AddRange(relatedObjects);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="message">Optional(MessageDataModel). The default value is null.</param>
        /// <param name="messageType">Message type</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public MessageModel(string key, MessageDataModel message, MessageType messageType, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            MessageType = messageType;

            if (message.IsNotNull())
                Message = message;

            if (relatedObjects.IsNotNull())
                RelatedObjects?.AddRange(relatedObjects);
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

            if (exception.IsNotNull())
                Message = ExceptionHelper.CreateTraceExceptionMessage(exception);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageModel" /> class.
        /// </summary>
        /// <param name="key">Required. Message key</param>
        /// <param name="exception">Optional(exception). Current exception. The default value is null.</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public MessageModel(string key, Exception? exception, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            MessageType = MessageType.Exception;

            if (exception.IsNotNull())
                Message = ExceptionHelper.CreateTraceExceptionMessage(exception);

            if (relatedObjects.IsNotNull())
                RelatedObjects?.AddRange(relatedObjects!);
        }

        #endregion

        /// <inheritdoc cref="IMessageModel.ToString" />
        public override string ToString()
            => string.Format(ToStringFormat, Key, Message);

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

                return parts.Length switch
                {
                    < 2 => new MessageModel(ExceptionCodes.UnhandledException, new MessageDataModel(formatted[0]), messageType),
                    2 => new MessageModel(parts[0], new MessageDataModel(parts[1])),
                    _ => new MessageModel(parts[0], new MessageDataModel(formatted[0].Substring(parts[0].Length).TrimStart(':').Trim()), messageType)
                };
            }
            catch
            {
                return new MessageModel(ExceptionCodes.UnhandledException, new MessageDataModel(formatted[0]), messageType);
            }
        }

        /// <summary>
        ///     Get uniq trace log id
        /// </summary>
        /// <returns></returns>
        private static string GetTraceLogId()
            => $"{Guid.NewGuid():N}#{DateTime.UtcNow:yyyyMMddHHmmssfff}".Base64Encode();
    }
}
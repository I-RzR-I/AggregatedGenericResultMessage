// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 19:49
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:20
// ***********************************************************************
//  <copyright file="GeneralMessageExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Result.Messages
{
    /// <summary>
    ///     General message extension
    /// </summary>
    public static class GeneralMessageExtensions
    {
        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string key = null, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string key = null, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, message, type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IResult<T> AddMessage<T>(this IResult<T> result, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, message, type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string key = null, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string key = null, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, message, type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param> 
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Result<T> AddMessage<T>(this Result<T> result, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, message, type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string key = null, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string key = null, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, new MessageDataModel(message), type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(key, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string key = null, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(key, message, type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, string message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, new MessageDataModel(message), 
                type, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, MessageDataModel message = null,
            MessageType type = MessageType.Error)
        {
            result?.Messages?.Add(new MessageModel(null, message, type));

            return result;
        }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="result">Current result</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static AggregatedGenericResultMessage.Result AddMessage(
            this AggregatedGenericResultMessage.Result result, MessageDataModel message = null,
            MessageType type = MessageType.Error, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(null, message, type, relatedObjects: relatedObjects));

            return result;
        }
    }
}
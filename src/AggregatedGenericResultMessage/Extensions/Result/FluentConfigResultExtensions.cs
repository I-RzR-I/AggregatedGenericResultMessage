// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 15:20
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 16:06
// ***********************************************************************
//  <copyright file="FluentConfigResultExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Abstractions.Models;
using System;
using System.Collections.Generic;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;
using System.Linq;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Result
{
    /// <summary>
    ///     Result extensions
    /// </summary>
    public static class FluentConfigResultExtensions
    {
        /// <summary>
        ///     Customize result with message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Message type. The default value is MessageType.None.</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithMessage<T>(this Result<T> result, string message,
            MessageType messageType = MessageType.None)
        {
            result?.Messages.Add(new MessageModel(null, message, messageType));

            return result;
        }

        /// <summary>
        ///     Customize result with message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Message type. The default value is MessageType.None.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithMessage<T>(this Result<T> result, string message,
            MessageType messageType = MessageType.None, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages.Add(new MessageModel(null, message, messageType, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Message type. The default value is MessageType.None.</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithMessage<T>(this Result<T> result, MessageDataModel message,
            MessageType messageType = MessageType.None)
        {
            result?.Messages.Add(new MessageModel(null, message, messageType));

            return result;
        }

        /// <summary>
        ///     Customize result with message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Message type. The default value is MessageType.None.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithMessage<T>(this Result<T> result, MessageDataModel message,
            MessageType messageType = MessageType.None, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages.Add(new MessageModel(null, message, messageType, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Message code</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message The default value is MessageType.None.</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithKeyCode<T>(this Result<T> result, string code,
            MessageType messageType = MessageType.None)
        {
            result?.Messages?.Add(new MessageModel(code, (string)null, messageType));

            return result;
        }

        /// <summary>
        ///     Customize result with code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Message code</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message The default value is MessageType.None.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithKeyCode<T>(this Result<T> result, string code,
            MessageType messageType = MessageType.None, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, (string)null, messageType, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with message and code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Code</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message. The default value is MessageType.None.</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithCodeMessage<T>(this Result<T> result, string code, string message,
            MessageType messageType = MessageType.None)
        {
            result?.Messages?.Add(new MessageModel(code, message, messageType));

            return result;
        }

        /// <summary>
        ///     Customize result with message and code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Code</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message. The default value is MessageType.None.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithCodeMessage<T>(this Result<T> result, string code, string message,
            MessageType messageType = MessageType.None, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, message, messageType, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with message and code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Code</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message. The default value is MessageType.None.</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithCodeMessage<T>(this Result<T> result, string code, MessageDataModel message,
            MessageType messageType = MessageType.None)
        {
            result?.Messages?.Add(new MessageModel(code, message, messageType));

            return result;
        }

        /// <summary>
        ///     Customize result with message and code
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="code">Required. Code</param>
        /// <param name="message">Required. Message</param>
        /// <param name="messageType" cref="MessageType">Optional. Type of message. The default value is MessageType.None.</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithCodeMessage<T>(this Result<T> result, string code, MessageDataModel message,
            MessageType messageType = MessageType.None, params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, message, messageType, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with error message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="error">Required. Error message</param>
        /// <param name="code">Optional. Error code. The default value is "".</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, string error, string code = null)
        {
            result?.Messages?.Add(new MessageModel(code, error));

            return result;
        }

        /// <summary>
        ///     Customize result with error message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="error">Required. Error message</param>
        /// <param name="code">Optional. Error code. The default value is "".</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, string error, string code = null,
            params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, error, MessageType.Error,
                relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with error message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="error">Required. Error message</param>
        /// <param name="code">Optional. Error code. The default value is "".</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, MessageDataModel error, string code = null)
        {
            result?.Messages?.Add(new MessageModel(code, error));

            return result;
        }

        /// <summary>
        ///     Customize result with error message
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="error">Required. Error message</param>
        /// <param name="code">Optional. Error code. The default value is "".</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, MessageDataModel error, string code = null,
            params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, error, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with occurred exception
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="exception">Required. occurred code exception</param>
        /// <param name="code">Optional. Exception code. The default value is "".</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, Exception exception, string code = null)
        {
            result?.Messages?.Add(new MessageModel(code, exception));

            return result;
        }

        /// <summary>
        ///     Customize result with occurred exception
        /// </summary>
        /// <param name="result">Required. Current Result</param>
        /// <param name="exception">Required. occurred code exception</param>
        /// <param name="code">Optional. Exception code. The default value is "".</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, Exception exception, string code = null,
            params RelatedObjectModel[] relatedObjects)
        {
            result?.Messages?.Add(new MessageModel(code, exception, relatedObjects: relatedObjects));

            return result;
        }

        /// <summary>
        ///     Customize result with custom error
        /// </summary>
        /// <param name="result">Current Result</param>
        /// <param name="error">Error detail</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithError<T>(this Result<T> result, ResultErrorModel error)
        {
            result?.Messages?.Add(error.MapToMessage());

            return result;
        }

        /// <summary>
        ///     Customize result with array of custom errors
        /// </summary>
        /// <param name="result">Current Result</param>
        /// <param name="errors">Error details</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithErrors<T>(this Result<T> result, params ResultErrorModel[] errors)
        {
            foreach (var error in errors)
                result?.Messages?.Add(error.MapToMessage());

            return result;
        }

        /// <summary>
        ///     Customize result with message with array of custom errors
        /// </summary>
        /// <param name="result">Current Result</param>
        /// <param name="errors">Error details</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of result</typeparam>
        /// <remarks></remarks>
        public static Result<T> WithErrors<T>(this Result<T> result, IEnumerable<ResultErrorModel> errors)
        {
            foreach (var error in errors)
                result?.Messages?.Add(error.MapToMessage());

            return result;
        }

        /// <summary>
        ///     An IResult extension method that add messages to the current result.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">Required. Current Result.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        public static Result<T> WithMessages<T>(
            this Result<T> result, 
            IEnumerable<IMessageModel> messages)
        {
            foreach (var message in messages)
            {
                result?.Messages.Add(message);
            }

            return result;
        }

        /// <summary>
        ///     An IResult extension method that add messages to the current result.
        /// </summary>
        /// <param name="result">Required. Current Result.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        public static AggregatedGenericResultMessage.Result WithMessages(
            this AggregatedGenericResultMessage.Result result, 
            IEnumerable<IMessageModel> messages)
        {
            foreach (var message in messages)
            {
                result?.Messages.Add(message);
            }

            return result;
        }

        /// <summary>
        ///     An IResult extension method that add messages to the current result.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">Required. Current Result.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        public static IResult<T> WithMessages<T>(
            this IResult<T> result, 
            IEnumerable<IMessageModel> messages)
        {
            foreach (var message in messages)
            {
                result?.Messages.Add(message);
            }

            return result;
        }

        /// <summary>
        ///     An IResult extension method that add messages to the current result.
        /// </summary>
        /// <param name="result">Required. Current Result.</param>
        /// <param name="messages">The messages.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        public static IResult WithMessages(
            this IResult result, 
            IEnumerable<IMessageModel> messages)
        {
            foreach (var message in messages)
            {
                result?.Messages.Add(message);
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Result&lt;T&gt; extension method that returns automatic success or failure.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">Required. Current Result.</param>
        /// <returns>
        ///     The automatic success or failure.
        /// </returns>
        /// =================================================================================================
        public static AggregatedGenericResultMessage.Result ReturnAutoSuccessOrFailure<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return AggregatedGenericResultMessage.Result
                    .Success()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
            else
            {
                return AggregatedGenericResultMessage.Result
                    .Failure()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Result&lt;T&gt; extension method that returns automatic success or failure.
        /// </summary>
        /// <param name="result">Required. Current Result.</param>
        /// <returns>
        ///     The automatic success or failure.
        /// </returns>
        /// =================================================================================================
        public static AggregatedGenericResultMessage.Result ReturnAutoSuccessOrFailure(
            this AggregatedGenericResultMessage.Result result)
        {
            if (result.IsSuccess)
            {
                return AggregatedGenericResultMessage.Result
                    .Success()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
            else
            {
                return AggregatedGenericResultMessage.Result
                    .Failure()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Result&lt;T&gt; extension method that returns automatic success or failure.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">Required. Current Result.</param>
        /// <returns>
        ///     The automatic success or failure.
        /// </returns>
        /// =================================================================================================
        public static AggregatedGenericResultMessage.Result ReturnAutoSuccessOrFailure<T>(this IResult<T> result)
        {
            if (result.IsSuccess)
            {
                return AggregatedGenericResultMessage.Result
                    .Success()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
            else
            {
                return AggregatedGenericResultMessage.Result
                    .Failure()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     A Result&lt;T&gt; extension method that returns automatic success or failure.
        /// </summary>
        /// <param name="result">Required. Current Result.</param>
        /// <returns>
        ///     The automatic success or failure.
        /// </returns>
        /// =================================================================================================
        public static AggregatedGenericResultMessage.Result ReturnAutoSuccessOrFailure(this IResult result)
        {
            if (result.IsSuccess)
            {
                return AggregatedGenericResultMessage.Result
                    .Success()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
            else
            {
                return AggregatedGenericResultMessage.Result
                    .Failure()
                    .WithMessages(result.Messages)
                    .ToBase();
            }
        }
    }
}
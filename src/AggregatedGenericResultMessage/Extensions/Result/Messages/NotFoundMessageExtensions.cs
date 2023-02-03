// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:06
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="NotFoundMessageExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Abstractions.MessageResults;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Result.Messages
{
    /// <summary>
    ///     Not found messages extensions
    /// </summary>
    public static class NotFoundMessageExtensions
    {
        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound()" />
        public static IResult<T> AddNotFound<T>(this IResult<T> result)
        {
            result.Messages?.Add(new MessageModel(null, null, MessageType.NotFound));

            return result;
        }

        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound()" />
        public static IResult<T> AddNotFound<T>(this Result<T> result)
        {
            result.Messages?.Add(new MessageModel(null, null, MessageType.NotFound));

            return result;
        }

        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound(string)" />
        public static IResult<T> AddNotFound<T>(this IResult<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(null, message, MessageType.NotFound));

            return result;
        }

        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound(string)" />
        public static IResult<T> AddNotFound<T>(this Result<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(null, message, MessageType.NotFound));

            return result;
        }

        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound(string, string)" />
        public static IResult<T> AddNotFound<T>(this IResult<T> result, string key, string message)
        {
            result.Messages?.Add(new MessageModel(key, message, MessageType.NotFound));

            return result;
        }

        /// <inheritdoc cref="INotFoundMessageResult{T}.AddNotFound(string, string)" />
        public static IResult<T> AddNotFound<T>(this Result<T> result, string key, string message)
        {
            result.Messages?.Add(new MessageModel(key, message, MessageType.NotFound));

            return result;
        }
    }
}
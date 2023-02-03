// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="AccessDeniedMessageExtensions.cs" company="">
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
    ///     Access denied messages extensions
    /// </summary>
    /// <remarks></remarks>
    public static class AccessDeniedMessageExtensions
    {
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied()" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result)
        {
            result.Messages?.Add(new MessageModel(null, null, MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied()" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result)
        {
            result.Messages?.Add(new MessageModel(null, null, MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(null, message, MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(null, message, MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string key, string message)
        {
            result.Messages?.Add(new MessageModel(key, message, MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string key, string message)
        {
            result.Messages?.Add(new MessageModel(key, message, MessageType.AccessDenied));

            return result;
        }
    }
}
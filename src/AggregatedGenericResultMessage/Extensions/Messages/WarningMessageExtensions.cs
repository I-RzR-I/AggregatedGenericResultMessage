// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:29
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="WarningMessageExtensions.cs" company="">
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

namespace AggregatedGenericResultMessage.Extensions.Messages
{
    /// <summary>
    ///     Warning message extensions
    /// </summary>
    /// <remarks></remarks>
    public static class WarningMessageExtensions
    {
        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(string.Empty, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(string.Empty, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string key, string warning)
        {
            result.Messages?.Add(new MessageModel(key, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string key, string warning)
        {
            result.Messages?.Add(new MessageModel(key, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(string.Empty, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(string.Empty, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string key, string warning)
        {
            result.Messages?.Add(new MessageModel(key, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string key, string warning)
        {
            result.Messages?.Add(new MessageModel(key, warning, MessageType.WarningConfirm));

            return result;
        }
    }
}
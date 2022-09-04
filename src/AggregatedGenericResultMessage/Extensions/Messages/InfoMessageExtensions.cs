// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 18:49
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="InfoMessageExtensions.cs" company="">
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
    /// </summary>
    /// <remarks></remarks>
    public static class InfoMessageExtensions
    {
        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfo(string)" />
        public static IResult<T> AddInfo<T>(this Result<T> result, string info)
        {
            result.Messages?.Add(new MessageModel(string.Empty, info, MessageType.Info));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfo(string)" />
        public static IResult<T> AddInfo<T>(this IResult<T> result, string info)
        {
            result.Messages?.Add(new MessageModel(string.Empty, info, MessageType.Info));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfo(string, string)" />
        public static IResult<T> AddInfo<T>(this Result<T> result, string key, string info)
        {
            result.Messages?.Add(new MessageModel(key, info, MessageType.Info));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfo(string, string)" />
        public static IResult<T> AddInfo<T>(this IResult<T> result, string key, string info)
        {
            result.Messages?.Add(new MessageModel(key, info, MessageType.Info));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfoConfirm(string)" />
        public static IResult<T> AddInfoConfirm<T>(this Result<T> result, string info)
        {
            result.Messages?.Add(new MessageModel(string.Empty, info, MessageType.InfoConfirm));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfoConfirm(string)" />
        public static IResult<T> AddInfoConfirm<T>(this IResult<T> result, string info)
        {
            result.Messages?.Add(new MessageModel(string.Empty, info, MessageType.InfoConfirm));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfoConfirm(string, string)" />
        public static IResult<T> AddInfoConfirm<T>(this Result<T> result, string key, string info)
        {
            result.Messages?.Add(new MessageModel(key, info, MessageType.InfoConfirm));

            return result;
        }

        /// <inheritdoc cref="IInfoMessageResult{T}.AddInfoConfirm(string, string)" />
        public static IResult<T> AddInfoConfirm<T>(this IResult<T> result, string key, string info)
        {
            result.Messages?.Add(new MessageModel(key, info, MessageType.InfoConfirm));

            return result;
        }
    }
}
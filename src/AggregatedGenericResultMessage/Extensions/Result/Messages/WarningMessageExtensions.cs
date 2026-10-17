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

using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Abstractions.MessageResults;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Models;

#endregion

namespace RzR.ResultMessage.Extensions.Result.Messages
{
    /// <summary>
    ///     Warning message extensions
    /// </summary>
    /// <remarks></remarks>
    public static class WarningMessageExtensions
    {
        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning()" />
        public static IResult<T> AddWarning<T>(this IResult<T> result)
        {
            result.Messages?.Add(new MessageModel(null, (string)null, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning()" />
        public static IResult<T> AddWarning<T>(this Result<T> result)
        {
            result.Messages?.Add(new MessageModel(null, (string)null, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(MessageDataModel)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(MessageDataModel)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this Result<T> result, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string code, string warning)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string code, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, MessageDataModel)" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string code, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this IResult<T> result, string code, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string code, string warning)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string code, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, MessageDataModel)" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string code, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.Warning));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarning(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarning<T>(this Result<T> result, string code, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.Warning, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(MessageDataModel)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string warning)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, new MessageDataModel(warning), MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(MessageDataModel)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, warning, MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string code, string warning)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string code, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, MessageDataModel)" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string code, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this IResult<T> result, string code, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string code, string warning)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string code, string warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, new MessageDataModel(warning), MessageType.WarningConfirm, relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, MessageDataModel)" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string code, MessageDataModel warning)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.WarningConfirm));

            return result;
        }

        /// <inheritdoc cref="IWarningMessageResult{T}.AddWarningConfirm(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddWarningConfirm<T>(this Result<T> result, string code, MessageDataModel warning, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code, warning, MessageType.WarningConfirm, relatedObjects));

            return result;
        }
    }
}
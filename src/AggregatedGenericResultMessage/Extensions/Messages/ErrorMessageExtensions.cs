// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:43
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="ErrorMessageExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Linq;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Abstractions.MessageResults;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Messages
{
    /// <summary>
    ///     Error result messages extensions
    /// </summary>
    /// <remarks></remarks>
    public static class ErrorMessageExtensions
    {
        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(string)" />
        public static IResult<T> AddError<T>(this IResult<T> result, string error)
        {
            result.Messages?.Add(new MessageModel(string.Empty, error));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(string)" />
        public static IResult<T> AddError<T>(this Result<T> result, string error)
        {
            result.Messages?.Add(new MessageModel(string.Empty, error));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(string, string)" />
        public static IResult<T> AddError<T>(this IResult<T> result, string key, string error)
        {
            result.Messages?.Add(new MessageModel(key, error));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(string, string)" />
        public static IResult<T> AddError<T>(this Result<T> result, string key, string error)
        {
            result.Messages?.Add(new MessageModel(key, error));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddErrorConfirm(string)" />
        public static IResult<T> AddErrorConfirm<T>(this IResult<T> result, string error)
        {
            result.Messages?.Add(new MessageModel(string.Empty, error, MessageType.ErrorConfirm));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddErrorConfirm(string)" />
        public static IResult<T> AddErrorConfirm<T>(this Result<T> result, string error)
        {
            result.Messages?.Add(new MessageModel(string.Empty, error, MessageType.ErrorConfirm));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddErrorConfirm(string, string)" />
        public static IResult<T> AddErrorConfirm<T>(this IResult<T> result, string key, string error)
        {
            result.Messages?.Add(new MessageModel(key, error, MessageType.ErrorConfirm));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddErrorConfirm(string, string)" />
        public static IResult<T> AddErrorConfirm<T>(this Result<T> result, string key, string error)
        {
            result.Messages?.Add(new MessageModel(key, error, MessageType.ErrorConfirm));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(Exception)" />
        public static IResult<T> AddError<T>(this IResult<T> result, Exception exception)
        {
            result.Messages?.Add(new MessageModel(string.Empty, exception.Message));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.AddError(Exception)" />
        public static IResult<T> AddError<T>(this Result<T> result, Exception exception)
        {
            result.Messages?.Add(new MessageModel(string.Empty, exception.Message));

            return result;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.GetFirstError" />
        public static string GetFirstError<T>(this IResult<T> result)
        {
            return result.Messages?.FirstOrDefault(x => x.MessageType == MessageType.Error)?.Message;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.GetFirstError" />
        public static string GetFirstError<T>(this Result<T> result)
        {
            return result.Messages?.FirstOrDefault(x => x.MessageType == MessageType.Error)?.Message;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.HasErrorCode(string)" />
        public static bool HasErrorCode<T>(this IResult<T> result, string errorCode)
        {
            return result.Messages?.Any(x => x.Key.Equals(errorCode)) ?? false;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.HasErrorCode(string)" />
        public static bool HasErrorCode<T>(this Result<T> result, string errorCode)
        {
            return result.Messages?.Any(x => x.Key.Equals(errorCode)) ?? false;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.HasAnyErrors" />
        public static bool HasAnyErrors<T>(this IResult<T> result)
        {
            return result.Messages?.Any(x => x.MessageType.Equals(MessageType.Error)) ?? false;
        }

        /// <inheritdoc cref="IErrorMessageResult{T}.HasAnyErrors" />
        public static bool HasAnyErrors<T>(this Result<T> result)
        {
            return result.Messages?.Any(x => x.MessageType.Equals(MessageType.Error)) ?? false;
        }
    }
}
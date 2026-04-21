// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-10-11 18:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-11 18:51
// ***********************************************************************
//  <copyright file="ExceptionMessageExtensions.cs" company="">
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
using System;
using System.Linq;

#endregion

namespace RzR.ResultMessage.Extensions.Result.Messages
{
    /// <summary>
    ///     Exception messages extensions
    /// </summary>
    /// <remarks></remarks>
    public static class ExceptionMessageExtensions
    {
        /// <inheritdoc cref="IExceptionMessageResult{T}.AddException(System.Exception)" />
        public static IResult<T> AddException<T>(this IResult<T> result, Exception exception)
        {
            result.Messages?.Add(new MessageModel(null, exception));

            return result;
        }
        /// <inheritdoc cref="IExceptionMessageResult{T}.AddException(Exception, RelatedObjectModel[])" />
        public static IResult<T> AddException<T>(this IResult<T> result, Exception exception, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, exception, relatedObjects: relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IExceptionMessageResult{T}.AddException(Exception)" />
        public static IResult<T> AddException<T>(this Result<T> result, Exception exception)
        {
            result.Messages?.Add(new MessageModel(null, exception));

            return result;
        }

        /// <inheritdoc cref="IExceptionMessageResult{T}.AddException(Exception, RelatedObjectModel[])" />
        public static IResult<T> AddException<T>(this Result<T> result, Exception exception, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(null, exception, relatedObjects: relatedObjects));

            return result;
        }

        /// <inheritdoc cref="IExceptionMessageResult{T}.HasAnyExceptions" />
        public static bool HasAnyExceptions<T>(this Result<T> result) 
            => result.Messages?.Any(x => x.MessageType.Equals(MessageType.Exception)) ?? false;

        /// <inheritdoc cref="IExceptionMessageResult{T}.HasAnyExceptions" />
        public static bool HasAnyExceptions<T>(this IResult<T> result)
            => result.Messages?.Any(x => x.MessageType.Equals(MessageType.Exception)) ?? false;
    }
}
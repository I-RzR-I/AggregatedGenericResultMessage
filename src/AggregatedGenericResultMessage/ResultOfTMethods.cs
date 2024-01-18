// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2024-01-18 23:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-18 23:34
// ***********************************************************************
//  <copyright file="ResultOfTMethods.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Helpers.Result;
using AggregatedGenericResultMessage.Models;
using System.Collections.Generic;

#endregion

#pragma warning disable CS1574

namespace AggregatedGenericResultMessage
{
    public partial class Result<T> : IResult<T>
    {
        /// <inheritdoc cref="ResultSuccessHelper.Success{T}()"/>
        public static Result<T> Success(T data = default)
            => ResultSuccessHelper.Success(data);

        /// <inheritdoc cref="ResultSuccessHelper.Success{T}(RelatedObjectModel[])"/>
        public static Result<T> Success(T data = default, params RelatedObjectModel[] relatedObjects)
            => ResultSuccessHelper.Success(data, relatedObjects);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}()"/>
        public static Result<T> Failure()
            => ResultFailureHelper.Failure<T>();

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string)"/>
        public static Result<T> Failure(string error)
            => ResultFailureHelper.Failure<T>(error);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string, RelatedObjectModel[])"/>
        public static Result<T> Failure(string error, params RelatedObjectModel[] relatedObjects)
            => ResultFailureHelper.Failure<T>(error, relatedObjects);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(MessageDataModel)"/>
        public static Result<T> Failure(MessageDataModel error)
            => ResultFailureHelper.Failure<T>(error);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> Failure(MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => ResultFailureHelper.Failure<T>(error, relatedObjects);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(IEnumerable{MessageDataModel})"/>
        public static Result<T> Failure(IEnumerable<MessageDataModel> errors)
            => ResultFailureHelper.Failure<T>(errors);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string, string)"/>
        public static Result<T> Failure(string code, string error)
            => ResultFailureHelper.Failure<T>(code, error);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string, string, RelatedObjectModel[])"/>
        public static Result<T> Failure(string code, string error, params RelatedObjectModel[] relatedObjects)
            => ResultFailureHelper.Failure<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string, MessageDataModel)"/>
        public static Result<T> Failure(string code, MessageDataModel error)
            => ResultFailureHelper.Failure<T>(code, error);

        /// <inheritdoc cref="ResultFailureHelper.Failure{T}(string, MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> Failure(string code, MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => ResultFailureHelper.Failure<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string)"/>
        public static Result<T> Warn(string warn)
            => ResultWarnHelper.Warn<T>(warn);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string, RelatedObjectModel[])"/>
        public static Result<T> Warn(string warn, params RelatedObjectModel[] relatedObjects)
            => ResultWarnHelper.Warn<T>(warn, relatedObjects);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(MessageDataModel)"/>
        public static Result<T> Warn(MessageDataModel warn)
            => ResultWarnHelper.Warn<T>(warn);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> Warn(MessageDataModel warn, params RelatedObjectModel[] relatedObjects)
            => ResultWarnHelper.Warn<T>(warn, relatedObjects);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string, string)"/>
        public static Result<T> Warn(string code, string error)
            => ResultWarnHelper.Warn<T>(code, error);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string, string, RelatedObjectModel[])"/>
        public static Result<T> Warn(string code, string error, params RelatedObjectModel[] relatedObjects)
            => ResultWarnHelper.Warn<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string, MessageDataModel)"/>
        public static Result<T> Warn(string code, MessageDataModel error)
            => ResultWarnHelper.Warn<T>(code, error);

        /// <inheritdoc cref="ResultWarnHelper.Warn{T}(string, MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> Warn(string code, MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => ResultWarnHelper.Warn<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string)"/>
        public static Result<T> AccessDenied(string message)
            => ResultAccessDeniedHelper.AccessDenied<T>(message);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string, RelatedObjectModel[])"/>
        public static Result<T> AccessDenied(string message, params RelatedObjectModel[] relatedObjects)
            => ResultAccessDeniedHelper.AccessDenied<T>(message, relatedObjects);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(MessageDataModel)"/>
        public static Result<T> AccessDenied(MessageDataModel message)
            => ResultAccessDeniedHelper.AccessDenied<T>(message);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> AccessDenied(MessageDataModel message, params RelatedObjectModel[] relatedObjects)
            => ResultAccessDeniedHelper.AccessDenied<T>(message, relatedObjects);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string, string)"/>
        public static Result<T> AccessDenied(string code, string error)
            => ResultAccessDeniedHelper.AccessDenied<T>(code, error);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string, string, RelatedObjectModel[])"/>
        public static Result<T> AccessDenied(string code, string error, params RelatedObjectModel[] relatedObjects)
            => ResultAccessDeniedHelper.AccessDenied<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string, MessageDataModel)"/>
        public static Result<T> AccessDenied(string code, MessageDataModel error)
            => ResultAccessDeniedHelper.AccessDenied<T>(code, error);

        /// <inheritdoc cref="ResultAccessDeniedHelper.AccessDenied{T}(string, MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> AccessDenied(string code, MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => ResultAccessDeniedHelper.AccessDenied<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string)"/>
        public static Result<T> NotFound(string message)
            => ResultNotFoundHelper.NotFound<T>(message);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string, RelatedObjectModel[])"/>
        public static Result<T> NotFound(string message, params RelatedObjectModel[] relatedObjects)
            => ResultNotFoundHelper.NotFound<T>(message, relatedObjects);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(MessageDataModel)"/>
        public static Result<T> NotFound(MessageDataModel message)
            => ResultNotFoundHelper.NotFound<T>(message);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> NotFound(MessageDataModel message, params RelatedObjectModel[] relatedObjects)
            => ResultNotFoundHelper.NotFound<T>(message, relatedObjects);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string, string)"/>
        public static Result<T> NotFound(string code, string error)
            => ResultNotFoundHelper.NotFound<T>(code, error);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string, string, RelatedObjectModel[])"/>
        public static Result<T> NotFound(string code, string error, params RelatedObjectModel[] relatedObjects)
            => ResultNotFoundHelper.NotFound<T>(code, error, relatedObjects);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string, MessageDataModel)"/>
        public static Result<T> NotFound(string code, MessageDataModel error)
            => ResultNotFoundHelper.NotFound<T>(code, error);

        /// <inheritdoc cref="ResultNotFoundHelper.NotFound{T}(string, MessageDataModel, RelatedObjectModel[])"/>
        public static Result<T> NotFound(string code, MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => ResultNotFoundHelper.NotFound<T>(code, error, relatedObjects);

    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 19:09
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:21
// ***********************************************************************
//  <copyright file="ResultFailureHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;
using System.Collections.Generic;

// ReSharper disable RedundantCast

#endregion

namespace AggregatedGenericResultMessage.Helpers.Result
{
    /// <summary>
    ///     Failure result helper
    /// </summary>
    internal static class ResultFailureHelper
    {
        /// <summary>
        ///     Failure
        /// </summary>
        /// <returns></returns>
        internal static Result<T> Failure<T>() 
            => (Result<T>) Result<T>.Instance; //.AddError();

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string error) 
            => (Result<T>) Result<T>.Instance.AddError(error);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string error, params RelatedObjectModel[] relatedObjects) 
            => (Result<T>) Result<T>.Instance.AddError(error, relatedObjects);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(MessageDataModel error) 
            => (Result<T>) Result<T>.Instance.AddError(error);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(MessageDataModel error, params RelatedObjectModel[] relatedObjects) 
            => (Result<T>) Result<T>.Instance.AddError(error, relatedObjects);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="errors">Error message</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(IEnumerable<MessageDataModel> errors) 
            => (Result<T>) Result<T>.Instance.AddErrors(errors);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string code, string error)
            => (Result<T>) Result<T>.Instance.AddError(code, error);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string code, string error, params RelatedObjectModel[] relatedObjects)
            => (Result<T>) Result<T>.Instance.AddError(code, error, relatedObjects);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string code, MessageDataModel error)
            => (Result<T>) Result<T>.Instance.AddError(code, error);

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        internal static Result<T> Failure<T>(string code, MessageDataModel error, params RelatedObjectModel[] relatedObjects)
            => (Result<T>) Result<T>.Instance.AddError(code, error, relatedObjects);
    }
}
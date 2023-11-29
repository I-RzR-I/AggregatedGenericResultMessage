// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 19:38
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:21
// ***********************************************************************
//  <copyright file="ResultNotFoundHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;

// ReSharper disable RedundantCast

#endregion

namespace AggregatedGenericResultMessage.Helpers.Result
{
    /// <summary>
    ///     Not found result helper
    /// </summary>
    internal static class ResultNotFoundHelper
    {
        /// <summary>
        ///     Not found
        /// </summary>
        /// <returns></returns>
        public static Result<T> NotFound<T>()
            => (Result<T>)Result<T>.Instance; //.AddNotFound();

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="message">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound<T>(string message)
            => (Result<T>)Result<T>.Instance.AddNotFound(message);

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="message">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound<T>(MessageDataModel message)
            => (Result<T>)Result<T>.Instance.AddNotFound(message);

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="code">Not found code</param>
        /// <param name="error">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound<T>(string code, string error)
            => (Result<T>)Result<T>.Instance.AddNotFound(code, error);

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="code">Not found code</param>
        /// <param name="error">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound<T>(string code, MessageDataModel error)
            => (Result<T>)Result<T>.Instance.AddNotFound(code, error);
    }
}
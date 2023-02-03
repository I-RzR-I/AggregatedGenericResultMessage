// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 02:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 18:01
// ***********************************************************************
//  <copyright file="ResultError.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using AggregatedGenericResultMessage.Abstractions.Models;

#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc cref="IResultError" />
    public class ResultError : IResultError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultError" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <remarks></remarks>
        public ResultError(string key, string message)
        {
            Key = key;
            Message = message;
            Exception = default;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultError" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <remarks></remarks>
        public ResultError(Exception exception)
        {
            Key = null;
            Message = null;
            Exception = exception;
        }

        /// <inheritdoc />
        public string Key { get; }

        /// <inheritdoc />
        public string Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <summary>
        ///     Map ResultError to MessageModel
        /// </summary>
        /// <returns>Mapped data</returns>
        /// <remarks></remarks>
        internal MessageModel MapToMessage()
        {
            return Exception != default
                ? new MessageModel(null, Exception)
                : new MessageModel(Key, Message);
        }
    }
}
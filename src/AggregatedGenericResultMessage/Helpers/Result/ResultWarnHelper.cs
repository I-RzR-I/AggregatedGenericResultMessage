// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 19:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:21
// ***********************************************************************
//  <copyright file="ResultWarnHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Extensions.Result.Messages;

// ReSharper disable RedundantCast

#endregion

namespace AggregatedGenericResultMessage.Helpers.Result
{
    /// <summary>
    ///     Result warning helper
    /// </summary>
    internal static class ResultWarnHelper
    {
        /// <summary>
        ///     Warning
        /// </summary>
        /// <returns></returns>
        internal static Result<T> Warn<T>() 
            => (Result<T>) Result<T>.Instance; //.AddWarning();

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="warn">Warning message</param>
        /// <returns></returns>
        internal static Result<T> Warn<T>(string warn) 
            => (Result<T>) Result<T>.Instance.AddWarning(warn);

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="code">Warning code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        internal static Result<T> Warn<T>(string code, string error)
            => (Result<T>) Result<T>.Instance.AddWarning(code, error);
    }
}
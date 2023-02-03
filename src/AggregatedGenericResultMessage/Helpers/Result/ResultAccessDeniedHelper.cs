// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 19:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:21
// ***********************************************************************
//  <copyright file="ResultAccessDeniedHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Extensions.Result.Messages;

#endregion

namespace AggregatedGenericResultMessage.Helpers.Result
{
    /// <summary>
    ///     Access denied result helper
    /// </summary>
    internal static class ResultAccessDeniedHelper
    {
        /// <summary>
        ///     Access denied
        /// </summary>
        /// <returns></returns>
        internal static Result<T> AccessDenied<T>()
        {
            return (Result<T>) Result<T>.Instance;//.AddAccessDenied();
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="message">Access denied message</param>
        /// <returns></returns>
        internal static Result<T> AccessDenied<T>(string message)
        {
            return (Result<T>) Result<T>.Instance.AddAccessDenied(message);
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="code">Access denied code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        internal static Result<T> AccessDenied<T>(string code, string error)
        {
            return (Result<T>) Result<T>.Instance.AddAccessDenied(code, error);
        }
    }
}
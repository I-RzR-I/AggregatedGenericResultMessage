// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:55
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:55
// ***********************************************************************
//  <copyright file="IMessageResults.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions.MessageResults;

#endregion

namespace AggregatedGenericResultMessage.Abstractions
{
    /// <summary>
    ///     Available result messages
    /// </summary>
    /// <typeparam name="T">Result type</typeparam>
    /// <remarks></remarks>
    public interface IMessageResults<T>
    {
        /// <summary>
        ///     Info message
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        IInfoMessageResult<T> Info();

        /// <summary>
        ///     Access denied message
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        IAccessDeniedMessageResult<T> AccessDenied();

        /// <summary>
        ///     Error message
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        IErrorMessageResult<T> Error();

        /// <summary>
        ///     Not found data message
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        INotFoundMessageResult<T> NotFound();

        /// <summary>
        ///     Warning message
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        IWarningMessageResult<T> Warning();
    }
}
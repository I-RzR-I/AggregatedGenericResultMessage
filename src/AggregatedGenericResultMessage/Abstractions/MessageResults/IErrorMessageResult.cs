// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 06:15
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="IErrorMessageResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    /// <summary>
    ///     Error message result
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    /// <remarks></remarks>
    public interface IErrorMessageResult<T>
    {
        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        IResult<T> AddError(string error);

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        IResult<T> AddError(string key, string error);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string error);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string key, string error);

        /// <summary>
        ///     Add exception error
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        IResult<T> AddError(Exception exception);

        /// <summary>
        ///     Check if has error
        /// </summary>
        /// <param name="errorCode">Error code</param>
        /// <returns></returns>
        bool HasErrorCode(string errorCode);

        /// <summary>
        ///     Check if in response persist any errors
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        bool HasAnyErrors();

        /// <summary>
        ///     Get first error from response
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetFirstError();

        /// <summary>
        ///     Check if in response persist any errors or exceptions
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        bool HasAnyErrorsOrExceptions();
    }
}
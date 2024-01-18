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

using AggregatedGenericResultMessage.Models;
using System;
using System.Collections.Generic;

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
        /// <returns></returns>
        IResult<T> AddError();


        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddError(string error);

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddError(string error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddError(MessageDataModel error);

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddError(MessageDataModel error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add errors
        /// </summary>
        /// <param name="errors">Error message</param>
        /// <returns></returns>
        IResult<T> AddErrors(IEnumerable<MessageDataModel> errors);
        

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddError(string key, string error);

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddError(string key, string error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddError(string key, MessageDataModel error);

        /// <summary>
        ///     Add error
        /// </summary>
        /// <param name="key"></param>
        /// <param name="error"></param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddError(string key, MessageDataModel error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string error);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(MessageDataModel error);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(MessageDataModel error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string key, string error, params RelatedObjectModel[] relatedObjects);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string key, string error);


        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string key, MessageDataModel error);

        /// <summary>
        ///     Add error for confirmation result
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="error">Error message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddErrorConfirm(string key, MessageDataModel error, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add exception error
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns></returns>
        IResult<T> AddError(Exception exception);

        /// <summary>
        ///     Add exception error
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddError(Exception exception, params RelatedObjectModel[] relatedObjects);


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
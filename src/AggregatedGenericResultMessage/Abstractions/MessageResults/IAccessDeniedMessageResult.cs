// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-06 08:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="IAccessDeniedMessageResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AggregatedGenericResultMessage.Models;

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    /// <summary>
    ///     Access denied result messages
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    public interface IAccessDeniedMessageResult<T>
    {
        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <returns></returns>
        IResult<T> AddAccessDenied();


        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string message, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(MessageDataModel message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(MessageDataModel message, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string key, string message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string key, string message, params RelatedObjectModel[] relatedObjects);


        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string key, MessageDataModel message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string key, MessageDataModel message, params RelatedObjectModel[] relatedObjects);
    }
}
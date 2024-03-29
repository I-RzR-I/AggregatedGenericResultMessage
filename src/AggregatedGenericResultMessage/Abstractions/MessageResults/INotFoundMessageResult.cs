﻿// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-06 08:17
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="INotFoundMessageResult.cs" company="">
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
    ///     Not found message result
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    public interface INotFoundMessageResult<T>
    {
        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <returns></returns>
        IResult<T> AddNotFound();

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string message, params RelatedObjectModel[] relatedObjects);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddNotFound(MessageDataModel message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddNotFound(MessageDataModel message, params RelatedObjectModel[] relatedObjects);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string key, string message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string key, string message, params RelatedObjectModel[] relatedObjects);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string key, MessageDataModel message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddNotFound(string key, MessageDataModel message, params RelatedObjectModel[] relatedObjects);
    }
}
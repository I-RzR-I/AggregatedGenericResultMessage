// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-10-11 18:42
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-11 18:51
// ***********************************************************************
//  <copyright file="IExceptionMessageResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Models;
using System;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    /// <summary>
    ///     Exception message extensions
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    /// <remarks></remarks>
    public interface IExceptionMessageResult<T>
    {
        /// <summary>
        ///     Add exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        IResult<T> AddException(Exception exception);
        /// <summary>
        ///     Add exception
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        IResult<T> AddException(Exception exception, params RelatedObjectModel[] relatedObjects);

        /// <summary>
        ///     Check if in response persist any exceptions
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        bool HasAnyExceptions();
    }
}
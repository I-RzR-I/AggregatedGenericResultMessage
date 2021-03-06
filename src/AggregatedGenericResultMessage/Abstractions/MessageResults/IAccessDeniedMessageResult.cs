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

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    public interface IAccessDeniedMessageResult<T>
    {
        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string message);

        /// <summary>
        ///     Add access denied message
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        IResult<T> AddAccessDenied(string key, string message);
    }
}
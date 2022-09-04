// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 05:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:55
// ***********************************************************************
//  <copyright file="IResultOfT.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Xml.Serialization;
using AggregatedGenericResultMessage.Enums;

#endregion

namespace AggregatedGenericResultMessage.Abstractions
{
    /// <inheritdoc cref="IResult" />
    [XmlInclude(typeof(Result<>))]
    public interface IResult<T> : IResult
    {
        /// <summary>
        ///     The result of the response, if there is no errors.
        /// </summary>
        T Response { get; set; }

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="key">Optional. Message key. The default value is null.</param>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        IResult<T> AddMessage(string key = null, string message = null, MessageType type = MessageType.Error);

        /// <summary>
        ///     Add message
        /// </summary>
        /// <param name="message">Optional. Message. The default value is null.</param>
        /// <param name="type">Optional. Message type. The default value is MessageType.Error.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        IResult<T> AddMessage(string message = null, MessageType type = MessageType.Error);

        /// <summary>
        ///     Get first message from response
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetFirstMessage();
    }
}
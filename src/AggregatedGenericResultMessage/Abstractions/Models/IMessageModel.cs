// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 04:09
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="IMessageModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S
#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif
using System.Xml.Serialization;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    /// <summary>
    ///     Message result model
    /// </summary>
    [XmlInclude(typeof(MessageModel))]
    public interface IMessageModel
    {
        /// <summary>
        ///     Gets or sets error code(key), localization key or Culture id/LCID or culture name like 'en-US', 'en'
        /// </summary>
#if !NETFRAMEWORK
        [JsonPropertyName("key")]
#endif
        string Key { get; set; }

        /// <summary>
        ///     Gets or sets detailed message
        /// </summary>
#if !NETFRAMEWORK
        [JsonPropertyName("message")]
#endif
        string Message { get; set; }

        /// <summary>
        ///     Gets or sets Message type.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
#if !NETFRAMEWORK
        [JsonPropertyName("messageType")]
#endif
        MessageType MessageType { get; set; }

        /// <summary>
        ///     Gets log trance id
        /// </summary>
#if !NETFRAMEWORK
        [JsonPropertyName("logTraceId")]
#endif
        string LogTraceId { get; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string ToString();
    }
}
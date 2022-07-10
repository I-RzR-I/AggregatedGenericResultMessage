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

using System.Xml.Serialization;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    /// <summary>
    /// </summary>
    [XmlInclude(typeof(MessageModel))]
    public interface IMessageModel
    {
        /// <summary>
        ///     Gets or sets error code(key), localization key or Culture id/LCID or culture name like 'en-US', 'en'
        /// </summary>
        string Key { get; set; }

        /// <summary>
        ///     Gets or sets detailed message
        /// </summary>
        string Message { get; set; }

        /// <summary>
        ///     Gets or sets Message type.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        MessageType MessageType { get; set; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string ToString();
    }
}
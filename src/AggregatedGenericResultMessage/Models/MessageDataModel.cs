// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-11-28 09:10
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-11-28 09:21
// ***********************************************************************
//  <copyright file="MessageDataModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Extensions.Common;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc cref="IMessageDataModel"/>
    public class MessageDataModel : IMessageDataModel
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>Default constructor.</summary>
        /// <remarks>RzR, 28-Nov-23.</remarks>
        ///=================================================================================================
        public MessageDataModel()
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Constructor.</summary>
        /// <remarks>RzR, 28-Nov-23.</remarks>
        /// <param name="info">The message.</param>
        ///=================================================================================================
        public MessageDataModel(string info) => Info = info;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Constructor.</summary>
        /// <remarks>RzR, 28-Nov-23.</remarks>
        /// <param name="info">The message.</param>
        /// <param name="details">The details.</param>
        ///=================================================================================================
        public MessageDataModel(string info, params string[] details)
        {
            Info = info;
            Details = (details ?? new string[] { }).ToList();
        }

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("info")]
#endif
        public string Info { get; set; }

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("details")]
#endif
        [XmlArray(ElementName = "Details")]
        [XmlArrayItem(ElementName = "Detail")]
        public List<string> Details { get; set; } = new List<string>();

        /// <inheritdoc cref="IMessageDataModel" />
        public override string ToString()
            => $"Message: {Info}{(Details.IsNotNull() && Details.Any() ? ($"; Details: {string.Join("#", Details)}") : "")}";
    }
}
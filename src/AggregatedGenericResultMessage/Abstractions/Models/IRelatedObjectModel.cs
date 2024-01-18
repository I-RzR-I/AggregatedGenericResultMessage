// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2024-01-17 11:07
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-17 11:07
// ***********************************************************************
//  <copyright file="IRelatedObjectModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

using AggregatedGenericResultMessage.Models;
using System.Xml.Serialization;

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>Interface for related object model.</summary>
    /// <remarks>17-Jan-24.</remarks>
    ///=================================================================================================
    [XmlInclude(typeof(RelatedObjectModel))]
    public interface IRelatedObjectModel
    {
        /// <summary>
        ///     Object name, used in source code
        /// </summary>
#if !NETFRAMEWORK
        [JsonPropertyName("inCodeName")]
#endif
        public string InCodeName { get; set; }

        /// <summary>
        ///     Object name used next to code (in data source)
        /// </summary>
        [XmlArray("InDataSourceNames")]
        [XmlArrayItem("InDataSourceName")]
#if !NETFRAMEWORK
        [JsonPropertyName("inDataSourceNames")]
#endif
        public string[] InDataSourceNames { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents this object.</returns>
        ///=================================================================================================
        string ToString();
    }
}
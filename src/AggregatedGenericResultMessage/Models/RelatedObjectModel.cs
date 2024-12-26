// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2024-01-17 09:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-17 09:54
// ***********************************************************************
//  <copyright file="RelatedObjectModel.cs" company="">
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

using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Extensions.Common;
using System.Xml.Serialization;

#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc cref="IRelatedObjectModel"/>
    [XmlInclude(typeof(RelatedObjectModel))]
    public class RelatedObjectModel : IRelatedObjectModel
    {
        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("inCodeName")]
#endif
        public string InCodeName { get; set; }

        /// <inheritdoc />
        [XmlArray("InDataSourceNames")]
        [XmlArrayItem("InDataSourceName")]
#if !NETFRAMEWORK
        [JsonPropertyName("inDataSourceNames")]
#endif
        public string[] InDataSourceNames { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelatedObjectModel"/> class.
        /// </summary>
        /// <remarks>17-Jan-24.</remarks>
        ///=================================================================================================
        public RelatedObjectModel() { }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Initializes a new instance of the <see cref="RelatedObjectModel"/> class.</summary>
        /// <remarks>17-Jan-24.</remarks>
        /// <param name="inCodeName">Name of the in code.</param>
        /// <param name="inDataSourceNames">(Optional) List of names of the in data sources.</param>
        ///=================================================================================================
        public RelatedObjectModel(string inCodeName, params string[] inDataSourceNames)
        {
            InCodeName = inCodeName;

            if (inDataSourceNames.IsNull())
                InDataSourceNames = inDataSourceNames;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelatedObjectModel"/> class.
        /// </summary>
        /// <remarks>17-Jan-24.</remarks>
        /// <param name="inDataSourceNames">List of names of the in data sources.</param>
        ///=================================================================================================
        public RelatedObjectModel(params string[] inDataSourceNames)
        {
            InCodeName = default;

            if (inDataSourceNames.IsNotNull())
                InDataSourceNames = inDataSourceNames;
        }

        /// <inheritdoc cref="IRelatedObjectModel"/>
        public override string ToString()
            => $"InCodeName: {InCodeName} <-> InDataSourceName: {string.Join("#", InDataSourceNames)}";
    }
}
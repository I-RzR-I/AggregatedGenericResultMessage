// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-12-27 15:31
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-12-29 01:50
// ***********************************************************************
//  <copyright file="SoapResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage
{
    /// <summary>
    ///     SOAP result
    /// </summary>
    /// <remarks></remarks>
    [Serializable]
    [DataContract(Name = "SoapResult")]
    [XmlRoot(IsNullable = false)]
    public class SoapResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.SoapResult" /> class.
        /// </summary>
        /// <remarks></remarks>
        public SoapResult()
        {
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance  is successfully executed.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance ; otherwise, <see langword="false" />.
        /// </value>
        /// <remarks></remarks>
        [DataMember(Name = "IsSuccess", IsRequired = true)]
        public bool IsSuccess { get; set; }

        /// <summary>
        ///     Gets or sets result messages.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        [DataMember(Name = "Messages", IsRequired = false)]
        [XmlArray(ElementName = "Messages", IsNullable = true)]
        [XmlArrayItemAttribute("Message", IsNullable = true, Type = typeof(MessageModel))]
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();

        /// <summary>
        ///     Gets or sets result.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        [DataMember(Name = "Response", IsRequired = false)]
        public XmlElement Response { get; set; }

        /// <summary>
        ///     Soap result instance
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static SoapResult Instance()
        {
            return new SoapResult();
        }
    }
}
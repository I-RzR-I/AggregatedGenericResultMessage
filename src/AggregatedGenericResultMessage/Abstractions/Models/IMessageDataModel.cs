// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-11-28 21:01
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-11-28 21:05
// ***********************************************************************
//  <copyright file="IMessageDataModel.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Models;
using System.Collections.Generic;
using System.Xml.Serialization;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>Interface for message data model.</summary>
    /// <remarks>RzR, 28-Nov-23.</remarks>
    /// =================================================================================================
    [XmlInclude(typeof(MessageDataModel))]
    public interface IMessageDataModel
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the message. The main result message</summary>
        /// <value>The message.</value>
        /// =================================================================================================
        public string Info { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the message details.</summary>
        /// <value>The details.</value>
        /// =================================================================================================
        public List<string> Details { get; set; }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Convert this object into a string representation.</summary>
        /// <returns>A string that represents this object.</returns>
        /// =================================================================================================
        string ToString();
    }
}
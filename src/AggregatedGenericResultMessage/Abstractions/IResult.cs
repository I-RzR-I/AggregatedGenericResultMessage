// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-06-30 18:36
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:55
// ***********************************************************************
//  <copyright file="IResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Xml.Serialization;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Abstractions
{
    /// <summary>
    ///     IResult message
    /// </summary>
    [XmlInclude(typeof(Result))]
    public interface IResult
    {
        /// <summary>
        ///     Bool indicating that the request resulted with success.
        ///     If 'False', then the <see cref="Messages" /> will
        ///     contain an Error message that produced this error.
        /// </summary>
        bool IsSuccess { get; set; }

        /// <summary>
        ///     Gets a value indicating whether this object is failure.
        /// </summary>
        /// <value>
        ///     True if this object is failure, false if not.
        /// </value>
        bool IsFailure { get; }

        /// <summary>
        ///     This property will contain messages keys if any.
        /// </summary>
        [XmlArray("Messages")]
        [XmlArrayItem("Message")]
        ICollection<IMessageModel> Messages { get; set; }

        /// <summary>
        ///     Get as base
        /// </summary>
        /// <returns></returns>
        Result ToBase();

        /// <summary>
        ///     Get result for SOAP services
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        SoapResult ToSoapResult();

        /// <summary>
        ///     Get first message from response
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        string GetFirstMessage();
        
        /// <summary>
        ///     Get first message from response
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        MessageDataModel GetFirstMessageWithDetails();
    }
}
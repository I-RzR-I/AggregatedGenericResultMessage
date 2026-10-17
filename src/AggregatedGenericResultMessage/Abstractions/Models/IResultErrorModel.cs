// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 17:56
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 18:04
// ***********************************************************************
//  <copyright file="IResultError.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Helpers;
using RzR.ResultMessage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

#endregion

namespace RzR.ResultMessage.Abstractions.Models
{
    /// <summary>
    ///     Result error.
    /// </summary>
    public interface IResultErrorModel
    {
        /// <summary>Gets the error code (identifier of the error).</summary>
        /// <value>The code.</value>
        string Code { get; }

        /// <summary>
        ///     Gets the error code. Obsolete alias of <see cref="Code"/>. 
        ///     Excluded from System.Text.Json (netstandard targets), XmlSerializer, 
        ///     DataContractSerializer and Newtonsoft.Json output.
        /// </summary>
        /// <value>The code.</value>
        [Obsolete(ObsoleteMessages.KeyToCode)]
        [EditorBrowsable(EditorBrowsableState.Never)]
#if !NETFRAMEWORK
        [JsonIgnore]
#endif
        string Key { get; }

        /// <summary>
        ///     Gets error message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        MessageDataModel Message { get; }

        /// <summary>
        ///     Gets current execution code exception.
        /// </summary>
        /// <value>
        ///     The exception.
        /// </value>
        Exception Exception { get; }

        /// <summary>
        ///     Gets or sets the related objects.
        /// </summary>
        /// <value>
        ///     The related objects.
        /// </value>
        ICollection<RelatedObjectModel> RelatedObjects { get; set; }
    }
}
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

using AggregatedGenericResultMessage.Models;
using System;
using System.Collections.Generic;

#endregion

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>Result error.</summary>
    /// <remarks>17-Jan-24.</remarks>
    ///=================================================================================================
    public interface IResultErrorModel
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets error key.</summary>
        /// <value>The key.</value>
        ///=================================================================================================
        string Key { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets error message.</summary>
        /// <value>The message.</value>
        ///=================================================================================================
        MessageDataModel Message { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets current execution code exception.</summary>
        /// <value>The exception.</value>
        ///=================================================================================================
        Exception Exception { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the related objects.</summary>
        /// <value>The related objects.</value>
        ///=================================================================================================
        ICollection<RelatedObjectModel> RelatedObjects { get; set; }
    }
}
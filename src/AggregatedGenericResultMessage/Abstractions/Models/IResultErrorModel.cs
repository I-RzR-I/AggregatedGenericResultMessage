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

#endregion

namespace AggregatedGenericResultMessage.Abstractions.Models
{
    /// <summary>
    ///     Result error
    /// </summary>
    public interface IResultErrorModel
    {
        /// <summary>
        ///     Gets error key.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        string Key { get; }

        /// <summary>
        ///     Gets error message.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        MessageDataModel Message { get; }

        /// <summary>
        ///     Gets current execution code exception.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        Exception Exception { get; }
    }
}
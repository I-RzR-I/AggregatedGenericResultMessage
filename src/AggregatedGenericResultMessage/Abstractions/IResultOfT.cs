﻿// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 05:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:55
// ***********************************************************************
//  <copyright file="IResultOfT.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Xml.Serialization;

#endregion

namespace AggregatedGenericResultMessage.Abstractions
{
    /// <inheritdoc cref="IResult" />
    [XmlInclude(typeof(Result<>))]
    public interface IResult<T> : IResult
    {
        /// <summary>
        ///     The result of the response, if there is no errors.
        /// </summary>
        T Response { get; set; }
    }
}
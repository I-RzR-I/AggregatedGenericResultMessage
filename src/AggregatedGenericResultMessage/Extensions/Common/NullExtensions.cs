// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-02 14:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-02 15:00
// ***********************************************************************
//  <copyright file="NullExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using CodeSource;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Common
{
    /// <summary>
    ///     Null extensions
    /// </summary>
    /// <remarks></remarks>
    [CodeSource("DomainCommonExtensions.CommonExtensions", "RzR", "RzR", 1)]
    public static class NullExtensions
    {
        /// <summary>
        ///     Is null
        /// </summary>
        /// <param name="obj">Source data</param>
        /// <returns></returns>
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR",
            "DomainCommonExtensions.CommonExtensions.NullExtensions.IsNull", 1)]
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
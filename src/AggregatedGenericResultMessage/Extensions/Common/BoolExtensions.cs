// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2024-01-31 22:55
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-31 22:58
// ***********************************************************************
//  <copyright file="BoolExtensions.cs" company="">
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
    /// -------------------------------------------------------------------------------------------------
    /// <summary>An extensions.</summary>
    /// <remarks>31-Jan-24.</remarks>
    /// =================================================================================================
    internal static class BoolExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>Check if source value is equals with true.</summary>
        /// <remarks></remarks>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>True if true, false if not.</returns>
        /// =================================================================================================
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR", 1D)]
        internal static bool IsTrue(this bool source)
            => source.IsNotNull() && source.Equals(true);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Check if source value is equals with false.</summary>
        /// <remarks></remarks>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>True if false, false if not.</returns>
        /// =================================================================================================
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR", 1D)]
        internal static bool IsFalse(this bool source)
            => source.IsNull() || source.Equals(false);
    }
}
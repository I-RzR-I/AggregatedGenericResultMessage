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

using RzR.Core.CodeSource;

#endregion

namespace RzR.ResultMessage.Extensions.Common
{
    /// <summary>
    ///     An extensions.
    /// </summary>
    internal static class BoolExtensions
    {
        /// <summary>
        ///     Check if source value is equals with true.
        /// </summary>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>
        ///     True if true, false if not.
        /// </returns>
        [CodeSource(SourceUrl = "https://github.com/I-RzR-I/DomainCommonExtensions", AuthorName = "RzR", Version = "1.0")]
        internal static bool IsTrue(this bool source)
            => source.IsNotNull() && source.Equals(true);

        /// <summary>
        ///     Check if source value is equals with false.
        /// </summary>
        /// <param name="source">Source object to be checked.</param>
        /// <returns>
        ///     True if false, false if not.
        /// </returns>
        [CodeSource(SourceUrl = "https://github.com/I-RzR-I/DomainCommonExtensions", AuthorName = "RzR", Version = "1.0")]
        internal static bool IsFalse(this bool source)
            => source.IsNull() || source.Equals(false);
    }
}
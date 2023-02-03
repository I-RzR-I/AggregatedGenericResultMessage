// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-02 14:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-02 14:59
// ***********************************************************************
//  <copyright file="StringExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Text;
using CodeSource;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Common
{
    /// <summary>
    ///     String extensions
    /// </summary>
    /// <remarks></remarks>
    [CodeSource("DomainCommonExtensions.CommonExtensions", "RzR", "RzR", 1)]
    public static class StringExtensions
    {
        /// <summary>
        ///     Is Null or empty snippet
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR",
            "DomainCommonExtensions.CommonExtensions.StringExtensions.IsNullOrEmpty", 1)]
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        ///     Cast string to enum value
        /// </summary>
        /// <param name="value">Input string to cast</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of Enum</typeparam>
        /// <remarks></remarks>
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR",
            "DomainCommonExtensions.CommonExtensions.StringExtensions.ToEnum", 1)]
        public static T ToEnum<T>(this string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            try
            {
                return (T) Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     Encode string to BASE 64
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF7.GetBytes(plainText);

            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-10 11:59
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
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

#endregion

namespace AggregatedGenericResultMessage.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Cast string to enum value
        /// </summary>
        /// <param name="value">Input string to cast</param>
        /// <returns></returns>
        /// <typeparam name="T">Type of Enum</typeparam>
        /// <remarks></remarks>
        public static T ToEnum<T>(this string value)
        {
            return (T) Enum.Parse(typeof(T), value, true);
        }
    }
}
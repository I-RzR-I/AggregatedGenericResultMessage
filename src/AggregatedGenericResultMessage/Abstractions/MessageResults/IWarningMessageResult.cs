// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 06:28
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="IWarningMessageResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    /// <summary>
    ///     Warning message result
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    /// <remarks></remarks>
    public interface IWarningMessageResult<T>
    {
        /// <summary>
        ///     Add Warning
        /// </summary>
        /// <returns></returns>
        IResult<T> AddWarning();

        /// <summary>
        ///     Add Warning
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        IResult<T> AddWarning(string warning);

        /// <summary>
        ///     Add Warning
        /// </summary>
        /// <param name="key"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        IResult<T> AddWarning(string key, string warning);

        /// <summary>
        ///     Add Warning for confirmation result
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        IResult<T> AddWarningConfirm(string warning);

        /// <summary>
        ///     Add Warning for confirmation result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="warning"></param>
        /// <returns></returns>
        IResult<T> AddWarningConfirm(string key, string warning);
    }
}
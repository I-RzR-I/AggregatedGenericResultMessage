// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-01 06:24
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="IInfoMessageResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace AggregatedGenericResultMessage.Abstractions.MessageResults
{
    /// <summary>
    ///     Information message result
    /// </summary>
    /// <typeparam name="T">Type of result/response</typeparam>
    /// <remarks></remarks>
    public interface IInfoMessageResult<T>
    {
        /// <summary>
        ///     Add Info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IResult<T> AddInfo(string info);

        /// <summary>
        ///     Add Info
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        IResult<T> AddInfo(string key, string info);

        /// <summary>
        ///     Add Info for confirmation result
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IResult<T> AddInfoConfirm(string info);

        /// <summary>
        ///     Add Info for confirmation result
        /// </summary>
        /// <param name="key"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        IResult<T> AddInfoConfirm(string key, string info);
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 18:25
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 20:21
// ***********************************************************************
//  <copyright file="ResultSuccessHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable ClassNeverInstantiated.Global

using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;
using System.Collections.Generic;
using System.Linq;

namespace AggregatedGenericResultMessage.Helpers.Result
{
    /// <summary>
    ///     Result success helper
    /// </summary>
    internal static class ResultSuccessHelper
    {
        /// <summary>
        ///     Success response
        /// </summary>
        /// <param name="data">Response data</param>
        /// <returns></returns>
        internal static Result<T> Success<T>(T data = default)
        {
            var result = Result<T>.Instance;
            result.IsSuccess = true;
            result.Response = data;

            return result;
        }

        /// <summary>
        ///     Success response
        /// </summary>
        /// <param name="data">Response data</param>
        /// <param name="relatedObjects">Related objects</param>
        /// <returns></returns>
        internal static Result<T> Success<T>(T data = default, params RelatedObjectModel[] relatedObjects)
        {
            var result = Result<T>.Instance;
            result.IsSuccess = true;
            result.Response = data;
            result.Messages = new List<IMessageModel>() { new MessageModel() { RelatedObjects = relatedObjects.ToList() } };

            return result;
        }
    }
}
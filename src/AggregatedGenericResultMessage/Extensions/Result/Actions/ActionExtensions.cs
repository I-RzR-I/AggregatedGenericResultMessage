// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-04-02 18:49
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 00:18
// ***********************************************************************
//  <copyright file="ActionExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Models;
using System;

#endregion

namespace RzR.ResultMessage.Extensions.Result.Actions
{
    /// <summary>
    ///     Result action extensions
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        ///     Execute action
        /// </summary>
        /// <typeparam name="TResult">Type result</typeparam>
        /// <param name="result">Result</param>
        /// <param name="actions">Actions to execute</param>
        /// <returns></returns>
        public static TResult ExecuteAction<TResult>(this TResult result, params Action<TResult>[] actions)
            where TResult : IResult
        {
            try
            {
                if (actions.IsNull())
                    return result;

                foreach (var action in actions)
                    action.Invoke(result);

                return result;
            }
            catch (Exception e)
            {
                result.Messages.Add(new MessageModel(string.Empty, e));
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
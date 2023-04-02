// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-04-02 18:49
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-04-02 22:09
// ***********************************************************************
//  <copyright file="ActionExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Extensions.Common;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Result.Actions
{
    /// <summary>
    ///     Result action extensions
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        ///     Action on success
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="result">Result</param>
        /// <param name="onSuccess">Success action</param>
        public static TResult ActionOnSuccess<TResult>(this TResult result, params Action<TResult>[] onSuccess)
            where TResult : IResult
        {
            if (!result.IsSuccess)
                return result;
            foreach (var action in onSuccess)
                action?.Invoke(result);

            return result;
        }

        /// <summary>
        ///     Action on failure
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="result">Result</param>
        /// <param name="onFailure">Failure action</param>
        public static TResult ActionOnFailure<TResult>(this TResult result, params Action<TResult>[] onFailure)
            where TResult : IResult
        {
            if (result.IsSuccess)
                return result;
            if (onFailure.IsNull())
                return result;
            foreach (var action in onFailure)
                action?.Invoke(result);

            return result;
        }

        /// <summary>
        ///     Action on success or failure
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="onSuccess">Action on success</param>
        /// <param name="onFailure">Action on failure</param>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <remarks></remarks>
        public static TResult ActionOn<TResult>(this TResult result, Action<TResult> onSuccess, Action<TResult> onFailure)
            where TResult : IResult
        {
            if (!result.IsSuccess)
            {
                if (onFailure.IsNull())
                    return result;
                onFailure?.Invoke(result);

                return result;
            }

            if (onSuccess.IsNull())
                return result;
            onSuccess?.Invoke(result);

            return result;
        }

        /// <summary>
        ///     Action on success or failure
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="onSuccess">Action on success</param>
        /// <param name="onFailure">Action on failure</param>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <remarks></remarks>
        public static TResult ActionOn<TResult>(this TResult result, Action<TResult> onSuccess, IEnumerable<Action<TResult>> onFailure)
            where TResult : IResult
        {
            if (!result.IsSuccess)
            {
                if (onFailure.IsNull())
                    return result;
                foreach (var action in onFailure)
                    action?.Invoke(result);

                return result;
            }

            if (onSuccess.IsNull())
                return result;
            onSuccess?.Invoke(result);

            return result;
        }

        /// <summary>
        ///     Action on success or failure
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="onSuccess">Action on success</param>
        /// <param name="onFailure">Action on failure</param>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <remarks></remarks>
        public static TResult ActionOn<TResult>(this TResult result, IEnumerable<Action<TResult>> onSuccess, Action<TResult> onFailure)
            where TResult : IResult
        {
            if (!result.IsSuccess)
            {
                if (onFailure.IsNull())
                    return result;
                onFailure?.Invoke(result);

                return result;
            }

            if (onSuccess.IsNull())
                return result;
            foreach (var action in onSuccess)
                action?.Invoke(result);

            return result;
        }

        /// <summary>
        ///     Action on success or failure
        /// </summary>
        /// <param name="result">Result</param>
        /// <param name="onSuccess">Action on success</param>
        /// <param name="onFailure">Action on failure</param>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <remarks></remarks>
        public static TResult ActionOn<TResult>(this TResult result, IEnumerable<Action<TResult>> onSuccess, IEnumerable<Action<TResult>> onFailure)
            where TResult : IResult
        {
            if (!result.IsSuccess)
            {
                if (onFailure.IsNull())
                    return result;
                foreach (var action in onFailure)
                    action?.Invoke(result);

                return result;
            }

            if (onSuccess.IsNull())
                return result;
            foreach (var action in onSuccess)
                action?.Invoke(result);

            return result;
        }

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
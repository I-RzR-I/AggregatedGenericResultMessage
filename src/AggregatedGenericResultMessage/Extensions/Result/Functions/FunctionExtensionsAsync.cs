// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2026-04-21 20:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-21 22:29
// ***********************************************************************
//  <copyright file="FunctionExtensionsAsync.cs" company="RzR SOFT & TECH">
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
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace RzR.ResultMessage.Extensions.Result.Functions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Async function extensions for <see cref="IResult" />-based pipelines.
    /// </summary>
    /// <remarks>
    ///     All methods use <c>ConfigureAwait(false)</c> so they can be safely awaited from library
    ///     code without capturing a <see cref="System.Threading.SynchronizationContext" />.
    /// </remarks>
    /// =================================================================================================
    public static class FunctionExtensionsAsync
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on success (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <param name="onSuccess">The on success.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnSuccessAsync<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] onSuccess) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
                return result;

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invoke = await func.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on failure (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <param name="onFailure">The on failure.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnFailureAsync<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] onFailure) where TResult : IResult
        {
            if (result.IsSuccess)
                return result;

            if (onFailure.IsNull())
                return result;

            foreach (var func in onFailure)
            {
                var invoke = await func.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on success or failure (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="onSuccess">The on success.</param>
        /// <param name="onFailure">The on failure.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnAsync<TResult>(
            this TResult result, Func<Task<TResult>> onSuccess,
            Func<Task<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = await onFailure.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = await onSuccess.Invoke().ConfigureAwait(false);
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on success or failure (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="onSuccess">The on success.</param>
        /// <param name="onFailure">The on failure.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnAsync<TResult>(
            this TResult result, Func<Task<TResult>> onSuccess,
            IEnumerable<Func<Task<TResult>>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                foreach (var func in onFailure)
                {
                    var invokeFailure = await func.Invoke().ConfigureAwait(false);
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = await onSuccess.Invoke().ConfigureAwait(false);
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on success or failure (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="onSuccess">The on success.</param>
        /// <param name="onFailure">The on failure.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnAsync<TResult>(
            this TResult result, IEnumerable<Func<Task<TResult>>> onSuccess,
            Func<Task<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = await onFailure.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = await func.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Function on success or failure (async).
        /// </summary>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="onSuccess">The on success.</param>
        /// <param name="onFailure">The on failure.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> FunctionOnAsync<TResult>(
            this TResult result, IEnumerable<Func<Task<TResult>>> onSuccess,
            IEnumerable<Func<Task<TResult>>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                foreach (var func in onFailure)
                {
                    var invokeFailure = await func.Invoke().ConfigureAwait(false);
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = await func.Invoke().ConfigureAwait(false);
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Execute function (async).
        /// </summary>
        /// <remarks>
        ///     Captures any thrown <see cref="Exception" />, appends it to <see cref="IResult.Messages" />
        ///     and marks the result as failed. Mirrors the behavior of the synchronous
        ///     <see cref="FunctionExtensions.ExecuteFunction{TResult}(TResult, bool, Func{TResult}[])" />
        ///     .
        /// </remarks>
        /// <typeparam name="TResult">Type of the result.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="ignoreError">(Optional) True to ignore error.</param>
        /// <param name="functions">A variable-length parameters list containing functions.</param>
        /// <returns>
        ///     A TResult.
        /// </returns>
        /// =================================================================================================
        public static async Task<TResult> ExecuteFunctionAsync<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] functions) where TResult : IResult
        {
            try
            {
                if (functions.IsNull())
                    return result;

                foreach (var func in functions)
                {
                    var invoke = await func.Invoke().ConfigureAwait(false);
                    if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                        return invoke;
                }

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
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2024-01-31 19:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-02-01 00:21
// ***********************************************************************
//  <copyright file="FunctionExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
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
    /// <summary>A function extensions.</summary>
    /// <remarks></remarks>
    /// =================================================================================================
    public static class FunctionExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="onSuccess">Success function.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOnSuccess<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<TResult>[] onSuccess)
            where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
                return result;

            foreach (var func in onSuccess)
            {
                var invoke = func.Invoke();
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="onSuccess">Success function.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnSuccessAsync instead.")]
        public static TResult FunctionOnSuccess<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] onSuccess) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
                return result;

            foreach (var func in onSuccess)
            {
                var invoke = func.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="onFailure">Failure function.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOnFailure<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<TResult>[] onFailure) where TResult : IResult
        {
            if (result.IsSuccess)
                return result;

            if (onFailure.IsNull())
                return result;

            foreach (var func in onFailure)
            {
                var invoke = func.Invoke();
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="onFailure">Failure function.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnFailureAsync instead.")]
        public static TResult FunctionOnFailure<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] onFailure) where TResult : IResult
        {
            if (result.IsSuccess)
                return result;

            if (onFailure.IsNull())
                return result;

            foreach (var func in onFailure)
            {
                var invoke = func.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invoke.IsSuccess.IsFalse())
                    return invoke;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOn<TResult>(
            this TResult result, Func<TResult> onSuccess,
            Func<TResult> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = onFailure.Invoke();
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = onSuccess.Invoke();
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnAsync instead.")]
        public static TResult FunctionOn<TResult>(
            this TResult result, Func<Task<TResult>> onSuccess,
            Func<Task<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = onFailure.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = onSuccess.Invoke().GetAwaiter().GetResult();
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOn<TResult>(
            this TResult result, Func<TResult> onSuccess,
            IEnumerable<Func<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                foreach (var func in onFailure)
                {
                    var invokeFailure = func.Invoke();
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = onSuccess.Invoke();
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnAsync instead.")]
        public static TResult FunctionOn<TResult>(
            this TResult result, Func<Task<TResult>> onSuccess,
            IEnumerable<Func<Task<TResult>>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;
                foreach (var func in onFailure)
                {
                    var invokeFailure = func.Invoke().GetAwaiter().GetResult();
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            var invokeSuccess = onSuccess.Invoke().GetAwaiter().GetResult();
            if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                return invokeSuccess;

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOn<TResult>(
            this TResult result, IEnumerable<Func<TResult>> onSuccess,
            Func<TResult> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = onFailure.Invoke();
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = func.Invoke();
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnAsync instead.")]
        public static TResult FunctionOn<TResult>(
            this TResult result, IEnumerable<Func<Task<TResult>>> onSuccess,
            Func<Task<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                var invokeFailure = onFailure.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                    return invokeFailure;

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = func.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult FunctionOn<TResult>(
            this TResult result, IEnumerable<Func<TResult>> onSuccess,
            IEnumerable<Func<TResult>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                foreach (var func in onFailure)
                {
                    var invokeFailure = func.Invoke();
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = func.Invoke();
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Function on success or failure.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="onSuccess">Function on success.</param>
        /// <param name="onFailure">Function on failure.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use FunctionOnAsync instead.")]
        public static TResult FunctionOn<TResult>(
            this TResult result, IEnumerable<Func<Task<TResult>>> onSuccess,
            IEnumerable<Func<Task<TResult>>> onFailure, bool ignoreError = true) where TResult : IResult
        {
            if (result.IsSuccess.IsFalse())
            {
                if (onFailure.IsNull())
                    return result;

                foreach (var func in onFailure)
                {
                    var invokeFailure = func.Invoke().GetAwaiter().GetResult();
                    if (ignoreError.IsFalse() && invokeFailure.IsSuccess.IsFalse())
                        return invokeFailure;
                }

                return result;
            }

            if (onSuccess.IsNull())
                return result;

            foreach (var func in onSuccess)
            {
                var invokeSuccess = func.Invoke().GetAwaiter().GetResult();
                if (ignoreError.IsFalse() && invokeSuccess.IsSuccess.IsFalse())
                    return invokeSuccess;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Execute function.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="functions">Functions to execute.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        public static TResult ExecuteFunction<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<TResult>[] functions) where TResult : IResult
        {
            try
            {
                if (functions.IsNull())
                    return result;

                foreach (var func in functions)
                {
                    var invoke = func.Invoke();
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

        /// -------------------------------------------------------------------------------------------------
        /// <summary>Execute function.</summary>
        /// <remarks></remarks>
        /// <typeparam name="TResult">Type result.</typeparam>
        /// <param name="result">Result.</param>
        /// <param name="ignoreError">(Optional)</param>
        /// <param name="functions">Functions to execute.</param>
        /// <returns>A TResult.</returns>
        /// =================================================================================================
        [Obsolete("This overload blocks on Task with .GetAwaiter().GetResult() and may deadlock under a SynchronizationContext. Use ExecuteFunctionAsync instead.")]
        public static TResult ExecuteFunction<TResult>(
            this TResult result,
            bool ignoreError = true, params Func<Task<TResult>>[] functions) where TResult : IResult
        {
            try
            {
                if (functions.IsNull())
                    return result;

                foreach (var func in functions)
                {
                    var invoke = func.Invoke().GetAwaiter().GetResult();
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
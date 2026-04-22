// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2026-04-22 00:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 01:19
// ***********************************************************************
//  <copyright file="ResultMatchExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Common;
using System;
using System.Threading.Tasks;

#endregion

namespace RzR.ResultMessage.Extensions.Result
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Fold/branch combinators for result pipelines.
    /// </summary>
    /// =================================================================================================
    public static class ResultMatchExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Folds an <see cref="IResult" /> into a <typeparamref name="TOut" /> value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="TOut">Type of the folded value.</typeparam>
        /// <param name="result">Result to fold.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when <paramref name="result" /> is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when <paramref name="result" /> is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static TOut Match<TOut>(
            this IResult result,
            Func<IResult, TOut> onSuccess,
            Func<IResult, TOut> onFailure)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            return result.IsSuccess ? onSuccess(result) : onFailure(result);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Folds an <see cref="IResult" /> into a <typeparamref name="TOut" /> value using
        ///     parameterless selectors.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="result">Result to fold.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when <paramref name="result" /> is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when <paramref name="result" /> is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static TOut Match<TOut>(
            this IResult result,
            Func<TOut> onSuccess,
            Func<TOut> onFailure)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            return result.IsSuccess ? onSuccess() : onFailure();
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Runs one of two side-effecting branches depending on <see cref="IResult.IsSuccess" />.
        ///     Prefer this over <c>ActionOn</c> when you do not need the source result back.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="result">Result to fold.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when <paramref name="result" /> is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when <paramref name="result" /> is a failure.
        /// </param>
        /// <returns>
        ///     The same <paramref name="result" /> instance for chaining.
        /// </returns>
        /// =================================================================================================
        public static IResult Match(
            this IResult result,
            Action<IResult> onSuccess,
            Action<IResult> onFailure)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            if (result.IsSuccess)
                onSuccess(result);
            else
                onFailure(result);

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Asynchronous fold of a <see cref="Result{T}" />.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="result">Result to fold.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when <paramref name="result" /> is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when <paramref name="result" /> is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static async Task<TOut> MatchAsync<T, TOut>(
            this Result<T> result,
            Func<T, Task<TOut>> onSuccess,
            Func<Result<T>, Task<TOut>> onFailure)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            return result.IsSuccess
                ? await onSuccess(result.Response).ConfigureAwait(false)
                : await onFailure(result).ConfigureAwait(false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then folds synchronously.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when the awaited result is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when the awaited result is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static async Task<TOut> MatchAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, TOut> onSuccess,
            Func<Result<T>, TOut> onFailure)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            var result = await resultTask.ConfigureAwait(false);

            return result.Match(onSuccess, onFailure);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then folds asynchronously.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when the awaited result is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when the awaited result is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static async Task<TOut> MatchAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, Task<TOut>> onSuccess,
            Func<Result<T>, Task<TOut>> onFailure)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            var result = await resultTask.ConfigureAwait(false);

            return await result.MatchAsync(onSuccess, onFailure).ConfigureAwait(false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Asynchronous fold of an <see cref="IResult" />.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="result">Result to fold.</param>
        /// <param name="onSuccess">
        ///     Selector invoked when <paramref name="result" /> is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked when <paramref name="result" /> is a failure.
        /// </param>
        /// <returns>
        ///     A TOut.
        /// </returns>
        /// =================================================================================================
        public static async Task<TOut> MatchAsync<TOut>(
            this IResult result,
            Func<IResult, Task<TOut>> onSuccess,
            Func<IResult, Task<TOut>> onFailure)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (onSuccess.IsNull())
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull())
                throw new ArgumentNullException(nameof(onFailure));

            return result.IsSuccess
                ? await onSuccess(result).ConfigureAwait(false)
                : await onFailure(result).ConfigureAwait(false);
        }
    }
}
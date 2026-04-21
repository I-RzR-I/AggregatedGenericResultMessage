// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2026-04-22 00:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 01:05
// ***********************************************************************
//  <copyright file="ResultAsyncCombinatorExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Extensions.Common;
using System;
using System.Threading.Tasks;

#endregion

namespace RzR.ResultMessage.Extensions.Result
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>Async monadic combinators for <see cref="Result{T}" /> pipelines.</summary>
    /// <remarks>
    /// <list type="bullet">
    ///         <item>
    ///             <description>The <b>source</b> may be a <see cref="Task{TResult}" /> of <see cref="Result{T}" />.</description>
    ///         </item>
    ///         <item>
    ///             <description>The <b>selector / binder / action</b> may be async (return a <see cref="Task" />).</description>
    ///         </item>
    ///     </list>
    ///     All awaits use <c>ConfigureAwait(false)</c> so these helpers are safe in library code.
    /// </remarks>
    /// =================================================================================================
    public static class ResultAsyncCombinatorExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Asynchronously projects <see cref="Result{T}.Response" /> through <paramref name="selector" />
        ///     when the result is successful; otherwise propagates the failure unchanged.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Source response type.</typeparam>
        /// <typeparam name="TOut">Projected response type.</typeparam>
        /// <param name="result">Source result.</param>
        /// <param name="selector">Async projection invoked only on success.</param>
        /// <returns>
        ///     A new <see cref="Result{TOut}" />.
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> MapAsync<T, TOut>(
            this Result<T> result,
            Func<T, Task<TOut>> selector)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (selector.IsNull())
                throw new ArgumentNullException(nameof(selector));

            if (result.IsSuccess.IsFalse())
                return result.Map<TOut>(_ => default);

            var projected = await selector(result.Response).ConfigureAwait(false);

            return result.Map(_ => projected);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then synchronously applies <paramref name="selector" />
        ///     via <see cref="Result{T}.Map{TOut}(Func{T,TOut})" />.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="selector">Async projection invoked only on success.</param>
        /// <returns>
        ///     A new <see cref="Result{TOut}" />.
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> MapAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, TOut> selector)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (selector.IsNull())
                throw new ArgumentNullException(nameof(selector));

            var result = await resultTask.ConfigureAwait(false);

            return result.Map(selector);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then asynchronously projects via <paramref name="selector" />
        ///     .
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="selector">Async projection invoked only on success.</param>
        /// <returns>
        ///     A new <see cref="Result{TOut}" />.
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> MapAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, Task<TOut>> selector)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (selector.IsNull())
                throw new ArgumentNullException(nameof(selector));

            var result = await resultTask.ConfigureAwait(false);

            return await result.MapAsync(selector).ConfigureAwait(false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Asynchronously flat-maps <see cref="Result{T}.Response" /> through <paramref name="binder" />
        ///     when the result is successful; otherwise propagates the failure unchanged.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is invalid.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="result">Source result.</param>
        /// <param name="binder">The binder.</param>
        /// <returns>
        ///     A Result&lt;TOut&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> BindAsync<T, TOut>(
            this Result<T> result,
            Func<T, Task<Result<TOut>>> binder)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (binder.IsNull())
                throw new ArgumentNullException(nameof(binder));

            if (result.IsSuccess.IsFalse())
                return result.Bind(_ => Result<TOut>.Failure());

            var bound = await binder(result.Response).ConfigureAwait(false);
            if (bound.IsNull())
            {
                throw new InvalidOperationException(
                    "BindAsync binder returned null. Return Result<TOut>.Failure(...) instead of null to signal failure.");
            }

            return bound;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then synchronously binds via <paramref name="binder" />
        ///     .
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="binder">The binder.</param>
        /// <returns>
        ///     A Result&lt;TOut&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> BindAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, Result<TOut>> binder)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (binder.IsNull())
                throw new ArgumentNullException(nameof(binder));

            var result = await resultTask.ConfigureAwait(false);

            return result.Bind(binder);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then asynchronously binds via <paramref name="binder" />
        ///     .
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <typeparam name="TOut">Type of the out.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="binder">The binder.</param>
        /// <returns>
        ///     A Result&lt;TOut&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<TOut>> BindAsync<T, TOut>(
            this Task<Result<T>> resultTask,
            Func<T, Task<Result<TOut>>> binder)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (binder.IsNull())
                throw new ArgumentNullException(nameof(binder));

            var result = await resultTask.ConfigureAwait(false);

            return await result.BindAsync(binder).ConfigureAwait(false);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then synchronously applies <see cref="Result{T}.Tap(Action{T})" />
        ///     .
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> Tap<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (action.IsNull())
                throw new ArgumentNullException(nameof(action));

            var result = await resultTask.ConfigureAwait(false);

            return result.Tap(action);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Asynchronously invokes <paramref name="action" /> on <see cref="Result{T}.Response" />
        ///     when the result is successful, then returns the same result.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">Source result.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> TapAsync<T>(
            this Result<T> result,
            Func<T, Task> action)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (action.IsNull())
                throw new ArgumentNullException(nameof(action));

            if (result.IsSuccess)
                await action(result.Response).ConfigureAwait(false);

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then runs the synchronous <paramref name="action" />
        ///     on success.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Action<T> action)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (action.IsNull())
                throw new ArgumentNullException(nameof(action));

            var result = await resultTask.ConfigureAwait(false);

            return result.Tap(action);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then runs the async <paramref name="action" /> on
        ///     success.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="action">The action.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> TapAsync<T>(
            this Task<Result<T>> resultTask,
            Func<T, Task> action)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (action.IsNull())
                throw new ArgumentNullException(nameof(action));

            var result = await resultTask.ConfigureAwait(false);

            return await result.TapAsync(action).ConfigureAwait(false);
        }
    }
}
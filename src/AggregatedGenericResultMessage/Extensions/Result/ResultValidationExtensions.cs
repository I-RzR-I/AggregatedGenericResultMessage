// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2026-04-22 02:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 08:21
// ***********************************************************************
//  <copyright file="ResultValidationExtensions.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.ResultMessage.Models;
using System;
using System.Threading.Tasks;

#endregion

namespace RzR.ResultMessage.Extensions.Result
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Fluent validation extensions that accumulate errors on a <see cref="Result{T}" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <see cref="Validate{T}(Result{T}, Func{T,bool}, string)" /> always evaluates the
    ///         predicate and appends the error if the predicate returns <c>false</c>, flipping
    ///         <see cref="Result{T}.IsSuccess" /> to <c>false</c>. This enables chaining several
    ///         validations
    ///         in a row to collect every violation in one pass:
    ///     </para>
    ///     <code>
    ///         var result = Result&lt;Order&gt;.Success(order)
    ///             .Validate(o =&gt; o.Items.Count &gt; 0, "Order must contain at least one item")
    ///             .Validate(o =&gt; o.Total &gt; 0, "Order total must be positive")
    ///             .Validate(o =&gt; o.Customer != null, "Order must have a customer");
    ///     </code>
    ///     <para>
    ///         Use <see cref="Ensure{T}(Result{T}, Func{T,bool}, string)" /> instead when you want to
    ///         short-circuit once the result is no longer successful (no further predicates
    ///         evaluated).
    ///     </para>
    /// </remarks>
    /// =================================================================================================
    public static class ResultValidationExtensions
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Evaluates <paramref name="predicate" /> against <see cref="Result{T}.Response" /> and
        ///     appends an error message (and flips <see cref="Result{T}.IsSuccess" /> to <c>false</c>)
        ///     when the predicate returns <c>false</c>. Always evaluates, allowing multiple chained
        ///     calls to accumulate all violations.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static Result<T> Validate<T>(this Result<T> result, Func<T, bool> predicate, string error)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            if (predicate(result.Response).IsFalse())
            {
                result.AddError(error);
                result.IsSuccess = false;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Same as <see cref="Validate{T}(Result{T}, Func{T,bool}, string)" /> but using a key and
        ///     message string.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="key">The key.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static Result<T> Validate<T>(this Result<T> result, Func<T, bool> predicate, string key, string error)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            if (predicate(result.Response).IsFalse())
            {
                result.AddError(key, error);
                result.IsSuccess = false;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Same as <see cref="Validate{T}(Result{T}, Func{T,bool}, string)" /> but using a
        ///     <see cref="MessageDataModel" /> for richer error info/details.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static Result<T> Validate<T>(this Result<T> result, Func<T, bool> predicate, MessageDataModel error)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));
            if (error.IsNull())
                throw new ArgumentNullException(nameof(error));

            if (predicate(result.Response).IsFalse())
            {
                result.AddError(error);
                result.IsSuccess = false;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Evaluates <paramref name="predicate" /> only if the current result is successful. Once <see cref="Result{T}.IsSuccess" />
        ///     is <c>false</c>, subsequent <c>Ensure</c> calls are skipped.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, string error)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            if (result.IsSuccess.IsFalse())
                return result;

            if (predicate(result.Response).IsFalse())
            {
                result.AddError(error);
                result.IsSuccess = false;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Async accumulating validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="result">The result to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> ValidateAsync<T>(
            this Result<T> result, Func<T, Task<bool>> predicate, string error)
        {
            if (result.IsNull())
                throw new ArgumentNullException(nameof(result));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            var ok = await predicate(result.Response).ConfigureAwait(false);
            if (ok.IsFalse())
            {
                result.AddError(error);
                result.IsSuccess = false;
            }

            return result;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then runs sync <see cref="Validate{T}(Result{T}, Func{T,bool}, string)" />
        ///     .
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> ValidateAsync<T>(
            this Task<Result<T>> resultTask, Func<T, bool> predicate, string error)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            var result = await resultTask.ConfigureAwait(false);

            return result.Validate(predicate, error);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Awaits <paramref name="resultTask" />, then runs async accumulating validation.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="resultTask">The resultTask to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="error">The error.</param>
        /// <returns>
        ///     A Result&lt;T&gt;
        /// </returns>
        /// =================================================================================================
        public static async Task<Result<T>> ValidateAsync<T>(
            this Task<Result<T>> resultTask, Func<T, Task<bool>> predicate, string error)
        {
            if (resultTask.IsNull())
                throw new ArgumentNullException(nameof(resultTask));
            if (predicate.IsNull())
                throw new ArgumentNullException(nameof(predicate));

            var result = await resultTask.ConfigureAwait(false);

            return await result.ValidateAsync(predicate, error).ConfigureAwait(false);
        }
    }
}
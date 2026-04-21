// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2026-04-21 23:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-21 23:52
// ***********************************************************************
//  <copyright file="ResultOfTMonadic.cs" company="RzR SOFT & TECH">
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

#endregion

namespace RzR.ResultMessage
{
    /// -------------------------------------------------------------------------------------------------
    /// <content>
    ///     A result.
    /// </content>
    /// =================================================================================================
    public partial class Result<T> : IResult<T>
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Projects the inner <see cref="Response" /> through <paramref name="selector" /> when the
        ///     result is successful; otherwise propagates the failure (and its messages) unchanged.
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="selector" /> is <c>null</c>.
        /// </exception>
        /// <typeparam name="TOut">Type of the projected response.</typeparam>
        /// <param name="selector">Projection invoked only on success.</param>
        /// <returns>
        ///     A new <see cref="Result{TOut}" /> carrying either the projected value or the propagated
        ///     failure.
        /// </returns>
        /// =================================================================================================
        public virtual Result<TOut> Map<TOut>(Func<T, TOut> selector)
        {
            if (selector.IsNull()) 
                throw new ArgumentNullException(nameof(selector));

            var mapped = Result<TOut>.CreateInstance();
            mapped.IsSuccess = IsSuccess;
            mapped.Messages = Messages;

            if (IsSuccess) 
                mapped.Response = selector(Response);

            return mapped;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Flat-maps the inner <see cref="Response" /> through <paramref name="binder" /> when the
        ///     result is successful; otherwise propagates the failure (and its messages) unchanged.
        /// 
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="binder" /> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when <paramref name="binder" /> returns <c>null</c> on success.
        /// </exception>
        /// <typeparam name="TOut">Type of the bound response.</typeparam>
        /// <param name="binder">Binder invoked only on success.</param>
        /// <returns>
        ///     The <see cref="Result{TOut}" /> produced by <paramref name="binder" />, or a propagated
        ///     failure.
        /// </returns>
        /// =================================================================================================
        public virtual Result<TOut> Bind<TOut>(Func<T, Result<TOut>> binder)
        {
            if (binder.IsNull()) throw new ArgumentNullException(nameof(binder));

            if (!IsSuccess)
            {
                var propagated = Result<TOut>.CreateInstance();
                propagated.IsSuccess = false;
                propagated.Messages = Messages;

                return propagated;
            }

            var bound = binder(Response);
            if (bound.IsNull())
            {
                throw new InvalidOperationException(
                    "Bind binder returned null. Return Result<TOut>.Failure(...) instead of null to signal failure.");
            }

            return bound;
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Folds the result into a single value by invoking <paramref name="onSuccess" /> when the
        ///     result is successful and <paramref name="onFailure" /> otherwise.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when either selector is <c>null</c>.
        /// </exception>
        /// <typeparam name="TOut">Type of the folded value.</typeparam>
        /// <param name="onSuccess">
        ///     Selector invoked with <see cref="Response" /> when the result is successful.
        /// </param>
        /// <param name="onFailure">
        ///     Selector invoked with the current result when it is not successful.
        /// </param>
        /// <returns>
        ///     The value produced by the matching selector.
        /// </returns>
        /// =================================================================================================
        public virtual TOut Match<TOut>(Func<T, TOut> onSuccess, Func<Result<T>, TOut> onFailure)
        {
            if (onSuccess.IsNull()) 
                throw new ArgumentNullException(nameof(onSuccess));
            if (onFailure.IsNull()) 
                throw new ArgumentNullException(nameof(onFailure));

            return IsSuccess ? onSuccess(Response) : onFailure(this);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Invokes a side-effecting <paramref name="action" /> on the inner <see cref="Response" />
        ///     only when the result is successful, then returns the same result instance.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="action" /> is <c>null</c>.
        /// </exception>
        /// <param name="action">Side effect invoked with <see cref="Response" /> on success.</param>
        /// <returns>
        ///     The current <see cref="Result{T}" /> (for chaining).
        /// </returns>
        /// =================================================================================================
        public virtual Result<T> Tap(Action<T> action)
        {
            if (action.IsNull()) 
                throw new ArgumentNullException(nameof(action));

            if (IsSuccess) 
                action(Response);

            return this;
        }
    }
}
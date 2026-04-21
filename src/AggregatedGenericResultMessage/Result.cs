// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-06-30 20:17
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-07 23:26
// ***********************************************************************
//  <copyright file="Result.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Helpers;
using RzR.ResultMessage.Models;
using System;

#pragma warning disable 8632
#pragma warning disable IDE0090

#endregion

// ReSharper disable VirtualMemberCallInConstructor

namespace RzR.ResultMessage
{
    /// <inheritdoc />
    public class Result : Result<object>
    {
        #region C T O R s

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <remarks></remarks>
        public Result()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result" /> class.
        /// </summary>
        /// <param name="isSuccess">
        ///     If set to <see langword="true" />, then current request was executed with success; otherwise,
        ///     request was executed with errors.
        /// </param>
        /// <remarks></remarks>
        private Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            if (isSuccess.Equals(false))
                Messages.Add(new MessageModel(key: ExceptionCodes.UnSuccessfullyReqExec, (string)null, messageType: MessageType.Error));
        }

        /// <inheritdoc />
        private Result(Exception? exception) : base(exception)
        {
        }

        #endregion

        #region O P E R A T O R S

        /// <summary>
        ///     Implicit exception result operator
        /// </summary>
        /// <param name="exception">Current exception</param>
        /// <returns>A failed <see cref="Result"/> wrapping <paramref name="exception"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="exception"/> is <c>null</c>. Converting a missing exception
        ///     into a result is meaningless; use <c>Result.Failure(...)</c> or the implicit <c>bool</c>
        ///     operator (<c>Result r = false;</c>) when no exception is available.
        /// </exception>
        public static implicit operator Result(Exception exception)
            => exception.IsNotNull()
                ? new Result(exception)
                : throw new ArgumentNullException(nameof(exception),
                    "Cannot implicitly convert a null Exception to a Result. Use Result.Failure(...) instead.");

        /// <summary>
        ///     Implicit success result operator
        /// </summary>
        /// <param name="isSuccess">Success execution</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result(bool isSuccess) 
            => new Result(isSuccess);

        #endregion
    }
}
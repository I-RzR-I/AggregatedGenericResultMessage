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

using System;
using System.ComponentModel.DataAnnotations;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Helpers;
using AggregatedGenericResultMessage.Models;

#pragma warning disable 8632

#endregion

// ReSharper disable VirtualMemberCallInConstructor

namespace AggregatedGenericResultMessage
{
    /// <inheritdoc />
    public class Result : Result<object>
    {
        #region C T O R s

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result" /> class.
        /// </summary>
        /// <remarks></remarks>
        public Result()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result" /> class.
        /// </summary>
        /// <param name="isSuccess">
        ///     If set to <see langword="true" />, then current request was executed with success; otherwise,
        ///     request was executed with errors.
        /// </param>
        /// <remarks></remarks>
        private Result([Required] bool isSuccess)
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
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result(Exception? exception)
        {
            return exception != null ? new Result(exception) : new Result(false);
        }

        /// <summary>
        ///     Implicit success result operator
        /// </summary>
        /// <param name="isSuccess">Success execution</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result([Required] bool isSuccess)
        {
            return new Result(isSuccess);
        }

        #endregion
    }
}
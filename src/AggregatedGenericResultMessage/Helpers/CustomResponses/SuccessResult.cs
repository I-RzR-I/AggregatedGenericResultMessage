﻿// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-13 23:34
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="SuccessResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

// ReSharper disable RedundantNameQualifier
// ReSharper disable EmptyConstructor
namespace AggregatedGenericResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class SuccessResult<T> : AggregatedGenericResultMessage.Result<T>
    {
        /// <inheritdoc />
        public SuccessResult() { }

        /// <inheritdoc />
        public SuccessResult(T result)
            => Response = result;

        /// <inheritdoc />
        public override bool IsSuccess { get; set; } = true;
    }

    /// <inheritdoc />
    public sealed class SuccessResult : AggregatedGenericResultMessage.Result
    {
        /// <inheritdoc />
        public SuccessResult() { }

        /// <inheritdoc />
        public override bool IsSuccess { get; set; } = true;
    }
}
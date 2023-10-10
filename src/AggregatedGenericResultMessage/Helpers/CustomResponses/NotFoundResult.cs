// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-13 23:25
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="NotFoundResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

// ReSharper disable RedundantNameQualifier

#endregion

namespace AggregatedGenericResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class NotFoundResult<T> : AggregatedGenericResultMessage.Result<T>
    {
        /// <inheritdoc />
        public NotFoundResult() { }

        /// <inheritdoc />
        public NotFoundResult(string message)
            => Messages.Add(new MessageModel(null, message, MessageType.NotFound));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, "Entry not found.", MessageType.NotFound)
        };
    }

    /// <inheritdoc />
    public sealed class NotFoundResult : AggregatedGenericResultMessage.Result
    {
        /// <inheritdoc />
        public NotFoundResult() { }

        /// <inheritdoc />
        public NotFoundResult(string message)
            => Messages.Add(new MessageModel(null, message, MessageType.NotFound));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, "Entry not found.", MessageType.NotFound)
        };
    }
}
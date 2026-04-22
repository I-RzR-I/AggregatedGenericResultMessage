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

using RzR.ResultMessage.Abstractions.Models;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Models;
using System.Collections.Generic;

// ReSharper disable RedundantNameQualifier

#endregion

namespace RzR.ResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class NotFoundResult<T> : Result<T>
    {
        /// <inheritdoc />
        public NotFoundResult() { }

        /// <inheritdoc />
        public NotFoundResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message), MessageType.NotFound));

        /// <inheritdoc />
        public NotFoundResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message, MessageType.NotFound));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("Entry not found."), MessageType.NotFound)
        };
    }

    /// <inheritdoc />
    public sealed class NotFoundResult : ResultMessage.Result
    {
        /// <inheritdoc />
        public NotFoundResult() { }

        /// <inheritdoc />
        public NotFoundResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message), MessageType.NotFound));

        /// <inheritdoc />
        public NotFoundResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message, MessageType.NotFound));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("Entry not found."), MessageType.NotFound)
        };
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-13 23:40
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="NotAuthorizedResult.cs" company="">
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
    public sealed class NotAuthorizedResult<T> : Result<T>
    {
        /// <inheritdoc />
        public NotAuthorizedResult() { }

        /// <inheritdoc />
        public NotAuthorizedResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message), MessageType.AccessDenied));

        /// <inheritdoc />
        public NotAuthorizedResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message, MessageType.AccessDenied));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("You are not authorized."), MessageType.AccessDenied)
        };
    }

    /// <inheritdoc />
    public sealed class NotAuthorizedResult : ResultMessage.Result
    {
        /// <inheritdoc />
        public NotAuthorizedResult() { }

        /// <inheritdoc />
        public NotAuthorizedResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message), MessageType.AccessDenied));

        /// <inheritdoc />
        public NotAuthorizedResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message, MessageType.AccessDenied));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("You are not authorized."), MessageType.AccessDenied)
        };
    }
}
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

using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class NotAuthorizedResult<T> : Result<T>
    {
        /// <inheritdoc />
        public NotAuthorizedResult()
        {
        }

        /// <inheritdoc />
        public NotAuthorizedResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message, MessageType.AccessDenied));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "You are not authorized.", MessageType.AccessDenied)
        };
    }

    /// <inheritdoc />
    public sealed class NotAuthorizedResult : Result
    {
        /// <inheritdoc />
        public NotAuthorizedResult()
        {
        }

        /// <inheritdoc />
        public NotAuthorizedResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message, MessageType.AccessDenied));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "You are not authorized.", MessageType.AccessDenied)
        };
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-13 23:50
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="ActionBlockedResult.cs" company="">
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
    public sealed class ActionBlockedResult<T> : Result<T>
    {
        /// <inheritdoc />
        public ActionBlockedResult()
        {
        }

        /// <inheritdoc />
        public ActionBlockedResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message, MessageType.AccessDenied));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "The action was blocked. You do not have permission to continue.",
                MessageType.AccessDenied)
        };
    }

    /// <inheritdoc />
    public sealed class ActionBlockedResult : Result
    {
        /// <inheritdoc />
        public ActionBlockedResult()
        {
        }

        /// <inheritdoc />
        public ActionBlockedResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message, 
                MessageType.AccessDenied));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "The action was blocked. You do not have permission to continue.",
                MessageType.AccessDenied)
        };
    }
}
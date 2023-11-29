// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-14 00:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="InvalidParametersResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;

// ReSharper disable RedundantNameQualifier

#endregion

namespace AggregatedGenericResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class InvalidParametersResult<T> : AggregatedGenericResultMessage.Result<T>
    {
        /// <inheritdoc />
        public InvalidParametersResult() { }

        /// <inheritdoc />
        public InvalidParametersResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message)));

        /// <inheritdoc />
        public InvalidParametersResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("Invalid parameters."))
        };
    }

    /// <inheritdoc />
    public sealed class InvalidParametersResult : AggregatedGenericResultMessage.Result
    {
        /// <inheritdoc />
        public InvalidParametersResult() { }

        /// <inheritdoc />
        public InvalidParametersResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message)));

        /// <inheritdoc />
        public InvalidParametersResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("Invalid parameters."))
        };
    }
}
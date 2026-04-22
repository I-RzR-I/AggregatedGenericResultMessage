// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-01-13 23:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-01-14 00:19
// ***********************************************************************
//  <copyright file="NullReferenceResult.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Abstractions.Models;
using RzR.ResultMessage.Models;
using System.Collections.Generic;

// ReSharper disable RedundantNameQualifier

#endregion

namespace RzR.ResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class NullReferenceResult<T> : Result<T>
    {
        /// <inheritdoc />
        public NullReferenceResult() { }

        /// <inheritdoc />
        public NullReferenceResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message)));

        /// <inheritdoc />
        public NullReferenceResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("A null reference was found."))
        };
    }

    /// <inheritdoc />
    public sealed class NullReferenceResult : ResultMessage.Result
    {
        /// <inheritdoc />
        public NullReferenceResult() { }

        /// <inheritdoc />
        public NullReferenceResult(string message)
            => Messages.Add(new MessageModel(null, new MessageDataModel(message)));

        /// <inheritdoc />
        public NullReferenceResult(MessageDataModel message)
            => Messages.Add(new MessageModel(null, message));

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(null, new MessageDataModel("A null reference was found."))
        };
    }
}
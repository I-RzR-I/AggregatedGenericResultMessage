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

using System.Collections.Generic;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage.Helpers.CustomResponses
{
    /// <inheritdoc />
    public sealed class NullReferenceResult<T> : Result<T>
    {
        /// <inheritdoc />
        public NullReferenceResult()
        {
        }

        /// <inheritdoc />
        public NullReferenceResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "A null reference was found.")
        };
    }

    /// <inheritdoc />
    public sealed class NullReferenceResult : Result
    {
        /// <inheritdoc />
        public NullReferenceResult()
        {
        }

        /// <inheritdoc />
        public NullReferenceResult(string message)
        {
            Messages.Add(new MessageModel(string.Empty, message));
        }

        /// <inheritdoc />
        public override ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>
        {
            new MessageModel(string.Empty, "A null reference was found.")
        };
    }
}
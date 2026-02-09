// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-03 02:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 18:01
// ***********************************************************************
//  <copyright file="ResultError.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Common;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace AggregatedGenericResultMessage.Models
{
    /// <inheritdoc cref="IResultErrorModel" />
    public class ResultErrorModel : IResultErrorModel
    {
        /// <inheritdoc />
        public string Key { get; }

        /// <inheritdoc />
        public MessageDataModel Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <inheritdoc />
        public ICollection<RelatedObjectModel> RelatedObjects { get; set; } = new List<RelatedObjectModel>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, string message)
        {
            Key = key;
            Message = new MessageDataModel(message);
            Exception = null;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, string message, RelatedObjectModel relatedObject = null)
        {
            Key = key;
            Message = new MessageDataModel(message);
            Exception = default;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, string message, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            Message = new MessageDataModel(message);
            Exception = default;

            if (relatedObjects.IsNotNull())
                foreach (var obj in relatedObjects)
                {
                    RelatedObjects?.Add(obj!);
                }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, MessageDataModel message)
        {
            Key = key;
            Message = message;
            Exception = default;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, MessageDataModel message, RelatedObjectModel relatedObject = null)
        {
            Key = key;
            Message = message;
            Exception = default;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="key">Error key</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public ResultErrorModel(string key, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            Key = key;
            Message = message;
            Exception = default;

            if (relatedObjects.IsNotNull())
                foreach (var obj in relatedObjects)
                {
                    RelatedObjects?.Add(obj!);
                }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception)
        {
            Key = null;
            Message = new MessageDataModel();
            Exception = exception;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception, RelatedObjectModel relatedObject = null)
        {
            Key = null;
            Message = new MessageDataModel();
            Exception = exception;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Models.ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <param name="relatedObjects">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception, params RelatedObjectModel[] relatedObjects)
        {
            Key = null;
            Message = new MessageDataModel();
            Exception = exception;

            if (relatedObjects.IsNotNull())
            {
                foreach (var obj in relatedObjects)
                {
                    RelatedObjects?.Add(obj!);
                }
            }
        }

        /// <summary>
        ///     Map ResultError to MessageModel
        /// </summary>
        /// <returns>Mapped data</returns>
        /// <remarks></remarks>
        internal MessageModel MapToMessage() => Exception != default
            ? new MessageModel(null, Exception, RelatedObjects.ToArray())
            : new MessageModel(Key, Message, MessageType.Error, RelatedObjects.ToArray());
    }
}
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

using RzR.ResultMessage.Abstractions.Models;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace RzR.ResultMessage.Models
{
    /// <inheritdoc cref="IResultErrorModel" />
    public class ResultErrorModel : IResultErrorModel
    {
        /// <inheritdoc />
        public string Code { get; }

        /// <inheritdoc />
        [Obsolete(ObsoleteMessages.KeyToCode)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlIgnore]
        [IgnoreDataMember]
#if !NETFRAMEWORK
        [JsonIgnore]
#endif
        public string Key => Code;

        /// <inheritdoc />
        public MessageDataModel Message { get; }

        /// <inheritdoc />
        public Exception Exception { get; }

        /// <inheritdoc />
        public ICollection<RelatedObjectModel> RelatedObjects { get; set; } = new List<RelatedObjectModel>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, string message)
        {
            Code = code;
            Message = new MessageDataModel(message);
            Exception = null;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, string message, RelatedObjectModel relatedObject = null)
        {
            Code = code;
            Message = new MessageDataModel(message);
            Exception = default;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, string message, params RelatedObjectModel[] relatedObjects)
        {
            Code = code;
            Message = new MessageDataModel(message);
            Exception = default;

            if (relatedObjects.IsNotNull())
            {
                foreach (var obj in relatedObjects)
                {
                    RelatedObjects?.Add(obj!);
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, MessageDataModel message)
        {
            Code = code;
            Message = message;
            Exception = default;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, MessageDataModel message, RelatedObjectModel relatedObject = null)
        {
            Code = code;
            Message = message;
            Exception = default;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="message">Error message</param>
        /// <param name="relatedObjects">Related message objects</param>
        /// <remarks></remarks>
        public ResultErrorModel(string code, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            Code = code;
            Message = message;
            Exception = default;

            if (relatedObjects.IsNotNull())
            {
                foreach (var obj in relatedObjects)
                {
                    RelatedObjects?.Add(obj!);
                }
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception)
        {
            Code = null;
            Message = new MessageDataModel();
            Exception = exception;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <param name="relatedObject">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception, RelatedObjectModel relatedObject = null)
        {
            Code = null;
            Message = new MessageDataModel();
            Exception = exception;

            if (relatedObject.IsNotNull())
                RelatedObjects?.Add(relatedObject);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResultErrorModel" /> class.
        /// </summary>
        /// <param name="exception">Current execution code exception</param>
        /// <param name="relatedObjects">Related message object</param>
        /// <remarks></remarks>
        public ResultErrorModel(Exception exception, params RelatedObjectModel[] relatedObjects)
        {
            Code = null;
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
            : new MessageModel(Code, Message, MessageType.Error, RelatedObjects.ToArray());
    }
}
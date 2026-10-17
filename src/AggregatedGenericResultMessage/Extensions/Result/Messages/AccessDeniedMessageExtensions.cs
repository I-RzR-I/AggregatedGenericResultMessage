// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-09-03 19:16
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 20:12
// ***********************************************************************
//  <copyright file="AccessDeniedMessageExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Abstractions.MessageResults;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Models;

#endregion

namespace RzR.ResultMessage.Extensions.Result.Messages
{
    /// <summary>
    ///     Access denied messages extensions
    /// </summary>
    /// <remarks></remarks>
    public static class AccessDeniedMessageExtensions
    {
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied()" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result)
        {
            result.Messages?.Add(new MessageModel(code: null, message: (string)null, messageType: MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied()" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result)
        {
            result.Messages?.Add(new MessageModel(code: null, message: (string)null, messageType: MessageType.AccessDenied));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(code: null, message: new MessageDataModel(message), messageType: MessageType.AccessDenied));

            return result;
        }

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: null, message: new MessageDataModel(message), 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(MessageDataModel)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, MessageDataModel message)
        {
            result.Messages?.Add(new MessageModel(code: null, message: message, messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: null, message: message, 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string code, string message)
        {
            result.Messages?.Add(new MessageModel(code: code, message: new MessageDataModel(message), 
                messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string code, string message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: code, message: new MessageDataModel(message), 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, MessageDataModel)" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string code, MessageDataModel message)
        {
            result.Messages?.Add(new MessageModel(code: code, message: message, messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this IResult<T> result, string code, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: code, message: message,
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string message)
        {
            result.Messages?.Add(new MessageModel(code: null, message: new MessageDataModel(message), messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: null, message: new MessageDataModel(message),
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(MessageDataModel)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, MessageDataModel message)
        {
            result.Messages?.Add(new MessageModel(code: null, message: message, messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: null, message: message, 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }


        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string code, string message)
        {
            result.Messages?.Add(new MessageModel(code: code, message: new MessageDataModel(message), messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, string, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string code, string message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: code, message: new MessageDataModel(message), 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }
        

        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, MessageDataModel)" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string code, MessageDataModel message)
        {
            result.Messages?.Add(new MessageModel(code: code, message: message, messageType: MessageType.AccessDenied));

            return result;
        }
        
        /// <inheritdoc cref="IAccessDeniedMessageResult{T}.AddAccessDenied(string, MessageDataModel, RelatedObjectModel[])" />
        public static IResult<T> AddAccessDenied<T>(this Result<T> result, string code, MessageDataModel message, params RelatedObjectModel[] relatedObjects)
        {
            result.Messages?.Add(new MessageModel(code: code, message: message, 
                messageType: MessageType.AccessDenied, relatedObjects: relatedObjects));

            return result;
        }
    }
}
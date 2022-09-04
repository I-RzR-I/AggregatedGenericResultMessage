// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-06-30 20:17
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-09-03 19:43
// ***********************************************************************
//  <copyright file="ResultOfT.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Messages;
using AggregatedGenericResultMessage.Models;

#endregion

namespace AggregatedGenericResultMessage
{
    /// <inheritdoc cref="IResult{T}" />
    public class Result<T> : IResult<T>, IXmlSerializable
    {
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable 649
        private static Result<T> _instance;
#pragma warning restore 649
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        ///     Instance
        /// </summary>
        public static Result<T> Instance => _instance ?? new Result<T>();

        /// <inheritdoc />
        [JsonPropertyName("isSuccess")]
        public virtual bool IsSuccess { get; set; }

        /// <inheritdoc />
        [JsonPropertyName("messages")]
        public virtual ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>();

        /// <inheritdoc />
        [JsonPropertyName("result")]
        public virtual T Response { get; set; }

        /// <inheritdoc />
        public virtual Result ToBase()
        {
            return new Result
            {
                IsSuccess = IsSuccess,
                Response = Response,
                Messages = Messages
            };
        }

        /// <inheritdoc />
        public IResult<T> AddMessage(string key = null, string message = null, MessageType type = MessageType.Error)
        {
            Messages?.Add(new MessageModel(key, message, type));

            return this;
        }

        /// <inheritdoc />
        public IResult<T> AddMessage(string message = null, MessageType type = MessageType.Error)
        {
            Messages?.Add(new MessageModel(string.Empty, message, type));

            return this;
        }

        /// <inheritdoc />
        public string GetFirstMessage()
        {
            return Messages?.FirstOrDefault()?.Message;
        }

        /// <inheritdoc />
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <inheritdoc />
        public void ReadXml(XmlReader reader)
        {
            // Method intentionally left empty.
        }

        /// <inheritdoc />
        public void WriteXml(XmlWriter writer)
        {
            // Method intentionally left empty.
        }

        /// <summary>
        ///     Add error from results
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public virtual IResult<T> JoinResults<TIn>(IEnumerable<Result<TIn>> results)
        {
            var collection = results.ToList();
            var response = new Result<T>
            {
                IsSuccess = collection.All(x => x.IsSuccess)
            };
            foreach (var error in collection.SelectMany(result => result.Messages)) response.Messages.Add(error);

            return response;
        }

        /// <summary>
        ///     Add error from results
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public virtual Result<T> JoinResults(IEnumerable<Result> results)
        {
            var collection = results.ToList();
            var response = new Result<T>
            {
                IsSuccess = collection.All(x => x.IsSuccess)
            };
            foreach (var error in collection.SelectMany(result => result.Messages)) response.Messages.Add(error);

            return response;
        }

        /// <summary>
        ///     Join all errors
        /// </summary>
        /// <param name="results">All results</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public virtual Result<T> JoinErrors(IEnumerable<Result> results)
        {
            var collection = results.ToList();
            var response = new Result<T>
            {
                IsSuccess = IsSuccess
            };
            foreach (var error in collection.SelectMany(result => result
                .Messages
                .Where(x => x.MessageType == MessageType.Error))) response.Messages.Add(error);

            return response;
        }

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual Result<T> SetResult(T result)
        {
            Response = result;

            return this;
        }

        /// <summary>
        ///     Adapt result model
        /// </summary>
        /// <typeparam name="TModelOutput"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual Result<TModelOutput> Map<TModelOutput>(TModelOutput result = default)
        {
            return new Result<TModelOutput>
            {
                IsSuccess = IsSuccess,
                Response = result,
                Messages = Messages
            };
        }

        /// <summary>
        ///     Success response
        /// </summary>
        /// <param name="data">Response data</param>
        /// <returns></returns>
        public static Result<T> Success(T data = default)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Response = data
            };
        }

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        public static Result<T> Failure(string error)
        {
            return (Result<T>) new Result<T>().AddError(error);
        }

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> Failure(string code, string error)
        {
            return (Result<T>) new Result<T>().AddError(code, error);
        }

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="warn">Warning message</param>
        /// <returns></returns>
        public static Result<T> Warn(string warn)
        {
            return (Result<T>) new Result<T>().AddWarning(warn);
        }

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="code">Warning code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> Warn(string code, string error)
        {
            return (Result<T>) new Result<T>().AddWarning(code, error);
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="message">Access denied message</param>
        /// <returns></returns>
        public static Result<T> AccessDenied(string message)
        {
            return (Result<T>) new Result<T>().AddAccessDenied(message);
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="code">Access denied code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> AccessDenied(string code, string error)
        {
            return (Result<T>) new Result<T>().AddAccessDenied(code, error);
        }

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="message">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound(string message)
        {
            return (Result<T>) new Result<T>().AddNotFound(message);
        }

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="code">Not found code</param>
        /// <param name="error">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound(string code, string error)
        {
            return (Result<T>) new Result<T>().AddNotFound(code, error);
        }
    }
}
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions;
using AggregatedGenericResultMessage.Extensions.Messages;
using AggregatedGenericResultMessage.Helpers;
using AggregatedGenericResultMessage.Models;
using DomainCommonExtensions.CommonExtensions;

// ReSharper disable VirtualMemberCallInConstructor
#pragma warning disable 8632

#endregion

namespace AggregatedGenericResultMessage
{
    /// <inheritdoc cref="IResult{T}" />
    public class Result<T> : IResult<T>, IXmlSerializable
    {
        #region I N S T A N C E

        /// <summary>
        ///     Gets result instance.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public static Result<T> Instance => CreateInstance() ?? new Result<T>();

        /// <summary>
        ///     Create an instance of <see cref="Result{T}"/>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private static Result<T> CreateInstance() => new Result<T>();

        #endregion

        #region P R O P s

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("isSuccess")]
#endif
        public virtual bool IsSuccess { get; set; }

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("messages")]
#endif
        public virtual ICollection<IMessageModel> Messages { get; set; } = new List<IMessageModel>();

        /// <inheritdoc />
#if !NETFRAMEWORK
        [JsonPropertyName("result")]
#endif
        public virtual T Response { get; set; }

        #endregion

        #region C T O R s

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result{T}" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Result() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result{T}" /> class. 
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <remarks></remarks>
        internal Result(Exception? exception)
        {
            if (!exception.IsNull())
                ExceptionHelper.PreserveStackTrace(exception);

            this.IsSuccess = false;
            this.Messages.Add(new MessageModel(null, exception?.Message ?? ""));

            if (!exception.IsNull())
                this.Messages.Add(new MessageModel(null, exception));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result{T}" /> class. 
        /// </summary>
        /// <param name="response">Response data</param>
        /// <remarks></remarks>
        private Result([Required] T response)
        {
            this.Response = response;
            this.IsSuccess = true;
        }

        #endregion

        /// <inheritdoc />
        public virtual Result ToBase() => new Result
        {
            IsSuccess = IsSuccess,
            Response = Response,
            Messages = Messages
        };

        /// <inheritdoc />
        public virtual SoapResult ToSoapResult() => new SoapResult
        {
            IsSuccess = IsSuccess,
            Messages = Messages?.Select(y => new MessageModel()
            {
                Key = y.Key,
                Message = y.Message,
                MessageType = y.MessageType
            }).ToList(),
            Response = Response.CastToSoapResponse()
        };
        
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
            try
            {
                var fMessage = Messages.FirstOrDefault(x => x.MessageType != MessageType.Exception)?.Message;

                return fMessage.IsNull() ? string.Empty : fMessage;
            }
            catch
            {
                return string.Empty;
            }
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
        /// <param name="results">List of results</param>
        /// <returns></returns>
        public virtual IResult<T> JoinErrorResults<TIn>(IEnumerable<Result<TIn>> results)
        {
            var collection = results.ToList();
            var response = CreateInstance();
            response.IsSuccess = collection.All(x => x.IsSuccess);
            foreach (var error in collection
                .SelectMany(result => result.Messages
                    .Where(x => new List<MessageType>()
                    {
                        MessageType.Error,
                        MessageType.Exception
                    }.Contains(x.MessageType))))
            { response.Messages.Add(error); }

            return response;
        }

        /// <summary>
        ///     Add error from results
        /// </summary>
        /// <param name="results">List of results</param>
        /// <returns></returns>
        public virtual Result<T> JoinErrorResults(IEnumerable<Result> results)
        {
            var collection = results.ToList();
            var response = CreateInstance();
            response.IsSuccess = collection.All(x => x.IsSuccess);
            foreach (var error in collection
                .SelectMany(result => result.Messages
                    .Where(x => new List<MessageType>()
                    {
                        MessageType.Error,
                        MessageType.Exception
                    }.Contains(x.MessageType))))
            { response.Messages.Add(error); }

            return response;
        }

        /// <summary>
        ///     Join all results
        /// </summary>
        /// <param name="results">List of results</param>
        /// <returns></returns>
        public virtual IResult<T> JoinResults<TIn>(IEnumerable<Result<TIn>> results)
        {
            var collection = results.ToList();
            var response = CreateInstance();
            response.IsSuccess = collection.All(x => x.IsSuccess);
            foreach (var res in collection
                .SelectMany(result => result.Messages))
            { response.Messages.Add(res); }

            return response;
        }

        /// <summary>
        ///     Join all results
        /// </summary>
        /// <param name="results">List of results</param>
        /// <returns></returns>
        public virtual Result<T> JoinResults(IEnumerable<Result> results)
        {
            var collection = results.ToList();
            var response = CreateInstance();
            response.IsSuccess = collection.All(x => x.IsSuccess);
            foreach (var res in collection
                .SelectMany(result => result.Messages))
            { response.Messages.Add(res); }

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
            var response = CreateInstance();
            response.IsSuccess = IsSuccess;
            foreach (var error in collection
                .SelectMany(result => result.Messages
                    .Where(x => new List<MessageType>()
                    {
                        MessageType.Error,
                        MessageType.Exception
                    }.Contains(x.MessageType))))
            { response.Messages.Add(error); }

            return response;
        }

        /// <summary>
        ///     Set result
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns></returns>
        public virtual Result<T> SetResult(T result)
        {
            Response = result;

            return this;
        }

        /// <summary>
        ///     Adapt result model
        /// </summary>
        /// <typeparam name="TModelOutput">Type of model</typeparam>
        /// <param name="result">Result</param>
        /// <returns></returns>
        public virtual Result<TModelOutput> Map<TModelOutput>(TModelOutput result = default)
        {
            var map = Result<TModelOutput>.CreateInstance();
            map.IsSuccess = IsSuccess;
            map.Response = result;
            map.Messages = Messages;

            return map;
        }

        /// <summary>
        ///     Success response
        /// </summary>
        /// <param name="data">Response data</param>
        /// <returns></returns>
        public static Result<T> Success(T data = default)
        {
            var result = CreateInstance();
            result.IsSuccess = true;
            result.Response = data;

            return result;
        }

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns></returns>
        public static Result<T> Failure(string error)
        {
            return (Result<T>)CreateInstance().AddError(error);
        }

        /// <summary>
        ///     Failure
        /// </summary>
        /// <param name="code">Error code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> Failure(string code, string error)
        {
            return (Result<T>)CreateInstance().AddError(code, error);
        }

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="warn">Warning message</param>
        /// <returns></returns>
        public static Result<T> Warn(string warn)
        {
            return (Result<T>)CreateInstance().AddWarning(warn);
        }

        /// <summary>
        ///     Warning
        /// </summary>
        /// <param name="code">Warning code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> Warn(string code, string error)
        {
            return (Result<T>)CreateInstance().AddWarning(code, error);
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="message">Access denied message</param>
        /// <returns></returns>
        public static Result<T> AccessDenied(string message)
        {
            return (Result<T>)CreateInstance().AddAccessDenied(message);
        }

        /// <summary>
        ///     Access denied
        /// </summary>
        /// <param name="code">Access denied code</param>
        /// <param name="error">Message</param>
        /// <returns></returns>
        public static Result<T> AccessDenied(string code, string error)
        {
            return (Result<T>)CreateInstance().AddAccessDenied(code, error);
        }

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="message">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound(string message)
        {
            return (Result<T>)CreateInstance().AddNotFound(message);
        }

        /// <summary>
        ///     Not found
        /// </summary>
        /// <param name="code">Not found code</param>
        /// <param name="error">Not found message</param>
        /// <returns></returns>
        public static Result<T> NotFound(string code, string error)
        {
            return (Result<T>)CreateInstance().AddNotFound(code, error);
        }

        #region O P E R A T O R S

        /// <summary>
        ///     Implicit result operator for response of T
        /// </summary>
        /// <param name="response">Response data</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result<T>(T response) =>
            new Result<T>(response);

        /// <summary>
        ///     Implicit result operator for exception
        /// </summary>
        /// <param name="exception">Current exceptions</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result<T>(Exception? exception) =>
            new Result<T>(exception);

        #endregion
    }
}
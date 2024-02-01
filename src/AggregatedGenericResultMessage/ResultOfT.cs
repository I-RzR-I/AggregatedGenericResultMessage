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
using AggregatedGenericResultMessage.Extensions.Common;
using AggregatedGenericResultMessage.Helpers;
using AggregatedGenericResultMessage.Models;

// ReSharper disable ArrangeThisQualifier
// ReSharper disable VirtualMemberCallInConstructor
#pragma warning disable 8632
#pragma warning disable IDE0090
#pragma warning disable IDE0003

#endregion

namespace AggregatedGenericResultMessage
{
    /// <inheritdoc cref="IResult{T}" />
    public partial class Result<T> : IResult<T>, IXmlSerializable
    {
        #region I N S T A N C E

        /// <summary>
        ///     Gets result instance.
        /// </summary>
        /// <value></value>
        /// <remarks></remarks>
        public static Result<T> Instance => CreateInstance();

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
        [JsonPropertyName("response")]
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
            if (exception.IsNotNull())
                ExceptionHelper.PreserveStackTrace(exception);

            this.IsSuccess = false;
            this.Messages.Add(new MessageModel(null, new MessageDataModel(exception?.Message ?? "")));

            if (exception.IsNotNull())
                this.Messages.Add(new MessageModel(null, exception));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AggregatedGenericResultMessage.Result{T}" /> class. 
        /// </summary>
        /// <param name="response">Response data</param>
        /// <remarks></remarks>
        private Result(T response)
        {
            this.Response = response;
            this.IsSuccess = true;
        }

        #endregion

        /// <inheritdoc />
        public virtual Result ToBase()
            => new Result()
            {
                IsSuccess = IsSuccess,
                Response = Response,
                Messages = Messages
            };

        /// <inheritdoc />
        public virtual SoapResult ToSoapResult()
        {
            var messages = Messages
                ?.Select(y => new MessageModel() { Key = y.Key, Message = y.Message, MessageType = y.MessageType }).ToList();
            var response = Response.CastToSoapResponse();

            return new SoapResult()
            {
                IsSuccess = IsSuccess,
                Messages = messages,
                Response = response
            };
        }

        /// <inheritdoc />
        public string GetFirstMessage()
        {
            try
            {
                var fMessage = Messages
                    .FirstOrDefault(x => x.MessageType != MessageType.Exception)
                    ?.Message;

                return fMessage.IsNull() ? string.Empty : fMessage?.Info;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public MessageDataModel GetFirstMessageWithDetails()
        {
            try
            {
                var fMessage = Messages
                    .FirstOrDefault(x => x.MessageType != MessageType.Exception)
                    ?.Message;

                return fMessage.IsNull() ? null : fMessage;
            }
            catch
            {
                return null;
            }
        }

        /// <inheritdoc />
        public XmlSchema GetSchema() => null;

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

        #region O P E R A T O R S

        /// <summary>
        ///     Implicit result operator for response of T
        /// </summary>
        /// <param name="response">Response data</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result<T>(T response)
            => new Result<T>(response);

        /// <summary>
        ///     Implicit result operator for exception
        /// </summary>
        /// <param name="exception">Current exceptions</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static implicit operator Result<T>(Exception? exception)
            => new Result<T>(exception);

        #endregion
    }
}
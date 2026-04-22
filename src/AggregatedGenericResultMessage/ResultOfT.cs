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

using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Abstractions.Models;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Helpers;
using RzR.ResultMessage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

#if !NETFRAMEWORK
using System.Text.Json.Serialization;
#endif

// ReSharper disable ArrangeThisQualifier
// ReSharper disable VirtualMemberCallInConstructor
#pragma warning disable 8632
#pragma warning disable IDE0090
#pragma warning disable IDE0003

#endregion

namespace RzR.ResultMessage
{
    /// <inheritdoc cref="IResult{T}" />
    public partial class Result<T> : IResult<T>, IXmlSerializable
    {
        #region I N S T A N C E

        /// <summary>
        ///     Creates a new <see cref="Result{T}"/> instance.
        /// </summary>
        /// <returns>A new, empty <see cref="Result{T}"/>.</returns>
        /// <remarks>
        ///     Each call returns a fresh instance — this is a factory.
        /// </remarks>
        public static Result<T> Create() => CreateInstance();

        /// <summary>
        ///     Gets a new result instance.
        /// </summary>
        /// <value>A new <see cref="Result{T}"/> on every access.</value>
        /// <remarks>
        ///     Misnamed legacy accessor: each access returns a fresh instance, not a singleton.
        ///     Use <see cref="Create"/> instead.
        /// </remarks>
        [Obsolete("Misleading name: each access returns a NEW instance. Use Result<T>.Create() instead.")]
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
        [JsonPropertyName("isFailure")]
#endif
        public virtual bool IsFailure { get => !IsSuccess; }

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
        ///     Initializes a new instance of the <see cref="Result{T}" /> class. 
        /// </summary>
        /// <remarks></remarks>
        public Result() { }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{T}" /> class. 
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <remarks>
        ///     Adds a single <see cref="MessageType.Exception"/> message containing the exception trace.
        ///     When <paramref name="exception"/> is <c>null</c>, no message is appended.
        /// </remarks>
        internal Result(Exception? exception)
        {
            this.IsSuccess = false;

            if (exception.IsNull())
                return;

            ExceptionHelper.PreserveStackTrace(exception);
            this.Messages.Add(new MessageModel(null, exception));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Result{T}" /> class. 
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
                ?.Select(y => new MessageModel()
                {
                    Key = y.Key,
                    Message = y.Message,
                    MessageType = y.MessageType
                }).ToList();

            var response = Response.CastToSoapResponse();

            return new SoapResult()
            {
                IsSuccess = IsSuccess,
                IsFailure = IsFailure,
                Messages = messages,
                Response = response
            };
        }

        /// <inheritdoc />
        public string GetFirstMessage()
        {
            try
            {
                var fMessage = (Messages.FirstOrDefault(x => x.MessageType != MessageType.Exception)
                                ?? Messages.FirstOrDefault())
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
                var fMessage = (Messages.FirstOrDefault(x => x.MessageType != MessageType.Exception)
                                ?? Messages.FirstOrDefault())
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
        /// <remarks>
        ///     <see cref="IResult.IsSuccess"/> of the returned result is <c>true</c> only when every result
        ///     in <paramref name="results"/> is successful; this matches the semantics of
        ///     <see cref="JoinResults(IEnumerable{Result})"/> and <see cref="JoinErrorResults(IEnumerable{Result})"/>.
        /// </remarks>
        public virtual Result<T> JoinErrors(IEnumerable<Result> results)
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
        /// <returns>A failed <see cref="Result{T}"/> wrapping <paramref name="exception"/>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="exception"/> is <c>null</c>. Use <c>Result&lt;T&gt;.Failure(...)</c>
        ///     when no exception is available.
        /// </exception>
        public static implicit operator Result<T>(Exception exception)
            => exception.IsNotNull()
                ? new Result<T>(exception)
                : throw new ArgumentNullException(nameof(exception),
                    "Cannot implicitly convert a null Exception to a Result<T>. Use Result<T>.Failure(...) instead.");

        #endregion
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2023-02-03 08:59
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-03 09:00
// ***********************************************************************
//  <copyright file="FluentConfigurationTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AggregatedGenericResultMessage.Extensions.Result;
using AggregatedGenericResultMessage.Models;
using DomainCommonExtensions.DataTypeExtensions;

namespace InfoResultTests
{
    [TestClass]
    public class FluentConfigurationTests
    {
        [TestMethod]
        public void ResultHasMessageSuccess()
        {
            var res = Result.Success()
                .WithMessage("simple message")
                .WithKeyCode("temp1_code")
                .WithCodeMessage("WithCodeMessage_Code", "WithCodeMessage_Message")
                .WithError("simple_error_message")
                .WithError("simple_error_message", "simple_error_message_code")
                .WithError(new ResultError("ResultError_code", "ResultError_message"))
                .WithError(new Exception("simple_exception"))
                .WithErrors(new List<ResultError>()
                {
                    new ResultError("list_key_1", "list_mess_1"),
                    new ResultError("list_key_2", "list_mess_2")
                });

            Assert.IsTrue(res.IsSuccess);
            Assert.IsNull(res.Response);
            Assert.IsTrue(res.Messages.Count(x => x.Message == "simple message"
                                                  && x.Key.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "temp1_code"
                                                  && x.Message.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "WithCodeMessage_Code" 
                                                  && x.Message == "WithCodeMessage_Message" 
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Message == "simple_error_message"
                                                  && x.Key.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "simple_error_message_code"
                                                  && x.Message == "simple_error_message"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "ResultError_code"
                                                  && x.Message == "ResultError_message"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.MessageType == MessageType.Exception 
                                                  && x.Message.Contains("simple_exception")) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "list_key_1"
                                                  && x.Message == "list_mess_1"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "list_key_2"
                                                  && x.Message == "list_mess_2"
                                                  && x.MessageType == MessageType.Error) == 1);

            var resOfT = Result<bool>
                .Success(true)
                .WithMessage("bool result")
                .WithKeyCode("temp1_code")
                .WithCodeMessage("WithCodeMessage_Code", "WithCodeMessage_Message")
                .WithError("simple_error_message")
                .WithError("simple_error_message", "simple_error_message_code")
                .WithError(new ResultError("ResultError_code", "ResultError_message"))
                .WithError(new Exception("simple_exception"))
                .WithErrors(new List<ResultError>()
                {
                    new ResultError("list_key_1", "list_mess_1"),
                    new ResultError("list_key_2", "list_mess_2")
                });

            Assert.IsTrue(resOfT.IsSuccess);
            Assert.IsNotNull(resOfT.Response);
            Assert.IsTrue(resOfT.Response);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Message == "bool result" 
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "temp1_code"
                                                     && x.Message.IsNullOrEmpty()
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "WithCodeMessage_Code"
                                                     && x.Message == "WithCodeMessage_Message"
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Message == "simple_error_message"
                                                     && x.Key.IsNullOrEmpty()
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "simple_error_message_code"
                                                     && x.Message == "simple_error_message"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "ResultError_code"
                                                     && x.Message == "ResultError_message"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.MessageType == MessageType.Exception
                                                     && x.Message.Contains("simple_exception")) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "list_key_1"
                                                     && x.Message == "list_mess_1"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "list_key_2"
                                                     && x.Message == "list_mess_2"
                                                     && x.MessageType == MessageType.Error) == 1);
        }
        
        [TestMethod]
        public void ResultHasMessageFailure()
        {
            var res = Result.Failure()
                .WithMessage("simple message")
                .WithKeyCode("temp1_code")
                .WithCodeMessage("WithCodeMessage_Code", "WithCodeMessage_Message")
                .WithError("simple_error_message")
                .WithError("simple_error_message", "simple_error_message_code")
                .WithError(new ResultError("ResultError_code", "ResultError_message"))
                .WithError(new Exception("simple_exception"))
                .WithErrors(new List<ResultError>()
                {
                    new ResultError("list_key_1", "list_mess_1"),
                    new ResultError("list_key_2", "list_mess_2"),
                    new ResultError(new Exception("Exception message"))
                });

            Assert.IsFalse(res.IsSuccess);
            Assert.IsNull(res.Response);
            Assert.IsTrue(res.Messages.Count(x => x.Message == "simple message"
                                                  && x.Key.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "temp1_code"
                                                  && x.Message.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "WithCodeMessage_Code" 
                                                  && x.Message == "WithCodeMessage_Message" 
                                                  && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Message == "simple_error_message"
                                                  && x.Key.IsNullOrEmpty()
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "simple_error_message_code"
                                                  && x.Message == "simple_error_message"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "ResultError_code"
                                                  && x.Message == "ResultError_message"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.MessageType == MessageType.Exception 
                                                  && x.Message.Contains("simple_exception")) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "list_key_1"
                                                  && x.Message == "list_mess_1"
                                                  && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(res.Messages.Count(x => x.Key == "list_key_2"
                                                  && x.Message == "list_mess_2"
                                                  && x.MessageType == MessageType.Error) == 1);

            var resOfT = Result<bool>
                .Failure()
                .WithMessage("bool result")
                .WithKeyCode("temp1_code")
                .WithCodeMessage("WithCodeMessage_Code", "WithCodeMessage_Message")
                .WithError("simple_error_message")
                .WithError("simple_error_message", "simple_error_message_code")
                .WithError(new ResultError("ResultError_code", "ResultError_message"))
                .WithError(new Exception("simple_exception"))
                .WithErrors(new List<ResultError>()
                {
                    new ResultError("list_key_1", "list_mess_1"),
                    new ResultError("list_key_2", "list_mess_2")
                });

            Assert.IsFalse(resOfT.IsSuccess);
            Assert.IsNotNull(resOfT.Response);
            Assert.IsFalse(resOfT.Response);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Message == "bool result" 
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "temp1_code"
                                                     && x.Message.IsNullOrEmpty()
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "WithCodeMessage_Code"
                                                     && x.Message == "WithCodeMessage_Message"
                                                     && x.MessageType == MessageType.None) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Message == "simple_error_message"
                                                     && x.Key.IsNullOrEmpty()
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "simple_error_message_code"
                                                     && x.Message == "simple_error_message"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "ResultError_code"
                                                     && x.Message == "ResultError_message"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.MessageType == MessageType.Exception
                                                     && x.Message.Contains("simple_exception")) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "list_key_1"
                                                     && x.Message == "list_mess_1"
                                                     && x.MessageType == MessageType.Error) == 1);
            Assert.IsTrue(resOfT.Messages.Count(x => x.Key == "list_key_2"
                                                     && x.Message == "list_mess_2"
                                                     && x.MessageType == MessageType.Error) == 1);
        }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 14:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="WarnTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Linq;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;
using InfoResultTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class ExceptionTests
    {
        [TestMethod]
        public void ExceptionReturnTest()
        {
            Result result = new Exception("test message");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result.Messages.Count, 2);
            Assert.AreEqual("test message", result.GetFirstMessage());
            Assert.AreEqual("test message", result.GetFirstError());
            Assert.IsTrue(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
        }

        [TestMethod]
        public void ExceptionReturnOfTypeTest()
        {
            Result<bool> result = new Exception("test message");

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result.Messages.Count, 2);
            Assert.AreEqual("test message", result.GetFirstMessage());
            Assert.AreEqual("test message", result.GetFirstError());
            Assert.IsTrue(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
        }

        [TestMethod]
        public void ExceptionReturnResultTest()
        {
            var result = BookService.Instance.GetBookItemException();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result.Messages.Count, 2);
            Assert.AreEqual("Null data", result.GetFirstMessage());
            Assert.AreEqual("Null data", result.GetFirstError());
            Assert.IsTrue(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
        }

        [TestMethod]
        public void ResultAddExceptionTest()
        {
            var result = new Result();
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);

            var resultOfT = new Result<bool>();
            Assert.IsFalse(resultOfT.IsSuccess);
            Assert.IsTrue(resultOfT.IsFailure);

            result.AddException(new Exception("Message"));
            resultOfT.AddException(new Exception("MessageOfT"));
            resultOfT.Messages.Add(new MessageModel(string.Empty, new Exception("MessageOfT-2")));

            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(result.GetFirstMessage(), string.Empty);
            Assert.AreEqual(result.GetFirstError(), string.Empty);
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
            Assert.IsFalse(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.AreEqual(result.Messages.Count(x => x.MessageType == MessageType.Exception), 1);

            Assert.IsFalse(resultOfT.IsSuccess);
            Assert.IsTrue(resultOfT.IsFailure);
            Assert.AreEqual(resultOfT.GetFirstMessage(), string.Empty);
            Assert.AreEqual(resultOfT.GetFirstError(), string.Empty);
            Assert.IsTrue(resultOfT.HasAnyErrorsOrExceptions());
            Assert.IsFalse(resultOfT.HasAnyErrors());
            Assert.IsTrue(resultOfT.HasAnyExceptions());
            Assert.AreEqual(resultOfT.Messages.Count(x=>x.MessageType == MessageType.Exception), 2);
        }
    }
}
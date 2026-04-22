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
using InfoResultTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.ResultMessage.Models;

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
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual("test message", result.GetFirstMessage());
            Assert.AreEqual("test message", result.GetFirstError());
            Assert.IsFalse(result.HasAnyErrors());
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
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual("test message", result.GetFirstMessage());
            Assert.AreEqual("test message", result.GetFirstError());
            Assert.IsFalse(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
        }

        [TestMethod]
        public void ExceptionReturnResultTest()
        {
            var result = BookService.Instance.GetBookItemException();

            Assert.IsNotNull(result);
            Assert.IsFalse((bool)result.IsSuccess);
            Assert.IsTrue((bool)result.IsFailure);
            Assert.AreEqual<int>(1, result.Messages.Count);
            Assert.AreEqual<string>("Null data", result.GetFirstMessage());
            Assert.AreEqual<string>("Null data", result.GetFirstError());
            Assert.IsFalse((bool)result.HasAnyErrors());
            Assert.IsTrue((bool)result.HasAnyExceptions());
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
            Assert.AreEqual("Message", result.GetFirstMessage());
            Assert.AreEqual("Message", result.GetFirstError());
            Assert.IsTrue(result.HasAnyErrorsOrExceptions());
            Assert.IsFalse(result.HasAnyErrors());
            Assert.IsTrue(result.HasAnyExceptions());
            Assert.AreEqual(result.Messages.Count(x => x.MessageType == MessageType.Exception), 1);

            Assert.IsFalse(resultOfT.IsSuccess);
            Assert.IsTrue(resultOfT.IsFailure);
            Assert.AreEqual("MessageOfT", resultOfT.GetFirstMessage());
            Assert.AreEqual("MessageOfT", resultOfT.GetFirstError());
            Assert.IsTrue(resultOfT.HasAnyErrorsOrExceptions());
            Assert.IsFalse(resultOfT.HasAnyErrors());
            Assert.IsTrue(resultOfT.HasAnyExceptions());
            Assert.AreEqual(resultOfT.Messages.Count(x=>x.MessageType == MessageType.Exception), 2);
        }

        [TestMethod]
        public void ExceptionImplicit_NullException_ThrowsArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Result<int> _ = (Exception)null;
            }, "Implicit conversion from a null Exception to Result<T> must throw ArgumentNullException.");

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                Result _ = (Exception)null;
            }, "Implicit conversion from a null Exception to Result must throw ArgumentNullException.");
        }
    }
}
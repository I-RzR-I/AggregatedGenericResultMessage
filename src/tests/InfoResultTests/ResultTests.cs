// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 14:07
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="ResultTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Linq;
using InfoResultTests.Dtos;
using InfoResultTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Helpers;
using RzR.ResultMessage.Models;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void SimpleInfoResultTest()
        {
            var date = DateTime.Now.Date;
            var data = BookService.Instance.GetBookByRegDate(date);

            var d1 = Result<BookDto>.Success(data.Response);

            Assert.IsTrue(d1.IsSuccess);
            Assert.IsTrue(d1.Response != null);
            Assert.IsTrue(d1.Response.RegisteredOn == date);
        }

        [TestMethod]
        public void NotFoundBookTest()
        {
            var date = DateTime.Now.Date.AddDays(-1);
            var data = BookService.Instance.GetBookByRegDate(date);

            Assert.IsFalse((bool)data.IsSuccess);
            Assert.IsFalse((bool)(data.Response != null));
            Assert.IsTrue(data.Messages.FirstOrDefault()?.MessageType == MessageType.Warning);
        }

        [TestMethod]
        public void ImplicitResultTest()
        {
            Result exResult = new Exception("Error message");
            Result resultTrue = true;
            Result resultFalse = false;


            Assert.IsNotNull(exResult);
            Assert.IsFalse(exResult.IsSuccess);

            Assert.IsNotNull(resultTrue);
            Assert.IsTrue(resultTrue.IsSuccess);

            Assert.IsNotNull(resultFalse);
            Assert.IsFalse(resultFalse.IsSuccess);
            Assert.AreEqual(ExceptionCodes.UnSuccessfullyReqExec, resultFalse.Messages.FirstOrDefault()?.Key);
        }

        [TestMethod]
        public void Success_WithoutRelatedObjects_DoesNotAddGhostMessage()
        {
            var data = BookService.Instance.GetBookByRegDate(DateTime.Now.Date).Response;

            var result = Result<BookDto>.Success(data);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Response);
            Assert.AreEqual(0, result.Messages.Count,
                "Success without related objects must not add any messages.");
        }

        [TestMethod]
        public void Success_WithEmptyRelatedObjects_DoesNotAddGhostMessage()
        {
            var data = BookService.Instance.GetBookByRegDate(DateTime.Now.Date).Response;

            // Calls the params overload with an explicitly empty array.
            var result = Result<BookDto>.Success(data, new RelatedObjectModel[0]);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Response);
            Assert.AreEqual(0, result.Messages.Count,
                "Success with an empty related-objects array must not add any messages.");
        }

        [TestMethod]
        public void Success_WithNullRelatedObjects_DoesNotAddGhostMessage()
        {
            var data = BookService.Instance.GetBookByRegDate(DateTime.Now.Date).Response;

            // Explicit null for the params array.
            var result = Result<BookDto>.Success(data, (RelatedObjectModel[])null);

            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Response);
            Assert.AreEqual(0, result.Messages.Count,
                "Success with a null related-objects array must not add any messages.");
        }

        [TestMethod]
        public void Success_WithRelatedObjects_AddsSingleMessageWithRelatedObjects()
        {
            var data = BookService.Instance.GetBookByRegDate(DateTime.Now.Date).Response;
            var related = new RelatedObjectModel("BookDto", "books");

            var result = Result<BookDto>.Success(data, related);

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual(1, result.Messages.First().RelatedObjects.Count);
            Assert.AreEqual("BookDto", result.Messages.First().RelatedObjects.First().InCodeName);
        }

        [TestMethod]
        public void Create_ReturnsDistinctInstances()
        {
            // Result<T>.Create() (and its legacy alias .Instance) must return a fresh instance every call
            var a = Result<int>.Create();
            var b = Result<int>.Create();

            Assert.IsNotNull(a);
            Assert.IsNotNull(b);
            Assert.AreNotSame(a, b);

            a.IsSuccess = true;
            Assert.IsFalse(b.IsSuccess);
        }
    }
}
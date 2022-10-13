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
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Messages;
using AggregatedGenericResultMessage.Helpers;
using InfoResultTests.Dtos;
using InfoResultTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.IsFalse(data.IsSuccess);
            Assert.IsFalse(data.Response != null);
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
    }
}
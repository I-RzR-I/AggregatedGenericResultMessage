// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 15:09
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
using AggregatedGenericResultMessage.Extensions.Common;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using InfoResultTests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class ErrorTests
    {
        [TestMethod]
        public void AddErrorTest()
        {
            var res = new Result
            {
                IsSuccess = false
            };

            res.AddError("Error-01");
            res.AddError("error-01", "ErrorMessage-01");

            res.AddErrorConfirm("ErrorMessage-Confirm-01");
            res.AddErrorConfirm("error-key-Confirm-01", "ErrorMessage-01");

            Assert.IsFalse(res.IsSuccess);
            Assert.IsNull(res.Response);
            Assert.IsTrue(res.Messages.Any(x =>
                 x.Key.IsNullOrEmpty() && x.Message == "Error-01" && x.MessageType == MessageType.Error));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "error-01" && x.Message == "ErrorMessage-01" && x.MessageType == MessageType.Error));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message == "ErrorMessage-Confirm-01" &&
                x.MessageType == MessageType.ErrorConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "error-key-Confirm-01" && x.Message == "ErrorMessage-01" &&
                x.MessageType == MessageType.ErrorConfirm));

            Assert.IsTrue(res.HasAnyErrors());
            Assert.IsFalse(res.HasAnyExceptions());
            Assert.IsTrue(res.HasAnyErrorsOrExceptions());
        }

        [TestMethod]
        public void GetFirstErrorTest()
        {
            var res = new Result();
            res.AddInfo("info message");
            res.AddError("error message");

            Assert.IsTrue(res.GetFirstMessage().Equals("info message"));
            Assert.IsTrue(res.GetFirstError().Equals("error message"));
            Assert.IsTrue(res.HasAnyErrors());
            Assert.IsFalse(res.HasAnyExceptions());
            Assert.IsTrue(res.HasAnyErrorsOrExceptions());
        }
    }
}
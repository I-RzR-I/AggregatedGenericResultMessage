// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-01 03:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="InfoTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Linq;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Result;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;
using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class InfoTests
    {
        [TestMethod]
        public void AddInfoTest()
        {
            var res = new Result
            {
                IsSuccess = true
            };

            res.AddInfo("InfoMessage-1");
            res.AddInfo("key-01", "InfoMessage-1");

            res.AddInfoConfirm("InfoMessage-Confirm-1");
            res.AddInfoConfirm("key-Confirm-01", "InfoMessage-1");

            Assert.IsTrue(res.IsSuccess);
            Assert.IsNull(res.Response);

            Assert.AreEqual("InfoMessage-1", res.GetFirstMessage());
            Assert.AreEqual("InfoMessage-1", res.GetFirstMessageWithDetails().Info);
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "key-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message.Info == "InfoMessage-Confirm-1" &&
                x.MessageType == MessageType.InfoConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "key-Confirm-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.InfoConfirm));
        }

        [TestMethod]
        public void AddInfoTest1()
        {
            var res = new Result() { IsSuccess = true };

            res.AddInfo("InfoMessage-1");
            res.AddInfo("key-01", "InfoMessage-1");

            res.AddInfoConfirm("InfoMessage-Confirm-1");
            res.AddInfoConfirm("key-Confirm-01", "InfoMessage-1");

            IResult res1 = res;

            Assert.IsTrue(res1.IsSuccess);

            Assert.AreEqual("InfoMessage-1", res1.GetFirstMessage());
            Assert.AreEqual("InfoMessage-1", res1.GetFirstMessageWithDetails().Info);
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Key == "key-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message.Info == "InfoMessage-Confirm-1" &&
                x.MessageType == MessageType.InfoConfirm));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Key == "key-Confirm-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.InfoConfirm));
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_NullDetails_Test()
        {
            var res = new Result() { IsSuccess = false };
            res.WithError("Error-Info-Message-1", "Error-Info-Code-1")
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual(null, fistMessageWithDetails.Details.FirstOrDefault());
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_Test()
        {
            var res = new Result() { IsSuccess = false };
            res.WithError(new MessageDataModel("Error-Info-Message-1", "Error-Info-Code-1"))
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual("Error-Info-Code-1", fistMessageWithDetails.Details.FirstOrDefault());
            Assert.AreEqual("Message: Error-Info-Message-1; Details: Error-Info-Code-1", fistMessageWithDetails.ToString());
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_InFailure_Test()
        {
            var res = MockFailure();

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual("Error-Info-Code-1", fistMessageWithDetails.Details.FirstOrDefault());
            Assert.AreEqual("Message: Error-Info-Message-1; Details: Error-Info-Code-1", fistMessageWithDetails.ToString());
        }

        private static IResult MockFailure()
        {
            return Result.Failure(new MessageDataModel("Error-Info-Message-1", "Error-Info-Code-1"))
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);
        }
    }
}
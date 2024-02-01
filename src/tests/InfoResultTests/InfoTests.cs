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
using AggregatedGenericResultMessage.Extensions.Result.Messages;
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
    }
}
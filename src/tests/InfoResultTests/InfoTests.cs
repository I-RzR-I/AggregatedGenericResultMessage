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
using AggregatedGenericResultMessage.Enums;
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
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == string.Empty && x.Message == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "key-01" && x.Message == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == string.Empty && x.Message == "InfoMessage-Confirm-1" &&
                x.MessageType == MessageType.InfoConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "key-Confirm-01" && x.Message == "InfoMessage-1" && x.MessageType == MessageType.InfoConfirm));
        }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 14:11
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="ErrorTests.cs" company="">
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
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class WarnTests
    {
        [TestMethod]
        public void AddWarningTest()
        {
            var res = new Result
            {
                IsSuccess = false
            };

            res.AddWarning("Warn-01");
            res.AddWarning("warn-01", "WarnMessage-01");

            res.AddWarningConfirm("WarnMessage-Confirm-01");
            res.AddWarningConfirm("warn-key-Confirm-01", "WarnMessage-01");

            Assert.IsFalse(res.IsSuccess);
            Assert.IsNull(res.Response);
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message == "Warn-01" && x.MessageType == MessageType.Warning));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "warn-01" && x.Message == "WarnMessage-01" && x.MessageType == MessageType.Warning));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key.IsNullOrEmpty() && x.Message == "WarnMessage-Confirm-01" &&
                x.MessageType == MessageType.WarningConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Key == "warn-key-Confirm-01" && x.Message == "WarnMessage-01" &&
                x.MessageType == MessageType.WarningConfirm));
        }
    }
}
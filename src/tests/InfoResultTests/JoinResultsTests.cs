// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2026-04-21 19:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-21 20:00
// ***********************************************************************
//  <copyright file="JoinResultsTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result.Messages;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class JoinResultsTests
    {
        [TestMethod]
        public void JoinErrors_AllSuccess_ReturnsSuccess()
        {
            var joined = Result<object>.Instance.JoinErrors(new List<Result>
            {
                new Result { IsSuccess = true },
                new Result { IsSuccess = true }
            });

            Assert.IsTrue(joined.IsSuccess);
            Assert.AreEqual(0, joined.Messages.Count);
        }

        [TestMethod]
        public void JoinErrors_AnyFailure_ReturnsFailureAndAggregatesErrorMessages()
        {
            var failed = new Result { IsSuccess = false };
            failed.AddError("boom");
            failed.AddException(new Exception("explosion"));
            failed.AddWarning("just-a-warning"); // must be filtered out

            var succeeded = new Result { IsSuccess = true };

            var joined = Result<object>.Instance.JoinErrors(new List<Result> { failed, succeeded });

            Assert.IsFalse(joined.IsSuccess,
                "JoinErrors must report failure when any input result has failed.");
            Assert.AreEqual(2, joined.Messages.Count,
                "Only Error + Exception messages should be aggregated; warnings must be excluded.");
            Assert.IsTrue(joined.Messages.All(m =>
                m.MessageType == MessageType.Error || m.MessageType == MessageType.Exception));
        }

        [TestMethod]
        public void JoinErrors_DoesNotInheritIsSuccessFromCaller()
        {
            // Regression: historically IsSuccess was copied from the caller (`this`),
            // so a successful caller aggregating failures produced a "successful" result
            // containing error messages. The fix derives IsSuccess from the input collection.
            var caller = Result<object>.Success();
            Assert.IsTrue(caller.IsSuccess);

            var failed = new Result { IsSuccess = false };
            failed.AddError("boom");

            var joined = caller.JoinErrors(new List<Result> { failed });

            Assert.IsFalse(joined.IsSuccess);
            Assert.AreEqual(1, joined.Messages.Count);
        }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2023-04-02 17:47
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-04-02 17:47
// ***********************************************************************
//  <copyright file="ActionResultTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Extensions.Result.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class ActionResultTests
    {
        [TestMethod]
        public void ActionOnSuccessTest()
        {
            var isSuccess = false;
            var isSuccess1 = false;
            var isSuccessOfT = false;
            var isSuccessOfT1 = false;

            var result = new Result { IsSuccess = true };
            IResult result1 = new Result { IsSuccess = true };
            var resultOfT = new Result<bool> { IsSuccess = true, Response = true };
            IResult<bool> resultOfT1 = new Result<bool> { IsSuccess = true, Response = true };

            result.ActionOnSuccess(x =>
            {
                isSuccess = true;
            });

            result1.ActionOnSuccess(x =>
            {
                isSuccess1 = true;
            });

            resultOfT.ActionOnSuccess(x =>
            {
                isSuccessOfT = true;
            });

            resultOfT1.ActionOnSuccess(x =>
            {
                isSuccessOfT1 = true;
            });

            Assert.IsTrue(isSuccess);
            Assert.IsTrue(isSuccess1);
            Assert.IsTrue(isSuccessOfT);
            Assert.IsTrue(isSuccessOfT1);
        }

        [TestMethod]
        public void ActionOnFailureTest()
        {
            var isSuccess = true;
            var isSuccessOfT = true;
            var isSuccess1 = true;
            var isSuccessOfT1 = true;

            var result = new Result { IsSuccess = false };
            var resultOfT = new Result<bool> { IsSuccess = false, Response = false };
            IResult result1 = new Result { IsSuccess = false };
            IResult<bool> resultOfT1 = new Result<bool> { IsSuccess = false, Response = false };

            result.ActionOnFailure(x =>
            {
                isSuccess = false;
            });
            resultOfT.ActionOnFailure(x =>
            {
                isSuccessOfT = false;
            });
            result1.ActionOnFailure(x =>
            {
                isSuccess1 = false;
            });
            resultOfT1.ActionOnFailure(x =>
            {
                isSuccessOfT1 = false;
            });

            Assert.IsFalse(isSuccess);
            Assert.IsFalse(isSuccessOfT);
            Assert.IsFalse(isSuccess1);
            Assert.IsFalse(isSuccessOfT1);
        }

        [TestMethod]
        public void ActionExecuteActionsTest()
        {
            var input = 0;
            var result = new Result() { IsSuccess = true }
                .ExecuteAction(
                    x => { input++; },
                    x => { input++; });

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(2, input);
        }
    }
}
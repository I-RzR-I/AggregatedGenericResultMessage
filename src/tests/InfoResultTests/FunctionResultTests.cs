// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2024-01-31 21:08
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-31 21:08
// ***********************************************************************
//  <copyright file="FunctionResultTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System.Threading.Tasks;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Extensions.Result.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfoResultTests
{
    [TestClass]
    public class FunctionResultTests
    {
        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public void ExecuteFunctionTest_1_Success(bool ignoreError, bool exceptedResult)
        {
            var result = new Result() { IsSuccess = true }
                //.ExecuteFunction(() => { return FuncTest();});
                .ExecuteFunction(ignoreError, FuncTestSuccess);

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, false)]
        [DataRow(true, true)]
        public void ExecuteFunctionTest_1_Failure(bool ignoreError, bool exceptedResult)
        {
            var result = new Result() { IsSuccess = true }
                //.ExecuteFunction(() => { return FuncTest();});
                .ExecuteFunction(ignoreError, FuncTestFailure);

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public void ExecuteFunctionTest_2_Success(bool ignoreError, bool exceptedResult)
        {
            var result = new Result() { IsSuccess = true }
                //.ExecuteFunction(FuncTestTask);
                //.ExecuteFunction(async () => { return await FuncTestTask(); });
                .ExecuteFunction(ignoreError, async () => await FuncTestSuccessTask());

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, false)]
        [DataRow(true, true)]
        public void ExecuteFunctionTest_2_Failure(bool ignoreError, bool exceptedResult)
        {
            var result = new Result() { IsSuccess = true }
                //.ExecuteFunction(FuncTestTask);
                //.ExecuteFunction(async () => { return await FuncTestTask(); });
                .ExecuteFunction(ignoreError, async () => await FuncTestFailureTask());
            
            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        private IResult FuncTestSuccess() => Result.Success();

        private IResult FuncTestFailure() => Result.Failure();

        private async Task<IResult> FuncTestSuccessTask() => await Task.FromResult(Result.Success());

        private async Task<IResult> FuncTestFailureTask() => await Task.FromResult(Result.Failure());
    }
}
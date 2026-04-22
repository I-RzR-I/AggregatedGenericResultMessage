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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Result.Functions;

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
#pragma warning disable CS0618 // legacy sync-over-async overload, intentionally exercised
            var result = new Result() { IsSuccess = true }
                .ExecuteFunction(ignoreError, async () => await FuncTestSuccessTask());
#pragma warning restore CS0618

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, false)]
        [DataRow(true, true)]
        public void ExecuteFunctionTest_2_Failure(bool ignoreError, bool exceptedResult)
        {
#pragma warning disable CS0618 // legacy sync-over-async overload, intentionally exercised
            var result = new Result() { IsSuccess = true }
                .ExecuteFunction(ignoreError, async () => await FuncTestFailureTask());
#pragma warning restore CS0618

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, true)]
        [DataRow(true, true)]
        public async Task ExecuteFunctionAsyncTest_2_Success(bool ignoreError, bool exceptedResult)
        {
            var result = await new Result() { IsSuccess = true }
                .ExecuteFunctionAsync(ignoreError, FuncTestSuccessTask);

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        [DataRow(false, false)]
        [DataRow(true, true)]
        public async Task ExecuteFunctionAsyncTest_2_Failure(bool ignoreError, bool exceptedResult)
        {
            var result = await new Result() { IsSuccess = true }
                .ExecuteFunctionAsync(ignoreError, FuncTestFailureTask);

            Assert.AreEqual(exceptedResult, result.IsSuccess);
        }

        [TestMethod]
        public async Task FunctionOnSuccessAsync_RunsOnlyWhenSuccess()
        {
            var calls = 0;

            var result = await Result.Success()
                .FunctionOnSuccessAsync(true, async () =>
                {
                    calls++;
                    return await Task.FromResult(Result.Success());
                });

            Assert.AreEqual(1, calls);
            Assert.IsTrue(result.IsSuccess);

            calls = 0;
            result = await Result.Failure()
                .FunctionOnSuccessAsync(true, async () =>
                {
                    calls++;
                    return await Task.FromResult(Result.Success());
                });

            Assert.AreEqual(0, calls);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public async Task FunctionOnFailureAsync_RunsOnlyWhenFailure()
        {
            var calls = 0;

            var result = await Result.Failure()
                .FunctionOnFailureAsync(true, async () =>
                {
                    calls++;
                    return await Task.FromResult(Result.Failure());
                });

            Assert.AreEqual(1, calls);

            calls = 0;
            result = await Result.Success()
                .FunctionOnFailureAsync(true, async () =>
                {
                    calls++;
                    return await Task.FromResult(Result.Failure());
                });

            Assert.AreEqual(0, calls);
        }

        [TestMethod]
        public async Task FunctionOnAsync_DispatchesOnSuccessOrFailure()
        {
            var successCalls = 0;
            var failureCalls = 0;

            await Result.Success().FunctionOnAsync(
                async () => { successCalls++; return await Task.FromResult(Result.Success()); },
                async () => { failureCalls++; return await Task.FromResult(Result.Failure()); });

            await Result.Failure().FunctionOnAsync(
                async () => { successCalls++; return await Task.FromResult(Result.Success()); },
                async () => { failureCalls++; return await Task.FromResult(Result.Failure()); });

            Assert.AreEqual(1, successCalls);
            Assert.AreEqual(1, failureCalls);
        }

        private IResult FuncTestSuccess() => Result.Success();

        private IResult FuncTestFailure() => Result.Failure();

        private async Task<IResult> FuncTestSuccessTask() => await Task.FromResult(Result.Success());

        private async Task<IResult> FuncTestFailureTask() => await Task.FromResult(Result.Failure());
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2026-04-22 00:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 00:23
// ***********************************************************************
//  <copyright file="MatchExtensionsTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Result;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class MatchExtensionsTests
    {
        [TestMethod]
        public void Match_OnIResult_Success_ReturnsOnSuccessValue()
        {
            IResult result = Result.Success();

            var code = result.Match(_ => 200, _ => 500);

            Assert.AreEqual(200, code);
        }

        [TestMethod]
        public void Match_OnIResult_Failure_ReturnsOnFailureValue()
        {
            IResult result = Result.Failure("boom");

            var code = result.Match(_ => 200, _ => 500);

            Assert.AreEqual(500, code);
        }

        [TestMethod]
        public void Match_OnIResult_Parameterless_Success_ReturnsOnSuccess()
        {
            IResult result = Result.Success();

            var label = result.Match(() => "ok", () => "err");

            Assert.AreEqual("ok", label);
        }

        [TestMethod]
        public void Match_OnIResult_ActionOverload_ReturnsSameInstanceAndRunsCorrectBranch()
        {
            IResult result = Result.Success();
            var ranSuccess = false;
            var ranFailure = false;

            Action<IResult> onSuccess = _ => { ranSuccess = true; };
            Action<IResult> onFailure = _ => { ranFailure = true; };

            var returned = result.Match(onSuccess, onFailure);

            Assert.AreSame(result, returned);
            Assert.IsTrue(ranSuccess);
            Assert.IsFalse(ranFailure);
        }

        [TestMethod]
        public void Match_OnIResult_NullArgs_Throw()
        {
            IResult result = Result.Success();

            Assert.ThrowsException<ArgumentNullException>(() => result.Match(null, _ => "x"));
            Assert.ThrowsException<ArgumentNullException>(() => result.Match(_ => "x", null));
            Assert.ThrowsException<ArgumentNullException>(() => ((IResult)null).Match(_ => "x", _ => "y"));
        }

        [TestMethod]
        public async Task MatchAsync_OnResultOfT_Success_InvokesOnSuccessAsync()
        {
            var result = Result<int>.Success(9);

            var folded = await result.MatchAsync(
                async v =>
                {
                    await Task.Yield();
                    return "ok:" + v;
                },
                async _ =>
                {
                    await Task.Yield();
                    return "fail";
                });

            Assert.AreEqual("ok:9", folded);
        }

        [TestMethod]
        public async Task MatchAsync_OnResultOfT_Failure_InvokesOnFailureAsync()
        {
            var result = Result<int>.Failure("bad");

            var folded = await result.MatchAsync(
                async _ =>
                {
                    await Task.Yield();
                    return "ok";
                },
                async r =>
                {
                    await Task.Yield();
                    return "fail:" + r.IsFailure;
                });

            Assert.AreEqual("fail:True", folded);
        }

        [TestMethod]
        public async Task MatchAsync_OnTask_SyncSelectors_Fold()
        {
            var source = Task.FromResult(Result<int>.Success(4));

            var folded = await source.MatchAsync(v => v * 10, _ => -1);

            Assert.AreEqual(40, folded);
        }

        [TestMethod]
        public async Task MatchAsync_OnTask_AsyncSelectors_Fold_OnFailure()
        {
            var source = Task.FromResult(Result<int>.Failure("x"));

            var folded = await source.MatchAsync(
                async v =>
                {
                    await Task.Yield();
                    return v.ToString();
                },
                async _ =>
                {
                    await Task.Yield();
                    return "failed";
                });

            Assert.AreEqual("failed", folded);
        }

        [TestMethod]
        public async Task MatchAsync_OnIResult_SelectsCorrectBranch()
        {
            IResult success = Result.Success();
            IResult failure = Result.Failure("e");

            var a = await success.MatchAsync(
                async _ =>
                {
                    await Task.Yield();
                    return 1;
                },
                async _ =>
                {
                    await Task.Yield();
                    return 2;
                });
            var b = await failure.MatchAsync(
                async _ =>
                {
                    await Task.Yield();
                    return 1;
                },
                async _ =>
                {
                    await Task.Yield();
                    return 2;
                });

            Assert.AreEqual(1, a);
            Assert.AreEqual(2, b);
        }

        [TestMethod]
        public async Task MatchAsync_NullArgs_Throw()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                ((Task<Result<int>>)null).MatchAsync(x => x, _ => 0));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Task.FromResult(Result<int>.Success(1))
                .MatchAsync(null, _ => 0));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Task.FromResult(Result<int>.Success(1))
                .MatchAsync(x => x, null));
        }
    }
}
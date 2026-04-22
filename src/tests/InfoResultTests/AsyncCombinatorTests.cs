// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2026-04-22 00:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 00:04
// ***********************************************************************
//  <copyright file="AsyncCombinatorTests.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Extensions.Result;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class AsyncCombinatorTests
    {
        [TestMethod]
        public async Task MapAsync_OnResult_AsyncSelector_ProjectsOnSuccess()
        {
            var source = Result<string>.Success("hello");

            var mapped = await source.MapAsync(async s =>
            {
                await Task.Yield();
                return s.Length;
            });

            Assert.IsTrue(mapped.IsSuccess);
            Assert.AreEqual(5, mapped.Response);
        }

        [TestMethod]
        public async Task MapAsync_OnResult_OnFailure_PropagatesAndDoesNotInvokeSelector()
        {
            var invoked = false;
            var source = Result<string>.Failure("boom");

            var mapped = await source.MapAsync(async _ =>
            {
                invoked = true;
                await Task.Yield();
                return 1;
            });

            Assert.IsFalse(invoked);
            Assert.IsFalse(mapped.IsSuccess);
            Assert.IsTrue(mapped.Messages.Any(m => m.Message.Info == "boom"));
        }

        [TestMethod]
        public async Task MapAsync_OnTask_SyncSelector_Projects()
        {
            var source = Task.FromResult(Result<int>.Success(4));

            var mapped = await source.MapAsync(x => x * 3);

            Assert.IsTrue(mapped.IsSuccess);
            Assert.AreEqual(12, mapped.Response);
        }

        [TestMethod]
        public async Task MapAsync_OnTask_AsyncSelector_Projects()
        {
            var source = Task.FromResult(Result<int>.Success(2));

            var mapped = await source.MapAsync(async x =>
            {
                await Task.Yield();
                return "n=" + x;
            });

            Assert.IsTrue(mapped.IsSuccess);
            Assert.AreEqual("n=2", mapped.Response);
        }

        [TestMethod]
        public async Task MapAsync_NullArgs_Throw()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                ((Result<int>)null).MapAsync(x => Task.FromResult(x)));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                Result<int>.Success(1).MapAsync((Func<int, Task<int>>)null));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => ((Task<Result<int>>)null).MapAsync(x => x));
        }

        [TestMethod]
        public async Task BindAsync_OnResult_AsyncBinder_OnSuccess_ReturnsBound()
        {
            var source = Result<int>.Success(7);

            var bound = await source.BindAsync(async x =>
            {
                await Task.Yield();
                return Result<string>.Success("v=" + x);
            });

            Assert.IsTrue(bound.IsSuccess);
            Assert.AreEqual("v=7", bound.Response);
        }

        [TestMethod]
        public async Task BindAsync_OnResult_OnFailure_PropagatesAndDoesNotInvokeBinder()
        {
            var invoked = false;
            var source = Result<int>.Failure("nope");

            var bound = await source.BindAsync(async _ =>
            {
                invoked = true;
                await Task.Yield();
                return Result<string>.Success("x");
            });

            Assert.IsFalse(invoked);
            Assert.IsFalse(bound.IsSuccess);
            Assert.IsTrue(bound.Messages.Any(m => m.Message.Info == "nope"));
        }

        [TestMethod]
        public async Task BindAsync_OnResult_BinderReturnsNull_Throws()
        {
            var source = Result<int>.Success(1);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
                source.BindAsync(_ => Task.FromResult<Result<string>>(null)));
        }

        [TestMethod]
        public async Task BindAsync_OnTask_SyncBinder_Binds()
        {
            var source = Task.FromResult(Result<int>.Success(3));

            var bound = await source.BindAsync(x => Result<string>.Success("=" + x));

            Assert.IsTrue(bound.IsSuccess);
            Assert.AreEqual("=3", bound.Response);
        }

        [TestMethod]
        public async Task BindAsync_OnTask_AsyncBinder_Binds()
        {
            var source = Task.FromResult(Result<int>.Success(5));

            var bound = await source.BindAsync(async x =>
            {
                await Task.Yield();
                return Result<int>.Success(x + 1);
            });

            Assert.IsTrue(bound.IsSuccess);
            Assert.AreEqual(6, bound.Response);
        }

        [TestMethod]
        public async Task Tap_OnTask_OnSuccess_RunsActionAndReturnsSameResult()
        {
            var seen = 0;
            var inner = Result<int>.Success(11);
            var source = Task.FromResult(inner);

            var returned = await source.Tap(v => seen = v);

            Assert.AreSame(inner, returned);
            Assert.AreEqual(11, seen);
        }

        [TestMethod]
        public async Task TapAsync_OnResult_AsyncAction_RunsOnlyOnSuccess()
        {
            var seen = 0;
            var success = Result<int>.Success(8);
            var failure = Result<int>.Failure("err");

            var rs = await success.TapAsync(async v =>
            {
                await Task.Yield();
                seen = v;
            });
            var rf = await failure.TapAsync(async _ =>
            {
                await Task.Yield();
                seen = -1;
            });

            Assert.AreSame(success, rs);
            Assert.AreSame(failure, rf);
            Assert.AreEqual(8, seen);
        }

        [TestMethod]
        public async Task TapAsync_OnTask_AsyncAction_Composes()
        {
            var seen = 0;
            var source = Task.FromResult(Result<int>.Success(2));

            var returned = await source.TapAsync(async v =>
            {
                await Task.Yield();
                seen = v * 10;
            });

            Assert.IsTrue(returned.IsSuccess);
            Assert.AreEqual(20, seen);
        }

        [TestMethod]
        public async Task TapAsync_NullArgs_Throw()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                ((Result<int>)null).TapAsync(_ => Task.CompletedTask));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Result<int>.Success(1).TapAsync(null));
        }

        [TestMethod]
        public async Task FullAsyncPipeline_MapAsync_BindAsync_TapAsync_Composes()
        {
            var sideEffect = 0;

            var result = await Task.FromResult(Result<int>.Success(2))
                .MapAsync(async x =>
                {
                    await Task.Yield();
                    return x * 5;
                }) // 10
                .TapAsync(async x =>
                {
                    await Task.Yield();
                    sideEffect = x;
                }) // sideEffect = 10
                .BindAsync(async x =>
                {
                    await Task.Yield();
                    return Result<string>.Success("n=" + x);
                }); // "n=10"

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("n=10", result.Response);
            Assert.AreEqual(10, sideEffect);
        }
    }
}
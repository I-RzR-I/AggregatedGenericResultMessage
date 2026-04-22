// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2026-04-22 02:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 08:24
// ***********************************************************************
//  <copyright file="ValidationExtensionsTests.cs" company="RzR SOFT & TECH">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using InfoResultTests.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Extensions.Result;
using RzR.ResultMessage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace InfoResultTests
{
    [TestClass]
    public class ValidationExtensionsTests
    {
        [TestMethod]
        public void Validate_AllPredicatesPass_KeepsResultSuccessful()
        {
            var order = new OrderDto() { ItemCount = 3, Total = 50m, Customer = "Alice" };

            var result = Result<OrderDto>.Success(order)
                .Validate(o => o.ItemCount > 0, "items required")
                .Validate(o => o.Total > 0, "total required")
                .Validate(o => o.Customer != null, "customer required");

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(0, result.Messages.Count);
        }

        [TestMethod]
        public void Validate_OnePredicateFails_FlipsToFailureAndAddsError()
        {
            var order = new OrderDto() { ItemCount = 0, Total = 50m, Customer = "Alice" };

            var result = Result<OrderDto>.Success(order)
                .Validate(o => o.ItemCount > 0, "items required")
                .Validate(o => o.Total > 0, "total required");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual("items required", result.Messages.First().Message.Info);
        }

        [TestMethod]
        public void Validate_AccumulatesMultipleErrors()
        {
            var order = new OrderDto() { ItemCount = 0, Total = -5m, Customer = null };

            var result = Result<OrderDto>.Success(order)
                .Validate(o => o.ItemCount > 0, "items required")
                .Validate(o => o.Total > 0, "total required")
                .Validate(o => o.Customer != null, "customer required");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(3, result.Messages.Count);
            CollectionAssert.AreEqual(
                new[] { "items required", "total required", "customer required" },
                result.Messages.Select(m => m.Message.Info).ToArray());
        }

        [TestMethod]
        public void Validate_WithKey_AppendsKeyedError()
        {
            var order = new OrderDto() { ItemCount = 0 };

            var result = Result<OrderDto>.Success(order)
                .Validate(o => o.ItemCount > 0, "Order.Items", "items required");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Order.Items", result.Messages.First().Key);
            Assert.AreEqual("items required", result.Messages.First().Message.Info);
        }

        [TestMethod]
        public void Validate_WithMessageDataModel_PreservesDetails()
        {
            var order = new OrderDto() { Total = 0m };
            var error = new MessageDataModel("total invalid", "must be > 0");

            var result = Result<OrderDto>.Success(order)
                .Validate(o => o.Total > 0, error);

            Assert.IsFalse(result.IsSuccess);
            var msg = result.Messages.First();
            Assert.AreEqual("total invalid", msg.Message.Info);
            CollectionAssert.AreEqual(new[] { "must be > 0" }, msg.Message.Details.ToArray());
        }

        [TestMethod]
        public void Validate_NullArgs_Throw()
        {
            var result = Result<int>.Success(1);

            Assert.ThrowsException<ArgumentNullException>(() => ((Result<int>)null).Validate(_ => true, "e"));
            Assert.ThrowsException<ArgumentNullException>(() => result.Validate(null, "e"));
            Assert.ThrowsException<ArgumentNullException>(() => result.Validate(_ => true, (MessageDataModel)null));
        }

        [TestMethod]
        public void Validate_FailingResult_StillRunsPredicateAndAccumulatesError()
        {
            // The whole point of accumulating Validate is that a previous failure does not stop
            // additional violations from being recorded.
            var result = Result<int>.Failure("upstream failure")
                .Validate(_ => false, "downstream violation");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(2, result.Messages.Count);
            Assert.IsTrue(result.Messages.Any(m => m.Message.Info == "upstream failure"));
            Assert.IsTrue(result.Messages.Any(m => m.Message.Info == "downstream violation"));
        }

        [TestMethod]
        public void Ensure_OnSuccess_RunsPredicate()
        {
            var result = Result<int>.Success(5)
                .Ensure(x => x > 10, "must be > 10");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count);
        }

        [TestMethod]
        public void Ensure_OnFailure_SkipsPredicate()
        {
            var invoked = false;
            var result = Result<string>.Failure("upstream")
                .Ensure(_ =>
                {
                    invoked = true;
                    return true;
                }, "should not run");

            Assert.IsFalse(invoked);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count); // upstream only
        }

        [TestMethod]
        public void Ensure_AfterFirstFailure_SkipsRest()
        {
            var second = false;
            var result = Result<int>.Success(1)
                .Ensure(x => x > 5, "first")
                .Ensure(_ =>
                {
                    second = true;
                    return true;
                }, "second");

            Assert.IsFalse(second, "Once Ensure flips IsSuccess to false, later Ensure calls must short-circuit.");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual("first", result.Messages.First().Message.Info);
        }

        [TestMethod]
        public async Task ValidateAsync_OnResult_AsyncPredicate_AccumulatesError()
        {
            var result = await Result<int>.Success(2)
                .ValidateAsync(async x =>
                {
                    await Task.Yield();
                    return x > 10;
                }, "must be > 10");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("must be > 10", result.Messages.First().Message.Info);
        }

        [TestMethod]
        public async Task ValidateAsync_OnTask_AsyncPredicate_Composes()
        {
            var result = await Task.FromResult(Result<int>.Success())
                .ValidateAsync(x => x >= 0, "non-negative")
                .ValidateAsync(async x =>
                {
                    await Task.Yield();
                    return x > 0;
                }, "positive");

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Messages.Count);
            Assert.AreEqual("positive", result.Messages.First().Message.Info);
        }

        [TestMethod]
        public async Task ValidateAsync_OnTask_NullArgs_Throw()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                ((Task<Result<int>>)null).ValidateAsync(_ => true, "e"));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => Task.FromResult(Result<int>.Success(1))
                .ValidateAsync((Func<int, bool>)null, "e"));
        }
    }
}
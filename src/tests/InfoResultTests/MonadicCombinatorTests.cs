using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;

namespace InfoResultTests
{
    [TestClass]
    public class MonadicCombinatorTests
    {
        [TestMethod]
        public void Map_OnSuccess_ProjectsResponse_Test()
        {
            var source = Result<string>.Success("hello");

            var mapped = source.Map(s => s.Length);

            Assert.IsTrue(mapped.IsSuccess);
            Assert.AreEqual(5, mapped.Response);
        }

        [TestMethod]
        public void Map_OnFailure_PropagatesFailureWithoutInvokingSelector_Test()
        {
            var invoked = false;
            var source = Result<string>.Failure("boom");

            var mapped = source.Map(_ =>
            {
                invoked = true;
                return 42;
            });

            Assert.IsFalse(invoked, "Selector must not run on failure.");
            Assert.IsFalse(mapped.IsSuccess);
            Assert.IsTrue(mapped.IsFailure);
            Assert.AreEqual(0, mapped.Response);
            Assert.IsTrue(mapped.Messages.Any(m => m.Message.Info == "boom"));
        }

        [TestMethod]
        public void Map_NullSelector_Throws_Test()
        {
            var source = Result<int>.Success(1);

            Assert.ThrowsException<ArgumentNullException>(() => source.Map<int>(null));
        }

        [TestMethod]
        public void Bind_OnSuccess_InvokesBinderAndReturnsItsResult_Test()
        {
            var source = Result<int>.Success(7);

            var bound = source.Bind(x => Result<string>.Success("v=" + x));

            Assert.IsTrue(bound.IsSuccess);
            Assert.AreEqual("v=7", bound.Response);
        }

        [TestMethod]
        public void Bind_OnFailure_PropagatesAndDoesNotInvokeBinder_Test()
        {
            var invoked = false;
            var source = Result<int>.Failure("nope");

            var bound = source.Bind(_ =>
            {
                invoked = true;
                return Result<string>.Success("x");
            });

            Assert.IsFalse(invoked);
            Assert.IsFalse(bound.IsSuccess);
            Assert.IsTrue(bound.Messages.Any(m => m.Message.Info == "nope"));
        }

        [TestMethod]
        public void Bind_BinderReturnsNull_Throws_Test()
        {
            var source = Result<int>.Success(1);

            Assert.ThrowsException<InvalidOperationException>(() => source.Bind<string>(_ => null));
        }

        [TestMethod]
        public void Bind_NullBinder_Throws_Test()
        {
            var source = Result<int>.Success(1);

            Assert.ThrowsException<ArgumentNullException>(() => source.Bind<string>(null));
        }

        [TestMethod]
        public void Match_OnSuccess_InvokesOnSuccess_Test()
        {
            var source = Result<int>.Success(10);

            var folded = source.Match(
                v => "ok:" + v,
                _ => "fail");

            Assert.AreEqual("ok:10", folded);
        }

        [TestMethod]
        public void Match_OnFailure_InvokesOnFailureWithCurrentResult_Test()
        {
            var source = Result<int>.Failure("bad");

            var folded = source.Match(
                _ => "ok",
                r => "fail:" + r.Messages.First().Message.Info);

            Assert.AreEqual("fail:bad", folded);
        }

        [TestMethod]
        public void Match_NullArgs_Throw_Test()
        {
            var source = Result<int>.Success(1);

            Assert.ThrowsException<ArgumentNullException>(() => source.Match(null, _ => "x"));
            Assert.ThrowsException<ArgumentNullException>(() => source.Match(_ => "x", null));
        }

        [TestMethod]
        public void Tap_OnSuccess_InvokesActionAndReturnsSameInstance_Test()
        {
            var source = Result<int>.Success(3);
            var seen = 0;

            var returned = source.Tap(v => seen = v);

            Assert.AreSame(source, returned);
            Assert.AreEqual(3, seen);
        }

        [TestMethod]
        public void Tap_OnFailure_DoesNotInvokeAction_Test()
        {
            var source = Result<int>.Failure("err");
            var invoked = false;

            var returned = source.Tap(_ => invoked = true);

            Assert.AreSame(source, returned);
            Assert.IsFalse(invoked);
        }

        [TestMethod]
        public void Tap_NullAction_Throws_Test()
        {
            var source = Result<int>.Success(1);

            Assert.ThrowsException<ArgumentNullException>(() => source.Tap(null));
        }

        [TestMethod]
        public void MapBindTap_Compose_AsExpected_Test()
        {
            var sideEffect = 0;

            var result = Result<int>.Success(2)
                .Map(x => x * 5)
                .Tap(x => sideEffect = x)
                .Bind(x => Result<string>.Success("n=" + x));

            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("n=10", result.Response);
            Assert.AreEqual(10, sideEffect);
        }
    }
}
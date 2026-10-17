using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Result;
using RzR.ResultMessage.Extensions.Result.Actions;

namespace InfoResultTests
{
    [TestClass]
    public class ActionResultTests
    {
        [TestMethod]
        public void ActionOnSuccess_Test()
        {
            var isSuccess = false;
            var isSuccess1 = false;
            var isSuccessOfT = false;
            var isSuccessOfT1 = false;

            var result = new Result { IsSuccess = true };
            IResult result1 = new Result { IsSuccess = true };
            var resultOfT = new Result<bool> { IsSuccess = true, Response = true };
            IResult<bool> resultOfT1 = new Result<bool> { IsSuccess = true, Response = true };

            result.Match(x => { isSuccess = true; }, _ => { });

            result1.Match(x => { isSuccess1 = true; }, _ => { });

            resultOfT.Match(x => { isSuccessOfT = true; }, _ => { });

            resultOfT1.Match(x => { isSuccessOfT1 = true; }, _ => { });

            Assert.IsTrue(isSuccess);
            Assert.IsTrue(isSuccess1);
            Assert.IsTrue(isSuccessOfT);
            Assert.IsTrue(isSuccessOfT1);
        }

        [TestMethod]
        public void ActionOnFailure_Test()
        {
            var isSuccess = true;
            var isSuccessOfT = true;
            var isSuccess1 = true;
            var isSuccessOfT1 = true;

            var result = new Result { IsSuccess = false };
            var resultOfT = new Result<bool> { IsSuccess = false, Response = false };
            IResult result1 = new Result { IsSuccess = false };
            IResult<bool> resultOfT1 = new Result<bool> { IsSuccess = false, Response = false };

            result.Match(_ => { }, x => { isSuccess = false; });
            resultOfT.Match(_ => { }, x => { isSuccessOfT = false; });
            result1.Match(_ => { }, x => { isSuccess1 = false; });
            resultOfT1.Match(_ => { }, x => { isSuccessOfT1 = false; });

            Assert.IsFalse(isSuccess);
            Assert.IsFalse(isSuccessOfT);
            Assert.IsFalse(isSuccess1);
            Assert.IsFalse(isSuccessOfT1);
        }

        [TestMethod]
        public void ActionExecuteActions_Test()
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
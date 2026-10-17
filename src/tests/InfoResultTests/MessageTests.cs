using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result.Messages;

namespace InfoResultTests
{
    [TestClass]
    public class MessageTests
    {
        private readonly Result _res;

        public MessageTests()
        {
            _res = new Result();

            _res.AddMessage("simple info message", MessageType.Info);
            _res.AddMessage("simple warning message", MessageType.Warning);
            _res.AddMessage("simple error message", MessageType.Error);

            _res.AddMessage("info-code", "simple info message with code", MessageType.Info);
            _res.AddMessage("warn-code", "simple warn message with code", MessageType.Warning);
            _res.AddMessage("error-code", "simple error message with code");
        }


        [TestMethod]
        public void GeneralMessageResult_Test()
        {
            Assert.IsFalse(_res.IsSuccess);
            Assert.IsNull(_res.Response);

            Assert.IsTrue(_res.GetFirstError().Equals("simple error message"));
            Assert.IsTrue(_res.GetFirstMessage().Equals("simple info message"));
        }
    }
}
using System.Linq;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result.Messages;

namespace InfoResultTests
{
    [TestClass]
    public class WarnTests
    {
        [TestMethod]
        public void AddWarning_Test()
        {
            var res = new Result
            {
                IsSuccess = false
            };

            res.AddWarning("Warn-01");
            res.AddWarning("warn-01", "WarnMessage-01");

            res.AddWarningConfirm("WarnMessage-Confirm-01");
            res.AddWarningConfirm("warn-key-Confirm-01", "WarnMessage-01");

            Assert.IsFalse(res.IsSuccess);
            Assert.IsNull(res.Response);
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "Warn-01" && x.MessageType == MessageType.Warning));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code == "warn-01" && x.Message.Info == "WarnMessage-01" && x.MessageType == MessageType.Warning));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "WarnMessage-Confirm-01" &&
                x.MessageType == MessageType.WarningConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code == "warn-key-Confirm-01" && x.Message.Info == "WarnMessage-01" &&
                x.MessageType == MessageType.WarningConfirm));
        }
    }
}
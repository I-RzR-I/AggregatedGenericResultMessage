using System.Linq;
using DomainCommonExtensions.CommonExtensions;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.ResultMessage.Models;

namespace InfoResultTests
{
    [TestClass]
    public class InfoTests
    {
        [TestMethod]
        public void AddInfo_Test()
        {
            var res = new Result
            {
                IsSuccess = true
            };

            res.AddInfo("InfoMessage-1");
            res.AddInfo("key-01", "InfoMessage-1");

            res.AddInfoConfirm("InfoMessage-Confirm-1");
            res.AddInfoConfirm("key-Confirm-01", "InfoMessage-1");

            Assert.IsTrue(res.IsSuccess);
            Assert.IsNull(res.Response);

            Assert.AreEqual("InfoMessage-1", res.GetFirstMessage());
            Assert.AreEqual("InfoMessage-1", res.GetFirstMessageWithDetails().Info);
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code == "key-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "InfoMessage-Confirm-1" &&
                x.MessageType == MessageType.InfoConfirm));
            Assert.IsTrue(res.Messages.Any(x =>
                x.Code == "key-Confirm-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.InfoConfirm));
        }

        [TestMethod]
        public void AddInfo1_Test()
        {
            var res = new Result() { IsSuccess = true };

            res.AddInfo("InfoMessage-1");
            res.AddInfo("key-01", "InfoMessage-1");

            res.AddInfoConfirm("InfoMessage-Confirm-1");
            res.AddInfoConfirm("key-Confirm-01", "InfoMessage-1");

            IResult res1 = res;

            Assert.IsTrue(res1.IsSuccess);

            Assert.AreEqual("InfoMessage-1", res1.GetFirstMessage());
            Assert.AreEqual("InfoMessage-1", res1.GetFirstMessageWithDetails().Info);
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Code == "key-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.Info));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Code.IsNullOrEmpty() && x.Message.Info == "InfoMessage-Confirm-1" &&
                x.MessageType == MessageType.InfoConfirm));
            Assert.IsTrue(res1.Messages.Any(x =>
                x.Code == "key-Confirm-01" && x.Message.Info == "InfoMessage-1" && x.MessageType == MessageType.InfoConfirm));
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_NullDetails_Test()
        {
            var res = new Result() { IsSuccess = false };
            res.WithError("Error-Info-Message-1", "Error-Info-Code-1")
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual(null, fistMessageWithDetails.Details.FirstOrDefault());
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_Test()
        {
            var res = new Result() { IsSuccess = false };
            res.WithError(new MessageDataModel("Error-Info-Message-1", "Error-Info-Code-1"))
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual("Error-Info-Code-1", fistMessageWithDetails.Details.FirstOrDefault());
            Assert.AreEqual("Message: Error-Info-Message-1; Details: Error-Info-Code-1", fistMessageWithDetails.ToString());
        }

        [TestMethod]
        public void AddInfoAndGetFirstMessageWithDetails_InFailure_Test()
        {
            var res = MockFailure();

            Assert.IsFalse(res.IsSuccess);
            Assert.AreEqual("Error-Info-Message-1", res.GetFirstMessage());

            var fistMessageWithDetails = res.GetFirstMessageWithDetails();
            Assert.IsTrue(!fistMessageWithDetails.IsNull());
            Assert.AreEqual("Error-Info-Message-1", fistMessageWithDetails.Info);
            Assert.AreEqual("Error-Info-Code-1", fistMessageWithDetails.Details.FirstOrDefault());
            Assert.AreEqual("Message: Error-Info-Message-1; Details: Error-Info-Code-1", fistMessageWithDetails.ToString());
        }

        private static IResult MockFailure()
        {
            return Result.Failure(new MessageDataModel("Error-Info-Message-1", "Error-Info-Code-1"))
                .WithMessage(new MessageDataModel("Message info", "Message detail"), MessageType.Error);
        }
    }
}
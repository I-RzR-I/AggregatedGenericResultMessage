// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2023-11-28 21:52
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-11-28 21:52
// ***********************************************************************
//  <copyright file="ToSoapTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.ResultMessage.Models;

namespace InfoResultTests
{
    [TestClass]
    public class ToSoapTests
    {
        [TestMethod]
        public void ToFailSoapTest()
        {
            var soap = Result.Failure("Error message").ToSoapResult();

            Assert.IsFalse(soap.IsSuccess);
            Assert.IsFalse(soap.Messages.First().LogTraceId.IsNullOrEmpty());
            Assert.AreEqual("Error message", soap.Messages.First().Message.Info);
        }

        [TestMethod]
        public void ToFailSoapXmlTest()
        {
            var result = Result.Failure("ToFailSoapXmlTest-Code", "Error message-ToFailSoapXmlTest");
            result.AddError("error-key1", new MessageDataModel("error-message-info-1", new[]
            {
                "error-message-detail-1",
                "error-message-detail-2"
            }));
            var soap = result.ToSoapResult();

            Assert.IsFalse(soap.IsSuccess);
            Assert.IsFalse(soap.Messages.First().LogTraceId.IsNullOrEmpty());
            Assert.AreEqual("Error message-ToFailSoapXmlTest", soap.Messages.First().Message.Info);

            var serializer = new XmlSerializer(typeof(SoapResult));
            using var writer = new StreamWriter("ToFailSoapXmlTest.xml");
            serializer.Serialize(writer, soap);
        }

        [TestMethod]
        public void SoapResult_XmlRoundTrip_PreservesCoreState()
        {
            var original = Result.Failure("RT-Code", "Round-trip error message");
            original.AddError("rt-key", new MessageDataModel("rt-info", new[] { "rt-detail-1", "rt-detail-2" }));
            var soap = original.ToSoapResult();

            var serializer = new XmlSerializer(typeof(SoapResult));
            using var ms = new MemoryStream();
            serializer.Serialize(ms, soap);
            ms.Position = 0;
            var roundTripped = (SoapResult)serializer.Deserialize(ms);

            Assert.IsNotNull(roundTripped);
            Assert.AreEqual(soap.IsSuccess, roundTripped.IsSuccess);
            Assert.AreEqual(soap.IsFailure, roundTripped.IsFailure);
            Assert.AreEqual(soap.Messages.Count, roundTripped.Messages.Count);

            var first = roundTripped.Messages.First();
            Assert.AreEqual("RT-Code", first.Key);
            Assert.AreEqual("Round-trip error message", first.Message.Info);

            var errorMsg = roundTripped.Messages.First(m => m.Key == "rt-key");
            Assert.AreEqual("rt-info", errorMsg.Message.Info);
            CollectionAssert.AreEqual(new[] { "rt-detail-1", "rt-detail-2" }, errorMsg.Message.Details.ToArray());
        }

        [TestMethod]
        public void ToSoapResult_OnSuccessWithoutResponse_ProducesEmptyResponse()
        {
            var soap = Result.Success().ToSoapResult();

            Assert.IsTrue(soap.IsSuccess);
            Assert.IsFalse(soap.IsFailure);
            Assert.IsNull(soap.Response, "A null Response on the source must round-trip as null on the SoapResult.");
        }
    }
}
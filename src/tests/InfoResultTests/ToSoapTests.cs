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
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;
using DomainCommonExtensions.DataTypeExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
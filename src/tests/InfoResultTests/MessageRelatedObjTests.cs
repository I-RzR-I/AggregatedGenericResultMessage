﻿// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2024-01-19 00:39
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-01-19 00:39
// ***********************************************************************
//  <copyright file="MessageRelatedObjTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using AggregatedGenericResultMessage.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AggregatedGenericResultMessage.Enums;
using DomainCommonExtensions.CommonExtensions;
// ReSharper disable PossibleMultipleEnumeration

namespace InfoResultTests
{
    [TestClass]
    public class MessageRelatedObjTests
    {
        [TestMethod]
        public void ResultWith1RelatedObjTest()
        {
            var result = Result
                .Failure("Failure_Code_1", new MessageDataModel("Failure_Info_1", "Failure_D_1", "Failure_D_2"))
                .AddError("Error_1", new MessageDataModel("Error_Info_1", "Error_D_1"), 
                    new RelatedObjectModel("R_Obj_InCode_1", new[] { "R_Obj_InDs_1", "R_Obj_InDs_2" }));

            Assert.IsFalse(result.IsSuccess);
            Assert.IsNull(result.Response);

            var failureCode1Result = result.Messages.Where(x => x.Key == "Failure_Code_1" && x.MessageType == MessageType.Error);
            var failureGetFirstMessageWithDetailsResult = result.GetFirstMessageWithDetails();

            Assert.IsTrue(failureCode1Result.Count() == 1);
            Assert.IsTrue(failureCode1Result.Select(x => x.Message.Details).Count() == 1);
            Assert.IsTrue(failureCode1Result.First().Message.Details.Count() == 2);

            Assert.IsTrue(!failureGetFirstMessageWithDetailsResult.IsNull());
            Assert.AreEqual("Failure_Info_1", failureGetFirstMessageWithDetailsResult.Info);
            Assert.IsTrue(failureGetFirstMessageWithDetailsResult.Details.Count() == 2);
            Assert.AreEqual("Message: Failure_Info_1; Details: Failure_D_1#Failure_D_2", failureGetFirstMessageWithDetailsResult.ToString());

            Assert.IsTrue(result.Messages.Count(x => x.Key == "Error_1"
                                                     && x.Message.Info == "Error_Info_1"
                                                     && x.MessageType == MessageType.Error) == 1);

            Assert.IsTrue(result.Messages.Count(x => x.Key == "Error_1"
                                                     && x.Message.Info == "Error_Info_1"
                                                     && x.Message.Details.Count(z => z == "Error_D_1") == 1
                                                     && x.MessageType == MessageType.Error
                                                     && x.RelatedObjects.Count == 1) == 1);

            Assert.IsTrue(result.Messages.Count(x => x.Key == "Error_1"
                                                     && x.Message.Info == "Error_Info_1"
                                                     && x.Message.Details.Count(z => z == "Error_D_1") == 1
                                                     && x.MessageType == MessageType.Error
                                                     && x.RelatedObjects.Count(z => z.InCodeName == "R_Obj_InCode_1") == 1) == 1);
        }
    }
}
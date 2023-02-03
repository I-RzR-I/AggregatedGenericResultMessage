// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 15:20
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="MessageTests.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Enums;
using AggregatedGenericResultMessage.Extensions.Result.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

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
        public void GeneralMessageResultTest()
        {
            Assert.IsFalse(_res.IsSuccess);
            Assert.IsNull(_res.Response);

            Assert.IsTrue(_res.GetFirstError().Equals("simple error message"));
            Assert.IsTrue(_res.GetFirstMessage().Equals("simple info message"));
        }
    }
}
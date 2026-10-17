using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RzR.ResultMessage;
using RzR.ResultMessage.Models;
using NewtonsoftConvert = Newtonsoft.Json.JsonConvert;

namespace InfoResultTests
{
    [TestClass]
    public class WireFormatTests
    {
        private const string FixedTraceId = "TRACE";
        private const string AliasName = "Key";
        private const string CodeName = "Code";

        private static MessageModel NewMessage(string code)
        {
            var model = new MessageModel { Code = code };
            model.LogTraceId = FixedTraceId;

            return model;
        }

        private static bool IsAliasName(string name)
            => string.Equals(name, AliasName, StringComparison.OrdinalIgnoreCase);

        private static string[] JsonNames(JsonElement obj)
            => obj.EnumerateObject().Select(p => p.Name).ToArray();

        private static string[] JObjectNames(JObject obj)
            => obj.Properties().Select(p => p.Name).ToArray();

        private static void AssertNoAliasName(IEnumerable<string> names, string context)
        {
            var offending = names.Where(IsAliasName).ToArray();
            Assert.AreEqual(0, offending.Length,
                $"{context}: the obsolete alias must never be serialized, found [{string.Join(", ", offending)}].");
        }

        private static void AssertNoAliasElement(XContainer container, string context)
            => AssertNoAliasName(container.Descendants().Select(e => e.Name.LocalName), context);

        private static XElement SingleByLocalName(XContainer container, string localName)
            => container.Descendants().Single(e => e.Name.LocalName == localName);

        private static XDocument XmlSerialize<T>(T value)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            serializer.Serialize(ms, value);
            ms.Position = 0;

            return XDocument.Load(ms);
        }

        private static T XmlDeserialize<T>(XDocument doc)
        {
            var serializer = new XmlSerializer(typeof(T));
            using var ms = new MemoryStream();
            doc.Save(ms);
            ms.Position = 0;

            return (T)serializer.Deserialize(ms);
        }

        private static XDocument DcsWrite<T>(T value)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using var ms = new MemoryStream();
            serializer.WriteObject(ms, value);
            ms.Position = 0;

            return XDocument.Load(ms);
        }

        private static T DcsRead<T>(XDocument doc)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using var ms = new MemoryStream();
            doc.Save(ms);
            ms.Position = 0;

            return (T)serializer.ReadObject(ms);
        }

        private static void InjectAliasSibling(XDocument doc, bool before, string aliasValue)
        {
            var code = SingleByLocalName(doc, CodeName);
            var alias = new XElement(code.Name.Namespace + AliasName, aliasValue);

            if (before)
                code.AddBeforeSelf(alias);
            else
                code.AddAfterSelf(alias);
        }

        private static void ReplaceCodeWithAlias(XDocument doc, string aliasValue)
        {
            var code = SingleByLocalName(doc, CodeName);
            code.ReplaceWith(new XElement(code.Name.Namespace + AliasName, aliasValue));
        }

        private static SoapResult NewSoapResult(params string[] codes)
        {
            var result = new Result<string> { IsSuccess = false };
            foreach (var code in codes)
                result.Messages.Add(NewMessage(code));

            return result.ToSoapResult();
        }

        [TestMethod]
        public void Stj_SerializeMessageModel_WritesLowercaseCode_AndNoAlias_Test()
        {
            var model = NewMessage("X1");

            var json = JsonSerializer.Serialize(model);

            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            Assert.IsTrue(root.TryGetProperty("code", out var code), "Property \"code\" must be present.");
            Assert.AreEqual("X1", code.GetString());
            AssertNoAliasName(JsonNames(root), "STJ MessageModel");
        }

        [TestMethod]
        public void Stj_SerializeResultWithMessages_InterfaceTypedMessage_WritesCode_AndNoAlias_Test()
        {
            var result = new Result<string> { IsSuccess = false };
            result.Messages.Add(NewMessage("X2"));

            var json = JsonSerializer.Serialize(result);

            using var doc = JsonDocument.Parse(json);
            var messages = doc.RootElement.GetProperty("messages");
            Assert.AreEqual(1, messages.GetArrayLength());
            var message = messages[0];
            Assert.IsTrue(message.TryGetProperty("code", out var code),
                "Property \"code\" must be present on the IMessageModel (interface) contract.");
            Assert.AreEqual("X2", code.GetString());
            AssertNoAliasName(JsonNames(message), "STJ Result.Messages[0]");
        }

        [TestMethod]
        public void Stj_SerializeNullCode_WritesCodeAsJsonNull_Test()
        {
            var model = NewMessage(null);

            var json = JsonSerializer.Serialize(model);

            using var doc = JsonDocument.Parse(json);
            Assert.IsTrue(doc.RootElement.TryGetProperty("code", out var code), "Property \"code\" must be present.");
            Assert.AreEqual(JsonValueKind.Null, code.ValueKind);
            AssertNoAliasName(JsonNames(doc.RootElement), "STJ null Code");
        }

        [TestMethod]
        public void Stj_SerializeEmptyCode_WritesCodeAsEmptyString_Test()
        {
            var model = NewMessage(string.Empty);

            var json = JsonSerializer.Serialize(model);

            using var doc = JsonDocument.Parse(json);
            Assert.IsTrue(doc.RootElement.TryGetProperty("code", out var code), "Property \"code\" must be present.");
            Assert.AreEqual(JsonValueKind.String, code.ValueKind);
            Assert.AreEqual(string.Empty, code.GetString());
        }

        [DataTestMethod]
        [DataRow(@"{""code"":""C"",""key"":""K""}", DisplayName = "code then key")]
        [DataRow(@"{""key"":""K"",""code"":""C""}", DisplayName = "key then code")]
        [DataRow(@"{""code"":""C"",""Key"":""K""}", DisplayName = "code then Key (CLR alias name)")]
        [DataRow(@"{""Key"":""K"",""code"":""C""}", DisplayName = "Key (CLR alias name) then code")]
        public void Stj_Deserialize_CodeAndAlias_BothOrders_CodeWins_Test(string json)
        {
            var model = JsonSerializer.Deserialize<MessageModel>(json);

            Assert.IsNotNull(model);
            Assert.AreEqual("C", model.Code);
        }

        [DataTestMethod]
        [DataRow(@"{""key"":""X""}", DisplayName = "key")]
        [DataRow(@"{""Key"":""X""}", DisplayName = "Key (CLR alias name)")]
        public void Stj_Deserialize_LegacyAliasOnly_CodeIsNull_Test(string json)
        {
            var model = JsonSerializer.Deserialize<MessageModel>(json);

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [TestMethod]
        public void Stj_DefaultOptions_PascalCaseCode_IsCaseSensitive_CodeIsNull_Test()
        {
            var model = JsonSerializer.Deserialize<MessageModel>(@"{""Code"":""X""}");

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [DataTestMethod]
        [DataRow(@"{""Key"":""X""}", DisplayName = "Key")]
        [DataRow(@"{""KEY"":""X""}", DisplayName = "KEY")]
        public void Stj_CaseInsensitive_AliasOnly_CodeIsNull_Test(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var model = JsonSerializer.Deserialize<MessageModel>(json, options);

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [DataTestMethod]
        [DataRow(@"{""Code"":""C"",""Key"":""K""}", DisplayName = "Code then Key")]
        [DataRow(@"{""Key"":""K"",""Code"":""C""}", DisplayName = "Key then Code")]
        public void Stj_CaseInsensitive_CodeAndAlias_BothOrders_CodeWins_Test(string json)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var model = JsonSerializer.Deserialize<MessageModel>(json, options);

            Assert.IsNotNull(model);
            Assert.AreEqual("C", model.Code);
        }

        [TestMethod]
        public void Stj_CaseInsensitive_UppercaseCode_IsBound_Test()
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var model = JsonSerializer.Deserialize<MessageModel>(@"{""CODE"":""X""}", options);

            Assert.IsNotNull(model);
            Assert.AreEqual("X", model.Code);
        }

        [DataTestMethod]
        [DataRow(@"{""Code"":""C"",""Key"":""K""}", DisplayName = "Code then Key")]
        [DataRow(@"{""Key"":""K"",""Code"":""C""}", DisplayName = "Key then Code")]
        public void Newtonsoft_Deserialize_CodeAndAlias_BothOrders_CodeWins_Test(string json)
        {
            var model = NewtonsoftConvert.DeserializeObject<MessageModel>(json);

            Assert.IsNotNull(model);
            Assert.AreEqual("C", model.Code);
        }

        [DataTestMethod]
        [DataRow(@"{""Key"":""X""}", DisplayName = "Key")]
        [DataRow(@"{""key"":""X""}", DisplayName = "key")]
        public void Newtonsoft_Deserialize_LegacyAliasOnly_CodeIsNull_Test(string json)
        {
            var model = NewtonsoftConvert.DeserializeObject<MessageModel>(json);

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [TestMethod]
        public void Newtonsoft_Deserialize_LowercaseCode_IsBoundCaseInsensitively_Test()
        {
            var model = NewtonsoftConvert.DeserializeObject<MessageModel>(@"{""code"":""X""}");

            Assert.IsNotNull(model);
            Assert.AreEqual("X", model.Code);
        }

        [TestMethod]
        public void Newtonsoft_SerializeNullCode_WritesCodeAsNull_Test()
        {
            var model = NewMessage(null);

            var obj = JObject.Parse(NewtonsoftConvert.SerializeObject(model));

            var code = obj.Property(CodeName);
            Assert.IsNotNull(code, "Property \"Code\" must be present.");
            Assert.AreEqual(JTokenType.Null, code.Value.Type);
            AssertNoAliasName(JObjectNames(obj), "Newtonsoft null Code");
        }

        [DataTestMethod]
        [DataRow(true, DisplayName = "alias before Code")]
        [DataRow(false, DisplayName = "alias after Code")]
        public void XmlSerializer_Deserialize_CodeWithAliasSibling_CodeWins_Test(bool aliasBeforeCode)
        {
            var doc = XmlSerialize(NewMessage("C"));
            InjectAliasSibling(doc, aliasBeforeCode, "K");

            var model = XmlDeserialize<MessageModel>(doc);

            Assert.IsNotNull(model);
            Assert.AreEqual("C", model.Code);
        }

        [TestMethod]
        public void XmlSerializer_Deserialize_LegacyAliasOnly_CodeIsNull_Test()
        {
            var doc = XmlSerialize(NewMessage("C"));
            ReplaceCodeWithAlias(doc, "X");

            var model = XmlDeserialize<MessageModel>(doc);

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [TestMethod]
        public void XmlSerializer_SerializeNullCode_OmitsCodeElement_Test()
        {
            var doc = XmlSerialize(NewMessage(null));

            Assert.AreEqual(0, doc.Descendants().Count(e => e.Name.LocalName == CodeName),
                "A null Code must not produce a Code element.");
            AssertNoAliasElement(doc, "XmlSerializer null Code");
        }

        [TestMethod]
        public void XmlSerializer_SerializeEmptyCode_WritesEmptyCodeElement_Test()
        {
            var doc = XmlSerialize(NewMessage(string.Empty));

            var code = SingleByLocalName(doc, CodeName);
            Assert.AreEqual(string.Empty, code.Value);
        }

        [DataTestMethod]
        [DataRow(true, DisplayName = "alias before Code")]
        [DataRow(false, DisplayName = "alias after Code")]
        public void Dcs_SoapResult_Deserialize_CodeWithAliasSibling_CodeWins_Test(bool aliasBeforeCode)
        {
            var doc = DcsWrite(NewSoapResult("C"));
            InjectAliasSibling(doc, aliasBeforeCode, "K");

            var soap = DcsRead<SoapResult>(doc);

            Assert.IsNotNull(soap);
            Assert.AreEqual(1, soap.Messages.Count);
            Assert.AreEqual("C", soap.Messages[0].Code);
        }

        [TestMethod]
        public void Dcs_SoapResult_Deserialize_LegacyAliasOnly_CodeIsNull_Test()
        {
            var doc = DcsWrite(NewSoapResult("C"));
            ReplaceCodeWithAlias(doc, "X");

            var soap = DcsRead<SoapResult>(doc);

            Assert.IsNotNull(soap);
            Assert.AreEqual(1, soap.Messages.Count);
            Assert.IsNull(soap.Messages[0].Code);
        }

        [DataTestMethod]
        [DataRow(true, DisplayName = "alias before Code")]
        [DataRow(false, DisplayName = "alias after Code")]
        public void Dcs_MessageModel_Deserialize_CodeWithAliasSibling_CodeWins_Test(bool aliasBeforeCode)
        {
            var doc = DcsWrite(NewMessage("C"));
            InjectAliasSibling(doc, aliasBeforeCode, "K");

            var model = DcsRead<MessageModel>(doc);

            Assert.IsNotNull(model);
            Assert.AreEqual("C", model.Code);
        }

        [TestMethod]
        public void Dcs_MessageModel_Deserialize_LegacyAliasOnly_CodeIsNull_Test()
        {
            var doc = DcsWrite(NewMessage("C"));
            ReplaceCodeWithAlias(doc, "X");

            var model = DcsRead<MessageModel>(doc);

            Assert.IsNotNull(model);
            Assert.IsNull(model.Code);
        }

        [TestMethod]
        public void XmlSerializer_MessageModel_WritesCodeAsFirstChild_NoAlias_RoundTrips_Test()
        {
            var doc = XmlSerialize(NewMessage("X1"));

            Assert.IsNotNull(doc.Root);
            var code = SingleByLocalName(doc, CodeName);
            Assert.AreEqual("X1", code.Value);
            Assert.AreEqual(CodeName, doc.Root.Elements().First().Name.LocalName,
                "Code must be the first child element of the MessageModel root.");
            AssertNoAliasElement(doc, "XmlSerializer MessageModel");

            var back = XmlDeserialize<MessageModel>(doc);
            Assert.AreEqual("X1", back.Code);
        }

        [TestMethod]
        public void XmlSerializer_SoapResult_WritesCodeElements_NoAlias_Test()
        {
            var soap = NewSoapResult("S1", "S2");
            Assert.IsNull(soap.Response);

            var doc = XmlSerialize(soap);

            var codes = doc.Descendants()
                .Where(e => e.Name.LocalName == CodeName)
                .Select(e => e.Value)
                .ToArray();
            CollectionAssert.AreEqual(new[] { "S1", "S2" }, codes);
            AssertNoAliasElement(doc, "XmlSerializer SoapResult");
        }

        [TestMethod]
        public void Dcs_SoapResultFromToSoapResult_EveryMessageHasCode_NoAlias_RoundTrips_Test()
        {
            var result = new Result<string> { IsSuccess = false };
            result.Messages.Add(NewMessage("A1"));
            result.Messages.Add(NewMessage("A2"));
            var soap = result.ToSoapResult();

            var doc = DcsWrite(soap);

            var items = SingleByLocalName(doc, "Messages").Elements().ToArray();
            Assert.AreEqual(2, items.Length);
            var expected = new[] { "A1", "A2" };
            for (var i = 0; i < items.Length; i++)
            {
                var code = items[i].Elements().Single(e => e.Name.LocalName == CodeName);
                Assert.AreEqual(expected[i], code.Value, $"Messages[{i}] Code element value.");
                AssertNoAliasElement(items[i], $"DCS SoapResult Messages[{i}]");
            }
            AssertNoAliasElement(doc, "DCS SoapResult");

            var back = DcsRead<SoapResult>(doc);
            CollectionAssert.AreEqual(expected, back.Messages.Select(m => m.Code).ToArray());
        }

        [TestMethod]
        public void Dcs_ToSoapResult_ResponseMessageModel_ResponseXmlHasCode_NoAlias_Test()
        {
            var result = new Result<MessageModel> { IsSuccess = true, Response = NewMessage("R1") };

            var soap = result.ToSoapResult();

            Assert.IsNotNull(soap.Response, "A MessageModel Response must be converted to an XmlElement.");
            var response = XElement.Parse(soap.Response.OuterXml);
            var code = SingleByLocalName(response, CodeName);
            Assert.AreEqual("R1", code.Value);
            AssertNoAliasElement(response, "SoapResult.Response");
        }

        [TestMethod]
        public void Newtonsoft_MessageModel_WritesCode_NoAlias_RoundTrips_Test()
        {
            var model = NewMessage("N1");

            var json = NewtonsoftConvert.SerializeObject(model);

            var obj = JObject.Parse(json);
            Assert.IsNotNull(obj.Property(CodeName), "Property \"Code\" must be present.");
            Assert.AreEqual("N1", (string)obj[CodeName]);
            AssertNoAliasName(JObjectNames(obj), "Newtonsoft MessageModel");

            var back = NewtonsoftConvert.DeserializeObject<MessageModel>(json);
            Assert.IsNotNull(back);
            Assert.AreEqual("N1", back.Code);
        }

        [TestMethod]
        public void Newtonsoft_ResultErrorModel_SerializeOnly_WritesCode_NoAlias_Test()
        {
            var model = new ResultErrorModel("E9", "m");

            var obj = JObject.Parse(NewtonsoftConvert.SerializeObject(model));

            Assert.IsNotNull(obj.Property(CodeName), "Property \"Code\" must be present.");
            Assert.AreEqual("E9", (string)obj[CodeName]);
            AssertNoAliasName(JObjectNames(obj), "Newtonsoft ResultErrorModel");
        }

        [TestMethod]
        public void Stj_ResultErrorModel_WritesCode_NoAlias_Test()
        {
            var model = new ResultErrorModel("E9", "m");

            var json = JsonSerializer.Serialize(model);

            using var doc = JsonDocument.Parse(json);
            Assert.IsTrue(doc.RootElement.TryGetProperty(CodeName, out var code), "Property \"Code\" must be present.");
            Assert.AreEqual("E9", code.GetString());
            AssertNoAliasName(JsonNames(doc.RootElement), "STJ ResultErrorModel");
        }
    }
}


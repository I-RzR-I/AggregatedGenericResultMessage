using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Abstractions.MessageResults;
using RzR.ResultMessage.Abstractions.Models;
using RzR.ResultMessage.Enums;
using RzR.ResultMessage.Extensions.Result;
using RzR.ResultMessage.Extensions.Result.Messages;
using RzR.ResultMessage.Helpers;
using RzR.ResultMessage.Models;

namespace InfoResultTests
{
    [TestClass]
    public class AliasForwardingTests
    {
        private const string ExpectedObsoleteMessage =
            "Use Code instead. Key will be removed in a future major version.";

        private static RelatedObjectModel NewRelated(string name)
            => new RelatedObjectModel(name, new[] { name + "_ds" });

        [TestMethod]
        public void ResultErrorModel_CtorCodeStringMessage_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-1", "message");

            Assert.AreEqual("REM-1", model.Code);
            Assert.AreEqual("message", model.Message.Info);
            Assert.IsNull(model.Exception);
        }

        [TestMethod]
        public void ResultErrorModel_CtorCodeStringMessageRelatedObject_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-2", "message", NewRelated("r1"));

            Assert.AreEqual("REM-2", model.Code);
            Assert.AreEqual(1, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void ResultErrorModel_CtorCodeStringMessageRelatedObjectsParams_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-3", "message", NewRelated("r1"), NewRelated("r2"));

            Assert.AreEqual("REM-3", model.Code);
            Assert.AreEqual(2, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void ResultErrorModel_CtorCodeMessageDataModel_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-4", new MessageDataModel("info"));

            Assert.AreEqual("REM-4", model.Code);
            Assert.AreEqual("info", model.Message.Info);
        }

        [TestMethod]
        public void ResultErrorModel_CtorCodeMessageDataModelRelatedObject_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-5", new MessageDataModel("info"), NewRelated("r1"));

            Assert.AreEqual("REM-5", model.Code);
            Assert.AreEqual(1, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void ResultErrorModel_CtorCodeMessageDataModelRelatedObjectsParams_SetsCode_Test()
        {
            var model = new ResultErrorModel("REM-6", new MessageDataModel("info"), NewRelated("r1"), NewRelated("r2"));

            Assert.AreEqual("REM-6", model.Code);
            Assert.AreEqual(2, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void ResultErrorModel_CtorException_CodeIsNull_Test()
        {
            var exception = new InvalidOperationException("boom");

            var model = new ResultErrorModel(exception);

            Assert.IsNull(model.Code);
            Assert.AreSame(exception, model.Exception);
        }

        [TestMethod]
        public void ResultErrorModel_CtorExceptionRelatedObject_CodeIsNull_Test()
        {
            var exception = new InvalidOperationException("boom");

            var model = new ResultErrorModel(exception, NewRelated("r1"));

            Assert.IsNull(model.Code);
            Assert.AreSame(exception, model.Exception);
        }

        [TestMethod]
        public void ResultErrorModel_CtorExceptionRelatedObjectsParams_CodeIsNull_Test()
        {
            var exception = new InvalidOperationException("boom");

            var model = new ResultErrorModel(exception, NewRelated("r1"), NewRelated("r2"));

            Assert.IsNull(model.Code);
            Assert.AreSame(exception, model.Exception);
        }

        [TestMethod]
        public void MapToMessage_ViaWithError_CopiesCodeToMessageModel_Test()
        {
            var result = new Result<int>().WithError(new ResultErrorModel("MAP-1", "mapped message"));

            var message = result.Messages.Single();
            Assert.AreEqual("MAP-1", message.Code);
            Assert.AreEqual("mapped message", message.Message.Info);
            Assert.AreEqual(MessageType.Error, message.MessageType);
        }

        [TestMethod]
        public void MapToMessage_ViaWithErrors_CopiesCodeForEveryError_Test()
        {
            var result = new Result<int>().WithErrors(
                new ResultErrorModel("MAP-A", "a"),
                new ResultErrorModel("MAP-B", "b"));

            CollectionAssert.AreEqual(new[] { "MAP-A", "MAP-B" }, result.Messages.Select(m => m.Code).ToArray());
        }

        [TestMethod]
        public void MapToMessage_ViaWithError_ExceptionError_CodeIsNull_Test()
        {
            var result = new Result<int>().WithError(new ResultErrorModel(new InvalidOperationException("boom")));

            var message = result.Messages.Single();
            Assert.IsNull(message.Code);
            Assert.AreEqual(MessageType.Exception, message.MessageType);
        }

        [TestMethod]
        public void ResultErrorModel_Key_ReturnsCode_Test()
        {
            var model = new ResultErrorModel("KEY-RE-1", "message");

#pragma warning disable CS0618
            Assert.AreEqual("KEY-RE-1", model.Key);
            Assert.AreEqual(model.Code, model.Key);
#pragma warning restore CS0618
        }

        [TestMethod]
        public void ResultErrorModel_Key_ExceptionCtor_ReturnsNull_Test()
        {
            var model = new ResultErrorModel(new InvalidOperationException("boom"));

#pragma warning disable CS0618
            Assert.IsNull(model.Key);
#pragma warning restore CS0618
        }

        [TestMethod]
        public void IResultErrorModel_Key_ThroughInterface_ReturnsCode_Test()
        {
            IResultErrorModel model = new ResultErrorModel("KEY-RE-2", "message");

#pragma warning disable CS0618
            Assert.AreEqual("KEY-RE-2", model.Key);
#pragma warning restore CS0618
        }

        [TestMethod]
        public void ResultErrorModel_Key_IsReadOnly_Test()
        {
            var property = typeof(ResultErrorModel).GetProperty("Key");

            Assert.IsNotNull(property);
            Assert.IsTrue(property.CanRead);
            Assert.IsFalse(property.CanWrite);
        }

        [TestMethod]
        public void MessageModel_CtorCodeStringMessageType_SetsCode_Test()
        {
            var model = new MessageModel("MM-1", "message", MessageType.Warning);

            Assert.AreEqual("MM-1", model.Code);
            Assert.AreEqual(MessageType.Warning, model.MessageType);
            Assert.AreEqual("message", model.Message.Info);
        }

        [TestMethod]
        public void MessageModel_CtorCodeStringMessageTypeRelatedObjects_SetsCode_Test()
        {
            var model = new MessageModel("MM-2", "message", MessageType.Info, NewRelated("r1"));

            Assert.AreEqual("MM-2", model.Code);
            Assert.AreEqual(MessageType.Info, model.MessageType);
            Assert.AreEqual(1, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void MessageModel_CtorCodeMessageDataModelType_SetsCode_Test()
        {
            var model = new MessageModel("MM-3", new MessageDataModel("info"), MessageType.Warning);

            Assert.AreEqual("MM-3", model.Code);
            Assert.AreEqual(MessageType.Warning, model.MessageType);
            Assert.AreEqual("info", model.Message.Info);
        }

        [TestMethod]
        public void MessageModel_CtorCodeMessageDataModelRelatedObjects_SetsCode_Test()
        {
            var model = new MessageModel("MM-4", new MessageDataModel("info"), NewRelated("r1"));

            Assert.AreEqual("MM-4", model.Code);
            Assert.AreEqual("info", model.Message.Info);
            Assert.AreEqual(1, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void MessageModel_CtorCodeMessageDataModelTypeRelatedObjects_SetsCode_Test()
        {
            var model = new MessageModel("MM-5", new MessageDataModel("info"), MessageType.Info, NewRelated("r1"), NewRelated("r2"));

            Assert.AreEqual("MM-5", model.Code);
            Assert.AreEqual(MessageType.Info, model.MessageType);
            Assert.AreEqual(2, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void MessageModel_CtorCodeException_SetsCode_Test()
        {
            var model = new MessageModel("MM-6", new InvalidOperationException("boom"));

            Assert.AreEqual("MM-6", model.Code);
            Assert.AreEqual(MessageType.Exception, model.MessageType);
        }

        [TestMethod]
        public void MessageModel_CtorCodeExceptionRelatedObjects_SetsCode_Test()
        {
            var model = new MessageModel("MM-7", new InvalidOperationException("boom"), NewRelated("r1"));

            Assert.AreEqual("MM-7", model.Code);
            Assert.AreEqual(MessageType.Exception, model.MessageType);
            Assert.AreEqual(1, model.RelatedObjects.Count);
        }

        [TestMethod]
        public void IMessageModel_Code_HasJsonPropertyNameCode_Test()
        {
            var property = typeof(IMessageModel).GetProperty(nameof(IMessageModel.Code));

            var attribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            Assert.IsNotNull(attribute);
            Assert.AreEqual("code", attribute.Name);
        }

        [TestMethod]
        public void IMessageModel_Code_IsReadWrite_Test()
        {
            IMessageModel model = new MessageModel();

            model.Code = "IMM-1";

            Assert.AreEqual("IMM-1", model.Code);
        }

        [TestMethod]
        public void MessageModel_SetKey_WritesCode_Test()
        {
            var model = new MessageModel();

#pragma warning disable CS0618
            model.Key = "K-1";
#pragma warning restore CS0618

            Assert.AreEqual("K-1", model.Code);
        }

        [TestMethod]
        public void MessageModel_SetCode_ReadsThroughKey_Test()
        {
            var model = new MessageModel { Code = "C-1" };

#pragma warning disable CS0618
            Assert.AreEqual("C-1", model.Key);
#pragma warning restore CS0618
        }

        [TestMethod]
        public void MessageModel_SetKeyNull_ClearsCode_Test()
        {
            var model = new MessageModel { Code = "C-2" };

#pragma warning disable CS0618
            model.Key = null;
            Assert.IsNull(model.Key);
#pragma warning restore CS0618

            Assert.IsNull(model.Code);
        }

        [TestMethod]
        public void MessageModel_SetKeyEmpty_SetsCodeEmpty_Test()
        {
            var model = new MessageModel { Code = "C-3" };

#pragma warning disable CS0618
            model.Key = "";
            Assert.AreEqual("", model.Key);
#pragma warning restore CS0618

            Assert.AreEqual("", model.Code);
        }

        [TestMethod]
        public void MessageModel_SetCodeNullAndEmpty_KeyFollows_Test()
        {
            var model = new MessageModel { Code = null };

#pragma warning disable CS0618
            Assert.IsNull(model.Key);

            model.Code = "";
            Assert.AreEqual("", model.Key);
#pragma warning restore CS0618
        }

        [TestMethod]
        public void IMessageModel_Key_ThroughInterface_ForwardsBothWays_Test()
        {
            IMessageModel model = new MessageModel("C-4", "message");

#pragma warning disable CS0618
            Assert.AreEqual("C-4", model.Key);

            model.Key = "K-4";
#pragma warning restore CS0618

            Assert.AreEqual("K-4", model.Code);
        }

        private static IEnumerable<PropertyInfo> AliasProperties()
        {
            yield return typeof(IResultErrorModel).GetProperty("Key");
            yield return typeof(ResultErrorModel).GetProperty("Key");
            yield return typeof(IMessageModel).GetProperty("Key");
            yield return typeof(MessageModel).GetProperty("Key");
        }

        [TestMethod]
        public void KeyAliases_AllFourExist_Test()
        {
            var properties = AliasProperties().ToList();

            Assert.AreEqual(4, properties.Count);
            Assert.IsTrue(properties.All(p => p != null), "Every Key alias property must exist.");
            Assert.IsTrue(properties.All(p => p.PropertyType == typeof(string)));
        }

        [TestMethod]
        public void KeyAliases_AreObsoleteAsWarningWithSharedLiteralMessage_Test()
        {
            var obsoletes = AliasProperties()
                .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<ObsoleteAttribute>() })
                .ToList();

            foreach (var item in obsoletes)
            {
                var name = item.Property.DeclaringType?.Name + ".Key";
                Assert.IsNotNull(item.Attribute, $"{name} must have [Obsolete].");
                Assert.IsFalse(item.Attribute.IsError, $"{name} [Obsolete] must be a warning, not an error.");
                Assert.AreEqual(ExpectedObsoleteMessage, item.Attribute.Message, $"{name} [Obsolete] message mismatch.");
            }

            Assert.AreEqual(1, obsoletes.Select(o => o.Attribute.Message).Distinct(StringComparer.Ordinal).Count(),
                "All four [Obsolete] messages must be identical.");
        }

        [TestMethod]
        public void KeyAliases_AreEditorBrowsableNever_Test()
        {
            foreach (var property in AliasProperties())
            {
                var attribute = property.GetCustomAttribute<EditorBrowsableAttribute>();

                Assert.IsNotNull(attribute, $"{property.DeclaringType?.Name}.Key must have [EditorBrowsable].");
                Assert.AreEqual(EditorBrowsableState.Never, attribute.State);
            }
        }

        [TestMethod]
        public void KeyAliases_HaveSystemTextJsonIgnore_Test()
        {
            foreach (var property in AliasProperties())
            {
                var attribute = property.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

                Assert.IsNotNull(attribute, $"{property.DeclaringType?.Name}.Key must have System.Text.Json [JsonIgnore].");
            }
        }

        [TestMethod]
        public void KeyAliases_OnClasses_HaveXmlIgnoreAndIgnoreDataMember_Test()
        {
            var classProperties = new[]
            {
                typeof(ResultErrorModel).GetProperty("Key"),
                typeof(MessageModel).GetProperty("Key")
            };

            foreach (var property in classProperties)
            {
                var name = property.DeclaringType?.Name + ".Key";
                Assert.IsNotNull(property.GetCustomAttribute<System.Xml.Serialization.XmlIgnoreAttribute>(),
                    $"{name} must have [XmlIgnore].");
                Assert.IsNotNull(property.GetCustomAttribute<System.Runtime.Serialization.IgnoreDataMemberAttribute>(),
                    $"{name} must have [IgnoreDataMember].");
            }
        }

        [TestMethod]
        public void PublicApi_HasNoParameterNamedKey_Test()
        {
            const BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic
                                       | BindingFlags.Instance | BindingFlags.Static;
            var allowlist = new HashSet<string>(StringComparer.Ordinal);

            var offenders = new List<string>();
            var types = typeof(MessageModel).Assembly.GetExportedTypes();

            foreach (var type in types)
            {
                var members = type.GetMethods(flags).Cast<MethodBase>()
                    .Concat(type.GetConstructors(flags))
                    .Where(m => m.IsPublic || m.IsFamily || m.IsFamilyOrAssembly);

                foreach (var member in members)
                {
                    foreach (var parameter in member.GetParameters())
                    {
                        if (!string.Equals(parameter.Name, "key", StringComparison.Ordinal))
                            continue;

                        var id = $"{type.FullName}.{member.Name}({string.Join(", ", member.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name))})";
                        if (!allowlist.Contains(id))
                            offenders.Add(id);
                    }
                }
            }

            Assert.IsTrue(types.Length > 0, "Sanity: the library must export types.");
            Assert.AreEqual(0, offenders.Count, "Parameters named 'key' found: " + string.Join("; ", offenders));
        }

        [TestMethod]
        public void MessageResultInterfaces_AcceptCodeNamedArgument_CompileTimeCheck_Test()
        {
            Func<IAccessDeniedMessageResult<int>, IResult<int>> fAccessDenied = r => r.AddAccessDenied(code: "c", message: "m");
            Func<IErrorMessageResult<int>, IResult<int>> fError = r => r.AddError(code: "c", error: "e");
            Func<IInfoMessageResult<int>, IResult<int>> fInfo = r => r.AddInfo(code: "c", info: "i");
            Func<INotFoundMessageResult<int>, IResult<int>> fNotFound = r => r.AddNotFound(code: "c", message: "m");
            Func<IWarningMessageResult<int>, IResult<int>> fWarning = r => r.AddWarning(code: "c", warning: "w");

            Assert.IsNotNull(fAccessDenied);
            Assert.IsNotNull(fError);
            Assert.IsNotNull(fInfo);
            Assert.IsNotNull(fNotFound);
            Assert.IsNotNull(fWarning);
        }

        [TestMethod]
        public void AccessDeniedMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            AccessDeniedMessageExtensions.AddAccessDenied(result, code: "AD-1", message: "m");

            Assert.AreEqual("AD-1", result.Messages.Single().Code);
        }

        [TestMethod]
        public void ErrorMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            ErrorMessageExtensions.AddError(result, code: "ER-1", error: "e");

            Assert.AreEqual("ER-1", result.Messages.Single().Code);
        }

        [TestMethod]
        public void GeneralMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            GeneralMessageExtensions.AddMessage(result, code: "GM-1", message: "m", type: MessageType.Info);

            var message = result.Messages.Single();
            Assert.AreEqual("GM-1", message.Code);
            Assert.AreEqual(MessageType.Info, message.MessageType);
        }

        [TestMethod]
        public void InfoMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            InfoMessageExtensions.AddInfo(result, code: "IN-1", info: "i");

            Assert.AreEqual("IN-1", result.Messages.Single().Code);
        }

        [TestMethod]
        public void NotFoundMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            NotFoundMessageExtensions.AddNotFound(result, code: "NF-1", message: "m");

            Assert.AreEqual("NF-1", result.Messages.Single().Code);
        }

        [TestMethod]
        public void WarningMessageExtensions_CodeNamedArgument_SetsCode_Test()
        {
            var result = new Result<int>();

            WarningMessageExtensions.AddWarning(result, code: "WA-1", warning: "w");

            Assert.AreEqual("WA-1", result.Messages.Single().Code);
        }

        [TestMethod]
        public void ResultValidationExtensions_Validate_CodeNamedArgument_SetsCode_Test()
        {
            var result = Result<int>.Success(1);

            var validated = ResultValidationExtensions.Validate(result, x => false, code: "VA-1", error: "e");

            Assert.IsFalse(validated.IsSuccess);
            Assert.AreEqual("VA-1", validated.Messages.Single().Code);
        }

        [TestMethod]
        public void ModelConstructors_CodeNamedArgument_SetsCode_Test()
        {
            var message = new MessageModel(code: "E", message: "m");
            var error = new ResultErrorModel(code: "E", message: "m");

            Assert.AreEqual("E", message.Code);
            Assert.AreEqual("E", error.Code);
        }

        [TestMethod]
        public void ToSoapResult_CopiesCodeForEveryMessage_Test()
        {
            var result = new Result<int>();
            result.AddError(code: "SOAP-1", error: "e");
            result.AddWarning(code: "SOAP-2", warning: "w");
            result.AddInfo(code: "SOAP-3", info: "i");
            result.Messages.Add(new MessageModel(null, "no code"));

            var soap = result.ToSoapResult();

            Assert.AreEqual(result.Messages.Count, soap.Messages.Count);
            CollectionAssert.AreEqual(
                new[] { "SOAP-1", "SOAP-2", "SOAP-3", null },
                soap.Messages.Select(m => m.Code).ToArray());
            CollectionAssert.AreEqual(
                result.Messages.Select(m => m.Code).ToArray(),
                soap.Messages.Select(m => m.Code).ToArray());
        }

        [TestMethod]
        public void ToSoapResult_ViaIResult_CopiesCode_Test()
        {
            IResult result = Result.Failure("SOAP-F", "failure message");

            var soap = result.ToSoapResult();

            Assert.AreEqual("SOAP-F", soap.Messages.Single().Code);
        }

        [TestMethod]
        public void MessageModel_ToString_UsesCodeAndMessageFormat_Test()
        {
            var model = new MessageModel("E1", "msg");

            Assert.AreEqual("E1: Message: msg", model.ToString());
            Assert.AreEqual($"E1: {model.Message}", model.ToString());
        }

        [TestMethod]
        public void ImplicitOperator_CodeColonMessage_SetsCode_Test()
        {
            MessageModel model = new[] { "E1:msg" };

            Assert.AreEqual("E1", model.Code);
            Assert.AreEqual("msg", model.Message.Info);
        }

        [TestMethod]
        public void ImplicitOperator_NoColon_UsesUnhandledExceptionCode_Test()
        {
            MessageModel model = new[] { "no-colon" };

            Assert.AreEqual(ExceptionCodes.UnhandledException, model.Code);
            Assert.AreEqual("no-colon", model.Message.Info);
        }

        [TestMethod]
        public void ImplicitOperator_ThreePartsWithType_SetsCodeMessageAndType_Test()
        {
            MessageModel model = new[] { "E1:a:b", "Warning" };

            Assert.AreEqual("E1", model.Code);
            Assert.AreEqual("a:b", model.Message.Info);
            Assert.AreEqual(MessageType.Warning, model.MessageType);
        }

        private static Result<int> ResultWithCodedErrors()
        {
            var result = new Result<int>();
            result.AddError(code: "ERR-A", error: "a");
            result.AddError(code: "ERR-B", error: "b");
            return result;
        }

        [TestMethod]
        public void HasErrorCode_ExactMatch_ReturnsTrue_Test()
        {
            var result = ResultWithCodedErrors();

            Assert.IsTrue(result.HasErrorCode("ERR-B"));
        }

        [TestMethod]
        public void HasErrorCode_DifferentCase_ReturnsFalse_Test()
        {
            var result = ResultWithCodedErrors();

            Assert.IsFalse(result.HasErrorCode("err-b"));
        }

        [TestMethod]
        public void HasErrorCode_ThroughIResult_ExactMatchTrueDifferentCaseFalse_Test()
        {
            IResult<int> result = ResultWithCodedErrors();

            Assert.IsTrue(result.HasErrorCode("ERR-A"));
            Assert.IsFalse(result.HasErrorCode("Err-A"));
        }
    }
}


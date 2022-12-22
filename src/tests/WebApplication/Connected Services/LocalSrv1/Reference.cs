﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalSrv1
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Book", Namespace="http://tempuri.org/")]
    public partial class Book : object
    {
        
        private string IdField;
        
        private string NameField;
        
        private string AuthorField;
        
        private System.DateTime RegisteredOnField;
        
        private long PrintedUnitsField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, EmitDefaultValue=false)]
        public string Id
        {
            get
            {
                return this.IdField;
            }
            set
            {
                this.IdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Author
        {
            get
            {
                return this.AuthorField;
            }
            set
            {
                this.AuthorField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public System.DateTime RegisteredOn
        {
            get
            {
                return this.RegisteredOnField;
            }
            set
            {
                this.RegisteredOnField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=4)]
        public long PrintedUnits
        {
            get
            {
                return this.PrintedUnitsField;
            }
            set
            {
                this.PrintedUnitsField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LocalSrv1.Srv1Soap")]
    public interface Srv1Soap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        LocalSrv1.HelloWorldResponse HelloWorld(LocalSrv1.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<LocalSrv1.HelloWorldResponse> HelloWorldAsync(LocalSrv1.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBooks", ReplyAction="*")]
        LocalSrv1.GetBooksResponse GetBooks(LocalSrv1.GetBooksRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBooks", ReplyAction="*")]
        System.Threading.Tasks.Task<LocalSrv1.GetBooksResponse> GetBooksAsync(LocalSrv1.GetBooksRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBook", ReplyAction="*")]
        LocalSrv1.GetBookResponse GetBook(LocalSrv1.GetBookRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBook", ReplyAction="*")]
        System.Threading.Tasks.Task<LocalSrv1.GetBookResponse> GetBookAsync(LocalSrv1.GetBookRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBookWithFaultCode", ReplyAction="*")]
        LocalSrv1.GetBookWithFaultCodeResponse GetBookWithFaultCode(LocalSrv1.GetBookWithFaultCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBookWithFaultCode", ReplyAction="*")]
        System.Threading.Tasks.Task<LocalSrv1.GetBookWithFaultCodeResponse> GetBookWithFaultCodeAsync(LocalSrv1.GetBookWithFaultCodeRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.HelloWorldRequestBody Body;
        
        public HelloWorldRequest()
        {
        }
        
        public HelloWorldRequest(LocalSrv1.HelloWorldRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody
    {
        
        public HelloWorldRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.HelloWorldResponseBody Body;
        
        public HelloWorldResponse()
        {
        }
        
        public HelloWorldResponse(LocalSrv1.HelloWorldResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorldResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody()
        {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult)
        {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBooksRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBooks", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBooksRequestBody Body;
        
        public GetBooksRequest()
        {
        }
        
        public GetBooksRequest(LocalSrv1.GetBooksRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetBooksRequestBody
    {
        
        public GetBooksRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBooksResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBooksResponse", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBooksResponseBody Body;
        
        public GetBooksResponse()
        {
        }
        
        public GetBooksResponse(LocalSrv1.GetBooksResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBooksResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public LocalSrv1.Book[] GetBooksResult;
        
        public GetBooksResponseBody()
        {
        }
        
        public GetBooksResponseBody(LocalSrv1.Book[] GetBooksResult)
        {
            this.GetBooksResult = GetBooksResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBookRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBook", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBookRequestBody Body;
        
        public GetBookRequest()
        {
        }
        
        public GetBookRequest(LocalSrv1.GetBookRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBookRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string id;
        
        public GetBookRequestBody()
        {
        }
        
        public GetBookRequestBody(string id)
        {
            this.id = id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBookResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBookResponse", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBookResponseBody Body;
        
        public GetBookResponse()
        {
        }
        
        public GetBookResponse(LocalSrv1.GetBookResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBookResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public LocalSrv1.Book GetBookResult;
        
        public GetBookResponseBody()
        {
        }
        
        public GetBookResponseBody(LocalSrv1.Book GetBookResult)
        {
            this.GetBookResult = GetBookResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBookWithFaultCodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBookWithFaultCode", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBookWithFaultCodeRequestBody Body;
        
        public GetBookWithFaultCodeRequest()
        {
        }
        
        public GetBookWithFaultCodeRequest(LocalSrv1.GetBookWithFaultCodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBookWithFaultCodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string id;
        
        public GetBookWithFaultCodeRequestBody()
        {
        }
        
        public GetBookWithFaultCodeRequestBody(string id)
        {
            this.id = id;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBookWithFaultCodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBookWithFaultCodeResponse", Namespace="http://tempuri.org/", Order=0)]
        public LocalSrv1.GetBookWithFaultCodeResponseBody Body;
        
        public GetBookWithFaultCodeResponse()
        {
        }
        
        public GetBookWithFaultCodeResponse(LocalSrv1.GetBookWithFaultCodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetBookWithFaultCodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public LocalSrv1.Book GetBookWithFaultCodeResult;
        
        public GetBookWithFaultCodeResponseBody()
        {
        }
        
        public GetBookWithFaultCodeResponseBody(LocalSrv1.Book GetBookWithFaultCodeResult)
        {
            this.GetBookWithFaultCodeResult = GetBookWithFaultCodeResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface Srv1SoapChannel : LocalSrv1.Srv1Soap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class Srv1SoapClient : System.ServiceModel.ClientBase<LocalSrv1.Srv1Soap>, LocalSrv1.Srv1Soap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Srv1SoapClient(EndpointConfiguration endpointConfiguration) : 
                base(Srv1SoapClient.GetBindingForEndpoint(endpointConfiguration), Srv1SoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Srv1SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Srv1SoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Srv1SoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Srv1SoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Srv1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public LocalSrv1.HelloWorldResponse HelloWorld(LocalSrv1.HelloWorldRequest request)
        {
            return base.Channel.HelloWorld(request);
        }
        
        public System.Threading.Tasks.Task<LocalSrv1.HelloWorldResponse> HelloWorldAsync(LocalSrv1.HelloWorldRequest request)
        {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public LocalSrv1.GetBooksResponse GetBooks(LocalSrv1.GetBooksRequest request)
        {
            return base.Channel.GetBooks(request);
        }
        
        public System.Threading.Tasks.Task<LocalSrv1.GetBooksResponse> GetBooksAsync(LocalSrv1.GetBooksRequest request)
        {
            return base.Channel.GetBooksAsync(request);
        }
        
        public LocalSrv1.GetBookResponse GetBook(LocalSrv1.GetBookRequest request)
        {
            return base.Channel.GetBook(request);
        }
        
        public System.Threading.Tasks.Task<LocalSrv1.GetBookResponse> GetBookAsync(LocalSrv1.GetBookRequest request)
        {
            return base.Channel.GetBookAsync(request);
        }
        
        public LocalSrv1.GetBookWithFaultCodeResponse GetBookWithFaultCode(LocalSrv1.GetBookWithFaultCodeRequest request)
        {
            return base.Channel.GetBookWithFaultCode(request);
        }
        
        public System.Threading.Tasks.Task<LocalSrv1.GetBookWithFaultCodeResponse> GetBookWithFaultCodeAsync(LocalSrv1.GetBookWithFaultCodeRequest request)
        {
            return base.Channel.GetBookWithFaultCodeAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Srv1Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.Srv1Soap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Srv1Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:2841/Srv1.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.Srv1Soap12))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:2841/Srv1.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            Srv1Soap,
            
            Srv1Soap12,
        }
    }
}

using System.ServiceModel;
using RzR.ResultMessage;

namespace TestWebServSvc
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        SoapResult Add();

        [OperationContract]
        SoapResult AddSoapSuccess();

        [OperationContract]
        SoapResult AddSoapFail();

        [OperationContract]
        SoapResult GetResultList();

        [OperationContract]
        SoapResult GetResultArrayList();

        [OperationContract]
        SoapResult GetResultStringArray();

        [OperationContract]
        SoapResult GetResultObjectArray1();

        [OperationContract]
        SoapResult GetResultObjectArray2();

        [OperationContract]
        SoapResult GetResultObj();

        [OperationContract]
        SoapResult GetResultWithError();
    }
}
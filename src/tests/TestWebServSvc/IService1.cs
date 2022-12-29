using System.ServiceModel;
using AggregatedGenericResultMessage;

namespace TestWebServSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
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
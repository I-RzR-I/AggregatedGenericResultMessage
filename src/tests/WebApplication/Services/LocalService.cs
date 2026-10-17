using System;
using LocalSrv1;
using DomainCommonExtensions.CommonExtensions;
using RzR.ResultMessage;

namespace WebApplication.Services
{
    public class LocalService
    {
        private readonly Srv1SoapClient _service;
#pragma warning disable IDE0090
        public static LocalService Instance => new LocalService();
#pragma warning restore IDE0090

        private LocalService()
        {
            _service = new Srv1SoapClient(Srv1SoapClient.EndpointConfiguration.Srv1Soap);
        }

        public Result<LocalSrv1.Book> GetBook(string id)
        {
            try
            {
                var b = _service.GetBook(new GetBookRequest(new GetBookRequestBody()
                {
                    id = id
                }));

                return !b.Body.GetBookResult.IsNull()
                    ? Result<Book>.Success(b.Body.GetBookResult) 
                    : Result<Book>.NotFound("Book not found");
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public Result<Book> GetBook2(string id)
        {
            try
            {
                var b = _service.GetBookWithFaultCode(new GetBookWithFaultCodeRequest(new GetBookWithFaultCodeRequestBody(id)));

                return Result<Book>.Success(b.Body.GetBookWithFaultCodeResult);
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
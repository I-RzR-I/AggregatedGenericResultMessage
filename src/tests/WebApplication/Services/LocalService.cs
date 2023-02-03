// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.WebApplication
//  Author           : RzR
//  Created On       : 2022-12-09 11:06
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-12-09 11:06
// ***********************************************************************
//  <copyright file="LocalService.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System;
using AggregatedGenericResultMessage;
using LocalSrv1;
using AggregatedGenericResultMessage.Extensions;
using AggregatedGenericResultMessage.Extensions.Common;

namespace WebApplication.Services
{
    public class LocalService
    {
        private readonly Srv1SoapClient _service;
#pragma warning disable IDE0090 // Use 'new(...)'
        public static LocalService Instance => new LocalService();
#pragma warning restore IDE0090 // Use 'new(...)'

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

        public Result<LocalSrv1.Book> GetBook2(string id)
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
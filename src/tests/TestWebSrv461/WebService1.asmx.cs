using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;
using TestWebSrv461.Models;

namespace TestWebSrv461
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public Result Add()
        {
            try
            {
                return (Result)Result.Success();
            }
            catch (Exception e)
            {
                return e;
            }
        }

        [WebMethod]
        public SoapResult AddSoapSuccess()
        {
            return Result.Success().ToSoapResult();
        }

        [WebMethod]
        public SoapResult AddSoapFail()
        {
            return Result.Failure("Error message").ToSoapResult();
        }

        [WebMethod]
        public SoapResult GetResultList()
        {
            try
            {
                var res = new Result<List<string>>
                {
                    IsSuccess = true,
                    Response = new List<string> { "test1", "test2" }
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultArrayList()
        {
            try
            {
                var res = new Result<ArrayList>
                {
                    IsSuccess = true,
                    Response = new ArrayList { 1, "test" }
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultStringArray()
        {
            try
            {
                var res = new Result<string[]>
                {
                    IsSuccess = true,
                    Response = new[] { "test1", "test2" }
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultObjectArray1()
        {
            try
            {
                var res = new Result<List<BookModel>>
                {
                    IsSuccess = true,
                    Response = new List<BookModel>
                    {
                        new BookModel
                        {
                            Name = "Book1",
                            Author = "A1",
                            Id = Guid.NewGuid(),
                            PrintedUnits = 10,
                            RegisteredOn = DateTime.Now.AddYears(-10)
                        },
                        new BookModel
                        {
                            Name = "Book2",
                            Author = "A1",
                            Id = Guid.NewGuid(),
                            PrintedUnits = 100,
                            RegisteredOn = DateTime.Now.AddYears(-5)
                        }
                    }
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultObjectArray2()
        {
            try
            {
                var res = new Result<List<BookModel>>
                {
                    IsSuccess = true,
                    Response = new List<BookModel>
                    {
                        new BookModel
                        {
                            Name = "Book1",
                            Author = "A1",
                            Id = Guid.NewGuid(),
                            PrintedUnits = 10,
                            RegisteredOn = DateTime.Now.AddYears(-10),
                            Comments = new List<string>
                            {
                                "Comment 1",
                                "Comment 2"
                            }
                        },
                        new BookModel
                        {
                            Name = "Book2",
                            Author = "A1",
                            Id = Guid.NewGuid(),
                            PrintedUnits = 100,
                            RegisteredOn = DateTime.Now.AddYears(-5),
                            Comments = new List<string>
                            {
                                "Comment 1.x",
                                "Comment 2.x"
                            },
                            BookReferences = new List<BookReference>
                            {
                                new BookReference
                                {
                                    Author = "RA1",
                                    Name = "RN1",
                                    RegisteredOn = DateTime.Now.AddYears(-8)
                                }
                            }
                        }
                    }
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultObj()
        {
            try
            {
                var res = new Result<string>
                {
                    IsSuccess = true,
                    Response = "data"
                };


                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [WebMethod]
        public SoapResult GetResultWithError()
        {
            var res = new Result<List<string>>
            {
                IsSuccess = false,
                Messages = new List<IMessageModel>
                {
                    new MessageModel("key1", "message1"),
                    new MessageModel("key1", new Exception("ex test"))
                }
            };

            return res.ToSoapResult();
        }
    }
}

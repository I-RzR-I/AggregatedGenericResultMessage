using System;
using System.Collections;
using System.Collections.Generic;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions.Models;
using AggregatedGenericResultMessage.Models;
using TestWebServSvc.Models;

namespace TestWebServSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void DoWork()
        {
        }

        public SoapResult Add()
        {
            try
            {
                return Result.Success().ToSoapResult();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SoapResult AddSoapSuccess()
        {
            return Result.Success().ToSoapResult();
        }

        public SoapResult AddSoapFail()
        {
            return Result.Failure("Error message").ToSoapResult();
        }

        public SoapResult GetResultList()
        {
            try
            {
                var res = new Result<List<string>>
                {
                    IsSuccess = true,
                    Response = new List<string> {"test1", "test2"}
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public SoapResult GetResultArrayList()
        {
            try
            {
                var res = new Result<ArrayList>
                {
                    IsSuccess = true,
                    Response = new ArrayList {1, "test"}
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public SoapResult GetResultStringArray()
        {
            try
            {
                var res = new Result<string[]>
                {
                    IsSuccess = true,
                    Response = new[] {"test1", "test2"}
                };

                return res.ToSoapResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

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
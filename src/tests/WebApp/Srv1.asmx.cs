using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Web.Services;
using Bogus;
using WebApp.Models;

namespace WebApp
{
    /// <summary>
    ///     Summary description for srv1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

#pragma warning disable IDE0060 // Remove unused parameter
    public class Srv1 : WebService
    {
        private readonly IEnumerable<Book> _list;

        public Srv1()
        {
            var faker = new Faker();
            _list = new List<Book>
            {
                new Book
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = DateTime.Now.Date
                },
                new Book
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = faker.Date.Recent()
                },
                new Book
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = faker.Date.Recent()
                }
            };
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Book> GetBooks()
        {
            return _list.Where(x => x.RegisteredOn <= DateTime.Now).ToList();
        }

        [WebMethod]
        public Book GetBook(string id)
        {
            var uid = Guid.Parse(id);

            return _list.FirstOrDefault(x => x.Id == uid);
        }

        [WebMethod]
        public Book GetBookWithFaultCode(string id)
        {
            throw new FaultException("Unknown reason", FaultCode.CreateSenderFaultCode(new FaultCode("code 001")));
        }

    }
}
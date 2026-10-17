using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using InfoResultTests.Dtos;
using RzR.ResultMessage;
using RzR.ResultMessage.Abstractions;
using RzR.ResultMessage.Extensions.Result;

namespace InfoResultTests.Services
{
    public class BookService
    {
#pragma warning disable IDE0090
        public static BookService Instance => new BookService();
#pragma warning restore IDE0090

        public IQueryable<BookDto> GetBooks()
        {
            var faker = new Faker();
            var books = new List<BookDto>
            {
                new BookDto
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = DateTime.Now.Date
                },
                new BookDto
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = faker.Date.Recent()
                },
                new BookDto
                {
                    Author = faker.Name.FullName(),
                    Name = faker.Commerce.ProductName(),
                    Id = faker.Hacker.Random.Guid(),
                    PrintedUnits = faker.Random.Long(),
                    RegisteredOn = faker.Date.Recent()
                }
            };

            return books.AsQueryable();
        }

        public IResult<BookDto> GetBookByRegDate(DateTime date)
        {
            try
            {
                var data = GetBooks().FirstOrDefault(x => x.RegisteredOn == date);

                return data != null
                    ? Result<BookDto>.Success(data)
                    : Result<BookDto>.Warn("No data found");
            }
            catch
            {
                return Result<BookDto>.Failure("Error");
            }
        }

        public Result<BookDto> GetBookItemException()
        {
            try
            {
                throw new Exception("Null data");
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public IResult Execute_IResult_WithEx()
        {
            try
            {
                throw new Exception("Null data");
            }
            catch (Exception e)
            {
                return Result.Failure().WithError(e);
            }
        }

        public Result Execute_Result_WithEx()
        {
            try
            {
                throw new Exception("Null data");
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
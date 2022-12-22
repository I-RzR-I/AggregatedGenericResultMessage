// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-02 19:44
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="BookService.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Collections.Generic;
using System.Linq;
using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions;
using Bogus;
using InfoResultTests.Dtos;
// ReSharper disable MemberCanBePrivate.Global

#endregion

namespace InfoResultTests.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BookService
    {
#pragma warning disable IDE0090 // Use 'new(...)'
        public static BookService Instance => new BookService();
#pragma warning restore IDE0090 // Use 'new(...)'

        // ReSharper disable once MemberCanBeMadeStatic.Global
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
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2022-07-01 03:43
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 12:16
// ***********************************************************************
//  <copyright file="BooksDto.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Collections.Generic;

// ReSharper disable ClassNeverInstantiated.Global

#endregion

namespace InfoResultTests.Dtos
{
    public class BooksDto
    {
        public IEnumerable<BookDto> Books { get; set; }
    }
}
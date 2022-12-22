// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.WebApp
//  Author           : RzR
//  Created On       : 2022-12-09 10:23
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-12-09 10:23
// ***********************************************************************
//  <copyright file="Book.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using System;

namespace WebApp.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime RegisteredOn { get; set; }
        public long PrintedUnits { get; set; }
    }
}
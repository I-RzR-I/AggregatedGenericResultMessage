// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.InfoResultTests
//  Author           : RzR
//  Created On       : 2026-04-22 08:04
// 
//  Last Modified By : RzR
//  Last Modified On : 2026-04-22 08:25
// ***********************************************************************
//  <copyright file="OrderDto.cs" company="RzR SOFT & TECH">
//   Copyright © RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace InfoResultTests.Dtos
{
    internal sealed class OrderDto
    {
        public int ItemCount { get; set; }
        public decimal Total { get; set; }
        public string Customer { get; set; }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-07-10 11:59
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-07-10 13:36
// ***********************************************************************
//  <copyright file="MessageType.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

namespace AggregatedGenericResultMessage.Enums
{
    /// <summary>
    ///     Result message type
    /// </summary>
    /// <remarks></remarks>
    public enum MessageType
    {
        /// <summary>
        ///     Information message
        /// </summary>
        /// <remarks></remarks>
        Info,

        /// <summary>
        ///     Information message that require confirmation
        /// </summary>
        /// <remarks></remarks>
        InfoConfirm,
        
        /// <summary>
        ///     Warning message
        /// </summary>
        /// <remarks></remarks>
        Warning,

        /// <summary>
        ///     Warning message that require confirmation
        /// </summary>
        /// <remarks></remarks>
        WarningConfirm,

        /// <summary>
        ///     Error message
        /// </summary>
        /// <remarks></remarks>
        Error,

        /// <summary>
        ///     Error message that require confirmation
        /// </summary>
        /// <remarks></remarks>
        ErrorConfirm,

        /// <summary>
        ///     Not found data/record
        /// </summary>
        /// <remarks></remarks>
        NotFound,

        /// <summary>
        ///     Access denied for current resource
        /// </summary>
        /// <remarks></remarks>
        AccessDenied,

        /// <summary>
        ///     Exception message
        /// </summary>
        /// <remarks></remarks>
        Exception
    }
}
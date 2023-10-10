// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2023-02-02 14:21
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-02 14:59
// ***********************************************************************
//  <copyright file="TExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using CodeSource;

#pragma warning disable SCS0007

#endregion

namespace AggregatedGenericResultMessage.Extensions.Common
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    ///     T type extensions
    /// </summary>
    /// <remarks></remarks>
    [CodeSource("DomainCommonExtensions.CommonExtensions", "RzR", "RzR", 1)]
    internal static class TExtensions
    {
        /// <summary>
        ///     Serialize source data to XML document
        /// </summary>
        /// <param name="source">Required. Source data</param>
        /// <param name="rootName">Optional. The default value is null. XML root tag name</param>
        /// <param name="rootNameSpaceName">Optional. The default value is null. XML root namespace name</param>
        /// <returns></returns>
        /// <typeparam name="T">Source data type</typeparam>
        /// <remarks></remarks>
        [CodeSource("https://github.com/I-RzR-I/DomainCommonExtensions", "RzR",
            "DomainCommonExtensions.CommonExtensions.TExtensions.SerializeToXmlDoc", 1)]
        internal static XmlDocument SerializeToXmlDoc<T>(this T source, string rootName = null,
            string rootNameSpaceName = null)
        {
            if (source.IsNull()) return null;

            rootName = rootName.IsNullOrEmpty() ? "Root" : rootName;
            rootNameSpaceName = rootNameSpaceName.IsNullOrEmpty() ? "RootNs" : rootNameSpaceName;
            var dataContractSerializer = new DataContractSerializer(source.GetType(), rootName!, rootNameSpaceName!);

            var ms = new MemoryStream();
            dataContractSerializer.WriteObject(ms, source);
            ms.Position = 0;

            var doc = new XmlDocument();
            doc.Load(ms);

            return doc;
        }
    }
}
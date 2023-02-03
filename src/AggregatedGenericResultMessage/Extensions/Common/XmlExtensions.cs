// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-12-29 08:43
// 
//  Last Modified By : RzR
//  Last Modified On : 2023-02-02 15:01
// ***********************************************************************
//  <copyright file="XmlExtensions.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Xml;
using CodeSource;

#endregion

namespace AggregatedGenericResultMessage.Extensions.Common
{
    /// <summary>
    ///     Xml extensions
    /// </summary>
    /// <remarks></remarks>
    internal static class XmlExtensions
    {
        /// <summary>
        ///     Get XmlElement from XmlDocument
        /// </summary>
        /// <param name="document">Source XmlDocument</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static XmlElement GetXmlElement(this XmlDocument document)
        {
            if (!document.IsNull() && !document.DocumentElement.IsNull())
                return document.DocumentElement;
            return null;
        }

        /// <summary>
        ///     Cast source data to XmlElement
        /// </summary>
        /// <param name="source">Source data</param>
        /// <returns></returns>
        /// <typeparam name="T">Source type</typeparam>
        /// <remarks></remarks>
        [CodeSource(
            "https://github.com/I-RzR-I/DomainCommonExtensions/blob/a53b1fc04cd73ff6bdd5ac817f0a5e723aa7db1a/src/DomainCommonExtensions/CommonExtensions/TExtensions.cs#L131",
            "RzR", "RzR", 1.0)]
        internal static XmlElement CastToSoapResponse<T>(this T source)
        {
            var doc = source.SerializeToXmlDoc("SoapResultResponse", "AggregatedGenericResultMessage.SoapResult");

            return doc.GetXmlElement();
        }
    }
}
// ***********************************************************************
//  Assembly         : RzR.Shared.ResultMessage.AggregatedGenericResultMessage
//  Author           : RzR
//  Created On       : 2022-10-09 15:44
// 
//  Last Modified By : RzR
//  Last Modified On : 2022-10-09 16:51
// ***********************************************************************
//  <copyright file="ExceptionHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using AggregatedGenericResultMessage.Extensions.Common;

// ReSharper disable PossibleNullReferenceException

#endregion

namespace AggregatedGenericResultMessage.Helpers
{
    /// <summary>
    ///     Exception helper
    /// </summary>
    /// <remarks></remarks>
    internal static class ExceptionHelper
    {
        /// <summary>
        ///     Preserves the Exception StackTrace, so that it can be caught and re-thrown over layers
        /// </summary>
        /// <param name="exception">Current exception</param>
        /// <remarks></remarks>
        internal static void PreserveStackTrace(Exception exception)
        {
            var streamingContext = new StreamingContext(StreamingContextStates.CrossAppDomain);
            var objectManager = new ObjectManager(null, streamingContext);
            var serializationInfo = new SerializationInfo(exception.GetType(), new FormatterConverter());

            exception.GetObjectData(serializationInfo, streamingContext);
            objectManager.RegisterObject(exception, 1, serializationInfo);
            objectManager.DoFixups();
        }

        /// <summary>
        ///     Create/convert to string exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static string CreateTraceExceptionString(Exception ex)
        {
            if (ex.IsNull())
                return "\tNO EXCEPTION.";

            var sb = new StringBuilder();
            sb.AppendLine("Exception: ");
            sb.AppendLine("Message: ").Append("\t");
            sb.AppendLine(ex.Message);
            sb.AppendLine("StackTrace: ");
            sb.AppendLine(GetStackFootprints(ex));
            sb.AppendLine("Original Trace: ").Append("\t");
            sb.AppendLine(ex.ToString());

            while (!ex.InnerException.IsNull())
            {
                ex = ex.InnerException;
                sb.AppendLine("Inner Exception: ");
                sb.AppendLine(ex?.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Get all info from StackTrance
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string GetStackFootprints(Exception exception)
        {
            var st = new StackTrace(exception, true);
            var frames = st.GetFrames();
            var traceString = new StringBuilder();
            if (frames.IsNull()) return traceString.ToString();

            foreach (var frame in frames)
            {
                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceString.AppendLine($"\tFile: {frame.GetFileName()}");
                traceString.AppendLine($"\tMethod: {frame.GetMethod().Name}");
                traceString.AppendLine($"\tLineNumber: {frame.GetFileLineNumber()}");
                traceString.AppendLine("\t-->");
            }

            return traceString.ToString();
        }
    }
}
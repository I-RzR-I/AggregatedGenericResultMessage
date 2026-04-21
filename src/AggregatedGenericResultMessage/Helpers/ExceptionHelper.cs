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

using RzR.ResultMessage.Extensions.Common;
using RzR.ResultMessage.Models;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text;

// ReSharper disable PossibleNullReferenceException

#endregion

namespace RzR.ResultMessage.Helpers
{
    /// <summary>
    ///     Exception helper
    /// </summary>
    /// <remarks></remarks>
    internal static class ExceptionHelper
    {
        /// <summary>
        ///     Captures the exception's current state (including stack trace) so that callers may
        ///     later re-throw it without losing the original trace via
        ///     <see cref="ExceptionDispatchInfo.Throw()"/>.
        /// </summary>
        /// <param name="exception">Current exception</param>
        /// <returns>
        ///     The captured <see cref="ExceptionDispatchInfo"/>, or <c>null</c> when
        ///     <paramref name="exception"/> is <c>null</c>.
        /// </returns>
        /// <remarks>
        ///     Replaces the legacy <c>SerializationInfo</c>/<c>ObjectManager</c> approach which is
        ///     obsolete (<c>SYSLIB0050</c>) and incompatible with NativeAOT.
        /// </remarks>
        internal static ExceptionDispatchInfo PreserveStackTrace(Exception exception)
        {
            if (exception.IsNull())
                return null;

            return ExceptionDispatchInfo.Capture(exception);
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

            while (ex.InnerException.IsNotNull())
            {
                ex = ex.InnerException;
                sb.AppendLine("Inner Exception: ");
                sb.AppendLine(ex?.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Create/convert to message model exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        /// <remarks></remarks>
        internal static MessageDataModel CreateTraceExceptionMessage(Exception ex)
        {
            if (ex.IsNull())
                return new MessageDataModel("\tNO EXCEPTION.");

            var sb = new StringBuilder();
            sb.AppendLine("Exception: ");
            sb.AppendLine("Message: ").Append("\t");
            sb.AppendLine(ex.Message);
            sb.AppendLine("StackTrace: ");
            sb.AppendLine(GetStackFootprints(ex));
            sb.AppendLine("Original Trace: ").Append("\t");
            sb.AppendLine(ex.ToString());

            while (ex.InnerException.IsNotNull())
            {
                ex = ex.InnerException;
                sb.AppendLine("Inner Exception: ");
                sb.AppendLine(ex?.ToString());
            }

            return new MessageDataModel(ex.Message, sb.ToString());
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
﻿using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using EllipseCommonsClassLibrary.Connections;
using EllipseCommonsClassLibrary.Utilities;
using EllipseCommonsClassLibrary.Utilities.MyDateTime;
using Screen = EllipseCommonsClassLibrary.ScreenService;

namespace EllipseCommonsClassLibrary
{
    /// <summary>
    /// Debugger para gestionar los logs de errores
    /// </summary>
    public static class Debugger
    {
        public static bool DebugErrors = false;
        public static bool DebugWarnings = false;
        public static bool DebugQueries = false;
        public static bool ForceRegionalization = false;

        private static DebugError _lastError;
        private static DebugError _lastWarning;

        public static void DebugScreen(Screen.ScreenSubmitRequestDTO request, Screen.ScreenDTO reply, string filename)
        {
            var requestJson = new JavaScriptSerializer().Serialize(request.screenFields);
            var replyJson = new JavaScriptSerializer().Serialize(reply.screenFields);
            var filePath = Configuration.LocalDataPath + @"debugger\";
            FileWriter.AppendTextToFile(requestJson, "ScreenRequest.txt", filePath);
            FileWriter.AppendTextToFile(replyJson, "ScreenReply.txt", filePath);
        }
        public static void LogError(string customDetails, string errorMessage)
        {
            try
            {
                var errorFilePath = Configuration.LocalDataPath + @"logs\";
                var errorFileName = @"error" + Utilities.MyDateTime.Operations.FormatDateToString(DateTime.Today, Utilities.MyDateTime.Formats.DateYYYYMMDD) + ".txt";

                var lastError = new DebugError
                {
                    CustomDetails = customDetails,
                    ErrorMessage = errorMessage,
                    DateTime = "" + DateTime.Now,
                    UrlLocation = errorFilePath + errorFileName
                };

                _lastError = lastError;

                if (DebugErrors)
                    MessageBox.Show(lastError.CustomDetails+ ": " + lastError.ErrorMessage, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                var stringError = lastError.DateTime + " - " + lastError.CustomDetails + " : " + lastError.ErrorMessage;

                FileWriter.CreateDirectory(errorFilePath);
                FileWriter.AppendTextToFile(stringError, errorFileName, errorFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede crear el Log de Error\n" + customDetails + ": " + ex + "\n" + errorMessage, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        public static void LogWarning(string customDetails, string warningMessage)
        {
            try
            {
                var warningFilePath = Configuration.LocalDataPath + @"logs\";
                var warningFileName = @"warning" + Utilities.MyDateTime.Operations.FormatDateToString(DateTime.Today, Utilities.MyDateTime.Formats.DateYYYYMMDD) + ".txt";

                var lastWarning = new DebugError
                {
                    CustomDetails = customDetails,
                    ErrorMessage = warningMessage,
                    DateTime = "" + DateTime.Now,
                    UrlLocation = warningFilePath + warningFileName
                };

                _lastWarning = lastWarning;

                if (DebugWarnings)
                    MessageBox.Show(lastWarning.CustomDetails + ": " + lastWarning.ErrorMessage, "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                var stringWarning = lastWarning.DateTime + " - " + lastWarning.CustomDetails + " : " + lastWarning.ErrorMessage;

                FileWriter.CreateDirectory(warningFilePath);
                FileWriter.AppendTextToFile(stringWarning, warningFileName, warningFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede crear el Log de Warning\n" + customDetails + ": " + ex + "\n" + warningMessage, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        public static void LogQuery(string query)
        {
            try
            {
                if (!DebugQueries)
                    return;

                var queryFilePath = Configuration.LocalDataPath + @"queries\";
                var queryFileName = @"queries" + Utilities.MyDateTime.Operations.FormatDateToString(DateTime.Today, Utilities.MyDateTime.Formats.DateYYYYMMDD) + ".txt";

                var dateTime = "" + DateTime.Now;

                var stringQuery = dateTime + "  : " + query;

                FileWriter.CreateDirectory(queryFilePath);
                FileWriter.AppendTextToFile(stringQuery, queryFileName, queryFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede crear el Log del query consultado\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DebugError GetLastError()
        {
            return _lastError;
        }
    }

    public class DebugError
    {
        public string CustomDetails;
        public string ErrorMessage;
        public string DateTime;
        public string UrlLocation;
    }
}

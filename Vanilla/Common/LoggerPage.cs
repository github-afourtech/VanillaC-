using System;
using System.Collections.Generic;
using System.IO;
using Vanilla.Development;

namespace Vanilla.Common
{
    public class LoggerPage:Configuration
    {
        #region Variable
        public static string testStep;
        public static string exception;
        public static string testFilePath;
        string filePath = "";
        StreamWriter log;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="testCaseName">Name of testcase</param>
        public LoggerPage(string testCaseName)
        {
            filePath= Configuration.currentDirectory+ "\\Logs\\"+ testCaseName+".log";
            log = new StreamWriter(filePath);
        }

        /// <summary>
        /// Function to write logs to logfile.
        /// </summary>
        /// <param name="testCaseLog">testCaseLogs added from test case</param>
        public void WriteLogs(List<string> testCaseLog)
        {
            try
            {
                foreach (var testStep in testCaseLog)
                {
                    log.WriteLine(testStep);
                    if (ExceptionInfo.ExceptionMessage != null)
                    {
                        // Write log + exceptions
                        log.WriteLine(ExceptionInfo.ExceptionMessage + ExceptionInfo.ExceptionStackTrace + ExceptionInfo.ExceptionType + ExceptionInfo.ExceptionInnerException);
                        // Capture screenshot
                        captureScreenshot();
                    }
                }
                log.Close();
            }
            catch(Exception exception)
            {
                throw new Exception("Can not write a log to file."+exception.Message);
            }
        }
    }

}

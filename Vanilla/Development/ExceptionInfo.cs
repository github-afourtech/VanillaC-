using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vanilla.Development
{
         /// <summary>
        /// ExceptionInfo
        /// </summary>
        public class ExceptionInfo
        {
            #region Private Variables

            /// <summary>
            /// Exception Message
            /// </summary>
            public static string _sExceptionMessage;

            /// <summary>
            /// Exception step type
            /// </summary>
            public static string _sExceptionStepType;

        /// <summary>
        /// Exception type
        /// </summary>
        public static string _sExceptionType;

        /// <summary>
        /// Exception stack trace
        /// </summary>
        public static string _sExceptionStackTrace;

        /// <summary>
        /// Inner exception
        /// </summary>
        public static string _sExceptionInnerException;

            #endregion

            #region Constructor
            public ExceptionInfo()
            {
            }

            public ExceptionInfo(string exceptionMessage, string exceptionType)
            {
                ExceptionMessage = exceptionMessage;
                ExceptionType = exceptionType;
            }
            public ExceptionInfo(Exception e)
            {
            try
            {
                ExceptionMessage = e.Message;
                ExceptionType = e.GetType().ToString();
                ExceptionStackTrace = e.StackTrace.ToString();
                ExceptionInnerException = e.InnerException.ToString();
            }
            catch { }
            }
            public ExceptionInfo(string exceptionMessage, string exceptionType, string exceptionStackTrace)
            {
                ExceptionMessage = exceptionMessage;
                ExceptionType = exceptionType;
                ExceptionStackTrace = exceptionStackTrace;
            }

            public ExceptionInfo(string exceptionType, string exceptionMessage, string exceptionStackTrace, string exceptionInnerException)
            {
                ExceptionMessage = exceptionMessage;
                ExceptionType = exceptionType;
                ExceptionStackTrace = exceptionStackTrace;
                ExceptionInnerException = exceptionInnerException;
            }

            #endregion Constructor

            #region Public Properties

            /// <summary>
            /// Exception Message
            /// </summary>
            public static string ExceptionMessage
            {
                get { return _sExceptionMessage; }
                set { _sExceptionMessage = value; }
            }


            /// <summary>
            /// Exception Step Type
            /// </summary>
            public static string ExceptionStepType
            {
                get { return _sExceptionStepType; }
                set { _sExceptionStepType = value; }
            }

            /// <summary>
            /// Exception Type
            /// </summary>
            public static string ExceptionType
            {
                get { return _sExceptionType; }
                set { _sExceptionType = value; }
            }

            /// <summary>
            /// Exception Stack Trace
            /// </summary>
            public static string ExceptionStackTrace
            {
                get { return _sExceptionStackTrace; }
                set { _sExceptionStackTrace = value; }
            }

            /// <summary>
            /// Exception Inner Exception
            /// </summary>
            public static string ExceptionInnerException
            {
                get { return _sExceptionInnerException; }
                set { _sExceptionInnerException = value; }
            }

            #endregion
        }
    }

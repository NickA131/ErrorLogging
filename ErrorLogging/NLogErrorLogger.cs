using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NinjaSoftwareLtd.ErrorLogging
{
    public class NLogErrorLogger : IErrorLogger
    {
        private string _className;
        private Logger _Logger = LogManager.GetCurrentClassLogger();
        public string ClassName
        {
            get { return _className; }
            set {
                _className = value;
                if(!string.IsNullOrEmpty(value)) _Logger = LogManager.GetLogger(value); }
        }

        public void LogFatal(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Fatal(exception, message); else _Logger.Fatal(message);
        }

        public void LogError(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Error(exception, message); else _Logger.Error(message);
        }

        public void LogWarn(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Warn(exception, message); else _Logger.Warn(message);
        }

        public void LogInfo(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Info(exception, message); else _Logger.Info(message);
        }

        public void LogDebug(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Debug(exception, message); else _Logger.Debug(message);
        }

        public void LogTrace(string message, Exception exception = null)
        {
            if (exception != null) _Logger.Trace(exception, message); else _Logger.Trace(message);
        }
    }
}

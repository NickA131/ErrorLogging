using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaSoftwareLtd.ErrorLogging
{
    public interface IErrorLogger
    {
        string ClassName { get; set; }
        
        void LogFatal(string message, Exception exception = null);
        void LogError(string message, Exception exception = null);
        void LogWarn(string message, Exception exception = null);
        void LogInfo(string message, Exception exception = null);
        void LogDebug(string message, Exception exception = null);
        void LogTrace(string message, Exception exception = null);
    }
}

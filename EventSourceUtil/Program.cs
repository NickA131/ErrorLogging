using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using NinjaSoftwareLtd.ErrorLogging;

namespace EventSourceUtil
{
    // Utility to register an Application with Windows EventLog
    class Program
    {
        private static string _defaultLog = "Application";
        private static List<string> _validArgs = new List<string>() { "l", "s", "h", "v", "d" };
        private static bool _verboseMode = false;
        private static NinjaSoftwareLtd.ErrorLogging.NLogErrorLogger _errorLogger = new NLogErrorLogger();

        public static void Main(string[] args)
        {
            _errorLogger.ClassName = "";

            var parseArgs = new ParseArgs(_validArgs);
            bool argsValid;

            try
            {
                // Check administrator permissions
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    throw new InvalidOperationException("You must be an administrator to run this executable.\n");
                }

                // Parse input arguments
                var parsedArgs = parseArgs.Parse(args, out argsValid);
                _verboseMode = parsedArgs.Keys.Contains("v");

                if (parsedArgs.Keys.Contains("h") || !parsedArgs.Keys.Contains("s") || string.IsNullOrEmpty(parsedArgs["s"]) || !argsValid)
                {
                    Usage();
                    return;
                }

                // Instantiate argument values
                var log = (parsedArgs.Keys.Contains("l") && string.IsNullOrEmpty(parsedArgs["l"]) ? parsedArgs["l"] : _defaultLog);
                var eventSourceBuilder = new EventSourceUpdater();

                if (!parsedArgs.Keys.Contains("d"))
                    eventSourceBuilder.Create(parsedArgs["s"], log);
                else
                    eventSourceBuilder.Delete(parsedArgs["s"], log);

            }
            catch (Exception ex)
            {
                if(_verboseMode)
                    _errorLogger.LogError(ex.Message, ex);
                else
                    Console.WriteLine("Error: {0}\n", ex.Message);
            }

        }

        // Usage message
        private static void Usage() {

            Console.WriteLine("Usage: EventSourceUtil -s <Source> [-l <Log>] [-d] [-h]");
            Console.WriteLine("\t-s <Source>\tName of source application.");
            Console.WriteLine("\t-l <Log>\tName of event log.\n\t\t\tIf not specified defaults to 'Application'.");
            Console.WriteLine("\t-d\t\tDelete specified event source.");
            Console.WriteLine("\t-h\t\tShow this help text.\n");
        }

    }
}

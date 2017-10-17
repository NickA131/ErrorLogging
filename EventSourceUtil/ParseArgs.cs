using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourceUtil
{
    // Generic code to parse input arguments
    public class ParseArgs
    {
        private List<string> _validArgs = new List<string>();

        public ParseArgs(List<string> validArgs)
        {
            _validArgs = validArgs;
        }
        
        public Dictionary<string, string> Parse(string[] args, out bool argsValid)
        {
            var parsedArgs = new Dictionary<string, string>();
            argsValid = true;

            if (args != null)
            {
                for (var i = 0; i < args.Length; i++)
                {
                    if (!string.IsNullOrEmpty(args[i]) && args[i].StartsWith("-") && args[i].Length > 1)
                    {
                        var value = string.Empty;
                        if (i < args.Length -1 && !string.IsNullOrEmpty(args[i + 1]) && !args[i + 1].StartsWith("-"))
                            value = args[i + 1];
                        var key = args[i].Substring(1, args[i].Length - 1);
                        if (_validArgs != null && !_validArgs.Contains(key)) argsValid = false;
                        parsedArgs.Add(key , value);
                        if (value.Length > 0) i++;
                    }
                }
            }

            return parsedArgs;
        }
    }
}

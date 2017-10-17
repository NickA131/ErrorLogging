using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EventSourceUtil
{
    // Perform update of EventLog
    public class EventSourceUpdater
    {
        public void Create(string source, string log)
        {
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, log);
                Console.WriteLine("Created Event Source: {0}\n", source);

            }
            else
                Console.WriteLine("Event Source '{0}' already exists\n", source);
        }

        public void Delete(string source, string log)
        {
            if (EventLog.SourceExists(source))
            {
                var logName = EventLog.LogNameFromSourceName(source, ".");
                if (logName != log)
                {
                    throw new ArgumentException(string.Format("Error: LogName specified for Event Source is invalid {0}\n", log));
                }

                EventLog.DeleteEventSource(source);
                Console.WriteLine("Deleted Event Source: {0}\n", source);

            }
            else
                Console.WriteLine("Event Source '{0}' was not found\n", source);
        }

    }
}

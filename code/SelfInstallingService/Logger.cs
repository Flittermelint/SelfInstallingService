using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MyService
{
    public class MyLogger
    {
        public static string Name = "";
        public static void Log(object message, EventLogEntryType type = EventLogEntryType.Information, [CallerMemberName] string memberName = null)
        {
            EventLog.WriteEntry((Name + ":" + memberName).TrimStart(':'), message.ToString(), type);
        }
    }
}

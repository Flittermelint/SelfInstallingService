using System;
using System.Runtime.InteropServices;

namespace MyService
{
    public static class AdvApiHelper
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern IntPtr RegisterServiceCtrlHandlerEx(string lpServiceName, ServiceControlHandlerEx cbex, IntPtr context);
    }
}

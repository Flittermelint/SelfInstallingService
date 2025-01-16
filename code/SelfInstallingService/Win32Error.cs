using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyService
{
    public enum ERROR
    {
        NO_ERROR            = 0,
        SUCCESS             = 0,
        INVALID_FUNCTION    = 0x0001,   //    1
        INVALID_HANDLE      = 0x0006,   //    6
        INVALID_DATA        = 0x000d,   //   13
        INSUFFICIENT_BUFFER = 0x007A,   //  122
        NO_MORE_ITEMS       = 0x0103,   //  259
        ERROR_NOT_FOUND     = 0x0490,   // 1168
    }

    public class Win32Error
    {
        public int    Error   { get; private set; }
        public ERROR  Code    { get; private set; }
        public string Causer  { get; private set; }
        public UInt32 Step    { get; private set; }
        public string Message { get; private set; }
        public bool   Success { get; private set; }
        public bool   Occured { get; private set; }

        public Win32Error(int error): this(error, 1, "") { }

        public Win32Error(int error, [CallerMemberName] string Causer = null): this(error, 1, Causer) { }

        public Win32Error(int error, UInt32 Step, [CallerMemberName] string Causer = null)
        {
            Win32Exception win32ex = new Win32Exception(error);

            this.Error   = error;

            this.Causer  = Causer;

            this.Step    = Step;

            this.Code    = (ERROR)error;
            this.Message = win32ex.Message;

            this.Success = (error == (int)ERROR.SUCCESS);
            this.Occured = (error != (int)ERROR.SUCCESS);
        }
    }
}

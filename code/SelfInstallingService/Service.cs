using System;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Diagnostics;

using ExtensionMethods;

namespace MyService
{
    public class MyService : ServiceBase
    {
        public const string Name = "SelfInstallingService";

        public const string Group = ""; // set HKLM:\SYSTEM\CurrentControlSet\Services\" + MyService.MyName, "Group", Group => for earlier start, e.g. Group = "base"

        public IntPtr RedirectedServiceHandle = IntPtr.Zero;

        private ServiceControlHandlerEx myCallback;

        public MyService()
        {
            //System.Diagnostics.Debugger.Launch();

            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

                this.CanPauseAndContinue         = false;
                this.CanShutdown                 = true;
                this.CanHandleSessionChangeEvent = true;
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex, EventLogEntryType.Error);
            }
        }

        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();

            try
            {
                base.OnStart(args);

                this.RedirectServiceControlHandler(); // allows the service to process device events !!!

                /*
                 * start worker threads here
                 *
                 * It is strongly recommended to perform all actions asynchronously !!!
                */
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex, EventLogEntryType.Error);
            }
            finally
            {
            }
        }
        
        private int ServiceControlHandler(int control, int eventType, IntPtr eventData, IntPtr context)
        {
            //System.Diagnostics.Debugger.Launch();

            try
            {
                var svcControl = (SERVICE_CONTROL)control;

                MyLogger.Log("SERVICE_CONTROL(" + svcControl + ")");

                if (svcControl == SERVICE_CONTROL.STOP
                 || svcControl == SERVICE_CONTROL.SHUTDOWN)
                {
                    try
                    {
                        /*
                         * stop worker threads here
                        */
                    }
                    catch (Exception ex)
                    {
                        MyLogger.Log(ex, EventLogEntryType.Error);
                    }
                    finally
                    {
                        MyLogger.Log("The End is near...");

                        base.Stop();
                    }
                }
                else if(svcControl == SERVICE_CONTROL.DEVICEEVENT)
                {
                    var DeviceEventType = (DBT)eventType;

                    switch (DeviceEventType)
                    {
                        case DBT.DBT_DEVICEARRIVAL:

                            break;

                        case DBT.DBT_DEVICEREMOVECOMPLETE:

                            break;

                        default:

                            break;
                    }
                }
                else if(svcControl == SERVICE_CONTROL.SESSIONCHANGE)
                {
                }
                else if (svcControl >= SERVICE_CONTROL.CUSTOM_COMMAND_MIN && svcControl <= SERVICE_CONTROL.CUSTOM_COMMAND_MAX)
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MyLogger.Log(ex, EventLogEntryType.Error);
            }

            return 0;
        }

        private void RedirectServiceControlHandler()
        {
            //System.Diagnostics.Debugger.Launch();

            myCallback = new ServiceControlHandlerEx(ServiceControlHandler);

            RedirectedServiceHandle = AdvApiHelper.RegisterServiceCtrlHandlerEx(this.ServiceName, myCallback, IntPtr.Zero);

            if (RedirectedServiceHandle == IntPtr.Zero)
            {
                var Error = new Win32Error(Marshal.GetLastWin32Error());

                MyLogger.Log(Error.Message, EventLogEntryType.Error);
            }
        }
    }
}

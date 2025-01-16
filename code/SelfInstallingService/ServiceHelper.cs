using System;

namespace MyService
{
    public delegate int ServiceControlHandlerEx(int control, int eventType, IntPtr eventData, IntPtr context);

    public enum SERVICE_CONTROL
    {
        START                 = 0x00000000,    // USER DEFINED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        STOP                  = 0x00000001,    // Notifies a service that it should stop. 
                                               // If a service accepts this control code, it must stop upon receipt and return NO_ERROR. After the SCM sends this control code, it will not send other control codes to the service. 
                                               // Windows XP/2000:  If the service returns NO_ERROR and continues to DoRun, it continues to receive control codes. This behavior changed starting with Windows Server 2003 and Windows XP with SP2. 
        PAUSE                 = 0x00000002,    // Notifies a service that it should pause.
        CONTINUE              = 0x00000003,    // Notifies a paused service that it should resume.
        INTERROGATE           = 0x00000004,    // Notifies a service to report its process status information to the service control manager.
                                               // The handler should simply return NO_ERROR; the SCM is aware of the process state of the service.
        SHUTDOWN              = 0x00000005,    // Notifies a service that the system is shutting down so the service can perform cleanup tasks. Note that services that register for SERVICE_CONTROL_PRESHUTDOWN notifications cannot receive this notification because they have already stopped. 
                                               // If a service accepts this control code, it must stop after it performs its cleanup tasks and return NO_ERROR. After the SCM sends this control code, it will not send other control codes to the service.
                                               // For more information, see the Remarks section of this topic.
        PARAMCHANGE           = 0x00000006,    // Notifies a service that service-specific startup parameters have changed. The service should reread its startup parameters.
        NETBINDADD            = 0x00000007,    // Notifies a network service that there is a new component for binding. The service should bind to the new component. 
                                               // Applications should use Plug and Play functionality instead.
        NETBINDREMOVE         = 0x00000008,    // Notifies a network service that a component for binding has been removed. The service should reread its binding information and unbind from the removed component. 
                                               // Applications should use Plug and Play functionality instead.
        NETBINDENABLE         = 0x00000009,    // Notifies a network service that a disabled binding has been enabled. The service should reread its binding information and add the new binding. 
                                               // Applications should use Plug and Play functionality instead.
        NETBINDDISABLE        = 0x0000000A,    // Notifies a network service that one of its bindings has been disabled. The service should reread its binding information and remove the binding. 
                                               // Applications should use Plug and Play functionality instead.
        DEVICEEVENT           = 0x0000000B,    // Notifies a service of device events. (The service must have registered to receive these notifications using the RegisterDeviceNotification function.) The dwEventType and lpEventData parameters contain additional information.
        HARDWAREPROFILECHANGE = 0x0000000C,    // Notifies a service that the computer's hardware profile has changed. The dwEventType parameter contains additional information.
        POWEREVENT            = 0x0000000D,    // Notifies a service of system power events. The dwEventType parameter contains additional information. If dwEventType is PBT_POWERSETTINGCHANGE, the lpEventData parameter also contains additional information.
        SESSIONCHANGE         = 0x0000000E,    // Notifies a service of session change events. Note that a service will only be notified of a user logon if it is fully loaded before the logon attempt is made. The dwEventType and lpEventData parameters contain additional information.
                                               // Windows 2000:  This value is not supported. 
        PRESHUTDOWN           = 0x0000000F,    // Notifies a service that the system will be shutting down. Services that need additional time to perform cleanup tasks beyond the tight time restriction at system shutdown can use this notification. The service control manager sends this notification to applications that have registered for it before sending a SERVICE_CONTROL_SHUTDOWN notification to applications that have registered for that notification. 
                                               // A service that handles this notification blocks system shutdown until the service stops or the preshutdown time-out interval specified through SERVICE_PRESHUTDOWN_INFO expires. Because this affects the user experience, services should use this feature only if it is absolutely necessary to avoid data loss or significant recovery time at the next system start.
                                               // Windows Server 2003 and Windows XP/2000:  This value is not supported. 
        TIMECHANGE            = 0x00000010,    // Notifies a service that the system time has changed. The lpEventData parameter contains additional information. The dwEventType parameter is not used.
                                               // Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This control code is not supported. 
        TRIGGEREVENT          = 0x00000020,    // Notifies a service registered for a service trigger event that the event has occurred. 
                                               // Windows Server 2008, Windows Vista, Windows Server 2003, and Windows XP/2000:  This control code is not supported. 
        CUSTOM_COMMAND_MIN    = 0x00000080,    // First possible servicedefined customcommand
        CUSTOM_COMMAND_MAX    = 0x000000FF,    // Last  possible servicedefined customcommand
    }
    public enum DBT
    {
        DBT_DEVNODES_CHANGED        = 0x0007,

        DBT_QUERYCHANGECONFIG       = 0x0017,
        DBT_CONFIGCHANGED           = 0x0018,
        DBT_CONFIGCHANGECANCELED    = 0x0019,

        DBT_DEVICEARRIVAL           = 0x8000,
        DBT_DEVICEQUERYREMOVE       = 0x8001,
        DBT_DEVICEQUERYREMOVEFAILED = 0x8002,
        DBT_DEVICEREMOVEPENDING     = 0x8003,
        DBT_DEVICEREMOVECOMPLETE    = 0x8004,
        DBT_DEVICETYPESPECIFIC      = 0x8005,
        DBT_CUSTOMEVENT             = 0x8006,

        DBT_USERDEFINED             = 0xFFFF, 
    }
}

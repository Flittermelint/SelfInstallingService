using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;

using Microsoft.Win32;

using ExtensionMethods;

namespace MyService
{
    public static class MyServiceStarter
    {
        public static void Main(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();

            MyLogger.Name = MyService.Name;

            int ExitCode = 0;

            bool install   = false;
            bool uninstall = false;

            bool start     = false;
            bool stop      = false;

            bool console   = false;

            bool RunService = true;

            try
            {
                foreach(string arg in args)
                {
                    if (arg.EqualsIC("-install"))   { install   = true; }
                    if (arg.EqualsIC("-uninstall")) { uninstall = true; }
                    if (arg.EqualsIC("-start"))     { start     = true; }
                    if (arg.EqualsIC("-stop"))      { stop      = true; }
                }
                //=====================================================================================================================
                if (install)
                { 
                    RunService = false;

                    install = true; 
                }

                if(uninstall)
                {
                    RunService = false;

                    uninstall = true;
                }

                if(start)
                {
                    RunService = false;

                    start = true;
                }

                if(stop)
                {
                    RunService = false;

                    stop = true;
                }
                   
                if(console)
                {
                    RunService = false;

                    console = true;
                }
                //=====================================================================================================================
                if (stop)
                {
                    MyLogger.Log("-Stop");

                    try
                    {
                        var sc = new ServiceController(MyService.Name); sc?.Stop();
                    }
                    catch(Exception ex)
                    {
                        MyLogger.Log(ex, EventLogEntryType.Error);
                    }
                }

                if (uninstall)
                {
                    MyLogger.Log("-Uninstall");

                    Uninstall(args); 
                } 

                if (install) 
                {
                    MyLogger.Log("-Install");

                    Install(args);

                    if(MyService.Group != "")
                    {
                        Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\" + MyService.Name, "Group", MyService.Group);
                    }
                }

                if (start)
                {
                    MyLogger.Log("-Start");

                    var sc = new ServiceController(MyService.Name); sc?.Start();
                }
                    
                if (console) 
                {
                    Console.WriteLine("ServiceStarter.Main(): Console-Mode currently not implemented...");

                    //  StartUp();
                    //
                    //  Console.WriteLine("System running; press any key to stop");
                    //  Console.ReadKey(true);
                    //
                    //  ShutDown();
                    //
                    //  Console.WriteLine("System stopped");
                }

                if (RunService)
                {
                    MyLogger.Log("Service:Starting...");

                    ServiceBase[] ServicesToRun;

                    ServicesToRun = new ServiceBase[] { new MyService() };

                    ServiceBase.Run(ServicesToRun);

                    MyLogger.Log("Service:Stopped...");
                }

                Environment.Exit(ExitCode);
            } 
            catch (Exception ex) 
            {
                MyLogger.Log(ex, EventLogEntryType.Error);

                Environment.Exit(ExitCode == 0 ? 42 : ExitCode);
            }
        }

        private static void Uninstall(string[] args) 
        { 
            Install(false, args); 
        }

        private static void Install(string[] args) 
        { 
            Install(true, args); 
        }

        private static void Install(bool install, string[] args) 
        {
            try 
            {
                using (AssemblyInstaller installer = new AssemblyInstaller(Assembly.GetExecutingAssembly().Location, new string [] {})) 
                { 
                    IDictionary state = new Hashtable(); 

                    installer.UseNewContext = true; 

                    try 
                    { 
                        if (install) 
                        { 
                            installer.Install(state); 
                            installer.Commit(state); 
                        } 
                        else 
                        { 
                            installer.Uninstall(state); 
                        } 
                    } 
                    catch 
                    { 
                        try 
                        { 
                            installer.Rollback(state); 
                        } 
                        catch
                        {
                        } 

                        throw; 
                    } 
                } 
            } 
            catch (Exception ex) 
            {
                MyLogger.Log(ex, EventLogEntryType.Error);
            }
        }
    }
}

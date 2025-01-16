using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

using ExtensionMethods;

namespace MyService
{
    [RunInstaller(true)]
    public class MyServiceInstaller : Installer
    {
        private ServiceInstaller        m_ThisService;
        private ServiceProcessInstaller m_ThisServiceProcess;

        private ServiceAccount acc = ServiceAccount.LocalSystem;

        private string usr = null;
        private string pwd = null;

        public MyServiceInstaller()
        {
            Console.WriteLine("Installer:CommandLine: " + Environment.CommandLine);

            foreach (string argv in Environment.GetCommandLineArgs())
            {
                if (argv.EqualsIC("-acc=user"))           { acc = ServiceAccount.User; }
                if (argv.EqualsIC("-acc=localservice"))   { acc = ServiceAccount.LocalService; }
                if (argv.EqualsIC("-acc=localsystem"))    { acc = ServiceAccount.LocalSystem; }
                if (argv.EqualsIC("-acc=networkservice")) { acc = ServiceAccount.NetworkService; }

                if (argv.StartsWithIC("-usr=")) { usr = (argv.Split("=".ToCharArray()))[1].Trim(); }
                if (argv.StartsWithIC("-pwd=")) { pwd = (argv.Split("=".ToCharArray()))[1].Trim(); }
            }

            Console.WriteLine("Installer: acc=" + acc.ToString());
            Console.WriteLine("Installer: usr=" + usr);
            Console.WriteLine("Installer: pwd=" + pwd);

            m_ThisService = new ServiceInstaller
            {
                ServiceName = MyService.Name,
                StartType   = ServiceStartMode.Automatic
            };

            m_ThisServiceProcess = new ServiceProcessInstaller
            {
                Account  = acc,
                Username = usr,
                Password = pwd
            };

            //m_ThisService.ServicesDependedOn = new string [] { "LanmanWorkStation", "RpcSs", "LanmanServer" };

            Installers.Add(m_ThisService);
            Installers.Add(m_ThisServiceProcess);
        }
    }
}
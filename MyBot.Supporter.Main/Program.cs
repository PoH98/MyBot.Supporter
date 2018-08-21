using System.Windows.Forms;
using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading;
namespace MyBot.Supporter.Main
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread loading = new Thread(Database.Load_);
            loading.Start();
            Database.loadingprocess = 5;
            //Delete all needed DLL, because of the dlls are already packed inside the resources
            if (File.Exists("Newtonsoft.Json.dll"))
            {
                File.Delete("Newtonsoft.Json.dll");
            }
            if (File.Exists("OpenHardwareMonitorLib.dll"))
            {
                File.Delete("OpenHardwareMonitorLib.dll");
            }
            if (File.Exists("Telegram.Bot.dll"))
            {
                File.Delete("Telegram.Bot.dll");
            }
            Database.loadingprocess = 7;
            if (!Directory.Exists(Database.Location))
            {
                Thread fu = new Thread(FirstUsage);
                fu.Start();
                while (fu.IsAlive)
                {
                    Thread.Sleep(10);
                }
                Directory.CreateDirectory(Database.Location);
            }
            //Check for existing of needed DLLs in Roaming folder, if it have single dll missing, delete the others to rewrite the zip file and unzip all again
            if (!File.Exists(Database.Location + "Newtonsoft.Json.dll") || !File.Exists(Database.Location + "OpenHardwareMonitorLib.dll") || !File.Exists(Database.Location + "Telegram.Bot.dll"))
            {
                if (File.Exists(Database.Location + "Newtonsoft.Json.dll"))
                {
                    File.Delete(Database.Location + "Newtonsoft.Json.dll");
                }
                if (File.Exists(Database.Location + "OpenHardwareMonitorLib.dll"))
                {
                    File.Delete(Database.Location + "OpenHardwareMonitorLib.dll");
                }
                if (File.Exists(Database.Location + "Telegram.Bot.dll"))
                {
                    File.Delete(Database.Location + "Telegram.Bot.dll");
                }
                File.WriteAllBytes(Database.Location + "dll.zip", Characters.DLL);
                ZipFile.ExtractToDirectory(Database.Location + "dll.zip", Database.Location);
                File.Delete(Database.Location + "dll.zip");
            }
            //Let program find dll in roaming folder, not current folder
            AppDomain domain = AppDomain.CurrentDomain;
            domain.AssemblyResolve += Domain_AssemblyResolve;
            //Show exception before program crush
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Run the form window
            Database.loadingprocess = 10;
            Application.Run(new MainScreen());
        }

        private static Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly assembly, obj;
            string path = "";
            obj = Assembly.GetExecutingAssembly();
            AssemblyName[] name = obj.GetReferencedAssemblies();
            foreach(var n in name)
            {
                if (n.FullName.Substring(0,n.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    path = Database.Location + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }
            }
            assembly = Assembly.LoadFrom(path);
            return assembly;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Database.WriteLog(e.ExceptionObject.ToString());
            File.WriteAllText("error.log", e.ExceptionObject.ToString());
            MessageBox.Show(e.ExceptionObject.ToString());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Database.WriteLog(e.Exception.ToString());
            File.WriteAllText("error.log", e.Exception.ToString());
            MessageBox.Show(e.Exception.Message.ToString());
        }

        private static void FirstUsage()
        {
            Form f = new FirstUse();
            f.ShowDialog();
        }
    }
}
///<summary>
///This program is creaed by PoH98, who is a student of Diploma in IT
///This program is only for learning purpose, to improve the coding skill of a student
///This program might contains bugs and errors that student is unable to fix, hence the need of professional is needed
///This program is used as multi-bot watchdog of MyBot.run, a botting program for Clash of Clans
///This program had used Telegram Bot API, Open Hardware Monitor and also Mihazupan NuGets
///This program can be edited by anyone, to create a watchdog or multi-runner of any program, but not responsible on any damage maded by this program after modded
///This program have no lisence and no copyright, it is free and no code protection as well
///This program will not do any damage on anyone, any party and any devices. If I done it, please shoot me using your AK-47 on the street
///</summary>
///<!--MyBot.Supporter is brought to you by PoH98-->>

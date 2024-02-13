using MyBot.Supporter.V2.Helper;
using MyBot.Supporter.V2.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using ThreadState = System.Threading.ThreadState;

namespace MyBot.Supporter.V2.Service
{
    public class Worker
    {
        private BotSettings settings
        {
            get
            {
                return supporterSettings.Bots;
            }
        }
        private SupporterSettings supporterSettings;
        private readonly AndroidKiller AndroidKiller;
        private string ProcessName;
        private bool Running;

        public Worker()
        {
            AndroidKiller = new AndroidKiller();
            Execute = new Thread(async () =>
            {
                do
                {
                    await Task.Delay(1250);
                    await Execute_Elapsed();
                    await Task.Delay(1250);
                }
                while(Running);
            });
            Execute.IsBackground = true;
            Logger.Instance.Write("Creating Worker...");
        }

        public bool IsRunning => Execute.ThreadState == ThreadState.Running || Running;

        private Thread Execute { get; set; }

        public async void Run(SupporterSettings settings)
        {
            supporterSettings = settings;
            Running = true;
            try
            {
                while(Execute.ThreadState == ThreadState.Running)
                {
                    await Task.Delay(1000);
                }
                Execute.Start();
            }
            catch
            {

            }
        }

        private async Task Execute_Elapsed()
        {
            var now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            foreach (var set in settings)
            {
                if (!set.IsEnabled && set.Id.HasValue)
                {
                    await NotInTime(set);
                }
                else if (set.IsEnabled)
                {
                    if (set.StartTime == set.EndTime)
                    {
                        //time not setted
                        await InTime(set);
                    }
                    else if (set.StartTime < set.EndTime)
                    {
                        var end = new TimeSpan(set.EndTime.Hours, set.EndTime.Minutes, 59);
                        var start = new TimeSpan(set.StartTime.Hours, set.StartTime.Minutes, 0);
                        if (start <= now && now <= end)
                        {
                            await InTime(set);
                        }
                        else
                        {
                            await NotInTime(set);
                        }
                    }
                    else
                    {
                        var end = new TimeSpan(set.EndTime.Hours, set.EndTime.Minutes, 59);
                        var start = new TimeSpan(set.StartTime.Hours, set.StartTime.Minutes, 0);
                        if (start >= now && now >= end)
                        {
                            await InTime(set);
                        }
                        else
                        {
                            await NotInTime(set);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            Running = false;
            foreach (var set in settings)
            {
                //softkill first
                _ = NotInTime(set);
            }
            //hardkill
            foreach (var process in Process.GetProcesses())
            {
                switch (process.ProcessName)
                {
                    case "MyBot.run":
                    case "adb":
                    case "MEmuHeadless":
                    case "MyBot.run.Watchdog":
                    case "MyBot.run.MiniGui":
                    case "AutoIt3":
                    case "HD-Adb":
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {
                            continue;
                        }
                        break;
                }
            }
        }


        private async Task InTime(BotSetting botSetting)
        {
            if (botSetting.Id.HasValue)
            {
                if (Process.GetProcessById(botSetting.Id.Value) != null)
                {
                    return;
                }
                else
                {
                    //not found, maybe it is killed or closed
                    if (!supporterSettings.Restart)
                    {
                        //no restart needed, return
                        return;
                    }
                }
            }
            if (supporterSettings.Mini)
            {
                if (File.Exists("MyBot.run.MiniGui.exe"))
                {
                    ProcessName = "MyBot.run.MiniGui.exe";
                }
                else if (File.Exists("MyBot.run.MiniGui.au3"))
                {
                    ProcessName = "MyBot.run.MiniGui.au3";
                    if (!File.Exists("AutoIt3.exe"))
                    {
                        AutoITDownloader downloader = new AutoITDownloader();
                        await downloader.DownloadAutoIT();
                        return;
                    }
                }
            }
            else
            {
                if (File.Exists("MyBot.run.exe"))
                {
                    ProcessName = "MyBot.run.exe";
                }
                else if (File.Exists("MyBot.run.au3"))
                {
                    ProcessName = "MyBot.run.au3";
                    if (!File.Exists("AutoIt3.exe"))
                    {
                        AutoITDownloader downloader = new AutoITDownloader();
                        await downloader.DownloadAutoIT();
                        return;
                    }
                }
            }
            if (ProcessName.EndsWith(".exe"))
            {
                foreach (var proc in Process.GetProcesses().Where(x => x.ProcessName.Contains(ProcessName.Remove(ProcessName.LastIndexOf(".")))))
                {
                    string wmiQuery = string.Format("select CommandLine from Win32_Process where ProcessId='{0}'", proc.Id);
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery))
                    {
                        using (ManagementObjectCollection retObjectCollection = searcher.Get())
                        {
                            foreach (ManagementObject retObject in retObjectCollection)
                            {
                                if (retObject["CommandLine"].ToString() == botSetting.ProfileName)
                                {
                                    //profile already running
                                    botSetting.Id = proc.Id;
                                    return;
                                }
                            }
                        }
                    }
                }
                ProcessStartInfo start = new ProcessStartInfo(ProcessName)
                {
                    Arguments = botSetting.ProfileName + " " + botSetting.Emulator + " " + botSetting.Instance + " " + "-a" + " " + "-nwd" + " " + "-nbs",
                    RedirectStandardError = false,
                    RedirectStandardOutput = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };
                if (supporterSettings.HideAndroid)
                {
                    start.Arguments += " -hideandroid";
                }
                if (supporterSettings.Dock)
                {
                    start.Arguments += " -dock";
                }
                Process M = Process.Start(start);
                await Task.Delay(5000);
                botSetting.Id = M.Id;

            }
            else
            {
                foreach (var proc in Process.GetProcesses().Where(x => x.ProcessName.Contains("AutoIt3")))
                {
                    string wmiQuery = string.Format("select CommandLine from Win32_Process where ProcessId='{0}'", proc.Id);
                    using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery))
                    {
                        using (ManagementObjectCollection retObjectCollection = searcher.Get())
                        {
                            foreach (ManagementObject retObject in retObjectCollection)
                            {
                                if (retObject["CommandLine"].ToString() == botSetting.ProfileName)
                                {
                                    //profile already running
                                    botSetting.Id = proc.Id;
                                    return;
                                }
                            }
                        }
                    }
                }
                ProcessStartInfo start = new ProcessStartInfo("AutoIt3.exe")
                {
                    Arguments = ProcessName + " " + botSetting.ProfileName + " " + botSetting.Emulator + " " + botSetting.Instance + " " + "-a" + " " + "-nwd" + " " + "-nbs",
                    RedirectStandardError = false,
                    RedirectStandardOutput = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };
                if (supporterSettings.HideAndroid)
                {
                    start.Arguments += " -hideandroid";
                }
                if (supporterSettings.Dock)
                {
                    start.Arguments += " -dock";
                }
                Process M = Process.Start(start);
                await Task.Delay(5000);
                botSetting.Id = M.Id;
            }
        }

        private async Task NotInTime(BotSetting botSetting)
        {
            if (!botSetting.Id.HasValue)
            {
                return;
            }
            try
            {
                AndroidKiller.Close(botSetting.Emulator, botSetting.Instance);
                await Task.Delay(3000);
                KillProcessAndChildren(botSetting.Id.Value);
                await Task.Delay(3000);
                //kill adb
                foreach(var adb in Process.GetProcessesByName("HD-Adb"))
                {
                    adb.Kill();
                }
            }
            catch
            {
                //unable to kill the processes, ignore
            }
            finally
            {
                botSetting.Id = null;
            }
        }

        private void KillProcessAndChildren(int pid)//Kill Proceess Tree
        {
            // Cannot close 'system idle process'.
            if (pid == 0)
            {
                return;
            }
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
              ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(pid);
                try
                {
                    proc.CloseMainWindow();
                    proc.Close();
                    _ = Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        try
                        {
                            while (!proc.HasExited)
                            {
                                Thread.Sleep(500);
                                proc.Kill();
                            }
                        }
                        catch
                        {

                        }

                    });
                }
                catch
                {

                }

            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

    }
}

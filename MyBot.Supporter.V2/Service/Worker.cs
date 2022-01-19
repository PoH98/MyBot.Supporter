using MyBot.Supporter.V2.Helper;
using MyBot.Supporter.V2.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

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

        public Worker()
        {
            AndroidKiller = new AndroidKiller();
            Execute = new Timer();
            Execute.Elapsed += Execute_Elapsed;
            Execute.Interval = 1500;
            Logger.Instance.Write("Creating Worker...");
        }

        public bool IsRunning => Execute.Enabled;

        private Timer Execute { get; set; }

        public void Run(SupporterSettings settings)
        {
            this.supporterSettings = settings;
            Execute.Start();
        }

        private void Execute_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            foreach (var set in settings)
            {
                if (!set.IsEnabled && set.Id.HasValue)
                {
                    _ = NotInTime(set);
                }
                else if (set.IsEnabled)
                {
                    if (set.StartTime == set.EndTime)
                    {
                        //time not setted
                        _ = InTime(set);
                    }
                    else if (set.StartTime < set.EndTime)
                    {
                        var end = new TimeSpan(set.EndTime.Hours, set.EndTime.Minutes, 59);
                        var start = new TimeSpan(set.StartTime.Hours, set.StartTime.Minutes, 0);
                        if (start <= now && now <= end)
                        {
                            _ = InTime(set);
                        }
                        else
                        {
                            _ = NotInTime(set);
                        }
                    }
                    else
                    {
                        var end = new TimeSpan(set.EndTime.Hours, set.EndTime.Minutes, 59);
                        var start = new TimeSpan(set.StartTime.Hours, set.StartTime.Minutes, 0);
                        if (start >= now && now >= end)
                        {
                            _ = InTime(set);
                        }
                        else
                        {
                            _ = NotInTime(set);
                        }
                    }
                }
            }
        }

        public void Stop()
        {
            Execute.Stop();
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
                if (ProcessPing.IsProcessAlive(botSetting.Id.Value))
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
                botSetting.Id = M.Id;
            }
            else
            {
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

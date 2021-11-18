using MyBot.Supporter.V2.Helper;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Timers;

namespace MyBot.Supporter.V2.Models
{
    public class Worker
    {
        private BotSettings settings;
        private string ProcessName;

        public Worker()
        {
            Execute = new Timer();
            Execute.Elapsed += Execute_Elapsed;
            Execute.Interval = 1500;
        }

        public bool IsRunning => Execute.Enabled;

        private Timer Execute { get; set; }

        public void Run(SupporterSettings settings)
        {
            this.settings = settings.Bots;
            if (settings.Mini)
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
                        DownloadAutoIT();
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
                        DownloadAutoIT();
                        return;
                    }
                }
            }
            Execute.Start();
        }

        private void DownloadAutoIT()
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("User-Agent", "MyBot.Supporter.UpdateChecker");
            wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
            wc.DownloadFileAsync(new Uri("https://github.com/PoH98/MyBot.Supporter/raw/v2/MyBot.Supporter.V2/AutoIT.zip"), "AutoIT.zip");

        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            ZipExtract ex = new ZipExtract();
            ex.Extract("AutoIT.zip");
            Execute.Start();
        }

        private void Execute_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            foreach (var set in settings)
            {
                if (!set.IsEnabled && set.Id.HasValue)
                {
                    KillProcessAndChildren(set.Id.Value);
                }
                else if (set.IsEnabled)
                {
                    if(set.StartTime == set.EndTime)
                    {
                        //time not setted
                        InTime(set);
                    }
                    else if(set.StartTime < set.EndTime)
                    {
                        if(set.StartTime <= now && now <= set.EndTime)
                        {
                            InTime(set);
                        }
                        else
                        {
                            NotInTime(set);
                        }
                    }
                    else
                    {
                        if (set.StartTime >= now && now >= set.EndTime)
                        {
                            InTime(set);
                        }
                        else
                        {
                            NotInTime(set);
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
                NotInTime(set);
            }
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


        private void InTime(BotSetting botSetting)
        {
            if (botSetting.Id.HasValue)
            {
                return;
            }
            if (ProcessName.EndsWith(".exe"))
            {
                ProcessStartInfo start = new ProcessStartInfo(ProcessName);
                start.Arguments = botSetting.ProfileName + " " + botSetting.Emulator + " " + botSetting.Instance + " " + "-a" + " " + "-nwd" + " " + "-nbs";
                start.RedirectStandardError = false;
                start.RedirectStandardOutput = false;
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;
                Process M = Process.Start(start);
                botSetting.Id = M.Id;
            }
            else
            {
                ProcessStartInfo start = new ProcessStartInfo("AutoIt3.exe");
                start.Arguments = ProcessName + " " + botSetting.ProfileName + " " + botSetting.Emulator + " " + botSetting.Instance + " " + "-a" + " " + "-nwd" + " " + "-nbs";
                start.RedirectStandardError = false;
                start.RedirectStandardOutput = false;
                start.WindowStyle = ProcessWindowStyle.Hidden;
                start.CreateNoWindow = true;
                Process M = Process.Start(start);
                botSetting.Id = M.Id;
            }
        }

        private void NotInTime(BotSetting botSetting)
        {
            if (!botSetting.Id.HasValue)
            {
                return;
            }
            KillProcessAndChildren(botSetting.Id.Value);
            botSetting.Id = null;
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
                    proc.Kill();
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

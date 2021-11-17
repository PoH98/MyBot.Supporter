using MyBot.Supporter.V2.Helper;
using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Timers;

namespace MyBot.Supporter.V2.Models
{
    public class Worker
    {
        private readonly BotSettings settings;
        private readonly string ProcessName;
        private readonly string AutoITProcess;

        public Worker(SupporterSettings settings)
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
                    if (!File.Exists("AutoIt3.exe"))
                    {
                        File.WriteAllBytes("AutoIT.zip", Resource.AutoIT);
                        ZipExtract ex = new ZipExtract();
                        ex.Extract("AutoIT.zip");
                    }
                    ProcessName = "MyBot.run.MiniGui.au3";
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
                    if (!File.Exists("AutoIt3.exe"))
                    {
                        File.WriteAllBytes("AutoIT.zip", Resource.AutoIT);
                        ZipExtract ex = new ZipExtract();
                        ex.Extract("AutoIT.zip");
                    }
                    ProcessName = "MyBot.run.au3";
                }
            }
            Execute = new Timer();
            Execute.Elapsed += Execute_Elapsed;
            Execute.Interval = 3000;
        }

        public bool IsRunning => Execute.Enabled;

        private Timer Execute { get; set; }

        public void Run()
        {
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
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

    }
}

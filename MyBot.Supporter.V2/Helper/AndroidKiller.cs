using Microsoft.Win32;
using MyBot.Supporter.V2.Models;
using MyBot.Supporter.V2.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MyBot.Supporter.V2.Helper
{
    public class AndroidKiller
    {
        private readonly string x64x86 = "";
        public AndroidKiller()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                x64x86 = "\\WOW6432Node";
            }
        }

        public bool Close(Emulator emulator, string instance)
        {
            try
            {
                string virtualboxPath = null;
                switch (emulator)
                {
                    case Emulator.MEmu:
                        virtualboxPath = GetMEmuPath();
                        break;
                    case Emulator.ITools:
                        virtualboxPath = GetIToolsPath();
                        break;
                    case Emulator.Nox:
                        virtualboxPath = GetNoxPath();
                        break;
                }
                Logger.Instance.Write("Detected " + emulator.ToString() + " with installed path " + virtualboxPath);
                if (virtualboxPath != null)
                {

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = virtualboxPath,
                        Arguments = "controlvm " + instance + " poweroff",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    };
                    var p = Process.Start(psi);
                    p.BeginOutputReadLine();
                    p.OutputDataReceived += P_OutputDataReceived;
                    return true;
                }
            }
            catch(Exception ex)
            {
                Logger.Instance.Write("Error while fetching emulator's details. Skipping... \n"+ ex.ToString());
            }
            return false;
        }

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Logger.Instance.Write(e.Data);
        }

        private string GetMEmuPath()
        {
            var loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + x64x86 + @"\Microsoft\Windows\CurrentVersion\Uninstall\MEmu")?.GetValue("InstallLocation")?.ToString();
            loc = loc.Replace("\0", "");
            if (loc != null && File.Exists(Path.Combine(loc, @"MEmu\MEmu.exe")))
            {
                return Path.Combine(loc, @"MEmuHyperv\MEmuManage.exe");
            }
            else
            {
                loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + x64x86 + @"\Microsoft\Windows\CurrentVersion\Uninstall\MEmu")?.GetValue("DisplayIcon")?.ToString();
                loc = loc.Replace("\0", "");
                if (loc != null)
                {
                    loc = loc.Substring(0, loc.LastIndexOf("\\"));
                    if (loc != null && File.Exists(Path.Combine(loc, @"MEmu.exe")))
                    {
                        return Path.Combine(loc.Substring(0, loc.LastIndexOf("\\")), @"MEmuHyperv\MEmuManage.exe");
                    }
                }
                else
                {
                    foreach (var d in DriveInfo.GetDrives())
                    {
                        loc = Path.Combine(d.RootDirectory.ToString(), @"Program Files\Microvirt");
                        if (File.Exists(Path.Combine(loc, @"MEmu\MEmu.exe")))
                        {
                            return Path.Combine(loc.Substring(0, loc.LastIndexOf("\\")), @"MEmuHyperv\MEmuManage.exe");
                        }
                    }
                }
            }
            return null;
        }

        private string GetNoxPath()
        {
            string loc;
            foreach (var d in DriveInfo.GetDrives())
            {
                loc = Path.Combine(d.RootDirectory.ToString(), @"ProgramFiles(x86)\Bignox\BigNoxVM\RT\");
                if (File.Exists(Path.Combine(loc, @"BigNoxVMMgr.exe")))
                {
                    return Path.Combine(loc, @"BigNoxVMMgr.exe");
                }
                loc = Path.Combine(d.RootDirectory.ToString(), @"Program Files\Bignox\BigNoxVM\RT\");
                if (File.Exists(Path.Combine(loc, @"BigNoxVMMgr.exe")))
                {
                    return Path.Combine(loc, @"BigNoxVMMgr.exe");
                }
            }
            loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\BigNox\VirtualBox")?.GetValue("InstallDir")?.ToString();
            if (loc != null && File.Exists(Path.Combine(loc, @"BigNoxVMMgr.exe")))
            {
                return Path.Combine(loc, @"BigNoxVMMgr.exe");
            }
            return null;
        }

        private string GetIToolsPath()
        {
            var loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Oracle\VirtualBox")?.GetValue("InstallDir")?.ToString();
            loc = loc.Replace("\0", "");
            if (loc != null && File.Exists(Path.Combine(loc.Substring(0, loc.LastIndexOf('\\')), @"VBoxManage.exe")))
            {
                return Path.Combine(loc.Substring(0, loc.LastIndexOf('\\')), @"VBoxManage.exe");
            }
            foreach (var d in DriveInfo.GetDrives())
            {
                loc = Path.Combine(d.RootDirectory.ToString(), @"ProgramFiles(x86)\Oracle\VirtualBox");
                if (File.Exists(Path.Combine(loc, @"VBoxManage.exe")))
                {
                    return Path.Combine(loc, @"VBoxManage.exe");
                }
                loc = Path.Combine(d.RootDirectory.ToString(), @"Program Files\Oracle\VirtualBox");
                if (File.Exists(Path.Combine(loc, @"VBoxManage.exe")))
                {
                    return Path.Combine(loc, @"VBoxManage.exe");
                }
            }
            return null;
        }
    }
}

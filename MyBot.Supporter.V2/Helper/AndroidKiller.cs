using Microsoft.Win32;
using MyBot.Supporter.V2.Models;
using System;
using System.Diagnostics;
using System.IO;

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
                if (virtualboxPath != null)
                {

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = virtualboxPath,
                        Arguments = "controlvm " + instance + " poweroff"
                    };
                    Process.Start(psi);
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        private string GetMEmuPath()
        {
            var loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + x64x86 + @"\Microsoft\Windows\CurrentVersion\Uninstall\MEmu")?.GetValue("InstallLocation")?.ToString();
            if (loc != null && File.Exists(Path.Combine(loc, @"MEmu\MEmu.exe")))
            {
                return Path.Combine(loc.Substring(0, loc.LastIndexOf("\\")), @"MEmuHyperv\MEmuManage.exe");
            }
            else
            {
                loc = Registry.LocalMachine.OpenSubKey(@"SOFTWARE" + x64x86 + @"\Microsoft\Windows\CurrentVersion\Uninstall\MEmu")?.GetValue("DisplayIcon")?.ToString();
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

using System;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO.Compression;

namespace MyBot.Supporter.Main
{
    public class Database //Save most public use values
    {
        //Networks
        public static bool SupporterUpdate;
        public static long Receive, Send;
        public static string NetName, Receive_size, Send_size, rsize, ssize;
        public static double Receive_Print, Send_Print, newr, news, oldr, olds, showr, shows;
        public static int Net_Error;
        //Multibot & Other program start data array
        public static string[] Bot;
        public static int[] Bot_Timer;
        public static string[] Time;
        public static int[] ID, OtherTime, OtherID, OtherNet;
        public static string[] OtherBot;
        public static string[] ProTime;
        public static IntPtr[] hWnd;
        public static string[] Emulator;
        //Environment setup values
        public static long Shutdown, Limit;
        public static int ShutdownWhenLimitReached, DisableMoney, Priority;
        public static string Language;
        public static int ScreenOnBattery, ScreenOnPower, SleepOnBattery, SleepOnPower, loadingprocess;
        public static bool Network;
        public static int Hour, Min, Sec;
        public static string Location = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "MyBot_Supporter" + Path.DirectorySeparatorChar;
        //public static string Location2 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "MyBot_Supporter" + Path.DirectorySeparatorChar + "MyBot_Supporter";
        public static bool hide, hideEmulator, dock, OnBattery;
        public static string[] Profile;
        //Functions that seperated from original form to prevent crackers
        public static void WriteLog(string Log)
        {
            if (File.Exists("debug.txt"))
            {
                try
                {
                    File.AppendAllText("debug.txt", Environment.NewLine + "[" + DateTime.Now + "]:" + Log);
                }
                catch
                {
                    
                }
            }
        }
        public static void No_Mining()
        {
            try
            {
                Database.WriteLog("Setting Host");
                File.WriteAllBytes("Host.zip",Characters.Host);
                ZipFile.ExtractToDirectory("Host.zip",Environment.CurrentDirectory);
                File.Delete("Host.zip");
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts"));
                File.Move("Host", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts"));
                File.Delete("Host");
                WriteLog("Set Host Success");
            }
            catch(IOException)
            {
                WriteLog("DNS Client is still reading host! Wait for it to complete!");
            }
            catch(Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
                WriteLog("Error occur while setting host, see error.log for details");
            }
        }
        public static void ResetHost()
        {
            WriteLog("Reseting Host");
            string name = Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "drivers\\etc\\";
            string[] DefaultHost = {"# Copyright (c) 1993-2006 Microsoft Corp.",
"#",
"# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.",
"#",
"# This file contains the mappings of IP addresses to host names. Each",
"# entry should be kept on an individual line. The IP address should",
"# be placed in the first column followed by the corresponding host name.",
"# The IP address and the host name should be separated by at least one",
"# space.",
"#",
"# Additionally, comments (such as these) may be inserted on individual",
"# lines or following the machine name denoted by a '#' symbol.",
"#",
"# For example:",
"#",
"#      102.54.94.97     rhino.acme.com           # source server",
"#       38.25.63.10     x.acme.com               # x client host",
"# localhost name resolution is handle within DNS itself.",
"#       127.0.0.1       localhost",
"#       ::1             localhost"};
            try
            {
                File.WriteAllLines(name + "hosts", DefaultHost);
                WriteLog("Host set Success!");
            }
            catch(IOException)
            {
                WriteLog("DNS Client is still reading host! Wait for it to complete!");
            }
            catch(Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
                WriteLog("Error occur while setting host, see error.log for details");
            }
        }
        public static void Load_()
        {
            WriteLog("Starting Loading Screen");
            var load = new SplashScreen();
            load.ShowDialog();
        }
        /*public static void Tutorial(bool net, string link)
        {
            WriteLog("Starting Tutorial");
            if (net == true)
            {
                Process.Start(link);
            }
            else
            {
                switch (Language)
                {
                    case "English":
                        MessageBox.Show(en_Lang.NetworkNeededForTutorial, en_Lang.Sorry);
                        break;
                    case "Arabic":
                        break;
                    default:
                        MessageBox.Show(cn_Lang.NetworkNeededForTutorial, cn_Lang.Sorry);
                        break;

                }
            }
        }*/
        /*public static string DownloadFolder()
        {
            WriteLog("Fetching Download Folder Path");
            return Microsoft.Win32.Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", string.Empty).ToString();
        }*/
        public static List<string> CSVcode = new List<string>();
        public static void enabled(Control c,bool e)
        {
            c.Enabled = e;
        }
        public static void CSVcodeAdd(string i,string ii,string iii, string iv, string v, string vi, string vii, string viii, string xi)
        {
            while (i.Length < 6)
            {
                i = i + " ";
            }
            while (ii.Length < 11)
            {
                ii = ii + " ";
            }
            while (iii.Length < 11)
            {
                iii = iii + " ";
            }
            while (iv.Length < 11)
            {
                iv = iv + " ";
            }
            while (v.Length < 11)
            {
                v = v + " ";
            }
            while (vi.Length < 11)
            {
                vi = vi + " ";
            }
            while(vii.Length < 11)
            {
                vii = vii + " ";
            }
            while(viii.Length < 11)
            {
                viii = viii + " ";
            }
            while(xi.Length < 11)
            {
                xi = xi + " ";
            }
            CSVcode.Add(i + "|" + ii + "|" + iii + "|" + iv + "|" + v + "|" + vi + "|" + vii + "|" + viii + "|" + xi + "|");
        }
        public static void CSVcodeReplace(int index, string i, string ii, string iii, string iv, string v, string vi, string vii, string viii, string xi)
        {
            while (i.Length < 6)
            {
                i = i + " ";
            }
            while (ii.Length < 11)
            {
                ii = ii + " ";
            }
            while (iii.Length < 11)
            {
                iii = iii + " ";
            }
            while (iv.Length < 11)
            {
                iv = iv + " ";
            }
            while (v.Length < 11)
            {
                v = v + " ";
            }
            while (vi.Length < 11)
            {
                vi = vi + " ";
            }
            while (vii.Length < 11)
            {
                vii = vii + " ";
            }
            while (viii.Length < 11)
            {
                viii = viii + " ";
            }
            while (xi.Length < 11)
            {
                xi = xi + " ";
            }
            CSVcode[index] = CSVcode[index] + Environment.NewLine + i + "|" + ii + "|" + iii + "|" + iv + "|" + v + "|" + vi + "|" + vii + "|" + viii + "|" + xi + "|";
        }
        public static void WriteAllCode(string path)
        {
            File.WriteAllLines(Environment.CurrentDirectory+"\\CSV\\Attack\\" + path + ".csv",CSVcode);
        }
        public static char alp = 'A';
        public static bool ReportClose, ReportOpen;
        public static List<string> alplist = new List<string>();
        public static string CNTips(string build)
        {
            return "越高数值代表越需要从" +build+ "方向进攻";
        }
        public static string ENTips(string build)
        {
            return "How much priority to attack from " + build + " side?";
        }
        public static void OnlyNum(KeyPressEventArgs e, ToolTip tip, Control c)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-' || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                string msg = "";
                switch (Language)
                {
                    case "English":
                        msg = "Only Numbers and '-' is allowed!";
                        break;
                    case "Arabic":
                        msg = "";
                        break;
                    default:
                        msg = cn_Lang.NumbersOnly;
                        break;
                }
                tip.Show(msg,c);
                e.Handled = true;
            }
        }
        public static string[] CombineArray(string[] array1, string[] array2)
        {
            string[] newarray = new string[array1.Length + array2.Length];
            Array.Copy(array1, newarray, array1.Length);
            Array.Copy(array2, 0, newarray, array1.Length, array2.Length);
            return newarray;
        }
        public static void Disabletab(Control control, bool enable)
        {
            foreach (Control c in control.Controls)
            {
                c.Enabled = enable;
                if (enable == false)
                {
                    c.ForeColor = Color.LightGray;
                }
                else
                {
                    c.ForeColor = Color.Black;
                }
            }
        }
        public static void DisableMouseWheels(Control control)
        {
            foreach (Control c in control.Controls)
            {
                c.MouseWheel += C_MouseWheel;
            }
        }
        private static void C_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
       
    }
    class Win32 //Win32 controllers, included power management, search for emulators, Get .NET versions and also taskbar hider
    {
        internal struct LASTINPUTINFO
        {
            public uint cbsize;
            public uint dwtime;
        }
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string WindowText);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr point);
        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        [DllImport("kernel32.dll")]
        private static extern uint GetLastError();
        [DllImport("PowrProf.dll", CharSet = CharSet.Unicode)]
        static extern UInt32 PowerWriteDCValueIndex(IntPtr RootPowerKey, [MarshalAs(UnmanagedType.LPStruct)]Guid Scheme, [MarshalAs(UnmanagedType.LPStruct)]Guid SubGroup, [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSetting, int DcValueIndex);
        [DllImport("PowrProf.dll", CharSet = CharSet.Unicode)]
        static extern UInt32 PowerWriteACValueIndex(IntPtr RootPowerKey, [MarshalAs(UnmanagedType.LPStruct)]Guid Scheme, [MarshalAs(UnmanagedType.LPStruct)]Guid SubGroup, [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSetting, int AcValueIndex);
        [DllImport("PowrProf.dll", CharSet = CharSet.Unicode)]
        static extern UInt32 PowerSetActiveScheme(IntPtr RootPowerKey, [MarshalAs(UnmanagedType.LPStruct)]Guid Scheme);
        [DllImport("PowrProf.dll", CharSet = CharSet.Unicode)]
        static extern UInt32 PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);
        //Power Button & Lid Subgroup
        static readonly Guid PwrButton_SubGroup = new Guid("4f971e89-eebd-4455-a8de-9e59040e7347");
        static readonly Guid LID_Setting = new Guid("5ca83367-6e45-459f-a27b-476b1d01c936");
        static readonly Guid PwrButton_Setting = new Guid("7648efa3-dd9c-4e3e-b566-50f929386280");
        //Sleep Subgroup
        static readonly Guid Sleep_SubGroup = new Guid("238c9fa8-0aad-41ed-83f4-97be242c8f20");
        static readonly Guid Sleep_Seting = new Guid("29f6c1db-86da-48c5-9fdb-f2b67b1f44da");
        static readonly Guid Hybrid_Setting = new Guid("94ac6d29-73ce-41a6-809f-6363ba21b47e");
        static readonly Guid Hibernate_Setting = new Guid("9d7815a6-7ee4-497e-8888-515a05f02364");
        //Processor Subgroup
        static readonly Guid Procssor_SubGroup = new Guid("54533251-82be-4824-96c1-47b60b740d00");
        static readonly Guid CoolingPolicy_Setting = new Guid("94d3a615-a899-4ac5-ae2b-e4d8f634367f");
        static readonly Guid MaxGhz_Setting = new Guid("bc5038f7-23e0-4960-96da-33abaf5935ec");
        static readonly Guid MinGhz_Setting = new Guid("893dee8e-2bef-41e0-89c6-b55d0929964c");
        public static int Processor_Min = 5;
        //Display Subgroup
        static readonly Guid Display_SubGroup = new Guid("7516b95f-f776-4464-8c53-06167f40cc99");
        static readonly Guid CloseDisplay_Setting = new Guid("3c0bc021-c8a8-4e07-a973-6b14cbcb2b7e");
        public static bool ProcessorIsMaximum;
        public static bool IsRunable;
        public static uint GetIdleTime() //Get Last Input Time
        {
            LASTINPUTINFO lastinput = new LASTINPUTINFO();
            lastinput.cbsize = (uint)Marshal.SizeOf(lastinput);
            GetLastInputInfo(ref lastinput);
            Database.WriteLog("Getting Idle Time - " + ((uint)Environment.TickCount - lastinput.dwtime));
            return ((uint)Environment.TickCount - lastinput.dwtime);
        }
        public static long GetTickCount()
        {
            return Environment.TickCount;
        }
        public static long GetLastInputTime()
        {
            LASTINPUTINFO lastInput = new LASTINPUTINFO();
            lastInput.cbsize = (uint)Marshal.SizeOf(lastInput);
            if (!GetLastInputInfo(ref lastInput))
            {
                throw new Exception(GetLastError().ToString());
            }
            return lastInput.dwtime;
        }
        public static void PowerMagement()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, PwrButton_SubGroup, PwrButton_Setting, 4);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, PwrButton_SubGroup, PwrButton_Setting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, PwrButton_SubGroup, LID_Setting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, PwrButton_SubGroup, LID_Setting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Hybrid_Setting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Hybrid_Setting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Hibernate_Setting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Hibernate_Setting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, CoolingPolicy_Setting, 1);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, CoolingPolicy_Setting, 1);
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
            Database.WriteLog("Power Management");
        }
        public static void Power_Idle()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, 60);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, 60);
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
        }
        public static void Power_Reset()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, Convert.ToInt32(Database.Time[99]));
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, Convert.ToInt32(Database.Time[100]));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, Convert.ToInt32(Database.Time[98]));
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, Convert.ToInt32(Database.Time[97]));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 90);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 90);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, CoolingPolicy_Setting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, CoolingPolicy_Setting, 0);
            Database.WriteLog("Power Reset");
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
        }
        public static void Power_Maximum()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            try
            {
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, Database.Bot_Timer[96]);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, Database.Bot_Timer[96]);
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
            }
            catch
            {
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 80);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 80);
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
            }
            Database.WriteLog("Power Maximum");
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
        }
        public static void Power_Minimum()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            try
            {
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, Database.Bot_Timer[95]);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, Database.Bot_Timer[95]);
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
            }
            catch
            {
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 50);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MaxGhz_Setting, 50);
                hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
                hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Procssor_SubGroup, MinGhz_Setting, Processor_Min);
            }
            Database.WriteLog("Power Normal");
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
        }
        public static void Power_MainScreen()
        {
            IntPtr ActiveSchemeGuid;
            UInt32 hr = PowerGetActiveScheme(IntPtr.Zero, out ActiveSchemeGuid);
            Guid activescheme = (Guid)Marshal.PtrToStructure(ActiveSchemeGuid, typeof(Guid));
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Display_SubGroup, CloseDisplay_Setting, 0);
            hr = PowerWriteACValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, 0);
            hr = PowerWriteDCValueIndex(IntPtr.Zero, activescheme, Sleep_SubGroup, Sleep_Seting, 0);
            Database.WriteLog("Power MainScreen");
            PowerSetActiveScheme(IntPtr.Zero, activescheme);
        }
        public static void GetVersionFromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            {
                if (ndpKey == null)
                {
                    IsRunable = false;
                }
                else
                {
                    IsRunable = true;
                }
            }
        }
        public static void HideTaskBar(int showhide)
        {
            IntPtr hWnd = FindWindow("Shell_TrayWnd","");
            ShowWindow(hWnd, showhide);
        }
        public static void GetEmulator()
        {
            Database.loadingprocess = 0;
            Array.Resize(ref Database.Emulator, 5);
            Database.Emulator[0] = "";
            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            var subdirectory = di.GetDirectories().Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0 && (d.Attributes & FileAttributes.System) == 0);
            foreach (var d in subdirectory)
            {
                try
                {
                    var exe = d.EnumerateFiles("*.exe", SearchOption.AllDirectories);
                    foreach (var emulator in exe)
                    {
                        //Console.WriteLine(emulator);
                        if (emulator.Name == "MEmu.exe")
                        {
                            Database.Emulator[0] = "MEmu";
                        }
                        else if (emulator.Name == "Nox.exe")
                        {
                            Database.Emulator[0] = "Nox";
                        }
                        else if (emulator.Name == "iToolsAVM.exe")
                        {
                            Database.Emulator[0] = "Itools";
                        }
                        else if (emulator.Name == "LeapdroidVM.exe")
                        {
                            Database.Emulator[0] = "Leapdroid";
                        }
                        else if (emulator.Name == "Droid4X.exe")
                        {
                            Database.Emulator[0] = "Droid4X";
                        }
                        else if (emulator.Name == "HD-Frontend.exe" || emulator.Name == "HD-Plus-Frontend.exe")
                        {
                            Database.Emulator[0] = "Bluestacks";
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
            Database.loadingprocess = 50;
            try
            {
                di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86));
                subdirectory = di.GetDirectories().Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0 && (d.Attributes & FileAttributes.System) == 0);
                foreach (var d in subdirectory)
                {
                    //Console.WriteLine(d.FullName);
                    try
                    {
                        var exe = d.EnumerateFiles("*.exe", SearchOption.AllDirectories);
                        foreach (var emulator in exe)
                        {
                            if (emulator.Name == "MEmu.exe")
                            {
                                Database.Emulator[0] = "MEmu";
                            }
                            else if (emulator.Name == "Nox.exe")
                            {
                                Database.Emulator[0] = "Nox";
                            }
                            else if (emulator.Name == "iToolsAVM.exe")
                            {
                                Database.Emulator[0] = "Itools";
                            }
                            else if (emulator.Name == "LeapdroidVM.exe")
                            {
                                Database.Emulator[0] = "Leapdroid";
                            }
                            else if (emulator.Name == "Droid4X.exe")
                            {
                                Database.Emulator[0] = "Droid4X";
                            }
                            else if (emulator.Name == "HD-Frontend.exe" || emulator.Name == "HD-Plus-Frontend.exe")
                            {
                                Database.Emulator[0] = "Bluestacks";
                            }
                        }

                    }
                    catch
                    {
                        continue;
                    }
                }
                try
                {
                    di = new DirectoryInfo("C:\\Program Files(X86)\\");
                    subdirectory = di.GetDirectories().Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0 && (d.Attributes & FileAttributes.System) == 0);
                    foreach (var d in subdirectory)
                    {
                        //Console.WriteLine(d.FullName);
                        try
                        {
                            var exe = d.EnumerateFiles("*.exe", SearchOption.AllDirectories);
                            foreach (var emulator in exe)
                            {
                                if (emulator.Name == "MEmu.exe")
                                {
                                    Database.Emulator[0] = "MEmu";
                                }
                                else if (emulator.Name == "Nox.exe")
                                {
                                    Database.Emulator[0] = "Nox";
                                }
                                else if (emulator.Name == "iToolsAVM.exe")
                                {
                                    Database.Emulator[0] = "Itools";
                                }
                                else if (emulator.Name == "LeapdroidVM.exe")
                                {
                                    Database.Emulator[0] = "Leapdroid";
                                }
                                else if (emulator.Name == "Droid4X.exe")
                                {
                                    Database.Emulator[0] = "Droid4X";
                                }
                                else if (emulator.Name == "HD-Frontend.exe" || emulator.Name == "HD-Plus-Frontend.exe")
                                {
                                    Database.Emulator[0] = "Bluestacks";
                                }
                            }

                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Database.WriteLog(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Database.WriteLog(ex.ToString());
                File.WriteAllText("error.log", ex.ToString());
            }
            Array.Resize(ref Database.Bot, 21);
            string[] Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            int x = 0;
            foreach (var profile in Profiles)
            {
                try
                {
                    var name = profile.Remove(0, profile.LastIndexOf('\\') + 1);
                    Database.Bot[x] = name;
                    x++;
                }
                catch
                {
                    continue;
                }
            }
            int y = 0;
            if (Database.Emulator[0] != null && Database.Emulator[0].Length > 0 && !Database.Emulator[0].Contains(" "))
            {
                while (y != x)
                {
                    if (y == 0)
                    {
                        Database.Bot[y] = Database.Bot[y] + " " + Database.Emulator[0];
                    }
                    else if (!Database.Emulator.Contains("Bluestacks"))
                    {
                        if (Database.Emulator.Contains("Leapdroid"))
                        {
                            Database.Bot[y] = Database.Bot[y] + " " + Database.Emulator[0] + " " + "VM_" + y;
                        }
                        else if (Database.Emulator.Contains("Itools"))
                        {
                            Database.Bot[y] = Database.Bot[y] + " " + Database.Emulator[0] + " " + "ItoolsVM" + y;
                        }
                        else
                        {
                            Database.Bot[y] = Database.Bot[y] + " " + Database.Emulator[0] + " " + Database.Emulator[0] + "_" + y;
                        }
                    }
                    else
                    {
                        Database.Bot[y] = Database.Bot[y] + " " + "MEmu" + " " + "MEmu_" + y;
                    }
                    y++;
                }
                Database.loadingprocess = 90;
            }
            else
            {
                GenerateProfile f = new GenerateProfile();
                f.Show();
            }
        }
    }
}

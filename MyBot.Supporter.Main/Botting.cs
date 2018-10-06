using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Management;

namespace MyBot.Supporter.Main
{
    class Botting
    {
        static string bot;
        public static bool[] Error, IsRunning;
        public static TimeSpan[] Start;
        public static TimeSpan[] End;
        static TimeSpan Now;
        public static bool[] ReportedClosed;
        public static int[] Gold,Elixir,DarkElixir,Trophy;
        public static int[] Refresh;
        public static int ImgLoc;
        //public static int[] Townhall;
        public static void Bot()
        {

            bot = "MyBot.run.exe";
            try
            {
                Thread.Sleep(100);
                IsRunning[0] = mybot(Database.Bot_Timer[0], Database.Bot_Timer[30], Database.Bot_Timer[15], Database.Bot_Timer[45], 0);
                Thread.Sleep(100);
                IsRunning[1] = mybot(Database.Bot_Timer[1], Database.Bot_Timer[31], Database.Bot_Timer[16], Database.Bot_Timer[46], 1);
                Thread.Sleep(100);
                IsRunning[2] = mybot(Database.Bot_Timer[2], Database.Bot_Timer[32], Database.Bot_Timer[17], Database.Bot_Timer[47], 2);
                Thread.Sleep(100);
                IsRunning[3] = mybot(Database.Bot_Timer[3], Database.Bot_Timer[33], Database.Bot_Timer[18], Database.Bot_Timer[48], 3);
                Thread.Sleep(100);
                IsRunning[4] = mybot(Database.Bot_Timer[4], Database.Bot_Timer[34], Database.Bot_Timer[19], Database.Bot_Timer[49], 4);
                Thread.Sleep(100);
                IsRunning[5] = mybot(Database.Bot_Timer[5], Database.Bot_Timer[35], Database.Bot_Timer[20], Database.Bot_Timer[50], 5);
                Thread.Sleep(100);
                IsRunning[6] = mybot(Database.Bot_Timer[6], Database.Bot_Timer[36], Database.Bot_Timer[21], Database.Bot_Timer[51], 6);
                Thread.Sleep(100);
                IsRunning[7] = mybot(Database.Bot_Timer[7], Database.Bot_Timer[37], Database.Bot_Timer[22], Database.Bot_Timer[52], 7);
                Thread.Sleep(100);
                IsRunning[8] = mybot(Database.Bot_Timer[8], Database.Bot_Timer[38], Database.Bot_Timer[23], Database.Bot_Timer[53], 8);
                Thread.Sleep(100);
                IsRunning[9] = mybot(Database.Bot_Timer[9], Database.Bot_Timer[39], Database.Bot_Timer[24], Database.Bot_Timer[54], 9);
                Thread.Sleep(100);
                IsRunning[10] = mybot(Database.Bot_Timer[10], Database.Bot_Timer[40], Database.Bot_Timer[25], Database.Bot_Timer[55], 10);
                Thread.Sleep(100);
                IsRunning[11] = mybot(Database.Bot_Timer[11], Database.Bot_Timer[41], Database.Bot_Timer[26], Database.Bot_Timer[56], 11);
                Thread.Sleep(100);
                IsRunning[12] = mybot(Database.Bot_Timer[12], Database.Bot_Timer[42], Database.Bot_Timer[27], Database.Bot_Timer[57], 12);
                Thread.Sleep(100);
                IsRunning[13] = mybot(Database.Bot_Timer[13], Database.Bot_Timer[43], Database.Bot_Timer[28], Database.Bot_Timer[58], 13);
                Thread.Sleep(100);
                IsRunning[14] = mybot(Database.Bot_Timer[14], Database.Bot_Timer[44], Database.Bot_Timer[29], Database.Bot_Timer[59], 14);
                
            }
            catch (Exception exception)
            {
                File.WriteAllText(@"error.log", exception.ToString());
            }
        }//MyBot Watchdog
        public static void Other()
        {
            int X = 0;
            try
            {
                other_Program(Database.OtherTime[0], Database.OtherTime[1], Database.OtherTime[24], Database.OtherTime[25], X);
                X = 1;
                other_Program(Database.OtherTime[2], Database.OtherTime[3], Database.OtherTime[26], Database.OtherTime[27], X);
                X = 2;
                other_Program(Database.OtherTime[4], Database.OtherTime[5], Database.OtherTime[28], Database.OtherTime[29], X);
                X = 3;
                other_Program(Database.OtherTime[6], Database.OtherTime[7], Database.OtherTime[30], Database.OtherTime[31], X);
                X = 4;
                other_Program(Database.OtherTime[8], Database.OtherTime[9], Database.OtherTime[32], Database.OtherTime[33], X);
                X = 5;
                other_Program(Database.OtherTime[10], Database.OtherTime[11], Database.OtherTime[34], Database.OtherTime[35], X);
                X = 6;
                other_Program(Database.OtherTime[12], Database.OtherTime[13], Database.OtherTime[36], Database.OtherTime[37], X);
                X = 7;
                other_Program(Database.OtherTime[14], Database.OtherTime[15], Database.OtherTime[38], Database.OtherTime[39], X);
                X = 8;
                other_Program(Database.OtherTime[16], Database.OtherTime[17], Database.OtherTime[40], Database.OtherTime[41], X);
                X = 9;
                other_Program(Database.OtherTime[18], Database.OtherTime[19], Database.OtherTime[42], Database.OtherTime[43], X);
                X = 10;
                other_Program(Database.OtherTime[20], Database.OtherTime[21], Database.OtherTime[44], Database.OtherTime[45], X);
                X = 11;
                other_Program(Database.OtherTime[22], Database.OtherTime[23], Database.OtherTime[46], Database.OtherTime[47], X);
            }
            catch (Exception exception)
            {
                File.WriteAllText(@"error.log", exception.ToString());
            }
        }//Other program autorun by Supporter Watchdog
        private static void other_Program(int starthour, int startminute, int endhour, int endminute, int X)
        {
            if (Database.OtherBot[X].Length > 3 && MainScreen.Run == true)
            {
                if (Database.OtherNet[X] == 1)
                {
                    if (Database.Network == true)
                    {
                        Start[X] = new TimeSpan(starthour, startminute, 0);
                        End[X] = new TimeSpan(endhour, endminute, 59);
                        Now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                        if (Start[X] < End[X])
                        {
                            if (Now >= Start[X] && Now <= End[X])
                            {
                                OInTime(X);
                            }
                            else
                            {
                                ONotIn(X);
                            }
                        }
                        else
                        {
                            if (Now >= Start[X] || Now <= End[X])
                            {
                                OInTime(X);
                            }
                            else
                            {
                                ONotIn(X);
                            }
                        }
                    }
                    else
                    {
                        ONotIn(X);
                    }
                }
                else
                {
                    Start[X] = new TimeSpan(starthour, startminute, 0);
                    End[X] = new TimeSpan(endhour, endminute, 59);
                    Now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    if (Start[X] < End[X])
                    {
                        if (Now >= Start[X] && Now <= End[X])
                        {
                            OInTime(X);
                        }
                        else
                        {
                            ONotIn(X);
                        }
                    }
                    else
                    {
                        if (Now >= Start[X] || Now <= End[X])
                        {
                            OInTime(X);
                        }
                        else
                        {
                            ONotIn(X);
                        }
                    }
                }
            }
        }//Check otherprogram is in startup time or not
        private static bool mybot(int starthour, int startminute, int endhour, int endminute, int botnum)
        {
            if(Database.Bot[botnum].Length < 3 && Database.ID[botnum] != 0)
            {
                KillProcessAndChildren(Database.ID[botnum]);
                Database.ID[botnum] = 0;
            }
            bool running = false;
            if (Database.Bot[botnum].Length > 3 && MainScreen.Run == true)
            {
                Start[botnum] = new TimeSpan(starthour, startminute, 0);
                End[botnum] = new TimeSpan(endhour, endminute, 59);
                Now = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                Database.WriteLog("MyBot " + Database.Bot[botnum] + " Starting Time is: " + starthour + ":" + startminute + " End Time is: " + endhour + ":" + endminute);
                if (starthour == endhour && startminute == endminute)
                {
                    Database.WriteLog("The time is not setted, starting MyBot " + Database.Bot[botnum]);
                    running = InTime(botnum);
                }
                else if (Start[botnum] < End[botnum]) // If Start time is smaller than End
                {
                    Database.WriteLog("MyBot " + Database.Bot[botnum] + " starting time is smaller than ending time.");
                    try
                    {
                        if (Now >= Start[botnum] && Now <= End[botnum]) //Time must in range of start and end
                        {
                            running = InTime(botnum);
                            
                        }
                        else
                        {
                            NotIn(botnum);
                        }
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText("error.log", ex.ToString());
                    }
                }
                else // If End time is smaller than Start
                {
                    Database.WriteLog("MyBot " + Database.Bot[botnum] + " ending time is smaller than starting time.");
                    try
                    {
                        if (Now >= Start[botnum] || Now <= End[botnum]) // Time will in tha range of today's start time and tommorow's end time
                        {
                            running = InTime(botnum);
                        }
                        else
                        {
                            NotIn(botnum);
                        }
                    }
                    catch (Exception ex)
                    {
                        File.WriteAllText("error.log", ex.ToString());
                    }
                }

            }
            return running;
        }//Check Profile of MyBot is in startup time or not
        private static void NotIn(int botnum)
        {
            Database.WriteLog("MyBot " + Database.Bot[botnum] + " is not in time");
            try
            {
                if (Database.ID[botnum] > 1)
                {
                    if (TBot.cid > 0 && Database.ReportClose && TBot.Bot != null)
                    {
                        try
                        {
                            if (Database.Language == "English" == true)
                            {
                                TBot.Bot.SendTextMessageAsync(TBot.cid, "Closing MyBot " + Database.Bot[botnum] + " because of not in running time!");
                            }
                            else
                            {
                                TBot.Bot.SendTextMessageAsync(TBot.cid, "由于MyBot " + Database.Bot[botnum] + " 不在运行时间范围，正在关闭！");
                            }
                        }
                        catch
                        {

                        }
                    }
                    foreach (var Watchdog in Process.GetProcessesByName("MyBot.run.Watchdog"))
                    {
                        Watchdog.Kill();
                    }
                    try
                    {
                        KillProcessAndChildren(Database.ID[botnum]);
                    }
                    catch
                    {
                        Database.ID[botnum] = 0;
                    }
                }
            }
            catch
            {
                Database.ID[botnum] = 0;
            }
            finally
            {
                Database.ID[botnum] = 0;
                Database.hWnd[botnum] = IntPtr.Zero;
            }
        }//Current MyBot Profile is not in startup time
        private static bool InTime(int botnum)
        {
            bool running = false;
            Database.WriteLog("MyBot " + Database.Bot[botnum] + " is in time");
            if (Database.ID[botnum] < 1)
            {
                if (TBot.cid > 0 && Database.ReportOpen && TBot.Bot != null)
                {
                    try
                    {
                        if (Database.Language == "English" == true)
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, "Starting MyBot"+Database.Bot[botnum] +" because of in running time!");
                        }
                        else
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, "由于MyBot" + Database.Bot[botnum] + "在运行时间范围，正在启动！");
                        }
                    }
                    catch
                    {

                    }
                }
                running = true;
                Database.WriteLog("Starting MyBot " + Database.Bot[botnum]);
                ProcessStartInfo start = new ProcessStartInfo(bot);
                start.Arguments = Database.Bot[botnum] + " " + "-a" + " " + "-nwd" + " " + "-nbs";
                start.RedirectStandardError = false;
                start.RedirectStandardOutput = false;
                Process M = Process.Start(start);
                ReportedClosed[botnum] = false;
                Database.ID[botnum] = M.Id;
            }
            else
            {
                try
                {
                    Process M = Process.GetProcessById(Database.ID[botnum]);
                    string Title = M.MainWindowTitle;
                    IntPtr compare = Win32.FindWindow(null, Title);
                    IntPtr Main = M.MainWindowHandle;
                    if(Main != IntPtr.Zero)
                    {
                        if (Main == compare)
                        {
                            Database.hWnd[botnum] = Main;
                        }
                    }
                    //IntPtr Render = IntPtr.Zero;
                    running = true;
                    
                    List<IntPtr> MyBot_Child = GetAllChildrenWindowHandles(Database.hWnd[botnum], 8);
                    if(MyBot_Child.Count > 4)
                    {
                        IntPtr Parent = MyBot_Child[4];
                        List<IntPtr> Child = GetAllChildrenWindowHandles(Parent, 65);
                        string BotStopped = "";
                        if (Child.Count > 64)
                        {
                            BotStopped = TBot.GetWindowTextRaw(Child[64]);
                        }
                        Database.WriteLog("Current Bot Log is " + BotStopped);
                        if (BotStopped.Contains("===================== Bot Stop ======================"))
                        {
                            if (Win32.GetIdleTime() > 60000)
                            {
                                IntPtr Start = Child[1];
                                Win32.PostMessage(Start, 0x0201, 1, IntPtr.Zero);
                                Win32.PostMessage(Start, 0x0202, 0, IntPtr.Zero);
                                Database.WriteLog("MyBot had stopped, restarting it!");
                                Thread.Sleep(10000);
                            }
                            else
                            {
                                Database.WriteLog("MyBot had stopped but user is using PC");
                            }
                        }
                        else if (BotStopped.Contains("Bot was Paused!"))
                        {
                            if (Win32.GetIdleTime() > 60000)
                            {
                                IntPtr Start = Child[1];
                                Win32.PostMessage(Start, 0x0201, 1, IntPtr.Zero);
                                Win32.PostMessage(Start, 0x0202, 0, IntPtr.Zero);
                                Database.WriteLog("MyBot had paused, restarting it!");
                                Thread.Sleep(10000);
                            }
                            else
                            {
                                Database.WriteLog("MyBot had paused but user is using PC");
                            }
                            if (TBot.cid > 0 && TBot.API_Key.Length > 0)
                            {
                                if (TBot.Bot != null)
                                {
                                    if (!TBot.PauseMessageSended[botnum])
                                    {
                                        try
                                        {
                                            if (Database.Language == "English")
                                            {
                                                TBot.Bot.SendTextMessageAsync(TBot.cid, Database.Bot[botnum] + " is paused!");
                                            }
                                            else
                                            {
                                                TBot.Bot.SendTextMessageAsync(TBot.cid, Database.Bot[botnum] + " 已暂停运行！");
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        TBot.PauseMessageSended[botnum] = true;
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (TBot.PauseMessageSended[botnum])
                            {
                                TBot.PauseMessageSended[botnum] = false;
                            }
                        }
                        try
                        {
                            List<IntPtr> ChildWindows = new Win32(Database.hWnd[botnum]).GetAllChildHandles();
                            if (Child.Count > 8)
                            {

                                    if (Database.hide == true)
                                    {
                                        Win32.ShowWindow(Database.hWnd[botnum], 0);
                                    }
                                    else
                                    {
                                        Win32.ShowWindow(Database.hWnd[botnum], 5);
                                    }
                                    if (Database.dock == true)
                                    {
                                        if (MyBot_Child.Count < 7)
                                        {
                                            IntPtr Dock = Child[8];
                                            if (Dock != IntPtr.Zero)
                                            {
                                                Win32.PostMessage(Dock, 0x0201, 1, IntPtr.Zero);
                                                Win32.PostMessage(Dock, 0x0202, 0, IntPtr.Zero);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MyBot_Child.Count > 6)
                                        {
                                            IntPtr Dock = Child[8];
                                            if (Dock != IntPtr.Zero)
                                            {
                                                Win32.PostMessage(Dock, 0x0201, 1, IntPtr.Zero);
                                                Win32.PostMessage(Dock, 0x0202, 0, IntPtr.Zero);
                                            }
                                        }
                                        
                                    
                                 }
                                if (Database.hideEmulator == true)
                                {
                                    const short SWP_NOZORDER = 0X4;
                                    const int SWP_SHOWWINDOW = 0x0040;
                                    const int SWP_NOSIZE = 0x0001;
                                    foreach (var Android in Database.Emulator)
                                    {
                                        foreach (var Emulator in Process.GetProcessesByName(Android))
                                        {
                                            var Handler = Emulator.MainWindowHandle;
                                            Win32.SetWindowPos(Handler, 0, -32000, -32000, 0, 0, SWP_NOZORDER | SWP_NOSIZE | SWP_SHOWWINDOW);
                                        }
                                    }
                                }
                                Database.WriteLog("Checking Error Alert Box");
                                foreach (var child in ChildWindows)
                                {
                                    Win32.STRINGBUFFER winTitle;
                                    Win32.GetWindowText(child, out winTitle, 256);
                                    String title = winTitle.szText;
                                    if (title.Length > 10)
                                    {
                                        if (title.Contains("Fatal Error") || title.Contains("AutoIT Error"))
                                        {
                                            KillProcessAndChildren(Database.ID[botnum]);
                                            MainScreen.AutoIT++;
                                        }
                                    }

                                }
                            }
                            try
                            {
                                IntPtr dialog = Win32.FindWindow("#32770",null);
                                Win32.STRINGBUFFER wintitle;
                                Win32.GetWindowText(dialog, out wintitle, 256);
                                String title = wintitle.szText;
                                if (title.Length > 10)
                                {
                                    if (title.Contains("BroadcastEventWindow"))
                                    {
                                        Win32.SendMessage(dialog, 0x0010,0,null);
                                        MainScreen.AutoIT++;
                                    }
                                }

                            }
                            catch
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            File.WriteAllText("error,log", ex.ToString());
                        }
                        if (Refresh[botnum] == 30)
                        {
                            string goldtemp;
                            string elixirtemp;
                            string darkelixirtemp;
                            string trophytemp;
                            Database.WriteLog("Fetching Current Resources");
                            try
                            {
                                goldtemp = new string(TBot.GetWindowTextRaw(Child[38]).ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                                Database.WriteLog("Gold: " + goldtemp);
                                if (goldtemp.Length > 0)
                                {
                                    Gold[botnum] = Convert.ToInt32(goldtemp);
                                }
                                else
                                {
                                    Gold[botnum] = 0;
                                }
                            }
                            catch
                            {
                                Gold[botnum] = 0;
                            }
                            try
                            {
                                elixirtemp = new string(TBot.GetWindowTextRaw(Child[42]).ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                                Database.WriteLog("Elixir: " + elixirtemp);
                                if (elixirtemp.Length > 0)
                                {
                                    Elixir[botnum] = Convert.ToInt32(elixirtemp);
                                }
                            }
                            catch
                            {
                                Elixir[botnum] = 0;
                            }
                            try
                            {
                                darkelixirtemp = new string(TBot.GetWindowTextRaw(Child[46]).ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                                Database.WriteLog("Dark Elixir: " + darkelixirtemp);
                                if (darkelixirtemp.Length > 0)
                                {
                                    DarkElixir[botnum] = Convert.ToInt32(darkelixirtemp);
                                }
                            }
                            catch
                            {
                                DarkElixir[botnum] = 0;
                            }
                            try
                            {
                                trophytemp = new string(TBot.GetWindowTextRaw(Child[50]).ToArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
                                Database.WriteLog("Trophy: " + trophytemp);
                                if (trophytemp.Length > 0)
                                {
                                    Trophy[botnum] = Convert.ToInt32(trophytemp);
                                }
                            }
                            catch
                            {
                                Trophy[botnum] = 0;
                            }
                            Refresh[botnum] = 0;
                        }
                        else
                        {
                            Database.WriteLog("Waiting for refresh");
                            Refresh[botnum]++;
                        }
                    }
                    else
                    {
                        Database.hWnd[botnum] = IntPtr.Zero;
                    }
                    foreach (var Watchdog in Process.GetProcessesByName("MyBot.run.Watchdog"))
                    {
                        Watchdog.Kill();
                    }
                }
                catch (ArgumentException)
                {
                    if (ReportedClosed[botnum] == false)
                    {
                        if (TBot.cid > 0 && Database.ReportClose && TBot.Bot != null)
                        {
                            try
                            {
                                if (Database.Language == "English" == true)
                                {
                                    TBot.Bot.SendTextMessageAsync(TBot.cid, "MyBot " + Database.Bot[botnum] + " is closed by non Supporter!");
                                }
                                else
                                {
                                    TBot.Bot.SendTextMessageAsync(TBot.cid, "MyBot " + Database.Bot[botnum] + " 被管理器以外的程序关闭！");
                                }
                            }
                            catch
                            {

                            }
                            ReportedClosed[botnum] = true;
                        }
                    }
                    Database.WriteLog("MyBot " + Database.Bot[botnum] + " is in time but not running");
                    if (Database.Bot_Timer[94] == 108 || Error[botnum] == true)
                    {
                        Database.WriteLog("Restarting MyBot " + Database.Bot[botnum]);
                        Main.Database.ID[botnum] = 0;
                        Error[botnum] = false;
                    }
                    var html = Directory.GetFiles(Environment.CurrentDirectory + "\\lib\\");
                    foreach (var h in html)
                    {
                        if (h.Split('.').Last().Contains("html"))
                        {
                            foreach (var mb in Process.GetProcessesByName("MyBot.run"))
                            {
                                if (!Database.ID.Contains(mb.Id))
                                {
                                    Database.WriteLog("Update MyBot ID after Illegal Use error");
                                    Database.ID[botnum] = mb.Id;
                                    File.Delete(h);
                                    ImgLoc++;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    Database.hWnd[botnum] = IntPtr.Zero;
                }
                catch(Exception ex)
                {
                    File.WriteAllText("error.log", ex.ToString());
                }
            }
            return running;
        }//Current MyBot Profile is in startup time
        private static void OInTime(int X)
        {

            if (Database.OtherNet[X] == 1)
            {
                Database.WriteLog("Other Program " + Database.OtherBot[X] + " is in time");
                if (Database.Network == true)
                {
                    if (Database.OtherID[X] < 1)
                    {
                        string[] splited = Database.OtherBot[X].Split(new string[] { " -" }, StringSplitOptions.None);
                        ProcessStartInfo start = new ProcessStartInfo();
                        start.FileName = splited[0];
                        if (splited.Length > 1)
                        {
                            string arguments = "";
                            int y = 1;
                            for(y = 1; y < splited.Length; y++)
                            {
                                    arguments += "-" + splited[y];
                            }
                            start.Arguments = arguments;
                        }
                        Process M = Process.Start(start);
                        Database.OtherID[X] = M.Id;
                    }
                }
                else
                {
                    if (Database.OtherID[X] > 0)
                    {
                        try
                        {
                            KillProcessAndChildren(Database.OtherID[X]);
                            Database.OtherID[X] = 0;
                        }
                        catch
                        {
                            Database.OtherID[X] = 0;
                        }
                    }
                }
            }
            else
            {
                if (Database.OtherID[X] < 1)
                {
                    Process M = Process.Start(Main.Database.OtherBot[X]);
                    Database.OtherID[X] = M.Id;
                }
            }
        }//Current autorun program is in startup time
        private static void ONotIn(int X)
        {
            Database.WriteLog("Other Program " + Database.OtherBot[X] + " is not in time");
            if (Main.Database.OtherID[X] > 0)
            {
                try
                {
                    KillProcessAndChildren(Database.OtherID[X]);
                    Database.OtherID[X] = 0;
                }
                catch
                {
                    Database.OtherID[X] = 0;
                }
            }
        }//Current autorun program is not in startup time
        public static List<IntPtr> GetAllChildrenWindowHandles(IntPtr hParent, int maxCount)
        {
                Database.WriteLog("Getting Childs Handlers");
                List<IntPtr> result = new List<IntPtr>();
                int ct = 0;
                IntPtr prevChild = IntPtr.Zero;
                IntPtr currChild = IntPtr.Zero;
                while (true && ct < maxCount)
                {
                    currChild = Win32.FindWindowEx(hParent, prevChild, null, null);
                    if (currChild == IntPtr.Zero) break;
                    result.Add(currChild);
                    prevChild = currChild;
                    ++ct;
                }
                return result;
        }//WinAPI get Child Handles func
        public static void KillProcessAndChildren(int pid)//Kill Proceess Tree
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

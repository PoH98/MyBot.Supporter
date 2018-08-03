using MyBot.Supporter.Main;
using System.Drawing.Imaging;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MyBot.Supporter.Main
{
    public class TBot
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, [Out] StringBuilder lParam);
        public static string API_Key = "";
        public static string command = "";
        public static string respond = "";
        public static string runningcheckrespond = "";
        public static bool CompletedResponding, Schedule_Respond;
        public static TelegramBotClient Bot;
        public static TelegramBotClient debugger = new TelegramBotClient("566861023:AAFutO_Oj5mpXuCA1O2zHOex0JLB1CtIx10");
        static Thread removebug = new Thread(Network_Bug_Fixer);
        public static long cid;
        public static string Socks5ServerAddress = "127.0.0.1";
        public static int Socks5ServerPort = 1080;
        public static bool[] PauseMessageSended;
        static int loop = 0;
        static string GoldText, ElixirText, DEText, TrophyText, BuildersText, GoldPHText, ElixirPHText, DEPHText, TrophyPHText;
        public static int Time;
        public static async void DebugBot(string log)
        {
            if(debugger != null)
            {
                FileStream fileStream = null;
                while (fileStream == null)
                {
                    try
                    {
                        fileStream = new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }
                    catch
                    {
                        fileStream = null;
                    }
                }
                try
                {
                    await debugger.SendDocumentAsync(288027359, fileStream);
                }
                catch
                {

                }
                fileStream.Close();
            }
            CompletedResponding = true;
        }
        private static void Network_Bug_Fixer()
        {
            while (MainScreen.Supporter)
            {
                if (!Database.Network)
                {
                    if (Bot != null)
                    {
                        if (Bot.IsReceiving)
                        {
                            Bot.StopReceiving();
                        }
                    }

                }
                else
                {
                    if (Bot != null)
                    {
                        if (!Bot.IsReceiving && API_Key.Length > 0)
                        {
                            try
                            {
                                Bot.StartReceiving();
                            }
                            catch
                            {
                                API_Key = "";
                                Bot.StopReceiving();
                                Bot = null;
                            }

                        }
                    }

                }
                Thread.Sleep(5000);
            }
        }
        public static async void BotMessageThreadStart()
        {
            var proxy = new HttpToSocks5Proxy(Socks5ServerAddress, Socks5ServerPort);
            proxy.ResolveHostnamesLocally = true;
            if (removebug.ThreadState == System.Threading.ThreadState.Unstarted)
            {
                removebug.Start(); //To prevent cpu consumation in 404 error for telegram bot while no network found
            }
            try
            {
                await debugger.SendTextMessageAsync(288027359, "Debug bot started");
                if (API_Key != "")
                {
                    try
                    {
                        Bot = new TelegramBotClient(API_Key);
                    }
                    catch (ArgumentException)
                    {
                        switch (Database.Language)
                        {
                            case "English":
                                MessageBox.Show("Invalid token!");
                                break;
                            case "Chinese":
                                MessageBox.Show(cn_Lang.InvalidToken, cn_Lang.Error);
                                break;
                        }
                        return;
                    }
                    Bot.OnMessage += Bot_OnMessage;
                    Bot.StartReceiving();
                    while (MainScreen.Supporter)
                    {
                        Thread.Sleep(10000);
                        if (Schedule_Respond)
                        {
                            if (loop == Time && cid != 0)
                            {
                                command = "runningcheck";
                                MainScreen.RunningCheckCompleted = false;
                                while (MainScreen.RunningCheckCompleted == false)
                                {
                                    Thread.Sleep(1000);
                                }
                                if (runningcheckrespond.Length > 0)
                                {
                                    try
                                    {
                                        if (runningcheckrespond.Length > 0)
                                        {
                                            await Bot.SendTextMessageAsync(cid, runningcheckrespond);
                                            loop = 0;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Schedule respond reached NULL Error!");
                                        loop = 0;
                                    }
                                    catch
                                    {

                                    }
                                }
                                runningcheckrespond = "";
                                respond = "";
                            }
                            else
                            {
                                loop++;
                            }
                        }
                    }
                }
            }
            catch
            {
                Bot = null;
                debugger = null;
                Database.WriteLog("Unable to connect to Telegram, ignore bot startup");
            }
        }
        public static void BotMessageThreadStop()
        {
            if (Bot != null)
            {
                if (Bot.IsReceiving)
                {
                    Bot.StopReceiving();
                }
            }
            if (MainScreen.Supporter == false)
            {
                Environment.Exit(0);
            }
        }
        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (cid != 0)
            {
                try
                {
                    if (e.Message.Type == MessageType.Text)
                    {
                        Database.WriteLog("Received " + e.Message.Text);
                        switch (e.Message.Text)
                        {
                            case "/start":
                                try
                                {
                                    switch (Database.Language)
                                    {
                                        case "English":
                                            await Bot.SendTextMessageAsync(cid, "Welcome using MyBot.Supporter.Main! \n Command Help: \n /s is for starting and stopping bots \n /en is changing language to Database.English \n /cn is changing language to chinese \n /cpu is for sending CPU informations \n /list is to shows the bot list and their starting & ending time" +
                                           "\n /h is hide all emulators and bots \n /sh shows all hidden MyBot and emulators \n /capt is capturing screenshots \n /sd is shutdown PC \n /re is restart PC \n /p to pause/resume all running MyBots");
                                            break;
                                        case "Chinese":
                                            await Bot.SendTextMessageAsync(cid, cn_Lang.TelegramHelpMessage);
                                            break;
                                    }
                                }
                                catch
                                {

                                }
                                respond = "";
                                break;
                            case "/s":
                                if (MainScreen.Run == true)
                                {
                                    try
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Stopping Bots!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "正在停止全部Bot!");
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Starting Bots!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "正在开始运行全部Bot!");
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                                command = "s";
                                break;
                            case "/en":
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "You are current using English! ");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Changing Language to English! ");
                                        command = "en";
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case "/cn":
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "正在切换中文！");

                                        command = "cn";
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "您已经在使用中文！");
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case "/cpu":
                                command = "cpu";
                                CompletedResponding = false;
                                while (CompletedResponding == false)
                                {
                                    Thread.Sleep(1000);
                                }
                                try
                                {
                                    if (respond.Length > 0)
                                    {
                                        await Bot.SendTextMessageAsync(cid, respond);
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "CPU get message failed!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "CPU资料获取失败！");
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                respond = "";
                                break;
                            case "/list":
                                Thread list = new Thread(List);
                                list.Start();
                                while (list.ThreadState == System.Threading.ThreadState.Running)
                                {
                                    Thread.Sleep(1000);
                                }
                                try
                                {
                                    if (respond.Length > 0)
                                    {
                                        await Bot.SendTextMessageAsync(cid, respond);
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Getting list failed!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "获取List失败！");
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                respond = "";
                                break;
                            case "/h":
                                command = "h";
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Hided! ");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "已隐藏! ");
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case "/sh":
                                command = "sh";
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Showed! ");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "已显示! ");
                                    }
                                }
                                catch
                                {

                                }
                                break;
                            case "/capt":
                                CaptureMyScreen();
                                try
                                {
                                    if (File.Exists("Capture.jpg"))
                                    {
                                        await Bot.SendChatActionAsync(cid, ChatAction.UploadPhoto);
                                        const string file = @"Capture.jpg";
                                        var fileName = file.Split(Path.DirectorySeparatorChar).Last();
                                        using (var fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                                        {
                                            await Bot.SendPhotoAsync(cid, fileStream, "");
                                        }
                                        File.Delete(@"Capture.jpg");
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Capture failed! ");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "截图失败! ");
                                        }
                                    }
                                }
                                catch
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Capture failed! ");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "截图失败! ");
                                    }
                                }
                                break;
                            case "/status":
                                Thread status = new Thread(Status);
                                status.Start();
                                while (status.ThreadState == System.Threading.ThreadState.Running)
                                {
                                    Thread.Sleep(1000);
                                }
                                try
                                {
                                    if (respond.Length > 0)
                                    {
                                        await Bot.SendTextMessageAsync(cid, respond);
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Unable to get status!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "截取MyBot字体失败! ");
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                respond = "";
                                break;
                            case "/earn":
                                Thread earn = new Thread(Earn);
                                earn.Start();
                                while (earn.ThreadState == System.Threading.ThreadState.Running)
                                {
                                    Thread.Sleep(1000);
                                }
                                try
                                {
                                    if (respond.Length > 0)
                                    {
                                        await Bot.SendTextMessageAsync(cid, respond);
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            await Bot.SendTextMessageAsync(cid, "Unable to get earn!");
                                        }
                                        else
                                        {
                                            await Bot.SendTextMessageAsync(cid, "截取MyBot字体失败! ");
                                        }
                                    }
                                }
                                catch
                                {

                                }
                                respond = "";
                                break;
                            case "/sd":
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Shutdown received!");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "正在关机！");
                                    }
                                }
                                catch
                                {

                                }
                                Process.Start("shutdown.exe", "/s /t 00");
                                break;

                            case "/re":
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "Restart received!");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "正在重启！");
                                    }
                                }
                                catch
                                {

                                }
                                Process.Start("shutdown.exe", "/r /t 00");
                                break;
                            case "/p":
                                try
                                {
                                    command = "p";

                                }
                                catch
                                {

                                }
                                break;
                            default:
                                try
                                {
                                    if (Database.Language == "English")
                                    {
                                        await Bot.SendTextMessageAsync(cid, "I don't understand what is " + e.Message.Text + "！");
                                    }
                                    else
                                    {
                                        await Bot.SendTextMessageAsync(cid, "我不明白你说的" + e.Message.Text + "是什么意思！");
                                    }
                                }
                                catch
                                {

                                }
                                break;
                        }
                    }
                    else
                    {
                        try
                        {
                            if (Database.Language == "English")
                            {
                                await Bot.SendTextMessageAsync(cid, "Starting.....Wait!! What are you sending to me?!");
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(cid, "正在开始……等等！你是在发什么给我？！");
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText("error.log", ex.ToString());
                }
            }
            else
            {
                cid = e.Message.Chat.Id;
            }
            
        }
        public static void List()
        {
            try
            {
                int x = 0;
                string buffer = "Listing: \n";
                List<string> starttime = new List<string>(21);
                starttime.Add(mybot(Database.Bot_Timer[0], Database.Bot_Timer[42], Database.Bot_Timer[1], Database.Bot_Timer[63]));
                starttime.Add(mybot(Database.Bot_Timer[2], Database.Bot_Timer[43], Database.Bot_Timer[3], Database.Bot_Timer[64]));
                starttime.Add(mybot(Database.Bot_Timer[4], Database.Bot_Timer[44], Database.Bot_Timer[5], Database.Bot_Timer[65]));
                starttime.Add(mybot(Database.Bot_Timer[6], Database.Bot_Timer[45], Database.Bot_Timer[7], Database.Bot_Timer[66]));
                starttime.Add(mybot(Database.Bot_Timer[8], Database.Bot_Timer[46], Database.Bot_Timer[9], Database.Bot_Timer[67]));
                starttime.Add(mybot(Database.Bot_Timer[10], Database.Bot_Timer[47], Database.Bot_Timer[11], Database.Bot_Timer[68]));
                starttime.Add(mybot(Database.Bot_Timer[12], Database.Bot_Timer[48], Database.Bot_Timer[13], Database.Bot_Timer[69]));
                starttime.Add(mybot(Database.Bot_Timer[14], Database.Bot_Timer[49], Database.Bot_Timer[15], Database.Bot_Timer[70]));
                starttime.Add(mybot(Database.Bot_Timer[16], Database.Bot_Timer[50], Database.Bot_Timer[17], Database.Bot_Timer[71]));
                starttime.Add(mybot(Database.Bot_Timer[18], Database.Bot_Timer[51], Database.Bot_Timer[19], Database.Bot_Timer[72]));
                starttime.Add(mybot(Database.Bot_Timer[20], Database.Bot_Timer[52], Database.Bot_Timer[21], Database.Bot_Timer[73]));
                starttime.Add(mybot(Database.Bot_Timer[22], Database.Bot_Timer[53], Database.Bot_Timer[23], Database.Bot_Timer[74]));
                starttime.Add(mybot(Database.Bot_Timer[24], Database.Bot_Timer[54], Database.Bot_Timer[25], Database.Bot_Timer[75]));
                starttime.Add(mybot(Database.Bot_Timer[26], Database.Bot_Timer[55], Database.Bot_Timer[27], Database.Bot_Timer[76]));
                starttime.Add(mybot(Database.Bot_Timer[28], Database.Bot_Timer[56], Database.Bot_Timer[29], Database.Bot_Timer[77]));
                starttime.Add(mybot(Database.Bot_Timer[30], Database.Bot_Timer[57], Database.Bot_Timer[31], Database.Bot_Timer[78]));
                starttime.Add(mybot(Database.Bot_Timer[32], Database.Bot_Timer[58], Database.Bot_Timer[33], Database.Bot_Timer[79]));
                starttime.Add(mybot(Database.Bot_Timer[34], Database.Bot_Timer[59], Database.Bot_Timer[35], Database.Bot_Timer[80]));
                starttime.Add(mybot(Database.Bot_Timer[36], Database.Bot_Timer[60], Database.Bot_Timer[37], Database.Bot_Timer[81]));
                starttime.Add(mybot(Database.Bot_Timer[38], Database.Bot_Timer[61], Database.Bot_Timer[39], Database.Bot_Timer[82]));
                starttime.Add(mybot(Database.Bot_Timer[40], Database.Bot_Timer[62], Database.Bot_Timer[41], Database.Bot_Timer[83]));
                foreach (var bot in Database.Bot)
                {
                    if (bot.Length > 3 && x <= 20)
                    {
                        buffer = buffer + "\n" + x + ". " + bot + ": " + starttime[x];
                    }
                    x++;
                }
                respond = buffer;
                CompletedResponding = true;
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
                if (Database.Language == "English")
                {
                    respond = "Error found when proccessing command, please try again later. \n" + ex.ToString();
                }
                else
                {
                    respond = "程序处理出现错误，请稍后尝试。\n" + ex.ToString();
                }
                CompletedResponding = true;
            }
        }
        public static void Status()
        {
            Array.Resize(ref Botting.Refresh, 21);
            for (int i = 0; i < Botting.Refresh.Length; i++)
            {
                Botting.Refresh[i] = 30;
            }
            if (Database.Language == "Chinese")
            {
                GoldText = cn_Lang.Gold;
                ElixirText = cn_Lang.Elixir;
                DEText = cn_Lang.DarkElixir;
                TrophyText = cn_Lang.Trophy;
                BuildersText = cn_Lang.Builders;
            }
            else
            {
                GoldText = en_Lang.Gold;
                ElixirText = en_Lang.Elixir;
                DEText = en_Lang.DarkElixir;
                TrophyText = en_Lang.Trophy;
                BuildersText = en_Lang.Builders;
            }
            if (Database.hWnd.All(o => o == IntPtr.Zero))
            {
                if (Database.Language == "English")
                {
                    TBot.respond = "No Running Bot found!";
                }
                else
                {
                    TBot.respond = "没有正在运行的MyBot!";
                }
            }
            else
            {
                try
                {
                    
                    for(int x = 0;x<21;x++)
                    {
                        if (Database.hWnd[x] != IntPtr.Zero)
                        {
                            IntPtr GUI = Win32.FindWindowEx(Database.hWnd[x], IntPtr.Zero, null, "My Bot Buttons");
                            List<IntPtr> gettext = Botting.GetAllChildrenWindowHandles(GUI, 55);
                            foreach (var l in gettext)
                            {
                                Database.WriteLog(GetWindowTextRaw(l));
                            }
                            string gold = Botting.Gold[x].ToString("## ### ##0");// + " / " + Botting.MaxStorage[Botting.Townhall[x]].ToString("## ### ##0");
                            string elixir = Botting.Elixir[x].ToString("## ### ##0");// + " / " + Botting.MaxStorage[Botting.Townhall[x]].ToString("## ### ##0");
                            string dark = Botting.DarkElixir[x].ToString("### ##0");// + " / " + Botting.MaxDE[Botting.Townhall[x]].ToString("### ##0");
                            string trophy = Botting.Trophy[x].ToString("# ##0");
                            TBot.respond = TBot.respond + GetWindowTextRaw(gettext[37]) + "\n";
                            TBot.respond = TBot.respond + GoldText + gold + "\n";
                            TBot.respond = TBot.respond + ElixirText + elixir + "\n";
                            TBot.respond = TBot.respond + DEText + dark + "\n";
                            TBot.respond = TBot.respond + TrophyText + trophy + "\n";
                            TBot.respond = TBot.respond + BuildersText + GetWindowTextRaw(gettext[54]) + "\n";
                            TBot.respond = TBot.respond + "\n---------------------------\n";
                        }
                    }
                    CompletedResponding = true;
                }
                catch (Exception ex)
                {
                    if (Database.Language == "English")
                    {
                        respond = "Error found when proccessing command, please try again later. \n" + ex.ToString();
                    }
                    else
                    {
                        respond = "程序处理出现错误，请稍后尝试。\n" + ex.ToString();
                    }
                    CompletedResponding = true;
                }
            }
        }
        public static void Earn()
        {

            if (Database.Language == "English")
            {
                GoldText = en_Lang.EarnedGold;
                ElixirText = en_Lang.EarnedElixir;
                DEText = en_Lang.EarnedDarkElixir;
                TrophyText = en_Lang.EarnedTrophy;
                GoldPHText = en_Lang.EarnedGoldPH;
                ElixirPHText = en_Lang.EarnedElixirPH;
                DEPHText = en_Lang.EarnedDarkElixirPH;
                TrophyPHText = en_Lang.EarnedTrophyPH;
            }
            else
            {
                GoldText = cn_Lang.EarnedGold;
                ElixirText = cn_Lang.EarnedElixir;
                DEText = cn_Lang.EarnedDarkElixir;
                TrophyText = cn_Lang.EarnedTrophy;
                GoldPHText = cn_Lang.EarnedGoldPH;
                ElixirPHText = cn_Lang.EarnedElixirPH;
                DEPHText = cn_Lang.EarnedDarkElixirPH;
                TrophyPHText = cn_Lang.EarnedTrophyPH;
            }
            if (Database.hWnd.All(o => o == IntPtr.Zero))
            {
                if (Database.Language == "English")
                {
                    TBot.respond = "No Running Bot found!";
                }
                else
                {
                    TBot.respond = "没有正在运行的MyBot!";
                }
            }
            else
            {
                try
                {
                    foreach (var hWnd in Database.hWnd)
                    {
                        if (hWnd != IntPtr.Zero)
                        {
                            IntPtr GUI = Win32.FindWindowEx(hWnd, IntPtr.Zero, null, "My Bot Buttons");
                            IntPtr Profile = Botting.GetAllChildrenWindowHandles(GUI, 38)[37];
                            TBot.respond = TBot.respond + GetWindowTextRaw(Profile) + "\n";
                            IntPtr Control = Win32.FindWindowEx(hWnd, IntPtr.Zero, null, "My Bot Controls");
                            if (Control != IntPtr.Zero)
                            {
                                IntPtr Control1 = Botting.GetAllChildrenWindowHandles(Control, 9)[8];
                                if (Control1 != IntPtr.Zero)
                                {
                                    IntPtr Control2 = Botting.GetAllChildrenWindowHandles(Control1, 2)[1];
                                    if (Control2 != IntPtr.Zero)
                                    {
                                        List<IntPtr> gettext = Botting.GetAllChildrenWindowHandles(Control2, 64);
                                        if (gettext.Count >= 63)
                                        {
                                            respond += GoldText + GetWindowTextRaw(gettext[57]) + "\n";
                                            respond += ElixirText + GetWindowTextRaw(gettext[59]) + "\n";
                                            respond += DEText + GetWindowTextRaw(gettext[61]) + "\n";
                                            respond += TrophyText + GetWindowTextRaw(gettext[63]) + "\n";
                                            respond += GoldPHText + GetWindowTextRaw(gettext[46]) + "\n";
                                            respond += ElixirPHText + GetWindowTextRaw(gettext[48]) + "\n";
                                            respond += DEPHText + GetWindowTextRaw(gettext[50]) + "\n";
                                            respond += TrophyPHText + GetWindowTextRaw(gettext[52]) + "\n";
                                            respond += "\n---------------------------\n";
                                        }
                                        foreach (var l in gettext)
                                        {
                                            Database.WriteLog(GetWindowTextRaw(l));
                                        }
                                    }
                                    else
                                    {
                                        if (Database.Language == "English")
                                        {
                                            TBot.respond += "Unable to locate text! \n";
                                        }
                                        else
                                        {
                                            TBot.respond += "找字失败！ \n";
                                        }
                                    }
                                }
                                else
                                {
                                    if (Database.Language == "English")
                                    {
                                        TBot.respond += "Unable to locate text! \n";
                                    }
                                    else
                                    {
                                        TBot.respond += "找字失败！ \n";
                                    }
                                }
                            }
                            else
                            {
                                if (Database.Language == "English")
                                {
                                    TBot.respond += "Unable to locate text! \n";
                                }
                                else
                                {
                                    TBot.respond += "找字失败！ \n";
                                }
                            }
                        }
                    }
                    CompletedResponding = true;
                }
                catch (Exception ex)
                {
                    if (Database.Language == "English")
                    {
                        respond = "Error found when proccessing command, please try again later. \n" + ex.ToString();
                    }
                    else
                    {
                        respond = "程序处理出现错误，请稍后尝试。\n" + ex.ToString();
                    }
                    CompletedResponding = true;
                }
                
            }
        }
        public static void Pause()
        {
            foreach (var bot in Database.hWnd)
            {
                IntPtr Parent = Win32.FindWindowEx(bot, IntPtr.Zero, null, "My Bot Buttons");
                Win32.PostMessage(Botting.GetAllChildrenWindowHandles(Parent, 4)[3], 0x0201, 1, IntPtr.Zero);
                Win32.PostMessage(Botting.GetAllChildrenWindowHandles(Parent, 4)[3], 0x0202, 0, IntPtr.Zero);
            }
        }

        #region OtherFunc
        private static string mybot(int starthour, int startminute, int endhour, int endminute)
        {
            return starthour.ToString("00") + "" + startminute.ToString("00") + " - " + endhour.ToString("00") + "" + endminute.ToString("00");
        }
        public static string GetWindowTextRaw(IntPtr hwnd)
        {
            // Allocate correct string length first
            int length = (int)SendMessage(hwnd, 0x000E, IntPtr.Zero, null);
            StringBuilder sb = new StringBuilder(length + 1);
            SendMessage(hwnd, 0x000D, (IntPtr)sb.Capacity, sb);
            return sb.ToString();
        }
        public static void CaptureMyScreen()
        {
            try
            {
                //Creating a new Bitmap object
                Bitmap captureBitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Rectangle captureRectangle = Screen.PrimaryScreen.Bounds;
                //Creating a New Graphics Object
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                //Copying Image from The Screen
                captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                captureBitmap.Save(@"Capture.jpg", ImageFormat.Jpeg);
            }
            catch
            {
                if (File.Exists(@"Capture.jpg"))
                {
                    File.Delete(@"Capture.jpg");
                }
            }
        }
        #endregion
    }
}

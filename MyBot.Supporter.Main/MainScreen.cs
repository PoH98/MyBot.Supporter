using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Linq;
using System.Net.NetworkInformation;
using System.Drawing;
using System.Management;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using OpenHardwareMonitor.Hardware;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Reflection;
using System.Net.Http;

namespace MyBot.Supporter.Main
{
    //Main form window
    public partial class MainScreen : Form
    {
        private static int CPUOverheat = 0;
        static List<string> ExceptionProcessName = new List<string>();
        static NetworkInterface nics;
        static PerformanceCounter CPU = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);
        static Thread telegram = new Thread(TBot.BotMessageThreadStart);
        static Computer computer = new Computer();
        static Thread send = new Thread(ReportError);
        private int size;
        static List<string> TreeNode = new List<string>();
        public static bool RunningCheckCompleted, Run, Supporter, AutoSet, Advance, ResetUI,ChangeUsingTemp, Update, UpdateMB;
        private static string CPUN;
        private static string[] AdvanceCPU = { };
        private static double CPUTM, CPUFM, CPUF, CPUV, CPUT;
        private static int CPUL, RAM, RefreshTreeNodes, AutoIT, NoNet;
        private static string SelectedCPUV, SelectedCPUF, SelectedCPUT, SelectedRAML, SelectedCPUL;
        protected static string version;
        private static WebProxy myProxy = new WebProxy();

        public MainScreen()
        {
            InitializeComponent();
        }
        private void RunningCheck()//Telegram schedulle respond function handler
        {
            if (checkBox32.Checked == true)
            {
                int runningbotcount = Process.GetProcessesByName("mybot.run").Length;
                try
                {
                    if (Database.Language == "English")
                    {
                        TBot.runningcheckrespond += "\n" + "Current running bot count: " + runningbotcount;
                    }
                    else
                    {
                        TBot.runningcheckrespond += "\n" + "正在运行的Bot数量: " + runningbotcount;
                    }
                }
                catch
                {
                    if (Database.Language == "English")
                    {
                        TBot.runningcheckrespond += "\nGetting current running bot failed!";
                    }
                    else
                    {
                        TBot.runningcheckrespond += "\n获取正在运行的Bot数量失败！";
                    }
                }
            }
            if (checkBox33.Checked == true)
            {
                try
                {
                    TBot.runningcheckrespond += "\n---------------------------\n";
                    TBot.CompletedResponding = false;
                    TBot.Status();
                    TBot.runningcheckrespond += "\n" + TBot.respond;
                    TBot.respond = "";
                }
                catch
                {
                    if (Database.Language == "English")
                    {
                        TBot.runningcheckrespond += "\nOCR read text failed!";
                    }
                    else
                    {
                        TBot.runningcheckrespond += "\nMyBot找字失败！";
                    }
                }

            }
            if (checkBox34.Checked == true)
            {
                try
                {
                    TBot.runningcheckrespond += "\n---------------------------\n";
                    TBot.CompletedResponding = false;
                    TBot.Earn();
                    while (TBot.CompletedResponding == false)
                    {
                        Thread.Sleep(1000);
                    }
                    TBot.runningcheckrespond += "\n" + TBot.respond;
                    TBot.respond = "";
                }
                catch
                {
                    if (Database.Language == "English")
                    {
                        TBot.runningcheckrespond += "\nOCR read text failed!";
                    }
                    else
                    {
                        TBot.runningcheckrespond += "\nMyBot找字失败！";
                    }
                }
            }
            if (checkBox35.Checked == true)
            {
                try
                {
                    TBot.runningcheckrespond += "\n---------------------------\n";
                    switch (Database.Language)
                    {
                        case "Chinese":
                            TBot.runningcheckrespond += "\n" + cn_Lang.CPUName+": "+ CPUN + " \n" + cn_Lang.CPUTemp + ": " + CPUT + "°C \n" + cn_Lang.CPUPower + ": " + CPUV.ToString("0.00") + "W \n" + cn_Lang.CPUFrequency + ": " + CPUF.ToString("0.00") + "Ghz \n" + cn_Lang.CPUMaxTemp + ": " + CPUTM + "°C";
                            break;
                        case "English":
                            TBot.runningcheckrespond += "\n" + en_Lang.CPUName + ": " + CPUN + " \n" + en_Lang.CPUTemp + ": " + CPUT + "°C \n" + en_Lang.CPUPower + ": " + CPUV.ToString("0.00") + "W \n" + en_Lang.CPUFrequency + ": " + CPUF.ToString("0.00") + "Ghz \n" + en_Lang.CPUMaxTemp + ": " + CPUTM + "°C";
                            break;
                    }
                }
                catch
                {
                    if (Database.Language == "English")
                    {
                        TBot.runningcheckrespond += "\nCPU status get failed!";
                    }
                    else
                    {
                        TBot.runningcheckrespond += "\nCPU资料获取失败！";
                    }
                }
            }
            RunningCheckCompleted = true;
        }
        private void button1_Click(object sender, EventArgs e)//Start bot or stop bot
        {
            if (!Run)
            {
                Database.loadingprocess = 0;
                Thread loading = new Thread(Database.Load_);
                if (TBot.cid > 0 && checkBox38.Checked && TBot.Bot != null)
                {
                    try
                    {
                        if (Database.Language == "English")
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, "Supporter is starting");
                        }
                        else
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, "管理器正在开始挂机");
                        }
                    }
                    catch
                    {

                    }
                }
                Database.WriteLog("Disable Button");
                Array.Resize(ref Botting.Start, 50);
                Array.Resize(ref Botting.End, 50);
                Array.Resize(ref Botting.ReportedClosed, 15);
                Array.Resize(ref Botting.Gold, 15);
                Array.Resize(ref Botting.Elixir, 15);
                Array.Resize(ref Botting.DarkElixir, 15);
                Array.Resize(ref Botting.Refresh, 15);
                Array.Resize(ref Botting.IsRunning, 15);
                Array.Resize(ref Botting.Trophy, 15);
                loading.Start();
                button1.Enabled = false;
                button2.Enabled = false;
                button20.Enabled = false;
                Database.Disabletab(panel1, false);
                Database.Disabletab(panel2, false);
                button25.Enabled = false;
                Database.Disabletab(panel4, false);
                startBottingToolStripMenuItem.Visible = false;
                stopBottingToolStripMenuItem.Visible = true;
                WriteAllSettings();
                Database.WriteLog("Fetching Screen Size");
                int H = Screen.FromControl(this).Bounds.Height;
                int W = Screen.FromControl(this).Bounds.Width;
                Database.WriteLog("Fetch completed. Screen Solution is " + H + "X" + W);
                if (H < 800 || W < 1400)
                {
                    Win32.HideTaskBar(0);
                    if (Database.Language == "English")
                    {
                        MessageBox.Show("Hide TaskBar Enabled for screen that below 1400*800 !!");
                    }
                    else
                    {
                        MessageBox.Show("屏幕分辨率不足1400*800，自动隐藏任务栏!!");
                    }
                }
                Database.WriteLog("Converting Settings");
                try
                {
                    Database.Bot_Timer = Array.ConvertAll(Database.Time, int.Parse);
                }
                catch (FormatException)
                {
                    int x = 0;
                    foreach (var t in Database.Time)
                    {
                        if (t == "")
                        {
                            Database.Time[x] = "0";
                        }
                        x++;
                    }
                    SetTextBox();
                    Database.Bot_Timer = Array.ConvertAll(Database.Time, int.Parse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.HResult.ToString(), en_Lang.Error);
                    File.WriteAllText("error.log", ex.ToString());
                    return;
                }
                Database.WriteLog("Writing Settings");
                File.WriteAllLines(Database.Location + "botsave", Database.Bot);
                File.WriteAllLines(Database.Location + "timesave", Database.Time);
                CheckOtherProgram();
                File.WriteAllLines(Database.Location + "programsave", Database.OtherBot);
                File.WriteAllLines(Database.Location + "programtimesave", Database.ProTime);
                Database.loadingprocess = 80;
                Database.WriteLog("Starting Setup");
                ExtraParameters_Setup();
                Run = true;
                Database.WriteLog("Finding Profile Exists");
                if (!Emulator.Emulator_Exists())
                {
                    button1_Click(sender, e);
                }
                panel3.Enabled = true;
                Database.WriteLog("Starting Main Worker");
                Thread m = new Thread(MainWorker);
                m.Start();
                Database.WriteLog("Starting Main Timer");
                timer2.Start();
                Database.loadingprocess = 100;
                switch (Database.Language)
                {
                    case "Chinese":
                        button1.Text = cn_Lang.Stop_Button;
                        break;
                    case "English":
                        button1.Text = en_Lang.Stop_Button;
                        break;
                }
                button1.BackColor = Color.Red;
                button1.Enabled = true;
                GC.Collect();
            }
            else
            {
                if (TBot.cid > 0 && checkBox38.Checked && TBot.Bot != null)
                {
                    try
                    {
                        if (Database.Language == "English")
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, "Supporter is stopping");
                        }
                        else
                        {
                            TBot.Bot.SendTextMessageAsync(TBot.cid, cn_Lang.TelegramSupporterStop);
                        }
                    }
                    catch
                    {

                    }
                }
                button1.Enabled = false;
                button2.Enabled = true;
                Database.WriteLog("Redrawing UI");
                button25.Enabled = true;
                Database.loadingprocess = 90;
                Thread loading = new Thread(Database.Load_);
                panel3.Enabled = false;
                Run = false;
                Database.WriteLog("Enabling Controls");
                Database.Disabletab(panel1, true);
                Database.Disabletab(panel2, true);
                Database.Disabletab(panel4, true);
                button20.Enabled = true;
                startBottingToolStripMenuItem.Visible = true;
                stopBottingToolStripMenuItem.Visible = false;
                Win32.HideTaskBar(1);
                Array.Clear(Database.ID, 0, 21);
                Database.WriteLog("Cleaning ID");
                textBox27.BackColor = Color.White;
                textBox26.BackColor = Color.White;
                textBox25.BackColor = Color.White;
                textBox24.BackColor = Color.White;
                textBox23.BackColor = Color.White;
                textBox22.BackColor = Color.White;
                textBox21.BackColor = Color.White;
                textBox19.BackColor = Color.White;
                textBox18.BackColor = Color.White;
                textBox17.BackColor = Color.White;
                textBox16.BackColor = Color.White;
                textBox12.BackColor = Color.White;
                textBox13.BackColor = Color.White;
                textBox14.BackColor = Color.White;
                textBox15.BackColor = Color.White;
                Database.WriteLog("Killing Bots");
                foreach (var process in Process.GetProcesses())
                {
                    switch (process.ProcessName)
                    {
                        case "MyBot.run":
                        case "adb":
                        case "MEmuHeadless":
                        case "MyBot.run.Watchdog":
                        case "MyBot.run.MiniGui":
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
                foreach (var Android in Database.Emulator)
                {
                    if(Android != null)
                    {
                        if (Android.Length > 0)
                        {
                            foreach (var Emulator in Process.GetProcessesByName(Android))
                            {
                                try
                                {
                                    Emulator.Kill();
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                Database.ResetHost();
                timer2.Stop();
                Database.WriteLog("Stopped");
                Database.loadingprocess = 100;
                switch (Database.Language)
                {
                    case "Chinese":
                        button1.Text = cn_Lang.Start_Button;
                        break;
                    case "English":
                        button1.Text = en_Lang.Start_Button;
                        break;
                }
                button1.BackColor = Color.Lime;
                button1.Enabled = true;
            }
        }

        public void WriteAllSettings()//Save all settings into Database temp memory
        {

            Array.Resize(ref Database.Time, 138);
            Array.Resize(ref Database.Bot, 44);
            Array.Resize(ref Database.OtherBot, 12);
            Array.Resize(ref Database.ProTime, 62);
            #region SaveBotIntoAray
            if (!checkBox1.Checked)
            {
                Database.Bot[0] = "# " + textBox27.Text + " " + comboBox1.Text + " " + textBox1.Text;
            }
            else
            {
                Database.Bot[0] = textBox27.Text + " " + comboBox1.Text + " " + textBox1.Text;
            }
            if (!checkBox2.Checked)
            {
                Database.Bot[1] = "# " + textBox26.Text + " " + comboBox2.Text + " " + textBox2.Text;
            }
            else
            {
                Database.Bot[1] = textBox26.Text + " " + comboBox2.Text + " " + textBox2.Text;
            }
            if (!checkBox3.Checked)
            {
                Database.Bot[2] = "# " + textBox25.Text + " " + comboBox3.Text + " " + textBox3.Text;
            }
            else
            {
                Database.Bot[2] = textBox25.Text + " " + comboBox3.Text + " " + textBox3.Text;
            }
            if (!checkBox4.Checked)
            {
                Database.Bot[3] = "# " + textBox24.Text + " " + comboBox4.Text + " " + textBox4.Text;
            }
            else
            {
                Database.Bot[3] = textBox24.Text + " " + comboBox4.Text + " " + textBox4.Text;
            }
            if (!checkBox5.Checked)
            {
                Database.Bot[4] = "# " + textBox23.Text + " " + comboBox5.Text + " " + textBox5.Text;
            }
            else
            {
                Database.Bot[4] = textBox23.Text + " " + comboBox5.Text + " " + textBox5.Text;
            }
            if (!checkBox6.Checked)
            {
                Database.Bot[5] = "# " + textBox22.Text + " " + comboBox6.Text + " " + textBox6.Text;
            }
            else
            {
                Database.Bot[5] = textBox22.Text + " " + comboBox6.Text + " " + textBox6.Text;
            }
            if (!checkBox7.Checked)
            {
                Database.Bot[6] = "# " + textBox21.Text + " " + comboBox7.Text + " " + textBox7.Text;
            }
            else
            {
                Database.Bot[6] = textBox21.Text + " " + comboBox7.Text + " " + textBox7.Text;
            }
            if (!checkBox8.Checked)
            {
                Database.Bot[7] = "# " + textBox19.Text + " " + comboBox8.Text + " " + textBox8.Text;
            }
            else
            {
                Database.Bot[7] = textBox19.Text + " " + comboBox8.Text + " " + textBox8.Text;
            }
            if (!checkBox9.Checked)
            {
                Database.Bot[8] = "# " + textBox18.Text + " " + comboBox9.Text + " " + textBox9.Text;
            }
            else
            {
                Database.Bot[8] = textBox18.Text + " " + comboBox9.Text + " " + textBox9.Text;
            }
            if (!checkBox10.Checked)
            {
                Database.Bot[9] = "# " + textBox17.Text + " " + comboBox10.Text + " " + textBox10.Text;
            }
            else
            {
                Database.Bot[9] = textBox17.Text + " " + comboBox10.Text + " " + textBox10.Text;
            }
            if (!checkBox11.Checked)
            {
                Database.Bot[10] = "# " + textBox16.Text + " " + comboBox11.Text + " " + textBox11.Text;
            }
            else
            {
                Database.Bot[10] = textBox16.Text + " " + comboBox11.Text + " " + textBox11.Text;
            }
            if (!checkBox12.Checked)
            {
                Database.Bot[11] = "# " + textBox12.Text + " " + comboBox12.Text + " " + textBox60.Text;
            }
            else
            {
                Database.Bot[11] = textBox12.Text + " " + comboBox12.Text + " " + textBox60.Text;
            }
            if (!checkBox13.Checked)
            {
                Database.Bot[12] = "# " + textBox13.Text + " " + comboBox13.Text + " " + textBox61.Text;
            }
            else
            {
                Database.Bot[12] = textBox13.Text + " " + comboBox13.Text + " " + textBox61.Text;
            }
            if (!checkBox14.Checked)
            {
                Database.Bot[13] = "# " + textBox14.Text + " " + comboBox14.Text + " " + textBox62.Text;
            }
            else
            {
                Database.Bot[13] = textBox14.Text + " " + comboBox14.Text + " " + textBox62.Text;
            }
            if (!checkBox15.Checked)
            {
                Database.Bot[14] = "# " + textBox15.Text + " " + comboBox15.Text + " " + textBox63.Text;
            }
            else
            {
                Database.Bot[14] = textBox15.Text + " " + comboBox15.Text + " " + textBox63.Text;
            }
            #endregion
            Database.Time[0] = shour1.Text;
            Database.Time[1] = shour2.Text;
            Database.Time[2] = shour3.Text;
            Database.Time[3] = shour4.Text;
            Database.Time[4] = shour5.Text;
            Database.Time[5] = shour6.Text;
            Database.Time[6] = shour7.Text;
            Database.Time[7] = shour8.Text;
            Database.Time[8] = shour9.Text;
            Database.Time[9] = shour10.Text;
            Database.Time[10] = shour11.Text;
            Database.Time[11] = shour12.Text;
            Database.Time[12] = shour13.Text;
            Database.Time[13] = shour14.Text;
            Database.Time[14] = shour15.Text;
            Database.Time[15] = ehour1.Text;
            Database.Time[16] = ehour2.Text;
            Database.Time[17] = ehour3.Text;
            Database.Time[18] = ehour4.Text;
            Database.Time[19] = ehour5.Text;
            Database.Time[20] = ehour6.Text;
            Database.Time[21] = ehour7.Text;
            Database.Time[22] = ehour8.Text;
            Database.Time[23] = ehour9.Text;
            Database.Time[24] = ehour10.Text;
            Database.Time[25] = ehour11.Text;
            Database.Time[26] = ehour12.Text;
            Database.Time[27] = ehour13.Text;
            Database.Time[28] = ehour14.Text;
            Database.Time[29] = ehour15.Text;
            Database.Time[30] = smin1.Text;
            Database.Time[31] = smin2.Text;
            Database.Time[32] = smin3.Text;
            Database.Time[33] = smin4.Text;
            Database.Time[34] = smin5.Text;
            Database.Time[35] = smin6.Text;
            Database.Time[36] = smin7.Text;
            Database.Time[37] = smin8.Text;
            Database.Time[38] = smin9.Text;
            Database.Time[39] = smin10.Text;
            Database.Time[40] = smin11.Text;
            Database.Time[41] = smin12.Text;
            Database.Time[42] = smin13.Text;
            Database.Time[43] = smin14.Text;
            Database.Time[44] = smin15.Text;
            Database.Time[45] = emin1.Text;
            Database.Time[46] = emin2.Text;
            Database.Time[47] = emin3.Text;
            Database.Time[48] = emin4.Text;
            Database.Time[49] = emin5.Text;
            Database.Time[50] = emin6.Text;
            Database.Time[51] = emin7.Text;
            Database.Time[52] = emin8.Text;
            Database.Time[53] = emin9.Text;
            Database.Time[54] = emin10.Text;
            Database.Time[55] = emin11.Text;
            Database.Time[56] = emin12.Text;
            Database.Time[57] = emin13.Text;
            Database.Time[58] = emin14.Text;
            Database.Time[59] = emin15.Text;
            Database.OtherBot[0] = textBox108.Text;
            Database.OtherBot[1] = textBox109.Text;
            Database.OtherBot[2] = textBox110.Text;
            Database.OtherBot[3] = textBox111.Text;
            Database.OtherBot[4] = textBox112.Text;
            Database.OtherBot[5] = textBox113.Text;
            Database.OtherBot[6] = textBox114.Text;
            Database.OtherBot[7] = textBox115.Text;
            Database.OtherBot[8] = textBox116.Text;
            Database.OtherBot[9] = textBox117.Text;
            Database.OtherBot[10] = textBox118.Text;
            Database.OtherBot[11] = textBox119.Text;
            Database.ProTime[0] = textBox131.Text;
            Database.ProTime[1] = textBox143.Text;
            Database.ProTime[2] = textBox130.Text;
            Database.ProTime[3] = textBox142.Text;
            Database.ProTime[4] = textBox129.Text;
            Database.ProTime[5] = textBox141.Text;
            Database.ProTime[6] = textBox128.Text;
            Database.ProTime[7] = textBox140.Text;
            Database.ProTime[8] = textBox127.Text;
            Database.ProTime[9] = textBox139.Text;
            Database.ProTime[10] = textBox126.Text;
            Database.ProTime[11] = textBox138.Text;
            Database.ProTime[12] = textBox125.Text;
            Database.ProTime[13] = textBox137.Text;
            Database.ProTime[14] = textBox124.Text;
            Database.ProTime[15] = textBox136.Text;
            Database.ProTime[16] = textBox123.Text;
            Database.ProTime[17] = textBox135.Text;
            Database.ProTime[18] = textBox122.Text;
            Database.ProTime[19] = textBox134.Text;
            Database.ProTime[20] = textBox121.Text;
            Database.ProTime[21] = textBox133.Text;
            Database.ProTime[22] = textBox120.Text;
            Database.ProTime[23] = textBox132.Text;
            Database.ProTime[24] = textBox155.Text; //End Time of other Program
            Database.ProTime[25] = textBox167.Text;
            Database.ProTime[26] = textBox154.Text;
            Database.ProTime[27] = textBox166.Text;
            Database.ProTime[28] = textBox153.Text;
            Database.ProTime[29] = textBox165.Text;
            Database.ProTime[30] = textBox152.Text;
            Database.ProTime[31] = textBox164.Text;
            Database.ProTime[32] = textBox151.Text;
            Database.ProTime[33] = textBox163.Text;
            Database.ProTime[34] = textBox150.Text;
            Database.ProTime[35] = textBox162.Text;
            Database.ProTime[36] = textBox149.Text;
            Database.ProTime[37] = textBox161.Text;
            Database.ProTime[38] = textBox148.Text;
            Database.ProTime[39] = textBox160.Text;
            Database.ProTime[40] = textBox147.Text;
            Database.ProTime[41] = textBox159.Text;
            Database.ProTime[42] = textBox146.Text;
            Database.ProTime[43] = textBox158.Text;
            Database.ProTime[44] = textBox145.Text;
            Database.ProTime[45] = textBox157.Text;
            Database.ProTime[46] = textBox144.Text;
            Database.ProTime[47] = textBox156.Text;
            Database.ProTime[48] = textBox143.Text;
            Database.Bot[21] = textBox172.Text;
            Database.Bot[22] = textBox173.Text;
            Array.Resize(ref Database.OtherNet, 12);
            OtherNET(checkBox19, 0);
            OtherNET(checkBox20, 1);
            OtherNET(checkBox22, 2);
            OtherNET(checkBox21, 3);
            OtherNET(checkBox24, 4);
            OtherNET(checkBox23, 5);
            OtherNET(checkBox26, 6);
            OtherNET(checkBox25, 7);
            OtherNET(checkBox28, 8);
            OtherNET(checkBox27, 9);
            OtherNET(checkBox30, 10);
            OtherNET(checkBox29, 11);
            int y = 49;
            foreach (var other in Database.OtherNet)
            {
                Database.ProTime[y] = other.ToString();
                y++;
            }
            Database.Time[79] = comboBox18.SelectedIndex.ToString();
            if (checkBox17.Checked)
            {
                Database.Time[80] = "1";
            }
            else
            {
                Database.Time[80] = "0";
            }
            if (MyBotHide.Checked)
            {
                Database.Time[81] = "1";
            }
            else
            {
                Database.Time[81] = "0";
            }
            if (EmulatorHide.Checked)
            {
                Database.Time[82] = "1";
            }
            else
            {
                Database.Time[82] = "0";
            }
            if (checkBox31.Checked)
            {
                Database.Time[83] = "1";
            }
            else
            {
                Database.Time[83] = "0";
            }
            if (Shutdown.Checked == true)
            {
                Database.Time[84] = "1";
            }
            else
            {
                Database.Time[84] = "0";
            }
            if (QuotaLimit.Checked == true)
            {
                var count = Limit.Value * 1024;
                int value = size * 1024;
                Database.Time[85] = value.ToString();
                Database.Time[86] = count.ToString();
            }
            else
            {
                Database.Time[85] = "134217728";
                Database.Time[86] = "40";
            }
            if (Database.Language == "English")
            {
                Database.Time[87] = "1";
            }
            else
            {
                Database.Time[87] = "0";
            }
            if (CloseLID.Checked == true)
            {
                Database.Time[88] = "1";
            }
            else
            {
                Database.Time[88] = "0";
            }
            if (ShutdownWhenLimitReached.Checked == true)
            {
                Database.Time[89] = "66";
            }
            else
            {
                Database.Time[89] = "0";
            }
            if (DisableMoney.Checked == false)
            {
                Database.Time[90] = "444";
            }
            else
            {
                Database.Time[90] = "888";
            }
            if (Scheduledshutdown.Checked == true)
            {
                Database.Time[91] = ShutdownTimeHour.Text;
                Database.Time[92] = ShutdownTimeMinute.Text;
            }
            else
            {
                Database.Time[91] = "77";
                Database.Time[92] = "99";
            }
            if (Priority.Checked == true)
            {
                Database.Time[93] = "918";
            }
            else
            {
                Database.Time[93] = "0";
            }
            if (RestartImm.Checked == true)
            {
                Database.Time[94] = "108";
            }
            else
            {
                Database.Time[94] = "0";
            }
            Database.Time[95] = CPU_over.Text;
            Database.Time[96] = CPU_Normal.Text;
            try
            {
                int SleepOnBattery_ = Convert.ToInt32(SleepOnBattery.Text);
                int SleepOnPower_ = Convert.ToInt32(SleepOnPower.Text);
                int ScreenOnBattery_ = Convert.ToInt32(ScreenOnBattery.Text);
                int ScreenOnPower_ = Convert.ToInt32(ScreenOnPower.Text);
                switch (SleepOnBatterySize.SelectedIndex)
                {
                    case 1:
                        SleepOnBattery_ *= 60;
                        Database.Time[97] = SleepOnBattery_.ToString();
                        break;
                    case 2:
                        SleepOnBattery_ *= 3600;
                        Database.Time[97] = SleepOnBattery_.ToString();
                        break;
                    default:
                        Database.Time[97] = SleepOnBattery_.ToString();
                        break;
                }
                switch (SleepOnPowerSize.SelectedIndex)
                {
                    case 1:
                        SleepOnPower_ *= 60;
                        Database.Time[98] = SleepOnPower_.ToString();
                        break;
                    case 2:
                        SleepOnPower_ *= 3600;
                        Database.Time[98] = SleepOnPower_.ToString();
                        break;
                    default:
                        Database.Time[98] = SleepOnPower_.ToString();
                        break;
                }
                switch (ScreenOnBatterySize.SelectedIndex)
                {
                    case 1:
                        ScreenOnBattery_ *= 60;
                        Database.Time[99] = ScreenOnBattery_.ToString();
                        break;
                    case 2:
                        ScreenOnBattery_ *= 3600;
                        Database.Time[99] = ScreenOnBattery_.ToString();
                        break;
                    default:
                        Database.Time[99] = ScreenOnBattery_.ToString();
                        break;
                }
                switch (ScreenOnPowerSize.SelectedIndex)
                {
                    case 1:
                        ScreenOnPower_ *= 60;
                        Database.Time[100] = ScreenOnPower_.ToString();
                        break;
                    case 2:
                        ScreenOnPower_ *= 3600;
                        Database.Time[100] = ScreenOnPower_.ToString();
                        break;
                    default:
                        Database.Time[100] = ScreenOnPower_.ToString();
                        break;
                }
            }
            catch (Exception ex)
            {
                Database.Time[97] = SleepOnBattery.Text;
                Database.Time[98] = SleepOnPower.Text;
                Database.Time[99] = ScreenOnBattery.Text;
                Database.Time[100] = ScreenOnPower.Text;
                File.WriteAllText("error.log", ex.ToString());
            }
            if (NoBotOnBattery.Checked == true)
            {
                Database.Time[101] = "1";
            }
            else
            {
                Database.Time[101] = "0";
            }
            if (HourSetting.Checked == true)
            {
                Database.Time[102] = "1";
            }
            else
            {
                Database.Time[102] = "0";
            }
            Database.Time[103] = shour1.Value.ToString();
            Database.Time[104] = trackBar3.Value.ToString();
            if (checkBox32.Checked == true)
            {
                Database.Time[105] = "1";
            }
            else
            {
                Database.Time[105] = "0";
            }
            if (checkBox33.Checked == true)
            {
                Database.Time[106] = "1";
            }
            else
            {
                Database.Time[106] = "0";
            }
            if (checkBox34.Checked == true)
            {
                Database.Time[107] = "1";
            }
            else
            {
                Database.Time[107] = "0";
            }
            if (checkBox35.Checked == true)
            {
                Database.Time[108] = "1";
            }
            else
            {
                Database.Time[108] = "0";
            }
            if (checkBox36.Checked)
            {
                Database.Time[109] = "1";
            }
            else
            {
                Database.Time[109] = "0";
            }
            if (checkBox37.Checked)
            {
                Database.Time[110] = "1";
            }
            else
            {
                Database.Time[110] = "0";
            }
            if (checkBox38.Checked)
            {
                Database.Time[111] = "1";
            }
            else
            {
                Database.Time[111] = "0";
            }
            if (checkBox39.Checked)
            {
                Database.Time[112] = "1";
            }
            else
            {
                Database.Time[112] = "0";
            }
            if (checkBox40.Checked)
            {
                Database.Time[113] = "1";
            }
            else
            {
                Database.Time[113] = "0";
            }
            if (checkBox42.Checked)
            {
                Database.Time[114] = "1";
            }
            else
            {
                Database.Time[114] = "0";
            }
            Database.Time[115] = TBot.cid.ToString();
            if (checkBox1.Checked)
            {
                Database.Time[116] = "1";
            }
            else
            {
                Database.Time[116] = "0";
            }

            Database.WriteLog("Checking Error");
            int x = 0;
            foreach (var Bot in Database.Bot)
            {
                if (Bot == null)
                {
                    Database.Bot[x] = "";
                }
                x++;
            }
            x = 0;
            foreach (var Time in Database.Time)
            {
                try
                {
                    Convert.ToInt32(Time);
                }
                catch
                {
                    Database.Time[x] = "0";
                }
                x++;
            }
            x = 0;
            foreach (var Other in Database.OtherBot)
            {
                if (Other == null)
                {
                    Database.OtherBot[x] = "";
                }
                x++;
            }
            x = 0;
            foreach (var ProTime in Database.ProTime)
            {
                if (ProTime == null)
                {
                    Database.ProTime[x] = "0";
                }
                else if (ProTime == "")
                {
                    Database.ProTime[x] = "0";
                }
                else
                {
                    try
                    {
                        Convert.ToInt32(ProTime);
                    }
                    catch
                    {
                        Database.ProTime[x] = "0";
                    }
                }
                x++;
            }
            SetTextBox();
        }

        private void timer1_Tick(object sender, EventArgs e)//Refresh UI values and accept Telegram control commands
        {
            GC.Collect();
            if (Run)
            {
                int x = 0;
                foreach (var running in Botting.IsRunning)
                {
                    switch (x)
                    {
                        case 0:
                            if (running)
                            {
                                textBox27.BackColor = Color.LightGreen;

                            }
                            else
                            {
                                textBox27.BackColor = Color.LightGray;
                            }
                            break;
                        case 1:
                            if (running)
                            {
                                textBox26.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox26.BackColor = Color.LightGray;
                            }
                            break;
                        case 2:
                            if (running)
                            {
                                textBox25.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox25.BackColor = Color.LightGray;
                            }
                            break;
                        case 3:
                            if (running)
                            {
                                textBox24.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox24.BackColor = Color.LightGray;
                            }
                            break;
                        case 4:
                            if (running)
                            {
                                textBox23.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox23.BackColor = Color.LightGray;
                            }
                            break;
                        case 5:
                            if (running)
                            {
                                textBox22.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox22.BackColor = Color.LightGray;
                            }
                            break;
                        case 6:
                            if (running)
                            {
                                textBox21.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox21.BackColor = Color.LightGray;
                            }
                            break;
                        case 7:
                            if (running)
                            {
                                textBox19.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox19.BackColor = Color.LightGray;
                            }
                            break;
                        case 8:
                            if (running)
                            {
                                textBox18.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox18.BackColor = Color.LightGray;
                            }
                            break;
                        case 9:
                            if (running)
                            {
                                textBox17.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox17.BackColor = Color.LightGray;
                            }
                            break;
                        case 10:
                            if (running)
                            {
                                textBox16.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox16.BackColor = Color.LightGray;
                            }
                            break;
                        case 11:
                            if (running)
                            {
                                textBox12.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox12.BackColor = Color.LightGray;
                            }
                            break;
                        case 12:
                            if (running)
                            {
                                textBox13.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox13.BackColor = Color.LightGray;
                            }
                            break;
                        case 13:
                            if (running)
                            {
                                textBox14.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox14.BackColor = Color.LightGray;
                            }
                            break;
                        case 14:
                            if (running)
                            {
                                textBox15.BackColor = Color.LightGreen;
                            }
                            else
                            {
                                textBox15.BackColor = Color.LightGray;
                            }
                            break;
                    }
                    x++;
                }
            }
            if (!checkBox32.Checked && !checkBox33.Checked && !checkBox34.Checked && !checkBox35.Checked)
            {
                TBot.Schedule_Respond = false;
            }
            else
            {
                TBot.Schedule_Respond = true;
            }
            if (TBot.command.Length > 0)
            {
                try
                {
                    Database.WriteLog("Converted into command: " + TBot.command);
                    switch (TBot.command)
                    {
                        case "s":
                            button1_Click(sender, e);
                            TBot.command = "";
                            break;
                        case "en":
                            englishToolStripMenuItem_Click(sender, e);
                            TBot.command = "";
                            break;
                        case "cn":
                            中文ToolStripMenuItem_Click(sender, e);
                            TBot.command = "";
                            break;
                        case "cpu":
                            TBot.respond = label2.Text + " " + CPUN + "\n" + label3.Text + " " + CPUT + "°C\n" + label6.Text + " " + CPUV.ToString("0.00") + "W \n" + label5.Text + " " + CPUF.ToString("0.00") + "Ghz\n" + label4.Text + " " + CPUTM + "°C\n" + label1.Text + ": " + progressBar1.Value + " %\n" + label7.Text + ": " + progressBar2.Value + "%";
                            TBot.command = "";
                            TBot.CompletedResponding = true;
                            break;
                        case "p":
                            Thread pause = new Thread(TBot.Pause);
                            pause.Start();
                            TBot.command = "";
                            break;
                        case "list":
                            Thread list = new Thread(TBot.List);
                            list.Start();
                            TBot.command = "";
                            break;
                        case "h":
                            EmulatorHide.Checked = true;
                            MyBotHide.Checked = true;
                            Database.hide = true;
                            Database.hideEmulator = true;
                            TBot.command = "";
                            break;
                        case "sh":
                            EmulatorHide.Checked = false;
                            MyBotHide.Checked = false;
                            Database.hide = false;
                            Database.hideEmulator = false;
                            TBot.command = "";
                            break;
                        case "runningcheck":
                            Thread check = new Thread(RunningCheck);
                            check.Start();
                            TBot.command = "";
                            break;
                        case "status":
                            Thread status = new Thread(TBot.Status);
                            status.Start();
                            TBot.command = "";
                            break;
                        case "earn":
                            Thread earn = new Thread(TBot.Earn);
                            earn.Start();
                            TBot.command = "";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    File.WriteAllText("error.log", ex.ToString());
                }
                Database.WriteLog("Result: " + TBot.respond);
            }
            switch (Database.Language)
            {
                case "English":
                    textBox20.Text = Botting.ImgLoc + en_Lang.Times;
                    textBox28.Text = AutoIT + en_Lang.Times;
                    textBox29.Text = NoNet + en_Lang.Times;
                    break;
                case "Chinese":
                    textBox20.Text = Botting.ImgLoc + cn_Lang.Times;
                    textBox28.Text = AutoIT + cn_Lang.Times;
                    textBox29.Text = NoNet + cn_Lang.Times;
                    break;
            }
            if (CPUT > 70)
            {
                CPUTemp.BackColor = Color.Pink;
                if (TBot.cid > 0 && TBot.API_Key.Length > 0)
                {
                    if (CPUOverheat == 0)
                    {
                        string alert;
                        if (Database.Language == "English")
                        {
                            alert = "Your CPU is overheat! Current Temperature is " + CPUT + " °C!";
                        }
                        else
                        {
                            alert = "您的电脑温度过高！目前温度为：" + CPUT + " °C！";
                        }
                        try
                        {
                             TBot.Bot.SendTextMessageAsync(TBot.cid, alert);
                        }
                        catch
                        {

                        }
                        CPUOverheat = 60;
                    }
                    else
                    {
                        if (CPUOverheat > 0)
                        {
                            CPUOverheat--;
                        }
                    }
                }
            }
            else
            {
                CPUTemp.BackColor = Color.LightGreen;
            }
            if (checkBox17.Checked)
            {
                richTextBox1.Visible = true;
                richTextBox1.Lines = AdvanceCPU;
                Advance = true;
            }
            else
            {
                richTextBox1.Visible = false;
                Advance = false;
                CPUTemp.Text = CPUT + " °C";
                if (CPUMaxTemp.Text != CPUTM + " °C")
                {
                    CPUMaxTemp.Text = CPUTM + " °C";
                    CPUMaxTemp.BackColor = Color.LightYellow;
                }
                else
                {
                    CPUMaxTemp.BackColor = Color.LightGreen;
                }
                CPUVoltage.Text = CPUV.ToString("0.00") + " W";
                CPUFrequency.Text = CPUF.ToString("0.00") + " Ghz";
                if (RAM > 70)
                {
                    progressBar2.BackColor = Color.Pink;
                }
                else
                {
                    progressBar2.BackColor = Color.LightGreen;
                }
                if (CPUL > 70)
                {
                    progressBar1.BackColor = Color.Pink;
                }
                else
                {
                    progressBar1.BackColor = Color.LightGreen;
                }
                progressBar2.Value = RAM; //Setting tracebar value for RAM
                progressBar1.Value = CPUL; //Setting tracebar value for CPU
            }
            
             Task.Delay(10);
            if (HourSetting.Checked)
            {
                Time.Text = DateTime.Now.ToString("hh:mm:ss tt");
            }
            else
            {
                Time.Text = DateTime.Now.ToString("HH:mm:ss");
            }
                if (!Database.Network)
                {
                    if (Received.BackColor != Color.LightYellow)
                    {
                        Received.BackColor = Color.Pink;
                        Sent.BackColor = Color.Pink;
                        UpSpeed.BackColor = Color.Pink;
                        DownSpeed.BackColor = Color.Pink;
                        NetworkName.BackColor = Color.Pink;
                    }
                    switch (Database.Language)
                    {
                        case "English":
                            Received.Text = en_Lang.NoNetwork;
                            Sent.Text = en_Lang.NoNetwork;
                            UpSpeed.Text = en_Lang.NoNetwork;
                            DownSpeed.Text = en_Lang.NoNetwork;
                            NetworkName.Text = string.Empty;
                            break;
                        case "Chinese":
                            Received.Text = cn_Lang.NoNetwork;
                            Sent.Text = cn_Lang.NoNetwork;
                            UpSpeed.Text = cn_Lang.NoNetwork;
                            DownSpeed.Text = cn_Lang.NoNetwork;
                            NetworkName.Text = string.Empty;
                            break;
                    }
                }
                else
                {
                    if (Received.BackColor != Color.LightYellow)
                    {
                        if (Database.Receive > 0 && Database.Send > 0)
                        {
                            Received.BackColor = Color.LightGreen;
                            Sent.BackColor = Color.LightGreen;
                            UpSpeed.BackColor = Color.LightGreen;
                            DownSpeed.BackColor = Color.LightGreen;
                            NetworkName.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            Received.BackColor = Color.LightCyan;
                            Sent.BackColor = Color.LightCyan;
                            UpSpeed.BackColor = Color.LightCyan;
                            DownSpeed.BackColor = Color.LightCyan;
                            NetworkName.BackColor = Color.LightCyan;
                        }
                    }
                    NetworkName.Text = Database.NetName;
                    Received.Text = Database.Receive_Print.ToString("0.000 ") + Database.Receive_size;
                    Sent.Text = Database.Send_Print.ToString("0.000 ") + Database.Send_size;
                    UpSpeed.Text = Database.shows.ToString("0.00 ") + Database.ssize;
                    DownSpeed.Text = Database.showr.ToString("0.00 ") + Database.rsize;
                }
        }

        private void timer2_Tick(object sender, EventArgs e)//Timer that runs after botting started, used to set PC environment system and watchdog of MB
        {
            if (RefreshTreeNodes == 0)
            {
                Database.WriteLog("Entering Tree Nodes");
                Thread node = new Thread(TreeViewHandler);
                node.Start();
                RefreshTreeNodes = 24;
            }
            else
            {
                Database.WriteLog("Waiting Tree Nodes Enter");
                RefreshTreeNodes--;
            }
            if (Database.Hour > 0 && Database.Hour < 8 && Win32.GetIdleTime() > 60000 && !File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables(directX).au3"))
            {
                if (!CloseLID.Checked)
                {
                    CloseLID.Checked = true; 
                    AutoSet = true;
                }
            }
            else
            {
                if (AutoSet)
                {
                    CloseLID.Checked = false;
                    AutoSet = false;
                }
            }
            Database.WriteLog("Checking Power Status");
            if (SystemInformation.PowerStatus.BatteryChargeStatus != BatteryChargeStatus.NoSystemBattery)
            {
                if (NoBotOnBattery.Checked)
                {
                    if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
                    {
                        if (Database.OnBattery)
                        {
                            Database.OnBattery = false;
                        }
                    }
                    else
                    {
                        if (!Database.OnBattery)
                        {
                            Database.OnBattery = true;
                        }
                    }
                }
                else
                {
                    if (Database.OnBattery)
                    {
                        Database.OnBattery = false;
                    }
                }
            }
            else
            {
                if (NoBotOnBattery.Visible)
                {
                    NoBotOnBattery.Checked = false;
                    NoBotOnBattery.Visible = false;
                    Database.OnBattery = false;
                }
            }
            if (EmulatorHide.Checked)
            {
                if (checkBox31.Enabled)
                {
                    checkBox31.Enabled = false;
                    checkBox31.Checked = false;
                }
            }
            else
            {
                if (!checkBox31.Enabled)
                {
                    checkBox31.Enabled = true;
                    checkBox31.Checked = false;
                }
            }
            if (checkBox31.Checked)
            {
                if (EmulatorHide.Enabled)
                {
                    EmulatorHide.Enabled = false;
                    EmulatorHide.Checked = false;
                }
            }
            else
            {
                if (!EmulatorHide.Enabled)
                {
                    EmulatorHide.Enabled = true;
                    EmulatorHide.Checked = false;
                }
            }
            Database.WriteLog("Quota Checking");
            if (Database.Limit <= Database.Receive + Database.Send) //Close all program that related to MyBot if Quota Limit Reached
            {
                Received.BackColor = Color.LightYellow;
                Sent.BackColor = Color.LightYellow;
                UpSpeed.BackColor = Color.LightYellow;
                DownSpeed.BackColor = Color.LightYellow;
                NetworkName.BackColor = Color.LightYellow;
                KillMyBot();
                Run = false;
                button1_Click(sender, e);
                if (Database.ShutdownWhenLimitReached == 66)
                {
                    Process.Start("shutdown.exe", "/s /t 00"); //Shutdown when shutdown when limit reached enabled
                }
                else
                {
                    Database.WriteLog("Disabling Networks");
                    SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
                    foreach (ManagementObject s in searcher.Get())
                    {
                        s.InvokeMethod("Disable", null);
                    }
                }
            }
            if (Database.Hour == Database.Bot_Timer[91] && Database.Min == Database.Bot_Timer[92])
            {
                Process.Start("shutdown.exe", "/s /t 00");
            }
            if (Database.Net_Error > 0)
            {
                Database.Net_Error -= 1; //Check the program had been started for 10 sec over
            }
        }

        private static void ReportError()
        {
                if (File.Exists("error.log"))
                {
                    FileInfo file = new FileInfo("error.log");
                    try
                    {
                        TBot.DebugBot("error.log");
                    }
                    catch
                    {

                    }
                    FileStream stream = null;
                    while (stream == null && File.Exists("error.log"))
                    {
                        try
                        {
                            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                        }
                        catch (IOException)
                        {

                        }
                        finally
                        {
                            if (stream != null)
                                stream.Close();
                        }
                    }
                    string filename = "Sended_On_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".log";
                    if (!Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Suporter_Archived_Error"))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\MyBot_Suporter_Archived_Error");
                    }
                    if (File.Exists("error.log"))
                    {
                        File.Move("error.log", Environment.CurrentDirectory + "\\MyBot_Suporter_Archived_Error\\" + filename);
                    }
                }
        }
        private void startBottingToolStripMenuItem_Click(object sender, EventArgs e)//Notification icon function
        {
            button1_Click(sender, e);
        }

        private void stopBottingToolStripMenuItem_Click(object sender, EventArgs e)//Notification icon function
        {
            button1_Click(sender, e);
        }

        private void MainScreen_Load(object sender, EventArgs e)//Things to done when startup
        {
            if (Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Suporter_Archived_Error\\"))
            {
                string[] log = Directory.GetFiles(Environment.CurrentDirectory + "\\MyBot_Suporter_Archived_Error\\");
                foreach (var l in log)
                {
                    File.Delete(l);
                }
            }
            if (!File.Exists("MyBot.run.exe"))
            {
                FirstUse f = new FirstUse();
                f.ShowDialog();
                Database.loadingprocess = 100;
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            version = myFileVersionInfo.FileVersion;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Supporter = false;
            Database.WriteLog("-----------------------------------------------");
            Database.WriteLog("MyBot.Supporter running on " + Environment.OSVersion);
            this.Icon = Characters._123456;
            if (File.Exists(Database.Location + "Bot.dll"))
            {
                File.Delete(Database.Location + "Bot.dll");
            }
            if (File.Exists(Database.Location + "Time.dll"))
            {
                File.Delete(Database.Location + "Time.dll");
            }
            if (File.Exists(Database.Location + "Program.dll"))
            {
                File.Delete(Database.Location + "Program.dll");
            }
            if (File.Exists(Database.Location + "ProTime.dll"))
            {
                File.Delete(Database.Location + "ProTime.dll");
            }
            if (File.Exists(Database.Location + "Custom.config"))
            {
                File.Delete(Database.Location + "Custom.config");
            }
            if (File.Exists(Database.Location + "NTU_4"))
            {
                File.Delete(Database.Location + "NTU_4");
            }
            Database.loadingprocess = 15;
            Supporter = true;
            Database.WriteLog("Drawing UI");
            Database.WriteLog("Disabling Tab MouseWheels Controler");
            Database.DisableMouseWheels(tabPage3);
            Database.DisableMouseWheels(tabPage4);
            Run = false;
            Supporter = true;
            //Database.WriteLog("Checking Registry .Net Version");
            //Win32.GetVersionFromRegistry();
            Database.WriteLog("Checking Settings");
            try
            {
                if (!File.Exists(Database.Location + "botsave"))
                {
                    Database.WriteLog("Creating botsave");
                    Array.Resize(ref Database.Bot, 44);
                    string[] profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
                    int y = 0;
                    foreach (var profile in profiles)
                    {
                        try
                        {
                            var name = profile.Remove(0, profile.LastIndexOf('\\') + 1);
                            Database.Bot[y] = name;
                            y++;
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    GenerateProfile g = new GenerateProfile();
                    g.ShowDialog();
                    File.WriteAllLines(Database.Location + "botsave", Database.Bot);
                }
                Array.Resize(ref Database.Bot, 44);
                Database.Bot = File.ReadAllLines(Database.Location + "botsave");
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
            }
            try
            {
                if (!File.Exists(Database.Location + "timesave"))
                {
                    Database.WriteLog("Creating timesave");
                    string[] set = { "00", "00", "00","00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23", "23",//Hour
                    "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59", "59",//Minute
                    "23", "00", "23", "00", "23", "00", "23", "00", "23", "00", "23","00", "00", "00", "00", "00", "00","0","134217728","40",FirstUse.Language.ToString(),"0","0","888","77","99","918","0",FirstUse.CPUO.ToString(),FirstUse.CPUN.ToString(),"0","0","0","0","0","0"};//Other
                    File.WriteAllLines(Database.Location + "timesave", set);
                }
                Database.Language = "English";
                Database.Time = File.ReadAllLines(Database.Location + "timesave");
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
            }
            try
            {
                if (!File.Exists(Database.Location + "programsave"))
                {
                    Database.WriteLog("Creating Programsave");
                    string[] set = { };
                    Array.Resize(ref set, 12);
                    File.WriteAllLines(Database.Location + "programsave", set);
                }
                Database.OtherBot = File.ReadAllLines(Database.Location + "programsave");
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
            }
            try
            {
                if (!File.Exists(Database.Location + "programtimesave"))
                {
                    Database.WriteLog("Creating programtimesave");
                    string[] set = { "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "00", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59", "23", "59" };
                    File.WriteAllLines(Database.Location + "programtimesave", set);
                }
                Database.ProTime = File.ReadAllLines(Database.Location + "programtimesave");
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
            }
            Database.WriteLog("Checking Error");
            Array.Resize(ref Database.Time, 138);
            Array.Resize(ref Database.Bot, 44);
            Array.Resize(ref Database.OtherBot, 12);
            Array.Resize(ref Database.OtherID, 12);
            Array.Resize(ref Database.Emulator, 6);
            Array.Resize(ref Database.ProTime, 62);
            Array.Resize(ref Botting.Error, 21);
            Array.Resize(ref Database.hWnd, 21);
            Array.Resize(ref TBot.PauseMessageSended, 21);
            int x = 0;
            string temp = Database.Bot[0];
            bool Equal = true;
            foreach (var Bot in Database.Bot)
            {
                if (Bot == null)
                {
                    Database.Bot[x] = "";
                }
                if(x < 15 && Bot != temp)
                {
                    Equal = false;
                }
                x++;
            }
            x = 0;
            foreach (var Time in Database.Time)
            {
                try
                {
                    Convert.ToInt32(Time);
                }
                catch
                {
                    Database.Time[x] = "0";
                }
                x++;
            }
            x = 0;
            foreach (var Other in Database.OtherBot)
            {
                if (Other == null)
                {
                    Database.OtherBot[x] = "";
                }
                x++;
            }
            x = 0;
            foreach (var ProTime in Database.ProTime)
            {
                try
                {
                    Convert.ToInt32(ProTime);
                }
                catch
                {
                    Database.ProTime[x] = "0";
                }
                x++;
            }
            TBot.API_Key = Database.Bot[21];
            if (Equal)
            {
                string[] profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
                x = 0;
                foreach (var profile in profiles)
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
                GenerateProfile g = new GenerateProfile();
                g.ShowDialog();
                Database.loadingprocess = 100;
            }
            if (TBot.API_Key.Length > 0)
            {
                telegram.Start();
            }

            Database.loadingprocess = 10;
            //Emulator Check
            int X = 0;
            Array.Resize(ref Database.Emulator, 6);
            string[] Profiles;
            Database.WriteLog("Scanning MyBot Profiles");
            try
            {
                Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + "MyVillage");
                Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            }
            string[] Temp = { };
            Array.Resize(ref Temp, 10);
            Database.WriteLog("Redrawing UI");
            foreach (var profile in Profiles)
            {
                var name = profile.Remove(0, profile.LastIndexOf('\\') + 1);
                comboBox16.Items.Add(name);
                comboBox17.Items.Add(name);
            }
            foreach (var bot in Database.Bot)
            {
                Temp = bot.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                if (Temp.Contains("MEmu") && !Database.Emulator.Contains("MEmu"))
                {
                    Database.Emulator[X] = "MEmu";
                    X++;
                }
                else if (Temp.Contains("Bluestacks") && !Database.Emulator.Contains("Bluestacks"))
                {
                    Database.Emulator[X] = "Bluestacks";
                    X++;
                }
                else if (Temp.Contains("Itools") && !Database.Emulator.Contains("Itools"))
                {
                    Database.Emulator[X] = "Itools";
                    X++;
                }
                else if (Temp.Contains("Leapdroid") && !Database.Emulator.Contains("Leapdroid"))
                {
                    Database.Emulator[X] = "Leapdroid";
                    X++;
                }
                else if (Temp.Contains("Droid4X") && !Database.Emulator.Contains("Droid4X"))
                {
                    Database.Emulator[X] = "Droid4X";
                    X++;
                }
                else if (Temp.Contains("Nox") && !Database.Emulator.Contains("Nox"))
                {
                    Database.Emulator[X] = "Nox";
                    X++;
                }
            }
            X = 0;
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    if(nic.Name != "Loopback Pseudo-Interface 1")
                    {
                        Database.Network = true;
                        Database.Receive = nic.GetIPStatistics().BytesReceived; //Get Received nework data volume
                        Database.Send = nic.GetIPStatistics().BytesSent; //Get Sended network data volume
                        Database.NetName = nic.Name;
                        nics = nic;
                        break;
                    }
                }
                x++;
            }
            if (!Database.Network)
            {
                NoNet++;
            }
            x = 0;
            try
            {
                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\images\\Logo.png", FileMode.Open, FileAccess.Read);
                pictureBox2.Image = Image.FromStream(fs);
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                fs.Close();
            }
            catch
            {

            }
            /*Database.WriteLog("Getting MyDocuments Folder");
            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            DirectoryInfo download = new DirectoryInfo(Database.DownloadFolder());
            var subdirectory2 = download.GetDirectories().Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0 && (d.Attributes & FileAttributes.System) == 0);
            foreach (DirectoryInfo d in subdirectory2)
            {
                var csv = d.GetFiles("*.csv", SearchOption.AllDirectories);
                if (csv.Length > 0)
                {
                    foreach (var script in csv)
                    {
                        if (File.Exists(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name))
                        {
                            File.Delete(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name);
                        }
                        try
                        {
                            script.MoveTo(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            Database.WriteLog("Finding CSV");
            var subdirectory = di.GetDirectories().Where(d => (d.Attributes & FileAttributes.ReparsePoint) == 0 && (d.Attributes & FileAttributes.System) == 0);
            foreach (DirectoryInfo d in subdirectory)
            {
                var csv = d.GetFiles("*.csv", SearchOption.AllDirectories);
                if (csv.Length > 0)
                {
                    foreach (var script in csv)
                    {
                        if (File.Exists(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name))
                        {
                            File.Delete(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name);
                        }
                        try
                        {
                            script.MoveTo(Environment.CurrentDirectory + @"\CSV\Attack\" + script.Name);
                        }
                        catch
                        {

                        }
                    }
                }
            }*/
            //Database.WriteLog("Moving CSV");
            Database.loadingprocess = 70;
            SetTextBox();
            Database.WriteLog("CPU Monitor Start");
            computer.CPUEnabled = true;
            computer.RAMEnabled = true;
            try
            {
                computer.Open();
            }
            catch
            {

            }
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    if (nic.Name != "Loopback Pseudo-Interface 1")
                    {
                        Database.Network = true;
                        Database.Receive = nic.GetIPStatistics().BytesReceived; //Get Received nework data volume
                        Database.Send = nic.GetIPStatistics().BytesSent; //Get Sended network data volume
                        Database.NetName = nic.Name;
                        nics = nic;
                        break;
                    }
                }
                x++;
            }
            double highestV = 0;
            double highestT = 0;
            double highestC = 0;
            double highestL = 0;
            foreach (var h in computer.Hardware)
            {
                if (h.HardwareType == HardwareType.CPU)
                {
                    h.Update();
                    CPUN = h.Name;
                    foreach (var s in h.Sensors)
                    {
                        if (s.SensorType == SensorType.Power)
                        {
                            if (highestV < Convert.ToDouble(s.Value))
                            {
                                highestV = Convert.ToDouble(s.Value);
                                SelectedCPUV = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Clock)
                        {
                            if (highestC < Convert.ToDouble(s.Value))
                            {
                                highestC = Convert.ToDouble(s.Value);
                                SelectedCPUF = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Temperature)
                        {
                            if (highestT < Convert.ToDouble(s.Value))
                            {
                                highestT = Convert.ToDouble(s.Value);
                                SelectedCPUT = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Load)
                        {
                            if (highestL < Convert.ToDouble(s.Value))
                            {
                                highestL = Convert.ToDouble(s.Value);
                                SelectedCPUL = s.Name;
                            }
                        }
                    }
                }
                else if (h.HardwareType == HardwareType.RAM)
                {
                    h.Update();
                    foreach (var s in h.Sensors)
                    {
                        if (s.SensorType == SensorType.Load)
                        {
                            if (s.Value > RAM)
                            {
                                RAM = Convert.ToInt16(s.Value);
                                SelectedRAML = s.Name;
                            }
                        }
                    }
                }
            }
            Database.WriteLog("CPU Name: " + CPUN + SelectedCPUF + SelectedCPUT + SelectedCPUV);
            timer3.Start();
            timer1.Start();
            CPUName.Text = CPUN;
            //Button Language
            Database.loadingprocess = 80;
            Language();
            Thread up = new Thread(Updator);
            up.Start();
            Database.loadingprocess = 90;
            UpdateMyBot();
            Database.WriteLog("Setting Language");
            GC.Collect();
            Database.loadingprocess = 100;
            Database.WriteLog("Loading Complete");
            Win32.Power_MainScreen();
            Database.WriteLog("Setting Power Management");
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            Database.WriteLog("Form close thread handle called");
        }
        private void Language()//Set the language of the UI
        {
            switch (Database.Language)
            {
                case "English":
                    this.Text = en_Lang.Form1 + " " + version;
                    if (Update)
                    {
                        Text = Text + " / Update Available: " + MyBotUpdator.NewestVersion;
                    }
                    if (UpdateMB)
                    {
                        Text = Text + " / MyBot Update Available: " + MyBotUpdator.MBNewestVersion;
                    }

                    if (Run)
                    {
                        button1.Text = en_Lang.Stop_Button;
                        button1.BackColor = Color.Red;
                    }
                    else
                    {
                        button1.Text = en_Lang.Start_Button;
                        button1.BackColor = Color.Lime;
                    }
                    englishToolStripMenuItem1.Visible = false;
                    Priority.Text = en_Lang.Priority_CheckBox;
                    CloseLID.Text = en_Lang.Close_LID_CheckBox;
                    Shutdown.Text = en_Lang.ShutdownWhenNoNetwork_CheckBox;
                    ShutdownWhenLimitReached.Text = en_Lang.ShutdownWhenLimitReached_CheckBox;
                    QuotaLimit.Text = en_Lang.QuotaLimit_CheckBox;
                    DisableMoney.Text = en_Lang.AdsBlock_CheckBox;
                    Scheduledshutdown.Text = en_Lang.ScheduledShutdown_CheckBox;
                    RestartImm.Text = en_Lang.RestartBot_CheckBox;
                    EmulatorHide.Text = en_Lang.HideEmulator_CheckBox;
                    MyBotHide.Text = en_Lang.HideBot_CheckBox;
                    checkBox31.Text = en_Lang.DockBot_Button;
                    NoBotOnBattery.Text = en_Lang.StopBotOnBattery_CheckBox;
                    Taskbar.Text = en_Lang.HideFromTaskBar_CheckBox;
                    HourSetting.Text = en_Lang.HouSetting_CheckBox;
                    checkBox16.Text = en_Lang.RAMCleaner_CheckBox;
                    checkBox17.Text = en_Lang.AdvancedCPU;
                    checkBox36.Text = en_Lang.Telegram_BotClose_CheckBox;
                    checkBox37.Text = en_Lang.Telegram_BotStart_CheckBox;
                    checkBox38.Text = en_Lang.Telegram_StopBotting_CheckBox;
                    checkBox39.Text = en_Lang.Telegram_StartBotting_CheckBox;
                    checkBox40.Text = en_Lang.Telegram_OverHeat_CheckBox;
                    checkBox42.Text = en_Lang.Telegram_ClosingSupporter_CheckBox;
                    checkBox32.Text = en_Lang.Telegram_RunningBotCount_CheckBox;
                    checkBox33.Text = en_Lang.Telegram_RunningBotStatus_CheckBox;
                    checkBox34.Text = en_Lang.Telegram_RunningBotEarned_CheckBox;
                    checkBox35.Text = en_Lang.Telegram_CPUStatus_CheckBox;
                    button2.Text = en_Lang.Regenerate_Button;
                    button21.Text = en_Lang.CloseRunningBot_Button;
                    button18.Text = en_Lang.StartSelectedMyBot_Button;
                    button19.Text = en_Lang.CopySettings_Button;
                    button27.Text = en_Lang.CSV_Writer_Button;
                    button25.Text = en_Lang.Injector_Button;
                    button20.Text = en_Lang.AddNewProfile_Button;
                    button38.Text = en_Lang.LowPerformanceMode_Button;
                    button39.Text = en_Lang.HighPerformanceMode_Button;
                    button37.Text = en_Lang.NormalPerformanceMode_Button;
                    button36.Text = en_Lang.DestroyingPerformanceMode_Button;
                    button43.Text = en_Lang.TelegramTokenUpdate_Button;
                    label1.Text = en_Lang.F1Label14;
                    label2.Text = en_Lang.CPUName;
                    label3.Text = en_Lang.CPUTemp;
                    label4.Text = en_Lang.CPUMaxTemp;
                    label5.Text = en_Lang.CPUFrequency;
                    label6.Text = en_Lang.CPUPower;
                    label7.Text = en_Lang.F1Label15;
                    label8.Text = en_Lang.F1Label07;
                    label9.Text = en_Lang.F1Label13;
                    label10.Text = en_Lang.F1Label12;
                    label11.Text = en_Lang.F1Label17;
                    label12.Text = en_Lang.F1Label16;
                    label21.Text = en_Lang.F1Label04;
                    label22.Text = en_Lang.F1Label06;
                    label31.Text = en_Lang.F1Label03;
                    label34.Text = en_Lang.F1Label04;
                    label36.Text = en_Lang.F1Label06;
                    label33.Text = en_Lang.F1Label09;
                    label67.Text = en_Lang.F1Label08;
                    label44.Text = en_Lang.When_CPU;
                    label32.Text = en_Lang.When_CPU + en_Lang.IsNormal_Set_Maximum;
                    if(comboBox1.SelectedIndex == 0)
                    {
                        label103.Text = en_Lang.IsOver70_Set_Maximum;
                    }
                    else
                    {
                        label103.Text = en_Lang.IsOver60C_Set_Maximum;
                    }
                    comboBox18.Items[0] = en_Lang.Usage;
                    comboBox18.Items[1] = en_Lang.Temperature;
                    label52.Text = en_Lang.Battery;
                    label59.Text = en_Lang.Battery;
                    label53.Text = en_Lang.OnPower;
                    label58.Text = en_Lang.OnPower;
                    label54.Text = "";
                    label55.Text = "";
                    label57.Text = "";
                    label56.Text = "";
                    label61.Text = en_Lang.Hour;
                    label60.Text = en_Lang.Minute;
                    label62.Text = en_Lang.F1Label07;
                    label37.Text = en_Lang.ErrorImgloc;
                    label78.Text = en_Lang.TelegramFrequency;
                    label101.Text = en_Lang.ErrorAutoIT;
                    label102.Text = en_Lang.NoNet;
                    groupbox2Text.Text = en_Lang.F1TabPage8;
                    groupbox3Text.Text = en_Lang.F1GroupBox1;
                    groupbox4Text.Text = en_Lang.F1GroupBox2;
                    tabPage3.Text = en_Lang.F1TabPage5;
                    tabPage4.Text = en_Lang.F1TabPage1;
                    tabPage7.Text = en_Lang.F1TabPage7;
                    tabPage8.Text = en_Lang.F1TabPage11;
                    tabPage10.Text = en_Lang.F1TabPage11;
                    tabPage12.Text = en_Lang.F1TabPage10;
                    tabPage5.Text = en_Lang.F1Label10;
                    tabPage6.Text = en_Lang.F1Label11;
                    tabPage1.Text = en_Lang.PCStatus;
                    tabPage2.Text = en_Lang.NetStatus;
                    tabPage9.Text = en_Lang.ErrorTab;
                    tabPage11.Text = en_Lang.MBResources;
                    ScreenOnBatterySize.Items[0] = en_Lang.Second;
                    ScreenOnBatterySize.Items[1] = en_Lang.Minute;
                    ScreenOnBatterySize.Items[2] = en_Lang.Hour;
                    ScreenOnPowerSize.Items[0] = en_Lang.Second;
                    ScreenOnPowerSize.Items[1] = en_Lang.Minute;
                    ScreenOnPowerSize.Items[2] = en_Lang.Hour;
                    SleepOnPowerSize.Items[0] = en_Lang.Second;
                    SleepOnPowerSize.Items[1] = en_Lang.Minute;
                    SleepOnPowerSize.Items[2] = en_Lang.Hour;
                    SleepOnBatterySize.Items[0] = en_Lang.Second;
                    SleepOnBatterySize.Items[1] = en_Lang.Minute;
                    SleepOnBatterySize.Items[2] = en_Lang.Hour;
                    break;
                case "Chinese":
                    this.Text = cn_Lang.Form1 + " " + version;
                    if (Update)
                    {
                        Text = Text + " / 可用升级: " + MyBotUpdator.NewestVersion;
                    }
                    if (UpdateMB)
                    {
                        Text = Text + " / MyBot可用升级: " + MyBotUpdator.MBNewestVersion;
                    }
                    if (Run)
                    {
                        button1.Text = cn_Lang.Stop_Button;
                        button1.BackColor = Color.Red;
                    }
                    else
                    {
                        button1.Text = cn_Lang.Start_Button;
                        button1.BackColor = Color.Lime;
                    }
                    中文ToolStripMenuItem1.Visible = false;
                    Priority.Text = cn_Lang.Priority_CheckBox;
                    CloseLID.Text = cn_Lang.Close_LID_CheckBox;
                    Shutdown.Text = cn_Lang.ShutdownWhenNoNetwork_CheckBox;
                    ShutdownWhenLimitReached.Text = cn_Lang.ShutdownWhenLimitReached_CheckBox;
                    QuotaLimit.Text = cn_Lang.QuotaLimit_CheckBox;
                    DisableMoney.Text = cn_Lang.AdsBlock_CheckBox;
                    Scheduledshutdown.Text = cn_Lang.ScheduledShutdown_CheckBox;
                    RestartImm.Text = cn_Lang.RestartBot_CheckBox;
                    EmulatorHide.Text = cn_Lang.HideEmulator_CheckBox;
                    MyBotHide.Text = cn_Lang.HideBot_CheckBox;
                    checkBox31.Text = cn_Lang.DockBot_Botton;
                    NoBotOnBattery.Text = cn_Lang.StopBotOnBattery_CheckBox;
                    Taskbar.Text = cn_Lang.HideFromTaskBar_CheckBox;
                    HourSetting.Text = cn_Lang.HouSetting_CheckBox;
                    checkBox16.Text = cn_Lang.RAMCleaner_CheckBox;
                    checkBox17.Text = cn_Lang.AdvancedCPU;
                    checkBox36.Text = cn_Lang.Telegram_BotClose_CheckBox;
                    checkBox37.Text = cn_Lang.Telegram_BotStart_CheckBox;
                    checkBox38.Text = cn_Lang.Telegram_StopBotting_CheckBox;
                    checkBox39.Text = cn_Lang.Telegram_StartBotting_CheckBox;
                    checkBox40.Text = cn_Lang.Telegram_OverHeat_CheckBox;
                    checkBox42.Text = cn_Lang.Telegram_ClosingSupporter_CheckBox;
                    checkBox32.Text = cn_Lang.Telegram_RunningBotCount_CheckBox;
                    checkBox33.Text = cn_Lang.Telegram_RunningBotStatus_CheckBox;
                    checkBox34.Text = cn_Lang.Telegram_RunningBotEarned_CheckBox;
                    checkBox35.Text = cn_Lang.Telegram_CPUStatus_CheckBox;
                    button2.Text = cn_Lang.Regenerate_Button;
                    button21.Text = cn_Lang.CloseRunningBot_Button;
                    button18.Text = cn_Lang.StartSelectedMyBot_Button;
                    button19.Text = cn_Lang.CopySettings_Button;
                    button27.Text = cn_Lang.CSV_Writer_Button;
                    button25.Text = cn_Lang.Injector_Button;
                    button20.Text = cn_Lang.AddNewProfile_Button;
                    button38.Text = cn_Lang.LowPerformaceMode_Button;
                    button39.Text = cn_Lang.HighPerformanceMode_Button;
                    button37.Text = cn_Lang.NormalPerformanceMode_Button;
                    button36.Text = cn_Lang.DestroyingPerformanceMode_Button;
                    button43.Text = cn_Lang.TelegramTokenUpdate_Button;
                    label1.Text = cn_Lang.F1Label14;
                    label2.Text = cn_Lang.CPUName;
                    label3.Text = cn_Lang.CPUTemp;
                    label4.Text = cn_Lang.CPUMaxTemp;
                    label5.Text = cn_Lang.CPUFrequency;
                    label6.Text = cn_Lang.CPUPower;
                    label7.Text = cn_Lang.F1Label15;
                    label8.Text = cn_Lang.F1Label07;
                    label9.Text = cn_Lang.F1Label13;
                    label10.Text = cn_Lang.F1Label12;
                    label11.Text = cn_Lang.F1Label17;
                    label12.Text = cn_Lang.F1Label16;
                    label21.Text = cn_Lang.F1Label04;
                    label22.Text = cn_Lang.F1Label06;
                    label31.Text = cn_Lang.F1Label03;
                    label34.Text = cn_Lang.F1Label04;
                    label36.Text = cn_Lang.F1Label06;
                    label33.Text = cn_Lang.F1Label09;
                    label67.Text = cn_Lang.F1Label08;
                    label44.Text = cn_Lang.When_CPU;
                    label32.Text = cn_Lang.When_CPU + en_Lang.IsNormal_Set_Maximum;
                    if (comboBox1.SelectedIndex == 0)
                    {
                        label103.Text = cn_Lang.IsOver70_Set_Maximum;
                    }
                    else
                    {
                        label103.Text = cn_Lang.IsOver60C_Set_Maximum;
                    }
                    comboBox18.Items[0] = cn_Lang.Usage;
                    comboBox18.Items[1] = cn_Lang.Temperature;
                    label52.Text = cn_Lang.Battery;
                    label59.Text = cn_Lang.Battery;
                    label53.Text = cn_Lang.OnPower;
                    label58.Text = cn_Lang.OnPower;
                    label54.Text = "";
                    label55.Text = "";
                    label57.Text = "";
                    label56.Text = "";
                    label61.Text = cn_Lang.Hour;
                    label60.Text = cn_Lang.Minute;
                    label62.Text = cn_Lang.F1Label07;
                    label37.Text = cn_Lang.ErrorImgloc;
                    label78.Text = cn_Lang.TelegramFrequency;
                    label101.Text = cn_Lang.ErrorAutoIT;
                    label102.Text = cn_Lang.NoNet;
                    groupbox2Text.Text = cn_Lang.F1TabPage8;
                    groupbox3Text.Text = cn_Lang.F1GroupBox1;
                    groupbox4Text.Text = cn_Lang.F1GroupBox2;
                    tabPage3.Text = cn_Lang.F1TabPage5;
                    tabPage4.Text = cn_Lang.F1TabPage1;
                    tabPage7.Text = cn_Lang.F1Tabpage7;
                    tabPage8.Text = cn_Lang.F1TabPage11;
                    tabPage10.Text = cn_Lang.F1TabPage11;
                    tabPage12.Text = cn_Lang.F1TabPage10;
                    tabPage5.Text = cn_Lang.F1Label10;
                    tabPage6.Text = cn_Lang.F1Label11;
                    tabPage1.Text = cn_Lang.PCStatus;
                    tabPage2.Text = cn_Lang.NetStatus;
                    tabPage9.Text = cn_Lang.ErrorTab;
                    tabPage11.Text = cn_Lang.MBResources;
                    ScreenOnBatterySize.Items[0] = cn_Lang.Second;
                    ScreenOnBatterySize.Items[1] = cn_Lang.Minute;
                    ScreenOnBatterySize.Items[2] = cn_Lang.Hour;
                    ScreenOnPowerSize.Items[0] = cn_Lang.Second;
                    ScreenOnPowerSize.Items[1] = cn_Lang.Minute;
                    ScreenOnPowerSize.Items[2] = cn_Lang.Hour;
                    SleepOnPowerSize.Items[0] = cn_Lang.Second;
                    SleepOnPowerSize.Items[1] = cn_Lang.Minute;
                    SleepOnPowerSize.Items[2] = cn_Lang.Hour;
                    SleepOnBatterySize.Items[0] = cn_Lang.Second;
                    SleepOnBatterySize.Items[1] = cn_Lang.Minute;
                    SleepOnBatterySize.Items[2] = cn_Lang.Hour;
                    break;
            }
        }

        private static void HardwareSensor(ISensor sensor, int x, out int y)
        {
            switch (sensor.SensorType)
            {
                case SensorType.Temperature:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " °C";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    if (sensor.Name == SelectedCPUT)
                    {
                        CPUT = Convert.ToDouble(sensor.Value);
                        if (CPUTM < CPUT)
                        {
                            CPUTM = CPUT;
                        }
                    }
                    break;
                case SensorType.Clock:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " Mhz";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    if (sensor.Name == SelectedCPUF)
                    {
                        CPUF = Convert.ToDouble(sensor.Value / 1024);
                        if (CPUF > CPUFM)
                        {
                            CPUFM = CPUF;
                        }
                    }
                    break;
                case SensorType.Power:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " V";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    if (sensor.Name == SelectedCPUV)
                    {
                        CPUV = Convert.ToDouble(sensor.Value);
                    }
                    break;
                case SensorType.Load:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " %";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    break;
            }
            y = x;
        }

        private static void RAMSensor(ISensor sensor, int x, out int y)
        {
            switch (sensor.SensorType)
            {
                case SensorType.Load:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " %";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    RAM = Convert.ToInt32(sensor.Value);
                    break;
                case SensorType.Data:
                    if (Advance)
                    {
                        try
                        {
                            AdvanceCPU[x] = sensor.Name + " : " + sensor.SensorType + " : " + Convert.ToDouble(sensor.Value).ToString("0.0") + " GB";
                        }
                        catch
                        {

                        }
                        finally
                        {
                            x++;
                        }
                    }
                    break;
            }
            y = x;
        }

        private void MainScreen_Resize(object sender, EventArgs e)//Hide window when Hide from taskbar enabled and user minimize it, C# will make the window into small size only when hided from taskbar
        {
            if (Taskbar.Checked == true && WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
                Hide();
                showToolStripMenuItem.Visible = true;
            }
            if (this.Width < 1151 || this.Height < 608)
            {
                Size = new Size(1151, 608);
            }
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)//Things to do while Appication is closing
        {
            if (checkBox42.Checked && TBot.cid > 0 && TBot.Bot != null)
            {
                try
                {
                    if (Database.Language == "English")
                    {
                        TBot.Bot.SendTextMessageAsync(TBot.cid, "Closing Supporter!");
                    }
                    else
                    {
                        TBot.Bot.SendTextMessageAsync(TBot.cid, cn_Lang.TelegramSupporterClose);
                    }
                }
                catch
                {

                }
            }
            if (Run)
            {
                button1_Click(sender, e);
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                this.Hide();
            });
            ProcessStartInfo cmd = new ProcessStartInfo();
            cmd.FileName = "cmd.exe";
            cmd.Arguments = "netsh winsock reset";
            cmd.CreateNoWindow = true;
            cmd.WindowStyle = ProcessWindowStyle.Hidden;
            WriteAllSettings();
            Process.Start(cmd);
            Win32.Power_Reset();
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject s in searcher.Get())
            {
                Database.WriteLog("Enabling Network: " + s.GetText(TextFormat.Mof));
                s.InvokeMethod("Enable", null);
            }
            Database.WriteLog("Writing Settings");
            File.WriteAllLines(Database.Location + "botsave", Database.Bot);
            File.WriteAllLines(Database.Location + "timesave", Database.Time);
            File.WriteAllLines(Database.Location + "programsave", Database.OtherBot);
            File.WriteAllLines(Database.Location + "programtimesave", Database.ProTime);
            Database.WriteLog("Closing");
            Supporter = false;
            if (computer.CPUEnabled == true)
            {
                try
                {
                    computer.Close();
                }
                catch
                {

                }
            }
            TBot.BotMessageThreadStop();
        }

        private void button21_Click(object sender, EventArgs e)//Stop all MyBots and Emulators including adb.exe button
        {
            Database.WriteLog("StopAll_Click Called");
            KillMyBot();
        }

        private static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)//Things to do while network connection changed, used to refresh connected network
        {
            Database.Network = false;
            int x = 0;
            try
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        if (nic.Name != "Loopback Pseudo-Interface 1")
                        {
                            Database.Network = true;
                            Database.Receive = nic.GetIPStatistics().BytesReceived; //Get Received nework data volume
                            Database.Send = nic.GetIPStatistics().BytesSent; //Get Sended network data volume
                            Database.NetName = nic.Name;
                            nics = nic;
                            break;
                        }
                    }
                    x++;
                }
            }
            catch
            {
                Database.Network = false;
            }
            if (!Database.Network)
            {
                NoNet++;
            }
        }

        private void button18_Click(object sender, EventArgs e)//Run selected MyBot to let ser set MyBot
        {
            Database.WriteLog("SetMyBot_Click Called");
            string selectedprofile;
            try
            {
                selectedprofile = comboBox16.SelectedItem.ToString();
                string message;
                string caption;
                if (Database.Language == "Chinese")
                {
                    message = cn_Lang.EditMyBot;
                    caption = cn_Lang.Notification;
                }
                else
                {
                    message = en_Lang.EditMyBot;
                    caption = en_Lang.Notification;
                }
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch
            {
                string message = "";
                string caption = "";
                switch (Database.Language)
                {
                    case "Chinese":
                        message = cn_Lang.NoneProfileSelected;
                        caption = cn_Lang.Error;
                        break;
                    case "English":
                        message = en_Lang.InvalidProfile;
                        caption = en_Lang.Error;
                        break;

                }
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string bot = "MyBot.run.exe";
            ProcessStartInfo start = new ProcessStartInfo(bot);
            start.Arguments = selectedprofile + " " + "-nwd" + " " + "-nbs";
            Process M = Process.Start(start);
            comboBox16.SelectedItem = null;
        }

        private void button19_Click(object sender, EventArgs e)//Copy selected MyBot profile's ini files to all others, overwriting them
        {
            string message = "";
            string caption = "";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            Database.WriteLog("Copy_Click Called");
            switch (Database.Language)
            {
                case "Chinese":
                    message = cn_Lang.OverWriteConfirmation;
                    caption = cn_Lang.Notification;
                    break;
                case "English":
                    message = en_Lang.OverWriteConfirmation;
                    caption = en_Lang.Notification;
                    break;

            }
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (result != DialogResult.OK)
            {
                return;
            }
            string selectedprofile;
            try
            {
                selectedprofile = comboBox16.SelectedItem.ToString();
            }
            catch
            {
                switch (Database.Language)
                {
                    case "Chinese":
                        message = cn_Lang.InvalidProfile;
                        caption = cn_Lang.Error;
                        break;
                    case "English":
                        message = en_Lang.InvalidProfile;
                        caption = en_Lang.Error;
                        break;

                }
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string[] Profiles = { };
            Array.Resize(ref Profiles, comboBox17.Items.Count + 1);
            if (comboBox17.SelectedItem == null)
            {
                Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            }
            else
            {
                if (comboBox16.SelectedItem.ToString() == comboBox17.SelectedItem.ToString())
                {
                    switch (Database.Language)
                    {
                        case "Chinese":
                            message = cn_Lang.SameProfileSelected;
                            caption = cn_Lang.Error;
                            break;
                        case "English":
                            message = en_Lang.InvalidProfile;
                            caption = en_Lang.Error;
                            break;

                    }
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
                else
                {
                    Profiles[0] = Path.Combine(@Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + comboBox17.SelectedItem.ToString());
                }
            }
            string from = Path.Combine(@Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles" + Path.DirectorySeparatorChar + selectedprofile);
            string[] inilist = Directory.GetFiles(from, "*.ini");
            string[] batch = { };
            Array.Resize(ref batch, comboBox1.Items.Count + 1);
            int X = 0;
            foreach (var to in Profiles)
            {
                if (to != null)
                {
                    batch[X] = "xcopy /s /y \"" + from + "\\*.ini\" \"" + to + "\\*.ini\"";
                    X++;
                }
            }
            batch[X] = "del copying.bat";
            File.WriteAllLines("copying.bat", batch);
            Process.Start("copying.bat");
            switch (Database.Language)
            {
                case "Chinese":
                    message = cn_Lang.OverwriteSuccess;
                    caption = cn_Lang.Notification;
                    break;
                case "English":
                    message = en_Lang.OverWriteSuccess;
                    caption = en_Lang.Notification;
                    break;

            }
            result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            comboBox16.SelectedItem = null;
        }

        private void button25_Click(object sender, EventArgs e)//Call injector, which is used to modify MyBot files
        {
            Database.WriteLog("Starting Injector");
            Hide();
            Database.loadingprocess = 50;
            Thread load = new Thread(Database.Load_);
            load.Start();
            foreach (var Emulator in Database.Emulator)
            {
                foreach (var Instance in Process.GetProcessesByName(Emulator))
                {
                    try
                    {
                        Instance.Kill();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            foreach (var mini in Process.GetProcessesByName("MyBot.run.MiniGui"))
            {
                try
                {
                    mini.Kill();
                }
                catch
                {
                    continue;
                }
            }
            foreach (var MyBot in Process.GetProcessesByName("MyBot.run"))
            {
                try
                {
                    MyBot.Kill();
                }
                catch
                {
                    continue;
                }
            }
            foreach (var adb in Process.GetProcessesByName("adb"))
            {
                try
                {
                    adb.Kill();
                }
                catch
                {
                    continue;
                }
            }
            foreach (var Watchdog in Process.GetProcessesByName("MyBot.run.Watchdog"))
            {
                try
                {
                    Watchdog.Kill();
                }
                catch
                {
                    continue;
                }
            }

            Form inject = new MyBotSetter();
            inject.Show();
            inject.FormClosing += Inject_FormClosing;
        }
        private void Inject_FormClosing(object sender, FormClosingEventArgs e)//Show this window form
        {
            Show();
        }

        private void button20_Click(object sender, EventArgs e)//Create new profile of supporter, easier for user, but only save the data in RAM
        {
            Database.WriteLog("Button20_Click Called");
            WriteAllSettings();
            Form set = new AddProfile();
            set.Show();
            set.FormClosing += Set_FormClosing;
        }
        private void Set_FormClosing(object sender, FormClosingEventArgs e)//Refresh UI to show all AutoSet data from RAM
        {
            Database.WriteLog("Setting Textbox Text");
            //Bot Profile
            SetTextBox();
        }
        private void SetTextBox()//Set all data to UI
        {
            Array.Resize(ref Database.Time, 138);
            Array.Resize(ref Database.Bot, 44);
            Array.Resize(ref Database.OtherBot, 12);
            Array.Resize(ref Database.ProTime, 62);
            Database.WriteLog("Setting Textbox Text");
            int x = 0;
            try
            {
                Database.WriteLog("Loading Bot Profile");
                foreach (var bot in Database.Bot)
                {
                    if (bot.Length > 5)
                    {
                        string[] split = bot.Split(' ');
                        Array.Resize(ref split, 4);
                        switch (x)
                        {
                            case 0:
                                if (split[0] == "#")
                                {
                                    checkBox1.Checked = false;
                                    textBox27.Text = split[1];
                                    comboBox1.Text = split[2];
                                    textBox1.Text = split[3];
                                }
                                else
                                {
                                    checkBox1.Checked = true;
                                    textBox27.Text = split[0];
                                    comboBox1.Text = split[1];
                                    textBox1.Text = split[2];
                                }
                                break;
                            case 1:
                                if (split[0] == "#")
                                {
                                    checkBox2.Checked = false;
                                    textBox26.Text = split[1];
                                    comboBox2.Text = split[2];
                                    textBox2.Text = split[3];
                                }
                                else
                                {
                                    checkBox2.Checked = true;
                                    textBox26.Text = split[0];
                                    comboBox2.Text = split[1];
                                    textBox2.Text = split[2];
                                }
                                break;
                            case 2:
                                if (split[0] == "#")
                                {
                                    checkBox3.Checked = false;
                                    textBox25.Text = split[1];
                                    comboBox3.Text = split[2];
                                    textBox3.Text = split[3];
                                }
                                else
                                {
                                    checkBox3.Checked = true;
                                    textBox25.Text = split[0];
                                    comboBox3.Text = split[1];
                                    textBox3.Text = split[2];
                                }
                                break;
                            case 3:
                                if (split[0] == "#")
                                {
                                    checkBox4.Checked = false;
                                    textBox24.Text = split[1];
                                    comboBox4.Text = split[2];
                                    textBox4.Text = split[3];
                                }
                                else
                                {
                                    checkBox4.Checked = true;
                                    textBox24.Text = split[0];
                                    comboBox4.Text = split[1];
                                    textBox4.Text = split[2];
                                }
                                break;
                            case 4:
                                if (split[0] == "#")
                                {
                                    checkBox5.Checked = false;
                                    textBox23.Text = split[1];
                                    comboBox5.Text = split[2];
                                    textBox5.Text = split[3];
                                }
                                else
                                {
                                    checkBox5.Checked = true;
                                    textBox23.Text = split[0];
                                    comboBox5.Text = split[1];
                                    textBox5.Text = split[2];
                                }
                                break;
                            case 5:
                                if (split[0] == "#")
                                {
                                    checkBox6.Checked = false;
                                    textBox22.Text = split[1];
                                    comboBox6.Text = split[2];
                                    textBox6.Text = split[3];
                                }
                                else
                                {
                                    checkBox6.Checked = true;
                                    textBox22.Text = split[0];
                                    comboBox6.Text = split[1];
                                    textBox6.Text = split[2];
                                }
                                break;
                            case 6:
                                if (split[0] == "#")
                                {
                                    checkBox7.Checked = false;
                                    textBox21.Text = split[1];
                                    comboBox7.Text = split[2];
                                    textBox7.Text = split[3];
                                }
                                else
                                {
                                    checkBox7.Checked = true;
                                    textBox21.Text = split[0];
                                    comboBox7.Text = split[1];
                                    textBox7.Text = split[2];
                                }
                                break;
                            case 7:
                                if (split[0] == "#")
                                {
                                    checkBox8.Checked = false;
                                    textBox19.Text = split[1];
                                    comboBox8.Text = split[2];
                                    textBox8.Text = split[3];
                                }
                                else
                                {
                                    checkBox8.Checked = true;
                                    textBox19.Text = split[0];
                                    comboBox8.Text = split[1];
                                    textBox8.Text = split[2];
                                }
                                break;
                            case 8:
                                if (split[0] == "#")
                                {
                                    checkBox9.Checked = false;
                                    textBox18.Text = split[1];
                                    comboBox9.Text = split[2];
                                    textBox9.Text = split[3];
                                }
                                else
                                {
                                    checkBox9.Checked = true;
                                    textBox18.Text = split[0];
                                    comboBox9.Text = split[1];
                                    textBox9.Text = split[2];
                                }
                                break;
                            case 9:
                                if (split[0] == "#")
                                {
                                    checkBox10.Checked = false;
                                    textBox17.Text = split[1];
                                    comboBox10.Text = split[2];
                                    textBox10.Text = split[3];
                                }
                                else
                                {
                                    checkBox10.Checked = true;
                                    textBox17.Text = split[0];
                                    comboBox10.Text = split[1];
                                    textBox10.Text = split[2];
                                }
                                break;
                            case 10:
                                if (split[0] == "#")
                                {
                                    checkBox11.Checked = false;
                                    textBox16.Text = split[1];
                                    comboBox11.Text = split[2];
                                    textBox11.Text = split[3];
                                }
                                else
                                {
                                    checkBox11.Checked = true;
                                    textBox16.Text = split[0];
                                    comboBox11.Text = split[1];
                                    textBox11.Text = split[2];
                                }
                                break;
                            case 11:
                                if (split[0] == "#")
                                {
                                    checkBox12.Checked = false;
                                    textBox12.Text = split[1];
                                    comboBox12.Text = split[2];
                                    textBox12.Text = split[3];
                                }
                                else
                                {
                                    checkBox12.Checked = true;
                                    textBox12.Text = split[0];
                                    comboBox12.Text = split[1];
                                    textBox12.Text = split[2];
                                }
                                break;
                            case 12:
                                if (split[0] == "#")
                                {
                                    checkBox13.Checked = false;
                                    textBox13.Text = split[1];
                                    comboBox13.Text = split[2];
                                    textBox13.Text = split[3];
                                }
                                else
                                {
                                    checkBox13.Checked = true;
                                    textBox13.Text = split[0];
                                    comboBox13.Text = split[1];
                                    textBox13.Text = split[2];
                                }
                                break;
                            case 13:
                                if (split[0] == "#")
                                {
                                    checkBox14.Checked = false;
                                    textBox14.Text = split[1];
                                    comboBox14.Text = split[2];
                                    textBox14.Text = split[3];
                                }
                                else
                                {
                                    checkBox14.Checked = true;
                                    textBox14.Text = split[0];
                                    comboBox14.Text = split[1];
                                    textBox14.Text = split[2];
                                }
                                break;
                            case 14:
                                if (split[0] == "#")
                                {
                                    checkBox15.Checked = false;
                                    textBox15.Text = split[1];
                                    comboBox15.Text = split[2];
                                    textBox15.Text = split[3];
                                }
                                else
                                {
                                    checkBox15.Checked = true;
                                    textBox15.Text = split[0];
                                    comboBox15.Text = split[1];
                                    textBox15.Text = split[2];
                                }
                                break;
                        }
                    }
                    x++;
                }
                Database.WriteLog("Loading Start Hour");
                shour1.Text = Database.Time[0];
                shour2.Text = Database.Time[1];
                shour3.Text = Database.Time[2];
                shour4.Text = Database.Time[3];
                shour5.Text = Database.Time[4];
                shour6.Text = Database.Time[5];
                shour7.Text = Database.Time[6];
                shour8.Text = Database.Time[7];
                shour9.Text = Database.Time[8];
                shour10.Text = Database.Time[9];
                shour11.Text = Database.Time[10];
                shour12.Text = Database.Time[11];
                shour13.Text = Database.Time[12];
                shour14.Text = Database.Time[13];
                shour15.Text = Database.Time[14];
                Database.WriteLog("Loading End Hour");
                Database.loadingprocess = 30;
                ehour1.Text = Database.Time[15];
                ehour2.Text = Database.Time[16];
                ehour3.Text = Database.Time[17];
                ehour4.Text = Database.Time[18];
                ehour5.Text = Database.Time[19];
                ehour6.Text = Database.Time[20];
                ehour7.Text = Database.Time[21];
                ehour8.Text = Database.Time[22];
                ehour9.Text = Database.Time[23];
                ehour10.Text = Database.Time[24];
                ehour11.Text = Database.Time[25];
                ehour12.Text = Database.Time[26];
                ehour13.Text = Database.Time[27];
                ehour14.Text = Database.Time[28];
                ehour15.Text = Database.Time[29];
                Database.WriteLog("Loading Start Minute");
                smin1.Text = Database.Time[30];
                smin2.Text = Database.Time[31];
                smin3.Text = Database.Time[32];
                smin4.Text = Database.Time[33];
                smin5.Text = Database.Time[34];
                smin6.Text = Database.Time[35];
                smin7.Text = Database.Time[36];
                smin8.Text = Database.Time[37];
                smin9.Text = Database.Time[38];
                smin10.Text = Database.Time[39];
                smin11.Text = Database.Time[40];
                smin12.Text = Database.Time[41];
                smin13.Text = Database.Time[42];
                smin14.Text = Database.Time[43];
                smin15.Text = Database.Time[44];
                Database.WriteLog("Loading End Minute");
                Database.loadingprocess = 40;
                emin1.Text = Database.Time[45];
                emin2.Text = Database.Time[46];
                emin3.Text = Database.Time[47];
                emin4.Text = Database.Time[48];
                emin5.Text = Database.Time[49];
                emin6.Text = Database.Time[50];
                emin7.Text = Database.Time[51];
                emin8.Text = Database.Time[52];
                emin9.Text = Database.Time[53];
                emin10.Text = Database.Time[54];
                emin11.Text = Database.Time[55];
                emin12.Text = Database.Time[56];
                emin13.Text = Database.Time[57];
                emin14.Text = Database.Time[58];
                emin15.Text = Database.Time[59];
                Database.WriteLog("Loading Other Software");
                textBox108.Text = Database.OtherBot[0];
                textBox109.Text = Database.OtherBot[1];
                textBox110.Text = Database.OtherBot[2];
                textBox111.Text = Database.OtherBot[3];
                textBox112.Text = Database.OtherBot[4];
                textBox113.Text = Database.OtherBot[5];
                textBox114.Text = Database.OtherBot[6];
                textBox115.Text = Database.OtherBot[7];
                textBox116.Text = Database.OtherBot[8];
                textBox117.Text = Database.OtherBot[9];
                textBox118.Text = Database.OtherBot[10];
                textBox119.Text = Database.OtherBot[11];
                Thread.Sleep(100);
                Database.loadingprocess = 50;
                Database.WriteLog("Loading Other Software Time");
                textBox131.Text = Database.ProTime[0];
                textBox143.Text = Database.ProTime[1];
                textBox130.Text = Database.ProTime[2];
                textBox142.Text = Database.ProTime[3];
                textBox129.Text = Database.ProTime[4];
                textBox141.Text = Database.ProTime[5];
                textBox128.Text = Database.ProTime[6];
                textBox140.Text = Database.ProTime[7];
                textBox127.Text = Database.ProTime[8];
                textBox139.Text = Database.ProTime[9];
                textBox126.Text = Database.ProTime[10];
                textBox138.Text = Database.ProTime[11];
                textBox125.Text = Database.ProTime[12];
                textBox137.Text = Database.ProTime[13];
                textBox124.Text = Database.ProTime[14];
                textBox136.Text = Database.ProTime[15];
                textBox123.Text = Database.ProTime[16];
                textBox135.Text = Database.ProTime[17];
                textBox122.Text = Database.ProTime[18];
                textBox134.Text = Database.ProTime[19];
                textBox121.Text = Database.ProTime[20];
                textBox133.Text = Database.ProTime[21];
                textBox120.Text = Database.ProTime[22];
                textBox132.Text = Database.ProTime[23];
                textBox155.Text = Database.ProTime[24];
                textBox167.Text = Database.ProTime[25];
                textBox154.Text = Database.ProTime[26];
                textBox166.Text = Database.ProTime[27];
                textBox153.Text = Database.ProTime[28];
                textBox165.Text = Database.ProTime[29];
                textBox152.Text = Database.ProTime[30];
                textBox164.Text = Database.ProTime[31];
                textBox151.Text = Database.ProTime[32];
                textBox163.Text = Database.ProTime[33];
                textBox150.Text = Database.ProTime[34];
                textBox162.Text = Database.ProTime[35];
                textBox149.Text = Database.ProTime[36];
                textBox161.Text = Database.ProTime[37];
                textBox148.Text = Database.ProTime[38];
                textBox160.Text = Database.ProTime[39];
                textBox147.Text = Database.ProTime[40];
                textBox159.Text = Database.ProTime[41];
                textBox146.Text = Database.ProTime[42];
                textBox158.Text = Database.ProTime[43];
                textBox145.Text = Database.ProTime[44];
                textBox157.Text = Database.ProTime[45];
                textBox144.Text = Database.ProTime[46];
                textBox156.Text = Database.ProTime[47];
                textBox143.Text = Database.ProTime[48];
                textBox172.Text = Database.Bot[21];
                textBox173.Text = Database.Bot[22];
                Database.loadingprocess = 60;
                Database.WriteLog("Loading CPU Settings");
                CPU_over.Text = Database.Time[95];
                CPU_Normal.Text = Database.Time[96];
                if(Database.Time[79] == "1")
                {
                    comboBox18.SelectedIndex = 1;
                    ChangeUsingTemp = true;
                }
                else
                {
                    comboBox18.SelectedIndex = 0;
                    ChangeUsingTemp = false;
                }
                if (Database.Time[80] == "1")
                {
                    checkBox17.Checked = true;
                    Advance = true;
                }
                if(Database.Time[81] == "1")
                {
                    MyBotHide.Checked = true;
                    Database.hide = true;
                }
                if(Database.Time[82] == "1")
                {
                    EmulatorHide.Checked = true;
                    Database.hideEmulator = true;
                }
                if(Database.Time[83] == "1")
                {
                    checkBox31.Checked = true;
                    Database.dock = true;
                }
                //Convert string into int
                try
                {
                    Database.OtherTime = Array.ConvertAll(Database.ProTime, int.Parse);
                    Database.Bot_Timer = Array.ConvertAll(Database.Time, int.Parse);
                }
                catch
                {

                }
                //Other Settings
                if (Convert.ToInt32(Database.Time[87]) == 1)
                {
                    Database.Language = "English";
                    englishToolStripMenuItem1.Visible = false;
                    中文ToolStripMenuItem1.Visible = true;
                }
                else
                {
                    Database.Language = "Chinese";
                    中文ToolStripMenuItem1.Visible = false;
                    englishToolStripMenuItem1.Visible = true;
                }
                if (Convert.ToInt32(Database.Time[93]) > 0)
                {
                    Priority.Checked = true;
                }
                if (Convert.ToInt32(Database.Time[84]) > 0)
                {
                    Shutdown.Checked = true;
                }
                Database.loadingprocess = 70;
                if (Convert.ToInt32(Database.Time[85]) != 134217728 && Convert.ToInt32(Database.Time[86]) != 40)
                {
                    QuotaLimit.Checked = true;
                    long tempvalue = Convert.ToInt64(Database.Time[85]) * Convert.ToInt64(Database.Time[86]);
                    try
                    {
                        tempvalue = tempvalue / 1024;
                        if (tempvalue >= 1024)
                        {
                            tempvalue = tempvalue / 1024;
                            Sizes.SelectedItem = "MB";
                            if (tempvalue >= 1024)
                            {
                                tempvalue = tempvalue / 1024;
                                Sizes.SelectedItem = "GB";
                                if (tempvalue >= 1024)
                                {
                                    tempvalue = tempvalue / 1024;
                                    Sizes.SelectedItem = "TB";
                                }
                            }
                        }
                        Limit.Value = tempvalue;
                    }
                    catch
                    {

                    }
                }
                if (!File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\MBR Global Variables(directX).au3"))
                {
                    if (Convert.ToInt32(Database.Time[88]) > 0)
                    {
                        CloseLID.Checked = true;
                    }
                    else
                    {
                        CloseLID.Checked = false;
                    }
                }
                else
                {
                    CloseLID.Checked = false;
                    CloseLID.Enabled = false;
                    CloseLID.Text = "Force DirectX MOD enabled";
                }
                if (Convert.ToInt32(Database.Time[89]) > 0)
                {
                    ShutdownWhenLimitReached.Checked = true;
                }
                else
                {
                    ShutdownWhenLimitReached.Checked = false;
                }
                if (Convert.ToInt32(Database.Time[90]) == 444)
                {
                    DisableMoney.Checked = false;
                }
                else
                {
                    DisableMoney.Checked = true;
                }
                if (Convert.ToInt32(Database.Time[91]) > 24 && Convert.ToInt32(Database.Time[92]) > 60)
                {
                    Scheduledshutdown.Checked = false;
                }
                else
                {
                    Scheduledshutdown.Checked = true;
                    ShutdownTimeHour.Text = Database.Time[91];
                    ShutdownTimeMinute.Text = Database.Time[92];
                }
                if (Convert.ToInt32(Database.Time[94]) == 108)
                {
                    RestartImm.Checked = true;
                }
                else
                {
                    RestartImm.Checked = false;
                }
                Database.WriteLog("Loading PowerConfig");
                Database.loadingprocess = 80;
                Thread.Sleep(500);
                try
                {
                    Database.SleepOnBattery = Convert.ToInt32(Database.Time[97]);
                    Database.SleepOnPower = Convert.ToInt32(Database.Time[98]);
                    Database.ScreenOnBattery = Convert.ToInt32(Database.Time[99]);
                    Database.ScreenOnPower = Convert.ToInt32(Database.Time[100]);
                }
                catch
                {
                    Database.SleepOnBattery = 0;
                    Database.SleepOnPower = 0;
                    Database.ScreenOnBattery = 0;
                    Database.ScreenOnPower = 0;
                }
                ScreenOnBatterySize.SelectedIndex = 0;
                ScreenOnPowerSize.SelectedIndex = 0;
                SleepOnBatterySize.SelectedIndex = 0;
                SleepOnPowerSize.SelectedIndex = 0;
                int ScreenOnBattery_ = Database.ScreenOnBattery;
                int ScreenOnPower_ = Database.ScreenOnPower;
                int SleepOnBattery_ = Database.SleepOnBattery;
                int SleepOnPower_ = Database.SleepOnPower;
                if (ScreenOnBattery_ > 59)
                {
                    ScreenOnBattery_ /= 60;
                    ScreenOnBatterySize.SelectedIndex = 1;
                    if (ScreenOnBattery_ > 59)
                    {
                        ScreenOnBattery_ /= 60;
                        ScreenOnBatterySize.SelectedIndex = 2;
                    }
                }
                ScreenOnBattery.Text = ScreenOnBattery_.ToString();
                if (ScreenOnPower_ > 59)
                {
                    ScreenOnPower_ /= 60;
                    ScreenOnPowerSize.SelectedIndex = 1;
                    if (ScreenOnPower_ > 59)
                    {
                        ScreenOnPower_ /= 60;
                        ScreenOnPowerSize.SelectedIndex = 2;
                    }
                }
                ScreenOnPower.Text = ScreenOnPower_.ToString();
                if (SleepOnBattery_ > 59)
                {
                    SleepOnBattery_ /= 60;
                    SleepOnBatterySize.SelectedIndex = 1;
                    if (SleepOnBattery_ > 59)
                    {
                        SleepOnBattery_ /= 60;
                        SleepOnBatterySize.SelectedIndex = 2;
                    }
                }
                SleepOnBattery.Text = SleepOnBattery_.ToString();
                if (SleepOnPower_ > 59)
                {
                    SleepOnPower_ /= 60;
                    SleepOnPowerSize.SelectedIndex = 1;
                    if (SleepOnPower_ > 59)
                    {
                        SleepOnPower_ /= 60;
                        SleepOnPowerSize.SelectedIndex = 2;
                    }
                }
                SleepOnPower.Text = SleepOnPower_.ToString();
                //ProfileSync
                Database.loadingprocess = 90;
                int botcount = 0;
                int disabledbotcount = 0;
                string[] Disabled = { };
                Array.Resize(ref Disabled, 24);
                foreach (var bot in Database.Bot)
                {
                    try
                    {
                        if (bot.Length > 3)
                        {
                            botcount++;
                            var temp = bot.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                            if (temp.Contains("#"))
                            {
                                Disabled[disabledbotcount] = bot;
                                disabledbotcount++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Array.Resize(ref Database.Bot, 44);
                        File.WriteAllText("error.log", ex.ToString());
                    }
                }
                int will;
                try
                {
                    will = botcount - disabledbotcount;
                }
                catch
                {
                    will = 0;
                }
                if (Database.Time[102] == "1")
                {
                    HourSetting.Checked = true;
                }
                else
                {
                    HourSetting.Checked = false;
                }
                SetOtherNET(checkBox19, 49);
                SetOtherNET(checkBox20, 50);
                SetOtherNET(checkBox22, 51);
                SetOtherNET(checkBox21, 52);
                SetOtherNET(checkBox24, 53);
                SetOtherNET(checkBox23, 54);
                SetOtherNET(checkBox26, 55);
                SetOtherNET(checkBox28, 56);
                SetOtherNET(checkBox27, 57);
                SetOtherNET(checkBox30, 58);
                SetOtherNET(checkBox29, 59);
                //numericUpDown1.Value = Convert.ToInt32(Database.Time[103]);
                if (Convert.ToInt32(Database.Time[104]) < 36)
                {
                    Database.Time[104] = "36";
                }
                trackBar3.Value = Convert.ToInt32(Database.Time[104]);
                TBot.Time = Convert.ToInt32(Database.Time[104]);
                if (Database.Time[105] == "1")
                {
                    checkBox32.Checked = true;
                }
                else
                {
                    checkBox32.Checked = false;
                }
                if (Database.Time[106] == "1")
                {
                    checkBox33.Checked = true;
                }
                else
                {
                    checkBox33.Checked = false;
                }
                if (Database.Time[107] == "1")
                {
                    checkBox34.Checked = true;
                }
                else
                {
                    checkBox34.Checked = false;
                }
                if (Database.Time[108] == "1")
                {
                    checkBox35.Checked = true;
                }
                else
                {
                    checkBox35.Checked = false;
                }
                if (Database.Time[109] == "1")
                {
                    checkBox36.Checked = true;
                }
                else
                {
                    checkBox36.Checked = false;
                }
                if (Database.Time[110] == "1")
                {
                    checkBox37.Checked = true;
                }
                else
                {
                    checkBox37.Checked = false;
                }
                if (Database.Time[111] == "1")
                {
                    checkBox38.Checked = true;
                }
                else
                {
                    checkBox38.Checked = false;
                }
                if (Database.Time[112] == "1")
                {
                    checkBox39.Checked = true;
                }
                else
                {
                    checkBox39.Checked = false;
                }
                if (Database.Time[113] == "1")
                {
                    checkBox40.Checked = true;
                }
                else
                {
                    checkBox40.Checked = false;
                }
                if (Database.Time[114] == "1")
                {
                    checkBox42.Checked = true;
                }
                else
                {
                    checkBox42.Checked = false;
                }
                TBot.cid = Convert.ToInt32(Database.Time[115]);
                if (Database.Time[116] == "1")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                for (x = 117; x < 138; x++)
                {
                    try
                    {
                        int temp = Convert.ToInt32(Database.Time[x]);
                        if (temp < 6)
                        {
                            Database.Time[x] = "11";
                        }
                    }
                    catch
                    {
                        Database.Time[x] = "11";
                    }
                }
            }
            catch (Exception ex)
            {
                Array.Resize(ref Database.Time, 138);
                Array.Resize(ref Database.Bot, 44);
                Array.Resize(ref Database.OtherBot, 12);
                Array.Resize(ref Database.ProTime, 62);
                Database.WriteLog(ex.ToString());
                SetTextBox();
            }
        }
        private void 中文ToolStripMenuItem_Click(object sender, EventArgs e)//Change language to CN
        {
            //Languages.TranslateLanguage("CN");
            中文ToolStripMenuItem1.Visible = false;
            englishToolStripMenuItem1.Visible = true;
            Database.Language = "Chinese";
            Language();
        }
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)//Change Language to EN
        {

            //Languages.TranslateLanguage("EN");
            中文ToolStripMenuItem1.Visible = true;
            englishToolStripMenuItem1.Visible = false;
            Database.Language = "English";
            Language();
        }
        private void OtherNET(CheckBox c, int arraynum)//Scan for other program need network to run or not and save it into RAM
        {
            if (c.Checked == true)
            {
                Database.OtherNet[arraynum] = 1;
            }
            else
            {
                Database.OtherNet[arraynum] = 0;
            }
        }
        private void SetOtherNET(CheckBox c, int arraynum)//Read data from RAM and set other program need network to run or not checkbox
        {
            if (Database.ProTime[arraynum] == "1")
            {
                c.Checked = true;
            }
            else
            {
                c.Checked = false;
            }
        }
        protected static void CalSize() //Convert Database.Send and received bytes to kb/mb/gb
        {
            if (Database.Receive > 1024)
            {
                Database.Receive_Print = Database.Receive / 1024;
                Database.Receive_size = "KB";
                if (Database.Receive_Print > 1024)
                {
                    Database.Receive_Print = Database.Receive_Print / 1024;
                    Database.Receive_size = "MB";
                    if (Database.Receive_Print > 1024)
                    {
                        Database.Receive_Print = Database.Receive_Print / 1024;
                        Database.Receive_size = "GB";
                    }
                }
            }
            else
            {
                Database.Receive_Print = Database.Receive;
                Database.Receive_size = "bytes";
            }
            if (Database.Send > 1024)
            {
                Database.Send_Print = Database.Send / 1024;
                Database.Send_size = "KB";
                if (Database.Send_Print > 1024)
                {
                    Database.Send_Print = Database.Send_Print / 1024;
                    Database.Send_size = "MB";
                    if (Database.Send_Print > 1024)
                    {
                        Database.Send_Print = Database.Send_Print / 1024;
                        Database.Send_size = "GB";
                    }
                }
            }
            else
            {
                Database.Send_Print = Database.Send;
                Database.Send_size = "bytes";
            }
            //Internet Speed

            Database.showr = Database.showr / 1024;
            Database.rsize = "KB/s";
            if (Database.showr > 1024)
            {
                Database.showr = Database.showr / 1024;
                Database.rsize = "MB/s";
            }
            Database.shows = Database.shows / 1024;
            Database.ssize = "KB/s";
            if (Database.shows > 1024)
            {
                Database.shows = Database.shows / 1024;
                Database.ssize = "MB/s";
            }
        }
        private static void Priority_Setter() //Set Priority when PC not in use if enabled
        {
            Database.WriteLog("Setting Priority");
            //Console.WriteLine("Priority Started");
            if (Database.Priority > 0)
            {
                if (Win32.GetIdleTime() > 60000)
                {
                    foreach (var bot in Process.GetProcessesByName("MyBot.run"))
                    {
                        try
                        {
                            if (bot.BasePriority != 13)
                            {
                                bot.PriorityClass = ProcessPriorityClass.High;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    foreach (var Android in Database.Emulator)
                    {
                        if (Android != null)
                        {
                            foreach (var Emulator in Process.GetProcessesByName(Android))
                            {
                                try
                                {

                                    if (Emulator.BasePriority != 13)
                                    {
                                        Emulator.PriorityClass = ProcessPriorityClass.High;
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }

                    }
                    foreach (var process in Process.GetProcesses())
                    {
                        if (!FindExceptionProcess(process.ProcessName) && !Database.Emulator.Contains(process.ProcessName) && process.ProcessName != "MyBot.run" && process.ProcessName != "Taskmgr")
                        {
                            try
                            {
                                if (process.BasePriority != 4)
                                {
                                    process.PriorityClass = ProcessPriorityClass.Idle;
                                }
                            }
                            catch
                            {
                                ExceptionProcessName.Add(process.ProcessName);
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var process in Process.GetProcesses())
                    {
                        try
                        {
                            if (!FindExceptionProcess(process.ProcessName) && process.BasePriority != 8)
                            {
                                process.PriorityClass = ProcessPriorityClass.Normal;
                            }
                        }
                        catch
                        {
                            ExceptionProcessName.Add(process.ProcessName);
                            continue;
                        }

                    }
                }
            }
            //Console.WriteLine("Priority Ended");
        }
        private static bool FindExceptionProcess(string name)//Find the program that are running which would not accept Priority set or Closing call from Supporter
        {
            bool x = false;
            foreach (var e in ExceptionProcessName)
            {
                if (e.Contains(name))
                {
                    x = true;
                }
            }
            return x;
        }
        public void CheckOtherProgram()//Make sure the program setted in Other program autorun is exe type and the path is existed
        {
            Array.Resize(ref Database.OtherID, 12);
            int X = 0;
            foreach (var running in Database.OtherID)
            {
                try
                {
                    Process.GetProcessById(running);
                }
                catch
                {
                    Database.OtherID[X] = 0;
                }
                X++;
            }
            X = 0;
            foreach (string path in Database.OtherBot)
            {
                string[] ExtraParCheck = path.Split('-');
                if (path.Length > 3)
                {
                    Database.WriteLog("Checking " + path + " is runnable");
                    if (!File.Exists(ExtraParCheck[0]))
                    {
                        Database.WriteLog(ExtraParCheck[0] + " is not found, disabling startup");
                        string message = "";
                        string caption = "";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        DialogResult result;
                        switch (Database.Language)
                        {
                            case "Chinese":
                                message = cn_Lang.UnableToLocate + ExtraParCheck[0] + cn_Lang.CancelStartOtherPrgram;
                                caption = cn_Lang.Error;
                                break;
                            case "English":
                                message = "Unable to locate " + ExtraParCheck[0] + " software! Supporter will disable starting it!";
                                caption = "Error";
                                break;
                        }
                        result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        Database.OtherBot[X] = "";
                        SetTextBox();
                    }
                    string last = ExtraParCheck[0].Split('\\').Last();
                    if (!last.Contains(".exe"))
                    {
                        Database.WriteLog(path + " is not an exe type program, disabling startup");
                        Database.OtherBot[X] = "";
                        SetTextBox();
                        switch (Database.Language)
                        {
                            case "Chinese":
                                MessageBox.Show(cn_Lang.DisallowOtherThanEXE, cn_Lang.Notification, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            case "English":
                                MessageBox.Show("No other than *.exe files are allowed to autorun!!", "No!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
                X++;
            }
        }
        private void MainWorker()//Main Process
        {
            while (Run == true)
            {
                if (CloseLID.Checked)
                {
                    Win32.Power_Idle();
                }
                else
                {
                    Win32.Power_MainScreen();
                }
                if (Database.Network == true)
                {
                    if (Database.OnBattery == false)
                    {
                        Botting.Bot();
                        Botting.Other();
                        Priority_Setter();
                    }
                    else
                    {
                        int x = 0;
                        foreach (var running in Botting.IsRunning)
                        {
                            Botting.IsRunning[x] = false;
                            Database.ID[x] = 0;
                            x++;
                        }
                        KillMyBot();
                    }
                }
                else //No internet connection is found
                {
                    Thread.Sleep(1000);
                    Console.Beep();
                    int x = 0;
                    foreach (var running in Botting.IsRunning)
                    {
                        Botting.IsRunning[x] = false;
                        Database.ID[x] = 0;
                        x++;
                    }
                    KillMyBot();
                    if (Database.Shutdown == 1) //If Shutdown when no Internet is avabile is on
                    {
                        if (Database.Net_Error > 0) //if Program just start for 10 seconds
                        {
                            if (Database.Language == "English")
                            {
                                string message = "Shutdown when no internet avabile but Supporter is just started, please check your internet connections";
                                string caption = "Error";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;
                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                Object sender = new Object();
                                EventArgs e = new EventArgs();
                                button1_Click(sender, e);
                            }
                            else
                            {
                                string message = "无网络关机系统已启动但是程序刚刚运行，请确保网络连接正常！";
                                string caption = "错误";
                                MessageBoxButtons buttons = MessageBoxButtons.OK;
                                DialogResult result;
                                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                                Object sender = new Object();
                                EventArgs e = new EventArgs();
                                button1_Click(sender, e);
                            }

                        }
                        else
                        {
                            Process.Start("shutdown.exe", "/s /t 00"); //Shutdown if Program had been no internet for 10 seconds
                            Close();
                        }
                    }
                }
                Botting.Other();
                try
                {
                    Database.WriteLog("Checking AutoIT Error");
                    IntPtr AutoITError = Win32.FindWindow("#32770", null);

                    if (!AutoITError.Equals(IntPtr.Zero))
                    {
                        Win32.STRINGBUFFER sLimitedLengthWindowTitle;
                        Win32.GetWindowText(AutoITError, out sLimitedLengthWindowTitle, 256);
                        String sWindowTitle = sLimitedLengthWindowTitle.szText;
                        if (sWindowTitle.Length > 0)
                        {
                            if (sWindowTitle.StartsWith("AutoIT Error"))
                            {
                                Database.WriteLog("AutoIT Error Found");
                                Win32.SendMessage(AutoITError, 0x0112, 0xF060, null);
                                Botting.Error[0] = true;
                                AutoIT++;
                            }
                            else if (sWindowTitle.StartsWith(".NET-BroadcastEventWindow"))
                            {
                                Database.WriteLog(".NET-BroadcastEventWindow Error Found");
                                Win32.SendMessage(AutoITError, 0x0010, 0, null);
                                Botting.Error[0] = true;
                                AutoIT++;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }
        private void ExtraParameters_Setup() //Load all settings from saved datain roaming folder
        {
            Database.WriteLog("Setup Environment");
            Database.Net_Error = 10;
            GC.Collect();
            Array.Resize(ref Database.ID, 21);
            Array.Resize(ref Database.hWnd, 21);
            Array.Resize(ref Database.OtherTime, 49);
            Array.Resize(ref Database.OtherBot, 12);
            Array.Resize(ref Database.OtherID, 12);
            Array.Resize(ref Database.Bot_Timer, 103);
            try
            {
                Win32.PowerMagement();
            }
            catch (Exception ex)
            {
                File.WriteAllText("error.log", ex.ToString());
            }
            try
            {
                Database.Bot = File.ReadAllLines(Database.Location + "botsave");
                int botnum = 0;
                Array.Resize(ref Database.Emulator, 6);
                foreach (var bots in Database.Bot)
                {
                    string[] Temp = bots.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                    if (Temp.Contains("#")) //If there is disable launch for specific profile
                    {
                        Database.Bot[botnum] = ""; //Remove the whole extra launch parameters for disable launch
                    }
                    botnum++;
                }
            }
            catch (DirectoryNotFoundException exception)
            {
                if (Database.Language == "English")
                {
                    string message = "botsave not found! Unable to continue!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }
                else
                {
                    string message = "找不到自动创建的botsave数据库！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }//Critical Error on creating dlls
            }
            catch (Exception exception)
            {
                if (Database.Language == "English")
                {
                    string message = "botsave read failed!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
                else
                {
                    string message = "botsave数据库读取失败！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }//Critical error found while reading DLLs
            }
            try
            {
                string[] Temp = File.ReadAllLines(Database.Location + "timesave");
                Database.Bot_Timer = Array.ConvertAll(Temp, s => int.Parse(s));
                Database.Shutdown = Database.Bot_Timer[84];
                long Tempvalue = Database.Bot_Timer[85] * Database.Bot_Timer[86];
                Database.Limit = Tempvalue * 1024;
                Database.ShutdownWhenLimitReached = Database.Bot_Timer[89];
                Database.DisableMoney = Database.Bot_Timer[90];
                //Database.ScheduledshutdownHour = Database.Bot_Timer[91];
                //Database.ScheduledshutdownMinute = Database.Bot_Timer[92];
                Database.Priority = Database.Bot_Timer[93];
            }
            catch (DirectoryNotFoundException exception)
            {
                if (Database.Language == "English")
                {
                    string message = "timesave not found! Unable to continue!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }
                else
                {
                    string message = "找不到自动创建的timesave数据库！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }//Critical Error on creating dlls
            }
            catch (Exception exception)
            {
                if (Database.Language == "English")
                {
                    string message = "timesave read failed!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
                else
                {
                    string message = "timesave数据库读取失败！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
            }

            try
            {
                Database.OtherBot = File.ReadAllLines(Database.Location + "programsave");
            }
            catch (DirectoryNotFoundException exception)
            {
                if (Database.Language == "English")
                {
                    string message = "programsave not found! Unable to continue!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }
                else
                {
                    string message = "找不到自动创建的programsave数据库！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }//Critical Error on creating dlls
            }
            catch (Exception exception)
            {
                if (Database.Language == "English")
                {
                    string message = "programsave read failed!";
                    string caption = "Error";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
                else
                {
                    string message = "programsave数据库读取失败！";
                    string caption = "错误";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
            }
            try
            {
                string[] Temp = File.ReadAllLines(Database.Location + "programtimesave");
                Database.OtherTime = Array.ConvertAll(Temp, s => int.Parse(s));
            }
            catch (DirectoryNotFoundException exception)
            {
                if (Database.Language == "English")
                {
                    string message = "programtimesave not found! Unable to continue!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }
                else
                {
                    string message = "找不到自动创建的programtimesave数据库！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                    Environment.Exit(0);
                }//Critical Error on creating dlls
            }
            catch (Exception exception)
            {
                if (Database.Language == "English")
                {
                    string message = "programtimesave read failed!";
                    string caption = en_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
                else
                {
                    string message = "programtimesave数据库读取失败！";
                    string caption = cn_Lang.Error;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    File.WriteAllText(@"error.log", exception.ToString());
                }
            }
            if (Database.DisableMoney == 888)
            {
                try
                {
                    Database.ResetHost(); //Reset Host for not adding too much host lines in the host file
                    Thread.Sleep(1000);
                    Database.No_Mining(); //Edit Host for blocking mining websites
                }
                catch (Exception ex)
                {
                    File.WriteAllText("error.log", ex.ToString());
                    Database.DisableMoney = 0;
                }
            }
            else
            {
                try
                {
                    Database.ResetHost(); //Reset Host if Blocking Mining Web is disabled
                }
                catch
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)//Show Form from hiding when Hide from Taskbar enabled
        {
            Show();
            showToolStripMenuItem.Visible = false;
        }

        private void EmulatorHide_CheckedChanged(object sender, EventArgs e)//To prevent conflict with docking MyBot and emulator
        {
            if (EmulatorHide.Checked == true)
            {
                Database.hideEmulator = true;
                checkBox31.Enabled = false;
            }
            else
            {
                Database.hideEmulator = false;
                checkBox31.Enabled = true;
            }
        }

        private void checkBox31_CheckedChanged(object sender, EventArgs e)//To prevent conflict with hiding emulator
        {
            if (checkBox31.Checked == true)
            {
                Database.dock = true;
                EmulatorHide.Enabled = false;
            }
            else
            {
                Database.dock = false;
                EmulatorHide.Enabled = true;
            }
        }

        private void button27_Click(object sender, EventArgs e)//call UI for csv writter
        {
            Database.loadingprocess = 50;
            Thread load = new Thread(Database.Load_);
            load.Start();
            Hide();
            CSVWriter csvwriter = new CSVWriter();
            csvwriter.Show();
            csvwriter.FormClosing += Inject_FormClosing;
        }

        //The functions of setting power saving mode or high performance or bla bla bla
        private void button38_Click(object sender, EventArgs e)
        {
            CPU_over.Value = 50;
            CPU_Normal.Value = 50;
            CloseLID.Checked = true;
            Priority.Checked = true;
            DisableMoney.Checked = true;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            CPU_over.Value = 90;
            CPU_Normal.Value = 50;
            CloseLID.Checked = false;
            Priority.Checked = true;
            DisableMoney.Checked = true;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            CPU_over.Value = 50;
            CPU_Normal.Value = 70;
            CloseLID.Checked = false;
            Priority.Checked = true;
            DisableMoney.Checked = true;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Win32.Processor_Min = 100;
            CPU_over.Value = 100;
            CPU_Normal.Value = 100;
            Win32.Power_Maximum();
        }

        private void button43_Click(object sender, EventArgs e)//Restart Telegram Thread to refresh Token and etc
        {
            try
            {
                telegram.Suspend();
            }
            catch
            {

            }
            TBot.API_Key = textBox172.Text;
            TBot.BotMessageThreadStop();
            telegram = new Thread(TBot.BotMessageThreadStart);
            telegram.Start();
        }

        private void QuotaLimit_CheckedChanged(object sender, EventArgs e)
        {
            if (QuotaLimit.Checked)
            {
                Limit.Visible = true;
                Sizes.Visible = true;
            }
            else
            {
                Limit.Visible = false;
                Sizes.Visible = false;
            }
        }

        private void Taskbar_CheckedChanged(object sender, EventArgs e)
        {
            Hide();
            if (Taskbar.Checked)
            {
                ShowInTaskbar = false;
            }
            else
            {
                ShowInTaskbar = true;
            }
            Show();
        }

        private void MyBotHide_CheckedChanged(object sender, EventArgs e)//Hide MyBot
        {
            if (MyBotHide.Checked)
            {
                Database.hide = true;
            }
            else
            {
                Database.hide = false;
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            Run = false;
            button1.BackColor = Color.Lime;
            switch (Database.Language)
            {
                case "English":
                    button1.Text = en_Lang.Start_Button;
                    break;
                case "Chinese":
                    button1.Text = cn_Lang.Start_Button;
                    break;
            }
        }

        private void MainScreen_KeyDown(object sender, KeyEventArgs e)//Shortcut key
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (panel3.Enabled == false)
                {
                    button1_Click(sender, e);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (panel3.Enabled == true)
                {
                    button1_Click(sender, e);
                }
                else
                {
                    Close();
                }

            }
            if (e.KeyCode == Keys.H)
            {
                if (panel3.Enabled == true)
                {
                    EmulatorHide.Checked = true;
                    MyBotHide.Checked = true;
                    checkBox31.Checked = false;
                }
            }
            if (e.KeyCode == Keys.S)
            {
                if (panel3.Enabled == true)
                {
                    EmulatorHide.Checked = false;
                    MyBotHide.Checked = false;
                    checkBox31.Checked = false;
                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                button36.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)//Read Profiles folder to get existed MyBot Pofiles and generate them into Supporter Profile
        {
            int x = 0;
            string[] Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            foreach (var profile in Profiles)
            {
                var name = profile.Remove(0, profile.LastIndexOf('\\') + 1);
                Database.Bot[x] = name;
                x++;
            }
            x = 0;
            foreach (var bot in Database.Bot)
            {
                if (bot.Contains("#"))
                {
                    Database.Bot[x] = "";
                }
                x++;
            }
            Form f = new GenerateProfile();
            f.Show();
            this.Enabled = false;
            f.FormClosing += F_FormClosing;
        }

        private void F_FormClosing(object sender, FormClosingEventArgs e)//Update UI to show the profiles generated
        {
            this.Enabled = true;
            SetTextBox();
        }

        private void TreeViewHandler()//Update treeview function
        {
            int x = 0;
            foreach (var bot in Database.Bot)
            {
                try
                {
                    IntPtr Control = Win32.FindWindowEx(Database.hWnd[x], IntPtr.Zero, null, "My Bot Controls");
                    IntPtr Control1 = Botting.GetAllChildrenWindowHandles(Control, 9)[8];
                    IntPtr Control2 = Botting.GetAllChildrenWindowHandles(Control1, 2)[1];
                    List<IntPtr> Child = Botting.GetAllChildrenWindowHandles(Control2, 64);
                    IntPtr GUI = Win32.FindWindowEx(Database.hWnd[x], IntPtr.Zero, null, "My Bot Buttons");
                    List<IntPtr> Child2 = Botting.GetAllChildrenWindowHandles(GUI, 55);
                    string[] botname = bot.Split(' ');
                    if (botname[0] != "#" || botname[0] != "")
                    {
                        if (Botting.IsRunning[x])
                        {
                            if (!TreeNode.Contains(botname[0]))
                            {
                                TreeNode node = new TreeNode();
                                node.Name = botname[0];
                                node.Text = botname[0];
                                treeView1.Invoke((MethodInvoker)delegate ()
                                {
                                    treeView1.Nodes.Add(node);
                                });
                                TreeNode.Add(botname[0]);
                                int y = TreeNode.IndexOf(botname[0]);
                                string gold = Botting.Gold[x].ToString("## ### ##0");
                                string elixir = Botting.Elixir[x].ToString("## ### ##0");
                                string dark = Botting.DarkElixir[x].ToString("### ##0");
                                string trophy = Botting.Trophy[x].ToString("# ##0");
                                string earng = "0";
                                string earne = "0";
                                string earnd = "0";
                                string earnt = "0";
                                string builders = "0";
                                string GoldPH = "0/h";
                                string ElixirPH = "0/h";
                                string DarkElixirPH = "0/h";
                                string TrophyPH = "0/h";
                                if (Child.Count > 63)
                                {
                                    earng = TBot.GetWindowTextRaw(Child[57]);
                                    earne = TBot.GetWindowTextRaw(Child[59]);
                                    earnd = TBot.GetWindowTextRaw(Child[61]);
                                    earnt = TBot.GetWindowTextRaw(Child[63]);
                                    builders = TBot.GetWindowTextRaw(Child2[54]);
                                    GoldPH = TBot.GetWindowTextRaw(Child[46]);
                                    ElixirPH = TBot.GetWindowTextRaw(Child[48]);
                                    DarkElixirPH = TBot.GetWindowTextRaw(Child[50]);
                                    TrophyPH = TBot.GetWindowTextRaw(Child[52]);
                                }
                                switch (Database.Language)
                                {
                                    case "Chinese":
                                        treeView1.Invoke((MethodInvoker)delegate ()
                                        {
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.Gold + gold);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.Elixir + elixir);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.DarkElixir + dark);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.Trophy + trophy);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.Builders + builders);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedGold + earng);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedElixir + earne);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedDarkElixir + earnd);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedTrophy + earnt);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedGoldPH + GoldPH);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedElixirPH + ElixirPH);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedDarkElixirPH + DarkElixirPH);
                                            treeView1.Nodes[y].Nodes.Add(cn_Lang.EarnedTrophyPH + TrophyPH);
                                        });
                                        break;
                                    case "English":
                                        treeView1.Invoke((MethodInvoker)delegate ()
                                        {
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.Gold + gold);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.Elixir + elixir);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.DarkElixir + dark);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.Trophy + trophy);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.Builders + builders);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedGold + earng);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedElixir + earne);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedDarkElixir + earnd);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedTrophy + earnt);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedGoldPH + GoldPH);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedElixirPH + ElixirPH);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedDarkElixirPH + DarkElixirPH);
                                            treeView1.Nodes[y].Nodes.Add(en_Lang.EarnedTrophyPH + TrophyPH);
                                        });
                                        break;
                                }
                            }
                            else
                            {
                                int y = TreeNode.IndexOf(botname[0]);
                                string gold = Botting.Gold[x].ToString("## ### ##0");
                                string elixir = Botting.Elixir[x].ToString("## ### ##0");
                                string dark = Botting.DarkElixir[x].ToString("### ##0");
                                string trophy = Botting.Trophy[x].ToString("# ##0");
                                string earng = TBot.GetWindowTextRaw(Child[57]);
                                string earne = TBot.GetWindowTextRaw(Child[59]);
                                string earnd = TBot.GetWindowTextRaw(Child[61]);
                                string earnt = TBot.GetWindowTextRaw(Child[63]);
                                string builders = TBot.GetWindowTextRaw(Child2[54]);
                                string GoldPH = TBot.GetWindowTextRaw(Child[46]);
                                string ElixirPH = TBot.GetWindowTextRaw(Child[48]);
                                string DarkElixirPH = TBot.GetWindowTextRaw(Child[50]);
                                string TrophyPH = TBot.GetWindowTextRaw(Child[52]);
                                switch (Database.Language)
                                {
                                    case "Chinese":
                                        treeView1.Invoke((MethodInvoker)delegate ()
                                        {
                                            treeView1.Nodes[y].Nodes[0].Text = cn_Lang.Gold + gold;
                                            treeView1.Nodes[y].Nodes[1].Text = cn_Lang.Elixir + elixir;
                                            treeView1.Nodes[y].Nodes[2].Text = cn_Lang.DarkElixir + dark;
                                            treeView1.Nodes[y].Nodes[3].Text = cn_Lang.Trophy + trophy;
                                            treeView1.Nodes[y].Nodes[4].Text = cn_Lang.Builders + builders;
                                            treeView1.Nodes[y].Nodes[5].Text = cn_Lang.EarnedGold + earng;
                                            treeView1.Nodes[y].Nodes[6].Text = cn_Lang.EarnedElixir + earne;
                                            treeView1.Nodes[y].Nodes[7].Text = cn_Lang.EarnedDarkElixir + earnd;
                                            treeView1.Nodes[y].Nodes[8].Text = cn_Lang.EarnedTrophy + earnt;
                                            treeView1.Nodes[y].Nodes[9].Text = cn_Lang.EarnedGoldPH + GoldPH;
                                            treeView1.Nodes[y].Nodes[10].Text = cn_Lang.EarnedElixirPH + ElixirPH;
                                            treeView1.Nodes[y].Nodes[11].Text = cn_Lang.EarnedDarkElixirPH + DarkElixirPH;
                                            treeView1.Nodes[y].Nodes[12].Text = cn_Lang.EarnedTrophyPH + TrophyPH;
                                        });
                                        break;
                                    case "English":
                                        treeView1.Invoke((MethodInvoker)delegate ()
                                        {
                                            treeView1.Nodes[y].Nodes[0].Text = en_Lang.Gold + gold;
                                            treeView1.Nodes[y].Nodes[1].Text = en_Lang.Elixir + elixir;
                                            treeView1.Nodes[y].Nodes[2].Text = en_Lang.DarkElixir + dark;
                                            treeView1.Nodes[y].Nodes[3].Text = en_Lang.Trophy + trophy;
                                            treeView1.Nodes[y].Nodes[4].Text = en_Lang.Builders + builders;
                                            treeView1.Nodes[y].Nodes[5].Text = en_Lang.EarnedGold + earng;
                                            treeView1.Nodes[y].Nodes[6].Text = en_Lang.EarnedElixir + earne;
                                            treeView1.Nodes[y].Nodes[7].Text = en_Lang.EarnedDarkElixir + earnd;
                                            treeView1.Nodes[y].Nodes[8].Text = en_Lang.EarnedTrophy + earnt;
                                            treeView1.Nodes[y].Nodes[9].Text = en_Lang.EarnedGoldPH + GoldPH;
                                            treeView1.Nodes[y].Nodes[10].Text = en_Lang.EarnedElixirPH + ElixirPH;
                                            treeView1.Nodes[y].Nodes[11].Text = en_Lang.EarnedDarkElixirPH + DarkElixirPH;
                                            treeView1.Nodes[y].Nodes[12].Text = en_Lang.EarnedTrophyPH + TrophyPH;
                                        });
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (TreeNode.Contains(botname[0]))
                            {
                                treeView1.Invoke((MethodInvoker)delegate ()
                                {
                                    int y = TreeNode.IndexOf(botname[0]);
                                    treeView1.Nodes[y].Remove();
                                });
                            }
                        }
                    }
                }
                catch
                {
                    continue;
                }
                x++;
            }
            treeView1.Invoke((MethodInvoker)delegate ()
            {
                treeView1.ExpandAll();
            });
        }
        private void Sizes_SelectedIndexChanged(object sender, EventArgs e)//Set the size
        {
            switch (Sizes.SelectedIndex)
            {
                case 0:
                    size = 1;
                    break;
                case 1:
                    size = 1024;
                    break;
                case 2:
                    size = 1024 * 1024;
                    break;
            }

        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Database.Hour = DateTime.Now.Hour; //Set Time
            Database.Min = DateTime.Now.Minute;
            Database.Sec = DateTime.Now.Second;
            int x = 0;
            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    try
                    {
                        AdvanceCPU[x] = hardware.HardwareType + " : " + hardware.Name;
                    }
                    catch
                    {

                    }
                    finally
                    {
                        x++;
                    }
                    foreach (var sensor in hardware.Sensors)
                    {
                        HardwareSensor(sensor, x, out x);
                    }
                    try
                    {
                        AdvanceCPU[x] = "=====================";
                    }
                    catch
                    {

                    }
                    finally
                    {
                        x++;
                    }

                }
                else if (hardware.HardwareType == HardwareType.RAM)
                {
                    hardware.Update();
                    try
                    {
                        AdvanceCPU[x] = hardware.HardwareType + " : " + hardware.Name;
                    }
                    catch
                    {

                    }
                    finally
                    {
                        x++;
                    }
                    foreach (var sensor in hardware.Sensors)
                    {
                        RAMSensor(sensor, x, out x);
                    }
                }
            }
            if (AdvanceCPU.Length < x)
            {
                Array.Resize(ref AdvanceCPU, x);

            }
            CPUL = Convert.ToInt16(CPU.NextValue());
            if (Database.Network)
            {
                try
                {
                    Database.Receive = nics.GetIPStatistics().BytesReceived; //Get Received nework data volume
                    Database.Send = nics.GetIPStatistics().BytesSent; //Get Sended network data volume
                    Database.newr = Database.Receive;
                    Database.news = Database.Send;
                    Database.showr = Database.newr - Database.oldr;
                    Database.shows = Database.news - Database.olds;
                    CalSize();
                    Database.oldr = Database.newr;
                    Database.olds = Database.news;
                }
                catch
                {

                }
            }
            if (!ChangeUsingTemp)
            {
                if (CPUL >= 70)
                {
                    if (Win32.ProcessorIsMaximum)
                    {
                        Thread set = new Thread(Win32.Power_Minimum);
                        set.Start();
                        Win32.ProcessorIsMaximum = false;
                    }
                }
                else
                {
                    if (!Win32.ProcessorIsMaximum)
                    {
                        Thread set = new Thread(Win32.Power_Maximum);
                        set.Start();
                        Win32.ProcessorIsMaximum = true;
                    }
                }
            }
            else
            {
                if (CPUT >= 60)
                {
                    if (Win32.ProcessorIsMaximum)
                    {
                        Thread set = new Thread(Win32.Power_Minimum);
                        set.Start();
                        Win32.ProcessorIsMaximum = false;
                    }
                }
                else
                {
                    if (!Win32.ProcessorIsMaximum)
                    {
                        Thread set = new Thread(Win32.Power_Maximum);
                        set.Start();
                        Win32.ProcessorIsMaximum = true;
                    }
                }
            }
            ReportError();
        }

        public static bool Pause = false;

        private void comboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox18.SelectedIndex == 1)
            {
                ChangeUsingTemp = true;
                switch (Database.Language)
                {
                    case "English":
                        label103.Text = en_Lang.IsOver60C_Set_Maximum;
                        break;
                    case "Chinese":
                        label103.Text = cn_Lang.IsOver60C_Set_Maximum;
                        break;
                }
            }
            else
            {
                ChangeUsingTemp = false;
                switch (Database.Language)
                {
                    case "English":
                        label103.Text = en_Lang.IsOver70_Set_Maximum;
                        break;
                    case "Chinese":
                        label103.Text = cn_Lang.IsOver70_Set_Maximum;
                        break;
                }
            }
            
        }

        private void Updator()
        {
            Database.WriteLog("Fetching Updates of MyBot.Supporter");
            try
            {
                Ping github = new Ping();
                var respond = github.Send("www.github.com").Status;
                if (respond == IPStatus.Success) //Github is usable
                {
                    Database.WriteLog("Use Github for checking");
                    try
                    {
                        string versionfile = "https://github.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/Version.txt";
                        WebClient wc = new WebClientOverride();
                        wc.Proxy = myProxy;
                        MyBotUpdator.NewestVersion = wc.DownloadString(new Uri(versionfile));
                        if (version != MyBotUpdator.NewestVersion)
                        {
                            if (Run)
                            {
                                object sender = new object();
                                EventArgs e = new EventArgs();
                                button1_Click(sender, e);
                                Run = false;
                                Pause = true;
                            }
                            Update = true;
                            this.Invoke(new Action(() =>Text = Text + " / Update Available: " + MyBotUpdator.NewestVersion));
                            button1.Invoke(new Action(() => button1.Enabled = false));
                            button1.Invoke(new Action(() => button1.Text = "Updating"));
                            button1.Invoke(new Action(() => button1.BackColor = Color.Yellow));
                            MyBotUpdator.Github = true;
                            Database.SupporterUpdate = true;
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MyBotUpdator update = new MyBotUpdator();
                                update.FormClosing += Update_FormClosing;
                                update.ShowDialog();
                            });
                        }
                    }
                    catch 
                    {

                    }
                }
                else //try using Gitee
                {
                    Database.WriteLog("Use Gitee for checking");
                    try
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                        string version = myFileVersionInfo.FileVersion;
                        string versionfile = "https://gitee.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/Version.txt";
                        WebClient wc = new WebClientOverride();
                        wc.Proxy = myProxy;
                        MyBotUpdator.NewestVersion = wc.DownloadString(new Uri(versionfile));
                        if (version != MyBotUpdator.NewestVersion)
                        {
                            if (Run)
                            {
                                object sender = new object();
                                EventArgs e = new EventArgs();
                                button1_Click(sender, e);
                                Run = false;
                                Pause = true;
                            }
                            Update = true;
                            this.Invoke(new Action(() => Text = Text + " / Update Available: " + MyBotUpdator.NewestVersion));
                            button1.Invoke(new Action(() => button1.Enabled = false));
                            button1.Invoke(new Action(() => button1.Text = "Updating"));
                            button1.Invoke(new Action(() => button1.BackColor = Color.Yellow));
                            Database.SupporterUpdate = true;
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MyBotUpdator update = new MyBotUpdator();
                                update.FormClosing += Update_FormClosing;
                                update.ShowDialog();
                            });
                        }
                    }
                    catch 
                    {

                    }
                }
            }
            catch
            {

            }
        }

        public void Update_FormClosing(object sender, FormClosingEventArgs e)
        {
            button1.Invoke(new Action(() => button1.Enabled = true));
            Database.SupporterUpdate = false;
            MyBotUpdator.Github = false;
            this.Invoke((MethodInvoker)delegate ()
            {
                if (Run)
                {
                    button1.Text = en_Lang.Stop_Button;
                    button1.BackColor = Color.Red;
                }
                else
                {
                    button1.Text = en_Lang.Start_Button;
                    button1.BackColor = Color.Lime;
                }
            });
            if (Pause)
            {
                object s = new object();
                EventArgs ev = new EventArgs();
                button1_Click(s, ev);
            }
        }

        private void UpdateMyBot()
        {
            ServicePointManager.DefaultConnectionLimit = ServicePointManager.DefaultConnectionLimit;
            Database.WriteLog("Checking Update for MyBot.Run");
            try
            {
                Ping github = new Ping();
                var respond = github.Send("www.github.com").Status;
                if (respond == IPStatus.Success) //Github is usable
                {
                    Database.WriteLog("Using Github for checking");
                    try
                    {
                        string version = "";
                        if (File.Exists("MyBot.Run.version.au3"))
                        {
                            version = File.ReadAllText("MyBot.Run.version.au3");
                        }
             
                        string versionfile = "https://github.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/MyBot.run.version.au3";
                        WebClient wc = new WebClientOverride();
                        wc.Proxy = myProxy;
                        string s = wc.DownloadString(new Uri(versionfile));
                        string compare1 = Database.getBetween(version, "$g_sBotVersion = \"","\"");
                        MyBotUpdator.MBNewestVersion = Database.getBetween(s, "$g_sBotVersion = \"", "\"");
                        if (compare1 != MyBotUpdator.MBNewestVersion)
                        {
                            UpdateMB = true;
                            this.Invoke(new Action(() => Text = Text + " / MyBot Update Available: " + MyBotUpdator.MBNewestVersion));
                            MyBotUpdator.Github = true;
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MyBotUpdator update = new MyBotUpdator();
                                update.FormClosing += Update_FormClosing;
                                update.ShowDialog();
                            });
                        }

                    }
                    catch 
                    {

                    }
                }
                else //try using Gitee
                {
                    Database.WriteLog("Using Gitee for checking");
                    try
                    {
                        string version = "";
                        if (File.Exists("MyBot.Run.version.au3"))
                        {
                            version = File.ReadAllText("MyBot.Run.version.au3");
                        }
                        string versionfile = "https://gitee.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/MyBot.run.version.au3";
                        WebClient wc = new WebClientOverride();
                        wc.Proxy = myProxy;
                        string s = wc.DownloadString(new Uri(versionfile));
                        string compare1 = Database.getBetween(version, "$g_sBotVersion = \"", "\"");
                        MyBotUpdator.MBNewestVersion = Database.getBetween(s, "$g_sBotVersion = \"", "\"");
                        if (compare1 != MyBotUpdator.MBNewestVersion)
                        {
                            UpdateMB = true;
                            this.Invoke(new Action(() => Text = Text + " / MyBot Update Available: " + MyBotUpdator.MBNewestVersion));
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MyBotUpdator update = new MyBotUpdator();
                                update.FormClosing += Update_FormClosing;
                                update.ShowDialog();
                            });
                            
                        }
                    }
                    catch 
                    {

                    }
                }              
            }
            catch
            {

            }
        }

        private static void KillMyBot()
        {
            foreach (var process in Process.GetProcesses())
            {
                switch (process.ProcessName)
                {
                    case "MyBot.run":
                    case "adb":
                    case "nox_adb":
                    case "MEmuHeadless":
                    case "MyBot.run.Watchdog":
                    case "MyBot.run.MiniGui":
                        try
                        {
                            Botting.KillProcessAndChildren(process.Id);
                        }
                        catch
                        {
                            continue;
                        }
                        break;
                }
            }
        }
        //Open file and let user choose an exe file to set into Other program AutoRun system
        #region OpenFileDialog
        private void OpenApplication(TextBox textbox)
        {
            Database.WriteLog("File Dialog Open");
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Applications|*.exe";
            openFile.Title = "!!!";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                textbox.Text = openFile.FileName;
            }
        }
        private void button35_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox108);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox109);
        }
        private void button33_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox110);
        }
        private void button32_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox111);
        }
        private void button31_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox112);
        }
        private void button30_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox113);
        }
        private void button29_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox114);
        }
        private void button28_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox115);
        }
        private void button26_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox116);
        }
        private void button24_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox117);
        }
        private void button23_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox118);
        }
        private void button22_Click(object sender, EventArgs e)
        {
            OpenApplication(textBox119);
        }
        #endregion
    }
    class Emulator
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, [Out] StringBuilder lParam);
        public static string[] Installed_Emulators;
        public static bool Emulator_Exists()//Scan Supporter Profiles and split it to read Emulator settings
        {
            int x = 0;
            bool t = true;
            Array.Resize(ref Installed_Emulators, 6);
            Array.Resize(ref Database.Emulator, 6);
            foreach (var bots in Database.Bot)
            {
                var Temp = bots.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
                if (Temp.Contains("MEmu"))
                {
                    Database.Emulator[0] = "MEmu";
                }
                if (Temp.Contains("Nox"))
                {
                    Database.Emulator[1] = "Nox";
                }
                if (Temp.Contains("Droid4X"))
                {
                    Database.Emulator[2] = "Droid4X";
                }
                if (Temp.Contains("Itools"))
                {
                    Database.Emulator[3] = "Itools";
                }
                if (Temp.Contains("Bluestacks"))
                {
                    Database.Emulator[4] = "Bluestacks";
                }
                if (Temp.Contains("Leapdroid"))
                {
                    Database.Emulator[5] = "Leapdroid";
                }
                if (bots.Length > 3 && x < 21 && Temp[0] != "#")
                {
                    if (!Directory.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles\\" + Temp[0]))
                    {
                        switch (Database.Language)
                        {
                            case "Chinese":
                                MessageBox.Show(cn_Lang.ProfileNotFound1 + Temp[0] + cn_Lang.ProfileNotFound2);
                                break;
                            case "English":
                                MessageBox.Show(cn_Lang.ProfileNotFound1 + Temp[0] + cn_Lang.ProfileNotFound2);
                                break;
                        }
                        t = false;
                        break;

                    }
                }
                x++;
            }
            return t;
        }
        /*public static List<Process> getFileProcesses(string strFile)
        {
            Process myProcess;
            List<Process> myProcessArray = new List<Process>();
            myProcessArray.Clear();
            Process[] processes = Process.GetProcesses();
            int i = 0;
            for (i = 0; i <= processes.GetUpperBound(0) - 1; i++)
            {
                myProcess = processes[i];
                //if (!myProcess.HasExited) //This will cause an "Access is denied" error
                if (myProcess.Threads.Count > 0)
                {
                    try
                    {
                        ProcessModuleCollection modules = myProcess.Modules;
                        int j = 0;
                        for (j = 0; j <= modules.Count - 1; j++)
                        {
                            if ((modules[j].FileName.ToLower().CompareTo(strFile.ToLower()) == 0))
                            {
                                myProcessArray.Add(myProcess);
                                break;
                                // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }

            return myProcessArray;
        }*/
    }
    
}


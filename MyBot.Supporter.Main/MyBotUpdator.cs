using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.IO.Compression;

namespace MyBot.Supporter.Main
{
    public partial class MyBotUpdator : Form
    {
        public static int Time = 5;
        public static bool IsDownloading = false, Github = false;
        public static string NewestVersion = "", MBNewestVersion = "", log = "Connecting...";
        public MyBotUpdator()
        {
            Database.loadingprocess = 100;
            InitializeComponent();
        }
        private void DownloadUpdate()//Github
        {
            try
            {
                string UpdateLink = "https://github.com/PoH98/MyBot.Supporter/releases/download/" + NewestVersion + "/MyBot.Supporter.Main.exe";
                using (var client = new WebClientOverride())
                {
                    client.DownloadFile(UpdateLink, Database.Location + "Update");
                }
                log = "Extracting...";
                File.Copy(Database.Location + "Update", Database.Location + "MyBot.Supporter.Main.exe");
                File.Delete(Database.Location + "Update");
                string[] copy = { "Taskkill /IM MyBot.Supporter.Main.exe /F", "xcopy /S /Y \"" + Database.Location + "MyBot.Supporter.Main.exe\" " + "\"" + Environment.CurrentDirectory + "\"", "del \"" + Database.Location + "MyBot.Supporter.Main.exe\"", "start MyBot.Supporter.Main.exe", "del Update.bat" };
                File.WriteAllLines("Update.bat", copy);
                log = "Completing...";
                Process.Start("Update.bat");
                Application.Exit();
            }
            catch (WebException)
            {
                TBot.debugger.SendTextMessageAsync(288027359,"Update your fxxking Github Release version please!");
                log = "Unable to connect to server";
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate
                {
                    Close();
                });
            }
            
        }

        private void DownloadUpdate2()//Gitee
        {
            try
            {
                using (var client = new WebClientOverride())
                {
                    string temp = client.DownloadString("https://gitee.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/UpdateLink.txt");
                    client.DownloadFile(temp, Database.Location + "Update");
                }
                log = "Extracting...";
                File.Copy(Database.Location + "Update", Database.Location + "MyBot.Supporter.Main.exe");
                File.Delete(Database.Location + "Update");
                string[] copy = { "Taskkill /IM MyBot.Supporter.Main.exe /F", "xcopy /S /Y \"" + Database.Location + "MyBot.Supporter.Main.exe\" " + "\"" + Environment.CurrentDirectory + "\"", "del \"" + Database.Location + "MyBot.Supporter.Main.exe\"", "start MyBot.Supporter.Main.exe", "del Update.bat" };
                File.WriteAllLines("Update.bat", copy);
                log = "Completing...";
                Process.Start("Update.bat");
                Application.Exit();
            }
            catch (WebException)
            {
                TBot.debugger.SendTextMessageAsync(288027359, "Update your fxxking gitee Release version please!");
                log = "Unable to connect to server";
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate
                {
                    Close();
                });
            }
           
        }

        public void DownloadMyBotUpdate()//Github
        {
            try
            {
                string UpdateLink = "https://codeload.github.com/MyBotRun/MyBot/zip/MBR_" + MBNewestVersion;
                using (var client = new WebClientOverride())
                {
                    client.DownloadFile(UpdateLink, Database.Location + "Update");
                }
                log = "Extracting...";
                File.Copy(Database.Location + "Update", Database.Location + "MyBot.Run.zip");
                File.Delete(Database.Location + "Update");
                Stream zipstream = new FileStream(Database.Location + "MyBot.Run.zip", FileMode.Open);
                ZipArchive archive = new ZipArchive(zipstream);
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.Combine(Environment.CurrentDirectory, file.FullName);
                    string directory = Path.GetDirectoryName(completeFileName);
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    if (file.Name != "")
                        file.ExtractToFile(completeFileName, true);
                }
                log = "Checking Customize MODs...";
                if (Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    string[] MOD = Directory.GetFiles(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                    if (MOD.Length > 0)
                    {
                        foreach (var f in MOD)
                        {
                            File.Delete(f);
                        }
                        string text = "";
                        string caption = "";
                        switch (Database.Language)
                        {
                            case "Chinese":
                                text = cn_Lang.CustomizeMODFound;
                                caption = cn_Lang.CustomizeMODFoundTitle;
                                break;
                            case "English":
                                text = en_Lang.CustomizeMODFound;
                                caption = en_Lang.CustomizeMODFoundTitle;
                                break;
                        }
                        DialogResult result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            MyBotSetter set = new MyBotSetter();
                            set.ShowDialog();
                        }
                    }
                }
                log = "Completing...";
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Completed", "Update Completed", MessageBoxButtons.OK);
                    Close();
                });
            }
            catch (WebException)
            {
                log = "Unable to connect to server";
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate
                {
                    Close();
                });
            }
        }

        private void DownloadMyBotUpdate2()//Gitee
        {
            try
            {
                using (var client = new WebClientOverride())
                {
                    string temp = client.DownloadString("https://gitee.com/PoH98/MyBot.Supporter/raw/master/Downloadable_Contents/MyBotUpdateLink.txt");
                    client.DownloadFile(temp, Database.Location + "Update");
                }
                log = "Extracting...";
                File.Copy(Database.Location + "Update", Database.Location + "MyBot.Run.zip");
                File.Delete(Database.Location + "Update");
                Stream zipstream = new FileStream(Database.Location + "MyBot.Run.zip", FileMode.Open);
                ZipArchive archive = new ZipArchive(zipstream);
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.Combine(Environment.CurrentDirectory, file.FullName);
                    string directory = Path.GetDirectoryName(completeFileName);
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    if (file.Name != "")
                        file.ExtractToFile(completeFileName, true);
                }
                log = "Checking Customize MODs...";
                if (Directory.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD"))
                {
                    string[] MOD = Directory.GetFiles(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD");
                    if (MOD.Length > 0)
                    {
                        foreach (var f in MOD)
                        {
                            File.Delete(f);
                        }
                        string text = "";
                        string caption = "";
                        switch (Database.Language)
                        {
                            case "Chinese":
                                text = cn_Lang.CustomizeMODFound;
                                caption = cn_Lang.CustomizeMODFoundTitle;
                                break;
                            case "English":
                                text = en_Lang.CustomizeMODFound;
                                caption = en_Lang.CustomizeMODFoundTitle;
                                break;
                        }
                        DialogResult result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            MyBotSetter set = new MyBotSetter();
                            set.ShowDialog();
                        }
                    }
                }
                log = "Completing...";
                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("Completed", "Update Completed", MessageBoxButtons.OK);
                    Close();
                });
            }
            catch (WebException)
            {
                log = "Unable to connect to server";
                Thread.Sleep(1000);
                this.Invoke((MethodInvoker)delegate
                {
                    Close();
                });
            }
        }

        private void MyBotUpdator_Load(object sender, EventArgs e)
        {
            if (File.Exists(Database.Location + "MyBot.Supporter.Main.exe"))
            {
                File.Delete(Database.Location + "MyBot.Supporter.Main.exe");
            }
            if (File.Exists(Database.Location + "MyBot.Run.zip"))
            {
                File.Delete(Database.Location + "MyBot.Run.zip");
            }
            if (Database.SupporterUpdate)
            {
                switch (Database.Language)
                {
                    case "Chinese":
                        Text = "发现新版本管理器！版本号："+NewestVersion;
                        label1.Text = cn_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = cn_Lang.UpdateNow;
                        button2.Text = cn_Lang.Cancel;
                        break;
                    case "English":
                        Text = "New version of Supporter found! Version: " + NewestVersion;
                        label1.Text = en_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = en_Lang.UpdateNow;
                        button2.Text = en_Lang.Cancel;
                        break;
                }
            }
            else
            {
                switch (Database.Language)
                {
                    case "Chinese":
                        Text = "发现新版本MyBot.Run！版本号："+MBNewestVersion;
                        label1.Text = cn_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = cn_Lang.UpdateNow;
                        button2.Text = cn_Lang.Cancel;
                        break;
                    case "English":
                        Text = "New version of MyBot.Run found! Version: " + MBNewestVersion;
                        label1.Text = en_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = en_Lang.UpdateNow;
                        button2.Text = en_Lang.Cancel;
                        break;
                }
            }
            Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Focus();
            if (IsDownloading)
            {
                if(File.Exists(Database.Location + "Update"))
                {
                    long bytes = new FileInfo(Database.Location + "Update").Length;
                    label2.Text = bytes + " bytes";
                }
                else
                {
                    label2.Text = log;
                }
            }
            else
            {
                Time--;
                switch (Database.Language)
                {
                    case "English":
                        label2.Text = "Cancel Update in " + Time + " second(s)";
                        break;
                    case "Chinese":
                        label2.Text = "将在" + Time + "秒后取消升级！";
                        break;
                }
                if (Time < 1)
                {
                    Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            button1.Enabled = false;
            IsDownloading = true;
            button2.Enabled = false;
            if (Database.SupporterUpdate)
            {
                if (Github)
                {
                    Thread download = new Thread(DownloadUpdate);
                    download.Start();
                }
                else
                {
                    Thread download = new Thread(DownloadUpdate2);
                    download.Start();
                }
            }
            else
            {
                if (Github)
                {
                    Thread download = new Thread(DownloadMyBotUpdate);
                    download.Start();
                }
                else
                {
                    Thread download = new Thread(DownloadMyBotUpdate2);
                    download.Start();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Close();
            Time = 0;
        }
    }
}

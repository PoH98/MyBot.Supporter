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
        private static int Time = 5;
        public static bool IsDownloading = false, Github = false;
        public MyBotUpdator()
        {
            InitializeComponent();
        }
        private static void DownloadUpdate()//Github
        {
            string UpdateLink = "https://github.com/PoH98/MyBot.Supporter/raw/master/MyBot.Supporter.Main.exe";
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
                client.DownloadFile(UpdateLink, Database.Location + "Update");

            }
            File.Copy(Database.Location + "Update", Database.Location + "MyBot.Supporter.Main.exe");
            File.Delete(Database.Location + "Update");
            string[] copy = { "Taskkill /IM MyBot.Supporter.Main.exe /F", "xcopy /S /Y \"" + Database.Location + "MyBot.Supporter.Main.exe\" " + "\"" + Environment.CurrentDirectory + "\"", "del \"" + Database.Location + "MyBot.Supporter.Main.exe\"", "start MyBot.Supporter.Main.exe", "del Update.bat" };
            File.WriteAllLines("Update.bat", copy);
            Process.Start("Update.bat");
            Application.Exit();
        }
        private static void DownloadUpdate2()//Gitee
        {
            string UpdateLink = "https://gitee.com/PoH98/MyBot.Supporter/raw/master/MyBot.Supporter.Main.exe";
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
                client.DownloadFile(UpdateLink, Database.Location + "Update");
            }
            File.Copy(Database.Location + "Update", Database.Location + "MyBot.Supporter.Main.exe");
            File.Delete(Database.Location + "Update");
            string[] copy = { "Taskkill /IM MyBot.Supporter.Main.exe /F", "xcopy /S /Y \"" + Database.Location + "MyBot.Supporter.Main.exe\" " + "\"" + Environment.CurrentDirectory + "\"", "del \"" + Database.Location + "MyBot.Supporter.Main.exe\"", "start MyBot.Supporter.Main.exe", "del Update.bat" };
            File.WriteAllLines("Update.bat", copy);
            Process.Start("Update.bat");
            Application.Exit();
        }
        public static void DownloadMyBotUpdate()//Github
        {
            string UpdateLink = "https://github.com/PoH98/MyBot.Supporter/raw/master/MyBot-MBR.zip";
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
                client.DownloadFile(UpdateLink, Database.Location + "Update");
            }
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
        }
        private static void DownloadMyBotUpdate2()//Gitee
        {
            string UpdateLink = "https://gitee.com/PoH98/MyBot.Supporter/raw/master/MyBot-MBR.zip";
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
                client.DownloadFile(UpdateLink, Database.Location + "Update");
            }
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
        }

        private void MyBotUpdator_Load(object sender, EventArgs e)
        {
            if (Database.SupporterUpdate)
            {
                if(File.Exists(Database.Location + "MyBot.Supporter.Main.exe"))
                {
                    File.Delete(Database.Location + "MyBot.Supporter.Main.exe");
                }
                if(File.Exists(Database.Location + "MyBot.Run.zip"))
                {
                    File.Delete(Database.Location + "MyBot.Run.zip");
                }
                switch (Database.Language)
                {
                    case "Chinese":
                        Text = "发现新版本管理器！";
                        label1.Text = cn_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = cn_Lang.UpdateNow;
                        button2.Text = cn_Lang.Cancel;
                        break;
                    case "English":
                        Text = "New version of Supporter found!";
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
                        Text = "发现新版本MyBot.Run！";
                        label1.Text = cn_Lang.AutoUpdateFound;
                        label2.Text = "";
                        button1.Text = cn_Lang.UpdateNow;
                        button2.Text = cn_Lang.Cancel;
                        break;
                    case "English":
                        Text = "New version of MyBot.Run found!";
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
                long bytes = new FileInfo(Database.Location + "Update").Length;
                label2.Text = bytes + " bytes";
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
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MyBot.Supporter.Main
{
    public partial class FirstUse : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public FirstUse()
        {
            Database.loadingprocess = 100;
            InitializeComponent();
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\images\\Logo.png", FileMode.Open, FileAccess.Read);
            pictureBox1.Image = Image.FromStream(fs);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            fs.Close();
        }
        public static int Language, CPUO, CPUN;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "English")
            {
                Database.Language = "English";
                Language = 1;
            }
            else if (comboBox1.Text == "中文")
            {
                Database.Language = "Chinese";
                Language = 0;
            }
            else
            {
                MessageBox.Show("Please select a valid language!");
                return;
            }
            string[] Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            Array.Resize(ref Database.Bot, 44);
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
            Form5 f = new Form5();
            f.Show();
            f.FormClosing += F_FormClosing;
        }

        private void F_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Language == 0)
            {
                label9.Text = "在进入程序后您将可以继续设置更多功能！" + Environment.NewLine + "* 更多电脑性能设置可到电脑环境设置调整" + Environment.NewLine + "* 如果需要调整多开，直接点击自动生成Profile即可" + Environment.NewLine + " * 使用MyBot注射器可创建自己的MOD版MyBot" + Environment.NewLine + "* 电报通知功能则在电报通知设置里面调整" + Environment.NewLine + "* PoH98 祝各位快乐挂机:)";
            }
            panel1.Visible = false;
            
        }

        private void FirstUse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CPUO = Convert.ToInt32(numericUpDown1.Value);
            CPUN = Convert.ToInt32(numericUpDown2.Value);
            Database.Bot[21] = textBox1.Text;
            Close();
            Database.loadingprocess = 50;
            Thread t = new Thread(Database.Load_);
            t.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "English")
            {
                Database.Language = "English";
                Language = 1;
                Languages();
            }
            else if (comboBox1.Text == "中文")
            {
                Database.Language = "Chinese";
                Language = 0;
                Languages();
            }
            else
            {
                MessageBox.Show("Please select a valid language!");
            }
        }

        private void Languages()
        {
            switch (Language)
            {
                case 0:
                    label1.Text = "欢迎使用！";
                    label2.Text = "您需要先设置一些基础设置才能继续使用管理器";
                    label3.Text = "语言";
                    groupBox1.Text = "电脑性能设置";
                    label4.Text = "当处理器使用超过70%，自动设置电脑最高性能至";
                    label5.Text = "当处理器使用低于70%，自动设置电脑最高性能至";
                    label6.Text = "电报通知机器人Token（如果不需要用到则留空）";
                    break;
                case 1:
                    label1.Text = "Welcome!";
                    label2.Text = "Please setup your first use before continue!";
                    label3.Text = "Languages";
                    groupBox1.Text = "Performance Settings";
                    label4.Text = "CPU Max Performace when usage above 70%: ";
                    label5.Text = "CPU Max Performance when usage is below 70%: ";
                    label6.Text = "Telegram Bot Token (Leave Blank if you don't use it!)";
                    break;
            }
        }
    }
}

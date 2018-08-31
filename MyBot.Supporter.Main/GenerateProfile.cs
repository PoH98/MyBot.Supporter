using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MyBot.Supporter.Main
{
    public partial class GenerateProfile : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private static int x = 0;
        public GenerateProfile()
        {
            InitializeComponent();
            string[] Profiles = Directory.GetDirectories(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Profiles");
            x = 0;
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
            for (int y = 0; y < Database.Bot.Length; y++)
            {
                if(Database.Bot[y] == "#   ")
                {
                    Database.Bot[y] = "";
                }
            }
            x = 0;
            Language();
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString();
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                if (numericUpDown1.Value > 0)
                {
                    Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString() + " VM_" + numericUpDown1.Value;
                }
                else
                {
                    Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString();
                }
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                if (numericUpDown1.Value > 0)
                {
                    Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString() + " ItoolsVM_" + numericUpDown1.Value;
                }
                else
                {
                    Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString();
                }
            }
            else
            {
                if(comboBox1.SelectedItem == null)
                {
                    switch (Database.Language)
                    {
                        case "English":
                            MessageBox.Show("Please select an emulator!");
                            break;
                        case "Chinese":
                            MessageBox.Show("请选择模拟器！");
                            break;
                    }
                    return;
                }
                else
                {
                    if (numericUpDown1.Value > 0)
                    {
                        Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString() + " " + comboBox1.SelectedItem.ToString() + "_" + numericUpDown1.Value;
                    }
                    else
                    {
                        Database.Bot[x] = label1.Text + " " + comboBox1.SelectedItem.ToString();
                    }
                }
            }
            x++;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(x < 15)
            {
                if(Database.Bot[x] != null)
                {
                    if (!Database.Bot[x].Contains("   "))
                    {
                        label1.Text = Database.Bot[x];
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                numericUpDown1.Enabled = false;
            }
            else
            {
                numericUpDown1.Enabled = true;
            }
        }
        private void Language()
        {
            if (Database.Language == "English")
            {
                label5.Text = "Selected Profile";
                label2.Text = "Please select the emulator used by the bot";
                label3.Text = "Emulator's instance number (Keep Zero if the instance is the first created)";
                //label4.Text = "The first emulator insance left it empty";
                button1.Text = "Add to Supporter's Profile";
                button2.Text = "Skip";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Database.Bot[x] = "#     ";
            x++;
        }

        private void Form5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void GenerateProfile_Load(object sender, EventArgs e)
        {
            Database.loadingprocess = 100;
            TopMost = true;
        }

        private void GenerateProfile_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.loadingprocess = 0;
            Thread loading = new Thread(Database.Load_);
            loading.Start();
        }
    }
}

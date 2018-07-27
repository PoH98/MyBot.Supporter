using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MyBot.Supporter.Main
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Language();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0 ||textBox1.Text == " ")
            {
                string message;
                string caption;
                if (Database.Language == "English")
                {
                    message = "Please filled up the bot name you want!";
                    caption = "Error";
                }
                else
                {
                    message = cn_Lang.BotNameNeeded;
                    caption = cn_Lang.Error;
                }
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            else
            {
                try
                {
                    int x = 0;
                    foreach (var pr in Database.Bot)
                    {
                        if (pr == "" || pr == "#   ")
                        {
                            if (comboBox1.SelectedIndex == 2)
                            {
                                Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString();
                            }
                            else if (comboBox1.SelectedIndex == 5)
                            {
                                if (numericUpDown1.Value != 0 )
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString() + " VM_" + numericUpDown1.Value;
                                }
                                else
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString();
                                }
                            }
                            else if (comboBox1.SelectedIndex == 4)
                            {
                                if (numericUpDown1.Value != 0)
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString() + " ItoolsVM_" + numericUpDown1.Value;
                                }
                                else
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString();
                                }
                            }
                            else
                            {
                                if (numericUpDown1.Value != 0)
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString() + " " + comboBox1.SelectedItem.ToString() + "_" + numericUpDown1.Value;
                                }
                                else
                                {
                                    Database.Bot[x] = textBox1.Text + " " + comboBox1.SelectedItem.ToString();
                                }
                            }
                            ProcessStartInfo start = new ProcessStartInfo();
                            start.FileName = "mybot.run.exe";
                            start.Arguments = Database.Bot[x] + " " + "-nwd" + " " + "-nbs";
                            Process.Start(start);
                            Close();
                            break;
                        }
                        x++;
                    }
                }
                catch (NullReferenceException)
                {
                    string message;
                    string caption;
                    if (Database.Language == "English")
                    {
                        message = "Please filled up the emulator for the bot!";
                        caption = "Error";
                    }
                    else
                    {
                        message = cn_Lang.SelectEmulatorNeeded;
                        caption = cn_Lang.Error;
                    }
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;
                    result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
        }
        private void Language()
        {
            if (Database.Language == "English")
            {
                label1.Text = "Please Input the name of the Bot";
                label2.Text = "Please select the emulator used by the bot";
                label3.Text = "Emulator instance's number";
                //label4.Text = "The first instance left it empty";
                button1.Text = "Add new MyBot Profile";
                button2.Text = "Cancel";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

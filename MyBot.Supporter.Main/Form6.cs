using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBot.Supporter.Main
{
    public partial class Form6 : Form
    {
        string[] Captcha = {"Send Debug","Debug File","PoH98 Debug","Telegram Debug" };
        public Form6()
        {
            InitializeComponent();
        }
        Random rnd = new Random();
        int rand;
        private void Form6_Load(object sender, EventArgs e)
        {
            this.Focus();
            rand = rnd.Next(0, 3);
            if (Database.Language == "Chinese")
            {
                label1.Text = cn_Lang.PleaseEnter+"\""+Captcha[rand]+"\"" + cn_Lang.SendDebug_Button;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != Captcha[rand])
            {
                MessageBox.Show(cn_Lang.Error +"! " + cn_Lang.NoSpammers);
                MainScreen.captcha = false;
                Close();
            }
            else
            {
                MainScreen.captcha = true;
                Close();
            }
        }
    }
}

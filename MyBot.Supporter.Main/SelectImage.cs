using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace MyBot.Supporter.Main
{
    public partial class SelectImage : Form
    {
        public SelectImage()
        {
            InitializeComponent();
        }
        private void SelectImage_Load(object sender, EventArgs e)
        {
            if (Database.Network)
            {
                try
                {
                    pictureBox1.Load("https://404store.com/2018/07/18/4ac4e221e190330f9afbb52a1d584896.jpg");
                    pictureBox2.Load("https://404store.com/2018/07/18/c02139f60431590adaf574c2f6764603.jpg");
                    pictureBox3.Load("https://404store.com/2018/07/18/a2a23b2f9063c44f1f2c7dff53a7525d.jpg");
                    pictureBox4.Load("https://404store.com/2018/07/18/d8569ea54af3ad5d6b64f66b1f5c4964.jpg");
                    pictureBox5.Load("https://404store.com/2018/07/18/2aba4a200a6ba2d523b2ca1021d90f48.jpg");
                    pictureBox6.Load("https://404store.com/2018/08/21/02721b5191359297c51c16628c2e5803.png");
                }
                catch (WebException w)
                {
                    MessageBox.Show(w.Status.ToString());
                    Close();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("404 Not Found");
                    Close();
                }
            }
            else
            {
                switch (Database.Language)
                {
                    case "English":
                        MessageBox.Show(en_Lang.NoNetwork);
                        break;
                    case "Chinese":
                        MessageBox.Show(cn_Lang.NoNetwork);
                        break;
                }
                Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox1.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox2.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox3.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox4.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox5.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch"))
            {
                File.Delete(Environment.CurrentDirectory + "\\images\\Logo.png");
            }
            else
            {
                File.Move(Environment.CurrentDirectory + "\\images\\Logo.png", Environment.CurrentDirectory + "\\MyBot_Supporter_MOD\\Logo.patch");
            }
            pictureBox6.Image.Save(Environment.CurrentDirectory + "\\images\\Logo.png", ImageFormat.Png);
            Close();
        }
    }
}

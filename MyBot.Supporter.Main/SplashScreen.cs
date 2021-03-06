﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyBot.Supporter.Main
{
    public partial class SplashScreen : Form
    {
        static string[] RandomText = {"Feeding Baby Dragons", "Buying Barbarian King's Insurance", "Painting Goblins green" , "Planting trees and bushes" , "Sharpening Archer's arrow" , "Drinking Elixir" , "Lighting up miner's candle" , "Finding gold that stealed by goblins" , "Cooking food for the troops" , "Buying Warden's Insurance" , "Coloring Elixir into Dark Elixir", "Rescuing barbarian that fall into the sea" , "Hiding Gems on the trees" , "Smelting Pekka's armor" , "Finding missing button" , "Chasing Minions" };
        int loadtime = 0;
        public SplashScreen()
        {
            InitializeComponent();
            Random rnd = new Random();
            int buffer = rnd.Next(0, RandomText.Length);
            label1.Text = RandomText[buffer];
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(loadtime % 20 == 0)
            {
                Random rnd = new Random();
                int buffer = rnd.Next(0, RandomText.Length);
                label1.Text = RandomText[buffer];
            }
            progressBar1.Value = Database.loadingprocess;
            if (progressBar1.Value == 100)
            {
               Close();
            }
            if (loadtime > 600)
            {
                Application.Exit();
                Environment.Exit(0);
            }
            loadtime++;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(Environment.CurrentDirectory + "\\images\\Logo.png", FileMode.Open, FileAccess.Read);
                pictureBox1.Image = Image.FromStream(fs);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                fs.Close();
            }
            catch
            {
                pictureBox1.Image = null;
            }
        }
    }
}

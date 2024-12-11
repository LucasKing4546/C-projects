using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Catch_the_Butterflies
{
    public partial class Game : Form
    {

        int timp_ramas = 60;
        int score = 0;
        public Game()
        {
            InitializeComponent();
            spawn.Start();
            fly.Start();
        }

        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Imagini\\");

        int[] butterflies = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        int k = -1;

        List<int> x_speed = new List<int>();
        List<int> y_speed = new List<int>();

        void spawnButterfly()
        {
            Random random = new Random();
            k++;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(70, 50);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = Image.FromFile(files[butterflies[timp_ramas % 10]]);
            pictureBox.BackColor = Color.Transparent;
            int X = random.Next(1, 1000000) % 700;
            int Y = random.Next(1, 100000) % 420;
            pictureBox.Location = new Point(X, Y);

            x_speed.Add(random.Next(-5, 5));
            y_speed.Add(random.Next(-5, 5));

            pictureBox.Tag = k;
            pictureBox.Click += PictureBox_Click;
            this.Controls.Add(pictureBox);
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            this.Controls.Remove(pictureBox);
            score++;
            label1.Text = "Score: " + score.ToString();
        }

        private void spawn_Tick(object sender, EventArgs e)
        {
            if (timp_ramas % 10 == 0)
            {
                Random random = new Random();
                butterflies.OrderBy(x => random.Next()).ToArray();            
            }
            timp_ramas--;
            label2.Text = "Timp ramas: " + timp_ramas;
            if (timp_ramas == 0)
            {
                fly.Stop();
                spawn.Stop();
                MessageBox.Show("Ai reusit sa colectezi " + score.ToString() + " de fluturi!" + System.Environment.NewLine + "Apasa OK pentru a juca din nou!");
                Game game = new Game();
                Game.ActiveForm.Hide();
                game.Show();
            }
            spawnButterfly();
        }

        void Movement()
        {
            foreach(PictureBox pictureBox in this.Controls.OfType<PictureBox>())
            {
                int tag = Convert.ToInt32(pictureBox.Tag);
                pictureBox.Left += x_speed[tag];
                pictureBox.Top += y_speed[tag];
                if (pictureBox.Left < 10 || pictureBox.Left > 720)
                {
                    x_speed[tag] *= -1;
                }
                if (pictureBox.Top < 10 || pictureBox.Top > 425)
                {
                    y_speed[tag] *= -1; 
                }
            }
        }

        private void fly_Tick(object sender, EventArgs e)
        {
            Movement();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            Game.ActiveForm.Hide();
            form.Show();
        }
    }
}

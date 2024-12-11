using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetUp();
        }

        int score;
        bool goRight, goLeft;
        int ballX, ballY, speed;
        int ok = 1;

        private void GameOver()
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= speed;

            }
            else if (goRight == true && player.Right < 800)
            {
                player.Left += speed;
            }
            ball.Left += ballX;
            ball.Top += ballY;
            if (ball.Left < 0 || ball.Right > 785)
            {
                ballX *= -1;
            }
            if (ball.Top < 0)
            {
                ballY *= -1;
            }
            if (ball.Right >= player.Left && ball.Left <= player.Right && ball.Top + 25 == player.Top)
            {
                Random randomx = new Random();
                if (ballX > 0)
                {
                    ballX = randomx.Next(5, 8);
                }
                else
                {
                    ballX = randomx.Next(5, 8);
                    ballX *= -1;
                }

                ballY = randomx.Next(5, 8);
                ballY *= -1;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds) && ball.Top >= player.Top && ok == 1)
            {
                ballX *= -1;
                ok = 0;
            }

            if (ball.Top + 25 > 500)
            {
                GameOver();
                MessageBox.Show("You lost!", "Game over!");
            }

            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Tag== "blocks")
                {
                    if (ball.Bounds.IntersectsWith(control.Bounds))
                    {
                        ballY *= -1;
                        this.Controls.Remove(control);
                        score++;
                        label1.Text = "Score: " + score.ToString();
                    }
                }
            }
            int status = 1;
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && control.Tag == "blocks")
                {
                    status = 0;
                }
            }
            if (status == 1)
            {
                GameOver();
                MessageBox.Show("You win!", "Congratulations!");
            }
        }

        private void KeyDownArrow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                goRight = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                goLeft = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUp();
        }

        private void KeyUpArrow(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SetUp()
        {

            player.Left = 350;
            ball.Left = 387;
            ball.Top = 400;

            goLeft = false;
            goRight = false;

            int left = 90, top = 60;

            for (int i=1; i<=15; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Left = left;
                pictureBox.Top = top;
                pictureBox.BackColor = Color.White;
                pictureBox.Tag = "blocks";
                Size size = new Size(100, 25);
                pictureBox.Size = size;
                this.Controls.Add(pictureBox);
                if (i % 5 == 0)
                {
                    left = 90;
                    top += 50;
                }
                else
                {
                    left += 130;
                }
            }

            ok = 1;
            score = 0;
            ballX = 5;
            ballY = 5;
            speed = 12;
            label1.Text = "Score: " + score.ToString();
            timer1.Start();
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Arrow_shoote
{
    public partial class Arrow_shooter : Form
    {
        bool goUp, goDown;
        int playerspeed = 5, balloonspeed = 3, arrowspeed = 7;
        int score = 0, tick = 0;

        List<PictureBox> balloons = new List<PictureBox>();
        public Arrow_shooter()
        {
            InitializeComponent();
            mainTimer.Start();
            boomTimer.Start();
            balloons.Add(balloon1);
            balloons.Add(balloon2);
            balloons.Add(balloon3);
        }

        private void Arrow_shooter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goDown = false;
                goUp = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goUp = false;
                goDown = true;  
            }
        }

        private void Arrow_shooter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            else if (e.KeyCode == Keys.Space)
            {
                
                ShootArrow();
            }
        }
       
        private void Arrow_shooter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        void relocateBalloon(PictureBox pictureBox)
        {
            Random random = new Random();
            int x = random.Next(1, 100000) % 551 + 200;
            pictureBox.Left = x;
            pictureBox.Top = 500;
        }

        private void boomTimer_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox pictureBox in this.Controls.OfType<PictureBox>())
            {
                if (pictureBox.Tag == "boom")
                {
                    this.Controls.Remove(pictureBox);
                }
            }
        }

        void explosion(PictureBox ballon)
        {
            PictureBox boom = new PictureBox();
            boom.Tag = "boom";
            boom.Image = Properties.Resources.Explosion;
            boom.SizeMode = PictureBoxSizeMode.AutoSize;
            boom.Location = ballon.Location;
            this.Controls.Add(boom);
        }

        void Movement()
        {
            foreach(PictureBox pictureBox in this.Controls.OfType<PictureBox>())
            {
                if (pictureBox.Tag == "arrow")
                {
                    pictureBox.Left += arrowspeed;
                    foreach (PictureBox balloon in balloons)
                    {
                        if (pictureBox.Bounds.IntersectsWith(balloon.Bounds))
                        {
                            explosion(balloon);
                            relocateBalloon(balloon);
                            this.Controls.Remove(pictureBox);
                            score++;
                        }
                    }
                    if (pictureBox.Left > 800)
                    {
                        this.Controls.Remove(pictureBox);
                    }
                }
               
            }

            foreach (PictureBox pictureBox in balloons)
            {
                pictureBox.Top -= balloonspeed;
                if (pictureBox.Top + pictureBox.Height < 0)
                {
                    relocateBalloon(pictureBox);
                }
            }

            if (goUp && green_arrow.Top > 0)
            {
                green_arrow.Top -= playerspeed;
            }
            else if (goDown && green_arrow.Top + green_arrow.Height < 450)
            {
                green_arrow.Top += playerspeed;
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick % 25 == 0) {
                green_arrow.Image = Properties.Resources.idle;
               
            }
            label1.Text = "Score: " + score.ToString();
            Movement();
        }

        void ShootArrow()
        {
            green_arrow.Image = Properties.Resources.shoot;
            PictureBox arrow = new PictureBox();
            arrow.Tag = "arrow";
            arrow.Image = Properties.Resources.arrow;
            arrow.SizeMode = PictureBoxSizeMode.AutoSize;
            int x = green_arrow.Left + green_arrow.Width - arrow.Width;
            int y = green_arrow.Top + green_arrow.Height / 2 - arrow.Height - 15;
            arrow.Location = new Point(x, y);
            this.Controls.Add(arrow);
        }
    }
}

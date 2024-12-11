using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Zombie_Shooter
{
    public partial class Game : Form
    {
        List<PictureBox> bullets = new List<PictureBox>();
        List<PictureBox> zombies = new List<PictureBox>();

        bool goLeft, goRight, goUp, goDown;
        string direction = "up";
        int playerspeed = 10, ammo_count = 10, zombiespeed = 3, kills = 0;
        int playerHeath = 100;
        public Game()
        {
            InitializeComponent();
            heath.Value = 100;
            ammo.Visible = false;
            Main.Start();
            zombies.Add(zombie1);
            zombies.Add(zombie2);
            zombies.Add(zombie3);
        }

        void PlayerMovement()
        {
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerspeed;
                player.Image = Properties.Resources.left;
            }
            else if (goRight == true && player.Left + player.Width < 1270)
            {
                player.Left += playerspeed;
                player.Image = Properties.Resources.right;
            }
            else if (goUp == true && player.Top > 40)
            {
                player.Top -= playerspeed;
                player.Image = Properties.Resources.up;
            }
            else if (goDown == true && player.Top + player.Height < 780)
            {
                player.Top += playerspeed;
                player.Image = Properties.Resources.down;
            }
            if (player.Bounds.IntersectsWith(ammo.Bounds) && ammo.Visible == true) {
                ammo_count += 5;
                ammo.Visible = false;
            }
        }

        void ZombieMovement(int x, int y, PictureBox pictureBox, int speed)
        {
            float tx = x - pictureBox.Location.X;
            float ty = y - pictureBox.Location.Y;
            float length = (float)Math.Sqrt(tx * tx + ty * ty);

            if (Math.Abs(tx) >= Math.Abs(ty) && tx < 0)
            {
                pictureBox.Image = Properties.Resources.zleft;
            }
            else if (Math.Abs(tx) >= Math.Abs(ty) && tx > 0)
            {
                pictureBox.Image= Properties.Resources.zright;
            }
            else if (Math.Abs(tx) < Math.Abs(ty) && ty < 0)
            {
                pictureBox.Image = Properties.Resources.zup;
            }
            else if (Math.Abs(tx) < Math.Abs(ty) && ty > 0)
            {
                pictureBox.Image = Properties.Resources.zdown;
            }

            if (length > speed)
            {
                pictureBox.Location = new Point((int)(pictureBox.Location.X + speed * tx / length), (int)(pictureBox.Location.Y + speed * ty / length));
            }
            else
            {
                pictureBox.Location = new Point(x, y);
            }
        }

        void gameOver()
        {
            player.Image = Properties.Resources.dead;
            Main.Stop();
            MessageBox.Show("Ai pierdut! Ai reusit sa omori " + kills.ToString() + " zombie!" + Environment.NewLine + "Apasa OK pentru a juca din nou!");
            Game game = new Game();
            game.Show();
            this.Hide();
        }

        void ShootBullet()
        {
            ammo_count--;
            PictureBox bullet =  new PictureBox();
            bullet.Size = new Size(5, 5);
            bullet.BackColor = Color.White;
            if (direction == "up")
            {
                bullet.Location = new Point(player.Left + player.Width / 2, player.Top - 10);
            }
            else if (direction == "left")
            {
                bullet.Location = new Point(player.Left - 10, player.Top + player.Height/2);
            }
            else if (direction == "down")
            {
                bullet.Location = new Point(player.Left + player.Width / 2, player.Bottom);
            }
            else
            {
                bullet.Location = new Point(player.Right, player.Top + player.Height / 2);
            }
            bullet.Tag = direction;
            bullet.Visible = true;
            bullets.Add(bullet);
            this.Controls.Add(bullet);
        }

        void BulletMovement()
        {
            foreach (PictureBox bullet in bullets) 
            {
                if (bullet.Tag == "up")
                {
                    bullet.Top -= playerspeed;
                }
                else if (bullet.Tag == "right")
                {
                    bullet.Left += playerspeed;
                }
                else if (bullet.Tag == "left")
                {
                    bullet.Left -= playerspeed;
                }
                else if (bullet.Tag == "down")
                {
                    bullet.Top += playerspeed;
                }
                foreach (PictureBox zombie in zombies)
                {
                    if (bullet.Bounds.IntersectsWith(zombie.Bounds) && bullet.Visible == true)
                    {
                        kills++;
                        bullet.Visible = false;
                        this.Controls.Remove(bullet);
                        Random random = new Random();
                        int location = random.Next(1, 4);
                        switch(location)
                        {
                            case 1:
                                zombie.Location = new Point(random.Next(0, 1210), 870);
                                break;
                            case 2:
                                zombie.Location = new Point(random.Next(0, 1210), -70);
                                break;
                            case 3:
                                zombie.Location = new Point(-70, random.Next(0, 720));
                                break;
                            case 4:
                                zombie.Location = new Point(1350, random.Next(0, 720));
                                break;
                        }
                    } 
                }

            }
        }

        void spawnAmmo()
        {
            if (ammo_count == 0 && ammo.Visible == false)
            {
                Random random = new Random();
                int x = random.Next(0, 1210);
                int y = random.Next(0, 720);
                ammo.Location = new Point(x, y);
                ammo.Visible = true;
            }
        }

        private void Main_Tick(object sender, EventArgs e)
        {
            txtAmmo.Text = "AMMO: " + ammo_count.ToString();
            txtKills.Text = "KILLS: " + kills.ToString();

            PlayerMovement();
            foreach (PictureBox zombie in zombies)
            {
                ZombieMovement(player.Location.X, player.Location.Y, zombie, zombiespeed);
            }
            BulletMovement();
            spawnAmmo();

            foreach (PictureBox zombie in zombies)
            {
                if (zombie.Bounds.IntersectsWith(player.Bounds))
                {
                    playerHeath -= 1;
                    if (heath.Value > 0)
                    {
                        heath.Value = playerHeath;
                    }
                    else
                    {
                        gameOver();
                        break;
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                goRight = false;
                goUp = false;
                goDown = false;
                direction = "left";
            }
            else if (e.KeyCode == Keys.Right)
            {
                goLeft = false;
                goRight = true;
                goUp = false;
                goDown = false;
                direction = "right";
            }
            else if (e.KeyCode == Keys.Up)
            {
                goLeft = false;
                goRight = false;
                goUp = true;
                goDown = false;
                direction = "up";
            }
            else if (e.KeyCode == Keys.Down)
            {
                goLeft= false;
                goRight = false;
                goUp = false;
                goDown = true;
                direction = "down";
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (ammo_count > 0)
                {
                    ShootBullet();
                }
            }
        }

        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }
    }
}

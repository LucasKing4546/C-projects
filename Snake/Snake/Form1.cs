using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            new Settings();
            this.KeyPreview = true;
        }

        List<Circle> Snake =  new List<Circle>();   
        Circle food = new Circle();
        bool goLeft = false, goRight = false, goUp = false , goDown = true;
        int score = 0;
        int highscore = 0;
        int countdown = 3;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && Settings.direction != "right")
            {
                goLeft = true;
                goRight = false;
                goUp = false;
                goDown = false;
            }
            if (e.KeyCode == Keys.Right && Settings.direction != "left")
            {
                goRight = true;
                goLeft = false;
                goUp = false;
                goDown = false;
            }
            if (e.KeyCode == Keys.Up && Settings.direction != "down")
            {
                goUp = true;
                goLeft = false;
                goRight = false;
                goDown = false;
            }
            if (e.KeyCode == Keys.Down && Settings.direction != "up")
            {
                goDown = true;
                goLeft = false;
                goRight = false;
                goUp = false;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Update(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < Snake.Count; i++)
            {
                Rectangle rectangle = new Rectangle(Snake[i].X, Snake[i].Y, Settings.Width, Settings.Height);
                SolidBrush redbrush = new SolidBrush(Color.Green);
                if (i == 0)
                {
                    redbrush.Color = Color.Red;
                }
                g.FillEllipse(redbrush, rectangle);
            }
            Rectangle rectangle1 = new Rectangle(food.X, food.Y, Settings.Width, Settings.Height);
            SolidBrush redbrush1 = new SolidBrush(Color.White);
            g.FillEllipse(redbrush1, rectangle1);
        }

        private void Restart()
        {
            Settings.direction = "down";
            goDown = true;
            goUp = false;
            goLeft = false;
            goRight = false;

            score = 0;
            label4.Text = "0";
            panel1.Invalidate();
            countdown = 3;
            label5.Text = "   " + countdown.ToString();
            label5.ForeColor = Color.Red;
            Snake.Clear();
            timer2.Start();
            button2.Enabled = false;
            button1.Enabled = false;
        }
        private void Start()
        {
            panel1.Invalidate();
            timer1.Start();
            button2.Enabled = true;
            Random random = new Random();
            int x = random.Next(6, 20);
            int y = random.Next(6, 20);
            Circle head = new Circle { X = x * Settings.Width, Y = y * Settings.Height };
            Snake.Add(head);
            for (int i = 0; i < 5; i++)
            {
                Circle body = new Circle { X = Snake[i].X, Y = Snake[i].Y - 15 };
                Snake.Add(body);
            }
            food = new Circle { X = random.Next(1, 28) * Settings.Width, Y = random.Next(1, 38) * Settings.Height };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            for (int i = Snake.Count-1; i >= 0 ; i--)
            {
                if (i != 0)
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
                else
                {
                    if (goUp == true)
                    {
                        Settings.direction = "up";
                        Snake[0].Y -= 15;
                    }
                    if (goDown == true)
                    {
                        Settings.direction = "down";
                        Snake[0].Y += 15;
                    }
                    if (goRight == true)
                    {
                        Settings.direction = "right";
                        Snake[0].X += 15;
                    }
                    if (goLeft == true)
                    {
                        Settings.direction = "left";
                        Snake[0].X -= 15;
                    }
                    if (Snake[i].X == food.X && Snake[i].Y == food.Y)
                    {
                        EatFood();
                    }
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            GameOver();
                        }
                    }
                    if (Snake[i].X == -15 || Snake[i].X == 465 || Snake[i].Y == -15 || Snake[i].Y == 625)
                    {
                        GameOver();
                    }
                }
            }
            panel1.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label5.Visible = true;
            if (countdown != 0 && countdown != -1)
            {
                if (countdown == 3)
                {
                    label5.ForeColor = Color.Red;
                    label5.Text = "   " + countdown.ToString();
                }
                if (countdown == 2)
                {
                    label5.ForeColor = Color.Yellow;
                    label5.Text = "   " + countdown.ToString();
                }
                if (countdown == 1)
                {
                    label5.ForeColor = Color.DarkGreen;
                    label5.Text = "   " + countdown.ToString();
                }
                countdown--;
            }
            else if (countdown == 0)
            {
                label5.ForeColor = Color.Green;
                label5.Text = "GO!";
                countdown--;
            }
            else
            {
                Start();
                label5.Visible = false;
                timer2.Stop();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (score > highscore)
            {
                highscore = score;
                label4.Text = highscore.ToString();
            }
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void EatFood()
        {
            Random random = new Random();
            score++;
            if (score % 10 == 0 && score != 0)
            {
                if (timer1.Interval - 10 == 0)
                {
                    timer1.Interval = 1;
                }
                else
                {
                    timer1.Interval -= 10;
                }
            }
            label4.Text = score.ToString();
            Circle body = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);
            food = new Circle { X = random.Next(1, 28) * Settings.Width, Y = random.Next(1, 38) * Settings.Height };
        }
        private void GameOver()
        {
            timer1.Stop();
            if (score > highscore) {
                highscore = score;
                label2.Text = highscore.ToString();
            }
            MessageBox.Show("YOU LOST", "GAME OVER");
            button1.Enabled = true;
            button2.Enabled = false;
        }
        private void KeyIsPressed(object sender, KeyPressEventArgs e)
        {
        }
    }
}

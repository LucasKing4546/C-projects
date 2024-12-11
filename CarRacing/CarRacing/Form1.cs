using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacing
{
    public partial class CarRacing : Form
    {
        bool left, right;
        int speed = 2, t1, t2, t3, playerspeed = 2;
        string n1, n2, n3;
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        List<PictureBox> linii = new List<PictureBox>();
        List<Image> masini = new List<Image>();
        public static int Scor;
        public CarRacing()
        {
            InitializeComponent();
            pictureBoxes.Add(car1);
            pictureBoxes.Add(car2);
            linii.Add(pictureBox2);
            linii.Add(pictureBox3);
            linii.Add(pictureBox4);
            linii.Add(pictureBox5);
            linii.Add(pictureBox6);
            linii.Add(pictureBox7);
            linii.Add(pictureBox8);
            linii.Add(pictureBox9);
            linii.Add(pictureBox10);
            linii.Add(pictureBox11);
        }

        private void Drum_Click(object sender, EventArgs e)
        {

        }

        private void Start_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void CarRacing_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
                right = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                left = false;
                right = false;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (scor.Text != "0")
                resetGame();
            refreshTop();
            Start.Enabled = false;
            Pilot.Start();
            Masini.Start();
        }

        private void Pilot_Tick(object sender, EventArgs e)
        {
            Scor += speed;
            scor.Text = (Scor / 5).ToString();
            if (Scor % 1000 == 0)
            {
                speed++;
                playerspeed += speed % 2;
            }

            if (left == true && player.Left > 5 + Drum.Left)
            {
                player.Left -= playerspeed * 2;
            }
            else if (right == true && player.Left < 410 + Drum.Left)
            {
                player.Left += playerspeed * 2;
            }
            if (player.Bounds.IntersectsWith(car1.Bounds) || player.Bounds.IntersectsWith(car2.Bounds))
            {
                gameOver();
            }
        }

        void playSound()
        {
            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.hit);
            soundPlayer.Play();
        }

        void gameOver()
        {
            GameOver gameOver = new GameOver();
            gameOver.Show();
            playSound();
            explosion.Visible = true;
            explosion.Location = player.Location;
            explosion.Top += 25;
            Reward.Visible = true;
            Start.Enabled = true;
            Pilot.Stop();
            Masini.Stop();
            if (Scor > 5000) Reward.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "imagini\\silver.png");
            if (Scor > 10000) Reward.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "imagini\\gold.png");
        }

        void refreshTop()
        {
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Masini.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            string command = "SELECT * FROM Highscore";
            SqlCommand cmd = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            t1 = 0;
            t2 = 0;
            t3 = 0;
            while (reader.Read())
            {
                int t = Convert.ToInt32(reader[2].ToString());
                if (t > t1)
                {
                    t3 = t2;
                    n3 = n2;
                    t2 = t1;
                    n2 = n1;
                    t1 = t;
                    n1 = reader[1].ToString();
                }
                else if (t > t2)
                {
                    t3 = t2;
                    n3 = n2;
                    t2 = t;
                    n2 = reader[1].ToString();
                }
                else if (t > t3)
                {
                    t3 = t;
                    n3 = reader[1].ToString();
                }
            }
            
            reader.Close();
            sqlConnection.Close();
            scor1.Text = t1.ToString();
            scor2.Text = t2.ToString();
            scor3.Text = t3.ToString();
            nume1.Text = n1.ToString();
            nume2.Text = n2.ToString();
            nume3.Text = n3.ToString();
        }

        void resetGame()
        {
            left = false;
            right = false;
            Scor = 0;
            scor.Text = "0";
            speed = 2;
            playerspeed = 2;
            explosion.Visible = false;
            Reward.Visible = false;
            car1.Top = -111;
            car2.Top = -333;
        }
        private void CarRacing_Load(object sender, EventArgs e)
        {
            refreshTop();
            car1.BackgroundImage = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "imagini\\TruckBlue.png");
            masini.Add(Properties.Resources.ambulance);
            masini.Add(Properties.Resources.carPink);
            masini.Add(Properties.Resources.TruckBlue);
            masini.Add(Properties.Resources.carGreen);
            masini.Add(Properties.Resources.carOrange); 
            masini.Add(Properties.Resources.carPink);
            masini.Add(Properties.Resources.CarRed);
            masini.Add(Properties.Resources.TruckWhite);
        }

        private void Masini_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            car1.Top += speed * 2;
            car2.Top += speed * 2;
            foreach (PictureBox pictureBox in pictureBoxes)
            {
                if (pictureBox.Location.Y > 591)
                {
                    int x = random.Next(5 + Drum.Left, 410 + Drum.Left);
                    pictureBox.Location = new Point(x, -111);
                    int id = random.Next(1, 10000) % 8;
                    pictureBox.BackgroundImage = masini[id];
                }
            }
            foreach (PictureBox pictureBox in linii)
            {
                pictureBox.Top += speed * 2 - 1;
                if (pictureBox.Location.Y > 594)
                {
                    pictureBox.Top = -113;
                }
            }
        }

        private void CarRacing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) 
            {
                left = true;
                right = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                right = true;
                left = false;
            }
        }
    }
}

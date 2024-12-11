using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Jocuri
{
    public partial class Popice_cu_litere : Form
    {
        string email, nume;
        public Popice_cu_litere(string emailLogat, string numeLogat)
        {
            InitializeComponent();
            email = emailLogat;
            nume = numeLogat;
        }

        int timpramas = 100;
        int ball_speed = 5;

        int[] ordonare = new int[14];
        int k = 0;

        List<string> numeImagini = new List<string>();
        List<char> litere = new List<char>();
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Imagini\\");

        bool goLeft, goRight, goUp;

        private void Popice_cu_litere_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 14; i++)
            {
                string[] nume = files[i].Split('\\');
                nume = nume[nume.Length - 1].Split('.');
                numeImagini.Add(nume[0]);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Setup();
            timp.Start();
            mingetimer.Start();
        }

        private void Popice_cu_litere_KeyDown(object sender, KeyEventArgs e)
        {
            if (goUp == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    goLeft = true;
                    goRight = false;
                    goUp = false;
                }
                if (e.KeyCode == Keys.Right)
                {
                    goRight = true;
                    goLeft = false;
                    goUp = false;
                }
            }
        }

        private void Popice_cu_litere_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void Popice_cu_litere_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
        }

        void Setup()
        {
            int[] reordonare = new int[2];

            for (int i = 0; i < 14; i++)
            {
                ordonare[i] = i;
            }

            Random random = new Random();
            ordonare = ordonare.OrderBy(x => random.Next()).ToArray();

            animal1.Image = Image.FromFile(files[ordonare[0]]);
            animal2.Image = Image.FromFile(files[ordonare[1]]);

            for (int i=0; i < numeImagini[ordonare[0]].Length; i++) 
            {
                litere.Add(numeImagini[ordonare[0]][i]);
            }
            for (int i = 0; i < numeImagini[ordonare[1]].Length; i++)
            {
                litere.Add(numeImagini[ordonare[1]][i]);
            }
            k = litere.Count;
            int[] randomize = new int[k];
            for (int i=0; i < k; i++)
            {
                randomize[i] = i;
            }
            randomize = randomize.OrderBy(x => random.Next()).ToArray();

            Font font = new Font("Tahoma", 20, FontStyle.Bold);

            int X = 40;
            int Y = 20;
            int space = (760 - 30 * k - 40) / (k - 1);

            Size size= new Size(X, Y);

            for (int i = 0; i < k; i++)
            {
                Label label = new Label();
                label.Font = font;
                label.Text = litere[randomize[i]].ToString().ToUpper();
                label.AutoSize = true;
                label.Location = new Point(X, Y);
                X += space + 30;
                groupBox1.Controls.Add(label);
                size = label.Size;
            }
            ball.Size = size;
            ball.Image = Properties.Resources.ball;
            ball.Location = new Point((this.Width - ball.Width)/2, 250);
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            groupBox1.Controls.Add(ball);
        }

        private void mingetimer_Tick(object sender, EventArgs e)
        {
            Movement();
            CheckWord();
            if (animal1.Image == null && animal2.Image == null) 
            {
                mingetimer.Stop();
                timp.Stop();
                gameOver(1);
            }
        }

        void Movement()
        {
            if (goLeft)
            {
                ball.Left -= ball_speed;
            }
            else if (goRight)
            {
                ball.Left += ball_speed;
            }
            else if (goUp) 
            {
                ball.Top -= ball_speed;
                foreach (Label label in groupBox1.Controls.OfType<Label>())
                {
                    if (ball.Bounds.IntersectsWith(label.Bounds))
                    {
                        textBox1.Text += label.Text;
                        groupBox1.Controls.Remove(label);
                        ball.Location = new Point((this.Width - ball.Width) / 2, 250);
                        goUp = false;
                        if (check() == false)
                        {
                            mingetimer.Stop();
                            timp.Stop();
                            gameOver(0);
                        }
                    }
                }
                if (ball.Top < 0)
                {
                    ball.Location = new Point((this.Width - ball.Width) / 2, 250);
                    goUp = false;
                }
            }
        }

        private void timp_Tick(object sender, EventArgs e)
        {
            timpramas--;
            label2.Text = "Timp ramas: " + timpramas.ToString();
            if (timpramas == 0)
            {
                mingetimer.Stop();
                timp.Stop();
                gameOver(0);
            }
        }

        private void groupBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Alege_joc alege_Joc = new Alege_joc(email, nume);
            Popice_cu_litere.ActiveForm.Hide();
            alege_Joc.Show();
        }

        private void button2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        bool check()
        {
            int i = textBox1.Text.Length - 1;
            if (animal1.Image != null && i < numeImagini[ordonare[0]].Length)
            {
                if (textBox1.Text[i].ToString().ToLower() == numeImagini[ordonare[0]][i].ToString().ToLower())
                {
                    return true;
                }
            }
            if (animal2.Image != null && i < numeImagini[ordonare[1]].Length)
            {
                if (textBox1.Text[i].ToString().ToLower() == numeImagini[ordonare[1]][i].ToString().ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        void CheckWord()
        {
            if (textBox1.Text.ToString().ToLower() == numeImagini[ordonare[0]].ToString().ToLower())
            {
                animal1.Image = null;
                textBox1.Text = "";
            }
            if (textBox1.Text.ToString().ToLower() == numeImagini[ordonare[1]].ToString().ToLower())
            {
                animal2.Image = null;
                textBox1.Text = "";
            }
        }

        void gameOver(int status) 
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = String.Format("INSERT INTO Rezultate VALUES(@tip, @email, @punctaj, @data);");
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@tip", 1);
            sqlCommand.Parameters.AddWithValue("@email", email);
            sqlCommand.Parameters.AddWithValue("@punctaj", timpramas);
            sqlCommand.Parameters.AddWithValue("@data", DateTime.Now);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            if (status == 0)
            {
                MessageBox.Show("Ai pierdut!");
            }
            else
            {
                MessageBox.Show("Ai castigat!");
            }
             Alege_joc alege_Joc = new Alege_joc(email, nume);
             Popice_cu_litere.ActiveForm.Hide();
             alege_Joc.Show();
        }
    }
}

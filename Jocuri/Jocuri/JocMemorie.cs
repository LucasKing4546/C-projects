using MessagingToolkit.QRCode.Geom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jocuri
{
    public partial class Testeaza_memoria : Form
    {
        string email, nume;
        public Testeaza_memoria(string emailLogat, string numeLogat)
        {
            InitializeComponent();
            email = emailLogat;
            nume = numeLogat;
        }

        int timpramas = 100;

        List<string> numeImagini = new List<string>();
        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Imagini\\");
        int[] n = {2, 3, 5, 8};
        int index = 0;
        int[] ordonare = new int[14];

        void Setup(int index)
        {
            int nr = n[index] * 2;
            int[] reordonare = new int[nr / 2];

            for (int i = 0; i < 14; i++)
            {
                ordonare[i] = i;
            }

            Random random = new Random();
            ordonare = ordonare.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i < nr / 2; i++)
            {
                reordonare[i] = ordonare[i];
            }

            reordonare = reordonare.OrderBy(x => random.Next()).ToArray();

            int X = 10, Y = 20;

            for (int i = 0; i < nr / 2; i++)
            {
                Button button = new Button();
                button.Text = null;
                button.BackgroundImage = null;
                button.BackColor = Color.LimeGreen;
                button.Click += Button_Click;
                button.Tag = ordonare[i];
                button.Size = new Size(90, 90);
                button.Location = new System.Drawing.Point(X, Y);
                button.BackgroundImageLayout = ImageLayout.Stretch;
                groupBox1.Controls.Add(button);
                X += 100;
            }

            X = 10;
            Y += 100;

            for (int i = 0; i < nr / 2; i++)
            {
                Button button = new Button();
                button.Text = null;
                button.BackgroundImage = null;
                button.Click += Button_Click;
                button.BackColor = Color.LimeGreen;
                button.ForeColor = Color.Black;
                button.Font = new Font("Tahoma", 10);   
                button.Tag = reordonare[i];
                button.Size = new Size(90, 90);
                button.Location = new System.Drawing.Point(X, Y);
                button.BackgroundImageLayout = ImageLayout.Stretch;
                groupBox1.Controls.Add(button);
                X += 100;
            }
        }
        private void Testeaza_memoria_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 14; i++)
            {
                string[] nume = files[i].Split('\\');
                nume = nume[nume.Length - 1].Split('.');
                numeImagini.Add(nume[0]);
            }
            
        }

        Button Button1, Button2;

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Location.Y == 20 && Button1 == null) 
            {
                button.BackgroundImage = Image.FromFile(files[Convert.ToInt32(button.Tag)]);
                Button1 = button;
            }
            else if (button.Location.Y == 120 && Button2 == null)
            {
                button.Text = numeImagini[Convert.ToInt32(button.Tag)];
                Button2 = button;
            }

            if (Button1 != null && Button2 != null)
            {
                imagine.Start();
            }
        }

        private void timp_Tick(object sender, EventArgs e)
        {
            timpramas--;
            label1.Text = "Timp ramas: " + timpramas.ToString();
            if (timpramas == 0)
            {
                gameOver(0);
            }
        }

        void Check()
        {
            if (Button1.Tag.ToString() != Button2.Tag.ToString())
            {
                foreach (Button button in groupBox1.Controls.OfType<Button>())
                {
                    if (button.Tag.ToString() == Button2.Tag.ToString())
                    {
                        button.Text = null;
                    }
                    else if (button.Tag.ToString() == Button1.Tag.ToString())
                    {
                        button.BackgroundImage = null;
                    }
                }
            }
            else
            {
                foreach (Button button in groupBox1.Controls.OfType<Button>())
                {
                    if (button.Tag.ToString() == Button1.Tag.ToString())
                    {
                        button.Enabled = false;
                        if (button.Location.Y == 120)
                        {
                            for (int i = 0; i < n[index]; i++)
                            {
                                if (button.Tag.ToString() == ordonare[i].ToString())
                                {
                                    button.Text = (i+1).ToString() + " - " + button.Text;
                                }
                            }
                        }
                    }
                }
            }
            Button1 = null;
            Button2 = null;

            bool ok = true;

            foreach (Button button in groupBox1.Controls.OfType<Button>())
            {
                if (button.Enabled == true)
                {
                    ok = false;
                }
            }

            if (ok == true)
            {
                NextLevel();
            }
        }

        private void imagine1_Tick(object sender, EventArgs e)
        {
            Check();
            imagine.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timp.Start();
            Setup(index);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Alege_joc alege_Joc = new Alege_joc(email, nume);
            Testeaza_memoria.ActiveForm.Hide();
            alege_Joc.Show();
        }

        void gameOver(int status)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = String.Format("INSERT INTO Rezultate VALUES(@tip, @email, @punctaj, @data);");
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@tip", 0);
            sqlCommand.Parameters.AddWithValue("@email", email);
            sqlCommand.Parameters.AddWithValue("@punctaj", timpramas);
            sqlCommand.Parameters.AddWithValue("@data", DateTime.Now);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
            if (status == 0) 
            {
                MessageBox.Show("Ai pierdut!");
                Alege_joc alege_Joc = new Alege_joc(email, nume);
                Testeaza_memoria.ActiveForm.Hide();
                alege_Joc.Show();
            }
            else
            {
                Animatie animatie = new Animatie(email, nume);
                Testeaza_memoria.ActiveForm.Hide();
                animatie.Show();
            }
        }
        void NextLevel()
        {
            groupBox1.Controls.Clear();
            index++;
            if (index == 4)
            {
                gameOver(1);
            }
            else 
            { 
                Setup(index); 
            }
            
        }
    }
}

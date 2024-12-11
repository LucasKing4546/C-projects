using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_UBB
{
    public partial class Admin : Form
    {

        ////                                               \\\\
        //// ------------- VARIABILE GLOBALE ------------- \\\\
        ////                                               \\\\

        int[] v = { 0, 0, 0, 0, 0, 0 };

        ////                                        \\\\
        //// ------------- DICTIONARY ------------- \\\\
        ////                                        \\\\

        Dictionary<string, string> utilizator = new Dictionary<string, string>();
        Dictionary<int, string> poze = new Dictionary<int, string>();

        ////                                                \\\\
        //// ------------- INITIALIZARE FORMA ------------- \\\\
        ////                                                \\\\
        public Admin()
        {
            InitializeComponent();
        }

        ////                                               \\\\
        //// ------------- INCARCARE IMAGINI ------------- \\\\
        ////                                               \\\\
        private void SelectareImagini(object sender, EventArgs e)
        {
            for (int i=0; i<v.Length; i++)
            {
                v[i] = 0;
            }
            PictureBox pictureBox = (PictureBox)sender;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.Title = "Incarca " + pictureBox.Name;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                v[Convert.ToInt32(pictureBox.Tag) - 1] = 1;
                string[] detalii = openFileDialog1.FileName.Split('\\');
                int length = detalii.Length;
                poze[Convert.ToInt32(pictureBox.Tag)] = detalii[length - 3] + '\\' + detalii[length - 2] + '\\' + detalii[length-1];
            }
        }

        ////                                               \\\\
        //// ------------- INCARCAREA FORMEI ------------- \\\\
        ////                                               \\\\
        private void Admin_Load(object sender, EventArgs e)
        {
            if (Form1.functieUtilizator == "2")
            {
                btnschimbare.Enabled = true;
            }

            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                utilizator[reader[1].ToString()] = reader[5].ToString();
                comboBox1.Items.Add(reader[1].ToString());
            }
            reader.Close();
            comboBox1.SelectedIndex = 0;
            if (utilizator[comboBox1.Text] == "1")
            {
                lbfunc.Text = "Admin";
                lbfuncop.Text = "elev/student";
            }
            else if (utilizator[comboBox1.Text] == "2")
            {
                lbfunc.Text = "Developer";
                lbfuncop.Text = "-";
            }
            else if (utilizator[comboBox1.Text] == "0")
            {
                lbfunc.Text = "Elev/Student";
                lbfuncop.Text = "admin";
            }
        }

        ////                                     \\\\
        //// ------------- BUTOANE ------------- \\\\
        ////                                     \\\\

        private void btnschimbare_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            if (utilizator[comboBox1.Text] == "1")
            {
                utilizator[comboBox1.Text] = "0";
            }
            else if (utilizator[comboBox1.Text] == "0")
            {
                utilizator[comboBox1.Text] = "1";
            }

            string command = String.Format("UPDATE Utilizatori SET admin='{0}' WHERE nume LIKE '{1}';", utilizator[comboBox1.Text], comboBox1.Text);

            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();

            if (utilizator[comboBox1.Text] == "1")
            {
                lbfunc.Text = "Admin";
                lbfuncop.Text = "elev/student";
            }
            else if (utilizator[comboBox1.Text] == "0")
            {
                lbfunc.Text = "Elev/Student";
                lbfuncop.Text = "admin";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rc;
            if (checkBox1.Checked.Equals(true)) rc = "1";
            else rc = "0";
            if (checkBox2.Checked.Equals(true)) rc = "1" + rc;
            else rc = "0" + rc;
            if (checkBox3.Checked.Equals(true)) rc = "1" + rc;
            else rc = "0" + rc;
            if (checkBox4.Checked.Equals(true)) rc = "1" + rc;
            else rc = "0" + rc;

            string[] detalii = comboBox2.Text.Split(' ');
            string tip, anul;
            anul = detalii[1];
            tip = detalii[2];

            int categorie=0;
            if (comboBox3.Text == "Expresii") categorie = 1;
            else if (comboBox3.Text == "Aritmetica modulara") categorie = 2;
            else if (comboBox3.Text == "Complexitate") categorie = 3;
            else if (comboBox3.Text == "Recursivitate") categorie = 4;
            else if (comboBox3.Text == "Divizibilitate") categorie = 5;
            else if (comboBox3.Text == "Prelucrarea cifrelor") categorie = 6;
            else if (comboBox3.Text == "Vectori") categorie = 7;
            else if (comboBox3.Text == "Matrici") categorie = 8;
            else if (comboBox3.Text == "Siruri") categorie = 9;
            else if (comboBox3.Text == "Backtracking") categorie = 10;

            int ok = 1;
            for (int i = 0; i<=5; i++)
            {
                if (i != 1)
                {
                    if (v[i] == 0) 
                    {
                        ok = 0; 
                    }
                }
            }

            if (v[1] == 0) poze[2] = "algoritm\\default.png";

            if (ok == 1 && rc!="0000" && rc!="1111" && comboBox2.Text!= "*(selecteaza subietul)*"  && comboBox3.Text != "*(selecteaza categoria)*")
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

                string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();

                string command = String.Format("INSERT INTO Grila VALUES(@cerinta, @image, @raspuns1, @raspuns2, @raspuns3, @raspuns4, @raspunsuricorecte, @data, @tip);");
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@cerinta", poze[1]);
                sqlCommand.Parameters.AddWithValue("@image", poze[2]);
                sqlCommand.Parameters.AddWithValue("@raspuns1", poze[3]);
                sqlCommand.Parameters.AddWithValue("@raspuns2", poze[4]); 
                sqlCommand.Parameters.AddWithValue("@raspuns3", poze[5]);
                sqlCommand.Parameters.AddWithValue("@raspuns4", poze[6]);
                sqlCommand.Parameters.AddWithValue("@raspunsuricorecte", rc.ToString());
                sqlCommand.Parameters.AddWithValue("@data", anul+tip);
                sqlCommand.Parameters.AddWithValue("@tip", categorie.ToString());
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Intrebare adaugata cu succes!");

                imageNull();
                CheckboxNull();
                resetComboBox();
            }
            else
            {
                MessageBox.Show("Verificati daca ati pus toate imaginile necesare si daca raspunsurile la intrebare sunt corect selectate!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pre_examen pre_Examen = new Pre_examen();
            Admin.ActiveForm.Hide();
            pre_Examen.Show();
        }

        ////                                          \\\\
        //// ------------- COMBOBOX-URI ------------- \\\\
        ////                                          \\\\

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (utilizator[comboBox1.Text] == "1")
            {
                lbfunc.Text = "Admin";
                lbfuncop.Text = "elev/student";
            }
            else if (utilizator[comboBox1.Text] == "2")
            {
                lbfunc.Text = "Developer";
                lbfuncop.Text = "-";
            }
            else if (utilizator[comboBox1.Text] == "0")
            {
                lbfunc.Text = "Elev/Student";
                lbfuncop.Text = "admin";
            }
        }

        ////                                               \\\\
        //// ------------- SUBPROGRAME UTILE ------------- \\\\
        ////                                               \\\\
        
        private void imageNull()
        {
            cerinta.Image = null;
            algoritm.Image = null;
            raspunsA.Image = null;
            raspunsB.Image = null;
            raspunsC.Image = null;
            raspunsD.Image = null;
        }
        private void CheckboxNull()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }
        private void resetComboBox()
        {
            comboBox2.Text = "*(selecteaza subietul)*";
            comboBox3.Text = "*(selecteaza categoria)*";
        }
    }
}

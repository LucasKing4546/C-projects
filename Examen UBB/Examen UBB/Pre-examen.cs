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
    public partial class Pre_examen : Form
    {
        ////                                               \\\\
        //// ------------- VARIABILE GLOBALE ------------- \\\\
        ////                                               \\\\
        
        public static int highscore;
        public static string tip_examen;
        public static int an_examen;

        ////                                                \\\\
        //// ------------- INITIALIZARE FORMA ------------- \\\\
        ////                                                \\\\
        
        public Pre_examen()
        {
            InitializeComponent();
        }

        ////                                               \\\\
        //// ------------- INCARCAREA FORMEI ------------- \\\\
        ////                                               \\\\
        
        private void Pre_examen_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            highscore = 0;
            int t1 = -1, t2 = -1, t3 = -1;
            string n1="", n2="", n3="";
            while (reader.Read())
            {
                if (Convert.ToInt32(reader[4].ToString()) > t1)
                {
                    t3 = t2;
                    t2 = t1;
                    t1 = Convert.ToInt32(reader[4].ToString());
                    n3 = n2;
                    n2 = n1;
                    n1 = reader[1].ToString().Trim();
                }
                else if (Convert.ToInt32(reader[4].ToString()) > t2)
                {
                    t3 = t2;
                    t2 = Convert.ToInt32(reader[4].ToString());
                    n3 = n2;
                    n2 = reader[1].ToString().Trim();
                }
                else if (Convert.ToInt32(reader[4].ToString()) > t1)
                {
                    t3 = Convert.ToInt32(reader[4].ToString());
                    n3 = reader[1].ToString().Trim();
                }
                if (Form1.numeUtilizator.ToString() == reader[1].ToString())
                {
                    if (reader[5].ToString().Trim() == "1" || reader[5].ToString().Trim() == "2")
                    {
                        btnAdmin.Visible = true;
                    }
                    if (Convert.ToInt32(reader[4].ToString()) > highscore)
                    {
                        highscore = Convert.ToInt32(reader[4].ToString());
                    }
                }
            }
            reader.Close();
            top1.Text = n1.ToString() + "   " + t1.ToString() + "p/100p";
            top2.Text = n2.ToString() + "   " + t2.ToString() + "p/100p";
            top3.Text = n3.ToString() + "   " + t3.ToString() + "p/100p";

            highscoretxt.Text = highscore.ToString() + "p/100p";

            string command2 = "SELECT * FROM Teste";
            SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection);
            SqlDataReader reader2 = sqlCommand2.ExecuteReader();

            chart1.Series["Rezultate"].BorderWidth = 3;

            while (reader2.Read())
            {
                if (Form1.numeUtilizator.ToString() == reader2[1].ToString())
                {
                    chart1.Series["Rezultate"].Points.AddXY(reader2[3].ToString(), reader2[2].ToString());
                }
            }
            reader2.Close();
            sqlConnection.Close();
        }

        ////                                     \\\\
        //// ------------- BUTOANE ------------- \\\\
        ////                                     \\\\

        private void btnExamen_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text!="*(selecteaza subiectul)*")
            {
                string[] detalii = comboBox2.Text.Split(' ');
                tip_examen = detalii[2];
                an_examen = Convert.ToInt32(detalii[1].ToString());
                Examen examen = new Examen();
                Pre_examen.ActiveForm.Hide();
                examen.Show();
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            Pre_examen.ActiveForm.Hide();
            admin.Show();
        }
        private void btnExerseaza_Click(object sender, EventArgs e)
        {
            tip_examen = comboBox1.Text;
            if (tip_examen == "Antrenament")
            {
                SelectareCategorii selectareCategorii = new SelectareCategorii();
                Pre_examen.ActiveForm.Hide();
                selectareCategorii.Show();
            }
            else
            {
                Examen examen = new Examen();
                Pre_examen.ActiveForm.Hide();
                examen.Show();
            }
        }
    }
}

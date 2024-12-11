using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jocuri
{
    public partial class Alege_joc : Form
    {
        public Alege_joc()
        {
            InitializeComponent();
        }

        public Alege_joc(string emailLogat, string numeLogat)
        {
            InitializeComponent();
            email = emailLogat;
            nume = numeLogat;
        }

        string email, nume;

        private void joc1_Click(object sender, EventArgs e)
        {
            Testeaza_memoria testeaza_Memoria = new Testeaza_memoria(email, nume);
            Alege_joc.ActiveForm.Hide();
            testeaza_Memoria.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Popice_cu_litere popice = new Popice_cu_litere(email, nume);
            Alege_joc.ActiveForm.Hide();
            popice.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrimQR primQR = new PrimQR(email, nume);
            Alege_joc.ActiveForm.Hide();
            primQR.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Alege_joc_Load(object sender, EventArgs e)
        {
            label1.Text = "Bine ai venit " + nume + "(" + email + ")!";
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = String.Format("SELECT * FROM Rezultate WHERE EmailUtilizator = '{0}'", email);
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while(reader.Read())
            {
                if (reader[1].ToString() == "0")
                {
                    DateTime d = DateTime.ParseExact(reader[4].ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    string date = d.ToString("dd/MM/yyyy");
                    chart1.Series["Testeaza memoria"].Points.AddXY(DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(reader[3].ToString()));
                }
                else
                {
                    DateTime d = DateTime.ParseExact(reader[4].ToString(), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    string date = d.ToString("dd/MM/yyyy");
                    chart1.Series["Popice cu litere"].Points.AddXY(DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), Convert.ToInt32(reader[3].ToString()));
                }
            }
        }
    }
}

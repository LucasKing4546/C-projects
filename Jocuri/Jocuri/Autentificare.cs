using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec.Data;

namespace Jocuri
{
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();
        }

        void resetDataBase()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            string command = "DELETE FROM Utilizatori";
            sqlConnection.Open();
            SqlCommand sqlComman = new SqlCommand(command, sqlConnection);
            sqlComman.ExecuteNonQuery();
            command = "DELETE FROM Rezultate";
            sqlComman = new SqlCommand(command, sqlConnection);
            sqlComman.ExecuteNonQuery();
            command = "DBCC CHECKIDENT('Rezultate', RESEED, 0);";
            sqlComman = new SqlCommand(command, sqlConnection);
            sqlComman.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void Autentificare_Load(object sender, EventArgs e)
        {
            /*resetDataBase();
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);

            sqlConnection.Open();
            StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Utilizatori.txt");
            string olinie = reader.ReadLine();
            while (olinie != null)
            {
                string command = String.Format("INSERT INTO Utilizatori VALUES(@email, @nume, @parola);");
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                string[] detalii = olinie.Split(';');
                sqlCommand.Parameters.AddWithValue("@email", detalii[0]);
                sqlCommand.Parameters.AddWithValue("@nume", detalii[1]);
                sqlCommand.Parameters.AddWithValue("@parola", detalii[2]);
                sqlCommand.ExecuteNonQuery();
                olinie = reader.ReadLine();
            }

         
            reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Rezultate.txt");
            olinie = reader.ReadLine();
            while (olinie != null)
            {
                string command = String.Format("INSERT INTO Rezultate VALUES(@tip, @email, @punctaj, @data);");
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                string[] detalii = olinie.Split(';');
                sqlCommand.Parameters.AddWithValue("@tip", Convert.ToInt32(detalii[0]));
                sqlCommand.Parameters.AddWithValue("@email", detalii[1]);
                sqlCommand.Parameters.AddWithValue("@punctaj", Convert.ToInt32(detalii[2]));
                sqlCommand.Parameters.AddWithValue("@data", DateTime.ParseExact(detalii[3].ToString(), "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture));
                sqlCommand.ExecuteNonQuery();
                olinie = reader.ReadLine();
            }
            reader.Close();
            sqlConnection.Close();
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "QRCode\\";
            openFileDialog.Filter = "(*.png)|*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
                MessagingToolkit.QRCode.Codec.QRCodeDecoder objDecodare = new MessagingToolkit.QRCode.Codec.QRCodeDecoder();
                string sirCodare = objDecodare.decode(new MessagingToolkit.QRCode.Codec.Data.QRCodeBitmapImage(pictureBox1.Image as Bitmap));
                string[] detalii = sirCodare.Split('\n');
                emailtxt.Text = detalii[1];
                parolatxt.Text = detalii[2];
            }
        }

        private void Inregistrare_Click(object sender, EventArgs e)
        {
            Inregistrare inregistrare = new Inregistrare();
            Autentificare.ActiveForm.Hide();
            inregistrare.Show();
        }

        private void Logarebtn_Click(object sender, EventArgs e)
        {
            bool ok = false;
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString() == emailtxt.Text && reader[2].ToString() == parolatxt.Text) 
                {
                    Alege_joc alege_Joc = new Alege_joc(reader[0].ToString(), reader[1].ToString());
                    Autentificare.ActiveForm.Hide();
                    alege_Joc.Show();
                    ok = true;
                }
            }
            if (!ok)
            {
                MessageBox.Show("Date de autentificare invalide!");
                emailtxt.Text = "";
                parolatxt.Text = "";
            }
        }
    }
}

using MessagingToolkit.QRCode.Geom;
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

namespace Jocuri
{
    public partial class Inregistrare : Form
    {
        public Inregistrare()
        {
            InitializeComponent();
        }

        private void renunta_Click(object sender, EventArgs e)
        {
            Autentificare autentificare = new Autentificare();
            Inregistrare.ActiveForm.Hide();
            autentificare.Show();
        }

        void resetTextBox()
        {
            foreach (TextBox textBox in this.Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
        }

        private void inregistrarebtn_Click(object sender, EventArgs e)
        {
            bool ok = true;
            
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                if (reader[0].ToString() == emailtxt.Text)
                {
                    MessageBox.Show("Email-ul introdus exista deja in baza de date!");
                    ok = false;
                    resetTextBox();
                }
            }
            reader.Close();
            if ((parolatxt.Text == "" || confirmaretxt.Text != parolatxt.Text) && ok)
            {
                MessageBox.Show("Parola invalida!");
                ok = false;
                resetTextBox() ;
            }
            int a = 0, p=0;
            for(int i = 0; i < emailtxt.TextLength; i++)
            {
                if (emailtxt.Text[i] == '@')
                {
                    a++;
                }
                if (emailtxt.Text[i] == '.' && a == 1)
                {
                    p++;
                }
            }
            if ((a != 1 || p != 1) && ok) {
                MessageBox.Show("Email invalid!");
                ok = false;
                resetTextBox();
            }
            if (emailtxt.Text != "" && numetxt.Text != "" && ok) 
            {
                command = String.Format("INSERT INTO Utilizatori VALUES(@email, @nume, @parola);");
                sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@email", emailtxt.Text);
                sqlCommand.Parameters.AddWithValue("@nume", numetxt.Text);
                sqlCommand.Parameters.AddWithValue("@parola", parolatxt.Text);
                sqlCommand.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Completati toate casutele de text!");
                ok = false;
                resetTextBox();
            }
            sqlConnection.Close();
        }

        private void Inregistrare_Load(object sender, EventArgs e)
        {

        }
    }
}

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

namespace CarRacing
{
    public partial class GameOver : Form
    {
        public GameOver()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
                string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Masini.mdf;Integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();
                string command = String.Format("INSERT INTO Highscore VALUES(@nume, @scor);");
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@nume", textBox1.Text.ToUpper());
                sqlCommand.Parameters.AddWithValue("@scor", (CarRacing.Scor) / 5);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
                GameOver.ActiveForm.Hide();
                
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Examen_UBB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        public static string numeUtilizator;
        public static string functieUtilizator;
        public static int id=0;
        public static int idLogat = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                string email = reader[2].ToString();
                email = email.Trim();

                string parola = reader[3].ToString();
                parola = parola.Trim();
               
                if (emailtxt.Text.ToString() == email && parolatxt.Text.ToString() == parola)
                {
                    MessageBox.Show("Autentificare reusita!");

                    numeUtilizator = reader[1].ToString();
                    functieUtilizator = reader[5].ToString();
                    idLogat = Convert.ToInt32(reader[0].ToString());
                    string b = String.Format("Bine ai venit {0}!", reader[1].ToString());
                    MessageBox.Show(b.ToString());

                    Pre_examen examen = new Pre_examen();
                    Form1.ActiveForm.Hide();
                    examen.Show();
                    break;
                }
                else
                {
                    MessageBox.Show("Autentificare nereusita!");

                    emailtxt.Clear();
                    parolatxt.Clear();
                }
            }
            reader.Close();
            sqlConnection.Close();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Utilizatori";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            int ok = 1;

            while (reader.Read())
            {
                id = Convert.ToInt32(reader[0].ToString());
                string email = reader[2].ToString();
                email = email.Trim();

                string parola = reader[3].ToString();
                parola = parola.Trim();

                if (emailtxti.Text.ToString() == email || numetxti.Text.ToString() == reader[1].ToString().Trim())
                {
                    ok = 0;
                }
                if (ok == 0)
                {
                    MessageBox.Show("Email-ul sau numele introdus exista deja in baza de date!");
                    break;
                }
            }

            reader.Close();

            if (ok != 0)
            {
                string nume = numetxti.Text.ToString();
                nume = nume.Trim(); 

                string email = emailtxti.Text.ToString();
                email = email.Trim();

                string parola = parolatxti.Text.ToString();
                parola = parola.Trim();

                int a = 0;
                int p = 0;
                for (int i = 1; i <= email.Length - 3; i++)
                {
                    if (email[i] == '@') a++;
                    if (email[i] == '.') p++;
                }

                if (a == 1 && p >= 1)
                {
                    if (parola.Length > 4)
                    {
                        string inserare = String.Format("INSERT INTO Utilizatori VALUES(@nume, @email, @parola, @maxp, @rol);");
                        SqlCommand sqlCommand1 = new SqlCommand(inserare, sqlConnection);
                        sqlCommand1.Parameters.AddWithValue("@nume", nume.ToString());
                        sqlCommand1.Parameters.AddWithValue("@email", email.ToString());
                        sqlCommand1.Parameters.AddWithValue("@parola", parola.ToString());
                        sqlCommand1.Parameters.AddWithValue("@maxp", "0");
                        sqlCommand1.Parameters.AddWithValue("@rol", "0");
                        sqlCommand1.ExecuteNonQuery();

                        MessageBox.Show("Inregistrare reusita!");

                        numetxti.Clear();
                        emailtxti.Clear();
                        parolatxti.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Parola este prea scurta!");
                    }
                }
                else
                {
                    MessageBox.Show("Email-ul introdus nu contine caractere necesare (@ sau .) pentru validare!");
                }

                string command2 = String.Format("INSERT INTO Greseli VALUES(@id, @expresii, @aritmetica, @complexitate, @recursivitate, @divizibilitate, @prelucrare, @vectori, @matrici, @siruri, @backtracking);");
                SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection);
                sqlCommand2.Parameters.AddWithValue("@id", id+1);
                sqlCommand2.Parameters.AddWithValue("@expresii", "0");
                sqlCommand2.Parameters.AddWithValue("@aritmetica", "0");
                sqlCommand2.Parameters.AddWithValue("@complexitate", "0");
                sqlCommand2.Parameters.AddWithValue("@recursivitate", "0");
                sqlCommand2.Parameters.AddWithValue("@divizibilitate", "0");
                sqlCommand2.Parameters.AddWithValue("@prelucrare", "0");
                sqlCommand2.Parameters.AddWithValue("@vectori", "0");
                sqlCommand2.Parameters.AddWithValue("@matrici", "0");
                sqlCommand2.Parameters.AddWithValue("@siruri", "0");
                sqlCommand2.Parameters.AddWithValue("@backtracking", "0");

                sqlCommand2.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            string command = String.Format("DBCC CHECKIDENT ('Utilizatori', RESEED, 0);");
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            */
        }
    }
}

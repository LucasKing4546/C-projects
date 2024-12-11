using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_UBB
{
    public partial class Examen : Form
    {
        ////                                            \\\\
        //// ------------- VECTORI STRUCT ------------- \\\\
        ////                                            \\\\
        
        public static Vec[] greseli = {
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0)
        };

        public static Vec[] probleme = {
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0),
            new Vec(0, 0)
        };

        ////                                        \\\\
        //// ------------- DICTIONARY ------------- \\\\
        ////                                        \\\\
        
        Dictionary<int, int> idprobleme = new Dictionary<int, int>();
        Dictionary<int, int> nrprobleme = new Dictionary<int, int>();
        Dictionary<int, int> raspuns = new Dictionary<int, int>();
        Dictionary<int, int> categorie = new Dictionary<int, int>();
        Dictionary<int, int> nrgreseli = new Dictionary<int, int>();

        ////                                               \\\\
        //// ------------- VARIABILE GLOBALE ------------- \\\\
        ////   

        int s = 3600 * 3;
        string intrebare = "1";
        int c = 0;
        int k = 0;
        public static int nr_intrebari = 24;
        public static double scortotal = 10;

        ////                                                                     \\\\
        //// ------------- PREULARE VARIABILE & INITIALIZARE FORMA ------------- \\\\
        ////                                                                     \\\\
        public Examen()
        {
            InitializeComponent();
        }

        public Examen(Vec[] prob)
        {
            InitializeComponent();
            probleme = prob;
        }

        ////                                               \\\\
        //// ------------- INCARCAREA FORMEI ------------- \\\\
        ////                                               \\\\
        private void Examen_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Greseli";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                if (reader[0].ToString() == Form1.idLogat.ToString())
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        nrgreseli[i] = Convert.ToInt32(reader[i].ToString());
                    }
                }
            }
            reader.Close();
            sqlConnection.Close();

            for (int i = 0; i < 10; i++)
            {
                nrprobleme[i] = 0;
            }

            numarareProbleme();

            if (Pre_examen.tip_examen == "Antrenament") loadAntrenament();
            else if (Pre_examen.tip_examen == "Test recomandat") loadTestRecomandat();
            else loadVarianta(Pre_examen.an_examen + Pre_examen.tip_examen);
        }

        ////                                      \\\\
        //// ------------- LOAD-URI ------------- \\\\
        ////                                      \\\\
        private void loadTestRecomandat()
        {
            setupTimer(timer1);

            double s = 0; //numarul de greseli totale
            int n = 0; //numarul de probleme

            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            string command = String.Format("SELECT * FROM Greseli WHERE idElev = '{0}'", Form1.idLogat);
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            int i;
            while (reader.Read())
            {
                for (i = 0; i < 10; i++)
                {
                    greseli[i].val = Convert.ToInt32(reader[i + 1].ToString());
                    greseli[i].poz = i + 1;
                    s += greseli[i].val;
                } 
            }
            
            reader.Close();
            sqlConnection.Close();

            sortare(greseli, 10);
            
            for (i = 9; i >= 0; i--)
            {
                double procentaj = (greseli[i].val*100) / s;
                double p = (procentaj * nr_intrebari) / 100;
                probleme[i].val = Convert.ToInt32(p);
                n += probleme[i].val;
                probleme[i].poz = greseli[i].poz;
            }
            selectareIdProbleme(probleme);
        }

        private void loadAntrenament()
        {
            setupTimer(timer1);
            selectareIdProbleme(probleme);
        }

        void loadVarianta(string varianta)
        {
            if (Convert.ToInt32(Pre_examen.an_examen)<2023)
            {
                nr_intrebari = 30;
                button25.Visible = Enabled;
                button26.Visible = Enabled;
                button27.Visible = Enabled;
                button28.Visible = Enabled;
                button29.Visible = Enabled;
                button30.Visible = Enabled;
            }

            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = String.Format("UPDATE Raspunsuri SET A='{0}',B='{1}',C='{2}',D='{3}';", 0, 0, 0, 0);
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();

            string command2 = "SELECT * FROM Grila";
            SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection);
            SqlDataReader reader = sqlCommand2.ExecuteReader();

            int i = 0;

            while (reader.Read())
            {

                if (reader[8].ToString() == varianta)
                {

                    if (i == 0) { setupImagini(reader); setupFont(); }
                    i++;
                    idprobleme[i] = Convert.ToInt32(reader[0].ToString());
                    raspuns[i] = Convert.ToInt32(reader[7].ToString());
                    categorie[i] = Convert.ToInt32(reader[9].ToString());
                    if (i == nr_intrebari) break;
                }
            }

            setupTimer(timer1);

            reader.Close();
            sqlConnection.Close();
        }

        ////                                               \\\\
        //// ------------- SUBPROGRAME UTILE ------------- \\\\
        ////                                               \\\\

        private void numarareProbleme()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT idGrila, tip FROM Grila";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                nrprobleme[Convert.ToInt32(reader[1].ToString()) - 1]++;
            }

            sqlConnection.Close();
            reader.Close();
        }

        private void selectareIdProbleme(Vec[] probleme)
        {

            AppDomain.CurrentDomain.SetData("DataDirectory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            for (int i = 0; i < 10; i++)
            {
                if (probleme[i].val != 0)
                {
                    int r;
                    int n = nrprobleme[probleme[i].poz];
                    Random random = new Random();
                    if (n - probleme[i].val == 0)
                    {
                        r = 0;
                    }
                    else
                    {
                        r = random.Next(1, 1000000) % (n - probleme[i].val) + 1;
                    }
                    string command = "SELECT * FROM Grila";
                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader[9].ToString()) == probleme[i].poz)
                        {
                            if (r == 0)
                            {
                                if (k == 0) { setupImagini(reader); setupFont(); }
                                k++;
                                idprobleme[k] = Convert.ToInt32(reader[0].ToString());
                                raspuns[k] = Convert.ToInt32(reader[7].ToString());
                                categorie[k] = Convert.ToInt32(reader[9].ToString());
                                if (k == nr_intrebari) break;
                            }
                            else
                            {
                                r--;
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void sortare(Vec[] a, int n)
        {
            for (int i=0; i<n; i++)
            {
                for (int j=i+1; j<n; j++)
                {
                    if (a[i].val > a[j].val)
                    {
                        (a[i], a[j]) = (a[j], a[i]);
                    }
                }
            }
        }

        private double returnarePunctaj(SqlDataReader reader, double t, double p, int ras, double scor, int poz)
        {
            while (ras != 0)
            {
                if (ras % 10 == 1)
                {
                    t++;
                }
                ras /= 10;
            }

            int A = Convert.ToInt32(reader[1].ToString().Trim());
            int B = Convert.ToInt32(reader[2].ToString().Trim());
            int C = Convert.ToInt32(reader[3].ToString().Trim());
            int D = Convert.ToInt32(reader[4].ToString().Trim());

            int nr = 0;
            ras = raspuns[Convert.ToInt32(reader[0].ToString().Trim())];

            if (ras % 10 == 1 && A == 1)
            {
                scor = scor + p / t;
            }
            else if (ras % 10 == 0 && A == 1)
            {
                scor = scor - (0.66) * p / t;
                nr++;
            }
            else if(ras % 10 == 1 && A == 0) nr++;

            ras /= 10;
            if (ras % 10 == 1 && B == 1)
            {
                scor = scor + p / t;
            }
            else if (ras % 10 == 0 && B == 1)
            {
                scor = scor - (0.66) * p / t;
                nr++;
            }
            else if (ras % 10 == 1 && B == 0) nr++;

            ras /= 10;
            if (ras % 10 == 1 && C == 1)
            {
                scor = scor + p / t;
            }
            else if (ras % 10 == 0 && C == 1)
            {
                scor = scor - (0.66) * p / t;
                nr++;
            }
            else if (ras % 10 == 1 && C == 0) nr++;

            ras /= 10;
            if (ras % 10 == 1 && D == 1)
            {
                scor = scor + p / t;
            }
            else if (ras % 10 == 0 && D == 1)
            {
                scor = scor - (0.66) * p / t;
                nr++;
            }
            else if (ras % 10 == 1 && D == 0) nr++;

            if (nr > 0) nrgreseli[poz]++;
            return scor;
        }

        ////                                       \\\\
        //// ------------- SETUP-URI ------------- \\\\
        ////                                       \\\\

        private void setupTimer(Timer timer)
        {
            nrintrebare.Text = "Intrebarea 1/" + nr_intrebari.ToString();
            timerrrr.Text = "3:00:00";
            timer.Start();
        }

        private void setupFont()
        {
            A.Font = new Font(A.Font.FontFamily, 12, FontStyle.Bold);
            B.Font = new Font(B.Font.FontFamily, 12, FontStyle.Bold);
            C.Font = new Font(C.Font.FontFamily, 12, FontStyle.Bold);
            D.Font = new Font(D.Font.FontFamily, 12, FontStyle.Bold);
        }

        private void setupImagini(SqlDataReader reader)
        {
            Image img1 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[1].ToString().Trim());
            cerinta.Height = img1.Height;
            cerinta.Image = img1;

            Image img2 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[2].ToString().Trim());
            algoritm.Height = img2.Height;
            algoritm.Width = img2.Width;
            algoritm.Image = img2;

            Image img3 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[3].ToString().Trim());
            raspunsA.Height = img3.Height;
            imagineA.Height = img3.Height - 5;
            imagineA.Image = img3;

            Image img4 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[4].ToString().Trim());
            raspunsB.Height = img4.Height;
            imagineB.Height = img4.Height - 5;
            imagineB.Image = img4;

            Image img5 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[5].ToString().Trim());
            raspunsC.Height = img5.Height;
            imagineC.Height = img5.Height - 5;
            imagineC.Image = img5;

            Image img6 = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + '\\' + reader[6].ToString().Trim());
            raspunsD.Height = img6.Height;
            imagineD.Height = img6.Height - 5;
            imagineD.Image = img6;

            int maxwidth = Math.Max(Math.Max(img3.Width, img4.Width), Math.Max(img5.Width, img6.Width));
            imagineA.Width = maxwidth;
            imagineB.Width = maxwidth;
            imagineC.Width = maxwidth;
            imagineD.Width = maxwidth;
            int Height = imagineA.Height + imagineB.Height + imagineC.Height + imagineD.Height;

            if (maxwidth > 1888 - algoritm.Width)
            {
                maxwidth = 1888;
                if (Height > 930 - cerinta.Height - algoritm.Height) raspunsuri.Height = Height + 30;
                else raspunsuri.Height = 930 - cerinta.Height - algoritm.Height;
            }
            else
            {
                maxwidth = 1888 - algoritm.Width;
                if (Height > 930 - cerinta.Height) raspunsuri.Height = Height;
                else raspunsuri.Height = 930 - cerinta.Height;
            }
            raspunsuri.Width = maxwidth;
            raspunsA.Width = maxwidth;
            raspunsB.Width = maxwidth;
            raspunsC.Width = maxwidth;
            raspunsD.Width = maxwidth;
        }

        ////                                                         \\\\
        //// ------------- CALCULARE PUNCTAJ & SALVARE ------------- \\\\
        ////                                                         \\\\

        private void submitAntrenament()
        {
            double p = 3.75;

            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Raspunsuri";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                double t = 0, scor = 0;
                int ras = raspuns[Convert.ToInt32(reader[0].ToString().Trim())];

                scor = returnarePunctaj(reader, t, p, ras, scor, categorie[Convert.ToInt32(reader[0].ToString().Trim())]);

                if (scor < 0) scor = 0;
                scortotal += scor;
            }
            reader.Close();
        }

        private void submitraspunsuri()
        {
            double p = 3.75;

            AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = "SELECT * FROM Raspunsuri";
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                if (Convert.ToInt32(reader[0].ToString().Trim()) > nr_intrebari) break;
                double t=0, scor=0;
                int ras = raspuns[Convert.ToInt32(reader[0].ToString().Trim())];

                scor = returnarePunctaj(reader, t, p, ras, scor, categorie[Convert.ToInt32(reader[0].ToString().Trim())]);

                if (scor < 0) scor = 0;
                scortotal += scor;
            }
            reader.Close();

            string commnad2 = String.Format("INSERT INTO Teste VALUES(@nume, @punctaj, @data);");
            SqlCommand sqlCommand1 = new SqlCommand(commnad2, sqlConnection);
            sqlCommand1.Parameters.AddWithValue("@nume", Form1.numeUtilizator);
            sqlCommand1.Parameters.AddWithValue("@punctaj", scortotal.ToString());
            sqlCommand1.Parameters.AddWithValue("@data", DateTime.Now);
            sqlCommand1.ExecuteNonQuery();

            if (scortotal > Pre_examen.highscore)
            {
                string command3 = String.Format("UPDATE Utilizatori SET maxpunctaj='{0}' WHERE nume LIKE '{1}';", scortotal.ToString(), Form1.numeUtilizator);
                SqlCommand sqlCommand2 = new SqlCommand(command3, sqlConnection);
                sqlCommand2.ExecuteNonQuery();
            }

            string command4 = String.Format("UPDATE Greseli SET expresii=@expresii, aritmetica_modulara=@aritmetica, complexitate=@complexitate, recursivitate=@recursivitate, divizibilitate=@divizibilitate, prelucrarea_cifrelor=@prelucrare, vectori=@vectori, matrici=@matrici, siruri=@siruri, backtracking=@backtracking WHERE idElev=@id;");
            SqlCommand sqlCommand3 = new SqlCommand(command4, sqlConnection);
            sqlCommand3.Parameters.AddWithValue("@id", Form1.idLogat);
            sqlCommand3.Parameters.AddWithValue("@expresii", nrgreseli[1].ToString());
            sqlCommand3.Parameters.AddWithValue("@aritmetica", nrgreseli[2].ToString());
            sqlCommand3.Parameters.AddWithValue("@complexitate", nrgreseli[3].ToString());
            sqlCommand3.Parameters.AddWithValue("@recursivitate", nrgreseli[4].ToString());
            sqlCommand3.Parameters.AddWithValue("@divizibilitate", nrgreseli[5].ToString());
            sqlCommand3.Parameters.AddWithValue("@prelucrare", nrgreseli[6].ToString());
            sqlCommand3.Parameters.AddWithValue("@vectori", nrgreseli[7].ToString());
            sqlCommand3.Parameters.AddWithValue("@matrici", nrgreseli[8].ToString());
            sqlCommand3.Parameters.AddWithValue("@siruri", nrgreseli[9].ToString());
            sqlCommand3.Parameters.AddWithValue("@backtracking", nrgreseli[10].ToString());
            sqlCommand3.ExecuteNonQuery();
            sqlConnection.Close();

            Rezultate rezultate = new Rezultate(raspuns);
            Examen.ActiveForm.Hide();
            rezultate.Show();
            
        }

        ////                                   \\\\
        //// ------------- TIMER ------------- \\\\
        ////                                   \\\\

        private void timer1_Tick(object sender, EventArgs e)
        {
            s--;
            if (s == 0)
            {
                MessageBox.Show("Timpul s-a terminat!");
                submitraspunsuri();
                Pre_examen pre_Examen = new Pre_examen();
                Examen.ActiveForm.Hide();
                pre_Examen.Show();
            }
            else
            {
                if (s % 60 < 10)
                {
                    if ((s / 60) % 60 < 10)
                    {
                        timerrrr.Text = (s / 3600).ToString() + ":0" + ((s / 60) % 60).ToString() + ":0" + (s % 60).ToString();
                    }
                    else
                    {
                        timerrrr.Text = (s / 3600).ToString() + ":" + ((s / 60) % 60).ToString() + ":0" + (s % 60).ToString();
                    }
                }
                else
                {
                    timerrrr.Text = (s / 3600).ToString() + ':' + ((s / 60) % 60).ToString() + ':' + (s % 60).ToString();
                }
            }
        }

        ////                                     \\\\
        //// ------------- BUTOANE ------------- \\\\
        ////                                     \\\\
        
        private void Intrebare(object sender, EventArgs e)
        {
            try
            {
                c = 1;
                A.Checked = false;
                B.Checked = false;
                C.Checked = false;
                D.Checked = false;
                c = 0;

                Button button = (Button)sender;
                nrintrebare.Text = "Intrebarea " + button.Text + '/' + nr_intrebari.ToString();
                intrebare = button.Text;

                AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

                string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();

                string command = "SELECT * FROM Grila";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    if (idprobleme[Convert.ToInt32(button.Text)].ToString() == reader[0].ToString().Trim())
                    {
                        setupImagini(reader);
                        setupFont();
                        break;
                    }
                }
                reader.Close();

                string command2 = "SELECT * FROM Raspunsuri";
                SqlCommand sqlCommand2 = new SqlCommand(command2, sqlConnection);

                SqlDataReader reader2 = sqlCommand2.ExecuteReader();

                while (reader2.Read())
                {
                    if (reader2[0].ToString().Trim() == button.Text)
                    {
                        if (reader2[1].ToString().Trim() == "1")
                        {
                            A.Checked = true;
                        }
                        if (reader2[2].ToString().Trim() == "1")
                        {
                            B.Checked = true;
                        }
                        if (reader2[3].ToString().Trim() == "1")
                        {
                            C.Checked = true;
                        }
                        if (reader2[4].ToString().Trim() == "1")
                        {
                            D.Checked = true;
                        }
                    }
                }
                reader2.Close();
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (Pre_examen.tip_examen == "Antrenament") submitAntrenament();
            else submitraspunsuri();
        }

        ////                                          \\\\
        //// ------------- CHECKBOX-URI ------------- \\\\
        ////                                          \\\\

        private void Bifa(object sender, EventArgs e)
        {
            if (c == 0)
            {
                CheckBox checkBox = (CheckBox)sender;

                AppDomain.CurrentDomain.SetData("Data Directory", System.Environment.CurrentDirectory.Replace("\\bin\\Debug", ""));

                string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Grile.mdf;Integrated Security=True";
                SqlConnection sqlConnection = new SqlConnection(connection);
                sqlConnection.Open();

                int ok = 0;
                string command = "";

                if (checkBox.Checked == true) ok = 1;

                if (checkBox.Name == "A")
                {
                    command = String.Format("UPDATE Raspunsuri SET A='{0}' WHERE idraspuns='{1}';", ok, Convert.ToInt32(intrebare));
                }
                else if (checkBox.Name == "B")
                {
                    command = String.Format("UPDATE Raspunsuri SET B='{0}' WHERE idraspuns='{1}';", ok, Convert.ToInt32(intrebare));
                }
                else if (checkBox.Name == "C")
                {
                    command = String.Format("UPDATE Raspunsuri SET C='{0}' WHERE idraspuns='{1}';", ok, Convert.ToInt32(intrebare));
                }
                else if (checkBox.Name == "D")
                {
                    command = String.Format("UPDATE Raspunsuri SET D='{0}' WHERE idraspuns='{1}';", ok, Convert.ToInt32(intrebare));
                }

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}

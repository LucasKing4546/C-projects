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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using MessagingToolkit.QRCode.Codec.Data;

namespace Jocuri
{
    public partial class PrimQR : Form
    {
        string emaill, nume;
        public PrimQR(string emailLogat, string numeLogat)
        {
            InitializeComponent();
            emaill = emailLogat;
            nume = numeLogat;
        }

        int[] numere = new int[100];
        int[] prim = new int[25];
        int k = 0;

        private void PrimQR_Load(object sender, EventArgs e)
        {
            for (int d = 2; d < 51; d++)
            {
                if (numere[d] == 0)
                {
                    for (int i = d+1; i<100; i++)
                    {
                        if (i%d == 0)
                            numere[i] = 1;
                    }
                }
            }
            for (int d = 2;d < 100; d++)
            {
                if (numere[d] == 0)
                {
                    prim[k++] = d;
                }
            }
        }

        int cautareBinara(int val)
        {
            int st = 0, dr = 24;
            int mij = 0, poz = 0;
            while (st <= dr)
            {
                mij = (st+dr)/2;
                if (prim[mij] > val)
                {
                    poz = mij;
                    dr = mij - 1;
                }
                else
                {
                    st = mij + 1;
                }
            }
            return poz;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxi = 0;
            string emailmaxi = "";
            string puctajprim = "";
            string connection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Jocuri.mdf;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();

            string command = String.Format("SELECT * FROM Rezultate");
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
        
            while (reader.Read())
            {
                int punctaj = Convert.ToInt32(reader[3].ToString());
                string email = reader[2].ToString();

                int poz = cautareBinara(punctaj);
                if (prim[poz] - punctaj == maxi)
                {
                    if (String.Compare(email, emailmaxi) < 0)
                    {
                        emailmaxi = email;
                    }
                }
                else if (prim[poz] - punctaj > maxi)
                {
                    maxi = prim[poz] - punctaj;
                    puctajprim = prim[poz].ToString();
                    emailmaxi = email;
                }
            }

            string rezultat = emailmaxi + "\r\n" + puctajprim;
            MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
            encoder.QRCodeScale = 8;
            Bitmap bmp = new Bitmap(400, 400);
            bmp = encoder.Encode(rezultat);
            Graphics graphics = Graphics.FromImage(bmp);
            Rectangle rectangle = new Rectangle(160, 160, 40, 40);
            graphics.DrawImage(Properties.Resources.Logo_C_, rectangle);
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Alege_joc alege_Joc = new Alege_joc(emaill, nume);
            PrimQR.ActiveForm.Hide();
            alege_Joc.Show();
        }

    }
}

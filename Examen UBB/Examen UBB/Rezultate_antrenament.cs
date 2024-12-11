using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_UBB
{
    public partial class Rezultate : Form
    {
        Dictionary<int, int> raspunsuri = new Dictionary<int, int>();

        public Rezultate()
        {
            InitializeComponent();
        }
        public Rezultate(Dictionary<int, int> ras)
        {
            InitializeComponent();
            foreach (KeyValuePair<int, int> pair in ras)
            {
                raspunsuri.Add(pair.Key, pair.Value);
            }
        }

        private void Rezultate_Load(object sender, EventArgs e)
        {
            if (Pre_examen.tip_examen == "Antrenament")
            {
                label1.Location = new Point(180, 74);
                label1.Text = "Rezultate antrenament";
            }
            else if (Pre_examen.tip_examen == "Test recomandat")
            {
                label1.Location = new Point(150, 74);
                label1.Text = "Rezultate test recomandat";
            }
            else
            {
                label1.Location = new Point(208, 74);
                label1.Text = "Rezultate varianta";
                label2.Text = Pre_examen.an_examen + " " + Pre_examen.tip_examen;
                label2.Visible = true;
            }
            punctaj.Text = Examen.scortotal.ToString() + "p/100p";
        }

        private void descarcare_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text (*.txt)|*.txt";
            saveFileDialog1.Title = "Salveaza barem";
            saveFileDialog1.DefaultExt = "txt";

            if (Pre_examen.tip_examen == "Antrenament") saveFileDialog1.FileName = "barem_Antrenament";
            else if (Pre_examen.tip_examen == "Test recomandat") saveFileDialog1.FileName = "barem_TestRecomandat";
            else saveFileDialog1.FileName = "barem_" + Pre_examen.an_examen + Pre_examen.tip_examen;
            

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                StreamWriter writer = new StreamWriter(path);
                for (int i = 1; i <= Examen.nr_intrebari; i++)
                {
                    string info = (i).ToString() + ". ";
                    int x = raspunsuri[i];
                    char c = 'A';
                    while (x != 0)
                    {
                        if (x % 10 == 1) info += c;
                        c++;
                        x /= 10;
                    }
                    writer.WriteLine(info);
                }
                writer.Close();

                MessageBox.Show("Barem salvat cu succes!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pre_examen pre_Examen = new Pre_examen();
            this.Close();
            pre_Examen.Show();
        }
    }
}

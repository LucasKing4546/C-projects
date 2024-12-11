using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jocuri
{
    public partial class Animatie : Form
    {
        string email, nume;
        public Animatie(string emailLogat, string numeLogat)
        {
            InitializeComponent();
            email = emailLogat;
            nume = numeLogat;
        }

        private void Animatie_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Artificii.Start();
        }

        string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Artificii\\");
        int tick = 0;
        private void Artificii_Tick(object sender, EventArgs e)
        {
            
           
            foreach (PictureBox artifica in this.Controls.OfType<PictureBox>())
            {
                int index = Convert.ToInt32(artifica.Tag);
                index++;
                if (index == files.Length - 1) 
                {
                    this.Controls.Remove(artifica);
                }
                else
                {
                    artifica.Tag = index;
                    artifica.Image = Image.FromFile(files[index]);
                }
            }
            Random random = new Random();
            int x = random.Next(1, 1000000) % 740;
            int y = random.Next(1, 10000000) % 440;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(60, 60);
            pictureBox.Location = new Point(x, y);  
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = Image.FromFile(files[0]);
            pictureBox.Tag = 0;
            this.Controls.Add(pictureBox);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            if (tick == 2)
            {
                timer1.Stop();
                Artificii.Stop();
                Alege_joc alege_Joc = new Alege_joc(email, nume);
                Animatie.ActiveForm.Hide();
                alege_Joc.Show();
            }
        }
    }
}

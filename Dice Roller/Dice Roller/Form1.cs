using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dice_Roller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int rolltime1 = 0;
        int rolltime2 = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (rolltime1 < 5) {
                rolltime1++;
                Random random = new Random();
                int dice1 = random.Next(1, 6);
                pictureBox1.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\pics\\" + dice1 + ".png");
            }

            if (rolltime2 < 8)
            {
                rolltime2++;
                Random random1 = new Random();
                int dice2 = random1.Next(1, 6);
                pictureBox2.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "\\pics\\" + dice2 + ".png");
            }
            else
            {
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rolltime1 = 0;
            rolltime2 = 0;
            timer1.Start();
        }
    }
}

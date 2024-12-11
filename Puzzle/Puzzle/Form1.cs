using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<PictureBox> puzzle = new List<PictureBox>();
        List<string> original = new List<string>();
        PictureBox clickedPictureBox;
        Stopwatch stopwatch = new Stopwatch();
        int left = 10;
        int top = 20;
        int[] n = { 0, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] m = { 0, 2, 3, 4, 5, 6, 7, 8, 9 };
        int moves = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            foreach (PictureBox pictureBox in puzzle)
            {
                pictureBox.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            foreach (PictureBox pictureBox in puzzle)
            {
                pictureBox.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stopwatch.Stop();
            Application.Exit();
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            stopwatch.Start();

            Random random = new Random();
            n = n.OrderBy(x => random.Next()).ToArray();

            for (int i = 0; i <= 8; i++)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Left = left;
                pictureBox.Top = top;
                Size size = new Size(150, 150);
                pictureBox.Size = size;
                pictureBox.Tag = i + 1;
                pictureBox.BackColor = Color.Gray;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Click += PictureBox_Click;
                if (n[i] != 0)
                {
                    pictureBox.Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + n[i] + ".jpg");
                }
                else
                {
                    pictureBox.Image = null;
                }
                puzzle.Add(pictureBox);
                this.groupBox1.Controls.Add(pictureBox);

                if ((i + 1) % 3 == 0)
                {
                    top += 150;
                    left = 10;
                }
                else
                {
                    left += 150;
                }
            }
        }
        private void GameOver()
        {
            timer1.Stop();
            puzzle[0].Image = Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + 1 + ".jpg");
            foreach (PictureBox pictureBox in puzzle)
            {
                pictureBox.Enabled = false;
            }
            MessageBox.Show("Felicitari ai castigat!");

        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
           
            clickedPictureBox = sender as PictureBox;
            int tag = Convert.ToInt32(clickedPictureBox.Tag);

            if (tag >= 2 && puzzle[tag - 2].Image == null && (tag-1) % 3 != 0)
            {
                Image image = clickedPictureBox.Image;
                puzzle[tag - 2].Image = image;
                clickedPictureBox.Image = null;
                int aux = n[tag - 2];
                n[tag - 2] = n[tag - 1];
                n[tag - 1] = aux;
                moves++;
                label2.Text = "Moves: " + moves.ToString();
            }
            else if (tag <= 8 && puzzle[tag].Image == null && tag%3!=0)
            {
                Image image = clickedPictureBox.Image;
                puzzle[tag].Image = image;
                clickedPictureBox.Image = null;
                int aux = n[tag];
                n[tag] = n[tag - 1];
                n[tag - 1] = aux;
                moves++;
                label2.Text = "Moves: " + moves.ToString();
            }
            else if (tag >= 4 && puzzle[tag - 4].Image == null)
            {
                Image image = clickedPictureBox.Image;
                puzzle[tag - 4].Image = image;
                clickedPictureBox.Image = null;
                int aux = n[tag - 4];
                n[tag - 4] = n[tag - 1];
                n[tag - 1] = aux;
                moves++;
                label2.Text = "Moves: " + moves.ToString();
            }
            else if (tag <= 6 && puzzle[tag + 2].Image == null)
            {
                Image image = clickedPictureBox.Image;
                puzzle[tag + 2].Image = image;
                clickedPictureBox.Image = null;
                int aux = n[tag + 2];
                n[tag + 2] = n[tag - 1];
                n[tag - 1] = aux;
                moves++;
                label2.Text = "Moves: " + moves.ToString();
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private bool Win()
        {
            int i;
            for (i=0; i<8; i++)
            {
                if (n[i] != m[i])
                    break;
            }
            if (i == 8)
                return true;
            return false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label1.Text = stopwatch.Elapsed.ToString("hh\\:mm\\:ss");
            if (Win())
            {
                GameOver();
            }
        }
    }
}

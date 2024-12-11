using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            button1.BackColor = colorDialog1.Color;
        }
        bool moving = false;
        Bitmap bitmap = new Bitmap(805, 375);
        int x, y;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Salveaza";
            saveFileDialog1.FileName = "desen";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog1.FileName + ".jpg", ImageFormat.Jpeg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics g2 = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.White, 2);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            g2.DrawRectangle(pen, 0, 0, 805, 375);
            g2.FillRectangle(solidBrush, 0, 0, 805, 375);
            button1.BackColor = colorDialog1.Color;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving){
                Graphics g = panel1.CreateGraphics();
                Graphics g2 = Graphics.FromImage(bitmap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Pen pen = new Pen(colorDialog1.Color, trackBar1.Value);
                g.DrawLine(pen, x, y, e.X, e.Y);
                g2.DrawLine(pen, x, y, e.X, e.Y);
                x = e.X;
                y = e.Y;
            }
        }
    }
}

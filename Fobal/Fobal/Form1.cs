using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fobal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
        }
        private bool moveLeft = false;
        private bool moveRight = false;
        private bool moveUp = false;
        private bool moveDown = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = 5;
            int y = 5;
            if (moveDown == true)
            {
                x = 0;
                Minge.Location = new Point(x, y);
            }
            else if (moveUp == true)
            {
                x = 0;
                y = y * -1;
            }
            else if (moveLeft == true)
            {
                y = 0;
                x = x * -1;
            }
            else
            {

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                moveLeft = false;
                moveRight = false;
                moveUp = false;
                moveDown = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                moveLeft = false;
                moveRight = false;
                moveUp = true;
                moveDown = false;
            }
            else if(e.KeyCode == Keys.Left)
            {
                moveLeft = true;
                moveRight = false;
                moveUp = false;
                moveDown = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                moveLeft = false;
                moveRight = true;
                moveUp = false;
                moveDown = false;
            }
           
        }
          
    }
}

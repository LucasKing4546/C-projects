using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Penalty
{
   
    public partial class Penalty : Form
    {
        public Penalty()
        {
            InitializeComponent();
        }
        int r;
        List<PictureBox> portari = new List<PictureBox>();
        List<PictureBox> targets = new List<PictureBox>();

        private void move(int x, int y, float speed, PictureBox pictureBox, Timer timer)
        {
            float tx = x - pictureBox.Location.X;
            float ty = y - pictureBox.Location.Y;
            float length = (float)Math.Sqrt(tx * tx + ty * ty);
            if (length > speed)
            {
                pictureBox.Location = new Point((int)(pictureBox.Location.X + speed * tx / length), (int)(pictureBox.Location.Y + speed * ty / length)); 
            }
            else
            {
                pictureBox.Location = new Point(x, y);
                timer.Stop();
                if (pictureBox.Location == minge.Location)
                {
                    minge.Location = new Point(503, 614);
                    portari[r].Location = new Point(ox, oy);
                    portar.Visible = true;
                    portari[r].Visible = false;
                    label1.Text = "Scor: " + scor.ToString();
                    label2.Text = "Ratate: " + ratate.ToString();
                    enable();
                }
            }
        }
        int x, xp;
        int y, yp;
        int ox, oy;
        int scor = 0, ratate=0;
        private void Penalty_Load(object sender, EventArgs e)
        {
            rightsave.Visible = false;
            leftsave.Visible = false;
            topsave.Visible = false;
            toprightsave.Visible = false;
            topleftsave.Visible = false;
            portari.Add(rightsave);
            portari.Add(toprightsave);
            portari.Add(topsave);
            portari.Add(topleftsave);
            portari.Add(leftsave);
            targets.Add(downright);
            targets.Add(topright);
            targets.Add(top);
            targets.Add(topleft);
            targets.Add(downleft);
        }
        
        private void picturebox_Click(object sender, EventArgs e) {
            disable();
            PictureBox pictureBox = (PictureBox)sender;
            x = pictureBox.Location.X;
            y = pictureBox.Location.Y;
            timer1.Start();
            Random random = new Random();
            r = random.Next(0, 5);
            portari[r].Visible = true;
            ox = portari[r].Location.X; 
            oy = portari[r].Location.Y;
            portar.Visible = false;
            xp = targets[r].Location.X;
            yp = targets[r].Location.Y;
            timer2.Start();
            if (x == xp && y == yp)
            {
                ratate++;
            }
            else {
                scor++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            move(x, y, (float)10, minge, timer1);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            move(xp, yp, (float)7, portari[r], timer2);
        }

        private void disable()
        {
            downright.Enabled = false;
            downleft.Enabled = false;
            top.Enabled = false;
            topleft.Enabled = false;
            topright.Enabled = false;
        }

        private void enable()
        {
            downright.Enabled = true;
            downleft.Enabled = true;
            top.Enabled = true;
            topleft.Enabled = true;
            topright.Enabled = true;
        }
    }
}

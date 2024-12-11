using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_UBB
{
    public partial class SelectareCategorii : Form
    {
        public SelectareCategorii()
        {
            InitializeComponent();
        }

        private Vec[] probleme = {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Pre_examen pre_Examen = new Pre_examen();
            SelectareCategorii.ActiveForm.Hide();
            pre_Examen.Show();
        }

        private void start_Click(object sender, EventArgs e)
        {
            int nrprobleme = 0;
            for (int i = 0; i < categoriiSelectate.Items.Count; i++) {
                if (categoriiSelectate.GetItemChecked(i) == true)
                {
                    probleme[i].val = 24 / categoriiSelectate.CheckedItems.Count;
                    nrprobleme += probleme[i].val;
                    probleme[i].poz = i + 1;
                }
            }
            if (nrprobleme != 24) {
                for (int i=0; i<24 && nrprobleme<24; i++)
                {
                    if (probleme[i].val != 0)
                    {
                        probleme[i].val++;
                        nrprobleme++;
                    }
                }
            }

            Examen examen = new Examen(probleme);
            SelectareCategorii.ActiveForm.Hide();
            examen.Show();
        }
    }
}

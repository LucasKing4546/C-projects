namespace Jocuri
{
    partial class Inregistrare
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.emailtxt = new System.Windows.Forms.TextBox();
            this.numetxt = new System.Windows.Forms.TextBox();
            this.parolatxt = new System.Windows.Forms.TextBox();
            this.confirmaretxt = new System.Windows.Forms.TextBox();
            this.renunta = new System.Windows.Forms.Button();
            this.inregistrarebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(27, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email utilizator: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nume utilizator: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(27, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Parola: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(27, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Confirmare parola: ";
            // 
            // emailtxt
            // 
            this.emailtxt.Location = new System.Drawing.Point(250, 34);
            this.emailtxt.Name = "emailtxt";
            this.emailtxt.Size = new System.Drawing.Size(300, 30);
            this.emailtxt.TabIndex = 4;
            // 
            // numetxt
            // 
            this.numetxt.Location = new System.Drawing.Point(250, 90);
            this.numetxt.Name = "numetxt";
            this.numetxt.Size = new System.Drawing.Size(300, 30);
            this.numetxt.TabIndex = 5;
            // 
            // parolatxt
            // 
            this.parolatxt.Location = new System.Drawing.Point(250, 145);
            this.parolatxt.Name = "parolatxt";
            this.parolatxt.PasswordChar = '*';
            this.parolatxt.Size = new System.Drawing.Size(300, 30);
            this.parolatxt.TabIndex = 6;
            // 
            // confirmaretxt
            // 
            this.confirmaretxt.Location = new System.Drawing.Point(250, 197);
            this.confirmaretxt.Name = "confirmaretxt";
            this.confirmaretxt.PasswordChar = '*';
            this.confirmaretxt.Size = new System.Drawing.Size(300, 30);
            this.confirmaretxt.TabIndex = 7;
            // 
            // renunta
            // 
            this.renunta.Location = new System.Drawing.Point(31, 267);
            this.renunta.Name = "renunta";
            this.renunta.Size = new System.Drawing.Size(191, 46);
            this.renunta.TabIndex = 8;
            this.renunta.Text = "Renunta";
            this.renunta.UseVisualStyleBackColor = true;
            this.renunta.Click += new System.EventHandler(this.renunta_Click);
            // 
            // inregistrarebtn
            // 
            this.inregistrarebtn.Location = new System.Drawing.Point(359, 267);
            this.inregistrarebtn.Name = "inregistrarebtn";
            this.inregistrarebtn.Size = new System.Drawing.Size(191, 46);
            this.inregistrarebtn.TabIndex = 9;
            this.inregistrarebtn.Text = "Inregistrare";
            this.inregistrarebtn.UseVisualStyleBackColor = true;
            this.inregistrarebtn.Click += new System.EventHandler(this.inregistrarebtn_Click);
            // 
            // Inregistrare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Jocuri.Properties.Resources.Untitled;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(577, 350);
            this.Controls.Add(this.inregistrarebtn);
            this.Controls.Add(this.renunta);
            this.Controls.Add(this.confirmaretxt);
            this.Controls.Add(this.parolatxt);
            this.Controls.Add(this.numetxt);
            this.Controls.Add(this.emailtxt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "Inregistrare";
            this.Text = "Inregistrare";
            this.Load += new System.EventHandler(this.Inregistrare_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox emailtxt;
        private System.Windows.Forms.TextBox numetxt;
        private System.Windows.Forms.TextBox parolatxt;
        private System.Windows.Forms.TextBox confirmaretxt;
        private System.Windows.Forms.Button renunta;
        private System.Windows.Forms.Button inregistrarebtn;
    }
}
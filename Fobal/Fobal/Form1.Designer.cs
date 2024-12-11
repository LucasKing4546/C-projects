namespace Fobal
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Minge = new System.Windows.Forms.PictureBox();
            this.poartaStanga = new System.Windows.Forms.PictureBox();
            this.poartaDreapta = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Minge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poartaStanga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poartaDreapta)).BeginInit();
            this.SuspendLayout();
            // 
            // Minge
            // 
            this.Minge.BackColor = System.Drawing.Color.Transparent;
            this.Minge.BackgroundImage = global::Fobal.Properties.Resources._500px_Soccerball_svg;
            this.Minge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Minge.Location = new System.Drawing.Point(380, 206);
            this.Minge.Name = "Minge";
            this.Minge.Size = new System.Drawing.Size(40, 40);
            this.Minge.TabIndex = 1;
            this.Minge.TabStop = false;
            // 
            // poartaStanga
            // 
            this.poartaStanga.Location = new System.Drawing.Point(0, 190);
            this.poartaStanga.Name = "poartaStanga";
            this.poartaStanga.Size = new System.Drawing.Size(23, 70);
            this.poartaStanga.TabIndex = 2;
            this.poartaStanga.TabStop = false;
            this.poartaStanga.Visible = false;
            // 
            // poartaDreapta
            // 
            this.poartaDreapta.Location = new System.Drawing.Point(778, 190);
            this.poartaDreapta.Name = "poartaDreapta";
            this.poartaDreapta.Size = new System.Drawing.Size(23, 70);
            this.poartaDreapta.TabIndex = 3;
            this.poartaDreapta.TabStop = false;
            this.poartaDreapta.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(296, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scor 0:0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(412, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Timp: 90:00";
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.YellowGreen;
            this.button1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(322, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 70);
            this.button1.TabIndex = 6;
            this.button1.Text = "Start!";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Fobal.Properties.Resources._399298;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 448);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Minge);
            this.Controls.Add(this.poartaDreapta);
            this.Controls.Add(this.poartaStanga);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Minge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poartaStanga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poartaDreapta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox Minge;
        private System.Windows.Forms.PictureBox poartaStanga;
        private System.Windows.Forms.PictureBox poartaDreapta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button1;
    }
}


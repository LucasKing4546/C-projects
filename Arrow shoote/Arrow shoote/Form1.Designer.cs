namespace Arrow_shoote
{
    partial class Arrow_shooter
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
            this.green_arrow = new System.Windows.Forms.PictureBox();
            this.balloon1 = new System.Windows.Forms.PictureBox();
            this.balloon2 = new System.Windows.Forms.PictureBox();
            this.balloon3 = new System.Windows.Forms.PictureBox();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.boomTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.green_arrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon3)).BeginInit();
            this.SuspendLayout();
            // 
            // green_arrow
            // 
            this.green_arrow.Image = global::Arrow_shoote.Properties.Resources.idle;
            this.green_arrow.Location = new System.Drawing.Point(12, 12);
            this.green_arrow.Name = "green_arrow";
            this.green_arrow.Size = new System.Drawing.Size(105, 124);
            this.green_arrow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.green_arrow.TabIndex = 0;
            this.green_arrow.TabStop = false;
            // 
            // balloon1
            // 
            this.balloon1.Image = global::Arrow_shoote.Properties.Resources.balloon_1;
            this.balloon1.Location = new System.Drawing.Point(345, 276);
            this.balloon1.Name = "balloon1";
            this.balloon1.Size = new System.Drawing.Size(31, 45);
            this.balloon1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.balloon1.TabIndex = 1;
            this.balloon1.TabStop = false;
            this.balloon1.Tag = "balloon";
            // 
            // balloon2
            // 
            this.balloon2.Image = global::Arrow_shoote.Properties.Resources.balloon_2;
            this.balloon2.Location = new System.Drawing.Point(465, 349);
            this.balloon2.Name = "balloon2";
            this.balloon2.Size = new System.Drawing.Size(31, 45);
            this.balloon2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.balloon2.TabIndex = 2;
            this.balloon2.TabStop = false;
            this.balloon2.Tag = "balloon";
            // 
            // balloon3
            // 
            this.balloon3.Image = global::Arrow_shoote.Properties.Resources.balloon_3;
            this.balloon3.Location = new System.Drawing.Point(609, 320);
            this.balloon3.Name = "balloon3";
            this.balloon3.Size = new System.Drawing.Size(31, 45);
            this.balloon3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.balloon3.TabIndex = 3;
            this.balloon3.TabStop = false;
            this.balloon3.Tag = "balloon";
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 1;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(350, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Score: 0";
            // 
            // boomTimer
            // 
            this.boomTimer.Interval = 1000;
            this.boomTimer.Tick += new System.EventHandler(this.boomTimer_Tick);
            // 
            // Arrow_shooter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(784, 446);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.balloon3);
            this.Controls.Add(this.balloon2);
            this.Controls.Add(this.balloon1);
            this.Controls.Add(this.green_arrow);
            this.Name = "Arrow_shooter";
            this.Text = "Arrow_shooter";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Arrow_shooter_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Arrow_shooter_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Arrow_shooter_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.green_arrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.balloon3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox green_arrow;
        private System.Windows.Forms.PictureBox balloon1;
        private System.Windows.Forms.PictureBox balloon2;
        private System.Windows.Forms.PictureBox balloon3;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer boomTimer;
    }
}


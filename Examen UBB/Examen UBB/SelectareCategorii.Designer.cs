namespace Examen_UBB
{
    partial class SelectareCategorii
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
            this.categoriiSelectate = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // categoriiSelectate
            // 
            this.categoriiSelectate.BackColor = System.Drawing.Color.MidnightBlue;
            this.categoriiSelectate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.categoriiSelectate.ForeColor = System.Drawing.Color.White;
            this.categoriiSelectate.FormattingEnabled = true;
            this.categoriiSelectate.Items.AddRange(new object[] {
            "Expresii",
            "Aritmetica modulara",
            "Complexitate",
            "Recursivitate",
            "Divizibilate",
            "Prelucrarea cifrelor",
            "Vectori",
            "Matrici",
            "Siruri",
            "Backtracking"});
            this.categoriiSelectate.Location = new System.Drawing.Point(40, 36);
            this.categoriiSelectate.Name = "categoriiSelectate";
            this.categoriiSelectate.Size = new System.Drawing.Size(263, 300);
            this.categoriiSelectate.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(770, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(667, 58);
            this.label1.TabIndex = 1;
            this.label1.Text = "*selecteaza una sau mai multe categorii din care vrei sa \r\nprimesti probleme:\r\n";
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.GhostWhite;
            this.start.Location = new System.Drawing.Point(40, 352);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(263, 50);
            this.start.TabIndex = 2;
            this.start.Text = "Start examen";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Crimson;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(87, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "<= Înapoi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SelectareCategorii
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.ClientSize = new System.Drawing.Size(339, 490);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.categoriiSelectate);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.Name = "SelectareCategorii";
            this.Text = "SelectareCategorii";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox categoriiSelectate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button button1;
    }
}
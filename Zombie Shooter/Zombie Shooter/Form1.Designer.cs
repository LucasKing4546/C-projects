namespace Zombie_Shooter
{
    partial class Game
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
            this.txtAmmo = new System.Windows.Forms.Label();
            this.txtKills = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.heath = new System.Windows.Forms.ProgressBar();
            this.player = new System.Windows.Forms.PictureBox();
            this.zombie1 = new System.Windows.Forms.PictureBox();
            this.zombie2 = new System.Windows.Forms.PictureBox();
            this.zombie3 = new System.Windows.Forms.PictureBox();
            this.ammo = new System.Windows.Forms.PictureBox();
            this.Main = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAmmo
            // 
            this.txtAmmo.AutoSize = true;
            this.txtAmmo.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmmo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtAmmo.Location = new System.Drawing.Point(12, 9);
            this.txtAmmo.Name = "txtAmmo";
            this.txtAmmo.Size = new System.Drawing.Size(98, 29);
            this.txtAmmo.TabIndex = 0;
            this.txtAmmo.Text = "AMMO:  0";
            // 
            // txtKills
            // 
            this.txtKills.AutoSize = true;
            this.txtKills.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKills.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtKills.Location = new System.Drawing.Point(467, 9);
            this.txtKills.Name = "txtKills";
            this.txtKills.Size = new System.Drawing.Size(89, 29);
            this.txtKills.TabIndex = 1;
            this.txtKills.Text = "KILLS:  0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Impact", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(923, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "HEALTH";
            // 
            // heath
            // 
            this.heath.Location = new System.Drawing.Point(1008, 9);
            this.heath.Name = "heath";
            this.heath.Size = new System.Drawing.Size(244, 29);
            this.heath.TabIndex = 3;
            this.heath.Value = 100;
            // 
            // player
            // 
            this.player.Image = global::Zombie_Shooter.Properties.Resources.up;
            this.player.Location = new System.Drawing.Point(575, 316);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(71, 100);
            this.player.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.player.TabIndex = 4;
            this.player.TabStop = false;
            this.player.Tag = "player";
            // 
            // zombie1
            // 
            this.zombie1.Image = global::Zombie_Shooter.Properties.Resources.zup;
            this.zombie1.Location = new System.Drawing.Point(-24, 246);
            this.zombie1.Name = "zombie1";
            this.zombie1.Size = new System.Drawing.Size(70, 70);
            this.zombie1.TabIndex = 6;
            this.zombie1.TabStop = false;
            this.zombie1.Tag = "zombie";
            // 
            // zombie2
            // 
            this.zombie2.Image = global::Zombie_Shooter.Properties.Resources.zup;
            this.zombie2.Location = new System.Drawing.Point(875, 714);
            this.zombie2.Name = "zombie2";
            this.zombie2.Size = new System.Drawing.Size(70, 70);
            this.zombie2.TabIndex = 7;
            this.zombie2.TabStop = false;
            this.zombie2.Tag = "zombie";
            // 
            // zombie3
            // 
            this.zombie3.Image = global::Zombie_Shooter.Properties.Resources.zup;
            this.zombie3.Location = new System.Drawing.Point(1220, 175);
            this.zombie3.Name = "zombie3";
            this.zombie3.Size = new System.Drawing.Size(70, 70);
            this.zombie3.TabIndex = 8;
            this.zombie3.TabStop = false;
            this.zombie3.Tag = "zombie";
            // 
            // ammo
            // 
            this.ammo.Image = global::Zombie_Shooter.Properties.Resources.ammo_Image;
            this.ammo.Location = new System.Drawing.Point(188, 150);
            this.ammo.Name = "ammo";
            this.ammo.Size = new System.Drawing.Size(60, 60);
            this.ammo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ammo.TabIndex = 9;
            this.ammo.TabStop = false;
            this.ammo.Tag = "ammo";
            // 
            // Main
            // 
            this.Main.Interval = 10;
            this.Main.Tick += new System.EventHandler(this.Main_Tick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.heath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKills);
            this.Controls.Add(this.txtAmmo);
            this.Controls.Add(this.ammo);
            this.Controls.Add(this.zombie3);
            this.Controls.Add(this.zombie2);
            this.Controls.Add(this.zombie1);
            this.Controls.Add(this.player);
            this.DoubleBuffered = true;
            this.Name = "Game";
            this.Text = "Zombie Shooter";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombie3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ammo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtAmmo;
        private System.Windows.Forms.Label txtKills;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar heath;
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox zombie1;
        private System.Windows.Forms.PictureBox zombie2;
        private System.Windows.Forms.PictureBox zombie3;
        private System.Windows.Forms.PictureBox ammo;
        private System.Windows.Forms.Timer Main;
    }
}


namespace Battleship
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Lucky = new System.Windows.Forms.RadioButton();
            this.Netter = new System.Windows.Forms.RadioButton();
            this.Normal = new System.Windows.Forms.RadioButton();
            this.Fast = new System.Windows.Forms.RadioButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(39, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 70);
            this.button1.TabIndex = 6;
            this.button1.Text = " ";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(382, 408);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 70);
            this.button2.TabIndex = 7;
            this.button2.Text = " ";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // Lucky
            // 
            this.Lucky.Appearance = System.Windows.Forms.Appearance.Button;
            this.Lucky.BackColor = System.Drawing.Color.Transparent;
            this.Lucky.Checked = true;
            this.Lucky.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lucky.FlatAppearance.BorderSize = 0;
            this.Lucky.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Lucky.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Lucky.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Lucky.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Lucky.Location = new System.Drawing.Point(19, 50);
            this.Lucky.Name = "Lucky";
            this.Lucky.Size = new System.Drawing.Size(160, 64);
            this.Lucky.TabIndex = 8;
            this.Lucky.TabStop = true;
            this.Lucky.UseVisualStyleBackColor = false;
            this.Lucky.CheckedChanged += new System.EventHandler(this.Lucky_CheckedChanged);
            // 
            // Netter
            // 
            this.Netter.Appearance = System.Windows.Forms.Appearance.Button;
            this.Netter.BackColor = System.Drawing.Color.Transparent;
            this.Netter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Netter.FlatAppearance.BorderSize = 0;
            this.Netter.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Netter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Netter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Netter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Netter.Location = new System.Drawing.Point(300, 50);
            this.Netter.Name = "Netter";
            this.Netter.Size = new System.Drawing.Size(165, 64);
            this.Netter.TabIndex = 9;
            this.Netter.UseVisualStyleBackColor = false;
            this.Netter.CheckedChanged += new System.EventHandler(this.Netter_CheckedChanged);
            // 
            // Normal
            // 
            this.Normal.Appearance = System.Windows.Forms.Appearance.Button;
            this.Normal.BackColor = System.Drawing.Color.Transparent;
            this.Normal.Checked = true;
            this.Normal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Normal.FlatAppearance.BorderSize = 0;
            this.Normal.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Normal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Normal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Normal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Normal.Location = new System.Drawing.Point(24, 41);
            this.Normal.Name = "Normal";
            this.Normal.Size = new System.Drawing.Size(142, 64);
            this.Normal.TabIndex = 10;
            this.Normal.TabStop = true;
            this.Normal.UseVisualStyleBackColor = false;
            this.Normal.CheckedChanged += new System.EventHandler(this.Normal_CheckedChanged);
            // 
            // Fast
            // 
            this.Fast.Appearance = System.Windows.Forms.Appearance.Button;
            this.Fast.BackColor = System.Drawing.Color.Transparent;
            this.Fast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Fast.FlatAppearance.BorderSize = 0;
            this.Fast.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.Fast.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.Fast.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Fast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Fast.Location = new System.Drawing.Point(297, 39);
            this.Fast.Name = "Fast";
            this.Fast.Size = new System.Drawing.Size(142, 64);
            this.Fast.TabIndex = 11;
            this.Fast.UseVisualStyleBackColor = false;
            this.Fast.CheckedChanged += new System.EventHandler(this.Fast_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Battleship.Properties.Resources._21__Handle_1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Enabled = false;
            this.pictureBox2.Location = new System.Drawing.Point(185, 33);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(109, 95);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::Battleship.Properties.Resources._21__Handle_1;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Enabled = false;
            this.pictureBox3.Location = new System.Drawing.Point(182, 20);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(109, 104);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.Netter);
            this.panel1.Controls.Add(this.Lucky);
            this.panel1.Location = new System.Drawing.Point(12, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(479, 168);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.Fast);
            this.panel2.Controls.Add(this.Normal);
            this.panel2.Location = new System.Drawing.Point(15, 266);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(476, 136);
            this.panel2.TabIndex = 15;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::Battleship.Properties.Resources._20__Lamer_settings;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(503, 527);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.RadioButton Lucky;
        private System.Windows.Forms.RadioButton Netter;
        private System.Windows.Forms.RadioButton Normal;
        private System.Windows.Forms.RadioButton Fast;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
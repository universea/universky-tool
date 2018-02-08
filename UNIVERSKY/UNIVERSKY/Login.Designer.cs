namespace UNIVERSKY
{
    partial class Login
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LoginMin = new System.Windows.Forms.PictureBox();
            this.LoginClose = new System.Windows.Forms.PictureBox();
            this.UNIVERSKY_TITLE = new System.Windows.Forms.Label();
            this.UNIVERSKY_SYSTEM = new System.Windows.Forms.Label();
            this.Author = new System.Windows.Forms.Label();
            this.Login_Button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginClose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.Login_Button);
            this.panel1.Controls.Add(this.Author);
            this.panel1.Location = new System.Drawing.Point(0, 320);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 46);
            this.panel1.TabIndex = 0;
            // 
            // LoginMin
            // 
            this.LoginMin.BackColor = System.Drawing.Color.Transparent;
            this.LoginMin.Image = global::UNIVERSKY.Properties.Resources.最小化;
            this.LoginMin.Location = new System.Drawing.Point(709, 1);
            this.LoginMin.Name = "LoginMin";
            this.LoginMin.Size = new System.Drawing.Size(24, 24);
            this.LoginMin.TabIndex = 3;
            this.LoginMin.TabStop = false;
            this.LoginMin.Click += new System.EventHandler(this.LoginMin_Click);
            this.LoginMin.MouseLeave += new System.EventHandler(this.LoginMin_MouseLeave);
            this.LoginMin.MouseHover += new System.EventHandler(this.LoginMin_MouseHover);
            // 
            // LoginClose
            // 
            this.LoginClose.BackColor = System.Drawing.Color.Transparent;
            this.LoginClose.ErrorImage = null;
            this.LoginClose.Image = global::UNIVERSKY.Properties.Resources.关闭;
            this.LoginClose.Location = new System.Drawing.Point(739, 1);
            this.LoginClose.Name = "LoginClose";
            this.LoginClose.Size = new System.Drawing.Size(24, 24);
            this.LoginClose.TabIndex = 2;
            this.LoginClose.TabStop = false;
            this.LoginClose.Click += new System.EventHandler(this.LoginClose_Click);
            this.LoginClose.MouseLeave += new System.EventHandler(this.LoginClose_MouseLeave);
            this.LoginClose.MouseHover += new System.EventHandler(this.LoginClose_MouseHover);
            // 
            // UNIVERSKY_TITLE
            // 
            this.UNIVERSKY_TITLE.AutoSize = true;
            this.UNIVERSKY_TITLE.Font = new System.Drawing.Font("微软雅黑", 42F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UNIVERSKY_TITLE.ForeColor = System.Drawing.Color.Snow;
            this.UNIVERSKY_TITLE.Location = new System.Drawing.Point(204, 90);
            this.UNIVERSKY_TITLE.Name = "UNIVERSKY_TITLE";
            this.UNIVERSKY_TITLE.Size = new System.Drawing.Size(361, 75);
            this.UNIVERSKY_TITLE.TabIndex = 4;
            this.UNIVERSKY_TITLE.Text = "UNIVERSKY";
            // 
            // UNIVERSKY_SYSTEM
            // 
            this.UNIVERSKY_SYSTEM.AutoSize = true;
            this.UNIVERSKY_SYSTEM.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.UNIVERSKY_SYSTEM.ForeColor = System.Drawing.Color.Snow;
            this.UNIVERSKY_SYSTEM.Location = new System.Drawing.Point(206, 169);
            this.UNIVERSKY_SYSTEM.Name = "UNIVERSKY_SYSTEM";
            this.UNIVERSKY_SYSTEM.Size = new System.Drawing.Size(363, 62);
            this.UNIVERSKY_SYSTEM.TabIndex = 5;
            this.UNIVERSKY_SYSTEM.Text = "飞行器控制系统";
            // 
            // Author
            // 
            this.Author.AutoSize = true;
            this.Author.BackColor = System.Drawing.Color.Transparent;
            this.Author.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Author.ForeColor = System.Drawing.Color.SeaShell;
            this.Author.Location = new System.Drawing.Point(12, 17);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(193, 19);
            this.Author.TabIndex = 7;
            this.Author.Text = "DESIGNED BY UNIVERSEA";
            // 
            // Login_Button
            // 
            this.Login_Button.FlatAppearance.BorderSize = 0;
            this.Login_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Login_Button.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Login_Button.ForeColor = System.Drawing.Color.Snow;
            this.Login_Button.Location = new System.Drawing.Point(676, 8);
            this.Login_Button.Name = "Login_Button";
            this.Login_Button.Size = new System.Drawing.Size(84, 33);
            this.Login_Button.TabIndex = 9;
            this.Login_Button.Text = "进入系统";
            this.Login_Button.UseVisualStyleBackColor = true;
            this.Login_Button.Click += new System.EventHandler(this.Login_Button_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(762, 366);
            this.Controls.Add(this.UNIVERSKY_SYSTEM);
            this.Controls.Add(this.UNIVERSKY_TITLE);
            this.Controls.Add(this.LoginMin);
            this.Controls.Add(this.LoginClose);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Login_MouseMove);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoginMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Author;
        private System.Windows.Forms.PictureBox LoginMin;
        private System.Windows.Forms.PictureBox LoginClose;
        private System.Windows.Forms.Label UNIVERSKY_TITLE;
        private System.Windows.Forms.Label UNIVERSKY_SYSTEM;
        private System.Windows.Forms.Button Login_Button;
    }
}
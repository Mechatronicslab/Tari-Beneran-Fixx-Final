namespace design_dance
{
    partial class f_userAct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_userAct));
            this.p_head = new System.Windows.Forms.Panel();
            this.b_back = new System.Windows.Forms.Button();
            this.b_logout = new System.Windows.Forms.Button();
            this.b_exit = new System.Windows.Forms.Button();
            this.p_mainCam = new System.Windows.Forms.Panel();
            this.e_videoCamera = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new f_userCamera.UserControl1();
            this.b_play = new design_dance.RoundedButton();
            this.p_head.SuspendLayout();
            this.p_mainCam.SuspendLayout();
            this.SuspendLayout();
            // 
            // p_head
            // 
            this.p_head.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.p_head.Controls.Add(this.b_back);
            this.p_head.Controls.Add(this.b_logout);
            this.p_head.Controls.Add(this.b_exit);
            this.p_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.p_head.Location = new System.Drawing.Point(0, 0);
            this.p_head.Name = "p_head";
            this.p_head.Size = new System.Drawing.Size(1300, 33);
            this.p_head.TabIndex = 0;
            // 
            // b_back
            // 
            this.b_back.AutoSize = true;
            this.b_back.BackColor = System.Drawing.Color.Transparent;
            this.b_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_back.Dock = System.Windows.Forms.DockStyle.Left;
            this.b_back.FlatAppearance.BorderSize = 0;
            this.b_back.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b_back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_back.Image = ((System.Drawing.Image)(resources.GetObject("b_back.Image")));
            this.b_back.Location = new System.Drawing.Point(0, 0);
            this.b_back.Name = "b_back";
            this.b_back.Size = new System.Drawing.Size(44, 33);
            this.b_back.TabIndex = 31;
            this.b_back.UseVisualStyleBackColor = false;
            this.b_back.Click += new System.EventHandler(this.b_back_Click);
            // 
            // b_logout
            // 
            this.b_logout.BackColor = System.Drawing.Color.Transparent;
            this.b_logout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_logout.Dock = System.Windows.Forms.DockStyle.Right;
            this.b_logout.FlatAppearance.BorderSize = 0;
            this.b_logout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b_logout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b_logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_logout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.b_logout.Location = new System.Drawing.Point(1208, 0);
            this.b_logout.Name = "b_logout";
            this.b_logout.Size = new System.Drawing.Size(54, 33);
            this.b_logout.TabIndex = 30;
            this.b_logout.Text = "Logout";
            this.b_logout.UseVisualStyleBackColor = false;
            this.b_logout.Click += new System.EventHandler(this.b_logout_Click);
            // 
            // b_exit
            // 
            this.b_exit.BackColor = System.Drawing.Color.Transparent;
            this.b_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.b_exit.FlatAppearance.BorderSize = 0;
            this.b_exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.b_exit.Image = ((System.Drawing.Image)(resources.GetObject("b_exit.Image")));
            this.b_exit.Location = new System.Drawing.Point(1262, 0);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(38, 33);
            this.b_exit.TabIndex = 17;
            this.b_exit.UseVisualStyleBackColor = false;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // p_mainCam
            // 
            this.p_mainCam.Controls.Add(this.e_videoCamera);
            this.p_mainCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_mainCam.Location = new System.Drawing.Point(0, 33);
            this.p_mainCam.Name = "p_mainCam";
            this.p_mainCam.Size = new System.Drawing.Size(1300, 647);
            this.p_mainCam.TabIndex = 1;
            // 
            // e_videoCamera
            // 
            this.e_videoCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e_videoCamera.Location = new System.Drawing.Point(0, 0);
            this.e_videoCamera.Name = "e_videoCamera";
            this.e_videoCamera.Size = new System.Drawing.Size(1300, 647);
            this.e_videoCamera.TabIndex = 1;
            this.e_videoCamera.Text = "e_videoCamera";
            this.e_videoCamera.Child = this.userControl11;
            // 
            // b_play
            // 
            this.b_play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.b_play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_play.FlatAppearance.BorderSize = 0;
            this.b_play.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(1)))), ((int)(((byte)(50)))), ((int)(((byte)(67)))));
            this.b_play.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(1)))), ((int)(((byte)(50)))), ((int)(((byte)(67)))));
            this.b_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_play.Image = ((System.Drawing.Image)(resources.GetObject("b_play.Image")));
            this.b_play.Location = new System.Drawing.Point(428, 213);
            this.b_play.Name = "b_play";
            this.b_play.Size = new System.Drawing.Size(145, 140);
            this.b_play.TabIndex = 4;
            this.b_play.UseVisualStyleBackColor = false;
            // 
            // f_userAct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 680);
            this.Controls.Add(this.p_mainCam);
            this.Controls.Add(this.p_head);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f_userAct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "f_userAct";
            this.p_head.ResumeLayout(false);
            this.p_head.PerformLayout();
            this.p_mainCam.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel p_head;
        private System.Windows.Forms.Panel p_mainCam;
        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Button b_logout;
        private System.Windows.Forms.Button b_back;
        private RoundedButton b_play;
        private System.Windows.Forms.Integration.ElementHost e_videoCamera;
        private f_userCamera.UserControl1 userControl11;
    }
}
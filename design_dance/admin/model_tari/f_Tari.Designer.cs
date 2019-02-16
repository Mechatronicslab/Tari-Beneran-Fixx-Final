namespace design_dance.admin.model_tari
{
    partial class f_Tari
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_Tari));
            this.p_head = new System.Windows.Forms.Panel();
            this.b_back = new System.Windows.Forms.Button();
            this.b_logout = new System.Windows.Forms.Button();
            this.b_exit = new System.Windows.Forms.Button();
            this.p_body = new System.Windows.Forms.Panel();
            this.e_data = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new WpfControlLibrary1.UserControl1();
            this.p_head.SuspendLayout();
            this.p_body.SuspendLayout();
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
            this.p_head.Size = new System.Drawing.Size(1000, 41);
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
            this.b_back.Size = new System.Drawing.Size(44, 41);
            this.b_back.TabIndex = 32;
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
            this.b_logout.Location = new System.Drawing.Point(890, 0);
            this.b_logout.Name = "b_logout";
            this.b_logout.Size = new System.Drawing.Size(64, 41);
            this.b_logout.TabIndex = 31;
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
            this.b_exit.Location = new System.Drawing.Point(954, 0);
            this.b_exit.Margin = new System.Windows.Forms.Padding(4);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(46, 41);
            this.b_exit.TabIndex = 6;
            this.b_exit.UseVisualStyleBackColor = false;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // p_body
            // 
            this.p_body.AutoSize = true;
            this.p_body.Controls.Add(this.e_data);
            this.p_body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_body.Location = new System.Drawing.Point(0, 41);
            this.p_body.Name = "p_body";
            this.p_body.Size = new System.Drawing.Size(1000, 599);
            this.p_body.TabIndex = 1;
            // 
            // e_data
            // 
            this.e_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e_data.Location = new System.Drawing.Point(0, 0);
            this.e_data.Name = "e_data";
            this.e_data.Size = new System.Drawing.Size(1000, 599);
            this.e_data.TabIndex = 0;
            this.e_data.Text = "e_data";
            this.e_data.Child = this.userControl11;
            // 
            // f_Tari
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.p_body);
            this.Controls.Add(this.p_head);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "f_Tari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.p_head.ResumeLayout(false);
            this.p_head.PerformLayout();
            this.p_body.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel p_head;
        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Panel p_body;
        private System.Windows.Forms.Integration.ElementHost e_data;
        private WpfControlLibrary1.UserControl1 userControl11;
        private System.Windows.Forms.Button b_logout;
        private System.Windows.Forms.Button b_back;
    }
}
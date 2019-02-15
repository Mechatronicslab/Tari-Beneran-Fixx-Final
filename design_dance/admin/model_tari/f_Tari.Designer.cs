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
            this.panel1 = new System.Windows.Forms.Panel();
            this.b_exit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.userControl11 = new WpfControlLibrary1.UserControl1();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.b_exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1343, 41);
            this.panel1.TabIndex = 0;
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
            this.b_exit.Location = new System.Drawing.Point(1297, 0);
            this.b_exit.Margin = new System.Windows.Forms.Padding(4);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(46, 41);
            this.b_exit.TabIndex = 6;
            this.b_exit.UseVisualStyleBackColor = false;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.elementHost2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1343, 740);
            this.panel2.TabIndex = 1;
            // 
            // elementHost2
            // 
            this.elementHost2.AutoSize = true;
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(0, 0);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(1343, 740);
            this.elementHost2.TabIndex = 3;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.userControl11;
            // 
            // f_Tari
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1343, 781);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "f_Tari";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private WpfControlLibrary1.UserControl1 userControl11;
    }
}
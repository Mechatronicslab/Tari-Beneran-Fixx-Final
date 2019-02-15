namespace design_dance
{
    partial class f_signin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_signin));
            this.panelRight = new System.Windows.Forms.Panel();
            this.b_signin = new FlatButton.JFlatButton();
            this.mPassword = new JMaterialTextbox.JMaterialTextbox();
            this.t_password = new System.Windows.Forms.Label();
            this.mUsername = new JMaterialTextbox.JMaterialTextbox();
            this.b_exit = new System.Windows.Forms.Button();
            this.t_copyright = new System.Windows.Forms.Label();
            this.t_username = new System.Windows.Forms.Label();
            this.t_descSignin = new System.Windows.Forms.Label();
            this.t_signin = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.i_siger = new System.Windows.Forms.PictureBox();
            this.t_sigehPengunten = new System.Windows.Forms.Label();
            this.panelRight.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.i_siger)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.b_signin);
            this.panelRight.Controls.Add(this.mPassword);
            this.panelRight.Controls.Add(this.t_password);
            this.panelRight.Controls.Add(this.mUsername);
            this.panelRight.Controls.Add(this.b_exit);
            this.panelRight.Controls.Add(this.t_copyright);
            this.panelRight.Controls.Add(this.t_username);
            this.panelRight.Controls.Add(this.t_descSignin);
            this.panelRight.Controls.Add(this.t_signin);
            this.panelRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(355, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(318, 514);
            this.panelRight.TabIndex = 1;
            // 
            // b_signin
            // 
            this.b_signin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.b_signin.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.b_signin.ButtonText = "     Sign In";
            this.b_signin.CausesValidation = false;
            this.b_signin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_signin.ErrorImageLeft = ((System.Drawing.Image)(resources.GetObject("b_signin.ErrorImageLeft")));
            this.b_signin.ErrorImageRight = ((System.Drawing.Image)(resources.GetObject("b_signin.ErrorImageRight")));
            this.b_signin.FocusBackground = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(58)))), ((int)(((byte)(147)))));
            this.b_signin.FocusFontColor = System.Drawing.Color.White;
            this.b_signin.ForeColors = System.Drawing.Color.White;
            this.b_signin.HoverBackground = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(58)))), ((int)(((byte)(147)))));
            this.b_signin.HoverFontColor = System.Drawing.Color.White;
            this.b_signin.ImageLeft = ((System.Drawing.Image)(resources.GetObject("b_signin.ImageLeft")));
            this.b_signin.ImageRight = null;
            this.b_signin.LeftPictureColor = System.Drawing.Color.Transparent;
            this.b_signin.Location = new System.Drawing.Point(11, 324);
            this.b_signin.Margin = new System.Windows.Forms.Padding(4);
            this.b_signin.Name = "b_signin";
            this.b_signin.PaddingLeftPicture = new System.Windows.Forms.Padding(0);
            this.b_signin.PaddingRightPicture = new System.Windows.Forms.Padding(0);
            this.b_signin.RightPictureColor = System.Drawing.Color.Transparent;
            this.b_signin.Size = new System.Drawing.Size(291, 30);
            this.b_signin.SizeModeLeft = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.b_signin.SizeModeRight = System.Windows.Forms.PictureBoxSizeMode.Normal;
            this.b_signin.TabIndex = 9;
            this.b_signin.Click += new System.EventHandler(this.b_signin_Click);
            // 
            // mPassword
            // 
            this.mPassword.BackColor = System.Drawing.Color.Transparent;
            this.mPassword.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPassword.Font_Size = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mPassword.ForeColors = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mPassword.HintText = "Password";
            this.mPassword.IsPassword = true;
            this.mPassword.LineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mPassword.LineThickness = 1;
            this.mPassword.Location = new System.Drawing.Point(11, 254);
            this.mPassword.Margin = new System.Windows.Forms.Padding(0);
            this.mPassword.MaxLength = 32767;
            this.mPassword.Name = "mPassword";
            this.mPassword.OnFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mPassword.OnFocusedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mPassword.ReadOnly = false;
            this.mPassword.Size = new System.Drawing.Size(291, 28);
            this.mPassword.TabIndex = 8;
            this.mPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.mPassword.TextName = "";
            // 
            // t_password
            // 
            this.t_password.AutoSize = true;
            this.t_password.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_password.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.t_password.Location = new System.Drawing.Point(9, 224);
            this.t_password.Name = "t_password";
            this.t_password.Size = new System.Drawing.Size(67, 16);
            this.t_password.TabIndex = 7;
            this.t_password.Text = "Password";
            // 
            // mUsername
            // 
            this.mUsername.BackColor = System.Drawing.Color.Transparent;
            this.mUsername.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mUsername.Font_Size = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mUsername.ForeColors = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mUsername.HintText = "Username";
            this.mUsername.IsPassword = false;
            this.mUsername.LineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mUsername.LineThickness = 1;
            this.mUsername.Location = new System.Drawing.Point(11, 171);
            this.mUsername.Margin = new System.Windows.Forms.Padding(0);
            this.mUsername.MaxLength = 32767;
            this.mUsername.Name = "mUsername";
            this.mUsername.OnFocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mUsername.OnFocusedTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.mUsername.ReadOnly = false;
            this.mUsername.Size = new System.Drawing.Size(291, 28);
            this.mUsername.TabIndex = 6;
            this.mUsername.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.mUsername.TextName = "";
            // 
            // b_exit
            // 
            this.b_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.b_exit.BackColor = System.Drawing.Color.Transparent;
            this.b_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_exit.FlatAppearance.BorderSize = 0;
            this.b_exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.b_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.b_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.b_exit.Image = ((System.Drawing.Image)(resources.GetObject("b_exit.Image")));
            this.b_exit.Location = new System.Drawing.Point(277, 3);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(38, 21);
            this.b_exit.TabIndex = 5;
            this.b_exit.UseVisualStyleBackColor = false;
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // t_copyright
            // 
            this.t_copyright.AutoSize = true;
            this.t_copyright.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_copyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.t_copyright.Location = new System.Drawing.Point(214, 490);
            this.t_copyright.Name = "t_copyright";
            this.t_copyright.Size = new System.Drawing.Size(92, 15);
            this.t_copyright.TabIndex = 4;
            this.t_copyright.Text = "Copyright 2019";
            // 
            // t_username
            // 
            this.t_username.AutoSize = true;
            this.t_username.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_username.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.t_username.Location = new System.Drawing.Point(11, 146);
            this.t_username.Name = "t_username";
            this.t_username.Size = new System.Drawing.Size(70, 16);
            this.t_username.TabIndex = 2;
            this.t_username.Text = "Username";
            // 
            // t_descSignin
            // 
            this.t_descSignin.AutoSize = true;
            this.t_descSignin.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_descSignin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.t_descSignin.Location = new System.Drawing.Point(9, 52);
            this.t_descSignin.Name = "t_descSignin";
            this.t_descSignin.Size = new System.Drawing.Size(182, 15);
            this.t_descSignin.TabIndex = 1;
            this.t_descSignin.Text = "Silahkan masukan data diri anda!";
            // 
            // t_signin
            // 
            this.t_signin.AutoSize = true;
            this.t_signin.Font = new System.Drawing.Font("Lucida Sans Unicode", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_signin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(83)))), ((int)(((byte)(155)))));
            this.t_signin.Location = new System.Drawing.Point(6, 23);
            this.t_signin.Name = "t_signin";
            this.t_signin.Size = new System.Drawing.Size(75, 25);
            this.t_signin.TabIndex = 0;
            this.t_signin.Text = "Log In";
            // 
            // panelLeft
            // 
            this.panelLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelLeft.BackgroundImage")));
            this.panelLeft.Controls.Add(this.i_siger);
            this.panelLeft.Controls.Add(this.t_sigehPengunten);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(355, 514);
            this.panelLeft.TabIndex = 0;
            // 
            // i_siger
            // 
            this.i_siger.BackColor = System.Drawing.Color.Transparent;
            this.i_siger.Image = ((System.Drawing.Image)(resources.GetObject("i_siger.Image")));
            this.i_siger.Location = new System.Drawing.Point(81, 171);
            this.i_siger.Name = "i_siger";
            this.i_siger.Size = new System.Drawing.Size(219, 124);
            this.i_siger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.i_siger.TabIndex = 1;
            this.i_siger.TabStop = false;
            // 
            // t_sigehPengunten
            // 
            this.t_sigehPengunten.AutoSize = true;
            this.t_sigehPengunten.BackColor = System.Drawing.Color.Transparent;
            this.t_sigehPengunten.Font = new System.Drawing.Font("Forte", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.t_sigehPengunten.ForeColor = System.Drawing.Color.White;
            this.t_sigehPengunten.Location = new System.Drawing.Point(50, 298);
            this.t_sigehPengunten.Name = "t_sigehPengunten";
            this.t_sigehPengunten.Size = new System.Drawing.Size(275, 41);
            this.t_sigehPengunten.TabIndex = 0;
            this.t_sigehPengunten.Text = "Sigeh Pengunten";
            // 
            // f_signin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 514);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f_signin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "f_signin";
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.i_siger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label t_sigehPengunten;
        private System.Windows.Forms.Label t_descSignin;
        private System.Windows.Forms.Label t_signin;
        private FlatButton.JFlatButton b_signin;
        private JMaterialTextbox.JMaterialTextbox mPassword;
        private System.Windows.Forms.Label t_password;
        private JMaterialTextbox.JMaterialTextbox mUsername;
        private System.Windows.Forms.Button b_exit;
        private System.Windows.Forms.Label t_copyright;
        private System.Windows.Forms.Label t_username;
        private System.Windows.Forms.PictureBox i_siger;
    }
}


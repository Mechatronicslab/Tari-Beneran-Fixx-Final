using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace design_dance
{
    public partial class f_signup : Form
    {
        public f_signup()
        {
            InitializeComponent();
        }       

        private void b_exit_Click_1(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }

        private void b_back_Click(object sender, EventArgs e)
        {            
            f_mainAdmin frm = new f_mainAdmin();
            frm.Show();
            this.Visible = false;
        }

        private void b_logout_Click(object sender, EventArgs e)
        {
            f_signin sin = new f_signin();
            sin.Show();
            this.Visible = false;
        }
    }
}

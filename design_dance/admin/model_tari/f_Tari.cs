using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace design_dance.admin.model_tari
{
    public partial class f_Tari : Form
    {
        public f_Tari()
        {
            InitializeComponent();           
        }        

        private void b_exit_Click(object sender, EventArgs e)
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

        private void b_logout_Click(object sender, EventArgs e)
        {
            f_signin sin = new f_signin();
            sin.Show();
            this.Visible = false;
            pi_videoM.Ctlcontrols.stop();
        }

        private void b_back_Click(object sender, EventArgs e)
        {
            f_mainAdmin admin = new f_mainAdmin();
            admin.Show();
            this.Visible = false;            
        }
    }
}

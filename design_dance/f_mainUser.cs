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
    public partial class f_mainUser : Form
    {

        public string url_a = "C:/Users/gakga/Desktop/videos/01.mp4";
        public string url_b = "C:/Users/gakga/Desktop/videos/02.mp4";
        public string url_c = "C:/Users/gakga/Desktop/videos/03.mp4";
        public string url_d = "C:/Users/gakga/Desktop/videos/04.mp4";

        public f_mainUser()
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
        }

        private void b_tariA_Click(object sender, EventArgs e)
        {            
            f_userAct A = new f_userAct(this,url_a);
            A.Show();
            this.Visible = false;
        }

        private void b_tariB_Click(object sender, EventArgs e)
        {
            f_userAct B = new f_userAct(this, url_b);
            B.Show();
            this.Visible = false;
        }

        private void b_tariC_Click(object sender, EventArgs e)
        {
            f_userAct C = new f_userAct(this, url_c);
            C.Show();
            this.Visible = false;
        }

        private void b_tariD_Click(object sender, EventArgs e)
        {
            f_userAct D = new f_userAct(this, url_d);
            D.Show();
            this.Visible = false;
        }
    }
}

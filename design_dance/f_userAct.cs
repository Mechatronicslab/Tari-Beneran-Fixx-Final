using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using f_userCam = f_userCamera;

namespace design_dance
{
    public partial class f_userAct : Form
    {        

        private f_mainUser parent;        
        private string url;        

        public f_userAct(f_mainUser parent, string url)
        {
            InitializeComponent();
            this.parent = parent;            
            this.url = url;         
        }        

        private void b_back_Click(object sender, EventArgs e)
        {
            f_mainUser user = new f_mainUser();
            user.Show();
            this.Visible = false;
            f_userCam.UserControl1 back = e_videoCamera.Child as f_userCam.UserControl1;
            back.stopVideo();
        }

        private void b_logout_Click(object sender, EventArgs e)
        {
            f_signin sin = new f_signin();
            sin.Show();
            this.Visible = false;
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
    }
}

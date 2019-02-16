using design_dance.admin.model_tari;
using design_dance.admin.rekam_tari;
using System;
using System.Threading;
using System.Windows.Forms;
namespace design_dance
{
    public partial class f_mainAdmin : Form
    {
        private f_signin f_signin;

        public f_mainAdmin()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
        }

        public f_mainAdmin(f_signin f_signin)
        {
            this.f_signin = f_signin;
        }

        public void StartForm()
        {
            Application.Run(new LoadingForm());
        }

        private void b_regist_Click(object sender, EventArgs e)
        {
            f_signup frm = new f_signup();
            frm.Show();
            this.Visible = false;
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

        private void b_logout_Click(object sender, EventArgs e)
        {
            f_signin sin = new f_signin();
            sin.Show();
            this.Visible = false;
        }

        private void b_rekam_Click(object sender, EventArgs e)
        {
            f_rekam_tari sin = new f_rekam_tari();
            sin.Show();
            this.Visible = false;
        }

        private void b_model_Click(object sender, EventArgs e)
        {
            f_Tari sin = new f_Tari();
            sin.Show();
            this.Visible = false;

        }
    }
}

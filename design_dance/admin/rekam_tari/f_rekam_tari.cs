using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace design_dance.admin.rekam_tari
{
    public partial class f_rekam_tari : Form
    {
        public f_rekam_tari()
        {
            InitializeComponent();

            System.IO.DirectoryInfo directory = new DirectoryInfo("./img/");
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }
    }
}

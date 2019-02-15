using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace design_dance
{
    public partial class f_signin : Form
    {
        static string developmentUri = "mongodb://localhost:27017";
        static string ProductionUri = "mongodb://maria:maria123@167.205.7.226:27017/kinect";
        public IMongoDatabase db;
        public IMongoClient client;
        public f_signin()
        {
            InitializeComponent();
           
            Connect();
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

        private void b_signin_Click(object sender, EventArgs e)
        {
            string username = mUsername.TextName;
            string password = mPassword.TextName;
            signIn(username, password);
            /*if(username == "admin" & password == "admin")
            {
                f_mainAdmin admin = new f_mainAdmin();
                admin.Show();
                this.Visible = false;
            }else if(username == "user" & password == "user"){
                f_mainUser user = new f_mainUser();
                user.Show();
                this.Visible = false;
            }*/
        }

        public void Connect()
        {
            client = new MongoClient(ProductionUri);
            db = client.GetDatabase("kinect");
            bool isMongoLive = db.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            if (isMongoLive)
            {
                // connected
                Console.WriteLine("Koneksi Berhasil");
            }
            else
            {
                // couldn't connect
                MessageBox.Show("Koneksi Bermasalah atau Server Mengalami Gangguan !! ");
                Environment.Exit(0);
            }
        }

        public void signIn(string username, string password)
        {
            LoadingForm loading = new LoadingForm();
            loading.Show();
            Application.DoEvents();
            IMongoCollection<User> collection = db.GetCollection<User>("users");
            var builder = Builders<User>.Filter;
            var filt = builder.Eq("username", username) & builder.Eq("password", password);
            var result = collection.Find(filt).FirstOrDefaultAsync().Result;
            
            if (result != null)
            {
                loading.Close();
                if (result.role.Equals("admin")) { 
                    f_mainAdmin frm = new f_mainAdmin();
                    frm.Show();
                    this.Visible = false;
                }
                else {
                    f_mainUser frm = new f_mainUser();
                    frm.Show();
                    this.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Username atau Password Salah !!");
            }
        }     
    }
}

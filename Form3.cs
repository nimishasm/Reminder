using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MyReminder
{
    public partial class frmView : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReminderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        string reminderID;
        public frmView(string remID)
        {
            InitializeComponent();
            this.reminderID = remID;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                string cmdText = "select * from tblReminder where id="+this.reminderID;
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                da.SelectCommand = sqlCmd;
                DataSet ds = new DataSet();
                sqlCon.Open();
                da.Fill(ds);

                lblTitle.Text = ds.Tables[0].Rows[0][1].ToString();
                lblDesc.Text = ds.Tables[0].Rows[0][6].ToString();
                lblH.Text = ds.Tables[0].Rows[0][3].ToString();
                lblM.Text = ds.Tables[0].Rows[0][4].ToString();
                lblMeridian.Text = ds.Tables[0].Rows[0][5].ToString();
                lblDate.Text = ds.Tables[0].Rows[0][2].ToString();

                sqlCon.Close();
                
               
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

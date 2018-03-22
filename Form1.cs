using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Media;

namespace MyReminder
{
    public partial class Form1 : Form
    {
        string[] dateArr = null;
        string[] timeArr = null;

        DataSet reminderSet = new DataSet();
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReminderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                string[] selectedID = listReminder.SelectedItem.ToString().Split(':');
                Form2 viewReminder = new Form2(selectedID[0]);
                viewReminder.Show();
                this.Hide();
            }
            catch (Exception ew)
            {
                Console.Write(ew.ToString());
                MessageBox.Show("Please select any reminder to modify!");
            }
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try { 
                string[] selectedID = listReminder.SelectedItem.ToString().Split(':');
                frmView viewReminder = new frmView(selectedID[0]);
                viewReminder.Show();
            }
            catch(Exception ew)
            {
                Console.Write(ew.ToString());
                MessageBox.Show("Please select any reminder to view!","Alert",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 addReminder = new Form4();
            addReminder.Show();
            this.Hide();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                dateArr = null;
                timeArr = null;

                string cmdText = "select id,title,date,timeH,timeM,meridian from tblReminder";
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                da.SelectCommand = sqlCmd;
                DataSet ds = new DataSet();
                sqlCon.Open();
                da.Fill(ds);
                reminderSet = ds;

                dateArr= new string[ds.Tables[0].Rows.Count];
                timeArr = new string[ds.Tables[0].Rows.Count];


                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    listReminder.Items.Add(" " + ds.Tables[0].Rows[i][0].ToString() + ": \t" + ds.Tables[0].Rows[i][1].ToString());

                }
                sqlCon.Close();
            }
            
            for (i = 0; i < reminderSet.Tables[0].Rows.Count; i++)
            {
                dateArr[i] = reminderSet.Tables[0].Rows[i][2].ToString();
                string Time = "" + reminderSet.Tables[0].Rows[i][3].ToString() + ":" + reminderSet.Tables[0].Rows[i][4].ToString() + " " + reminderSet.Tables[0].Rows[i][5].ToString();
                DateTime tym = DateTime.Parse(Time, System.Globalization.CultureInfo.CurrentCulture);
                timeArr[i] = tym.ToString("HH:mm tt");
            }


        }

        public void onePing()
        {
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i;
            DateTime dTime = DateTime.Now;
            for (i = 0; i < reminderSet.Tables[0].Rows.Count; i++)
            {
                DateTime dt = new DateTime();
                    dt=DateTime.Now.Date;
                if (dt.CompareTo(Convert.ToDateTime(dateArr[i]))==0)
                {
                    if (DateTime.Now.ToString("HH:mm tt") == (timeArr[i].ToString())){
                        onePing();
                        
                        ////////////
                        

                        try
                        {
                            frmView viewReminder = new frmView(reminderSet.Tables[0].Rows[i][0].ToString());
                            viewReminder.Show();
                        }
                        catch (Exception ew)
                        {
                            Console.Write(ew.ToString());
                        }
                        MessageBox.Show("New Reminder!","Notification",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        //////////
                    }
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to exit?", "Confirm",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Are you sure to exit?", "Confirm",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question);

            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
    
}

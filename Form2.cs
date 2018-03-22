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
    public partial class Form2 : Form
    {
        string remID;
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReminderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public Form2(string id)
        {
            this.remID = id;
            InitializeComponent(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text.ToString() != "" && datePicker.Text.ToString() != "" && comboHour.Text.ToString() != "" && comboMinute.Text.ToString() != "" && comboTimezone.Text.ToString() != "" && txtDescribe.Text.ToString() != "")
                {
                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string cmdText = @"update tblReminder set Title='" + txtTitle.Text.ToString() + "' , date='" + datePicker.Text.ToString() + "', Timeh='" + comboHour.Text.ToString() + "', Timem='" + comboMinute.Text.ToString() + "', meridian='" + comboTimezone.Text.ToString() + "', describe='" + txtDescribe.Text.ToString() + "' where id="+remID;
                        SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                        sqlCmd.ExecuteNonQuery();

                        MessageBox.Show("Reminder Updated...");
                        sqlCon.Close();
                 
                        this.Close();

                    }
                }
                else
                {
                    MessageBox.Show("Fill all the fields...");
                }
            }
            catch (Exception en) {
                Console.Write(en.ToString());
                MessageBox.Show("Remove Invalid Character","Invalid Character",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                string cmdText = "select * from tblReminder where id=" + this.remID;
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                da.SelectCommand = sqlCmd;
                DataSet ds = new DataSet();
                sqlCon.Open();
                da.Fill(ds);

                txtTitle.Text = ds.Tables[0].Rows[0][1].ToString();
                datePicker.Text = ds.Tables[0].Rows[0][2].ToString();
                comboHour.Text = ds.Tables[0].Rows[0][3].ToString();
                comboMinute.Text = ds.Tables[0].Rows[0][4].ToString();
                comboTimezone.Text = ds.Tables[0].Rows[0][5].ToString();
                txtDescribe.Text = ds.Tables[0].Rows[0][6].ToString();

                sqlCon.Close();
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Leave(object sender, EventArgs e)
        {
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string cmdText = "delete from tblReminder where id=" + remID;
                SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                sqlCmd.ExecuteNonQuery();

                MessageBox.Show("Reminder deleted...");
                sqlCon.Close();

                this.Close();

            }
        }
    }
    }


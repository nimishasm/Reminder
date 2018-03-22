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
    public partial class Form4 : Form
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReminderDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public Form4()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int getId()
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    string cmdText = "select MAX(id) from tblReminder";
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                    da.SelectCommand = sqlCmd;
                    DataSet ds = new DataSet();
                    sqlCon.Open();
                    da.Fill(ds);
                    DataSet ds1 = ds;
                    sqlCon.Close();
                    int i = Int16.Parse(ds.Tables[0].Rows[0][0].ToString());
                    return i + 1;
                }
            }
            catch (Exception)
            {
                return 100;
            }
 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text.ToString() != "" && datePicker.Text.ToString() != "" && comboHour.Text.ToString() != "" && comboMinute.Text.ToString() != "" && comboTimezone.Text.ToString() != "" && txtDescribe.Text.ToString() != "")
                {



                    using (SqlConnection sqlCon = new SqlConnection(connectionString))
                    {
                        sqlCon.Open();
                        string cmdText = "insert into tblReminder values(" + getId() + ",'" + txtTitle.Text.ToString() + "','" + datePicker.Text.ToString() + "','" + comboHour.Text.ToString() + "','" + comboMinute.Text.ToString() + "','" + comboTimezone.Text.ToString() + "','" + txtDescribe.Text.ToString() + "')";
                        SqlCommand sqlCmd = new SqlCommand(cmdText, sqlCon);
                        sqlCmd.ExecuteNonQuery();

                        MessageBox.Show("Reminder Added...");
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
                MessageBox.Show("Remove Invalid Character", "Invalid Character", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void datePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void lblTimezone_Click(object sender, EventArgs e)
        {

        }

        private void lblMinute_Click(object sender, EventArgs e)
        {

        }

        private void lblHour_Click(object sender, EventArgs e)
        {

        }

        private void comboMinute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblDescribe_Click(object sender, EventArgs e)
        {

        }

        private void lblTime_Click(object sender, EventArgs e)
        {

        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

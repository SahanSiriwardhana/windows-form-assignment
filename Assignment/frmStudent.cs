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

namespace Assignment
{
    public partial class frmStudent : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-U4TJ8RU;Initial Catalog=studentDB;Integrated Security=True");
        public frmStudent()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            Load_data_for_data_grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string firstName = txtFistName.Text;
            string email = txtEmail.Text;
            int tel = int.Parse(txtTp.Text);
            string gender;

            if (radFemail.Checked)
            {
                gender = "Male";
            }
            else {
                gender = "Female";
            }

            string grade = cmbGrade.Text;
            
            try
            {
                string quary = "INSERT INTO Student VALUES('"+firstName+"','"+email+"','"+tel+"','"+gender+"','"+grade+"')";
                SqlCommand cmd = new SqlCommand(quary,conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved Successfuly !");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving : "+ex);
            }
            finally {
                conn.Close();
            }
        }

        public void Load_data_for_data_grid()
        {
            try
            {
                string quary = "SELECT * FROM Student";
                SqlCommand cmd = new SqlCommand(quary, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while read data : " + ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}

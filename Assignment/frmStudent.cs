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

        public int STD_ID = 0;
        string connectionString = @"Data Source=DESKTOP-U4TJ8RU;Initial Catalog=studentDB;Integrated Security=True";
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-U4TJ8RU;Initial Catalog=studentDB;Integrated Security=True");
        DataTable dt = new DataTable();

        public frmStudent()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTp.Text.Length<=10) {
                string firstName = txtFistName.Text;
                string email = txtEmail.Text;

                int tel = int.Parse(txtTp.Text);
                string gender = "Male";

                if (radFemail.Checked)
                {
                    gender = "Female";
                }
                if (radMale.Checked)
                {
                    gender = "Male";
                }

                string grade = cmbGrade.Text;

                try
                {
                    string quary = "INSERT INTO Student(firstName,email,tp,gender,grade) VALUES('" + firstName + "','" + email + "','" + tel + "','" + gender + "','" + grade + "')";
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved Successfuly !");

                    Clear_form();
                    Load_data_for_data_grid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while saving : " + ex);
                }
                finally {
                    conn.Close();
                }
            }
            else {
                MessageBox.Show("Please enter valide telephone number");
            }
        }

        public void Clear_form()
        {
            // Clear the form
            txtFistName.Clear();
            txtEmail.Clear();
            txtTp.Clear();
            cmbGrade.ResetText();
            
        }

        public void Load_data_for_data_grid()
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT StudentID as ID, firstName as 'First Name',email as Email ,tp as 'Telephone No', gender as Gender, grade as Grade FROM Student",sqlCon);
                dt.Rows.Clear();
                sqlDa.Fill(dt);
                dataGridView1.DataSource = dt;

            }
        }

        private void frmStudent_Load(object sender, EventArgs e)
        {
            Load_data_for_data_grid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
          string email =  txtSearch.Text;
            Clear_form();
            try
            {
                conn.Open();
                string quary = "SELECT * FROM Student where email=@email";
                SqlCommand cmd = new SqlCommand(quary, conn);
                cmd.Parameters.AddWithValue("@email",email);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        //set data to textbox and other controllers
                        txtFistName.Text = reader["firstName"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtTp.Text = reader["tp"].ToString();

                        if (reader["gender"].ToString()=="Male") {
                            radMale.Checked = true;
                            radFemail.Checked = false;
                        }
                        else {
                            radMale.Checked = false;
                            radFemail.Checked = true;
                        }
                        cmbGrade.Text = reader["grade"].ToString();
                        //cmbGrade.SelectedIndex = cmbGrade.FindString(reader["grade"].ToString());
                        STD_ID =int.Parse(reader["StudentID"].ToString());
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving : " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (STD_ID > 0)
            {
                if (txtTp.Text.Length < 10)
                {
                    string firstName = txtFistName.Text;
                    string email = txtEmail.Text;
                    Int32 tel = Convert.ToInt32(txtTp.Text);
                    string gender = "Male";

                    if (radFemail.Checked)
                    {
                        gender = "Female";
                    }
                    if (radMale.Checked)
                    {
                        gender = "Male";
                    }

                    string grade = cmbGrade.Text;

                    try
                    {
                        string quary = "UPDATE Student SET firstName='" + firstName + "' ,email='" + email + "' ,tp='" + tel + "' ,gender='" + gender + "',grade= '" + grade + "' WHERE StudentID='" + STD_ID + "'";
                        SqlCommand cmd = new SqlCommand(quary, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Update Successfuly !");

                        Clear_form();
                        txtSearch.Clear();
                        STD_ID = 0;
                        Load_data_for_data_grid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error while updating : " + ex);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else {
                    MessageBox.Show("Please enter valide telephone number");
                }
            }
            else
            {
                MessageBox.Show("Please search a student");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (STD_ID > 0)
            {
                string firstName = txtFistName.Text;
                string email = txtEmail.Text;
                int tel = int.Parse(txtTp.Text);
                string gender = "Male";

                if (radFemail.Checked)
                {
                    gender = "Female";
                }
                if (radMale.Checked)
                {
                    gender = "Male";
                }

                string grade = cmbGrade.Text;

                try
                {
                    string quary = "DELETE FROM Student  WHERE StudentID='" + STD_ID + "'";
                    SqlCommand cmd = new SqlCommand(quary, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted Successfuly !");

                    Clear_form();
                    txtSearch.Clear();
                    STD_ID = 0;
                    Load_data_for_data_grid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting : " + ex);
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Please search a student");
            }

        }

        private void txtTp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
       (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}

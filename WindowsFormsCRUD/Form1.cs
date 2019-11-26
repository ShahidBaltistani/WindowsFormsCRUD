using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCRUD
{
    public partial class studentRecordForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-3NS0BQ0;Initial Catalog=WinForm;Integrated Security=True");
        public int Id;
        public studentRecordForm()
        {
            InitializeComponent();
        }
        private void name_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void studentRecordForm_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
        }

        private void GetStudentRecord()
        {
            SqlCommand cmd = new SqlCommand("select * from Students",conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void insert_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Students VALUES(@Name, @Father, @Phone, @Address)", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Father", textBox2.Text);
                cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
               cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("New Student is Successfully inserted", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                NewMethod();

            }
        }

        private bool IsValid()
        {
            if (name.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void reset_Click(object sender, EventArgs e)
        {
            NewMethod();
        }

        private void NewMethod()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = Convert.ToInt32 (dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();

        }

        private void update_Click(object sender, EventArgs e)
        {
            if (Id>0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE  Students SET Name = @Name, FatherName = @Father, PhoneNumber = @Phone, Address = @Address WHERE Id= @ID ", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Father", textBox2.Text);
                cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
                cmd.Parameters.AddWithValue("@Address", textBox4.Text);
                cmd.Parameters.AddWithValue("@ID", this.Id);

                conn.Open();
                 cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student Updated", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

                GetStudentRecord();
                NewMethod();
            }
            else
            {
                MessageBox.Show("Please Select Student First", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (Id>0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM  Students WHERE Id=@ID ", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
              MessageBox.Show("Selected Student has deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentRecord();
                NewMethod();

            }
            else
            {
                MessageBox.Show("Please Select Student First", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}

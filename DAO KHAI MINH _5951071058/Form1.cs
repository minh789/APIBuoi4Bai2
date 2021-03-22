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

namespace DAO_KHAI_MINH__5951071058
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-OQJ7HTJ\SQLEXPRESS;Initial Catalog=DemoCRUD;Integrated Security=True");
        public int StudentID;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentRecord();
            button3.Enabled = true;
        }

        private void GetStudentRecord()
        {
            String query = "Select * From Studentstb";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            dtgvSinhVien.DataSource = dt;
        }

        private bool IsValidData()
        {
            if(TxtHName.Text == string.Empty || TxtNName.Text == string.Empty || TxtAdress.Text == string.Empty || string.IsNullOrEmpty(TxtPhone.Text) || string.IsNullOrEmpty(TxtRoll.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!",
                    "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES(@Name,@FatherName,@RollNumber,@Address,@Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (StudentID >0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET Name = @Name,FatherName=@FatherName,RollNumber=@RollNumber,Address=@Address,Mobile=@Mobile WHERE StudentID=@ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", TxtHName.Text);
                cmd.Parameters.AddWithValue("@FatherName", TxtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", TxtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", TxtAdress.Text);
                cmd.Parameters.AddWithValue("@Mobile", TxtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi !!!", "Lỗi !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dtgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            StudentID = Convert.ToInt32(dtgvSinhVien.Rows[index].Cells[0].Value);
            TxtHName.Text = dtgvSinhVien.Rows[index].Cells[1].Value.ToString();
            TxtNName.Text = dtgvSinhVien.Rows[index].Cells[2].Value.ToString();
            TxtRoll.Text = dtgvSinhVien.Rows[index].Cells[3].Value.ToString();
            TxtAdress.Text = dtgvSinhVien.Rows[index].Cells[4].Value.ToString();
            TxtPhone.Text = dtgvSinhVien.Rows[index].Cells[5].Value.ToString();
            button3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

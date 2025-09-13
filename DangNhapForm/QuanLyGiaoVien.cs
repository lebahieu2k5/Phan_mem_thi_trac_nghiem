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
namespace QLGV
{
    public partial class QuanLyGiaoVien : Form
    {
        public QuanLyGiaoVien()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM giangvien";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string idGV = txtID.Text.Trim();
            string tenTaiKhoan = txtTenTk.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtMail.Text.Trim();
            string khoa = txtKhoa.Text.Trim();

            // Kiểm tra các trường nhập liệu có bị bỏ trống không
            if (string.IsNullOrEmpty(idGV) || string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(hoTen) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(khoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem khóa có tồn tại trong bảng Khoa không (nếu có khóa ngoại)
            string checkKhoaQuery = "SELECT COUNT(*) FROM khoa WHERE KhoaID = @KhoaID";
            SqlCommand checkKhoaCommand = new SqlCommand(checkKhoaQuery, connection);
            checkKhoaCommand.Parameters.Add("@KhoaID", SqlDbType.VarChar).Value = khoa;

            int khoaExists = (int)checkKhoaCommand.ExecuteScalar();
            if (khoaExists == 0)
            {
                MessageBox.Show("Khoa không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm dữ liệu vào bảng giangvien
            string query = "INSERT INTO giangvien (GiangVienID, TenTaiKhoan, HoTen, Email, KhoaID) " +
                           "VALUES (@GiangVienID, @TenTaiKhoan, @HoTen, @Email, @KhoaID)";

            // Tạo đối tượng SqlCommand và thêm các tham số
            command = new SqlCommand(query, connection);
            command.Parameters.Add("@GiangVienID", SqlDbType.VarChar).Value = idGV;
            command.Parameters.Add("@TenTaiKhoan", SqlDbType.VarChar).Value = tenTaiKhoan;
            command.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = hoTen;
            command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@KhoaID", SqlDbType.VarChar).Value = khoa;

            try
            {
                // Thực thi câu lệnh SQL để thêm dữ liệu
                command.ExecuteNonQuery();

                // Sau khi thêm thành công, gọi lại dữ liệu từ database và làm mới giao diện
                loaddata();

                // Xóa dữ liệu khỏi các ô nhập liệu
                txtTenTk.Clear();
                txtID.Clear();
                txtKhoa.Clear();
                txtMail.Clear();
                txtHoTen.Clear();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Nếu có lỗi trong quá trình thêm, hiển thị thông báo lỗi
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng trong bảng để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ dòng được chọn
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            string idGV = selectedRow.Cells["GiangVienID"].Value.ToString(); // Lấy ID từ dòng được chọn

            // Lấy dữ liệu từ TextBox
            string tenTaiKhoan = txtTenTk.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtMail.Text.Trim();
            string khoa = txtKhoa.Text.Trim();

            // Kiểm tra xem các giá trị có hợp lệ không
            if (string.IsNullOrEmpty(idGV) || string.IsNullOrEmpty(tenTaiKhoan) ||
                string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(khoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL UPDATE
            string query = "UPDATE giangvien SET TenTaiKhoan = @TenTaiKhoan, HoTen = @HoTen, Email = @Email, KhoaID = @KhoaID WHERE GiangVienID = @GiangVienID";

            try
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm tham số vào lệnh SQL
                    command.Parameters.AddWithValue("@GiangVienID", idGV);
                    command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                    command.Parameters.AddWithValue("@HoTen", hoTen);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@KhoaID", khoa);

                    // Thực thi câu lệnh
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra kết quả
                    if (rowsAffected > 0)
                    {
                        loaddata(); // Cập nhật lại dữ liệu trong DataGridView
                        MessageBox.Show("Sửa thông tin giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy giáo viên với ID được cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            string idGV = txtID.Text.Trim();
            if (string.IsNullOrEmpty(idGV))
            {
                MessageBox.Show("Vui lòng nhập ID giáo viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "DELETE FROM giangvien WHERE GiangVienID = @GiangVienID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@GiangVienID", idGV);
            try
            {
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    loaddata();
                    txtID.Clear();
                    txtTenTk.Clear();
                    txtHoTen.Clear();
                    txtMail.Clear();
                    txtKhoa.Clear();
                    MessageBox.Show("Xóa giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(" Không tìm thấy ID trên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btbTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                loaddata();
            }
            else
            {              
                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "SELECT * FROM giangvien WHERE GiangVienID LIKE @Keyword OR HoTen LIKE @Keyword OR KhoaID LIKE @Keyword";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                try
                {
                    DataTable searchTable = new DataTable();
                    adapter.SelectCommand = command;
                    searchTable.Clear();
                    adapter.Fill(searchTable);
                    dataGridView1.DataSource = searchTable;

                    if (searchTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtID.Text = row.Cells["GiangVienID"].Value.ToString();
                txtTenTk.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtMail.Text = row.Cells["Email"].Value.ToString();
                txtKhoa.Text = row.Cells["KhoaID"].Value.ToString();
            }
        }
    }
}

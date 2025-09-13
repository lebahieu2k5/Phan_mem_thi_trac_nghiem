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

namespace DangNhapForm
{
    public partial class QuanLyHocSinh : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        void loaddata()
        {
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM sinhvien";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        public QuanLyHocSinh()
        {
            InitializeComponent();
        }

        private void QuanLyHocSinh_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loaddata();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string idSV = txtID.Text.Trim();
            string tenTaiKhoan = txtTenTk.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtMail.Text.Trim();
            string khoa = txtKhoa.Text.Trim(); // Nếu bảng sinhvien không có cột Khoa, bạn có thể bỏ qua dòng này

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(idSV) || string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Câu lệnh SQL để thêm dữ liệu vào bảng sinhvien
            string query = "INSERT INTO sinhvien (SinhVienID, TenTaiKhoan, HoTen, Email) VALUES (@SinhVienID, @TenTaiKhoan, @HoTen, @Email)";
            command = new SqlCommand(query, connection);

            // Gán giá trị cho các tham số
            command.Parameters.AddWithValue("@SinhVienID", idSV);
            command.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
            command.Parameters.AddWithValue("@HoTen", hoTen);
            command.Parameters.AddWithValue("@Email", email);

            try
            {
                // Thực thi câu lệnh SQL
                command.ExecuteNonQuery();

                // Tải lại dữ liệu vào DataGridView
                loaddata();

                // Xóa nội dung trong các TextBox sau khi thêm
                txtTenTk.Clear();
                txtID.Clear();
                txtMail.Clear();
                txtHoTen.Clear();

                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
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
            string idSV = selectedRow.Cells["SinhVienID"].Value.ToString(); // Lấy ID từ dòng được chọn

            // Lấy dữ liệu từ TextBox
            string tenTaiKhoan = txtTenTk.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtMail.Text.Trim();
            string khoa = txtKhoa.Text.Trim();


            // Kiểm tra xem các giá trị có hợp lệ không
            if (string.IsNullOrEmpty(idSV) || string.IsNullOrEmpty(tenTaiKhoan) ||
                string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email))
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
                    command.Parameters.AddWithValue("@GiangVienID", idSV);
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
                        MessageBox.Show("Sửa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên với ID được cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            string idSV = txtID.Text.Trim();
            if (string.IsNullOrEmpty(idSV))
            {
                MessageBox.Show("Vui lòng nhập ID sinh viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string query = "DELETE FROM sinhvien WHERE SinhVienID = @SinhVienID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SinhVienID", idSV);
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
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy ID trên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                string query = "SELECT * FROM sinhvien WHERE SinhVienID LIKE @Keyword OR HoTen LIKE @Keyword OR TenTaiKhoan LIKE @Keyword";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0)
            //{
            //    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            //    txtID.Text = row.Cells["SinhVienID"].Value.ToString();
            //    txtTenTk.Text = row.Cells["TenTaiKhoan"].Value.ToString();
            //    txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            //    txtMail.Text = row.Cells["Email"].Value.ToString();
            //}
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Gán giá trị vào các TextBox
                txtID.Text = row.Cells["SinhVienID"].Value.ToString();
                txtTenTk.Text = row.Cells["TenTaiKhoan"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtMail.Text = row.Cells["Email"].Value.ToString();
                txtKhoa.Text = row.Cells["KhoaID"].Value.ToString();
            }
        }
    }
}

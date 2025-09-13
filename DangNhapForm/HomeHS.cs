using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HocSinh
{
    public partial class Form1 : Form
    {
        // Khai báo SqlConnection
        private SqlConnection connection;

        public Form1()
        {
            InitializeComponent();

            // Khởi tạo SqlConnection
            string connectionString = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                LoadMonIDToComboBox(); // Tải dữ liệu vào ComboBox khi form load
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void LoadMonIDToComboBox()
        {
            try
            {
                string query = "SELECT MonID, TenMon FROM monhoc";
                DataTable dtMonHoc = new DataTable();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtMonHoc);

                // Gán DataSource cho ComboBox
                comboBox1.DataSource = dtMonHoc;
                comboBox1.DisplayMember = "TenMon"; // Tên môn học
                comboBox1.ValueMember = "MonID";  // Mã môn học
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu vào ComboBox: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null && comboBox1.SelectedValue is int selectedMonID)
            {
                try
                {
                    // Truy vấn danh sách đề thi liên quan đến MonID
                    string query = "SELECT DeThiID, TenDeThi FROM dethi WHERE MonID = @MonID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MonID", selectedMonID);

                    // Lưu dữ liệu vào DataTable
                    DataTable dtDeThi = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dtDeThi);

                    // Gán dữ liệu vào ComboBox `Chọn Đề Thi`
                    comboBox2.DataSource = dtDeThi;
                    comboBox2.DisplayMember = "TenDeThi"; // Hiển thị tên đề thi
                    comboBox2.ValueMember = "DeThiID";   // Giá trị là DeThiID
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách đề thi: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu có item được chọn trong ComboBox `Chọn Đề Thi`
                if (comboBox2.SelectedValue != null)
                {
                    // Lấy DeThiID từ ComboBox `Chọn Đề Thi`
                    int selectedDeThiID = Convert.ToInt32(comboBox2.SelectedValue);

                    // Mở form DeThi với DeThiID được chọn
                    DeThiHS dt = new DeThiHS(selectedDeThiID);

                    dt.FormClosed += (s, args) => this.Show();
                    dt.Show();
                    this.Hide(); // Ẩn Form1 khi mở DeThi
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một đề thi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở đề thi: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.SelectedValue != null && int.TryParse(comboBox2.SelectedValue.ToString(), out int selectedDeThiID))
                {
                    MessageBox.Show($"Đang mở form Thi với DeThiID: {selectedDeThiID}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DeThiHS thiForm = new DeThiHS(selectedDeThiID);
                    thiForm.FormClosed += (s, args) => this.Show();
                    thiForm.Show();
                    //this.Hide();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một đề thi hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form Thi: " + ex.Message);
            }
        }

    }
}

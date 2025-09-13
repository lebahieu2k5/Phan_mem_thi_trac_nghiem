using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HocSinh
{
    public partial class ThongTinHocSinh : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        //string str = @"Data Source=LAPTOP-2OA5RPHI;Initial Catalog=BTL1;Integrated Security=True;Encrypt=False";

        public ThongTinHocSinh()
        {
            InitializeComponent();
            connection = new SqlConnection(str);
        }

        private void ThongTinHocSinh_Load(object sender, EventArgs e)
        {
            // Code xử lý khi form được load, nếu có.
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            // Code xử lý khi textBox5 thay đổi (nếu cần).
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Khởi tạo kết nối
            connection = new SqlConnection(str);

            try
            {
                // Mở kết nối
                connection.Open();

                // Kiểm tra SinhVienID có tồn tại trong bảng sinhvien không
                string checkSinhVienIDQuery = "SELECT COUNT(*) FROM sinhvien WHERE SinhVienID = @SinhVienID";
                command = new SqlCommand(checkSinhVienIDQuery, connection);
                command.Parameters.AddWithValue("@SinhVienID", textBox_SinhVien.Text);

                int sinhVienIDCount = (int)command.ExecuteScalar(); // Trả về số lượng bản ghi tìm được

                // Nếu SinhVienID đã tồn tại
                if (sinhVienIDCount > 0)
                {
                    MessageBox.Show("SinhVienID đã tồn tại. Vui lòng nhập SinhVienID khác!");
                    return; // Dừng lại không thực hiện thêm dữ liệu vào bảng sinhvien
                }

                // Kiểm tra TenTaiKhoan có tồn tại trong bảng sinhvien không
                string checkTenTaiKhoanQuery = "SELECT COUNT(*) FROM sinhvien WHERE TenTaiKhoan = @TenTaiKhoan";
                command = new SqlCommand(checkTenTaiKhoanQuery, connection);
                command.Parameters.AddWithValue("@TenTaiKhoan", textBox_TenTaiKhoan.Text);

                int tenTaiKhoanCount = (int)command.ExecuteScalar(); // Trả về số lượng bản ghi tìm được

                // Nếu TenTaiKhoan đã tồn tại trong bảng sinhvien
                if (tenTaiKhoanCount > 0)
                {
                    MessageBox.Show("Tài khoản đã có sẵn. Vui lòng chọn tên tài khoản khác!");
                    return; // Dừng lại không thực hiện thêm dữ liệu vào bảng sinhvien
                }

                // Nếu SinhVienID và TenTaiKhoan đều hợp lệ, tiếp tục thêm dữ liệu vào bảng sinhvien
                command = new SqlCommand("INSERT INTO sinhvien (SinhVienID, TenTaiKhoan, HoTen, Email, KhoaID) " +
                                         "VALUES (@SinhVienID, @TenTaiKhoan, @HoTen, @Email, @KhoaID)", connection);

                // Gán giá trị cho các tham số
                command.Parameters.AddWithValue("@SinhVienID", textBox_SinhVien.Text);
                command.Parameters.AddWithValue("@TenTaiKhoan", textBox_TenTaiKhoan.Text);
                command.Parameters.AddWithValue("@HoTen", textBox_HoTen.Text);
                command.Parameters.AddWithValue("@Email", textBox_Email.Text);
                command.Parameters.AddWithValue("@KhoaID", int.Parse(textBox_Khoa.Text));

                // Thực thi câu lệnh SQL để thêm dữ liệu vào bảng sinhvien
                command.ExecuteNonQuery();

                MessageBox.Show("Dữ liệu đã được thêm thành công!");
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}

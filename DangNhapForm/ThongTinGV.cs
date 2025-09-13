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

namespace BTN1
{
    public partial class ThongTinGV : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public ThongTinGV()
        {
            InitializeComponent();
            connection = new SqlConnection(str);
        }
        private string gv, hoten, em, tentk, khoaid;

        private void buttonCapNhat_Click(object sender, EventArgs e)
        {
            UpdateThongTinGiangVien();
        }

        private void UpdateThongTinGiangVien()
        {
            // Chuỗi kết nối đến cơ sở dữ liệu
            string connectionString = @"Data Source=LAPTOP-2OA5RPHI;Initial Catalog=BTL1;Integrated Security=True;Encrypt=False";

            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra xem TenTaiKhoan có tồn tại trong bảng taikhoan không
                    string checkAccountQuery = "SELECT COUNT(*) FROM taikhoan WHERE TenTaiKhoan = @TenTaiKhoan";
                    using (SqlCommand checkAccountCommand = new SqlCommand(checkAccountQuery, connection))
                    {
                        checkAccountCommand.Parameters.AddWithValue("@TenTaiKhoan", txtTenTK.Text);
                        int accountExists = (int)checkAccountCommand.ExecuteScalar();

                        if (accountExists > 0) // Nếu TenTaiKhoan có trong bảng taikhoan
                        {
                            // Tiếp tục kiểm tra xem TenTaiKhoan có tồn tại trong bảng giangvien chưa
                            string checkGiangVienQuery = "SELECT COUNT(*) FROM giangvien WHERE TenTaiKhoan = @TenTaiKhoan";
                            using (SqlCommand checkGiangVienCommand = new SqlCommand(checkGiangVienQuery, connection))
                            {
                                checkGiangVienCommand.Parameters.AddWithValue("@TenTaiKhoan", txtTenTK.Text);
                                int giangVienExists = (int)checkGiangVienCommand.ExecuteScalar();

                                if (giangVienExists == 0) // Nếu TenTaiKhoan chưa có trong bảng giangvien
                                {
                                    // Cập nhật thông tin vào bảng giangvien
                                    string updateQuery = @"
                            INSERT INTO giangvien (GiangVienID,TenTaiKhoan, HoTen, Email, KhoaID)
                            VALUES (@GiangVienID, @TenTaiKhoan,@HoTen, @Email, @KhoaID)";
                                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@TenTaiKhoan", txtTenTK.Text);
                                        updateCommand.Parameters.AddWithValue("@GiangVienID", txtGVID.Text); 
                                        updateCommand.Parameters.AddWithValue("@HoTen", txtHT.Text); 
                                        updateCommand.Parameters.AddWithValue("@Email", txtEmail.Text); 
                                        updateCommand.Parameters.AddWithValue("@KhoaID", txtKhoaId.Text); 

                                        int rowsAffected = updateCommand.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Cập nhật thông tin giảng viên thành công.");
                                            txtTenTK.Enabled = true;
                                            txtGVID.Enabled = true;
                                            txtHT.Enabled = true;
                                            txtEmail.Enabled = true;
                                            txtKhoaId.Enabled = true;
                                            khoaid = txtKhoaId.Text;
                                            hoten = txtHT.Text;
                                            em = txtEmail.Text;
                                            tentk = txtTenTK.Text;
                                            gv = txtGVID.Text;// Fix ham load lai de luu thong tin vua nhap vao 
                                        }
                                        else
                                        {
                                            MessageBox.Show("Không có dữ liệu được cập nhật.");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Tên tài khoản đã tồn tại trong bảng giảng viên.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên tài khoản không tồn tại trong bảng tài khoản.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        
    }
}
    

            

using BTN1;
using HocSinh;
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
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
       
        }

        private void linkLabel_QuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau quenMatKhau = new QuenMatKhau();
            quenMatKhau.ShowDialog();

        }

        private void linkLabel_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKy dangKy = new DangKy();
            dangKy.ShowDialog();

        }
        Modify modify = new Modify();
        private void DangNhap_Click(object sender, EventArgs e)
        {
            string tentk = textBox_TenTaiKhoan.Text;
            string mk = textBox_MatKhau.Text;

            if (tentk.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!");
            }
            else if (mk.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!");
            }
            else
            {
                // Kết nối với cơ sở dữ liệu
                string connectionString = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
                SqlConnection conn = new SqlConnection(connectionString);

                try
                {
                    conn.Open();
                    string query = "SELECT * FROM taikhoan WHERE TenTaiKhoan = @TenTaiKhoan AND MatKhau = @MatKhau";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", tentk);
                    cmd.Parameters.AddWithValue("@MatKhau", mk);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int role = Convert.ToInt32(reader["LoaiTaiKhoan"]);  // Lấy giá trị role từ cơ sở dữ liệu

                        if (role == 2)  // Kiểm tra nếu là ADMIN
                        {
                            MessageBox.Show("Đăng nhập thành công với quyền ADMIN", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            ADMIN adminForm = new ADMIN();  // Mở form Admin
                            adminForm.ShowDialog();
                            this.Close();
                        } else if ( role == 0 )  {
                            MessageBox.Show("Đăng nhập thành công với quyền Sinh Viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Form1 hs = new Form1();
                            hs.ShowDialog(); 
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thành công với quyền Giáo Viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Home home = new Home();  // Mở form Home nếu không phải là ADMIN
                            home.ShowDialog();
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        private void LoadData()
        {
            Modify modify = new Modify();
            using (SqlConnection connection = Connection.GetSqlConnection())
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM taikhoan", connection);
            }
        }



        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void textBox_TenTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_MatKhau_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

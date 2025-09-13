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
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace TaoDeThi
{
    public partial class DeThi1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public DeThi1()
        {
            InitializeComponent();
        }
        void loadata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select*from dethi";
            //command.ExecuteNonQuery();
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }
        void LoadComboBoxMonThi()
        {
            try
            {
                // Tạo câu lệnh SQL để lấy dữ liệu MonID và TenMon
                SqlCommand command = new SqlCommand("SELECT MonID, TenMon FROM monhoc", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Gán dữ liệu vào ComboBox
                comboBox_MonThi.DataSource = dt;
                comboBox_MonThi.DisplayMember = "TenMon";  // Hiển thị tên môn học
                comboBox_MonThi.ValueMember = "MonID";    // Giá trị là MonID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu môn học: " + ex.Message);
            }
        }

        void LoadComboBoxGiangVien()
        {
            try
            {
                // Tạo câu lệnh SQL để lấy dữ liệu GiangVienID và HoTen
                SqlCommand command = new SqlCommand("SELECT GiangVienID, HoTen FROM giangvien", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Gán dữ liệu vào ComboBox
                comboBox_GiangVien.DataSource = dt;
                comboBox_GiangVien.DisplayMember = "HoTen";       // Hiển thị tên giảng viên
                comboBox_GiangVien.ValueMember = "GiangVienID";   // Giá trị là GiangVienID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu giảng viên: " + ex.Message);
            }
        }

        void LoadComboBoxKhoa()
        {
            try
            {
                // Tạo câu lệnh SQL để lấy dữ liệu KhoaID và TenKhoa
                SqlCommand command = new SqlCommand("SELECT KhoaID, TenKhoa FROM khoa", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // Gán dữ liệu vào ComboBox
                comboBox_Khoa.DataSource = dt;
                comboBox_Khoa.DisplayMember = "TenKhoa";  // Hiển thị tên khoa
                comboBox_Khoa.ValueMember = "KhoaID";    // Giá trị là KhoaID
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khoa: " + ex.Message);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadata();
            LoadComboBoxMonThi();
            LoadComboBoxGiangVien();
            LoadComboBoxKhoa();
        }




        private void button4_Click(object sender, EventArgs e)
        {
            // Tạo câu lệnh SQL để thêm dữ liệu vào bảng dethi
            command = new SqlCommand("INSERT INTO dethi (DeThiID, MonID, TenDeThi, SoCauHoi, NgayTao, GiangVienID, KhoaID) " +
                                      "VALUES (@DeThiID, @MonID, @TenDeThi, @SoCauHoi, @NgayTao, @GiangVienID, @KhoaID)", connection);

            // Gán giá trị cho các tham số từ ComboBox và TextBox
            command.Parameters.AddWithValue("@DeThiID", textBox_DeThi.Text);
            command.Parameters.AddWithValue("@MonID", comboBox_MonThi.SelectedValue); // Lấy giá trị MonID từ ComboBox_MonThi
            command.Parameters.AddWithValue("@TenDeThi", textBox_TenDeThi.Text);
            command.Parameters.AddWithValue("@SoCauHoi", int.Parse(textBox_CauHoi.Text)); // Chuyển thành số nguyên
            command.Parameters.AddWithValue("@NgayTao", dateTimePicker1.Value);          // Lấy giá trị từ DateTimePicker
            command.Parameters.AddWithValue("@GiangVienID", comboBox_GiangVien.SelectedValue); // Lấy GiangVienID từ ComboBox_GiangVien
            command.Parameters.AddWithValue("@KhoaID", comboBox_Khoa.SelectedValue);    // Lấy giá trị KhoaID từ ComboBox_Khoa

            try
            {
                // Thực thi câu lệnh SQL
                command.ExecuteNonQuery();
                MessageBox.Show("Dữ liệu đã được thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            loadata();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //    textBox_GiangVien.Enabled = false;
            //    int i;
            //    i = dataGridView1.CurrentRow.Index;
            //    textBox_DeThi.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            //    textBox_MonThi.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            //    textBox_TenDeThi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            //    textBox_CauHoi.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            //    textBox_NgayTao.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            //    textBox_GiangVien.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            command = new SqlCommand("DELETE FROM dethi WHERE DeThiID = @DeThiID", connection);
            command.Parameters.AddWithValue("@DeThiID", textBox_DeThi.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Xóa đề thi thành công!");
            loadata();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Tạo câu lệnh SQL để cập nhật dữ liệu trong bảng dethi
            command = new SqlCommand("UPDATE dethi SET MonID = @MonID, TenDeThi = @TenDeThi, " +
                     "SoCauHoi = @SoCauHoi, NgayTao = @NgayTao, GiangVienID = @GiangVienID, KhoaID = @KhoaID " +
                     "WHERE DeThiID = @DeThiID", connection);

            // Gán giá trị cho các tham số từ ComboBox và TextBox
            command.Parameters.AddWithValue("@MonID", comboBox_MonThi.SelectedValue);  // Lấy giá trị MonID từ ComboBox_MonThi
            command.Parameters.AddWithValue("@TenDeThi", textBox_TenDeThi.Text);      // Lấy giá trị từ textBox_TenDeThi
            command.Parameters.AddWithValue("@SoCauHoi", int.Parse(textBox_CauHoi.Text)); // Chuyển thành số nguyên
            command.Parameters.AddWithValue("@NgayTao", dateTimePicker1.Value);       // Lấy giá trị từ DateTimePicker
            command.Parameters.AddWithValue("@GiangVienID", comboBox_GiangVien.SelectedValue); // Lấy GiangVienID từ ComboBox_GiangVien
            command.Parameters.AddWithValue("@KhoaID", comboBox_Khoa.SelectedValue);  // Lấy KhoaID từ ComboBox_Khoa
            command.Parameters.AddWithValue("@DeThiID", textBox_DeThi.Text);          // Lấy giá trị DeThiID từ textBox_DeThi

            try
            {
                // Thực thi câu lệnh SQL
                command.ExecuteNonQuery();
                MessageBox.Show("Cập nhật đề thi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật đề thi: " + ex.Message);
            }

            // Tải lại dữ liệu sau khi cập nhật
            loadata();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            DeThi2 f2 = new DeThi2();
            f2.Show();
        }
        private int GetNextQuestionID()
        {
            int nextId = 1; // Giá trị mặc định nếu bảng trống

            string query = "SELECT MAX(DeThiID) FROM dethi";
            using (SqlConnection connection = new SqlConnection(str))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                // Nếu bảng cauhoi không có dữ liệu, giá trị MAX sẽ là null, do đó set nextId = 1
                object result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    nextId = Convert.ToInt32(result) + 1;
                }
            }
            return nextId;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int i = dataGridView1.CurrentRow.Index;

                // Gán giá trị cho TextBox
                textBox_DeThi.Text = dataGridView1.Rows[i].Cells["DeThiID"].Value.ToString();
                textBox_TenDeThi.Text = dataGridView1.Rows[i].Cells["TenDeThi"].Value.ToString();
                textBox_CauHoi.Text = dataGridView1.Rows[i].Cells["SoCauHoi"].Value.ToString();

                // Gán giá trị cho DateTimePicker
                if (dataGridView1.Rows[i].Cells["NgayTao"].Value != DBNull.Value)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["NgayTao"].Value);
                }

                // Gán giá trị cho ComboBox_MonThi (MonID)
                if (dataGridView1.Rows[i].Cells["MonID"].Value != DBNull.Value)
                {
                    comboBox_MonThi.SelectedValue = dataGridView1.Rows[i].Cells["MonID"].Value.ToString();
                }

                // Gán giá trị cho ComboBox_GiangVien (GiangVienID)
                if (dataGridView1.Rows[i].Cells["GiangVienID"].Value != DBNull.Value)
                {
                    comboBox_GiangVien.SelectedValue = dataGridView1.Rows[i].Cells["GiangVienID"].Value.ToString();
                }

                // Gán giá trị cho ComboBox_Khoa (KhoaID)
                if (dataGridView1.Rows[i].Cells["KhoaID"].Value != DBNull.Value)
                {
                    comboBox_Khoa.SelectedValue = dataGridView1.Rows[i].Cells["KhoaID"].Value.ToString();
                }

                // Kiểm tra nếu là dòng cuối cùng thì lấy ID tiếp theo
                if (i == dataGridView1.Rows.Count - 1)
                {
                    // Lấy ID tiếp theo (Giả sử bạn có hàm để lấy ID tiếp theo)
                    int newId = GetNextQuestionID();
                    textBox_DeThi.Text = newId.ToString();  // Hiển thị ID mới trong TextBox
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox_MonThi.SelectedValue != null)
            //{
            //    string selectedMonID = comboBox_MonThi.SelectedValue.ToString();
            //    MessageBox.Show("Bạn đã chọn MônID: " + selectedMonID);
            //}




        }

        private void button1_Click(object sender, EventArgs e)
        {
            XemDiemSV xemDiemSV = new XemDiemSV();
            xemDiemSV.ShowDialog();
        }
    }
}

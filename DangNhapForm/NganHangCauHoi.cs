using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace Nganhangcauhoi
{
    public partial class NganHangCauHoi : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public NganHangCauHoi()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            // Mở kết nối với cơ sở dữ liệu
            connection = new SqlConnection(str);
            connection.Open();

            // Lấy giá trị MAX(CauHoiID) từ bảng cauhoi
            SqlCommand cmd = new SqlCommand("SELECT MAX(CauHoiID) FROM cauhoi", connection);
            object result = cmd.ExecuteScalar();

            // Nếu bảng chưa có dữ liệu, bắt đầu từ CauHoiID = 1
            int nextCauHoiID = result != DBNull.Value ? Convert.ToInt32(result) + 1 : 1;

            // Kiểm tra nếu CauHoiID đã tồn tại
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM cauhoi WHERE CauHoiID = @CauHoiID", connection);
            checkCmd.Parameters.AddWithValue("@CauHoiID", nextCauHoiID);
            int count = (int)checkCmd.ExecuteScalar();

            // Nếu CauHoiID tồn tại, tăng giá trị cho đến khi tìm được giá trị duy nhất
            while (count > 0)
            {
                nextCauHoiID++;
                checkCmd.Parameters["@CauHoiID"].Value = nextCauHoiID;
                count = (int)checkCmd.ExecuteScalar();
            }

            // Hiển thị giá trị vào TextBox CauHoiID
            txtid.Text = nextCauHoiID.ToString();
            txtid.Enabled = false;

            // Load dữ liệu từ bảng 'cauhoi' vào DataGridView
            SqlCommand command = new SqlCommand("SELECT * FROM cauhoi", connection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            // Load dữ liệu vào ComboBox
            string query = "SELECT MonID, TenMon FROM monhoc";
            SqlDataAdapter adapterMonHoc = new SqlDataAdapter(query, connection);
            DataTable tableMonHoc = new DataTable();
            adapterMonHoc.Fill(tableMonHoc);

            // Kiểm tra nếu tableMonHoc có dữ liệu
            if (tableMonHoc.Rows.Count > 0)
            {
                comboBox1.DataSource = tableMonHoc;
                comboBox1.DisplayMember = "TenMon"; // Hiển thị tên môn học
                comboBox1.ValueMember = "MonID";    // Lấy giá trị MonID từ mỗi mục trong ComboBox
            }
        }
            void loaddata()
            {

                command = connection.CreateCommand();
                command.CommandText = "SELECT*FROM  cauhoi";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);// đổ dl lên table
                dataGridView1.DataSource = table;


            }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị lớn nhất hiện tại của CauHoiID
                SqlCommand getMaxIdCmd = new SqlCommand("SELECT ISNULL(MAX(CauHoiID), 0) FROM cauhoi", connection);
                int nextCauHoiID = (int)getMaxIdCmd.ExecuteScalar() + 1;

                // Thêm câu hỏi mới
                SqlCommand insertCmd = new SqlCommand("INSERT INTO cauhoi (CauHoiID, MonID, NoiDungCauHoi, DapAnA, DapAnB, DapAnC, DapAnD, DapAnDung) " +
                                                      "VALUES (@CauHoiID, @MonID, @NoiDungCauHoi, @DapAnA, @DapAnB, @DapAnC, @DapAnD, @DapAnDung)", connection);
                insertCmd.Parameters.AddWithValue("@CauHoiID", nextCauHoiID);
                insertCmd.Parameters.AddWithValue("@MonID", comboBox1.SelectedValue);
                insertCmd.Parameters.AddWithValue("@NoiDungCauHoi", txtcauhoi.Text);
                insertCmd.Parameters.AddWithValue("@DapAnA", txtA.Text);
                insertCmd.Parameters.AddWithValue("@DapAnB", txtB.Text);
                insertCmd.Parameters.AddWithValue("@DapAnC", txtC.Text);
                insertCmd.Parameters.AddWithValue("@DapAnD", txtD.Text);
                insertCmd.Parameters.AddWithValue("@DapAnDung", txtdapan.Text);

                // Thực thi lệnh INSERT
                insertCmd.ExecuteNonQuery();

                // Cập nhật lại dữ liệu trong DataGridView
                loaddata();

                // Hiển thị thông báo
                MessageBox.Show("Thêm câu hỏi thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm câu hỏi: " + ex.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            command = new SqlCommand("DELETE FROM cauhoi WHERE CauHoiID = @CauHoiID", connection);

            // Add parameters
            command.Parameters.AddWithValue("@CauHoiID", txtid.Text);

            // Execute the command
            command.ExecuteNonQuery();

            // Reload data
            loaddata();

            MessageBox.Show("Xóa câu hỏi thành công!");

        }
    
    
        private void button2_Click(object sender, EventArgs e)
        {
        command = new SqlCommand("UPDATE cauhoi SET " +
                             "MonID = @MonID, " +
                             "NoiDungCauHoi = @NoiDungCauHoi, " +
                             "DapAnA = @DapAnA, " +
                             "DapAnB = @DapAnB, " +
                             "DapAnC = @DapAnC, " +
                             "DapAnD = @DapAnD, " +
                             "DapAnDung = @DapAnDung " +
                             "WHERE CauHoiID = @CauHoiID", connection);

        // Add parameters
        command.Parameters.AddWithValue("@MonID", comboBox1.SelectedValue);
        command.Parameters.AddWithValue("@NoiDungCauHoi", txtcauhoi.Text);
        command.Parameters.AddWithValue("@DapAnA", txtA.Text);
        command.Parameters.AddWithValue("@DapAnB", txtB.Text);
        command.Parameters.AddWithValue("@DapAnC", txtC.Text);
        command.Parameters.AddWithValue("@DapAnD", txtD.Text);
        command.Parameters.AddWithValue("@DapAnDung", txtdapan.Text);
        command.Parameters.AddWithValue("@CauHoiID", txtid.Text);

        // Execute the command  
        command.ExecuteNonQuery();

        // Reload data
        loaddata();

        MessageBox.Show("Cập nhật câu hỏi thành công!");
    }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có hàng nào được chọn
            if (dataGridView1.CurrentRow != null)
            {
                // Truy cập các giá trị của các ô trong dòng đã chọn
                try
                {
                    int i = dataGridView1.CurrentRow.Index;  // Lấy chỉ số dòng hiện tại
                    txtid.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    txtcauhoi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    txtA.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    txtB.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    txtC.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    txtD.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    txtdapan.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy cập dữ liệu: " + ex.Message);
                }
            }
           
        }
        private void LoadMonIDToComboBox()
        {
            string query = "SELECT MonID, TenMon FROM monhoc";
            DataTable dtMonHoc = new DataTable();

            using (SqlConnection connection = new SqlConnection(str))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dtMonHoc.Load(reader);
                reader.Close();
            }

            // Gán DataSource cho ComboBox
            comboBox1.DataSource = dtMonHoc;
            comboBox1.DisplayMember = "TenMon"; // Tên môn học
            comboBox1.ValueMember = "MonID";  // Mã môn học
        }

        private void comboBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            // Nếu cần xử lý khi thay đổi lựa chọn trong ComboBox, bạn có thể làm ở đây
            if (comboBox1.SelectedItem != null)
            {
                // Lấy giá trị MonID từ item đã chọn trong ComboBox
                DataRowView selectedRow = comboBox1.SelectedItem as DataRowView;
                int selectedMonID = Convert.ToInt32(selectedRow["MonID"]);

                // Lọc dữ liệu trong DataGridView theo MonID đã chọn
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = "MonID = " + selectedMonID;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            command = new SqlCommand("UPDATE cauhoi SET " +
                                "MonID = @MonID, " +
                                "NoiDungCauHoi = @NoiDungCauHoi, " +
                                "DapAnA = @DapAnA, " +
                                "DapAnB = @DapAnB, " +
                                "DapAnC = @DapAnC, " +
                                "DapAnD = @DapAnD, " +
                                "DapAnDung = @DapAnDung " +
                                "WHERE CauHoiID = @CauHoiID", connection);

            // Add parameters
            command.Parameters.AddWithValue("@MonID", comboBox1.SelectedValue);
            command.Parameters.AddWithValue("@NoiDungCauHoi", txtcauhoi.Text);
            command.Parameters.AddWithValue("@DapAnA", txtA.Text);
            command.Parameters.AddWithValue("@DapAnB", txtB.Text);
            command.Parameters.AddWithValue("@DapAnC", txtC.Text);
            command.Parameters.AddWithValue("@DapAnD", txtD.Text);
            command.Parameters.AddWithValue("@DapAnDung", txtdapan.Text);
            command.Parameters.AddWithValue("@CauHoiID", txtid.Text);

            // Execute the command
            command.ExecuteNonQuery();

            // Reload data
            loaddata();

            MessageBox.Show("Cập nhật câu hỏi thành công!");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


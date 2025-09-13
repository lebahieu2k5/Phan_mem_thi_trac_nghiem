using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TaoDeThi
{
    public partial class XemDiemSV : Form
    {
        // con tró hếu 
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public XemDiemSV()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtSearchSinhVienID, "Con lợn Hiếu");
            // Khởi tạo kết nối
            connection = new SqlConnection(str);

            try
            {
                // Mở kết nối
                connection.Open();

                // Gọi hàm LoadData để hiển thị dữ liệu ban đầu lên DataGridView
                LoadData();

                // Gọi hàm LoadTenDeThiIDToComboBox để tải dữ liệu vào ComboBox
                LoadTenDeThiIDToComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            // Câu lệnh truy vấn dữ liệu
            string query = "SELECT * FROM ketquathi";

            // Tạo SqlCommand với truy vấn
            command = new SqlCommand(query, connection);

            // Sử dụng SqlDataAdapter để điền dữ liệu vào DataTable
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);

            // Gán dữ liệu vào DataGridView
            dataGridView1.DataSource = table;
        }

        private void LoadTenDeThiIDToComboBox()
        {
            // Truy vấn lấy danh sách đề thi duy nhất
            string query = @"
            SELECT DISTINCT d.DeThiID, d.TenDeThi
            FROM dethi d
            INNER JOIN ketquathi k ON d.DeThiID = k.DeThiID";

            // Tạo SqlCommand và SqlDataAdapter
            command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            // Điền dữ liệu vào DataTable
            adapter.Fill(dataTable);

            // Xóa dữ liệu cũ trong ComboBox (nếu có)
            cbb_kqmon.DataSource = null;
            cbb_kqmon.Items.Clear();

            // Gán dữ liệu vào ComboBox
            cbb_kqmon.DataSource = dataTable;
            cbb_kqmon.DisplayMember = "TenDeThi";  // Hiển thị tên đề thi
            cbb_kqmon.ValueMember = "DeThiID";    // Giá trị là DeThiID
        }

        private void cbb_kqmon_SelectedValueChanged(object sender, EventArgs e)
        {
            // Kiểm tra giá trị SelectedValue không null và không phải là kiểu DataRowView
            if (cbb_kqmon.SelectedValue != null && !(cbb_kqmon.SelectedValue is DataRowView))
            {
                try
                {
                    // Chuyển đổi SelectedValue thành chuỗi và sau đó sử dụng trong truy vấn
                    string selectedDeThiID = cbb_kqmon.SelectedValue.ToString();

                    string query = @"
            SELECT k.KetQuaID, k.DeThiID, k.SinhVienID, k.DiemSo, k.KhoaID, k.GiangVienID
            FROM ketquathi k
            WHERE k.DeThiID = @DeThiID";

          
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                    DataTable filteredTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(filteredTable);
                    dataGridView1.DataSource = filteredTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
                }
            }
        }


        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đóng kết nối khi form đóng
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void cbb_kqmon_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị SinhVienID từ TextBox
            string searchSinhVienID = txtSearchSinhVienID.Text.Trim();

            if (!string.IsNullOrEmpty(searchSinhVienID))
            {
                try
                {
                    // Truy vấn dữ liệu theo SinhVienID
                    string query = @"
            SELECT k.KetQuaID, k.DeThiID, k.SinhVienID, k.DiemSo, k.KhoaID, k.GiangVienID
            FROM ketquathi k
            WHERE k.SinhVienID = @SinhVienID";

                    // Tạo SqlCommand
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@SinhVienID", searchSinhVienID);

                    // Sử dụng SqlDataAdapter để điền dữ liệu vào DataTable
                    DataTable searchTable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(searchTable);

                    // Kiểm tra nếu không có dữ liệu
                    if (searchTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy kết quả phù hợp.");
                    }

                    // Gán dữ liệu vào DataGridView
                    dataGridView1.DataSource = searchTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã SinhVienID để tìm kiếm.");
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}

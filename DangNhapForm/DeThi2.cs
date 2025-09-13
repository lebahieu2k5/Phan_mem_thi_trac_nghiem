using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace TaoDeThi
{
    public partial class DeThi2 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public DeThi2()
        {
            InitializeComponent();
        }

        void loadata()
        {
            command = connection.CreateCommand();
            command.CommandText = "select * from cauhoi";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void LoadRandomQuestions(int monID)
        {
            using (SqlConnection connection = new SqlConnection(str))
            {

                string query = "SELECT TOP 20 * FROM cauhoi WHERE MonID = @MonID ORDER BY NEWID()";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@MonID", monID);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();

            // Lấy danh sách các MonID từ cơ sở dữ liệu và thêm vào ComboBox
            //LoadMonIDToComboBox();
            LoadDeThiIDToComboBox();
        }

        //private void LoadMonIDToComboBox()
        //{
        //    string query = "SELECT MonID, TenMon FROM monhoc";
        //    using (SqlConnection connection = new SqlConnection(str))
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        // Clear existing items in ComboBox
        //        comboBox_MonID.Items.Clear();

        //        // Add KeyValuePair to ComboBox (MonID as Key, TenMon as Value)
        //        while (reader.Read())
        //        {
        //            comboBox_MonID.Items.Add(new KeyValuePair<int, string>(
        //                (int)reader["MonID"],
        //                reader["TenMon"].ToString()
        //            ));
        //        }
        //        reader.Close();
        //    }

        //    // Set DisplayMember and ValueMember
        //    comboBox_MonID.DisplayMember = "Value";  // Display the name of the subject
        //    comboBox_MonID.ValueMember = "Key";     // Store MonID (as Key)
        //}
        private void LoadDeThiIDToComboBox()
        {
            string query = "SELECT DeThiID, TenDeThi FROM dethi";

            // Mở kết nối đến cơ sở dữ liệu
            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();

                // Tạo câu lệnh SQL để lấy danh sách các đề thi
                SqlCommand command = new SqlCommand(query, connection);

                // Tạo SqlDataAdapter và DataTable để lưu trữ kết quả
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                // Điền dữ liệu vào DataTable
                adapter.Fill(dataTable);

                // Gán DataTable vào ComboBox
                comboBox_DeThiID.DataSource = dataTable;
                comboBox_DeThiID.DisplayMember = "TenDeThi";  // Hiển thị tên đề thi
                comboBox_DeThiID.ValueMember = "DeThiID";    // Lưu trữ DeThiID
            }
        }


        //private void comboBox_MonID_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Không cần thực hiện gì trong sự kiện này nếu chỉ muốn random khi nhấn nút
        //}

        private void button_TaoDeThiTuNganHangChung_Click(object sender, EventArgs e)
        {
            if (comboBox_DeThiID.SelectedItem is KeyValuePair<int, string> selectedMon)
            {
                LoadRandomQuestions(selectedMon.Key); // Random 20 câu hỏi theo MonID được chọn
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Môn Thi ID trước khi tạo đề thi.");
            }
        }

        private void button_Sua_Click(object sender, EventArgs e)
        {
            DataRowView selectedRow = (DataRowView)comboBox_DeThiID.SelectedItem;

            // Lấy DeThiID từ DataRowView
            int selectedDeThiID = Convert.ToInt32(selectedRow["DeThiID"]);

            // Truy vấn để lấy MonID và TenMon tương ứng với DeThiID đã chọn
            string query = "SELECT MonID FROM dethi WHERE DeThiID = @DeThiID";
            int monID = -1;
            string tenMon = string.Empty;

            using (SqlConnection connection = new SqlConnection(str))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    monID = Convert.ToInt32(dt.Rows[0]["MonID"]);
                }
            }
            // Kiểm tra nếu textBox_MonHocID không có giá trị hợp lệ
            if (string.IsNullOrWhiteSpace(textBox_MonID.Text))
            {
                MessageBox.Show("Vui lòng nhập Môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại nếu MonID chưa được nhập
            }

            // Kiểm tra nếu người dùng chưa nhập CauHoiID
            if (string.IsNullOrWhiteSpace(textBox_CauHoi2.Text) || !int.TryParse(textBox_CauHoi2.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập CauHoiID hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu các đáp án không trống
            if (string.IsNullOrWhiteSpace(textBox_DapanA.Text) || string.IsNullOrWhiteSpace(textBox_DapAnB.Text) ||
                string.IsNullOrWhiteSpace(textBox_DapAnC.Text) || string.IsNullOrWhiteSpace(textBox_DapAnD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các đáp án!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Lấy giá trị MonID từ TextBox

                // Lấy giá trị CauHoiID từ TextBox
                int cauHoiID = int.Parse(textBox_CauHoi2.Text);

                // Chuẩn bị câu lệnh INSERT với tham số
                using (SqlConnection connection = new SqlConnection(str)) // str là chuỗi kết nối tới CSDL
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "UPDATE cauhoi SET NoiDungCauHoi = '" + textBox_ND.Text + "', " +
                        "DapAnA = '" + textBox_DapanA.Text + "', " +
                        "DapAnB = '" + textBox_DapAnB.Text + "', " +
                        "DapAnC = '" + textBox_DapAnC.Text + "', " +
                        "DapAnD = '" + textBox_DapAnD.Text + "', " +
                        "DapAnDung = '" + textBox_DapAnDung.Text + "' " +
                        "WHERE CauHoiID = '" + textBox_CauHoi2.Text + "' ";

                    // Thực thi câu lệnh INSERT
                    int result = command.ExecuteNonQuery();

                    // Kiểm tra kết quả thực thi câu lệnh
                    if (result > 0)
                    {
                        MessageBox.Show("Sửa câu hỏi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        command = connection.CreateCommand();
                        command.CommandText = "select * from cauhoi where MonID = '" + monID + "'";
                        adapter.SelectCommand = command;
                        table.Clear();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                    else
                    {
                        MessageBox.Show("Thêm câu hỏi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề trong quá trình thực hiện
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (comboBox_MonID.SelectedItem is KeyValuePair<int, string> selectedMon)
            //{
            //    LoadRandomQuestions(selectedMon.Key); // Random 20 câu hỏi theo MonID được chọn
            //}
            DataRowView selectedRow = (DataRowView)comboBox_DeThiID.SelectedItem;
            if (textBox_MonID.Text != null)
            {
                int selectedDeThiID = Convert.ToInt32(selectedRow["DeThiID"]);

                string query = "SELECT MonID FROM dethi WHERE DeThiID = @DeThiID";
                int monID = -1;
                string tenMon = string.Empty;

                using (SqlConnection connection = new SqlConnection(str))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        monID = Convert.ToInt32(dt.Rows[0]["MonID"]);
                    }
                }
                LoadRandomQuestions(monID);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Môn Thi ID trước khi tạo đề thi.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox_CauHoi2.Enabled = false;
            int i;
            i = dataGridView1.CurrentRow.Index;
            textBox_CauHoi2.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            textBox_ND.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            textBox_DapanA.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            textBox_DapAnB.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            textBox_DapAnC.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
            textBox_DapAnD.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
            textBox_DapAnDung.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                textBox_CauHoi2.Enabled = false;
                int i = dataGridView1.CurrentRow.Index;

                // Gán giá trị cho các textbox từ các ô của dòng hiện tại
                textBox_CauHoi2.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                textBox_ND.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                textBox_DapanA.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                textBox_DapAnB.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                textBox_DapAnC.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                textBox_DapAnD.Text = dataGridView1.Rows[i].Cells[6].Value.ToString();
                textBox_DapAnDung.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();

                // Kiểm tra nếu dòng hiện tại là dòng cuối cùng
                if (i == dataGridView1.Rows.Count - 1)
                {
                    // Lấy ID tiếp theo
                    int newId = GetNextQuestionID();
                    textBox_CauHoi2.Text = newId.ToString();  // Hiển thị ID mới trong TextBox
                }
            }
        }

        private int GetNextQuestionID()
        {
            int nextId = 1; // Giá trị mặc định nếu bảng trống

            string query = "SELECT MAX(CauHoiID) FROM cauhoi";
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            // Thêm cột cho DataTable tương ứng với bảng cauhoi
            dt.Columns.Add("CauHoiID", typeof(int));
            dt.Columns.Add("MonID", typeof(int));
            dt.Columns.Add("NoiDungCauHoi", typeof(string));
            dt.Columns.Add("DapAnA", typeof(string));
            dt.Columns.Add("DapAnB", typeof(string));
            dt.Columns.Add("DapAnC", typeof(string));
            dt.Columns.Add("DapAnD", typeof(string));
            dt.Columns.Add("DapAnDung", typeof(string));

            // Đặt DataSource cho DataGridView để hiển thị bảng trống với tên cột
            dataGridView1.DataSource = dt;
        }


        private void comboBox_DeThiID_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox_DeThiID.SelectedIndex >= 0)
            {
                // Lấy DataRowView từ SelectedItem
                DataRowView selectedRow = (DataRowView)comboBox_DeThiID.SelectedItem;

                // Lấy DeThiID từ DataRowView
                int selectedDeThiID = Convert.ToInt32(selectedRow["DeThiID"]);

                // Truy vấn để lấy MonID và TenMon tương ứng với DeThiID đã chọn
                string query = "SELECT MonID FROM dethi WHERE DeThiID = @DeThiID";
                int monID = -1;
                string tenMon = string.Empty;

                using (SqlConnection connection = new SqlConnection(str))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        monID = Convert.ToInt32(dt.Rows[0]["MonID"]);
                    }
                }

                // Lấy tên môn từ bảng môn học (monhoc)
                if (monID >= 0)
                {
                    // Truy vấn để lấy tên môn học
                    string queryMon = "SELECT TenMon FROM monhoc WHERE MonID = @MonID";
                    using (SqlConnection connection = new SqlConnection(str))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(queryMon, connection);
                        adapter.SelectCommand.Parameters.AddWithValue("@MonID", monID);
                        DataTable dtMon = new DataTable();
                        adapter.Fill(dtMon);

                        if (dtMon.Rows.Count > 0)
                        {
                            tenMon = dtMon.Rows[0]["TenMon"].ToString();
                            textBox_MonID.Text = tenMon;  // Hiển thị tên môn vào TextBox
                        }
                    }
                }
                command = connection.CreateCommand();
                command.CommandText = "select * from cauhoi where MonID = '" + monID + "'";
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table);
                dataGridView1.DataSource = table;


            }
        }


        private void button5_Click(object sender, EventArgs e)
        {

            DataRowView selectedRow = (DataRowView)comboBox_DeThiID.SelectedItem;

            // Lấy DeThiID từ DataRowView
            int selectedDeThiID = Convert.ToInt32(selectedRow["DeThiID"]);

            // Truy vấn để lấy MonID và TenMon tương ứng với DeThiID đã chọn
            string query = "SELECT MonID FROM dethi WHERE DeThiID = @DeThiID";
            int monID = -1;
            string tenMon = string.Empty;

            using (SqlConnection connection = new SqlConnection(str))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    monID = Convert.ToInt32(dt.Rows[0]["MonID"]);
                }
            }
            int selectedMonID = monID;
            // Xóa các bản ghi tham chiếu trong bảng chitietdethi
            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM chitietdethi WHERE CauHoiID = @CauHoiID";
            command.Parameters.AddWithValue("@CauHoiID", textBox_CauHoi2.Text);
            command.ExecuteNonQuery();

            // Sau khi xóa các bản ghi tham chiếu, xóa câu hỏi trong bảng cauhoi
            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM cauhoi WHERE CauHoiID = @CauHoiID";
            command.Parameters.AddWithValue("@CauHoiID", textBox_CauHoi2.Text);
            command.ExecuteNonQuery();

            // Cập nhật lại danh sách câu hỏi
            command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM cauhoi WHERE MonID = @MonID";
            command.Parameters.AddWithValue("@MonID", selectedMonID);
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            DeThi1  form = new DeThi1();
            form.ShowDialog();
            loadata();
        }

        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void button_Them_Click(object sender, EventArgs e)
        //{
        //    int cauHoiID = (int)dataGridView1.CurrentRow.Cells["CauHoiID"].Value;
        //    command = connection.CreateCommand();
        //    command.CommandText = "INSERT INTO cauhoi SET NoiDungCauHoi = '" + textBox_ND.Text + "', " +
        //              "DapAnA = '" + textBox_DapanA.Text + "', " +
        //              "DapAnB = '" + textBox_DapAnB.Text + "', " +
        //              "DapAnC = '" + textBox_DapAnC.Text + "', " +
        //              "DapAnD = '" + textBox_DapAnD.Text + "', " +
        //              "DapAnDung = '" + textBox_DapAnDung.Text + "' " +
        //              "WHERE CauHoiID = '" + textBox_CauHoi2.Text + "' ";
        //    command.ExecuteNonQuery();
        //    loadata();
        //}
        private void button_Them_Click(object sender, EventArgs e)
        {

            DataRowView selectedRow = (DataRowView)comboBox_DeThiID.SelectedItem;

            // Lấy DeThiID từ DataRowView
            int selectedDeThiID = Convert.ToInt32(selectedRow["DeThiID"]);

            // Truy vấn để lấy MonID và TenMon tương ứng với DeThiID đã chọn
            string query = "SELECT MonID FROM dethi WHERE DeThiID = @DeThiID";
            int monID = -1;
            string tenMon = string.Empty;

            using (SqlConnection connection = new SqlConnection(str))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    monID = Convert.ToInt32(dt.Rows[0]["MonID"]);
                }
            }
            // Kiểm tra nếu textBox_MonHocID không có giá trị hợp lệ
            if (string.IsNullOrWhiteSpace(textBox_MonID.Text))
            {
                MessageBox.Show("Vui lòng nhập Môn học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng lại nếu MonID chưa được nhập
            }

            // Kiểm tra nếu người dùng chưa nhập CauHoiID
            if (string.IsNullOrWhiteSpace(textBox_CauHoi2.Text) || !int.TryParse(textBox_CauHoi2.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập CauHoiID hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu các đáp án không trống
            if (string.IsNullOrWhiteSpace(textBox_DapanA.Text) || string.IsNullOrWhiteSpace(textBox_DapAnB.Text) ||
                string.IsNullOrWhiteSpace(textBox_DapAnC.Text) || string.IsNullOrWhiteSpace(textBox_DapAnD.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ các đáp án!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Lấy giá trị MonID từ TextBox

                // Lấy giá trị CauHoiID từ TextBox
                int cauHoiID = int.Parse(textBox_CauHoi2.Text);

                // Chuẩn bị câu lệnh INSERT với tham số
                using (SqlConnection connection = new SqlConnection(str)) // str là chuỗi kết nối tới CSDL
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO cauhoi (CauHoiID, MonID, NoiDungCauHoi, DapAnA, DapAnB, DapAnC, DapAnD, DapAnDung) " +
                                                       "VALUES (@CauHoiID, @MonID, @NoiDungCauHoi, @DapAnA, @DapAnB, @DapAnC, @DapAnD, @DapAnDung)", connection);

                    // Thêm tham số vào câu lệnh SQL
                    command.Parameters.AddWithValue("@CauHoiID", cauHoiID);
                    command.Parameters.AddWithValue("@MonID", monID);
                    command.Parameters.AddWithValue("@NoiDungCauHoi", textBox_CauHoi2.Text);
                    command.Parameters.AddWithValue("@DapAnA", textBox_DapanA.Text);
                    command.Parameters.AddWithValue("@DapAnB", textBox_DapAnB.Text);
                    command.Parameters.AddWithValue("@DapAnC", textBox_DapAnC.Text);
                    command.Parameters.AddWithValue("@DapAnD", textBox_DapAnD.Text);
                    command.Parameters.AddWithValue("@DapAnDung", textBox_DapAnDung.Text);

                    // Thực thi câu lệnh INSERT
                    int result = command.ExecuteNonQuery();

                    // Kiểm tra kết quả thực thi câu lệnh
                    if (result > 0)
                    {
                        MessageBox.Show("Thêm câu hỏi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        command = connection.CreateCommand();
                        command.CommandText = "select * from cauhoi where MonID = '" + monID + "'";
                        adapter.SelectCommand = command;
                        table.Clear();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                    else
                    {
                        MessageBox.Show("Thêm câu hỏi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có vấn đề trong quá trình thực hiện
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button_Luu_Click(object sender, EventArgs e)
        {
            if (comboBox_DeThiID.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn Đề Thi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedDeThiID = Convert.ToInt32(comboBox_DeThiID.SelectedValue);

            using (SqlConnection connection = new SqlConnection(str))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        int cauHoiID = Convert.ToInt32(row.Cells["CauHoiID"].Value);

                        // Kiểm tra tồn tại
                        command.CommandText = "SELECT COUNT(*) FROM chitietdethi WHERE DeThiID = @DeThiID AND CauHoiID = @CauHoiID";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                        command.Parameters.AddWithValue("@CauHoiID", cauHoiID);

                        int count = (int)command.ExecuteScalar();

                        if (count == 0) // Nếu chưa tồn tại
                        {
                            // Tạo ID duy nhất
                            command.CommandText = "SELECT ISNULL(MAX(ChiTietDeThiID), 0) + 1 FROM chitietdethi";
                            int nextChiTietDeThiID = (int)command.ExecuteScalar();

                            // Chèn dữ liệu
                            command.CommandText = "INSERT INTO chitietdethi (ChiTietDeThiID, DeThiID, CauHoiID) VALUES (@ChiTietDeThiID, @DeThiID, @CauHoiID)";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@ChiTietDeThiID", nextChiTietDeThiID);
                            command.Parameters.AddWithValue("@DeThiID", selectedDeThiID);
                            command.Parameters.AddWithValue("@CauHoiID", cauHoiID);

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            MessageBox.Show("Lưu đề thi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace HocSinh
{
    public partial class DeThiHS : Form
    {
        // Khai báo các biến toàn cục
        private SqlConnection connection;
        private SqlCommand command;
        private string connectionString = @"Data Source = ADMIN; Initial Catalog = BTL; Integrated Security = True; Connect Timeout = 30; Encrypt=True;TrustServerCertificate=True;";
        private Dictionary<int, string> userAnswers = new Dictionary<int, string>(); // Lưu câu trả lời của người dùng
        private List<int> checkedAnswers = new List<int>(); // Danh sách câu hỏi đã kiểm tra đáp án
        private int currentCauHoiID = 1; // ID của câu hỏi hiện tại
        private int correctAnswerCount = 0; // Số câu trả lời đúng
        private int deThiID;

        private int sinhVienID = 1; // ID sinh viên (giả sử đã được lấy từ hệ thống)
        private int khoaID = 1; // ID khoa
        private int giangVienID = 1; // ID giảng viên (nếu cần)
        private int ketquaID = 1;

        // Timer variables
        private int remainingTime = 30 * 60; // 30 phút (1800 giây)
        private DateTime startTime; // Start time for the exam
        private TimeSpan elapsedTime => DateTime.Now - startTime; // Elapsed time since the exam started


        // Hàm khởi tạo với ID đề thi
        public DeThiHS(int deThiID)
        {
            this.deThiID = deThiID;

            MessageBox.Show($"DeThiID nhận được: {deThiID}", "Debug");
            InitializeComponent();
            this.deThiID = deThiID;
            connection = new SqlConnection(connectionString);

            AssignQuestionButtonEvents();
            StartExam();
        }

        // Hàm khởi tạo mặc định
        public DeThiHS() : this(1) { }
      
        private void StartExam()
        {
            startTime = DateTime.Now;
            lblTimer.Text = TimeSpan.FromSeconds(remainingTime).ToString(@"mm\:ss"); 
            timer1.Start(); 
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                remainingTime--; // Giảm thời gian còn lại
                TimeSpan time = TimeSpan.FromSeconds(remainingTime);

                // Hiển thị thời gian theo định dạng phút:giây
                lblTimer.Text = time.ToString(@"mm\:ss");
            }
            else
            {
                timer1.Stop(); // Dừng bộ đếm thời gian
                MessageBox.Show("Hết thời gian làm bài.");
                SubmitExam();

            }
        }
        private void SubmitExam()
        {
            timer1.Stop();

            // Lưu câu trả lời hiện tại
            SaveCurrentAnswer();

            // Tính điểm
            double diemSo = CalculateScore();

            // Ghi lại thời gian làm bài
            DateTime thoiGianLam = DateTime.Now;

            // Lưu kết quả thi vào cơ sở dữ liệu
            SaveKetQuaThi(deThiID, sinhVienID, diemSo, thoiGianLam, khoaID, giangVienID);

            // Hiển thị kết quả
            MessageBox.Show($"Điểm của bạn là: {diemSo}\nKết quả đã được lưu.");
            this.Close();
        }


        // Khi kết thúc bài thi
        private void button8_Click(object sender, EventArgs e)
        {
            SubmitExam();
        }

        // Gán sự kiện cho các nút câu hỏi
        private void AssignQuestionButtonEvents()
        {
            for (int i = 1; i <= 10; i++)
            {
                Button btnCauHoi = Controls.Find("btnCauHoi" + i, true)[0] as Button;
                if (btnCauHoi != null)
                {
                    btnCauHoi.Click += CauHoiButton_Click;
                }
            }
        }

        // Hàm tải câu hỏi theo ID
        private void LoadCauHoi(int cauHoiID)
        {
            try
            {
                connection.Open();
                command = new SqlCommand(
                    "SELECT ch.NoiDungCauHoi, ch.DapAnA, ch.DapAnB, ch.DapAnC, ch.DapAnD " +
                    "FROM chitietdethi ctdt " +
                    "INNER JOIN CauHoi ch ON ctdt.CauHoiID = ch.CauHoiID " +
                    "WHERE ctdt.DeThiID = @DeThiID AND ch.CauHoiID = @CauHoiID", connection);

                command.Parameters.AddWithValue("@DeThiID", deThiID);
                command.Parameters.AddWithValue("@CauHoiID", cauHoiID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lblCauHoi.Text = reader["NoiDungCauHoi"].ToString();
                    rdbA.Text = reader["DapAnA"].ToString();
                    rdbB.Text = reader["DapAnB"].ToString();
                    rdbC.Text = reader["DapAnC"].ToString();
                    rdbD.Text = reader["DapAnD"].ToString();

                    if (userAnswers.ContainsKey(cauHoiID))
                    {
                        MarkSelectedAnswer(userAnswers[cauHoiID]);
                    }
                    else
                    {
                        ClearAnswerSelection();
                    }
                }
                else
                {
                    MessageBox.Show("Câu hỏi không tồn tại trong đề thi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        // Lưu câu trả lời hiện tại
        private void SaveCurrentAnswer()
        {
            if (rdbA.Checked) userAnswers[currentCauHoiID] = "A";
            if (rdbB.Checked) userAnswers[currentCauHoiID] = "B";
            if (rdbC.Checked) userAnswers[currentCauHoiID] = "C";
            if (rdbD.Checked) userAnswers[currentCauHoiID] = "D";

         
        }

       
        // Khi người dùng chọn một câu hỏi
        private void CauHoiButton_Click(object sender, EventArgs e)
        {
            SaveCurrentAnswer();

            Button clickedButton = (Button)sender;
            currentCauHoiID = Convert.ToInt32(clickedButton.Text);

            LoadCauHoi(currentCauHoiID);
        }

        // Kiểm tra đáp án đúng
       
        private bool CheckAnswer(int cauHoiID, string userAnswer)
        {
            try
            {
                connection.Open();
                command = new SqlCommand("SELECT ch.DapAnDung FROM cauhoi ch WHERE ch.CauHoiID = @CauHoiID", connection);
                command.Parameters.AddWithValue("@CauHoiID", cauHoiID);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string correctAnswer = reader["DapAnDung"].ToString();
                    return userAnswer == correctAnswer; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return false; // Trả về false nếu có lỗi
        }

        // Tính điểm dựa trên số câu đúng
        private double CalculateScore()
        {
            double scorePerQuestion = 1.0; // Mỗi câu đúng được 1 điểm
            correctAnswerCount = 0; // Đặt lại số câu đúng trước khi tính toán

            foreach (var answer in userAnswers)
            {
                if (CheckAnswer(answer.Key, answer.Value))
                {
                    correctAnswerCount++;
                }
            }

            return correctAnswerCount * scorePerQuestion; // Tổng điểm
        }




        // Khi người dùng chọn đáp án
        private void AnswerSelected(object sender, EventArgs e)
        {
            SaveCurrentAnswer();
            if (userAnswers.ContainsKey(currentCauHoiID))
            {
                CheckAnswer(currentCauHoiID, userAnswers[currentCauHoiID]);
            }
        }

        private void rdbA_CheckedChanged(object sender, EventArgs e) => AnswerSelected(sender, e);
        private void rdbB_CheckedChanged(object sender, EventArgs e) => AnswerSelected(sender, e);
        private void rdbC_CheckedChanged(object sender, EventArgs e) => AnswerSelected(sender, e);
        private void rdbD_CheckedChanged(object sender, EventArgs e) => AnswerSelected(sender, e);

        // Khi kết thúc bài thi
     
        // Save the exam result to the database
        private void SaveKetQuaThi(int deThiID, int sinhVienID, double diemSo, DateTime thoiGianLam, int khoaID, int giangVienID)
        {
            try
            {
                connection.Open();

                // Đếm số hàng trong bảng ketquathi
                string countQuery = "SELECT COUNT(*) FROM ketquathi";
                command = new SqlCommand(countQuery, connection);
                int currentCount = (int)command.ExecuteScalar(); // Lấy số lượng hàng hiện tại

                // Tăng KetQuaID lên 1
                int newKetQuaID = currentCount + 1;

                // Thực hiện chèn dữ liệu mới
                string insertQuery = "INSERT INTO ketquathi (KetQuaID, DeThiID, SinhVienID, DiemSo, ThoiGianLam, KhoaID, GiangVienID) " +
                                     "VALUES (@KetQuaID, @DeThiID, @SinhVienID, @DiemSo, @ThoiGianLam, @KhoaID, @GiangVienID)";

                command = new SqlCommand(insertQuery, connection);

                // Thêm các tham số
                command.Parameters.AddWithValue("@KetQuaID", newKetQuaID);
                command.Parameters.AddWithValue("@DeThiID", deThiID);
                command.Parameters.AddWithValue("@SinhVienID", sinhVienID);
                command.Parameters.AddWithValue("@DiemSo", diemSo);
                command.Parameters.AddWithValue("@ThoiGianLam", thoiGianLam);
                command.Parameters.AddWithValue("@KhoaID", khoaID);
                command.Parameters.AddWithValue("@GiangVienID", giangVienID);

                // Thực hiện câu lệnh
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu kết quả thi: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void ClearAnswerSelection()
        {
            rdbA.Checked = false;
            rdbB.Checked = false;
            rdbC.Checked = false;
            rdbD.Checked = false;
        }

        private void MarkSelectedAnswer(string answer)
        {
            rdbA.Checked = answer == "A";
            rdbB.Checked = answer == "B";
            rdbC.Checked = answer == "C";
            rdbD.Checked = answer == "D";
        }

        private void DeThiHS_Load(object sender, EventArgs e)
        {

        }
    }
}

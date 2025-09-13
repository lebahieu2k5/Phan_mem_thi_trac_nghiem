using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DangNhapForm
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public bool checkAccount(string ac)
        {
            return Regex.IsMatch(ac, @"^[a-zA-Z0-9]{6,24}$");
        }
        public bool CheckEmail(string em)
        {
            return Regex.IsMatch(em, @"^[a-zA-Z0-9_.]{3,20}@gmail.com(.vn|)$");
        }
        Modify modify = new Modify();
        private void button1_Click(object sender, EventArgs e)
        {
            string tentk = TenTaiKhoan.Text;
            string mk = MatKhau.Text;
            string xnmk = XNMatKhau.Text;
            string em = Email.Text;
            int loai = radioButton_GiaoVien.Checked ? 1 : 0;
            if(radioButton_GiaoVien.Checked && radioButton2.Checked || !radioButton_GiaoVien.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Vui lòng chọn 1 loại tài khoản!");
                return;
            }    
            if(!checkAccount(tentk))
            {
                MessageBox.Show("Vui long nhap ten tai khoan dai 6-24 ky tu, voi cac ky tu chu va so, chu hoa va chu thuong!");
                return;
            }
            if(!checkAccount(mk))
            {
                MessageBox.Show("Vui long nhap mat khau dai 3-20 ky tu, voi cac ky tu chu va so, chu hoa va chu thuong!");
                return;
            }
            if(xnmk != mk)
            {
                MessageBox.Show("Vui long xac nhan mat khau chinh xac");
                return ;
            }
            if(!(CheckEmail(em))) 
            {
                MessageBox.Show("Vui long nhap dung dinh dang email");
                return;
            }
            if(modify.TaiKhoans("Select * From taikhoan where Email = '" + em + "'" ).Count!=0)
            {
                MessageBox.Show("Email da duoc dky, Vui long dky email khac!");
                return;
            }
            try
            {
                string query = "Insert into taikhoan (TenTaiKhoan, MatKhau, Email, LoaiTaiKhoan) values ('" + tentk + "','" + mk + "','" + em + "','"+ loai + "')";
                modify.Command(query);
                if (MessageBox.Show("Dang ky thanh cong! Ban co muon dang nhap luon khong?","Thong Bao",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ten tai khoan da duoc dang ky!");
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TenTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void DangKy_Load(object sender, EventArgs e)
        {

        }

        private void radioButton_GiaoVien_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DangNhapForm
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
            label2.Text = "";
        }
        Modify modify = new Modify();

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            if(email.Trim() == "")
            {
                MessageBox.Show("Vui long nhap email dang ky!");
            }
            else
            {
                string query = "Select * From taikhoan where Email = '" + email + "'";
                if(modify.TaiKhoans(query).Count!=0)
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = "Mat Khau: " + modify.TaiKhoans(query)[0].MatKhau;
                }
                else
                {
                    label2.ForeColor = Color.Red;
                    label2.Text = "Email nay chua duoc dang ky";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

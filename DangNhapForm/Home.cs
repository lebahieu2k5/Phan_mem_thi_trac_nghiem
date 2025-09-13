using DangNhapForm;
using Nganhangcauhoi;
using QLGV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaoDeThi;

namespace BTN1
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_body.Controls.Add(childForm);
            panel_body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void panel_body_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            lblHT.Text = "Home";
        }

        private void btnDeThi_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DeThi1()); 
            lblHT.Text= "Đề Thi"; 
        }

        private void btnKND_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhoND());
            lblHT.Text = "Kho nội dung";
        }

        private void btnNH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NganHangCauHoi());
            lblHT.Text = "Ngân Hàng Câu Hỏi";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DeThi1());
            lblHT.Text = "Đề Thi";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhoND());
            lblHT.Text = "Kho nội dung";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenChildForm(new NganHangCauHoi());
            lblHT.Text = "Ngân Hàng Câu Hỏi";
        }

        private void button11_Click(object sender, EventArgs e)
        {
          ThongTinGV tt = new ThongTinGV();
            tt.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            lblHT.Text = "Home";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(btnSettings, new Point(0, btnSettings.Height));
        }

        private void thôngTinGiáoViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            ThongTinGV tt = new ThongTinGV();
            tt.ShowDialog(); 
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                FormDangNhap formDangNhap = new FormDangNhap();
                formDangNhap.ShowDialog();
            }
        }
    }
}

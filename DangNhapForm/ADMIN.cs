using QLGV;
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
    public partial class ADMIN : Form
    {
        public ADMIN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuanLyGiaoVien gv = new QuanLyGiaoVien();
            gv.ShowDialog(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            QuanLyHocSinh sv = new QuanLyHocSinh();
            sv.ShowDialog();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(btnSettings, new Point(0, btnSettings.Height));
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormDangNhap formDangNhap = new FormDangNhap();
            formDangNhap.ShowDialog();
        }
    }
}

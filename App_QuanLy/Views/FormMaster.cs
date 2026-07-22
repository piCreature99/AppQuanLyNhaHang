using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_QuanLy.BLL;

namespace App_QuanLy.Views
{
    public partial class FormMaster : Form
    {
        public string idAdmin;
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        public FormMaster()
        {
            InitializeComponent();
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            // Vừa mở app lên là hiện màn hình đăng nhập ngay
            HienThiFormDangNhap();
            //HienThiFormMain();
            //HienThiFormQuenMatKhau();
        }

        private void FormMaster_Closed(object sender, FormClosedEventArgs e)
        {
            if (idAdmin != null)
            {
                _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(idAdmin, "Đang nghỉ");
            }
        }

        public void HienThiFormDangNhap()
        {
            panelMasterContainer.Controls.Clear();

            FormDangNhap formDangNhap = new FormDangNhap(this);
            formDangNhap.TopLevel = false;
            formDangNhap.FormBorderStyle = FormBorderStyle.None;
            formDangNhap.Dock = DockStyle.Fill;

            Panel content = formDangNhap.Controls.Find("panelMain", true).FirstOrDefault() as Panel;
            //MessageBox.Show(content == null ? "" : "not null");
            if (content != null)
            {
                content.Location = new Point(0, 0);
                //MessageBox.Show("Hello");
                panelMasterContainer.Controls.Add(content); // Chỉ thêm cái Panel vào
            }
            else
            {
                panelMasterContainer.Controls.Add(formDangNhap); // Nếu không tìm thấy thì thêm cả cái Form
            }
        }

        public void HienThiFormMain()
        {
            panelMasterContainer.Controls.Clear();

            FormMain formMain = new FormMain(this);
            formMain.TopLevel = false;
            formMain.FormBorderStyle = FormBorderStyle.None;
            formMain.Dock = DockStyle.Fill;

            Panel content = formMain.Controls.Find("panelMainContainer", true).FirstOrDefault() as Panel;

            if (content != null)
            {
                content.Location = new Point(0, 0);
                //MessageBox.Show("Hello");
                panelMasterContainer.Controls.Add(content); // Chỉ thêm cái Panel vào
            }
            else
            {
                panelMasterContainer.Controls.Add(formMain); // Nếu không tìm thấy thì thêm cả cái Form
            }
        }

        public void HienThiFormQuenMatKhau()
        {
            panelMasterContainer.Controls.Clear();

            FormQuenMatKhau formQuenMatKhau = new FormQuenMatKhau(this);
            formQuenMatKhau.TopLevel = false;
            formQuenMatKhau.FormBorderStyle = FormBorderStyle.None;
            formQuenMatKhau.Dock = DockStyle.Fill;

            Panel content = formQuenMatKhau.Controls.Find("panelMain", true).FirstOrDefault() as Panel;

            if(content != null)
            {
                content.Location = new Point(0, 0);

                panelMasterContainer.Controls.Add(content);
            } else
            {
                panelMasterContainer.Controls.Add(formQuenMatKhau);
            }
        }
    }
}

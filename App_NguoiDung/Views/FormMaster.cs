using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_NguoiDung.BLL;
using App_NguoiDung.Models;

namespace App_NguoiDung.Views
{
    public partial class FormMaster : Form
    {
        public TaiKhoan TaiKhoanHienTai;
        public string _idNguoiDung = "";
        private readonly TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private Form activeForm = null;

        public FormMaster()
        {
            InitializeComponent();
        }

        private void FormMaster_Load(object sender, EventArgs e)
        {
            //HienThiFormMain();
            HienThiFormDangNhap();
        }
        private void FormMaster_Closed(object sender, FormClosedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_idNguoiDung))
            {
                _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(_idNguoiDung, "Đang nghỉ");
            }
        }

        public void OpenChildForm(Form childForm, string panelName)
        {
            // 1. Ẩn Panel ngay lập tức để người dùng không thấy cảnh "vẽ từng phần"
            var oldSize = panelMasterContainer.Size;
            panelMasterContainer.Size = new Size(0, 0);


            // 2. Thực hiện các thao tác load form (như cũ)
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMasterContainer.Controls.Clear();
            Panel content = childForm.Controls.Find(panelName, true).FirstOrDefault() as Panel;

            if (content != null)
            {
                content.Location = new Point(0, 0);
                //MessageBox.Show("Hello");
                panelMasterContainer.Controls.Add(content); // Chỉ thêm cái Panel vào
            }
            else
            {
                panelMasterContainer.Controls.Add(childForm); // Nếu không tìm thấy thì thêm cả cái Form
            }

            // 3. Đợi form con thực sự load xong (sự kiện Load của form con)
            childForm.Show();

            // Sau khi xong
            panelMasterContainer.Size = oldSize;
        }

        public void HienThiFormDangNhap()
        {
            panelMasterContainer.Controls.Clear();

            FormDangNhap formDangNhap = new FormDangNhap(this);
            formDangNhap.TopLevel = false;
            formDangNhap.FormBorderStyle = FormBorderStyle.None;
            formDangNhap.Dock = DockStyle.Fill;

            Panel content = formDangNhap.Controls.Find("panelMain", true).FirstOrDefault() as Panel;

            if (content != null)
            {
                content.Location = new Point(0, 0);
                panelMasterContainer.Controls.Add(content); 
            }
            else
            {
                panelMasterContainer.Controls.Add(formDangNhap);
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
                panelMasterContainer.Controls.Add(content);
            }
            else
            {
                panelMasterContainer.Controls.Add(formMain);
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

        public void HienThiFormDangKy()
        {
            panelMasterContainer.Controls.Clear();

            FormDangKy formDangKy = new FormDangKy(this);
            formDangKy.TopLevel = false;
            formDangKy.FormBorderStyle = FormBorderStyle.None;
            formDangKy.Dock = DockStyle.Fill;

            Panel content = formDangKy.Controls.Find("panelMain", true).FirstOrDefault() as Panel;

            if(content != null)
            {
                content.Location = new Point(0, 0);
                panelMasterContainer.Controls.Add(content);
            } else
            {
                panelMasterContainer.Controls.Add(formDangKy);
            }
        }
    }
}

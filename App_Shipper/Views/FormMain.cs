using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_Shipper.BLL;
using App_Shipper.Helpers;

namespace App_Shipper.Views
{
    public partial class FormMain : Form
    {
        private Form activeForm;
        private FormMaster _master;
        private readonly HelperFunctions _helperFuncs = new HelperFunctions();
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();

        public FormMain(FormMaster master)
        {
            InitializeComponent();
            _master = master;
        }

        private void pbTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormXemTaiKhoan(_master));
            OnMenuButtonClicked(sender, e);
        }

        private void pbHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyHoaDon(_master));
            OnMenuButtonClicked(sender, e);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_master._idShipper))
            {
                //MessageBox.Show(_master._idShipper);
                _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(_master._idShipper, "Đang nghỉ");
            }
            _master.HienThiFormDangNhap();
        }

        private void OpenChildForm(Form childForm)
        {
            //var oldSize = panelMain.Size;
            //panelMain.Size = new Size(0, 0);

            if (activeForm != null) activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMain.Controls.Clear();
            Panel content = childForm.Controls.Find("panelMain", true).FirstOrDefault() as Panel;

            if (content != null)
            {
                content.Location = new Point(0, 0);
                panelMain.Controls.Add(content);
            }
            else
            {
                panelMain.Controls.Add(childForm);
            }
            childForm.Show();
            //panelMain.Size = oldSize;
        }
        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            ResetAllButtons();

            PictureBox clickedPb = sender as PictureBox;
            if (clickedPb == null) return;

            string imgPath = Path.Combine(Application.StartupPath, "Images/Buttons/Storage", clickedPb.Name.ToLower() + "_selected.png");

            if (File.Exists(imgPath))
            {
                if (clickedPb.Image != null) // dispose đúng cách
                {
                    clickedPb.Image.Dispose();
                    clickedPb.Image = null; // xóa liên kết cũ để ngăn con trỏ ma
                }
                clickedPb.Image = _helperFuncs.LoadImage(imgPath);
            }
        }

        private void ResetAllButtons()
        {
            if (pbTaiKhoan.Image != null) // dispose đúng cách
            {
                pbTaiKhoan.Image.Dispose();
                pbTaiKhoan.Image = null; // xóa liên kết cũ để ngăn con trỏ ma
            }
            if (pbHoaDon.Image != null) // dispose đúng cách
            {
                pbHoaDon.Image.Dispose();
                pbHoaDon.Image = null; // xóa liên kết cũ để ngăn con trỏ ma
            }


            pbTaiKhoan.Image = _helperFuncs.LoadImage(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbtaikhoan_normal.png"));
            pbHoaDon.Image = _helperFuncs.LoadImage(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbhoadon_normal.png"));

        }

        
    }
}

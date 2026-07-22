using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_QuanLy.BLL;
using App_QuanLy.Models;

namespace App_QuanLy.Views
{
    public partial class FormDangNhap : Form
    {
        // KHỞI TẠO ĐỐI TƯỢNG THỰC THỂ Ở ĐÂY
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        // Khai báo một Callback để liên lạc ngược lại với Form Khung
        private readonly Action _loginSuccessCallback;

        private FormMaster _master;
        // Ép Form Khung phải truyền hành động xử lý vào khi khởi tạo
        public FormDangNhap(FormMaster master)
        {
            InitializeComponent();
            _master = master;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            try
            {
                // Gọi BLL xử lý
                TaiKhoan tk = _taiKhoanBLL.DangNhap(username, password);

                if (tk != null)
                {
                    _master.idAdmin = tk.MaTaiKhoan;
                    if (_master.idAdmin != null)
                    {
                        _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(_master.idAdmin, "Sẵn sàng");
                    }
                    // Đăng nhập thành công với quyền Admin
                    MessageBox.Show($"Chào mừng Quản lý {tk.HoTen} quay trở lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _master.HienThiFormMain();
                }
                else
                {
                    // Tài khoản hoặc mật khẩu không khớp dưới DB
                    MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không chính xác!", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Bắt đúng trường hợp tài khoản có thật nhưng sai vai trò (VaiTro != 'admin')
                MessageBox.Show(ex.Message, "Từ Chối Truy Cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello");
            _master.HienThiFormQuenMatKhau();
        }
    }
}

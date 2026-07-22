using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_NguoiDung.BLL;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace App_NguoiDung.Views
{
    public partial class FormDangNhap : Form
    {
        private FormMaster _master;
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        public FormDangNhap(FormMaster formMaster)
        {
            InitializeComponent();
            _master = formMaster;
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            _master.OpenChildForm(new FormQuenMatKhau(_master), "panelMain");
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện (bạn nhớ đổi lại đúng tên các TextBox của bạn nhé)
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;

            // 2. Khởi tạo đối tượng tầng BLL
            App_NguoiDung.BLL.TaiKhoanBLL taiKhoanBLL = new App_NguoiDung.BLL.TaiKhoanBLL();

            try
            {
                // 3. Gọi BLL xử lý kiểm tra (hàm này đã có Trim và ToLower tự động như ta bàn ở trên)
                string loginResultId = taiKhoanBLL.KiemTraDangNhap(username, password);
                //string loginResultId = "NV005";

                if (!string.IsNullOrEmpty(loginResultId))
                {
                    // ĐĂNG NHẬP THÀNH CÔNG: 
                    // Gán ID thật từ database trả về thay cho chữ "placeholder"
                    _master._idNguoiDung = loginResultId;
                    _master.TaiKhoanHienTai = taiKhoanBLL.LayThongTinTaiKhoan(loginResultId);
                    _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(_master._idNguoiDung, "Sẵn sàng");
                    _master.OpenChildForm(new FormMain(_master), "panelMainContainer");
                }
                else
                {
                    // ĐĂNG NHẬP THẤT BẠI
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác, hoặc tài khoản đã bị khóa!",
                                    "Đăng nhập thất bại",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // Focus lại ô mật khẩu để người dùng nhập lại cho tiện
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Hiển thị các lỗi hệ thống hoặc lỗi do BLL quăng ra (ví dụ: rỗng chuỗi đầu vào)
                MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            _master.OpenChildForm(new FormDangKy(_master), "panelMain");
        }
    }
}

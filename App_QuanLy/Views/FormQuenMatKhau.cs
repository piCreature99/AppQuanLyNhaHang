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
    public partial class FormQuenMatKhau : Form
    {
        // Triệu hồi lớp xử lý nghiệp vụ ở tầng BLL
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private FormMaster _master;

        public FormQuenMatKhau(FormMaster master)
        {
            InitializeComponent();
            _master = master;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            // 1. Thu thập dữ liệu thô từ các ô nhập liệu trên giao diện
            string sdt = txtSDT.Text;
            string mkMoi = txtMatKhau.Text;
            string nhapLaiMk = txtNhapLaiMatKhau.Text;

            try
            {
                // 2. Gửi dữ liệu xuống tầng "đầu não" BLL để kiểm tra logic và cập nhật DB
                bool thongBaoLoi = _taiKhoanBLL.XuLyQuenMatKhau(sdt, mkMoi, nhapLaiMk);

                // 3. Tiếp nhận phản hồi từ BLL để hiển thị lên màn hình cho người dùng
                if (thongBaoLoi)
                {
                    // Nếu chuỗi trả về rỗng -> Không có lỗi -> Thành công rực rỡ
                    MessageBox.Show("Đặt lại mật khẩu thành công! Bạn có thể sử dụng mật khẩu mới này để đăng nhập hệ thống.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _master.HienThiFormDangNhap();
                    // Đóng Form lấy lại mật khẩu để quay về Form Đăng nhập
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Lấy mật khẩu thất bại! Không tìm thấy tài khoản hợp lệ trong hệ thống.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    XoaTrongOMatKhau();
                }
            }
            catch (ArgumentException ex)
            {
                // Bắt chính xác các lỗi nghiệp vụ dữ liệu đầu vào (Độ dài, trống, sai định dạng, khoảng trắng...)
                MessageBox.Show(ex.Message, "Lỗi nhập dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Bắt các lỗi hệ thống nghiêm trọng phát sinh từ DAL hoặc Database
                MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hàm phụ giúp dọn dẹp giao diện khi người dùng nhập sai
        /// </summary>
        private void XoaTrongOMatKhau()
        {
            txtMatKhau.Clear();
            txtNhapLaiMatKhau.Clear();
            txtMatKhau.Focus(); // Đặt con trỏ chuột sẵn vào ô mật khẩu mới
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            
            _master.HienThiFormDangNhap();
        }
    }
}

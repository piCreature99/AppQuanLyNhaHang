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
    public partial class FormDangKy : Form
    {
        private FormMaster _formMaster;
        public FormDangKy(FormMaster master)
        {
            InitializeComponent();
            _formMaster = master;

        }
        public void FormDangKy_Load(object sender, EventArgs e)
        {
            NapComboBoxGioiTinh();
        }
        private void NapComboBoxGioiTinh()
        {
            // Tạo mảng gồm một chuỗi rỗng (để trống), "Nam", và "Nữ"
            string[] danhSachGioiTinh = new string[] { "", "Nam", "Nữ" };

            // Nạp mảng này vào ComboBox
            cboGioiTinh.DataSource = danhSachGioiTinh;

            // Đặt kiểu hiển thị là DropDownList để người dùng chỉ được chọn, không tự gõ bậy chữ khác vào ô này
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnQuayLaiDangNhap_Click(object sender, EventArgs e)
        {
            _formMaster.OpenChildForm(new FormDangNhap(_formMaster), "panelMain");
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            TaiKhoan taiKhoanDangKy = new TaiKhoan
            {
                TenDangNhap = txtTaiKhoan.Text,
                MatKhau = txtMatKhau.Text,
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSDT.Text,
                DiaChi = txtDiaChi.Text,
                GioiTinh = cboGioiTinh.Text,
            };

            // 2. Khởi tạo BLL để xử lý nghiệp vụ
            TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

            try
            {
                // 3. Gọi hàm cập nhật của BLL
                bool ketQua = taiKhoanBLL.DangKyTaiKhoanNguoiDung(taiKhoanDangKy, txtNhapLaiMatKhau.Text);

                if (ketQua)
                {
                    //MessageBox.Show(_duongDanAnhDangChonDeCopy);
                    MessageBox.Show("Đăng ký tài khoản thành công!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _formMaster.OpenChildForm(new FormDangNhap(_formMaster), "panelMain");
                    // Tùy chọn: Gọi lại hàm nạp dữ liệu để làm mới giao diện nếu cần
                    // LoadThongTinTaiKhoan(taiKhoanCapNhat.MaTaiKhoan);
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_Shipper.BLL;
using App_Shipper.Helpers;
using App_Shipper.Models;

namespace App_Shipper.Views
{
    public partial class FormXemTaiKhoan : Form
    {
        private readonly TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private string _idShipper;
        private HelperFunctions _helperFuncs = new HelperFunctions();
        private string _tenAnhDangChon;
        private string _duongDanAnhDangChonDeCopy;
        private FormMaster _master;

        public FormXemTaiKhoan(FormMaster master)
        {
            InitializeComponent();
            _idShipper = master._idShipper;
            _master = master;
        }

        public void FormShipperTaiKhoan_Load(object sender, EventArgs e)
        {
            _helperFuncs.DefaultImgInitializer();
            NapAnhDaiDien("default", "shipper");
            // Kích hoạt hàm tải dữ liệu ngay khi Form vừa được load lên
            LoadThongTinTaiKhoan(_idShipper);

            // Khởi tạo ảnh mặc đinh
        }
        

        private void NapAnhDaiDien(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (pbAnhDaiDien.Image != null)
                {
                    pbAnhDaiDien.Image.Dispose();
                    pbAnhDaiDien.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    pbAnhDaiDien.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhDangChon = tenAnh;
                    _duongDanAnhDangChonDeCopy = fullPath;
                    //MessageBox.Show(tenAnh);
                    //MessageBox.Show(_duongDanAnhDangChonDeCopy);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    pbAnhDaiDien.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                pbAnhDaiDien.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }

        private void CapNhatAnhDaiDien()
        {
            string folderDich = Path.Combine(Application.StartupPath, "Images\\Shippers");
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }
            string tenFileAnh = txtTaiKhoan.Text.Trim() + Path.GetExtension(_duongDanAnhDangChonDeCopy);
            string duongDanAnhShipper = Path.Combine(folderDich, tenFileAnh);

            File.Copy(_duongDanAnhDangChonDeCopy, duongDanAnhShipper, true);
        }

        private void LoadThongTinTaiKhoan(string maTaiKhoan)
        {
            try
            {
                // 1. Gọi BLL để lấy thông tin tài khoản (trả về đối tượng TaiKhoan hoặc null)
                var taiKhoan = _taiKhoanBLL.LayThongTinTaiKhoan(maTaiKhoan);

                // 2. Nếu tìm thấy dữ liệu, tiến hành gán vào các Textbox tương ứng
                if (taiKhoan != null)
                {
                    txtTaiKhoan.Text = taiKhoan.TenDangNhap;
                    txtMatKhau.Text = taiKhoan.MatKhau;
                    txtHoTen.Text = taiKhoan.HoTen;
                    txtSDT.Text = taiKhoan.SoDienThoai;
                    txtDiaChi.Text = taiKhoan.DiaChi;
                    txtGioiTinh.Text = taiKhoan.GioiTinh;
                    _tenAnhDangChon = taiKhoan.HinhAnh;

                    NapAnhDaiDien(taiKhoan.HinhAnh, "shipper");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin của tài khoản này trong hệ thống!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Bắt các lỗi phát sinh từ tầng BLL/DAL quăng lên nếu có
                MessageBox.Show("Lỗi khi tải thông tin tài khoản: " + ex.Message,
                                "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_duongDanAnhDangChonDeCopy);
            string folderDich = _helperFuncs.KhoiTaoFolderDichTheoVaiTro("shipper");
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }
            string tenFileAnh = txtTaiKhoan.Text.Trim() + Path.GetExtension(_duongDanAnhDangChonDeCopy);
            string duongDanAnhShipper = Path.Combine(folderDich, tenFileAnh);
            // 1. Thu thập dữ liệu và đóng gói vào đối tượng TaiKhoan
            // Lưu ý: Đổi lại đúng tên các điều khiển (TextBox/ComboBox) trên giao diện của bạn
            TaiKhoan taiKhoanCapNhat = new TaiKhoan
            {
                MaTaiKhoan = _idShipper, // Khóa chính bắt buộc để tìm đúng dòng cần sửa
                TenDangNhap = txtTaiKhoan.Text,
                MatKhau = txtMatKhau.Text,
                HoTen = txtHoTen.Text,
                SoDienThoai = txtSDT.Text,
                DiaChi = txtDiaChi.Text,
                GioiTinh = txtGioiTinh.Text,

                // Giả sử tên file ảnh mới (hoặc giữ nguyên file cũ) được bạn lưu ở biến _tenFileAnhMoi
                HinhAnh = tenFileAnh
            };

            // 2. Khởi tạo BLL để xử lý nghiệp vụ
            TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();

            try
            {
                // 3. Gọi hàm cập nhật của BLL
                bool ketQua = taiKhoanBLL.CapNhatThongTinTaiKhoan(taiKhoanCapNhat);

                if (ketQua)
                {
                    if (_duongDanAnhDangChonDeCopy != duongDanAnhShipper) File.Copy(_duongDanAnhDangChonDeCopy, duongDanAnhShipper, true);
                    _master.TaiKhoanHienTai = taiKhoanBLL.LayThongTinTaiKhoan(_master._idShipper);
                    //MessageBox.Show(_duongDanAnhDangChonDeCopy);
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tùy chọn: Gọi lại hàm nạp dữ liệu để làm mới giao diện nếu cần
                    // LoadThongTinTaiKhoan(taiKhoanCapNhat.MaTaiKhoan);
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Không tìm thấy tài khoản hợp lệ trong hệ thống.",
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

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _duongDanAnhDangChonDeCopy = ofd.FileName; // Lấy đường dẫn file ảnh
                    if (pbAnhDaiDien.Image != null)
                    {
                        pbAnhDaiDien.Image.Dispose();
                        pbAnhDaiDien.Image = null;
                    }
                    pbAnhDaiDien.Image = _helperFuncs.LoadImage(_duongDanAnhDangChonDeCopy);
                }
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            if (pbAnhDaiDien.Image != null)
            {
                pbAnhDaiDien.Image.Dispose();
                pbAnhDaiDien.Image = null;
            }


            string folderPath = Path.Combine(Application.StartupPath, "Images", "Shippers");
            string defaultPath = Path.Combine(folderPath, "default.png");
            _tenAnhDangChon = "default.png";
            _duongDanAnhDangChonDeCopy = defaultPath;

            pbAnhDaiDien.Image = _helperFuncs.LoadImage(defaultPath);
        }

        private void btnNgungHoatDong_Click(object sender, EventArgs e)
        {
            string tieuDe = "XÁC NHẬN NGỪNG HOẠT ĐỘNG TÀI KHOẢN";
            string noiDungCanhBao = "CẢNH BÁO:\n\n" +
                                    "- Hành động này cần sự can thiệp của Quản lý nếu muốn tài khoản hoạt động trở lại. Vui lòng liên hệ số 0928335825\n" +
                                    "- Tài khoản sẽ có hiệu lực chờ trong vòng 30 NGÀY trước khi bị xóa vĩnh viễn.\n\n" +
                                    "Bạn có chắc chắn muốn tiếp tục ngừng hoạt động tài khoản này không?";

            // 2. Hiển thị hộp thoại Yes/No
            DialogResult luaChon = MessageBox.Show(noiDungCanhBao, tieuDe, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (luaChon == DialogResult.No) return;

            try
            {
                bool ketQua = _taiKhoanBLL.NgungHoatDongShipper(_idShipper);

                if (ketQua)
                {
                    MessageBox.Show("Ngưng hoạt động tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _master.HienThiFormDangNhap();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }
    }
}

using System.Data;
using System.Text.RegularExpressions;
using App_NguoiDung.DAL;
using App_NguoiDung.Helpers;
using App_NguoiDung.Models;

namespace App_NguoiDung.BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _taiKhoanDAL = new TaiKhoanDAL();
        private HelperFunctions _helperFuncs = new HelperFunctions();

        public TaiKhoan LayThongTinTaiKhoan(string maTaiKhoan)
        {
            if (string.IsNullOrWhiteSpace(maTaiKhoan)) return null;

            try
            {
                // Đổi tên gọi dưới DAL cho khớp với hàm bạn vừa sửa
                return _taiKhoanDAL.GetTaiKhoanDTOById(maTaiKhoan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng BLL: " + ex.Message);
            }
        }

        public string KiemTraDangNhap(string username, string password)
        {
            // 1. Kiểm tra nghiệp vụ cơ bản: Không được để trống dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Tên tài khoản hoặc mật khẩu không được để trống!"); // Hoặc bạn có thể throw một Exception thông báo cụ thể
            }

            try
            {
                // 2. Gọi xuống DAL để xác thực và nhận về MaTaiKhoan (nếu thành công)
                string maTaiKhoan = _taiKhoanDAL.GetMaTaiKhoanByCredentials(username.Trim(), password);

                return maTaiKhoan;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng BLL khi xử lý đăng nhập: " + ex.Message);
            }
        }

        public bool DangKyTaiKhoanNguoiDung(TaiKhoan taiKhoan, string nhapLaiMatKhau)
        {

            _helperFuncs.RangBuocNghiepVuThongTinDangKy(taiKhoan);

            return _taiKhoanDAL.DangKyTaiKhoanNguoiDung(taiKhoan);
        }

        public bool CapNhatThongTinTaiKhoan(TaiKhoan taiKhoan)
        {

            _helperFuncs.RangBuocNghiepVuThongTinCapNhat(taiKhoan);

            return _taiKhoanDAL.UpdateTaiKhoanDTO(taiKhoan);
        }

        public bool XuLyQuenMatKhau(string soDienThoai, string matKhauMoi, string nhapLaiMatKhau)
        {
            _helperFuncs.RangBuocNghiepVuLayLaiMatKhau(soDienThoai, matKhauMoi, nhapLaiMatKhau);
            
            bool ketQuaUpdate = _taiKhoanDAL.CapNhatMatKhauTheoSDT(soDienThoai, matKhauMoi);

            return ketQuaUpdate;
        }

        public bool NgungHoatDongNguoiDung(string maTaiKhoan)
        {
            // Kiểm tra mã tài khoản không được rỗng
            if (string.IsNullOrWhiteSpace(maTaiKhoan))
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ để cập nhật trạng thái!");
            }

            try
            {
                // Gọi xuống DAL thực thi
                return _taiKhoanDAL.UpdateTrangThaiLamViec(maTaiKhoan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng BLL khi xử lý trạng thái: " + ex.Message);
            }
        }

        public bool KichHoatTrangThaiTaiKhoan(string maTaiKhoan, string trangThai)
        {
            if (string.IsNullOrEmpty(maTaiKhoan))
                throw new ArgumentException("Mã tài khoản không được trống");
            return _taiKhoanDAL.KichHoatTrangThaiTaiKhoan(maTaiKhoan, trangThai);
        }

    }
}
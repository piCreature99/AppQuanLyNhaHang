using System.Data;
using System.Text.RegularExpressions;
using App_QuanLy.DAL;
using App_QuanLy.Helpers;
using App_QuanLy.Models;

namespace App_QuanLy.BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _taiKhoanDAL = new TaiKhoanDAL();
        private HelperFunctions _helperFuncs = new HelperFunctions();

        public TaiKhoan DangNhap(string username, string password)
        {
            // 1. Kiểm tra nghiệp vụ cơ bản trước khi xuống DB
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            // 2. Gọi xuống DAL để kiểm tra sự tồn tại trong DB
            TaiKhoan tk = _taiKhoanDAL.KiemTraDangNhap(username.Trim(), password);

            // 3. Nếu tìm thấy tài khoản, kiểm tra xem có quyền truy cập App Quản Lý không
            if (tk != null)
            {
                // Chỉ cho phép vai trò 'admin' đăng nhập vào hệ thống này
                if (tk.VaiTro.ToLower() == "admin")
                {
                    return tk; // Hợp lệ, trả về thực thể admin
                }
                else
                {
                    // Nếu là khách hàng hoặc shipper cố tình vào app quản lý, bắn lỗi chặn lại
                    throw new UnauthorizedAccessException("Tài khoản của bạn không có quyền truy cập vào ứng dụng này!");
                }
            }

            // Trả về null nếu sai tên đăng nhập hoặc mật khẩu
            return null;
        }

        public bool DangKy(TaiKhoan tk, string xacNhanPassword)
        {
            // 1. RÀNG BUỘC SỰ TỒN TẠI: Kiểm tra các trường bắt buộc không được để trống
            if (string.IsNullOrWhiteSpace(tk.TenDangNhap))
                throw new ArgumentException("Tên đăng nhập không được để trống!");

            if (string.IsNullOrWhiteSpace(tk.MatKhau)) // Kiểm tra trường mật khẩu nằm ngay trong Model
                throw new ArgumentException("Mật khẩu không được để trống!");

            if (string.IsNullOrWhiteSpace(tk.HoTen))
                throw new ArgumentException("Họ và tên không được để trống!");

            // Chuẩn hóa dữ liệu: Cắt bỏ các khoảng trắng thừa ở hai đầu chuỗi (Sanitization)
            tk.TenDangNhap = tk.TenDangNhap.Trim();
            tk.HoTen = tk.HoTen.Trim();
            tk.SoDienThoai = tk.SoDienThoai?.Trim() ?? "";

            // 2. RÀNG BUỘC SO SÁNH LOGIC: Mật khẩu nhập vào phải khớp với ô xác nhận trên giao diện
            if (tk.MatKhau != xacNhanPassword)
                throw new ArgumentException("Mật khẩu xác nhận không trùng khớp!");

            // 3. RÀNG BUỘC ĐỘ DÀI: Đảm bảo độ an toàn của mật khẩu và độ dài lưu trữ của DB
            if (tk.TenDangNhap.Length < 4 || tk.TenDangNhap.Length > 50)
                throw new ArgumentException("Tên đăng nhập phải chứa từ 4 đến 50 ký tự!");

            if (tk.MatKhau.Length < 6 || tk.MatKhau.Length > 255)
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");

            if (tk.HoTen.Length < 2 || tk.MatKhau.Length > 100)
                throw new ArgumentException("Họ tên phải chứa từ 2 đến 50 ký tự!");

            // 4. RÀNG BUỘC ĐỊNH DẠNG (Regex): Bảo vệ hệ thống khỏi các ký tự lạ hoặc khoảng trắng ở giữa
            // Tên đăng nhập chỉ chấp nhận chữ không dấu, số và dấu gạch dưới
            if (!Regex.IsMatch(tk.TenDangNhap, "^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Tên đăng nhập chứa ký tự không hợp lệ (chỉ chấp nhận chữ, số và dấu '_')!");

            // Định dạng số điện thoại chuẩn Việt Nam (nếu người dùng có điền)
            if (!string.IsNullOrEmpty(tk.SoDienThoai) && !Regex.IsMatch(tk.SoDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
                throw new ArgumentException("Số điện thoại không đúng định dạng hợp lệ!");

            // 5. CHUYỂN TIẾP XUỐNG DAL: Đẩy nguyên si thực thể 'tk' khi mọi bộ lọc đã hợp lệ
            return _taiKhoanDAL.ThemTaiKhoan(tk);
        }

        public bool ThemTaiKhoan(TaiKhoan tk)
        {
            _helperFuncs.RangBuocNghiepVuThongTinDangKy(tk);

            return _taiKhoanDAL.ThemTaiKhoan(tk);
        }

        public bool SuaTaiKhoan(TaiKhoan tk)
        {
            _helperFuncs.RangBuocNghiepVuThongTinCapNhat(tk);

            return _taiKhoanDAL.SuaTaiKhoan(tk);
        }

        public bool XuLyQuenMatKhau(string soDienThoai, string matKhauMoi, string nhapLaiMatKhau)
        {
            _helperFuncs.RangBuocNghiepVuLayLaiMatKhau(soDienThoai, matKhauMoi, nhapLaiMatKhau);

            // 2. Gọi thẳng lệnh UPDATE dưới DAL. 
            // Nếu SDT sai hoặc không tồn tại, rowsAffected sẽ bằng 0 -> Hàm trả về false.
            bool ketQuaUpdate = _taiKhoanDAL.CapNhatMatKhauTheoSDT(soDienThoai, matKhauMoi);
            // Trả về thông báo này vì nếu không update được dòng nào, chứng tỏ SDT không có trong hệ thống
            return ketQuaUpdate;
        }

        public DataTable LayDanhSachShipper(string tuKhoa, string trangThai)
        {
            return _taiKhoanDAL.LayDanhSachShipper(tuKhoa ?? "", trangThai ?? "");
        }

        public DataTable PollDanhSachTaiKhoan()
        {
            return _taiKhoanDAL.PollDanhSachTaiKhoan();
        }

        public DataTable PollDanhSachShipperSS()
        {
            return _taiKhoanDAL.PollDanhSachShipperSS();
        }

        public bool CapNhatTrangThaiHoatDong(string maTaiKhoan, bool trangThaiMoi)
        {
            return _taiKhoanDAL.CapNhatTrangThaiHoatDong(maTaiKhoan, trangThaiMoi);
        }

        public int LaySoLuongTaiKhoanAdmin()
        {
            return _taiKhoanDAL.LaySoLuongTaiKhoanAdminHoatDong();
        }

        public bool XoaTaiKhoan(string maTaiKhoan, string vaiTro)
        {
            if (string.IsNullOrEmpty(maTaiKhoan) || string.IsNullOrEmpty(vaiTro))
                throw new ArgumentException("thông tin tài khoản không hợp lệ hoặc chưa chọn tài khoản");

            if (vaiTro == "admin" && LaySoLuongTaiKhoanAdmin() <= 1)
            {
                throw new ArgumentException("Phải có ít nhất 1 tài khoản admin hoạt động");
            }

            return _taiKhoanDAL.XoaTaiKhoan(maTaiKhoan, vaiTro);

        }

        public bool KichHoatTrangThaiTaiKhoan(string maTaiKhoan, string trangThai)
        {
            if (string.IsNullOrEmpty(maTaiKhoan))
                throw new ArgumentException("Mã tài khoản không được trống");
            return _taiKhoanDAL.KichHoatTrangThaiTaiKhoan(maTaiKhoan, trangThai);
        }
    }
}
using System.Data;
using System.Text.RegularExpressions;
using App_Shipper.DAL;
using App_Shipper.Data;
using App_Shipper.Models;

namespace App_Shipper.BLL
{
    public class TaiKhoanBLL
    {
        private TaiKhoanDAL _taiKhoanDAL = new TaiKhoanDAL();

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

        public bool DangKyTaiKhoanShipper(TaiKhoan taiKhoan, string nhapLaiMatKhau)
        {
            // 1. CHUẨN HÓA DỮ LIỆU (Sanitization - Đưa lên trước để lọc chính xác)
            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.Trim().ToLower();
            taiKhoan.HoTen = taiKhoan.HoTen.Trim();
            taiKhoan.SoDienThoai = taiKhoan.SoDienThoai?.Trim() ?? "";
            if (taiKhoan.DiaChi != null) taiKhoan.DiaChi = taiKhoan.DiaChi.Trim();
            
            // 2. RÀNG BUỘC SỰ TỒN TẠI (Kiểm tra null/rỗng cơ bản)
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan), "Dữ liệu tài khoản không được để null!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }
            if (taiKhoan.MatKhau != nhapLaiMatKhau)
            {
                throw new ArgumentException("Mật khẩu mới và Mật khẩu nhập lại không khớp nhau!");
            }
            
            if (string.IsNullOrWhiteSpace(taiKhoan.HoTen))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.DiaChi))
            {
                throw new ArgumentException("Địa chỉ giao hàng không được để trống!");
            }

            taiKhoan.DiaChi = taiKhoan.DiaChi.Trim();

            string diaChiChuan = taiKhoan.DiaChi;


            // 3. RÀNG BUỘC ĐỘ DÀI
            if (taiKhoan.TenDangNhap.Length < 4 || taiKhoan.TenDangNhap.Length > 50)
            {
                throw new ArgumentException("Tên đăng nhập phải chứa từ 4 đến 50 ký tự!");
            }
            if (taiKhoan.MatKhau.Length < 6 || taiKhoan.MatKhau.Length > 255)
            {
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            }
            if (taiKhoan.HoTen.Length < 2 || taiKhoan.HoTen.Length > 100)
            {
                throw new ArgumentException("Họ tên phải chứa từ 2 đến 100 ký tự!");
            }

            // 4. RÀNG BUỘC ĐỊNH DẠNG VÀ CHẶN KHOẢNG TRẮNG Ở GIỮA (Dùng Regex như hàm đăng ký)
            // Regex này tự động cấm luôn khoảng trắng ở giữa tên đăng nhập
            if (!Regex.IsMatch(taiKhoan.TenDangNhap, "^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Tên đăng nhập chứa ký tự không hợp lệ (chỉ chấp nhận chữ cái không dấu, số và dấu '_')!");
            }

            // Định dạng số điện thoại chuẩn Việt Nam (nếu người dùng có điền)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai) && !Regex.IsMatch(taiKhoan.SoDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
            {
                throw new ArgumentException("Số điện thoại không đúng định dạng hợp lệ!");
            }

            // Kiểm tra trùng Số điện thoại chủ động
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai))
            {
                if (_taiKhoanDAL.KiemTraTrungSoDienThoai(taiKhoan.SoDienThoai))
                    throw new ArgumentException("Số điện thoại này đã tồn tại trong hệ thống!");
            }

            if (_taiKhoanDAL.KiemTraTrungTenDangNhap(taiKhoan.TenDangNhap))
                throw new ArgumentException("Tên đăng nhập này đã được sử dụng!");

            // 5. GỌI XUỐNG TẦNG DAL THỰC THI
            try
            {
                return _taiKhoanDAL.DangKyTaiKhoanShipper(taiKhoan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng BLL khi xử lý cập nhật dữ liệu: " + ex.Message);
            }
        }

        public bool CapNhatThongTinTaiKhoan(TaiKhoan taiKhoan)
        {
            // 1. RÀNG BUỘC SỰ TỒN TẠI (Kiểm tra null/rỗng cơ bản)
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan), "Dữ liệu tài khoản không được để null!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.MaTaiKhoan))
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ!", nameof(taiKhoan.MaTaiKhoan));
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.HoTen))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }

            // 2. CHUẨN HÓA DỮ LIỆU (Sanitization - Đưa lên trước để lọc chính xác)
            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.Trim().ToLower();
            taiKhoan.HoTen = taiKhoan.HoTen.Trim();
            taiKhoan.SoDienThoai = taiKhoan.SoDienThoai?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(taiKhoan.DiaChi))
            {
                throw new ArgumentException("Địa chỉ giao hàng không được để trống!");
            }
            taiKhoan.DiaChi = taiKhoan.DiaChi.Trim();

            string diaChiChuan = taiKhoan.DiaChi;


            // 3. RÀNG BUỘC ĐỘ DÀI
            if (diaChiChuan.Length < 10)
            {
                throw new ArgumentException("Địa chỉ giao hàng quá ngắn! Vui lòng nhập chi tiết (Số nhà, tên đường, phường/xã...).");
            }
            if (diaChiChuan.Length > 255)
            {
                throw new ArgumentException("Địa chỉ giao hàng quá dài! Vui lòng nhập ngắn gọn dưới 255 ký tự.");
            }
            if (taiKhoan.TenDangNhap.Length < 4 || taiKhoan.TenDangNhap.Length > 50)
            {
                throw new ArgumentException("Tên đăng nhập phải chứa từ 4 đến 50 ký tự!");
            }
            if (taiKhoan.MatKhau.Length < 6 || taiKhoan.MatKhau.Length > 255)
            {
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            }
            if (taiKhoan.HoTen.Length < 2 || taiKhoan.HoTen.Length > 100)
            {
                throw new ArgumentException("Họ tên phải chứa từ 2 đến 100 ký tự!");
            }

            // 4. RÀNG BUỘC ĐỊNH DẠNG VÀ CHẶN KHOẢNG TRẮNG Ở GIỮA (Dùng Regex như hàm đăng ký)
            // Regex này tự động cấm luôn khoảng trắng ở giữa tên đăng nhập
            if (!Regex.IsMatch(taiKhoan.TenDangNhap, "^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Tên đăng nhập chứa ký tự không hợp lệ (chỉ chấp nhận chữ cái không dấu, số và dấu '_')!");
            }

            // Định dạng số điện thoại chuẩn Việt Nam (nếu người dùng có điền)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai) && !Regex.IsMatch(taiKhoan.SoDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
            {
                throw new ArgumentException("Số điện thoại không đúng định dạng hợp lệ!");
            }

            // Kiểm tra trùng Số điện thoại chủ động
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai))
            {
                if (_taiKhoanDAL.KiemTraTrungSoDienThoai(taiKhoan.SoDienThoai, taiKhoan.MaTaiKhoan))
                    throw new ArgumentException("Số điện thoại này đã tồn tại trong hệ thống!");
            }

            if (_taiKhoanDAL.KiemTraTrungTenDangNhapCoLoaiTru(taiKhoan.TenDangNhap, taiKhoan.MaTaiKhoan))
                throw new ArgumentException("Tên đăng nhập này đã được sử dụng!");

            // 5. GỌI XUỐNG TẦNG DAL THỰC THI
            try
            {
                return _taiKhoanDAL.UpdateTaiKhoanDTO(taiKhoan);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng BLL khi xử lý cập nhật dữ liệu: " + ex.Message);
            }
        }

        public bool XuLyQuenMatKhau(string soDienThoai, string matKhauMoi, string nhapLaiMatKhau)
        {
            if (string.IsNullOrEmpty(soDienThoai))
                throw new ArgumentException("Vui lòng nhập số điện thoại!");
            if (!string.IsNullOrEmpty(soDienThoai) && !Regex.IsMatch(soDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
                throw new ArgumentException("Số điện thoại không hợp lệ!");
            if (string.IsNullOrEmpty(matKhauMoi))
                throw new ArgumentException("Vui lòng nhập mật khẩu mới!");
            if (string.IsNullOrEmpty(nhapLaiMatKhau))
                throw new ArgumentException("Vui lòng nhập lại mật khẩu xác nhận!");
            if (matKhauMoi.Length < 6 || matKhauMoi.Length > 255)
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            if (matKhauMoi != nhapLaiMatKhau)
                throw new ArgumentException("Mật khẩu mới và Mật khẩu nhập lại không khớp nhau!");

            // 2. Gọi thẳng lệnh UPDATE dưới DAL. 
            // Nếu SDT sai hoặc không tồn tại, rowsAffected sẽ bằng 0 -> Hàm trả về false.
            bool ketQuaUpdate = _taiKhoanDAL.CapNhatMatKhauTheoSDT(soDienThoai, matKhauMoi);

            return ketQuaUpdate;
        }
        public bool NgungHoatDongShipper(string maTaiKhoan)
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

        public int LayTongSoHoaDon(string maShipper)
        {
            string query = "SELECT COUNT(*) FROM DON_HANG";
            object result = DatabaseHelper.ExecuteScalar(query);
            return Convert.ToInt32(result);
        }

        public bool KichHoatTrangThaiTaiKhoan(string maTaiKhoan, string trangThai)
        {
            if (string.IsNullOrEmpty(maTaiKhoan))
                throw new ArgumentException("Mã tài khoản không được trống");
            return _taiKhoanDAL.KichHoatTrangThaiTaiKhoan(maTaiKhoan, trangThai);
        }
    }
}
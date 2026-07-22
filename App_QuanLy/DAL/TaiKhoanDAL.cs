using System.Data;
using App_QuanLy.Data;
using App_QuanLy.Models;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.DAL
{
    public class TaiKhoanDAL
    {
        // Kiểm tra tài khoản khi đăng nhập
        public TaiKhoan KiemTraDangNhap(string tenDangNhap, string matKhau)
        {
            string query = @"SELECT MaTaiKhoan, TenDangNhap, HoTen, SoDienThoai, VaiTro 
                            FROM TAI_KHOAN 
                            WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = tenDangNhap },
                new SqlParameter("@MatKhau", SqlDbType.VarChar, 255) { Value = matKhau } // Nên hash mật khẩu thực tế
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new TaiKhoan
                {
                    // Trả về thực thể với các thông tin sau để lưu vết lịch sử thao táo
                    MaTaiKhoan = row["MaTaiKhoan"].ToString(),
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    SoDienThoai = row["SoDienThoai"].ToString(),
                    VaiTro = row["VaiTro"].ToString()
                };
            }
            return null;
        }

        // Hàm thêm mới tài khoản Shipper do Admin chỉ định
        public bool ThemTaiKhoan(TaiKhoan tk)
        {
            string query = @"INSERT INTO TAI_KHOAN (TenDangNhap, MatKhau, HoTen, SoDienThoai, DiaChi, VaiTro, HinhAnh, TrangThai, GioiTinh) 
                            VALUES (@TenDangNhap, @MatKhau, @HoTen, @SoDienThoai, @DiaChi, @VaiTro, @HinhAnh, @TrangThai, @GioiTinh)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", tk.TenDangNhap ),
                new SqlParameter("@MatKhau", tk.MatKhau ),
                new SqlParameter("@HoTen",  tk.HoTen ),
                new SqlParameter("@SoDienThoai", tk.SoDienThoai),
                new SqlParameter("@DiaChi", tk.DiaChi),
                new SqlParameter("@HinhAnh", (object)tk.HinhAnh ?? DBNull.Value),
                new SqlParameter("@TrangThai", tk.TrangThai),
                new SqlParameter("@GioiTinh", tk.GioiTinh ?? (object)DBNull.Value),
                new SqlParameter("@VaiTro", tk.VaiTro) 
            };

            int result = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return result > 0;
        }

        // Hàm sửa thông tin Shipper
        public bool SuaTaiKhoan(TaiKhoan tk)
        {
            // Cập nhật các thông tin cá nhân và hình ảnh dựa theo mã tài khoản
            string query = "UPDATE TAI_KHOAN SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, HoTen = @HoTen, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, HinhAnh = @HinhAnh, GioiTinh = @GioiTinh, VaiTro = @VaiTro WHERE MaTaiKhoan = @MaTaiKhoan";

            // Giả định hàm ExecuteNonQuery của DatabaseHelper nhận vào câu lệnh và mảng tham số (parameters)
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", tk.MaTaiKhoan),
                new SqlParameter("@TenDangNhap", tk.TenDangNhap),
                new SqlParameter("@MatKhau", tk.MatKhau),
                new SqlParameter("@HoTen", tk.HoTen),
                new SqlParameter("@SoDienThoai", tk.SoDienThoai),
                new SqlParameter("@DiaChi", tk.DiaChi),
                new SqlParameter("@HinhAnh", tk.HinhAnh ?? (object)DBNull.Value), // Ảnh rất hay bị null
                new SqlParameter("@GioiTinh", tk.GioiTinh ?? (object)DBNull.Value),
                new SqlParameter("@VaiTro", tk.VaiTro)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Hàm xóa Shipper
        public bool XoaShipper(string maTaiKhoan)
        {
            string query = "DELETE FROM TAI_KHOAN WHERE MaTaiKhoan = @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool KiemTraTrungSoDienThoai(string soDienThoai)
        {
            string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE SoDienThoai = @SoDienThoai";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = soDienThoai }
            };

            // Giả định ExecuteScalar trả về giá trị ô đầu tiên dòng đầu tiên (kiểu int)
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            return count > 0; // Trả về true nếu đã tồn tại
        }

        public bool KiemTraTrungTenDangNhap(string tenDangNhap)
        {
            string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@TenDangNhap", SqlDbType.VarChar, 50) { Value = tenDangNhap }
            };

            // Giả định ExecuteScalar trả về giá trị ô đầu tiên dòng đầu tiên (kiểu int)
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            return count > 0; // Trả về true nếu đã tồn tại
        }

        // Kiểm tra trùng SĐT có loại trừ bản ghi hiện tại
        public bool KiemTraTrungSoDienThoai(string soDienThoai, string maTaiKhoan)
        {
            // Tìm những tài khoản có SĐT trùng nhưng MÃ lại KHÁC mã đang chỉnh sửa
            string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE SoDienThoai = @SoDienThoai AND MaTaiKhoan != @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@SoDienThoai", soDienThoai),
            new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            return count > 0; // Trả về true nếu trùng với NGƯỜI KHÁC
        }

        // Kiểm tra trùng Tên đăng nhập có loại trừ bản ghi hiện tại
        public bool KiemTraTrungTenDangNhap(string tenDangNhap, string maTaiKhoan)
        {
            // Tìm những tài khoản có Username trùng nhưng MÃ lại KHÁC mã đang chỉnh sửa
            string query = "SELECT COUNT(*) FROM TAI_KHOAN WHERE TenDangNhap = @TenDangNhap AND MaTaiKhoan != @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@TenDangNhap", tenDangNhap),
            new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, parameters));
            return count > 0; // Trả về true nếu trùng với NGƯỜI KHÁC
        }

        public DataTable LayDanhSachShipper(string tuKhoa, string vaiTro)
        {
            string query = @"SELECT MaTaiKhoan, TenDangNhap, MatKhau, HoTen, SoDienThoai, DiaChi, GioiTinh, HinhAnh, VaiTro, TrangThai, TrangThaiLamViec FROM
                TAI_KHOAN WHERE
                HoTen COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @tuKhoa AND
                (@VaiTro IS NULL OR @VaiTro = '' OR VaiTro = @VaiTro)";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@tuKhoa", SqlDbType.NVarChar) { Value = "%" + tuKhoa + "%" },
                new SqlParameter("@VaiTro", SqlDbType.VarChar) { Value = vaiTro }
            };

            return DatabaseHelper.ExecuteQuery(query, parameter);
        }

        public DataTable PollDanhSachTaiKhoan()
        {
            //Chỉ lấy các tải khoản và các cột cần poll của shipper
            string query = "SELECT MaTaiKhoan, TrangThai, TrangThaiLamViec FROM TAI_KHOAN";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public DataTable PollDanhSachShipperSS()
        {
            string query = @"SELECT MaTaiKhoan, HoTen, TrangThai, TrangThaiLamViec, HinhAnh, SoDienThoai 
                     FROM TAI_KHOAN 
                     WHERE VaiTro = 'shipper' AND TrangThaiLamViec = 1 AND TrangThai = N'Sẵn sàng';
                       ";

            return DatabaseHelper.ExecuteQuery(query);
        }
        public bool CapNhatTrangThaiHoatDong(string maTaiKhoan, bool trangThaiMoi)
        {
            string query = "UPDATE TAI_KHOAN SET TrangThaiLamViec = @TrangThaiLamViec WHERE MaTaiKhoan = @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan),
                new SqlParameter("@TrangThaiLamViec", trangThaiMoi) // true hoặc false
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Thực hiện cập nhật mật khẩu mới cho tài khoản dựa trên số điện thoại của Quản lý
        /// </summary>
        public bool CapNhatMatKhauTheoSDT(string soDienThoai, string matKhauMoi)
        {
            // Sử dụng lệnh UPDATE trực tiếp theo số điện thoại
            string query = "UPDATE TAI_KHOAN SET MatKhau = @MatKhauMoi WHERE SoDienThoai = @SoDienThoai AND VaiTro = 'admin'";

            // Sử dụng syntax mảng cố định SqlParameter[] đồng bộ với mẫu của bạn
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MatKhauMoi", SqlDbType.VarChar, 255) { Value = matKhauMoi }, // Thực tế nên hash mật khẩu
            new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 15) { Value = soDienThoai.Trim() }
            };

            // Thực thi lệnh hiệu chỉnh dữ liệu bằng ExecuteNonQuery, trả về số dòng bị ảnh hưởng
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

            // Nếu số dòng ảnh hưởng > 0 tức là số điện thoại có tồn tại và đã cập nhật thành công
            return rowsAffected > 0;
        }

        public int LaySoLuongTaiKhoanAdminHoatDong()
        {
            string query = @"SELECT COUNT(*) FROM TAI_KHOAN WHERE VaiTro = 'admin' AND TrangThaiLamViec = 1";
            int result = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query));
            return result;
        }

        public DataTable LayTongSoTaiKhoan()
        {
            string query = "SELECT COUNT(*) FROM TAI_KHOAN";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public bool XoaTaiKhoan(string maTaiKhoan, string vaiTro)
        {
            string query = "DELETE FROM TAI_KHOAN WHERE MaTaiKhoan = @MaTaiKhoan AND VaiTro = @VaiTro;";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan),
                new SqlParameter("@VaiTro", vaiTro)
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool KichHoatTrangThaiTaiKhoan(string maTaiKhoan, string trangThai)
        {
            // Sử dụng lệnh UPDATE trực tiếp theo số điện thoại
            string query = "UPDATE TAI_KHOAN SET TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan AND VaiTro = 'admin'";

            // Sử dụng syntax mảng cố định SqlParameter[] đồng bộ với mẫu của bạn
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MaTaiKhoan", maTaiKhoan ), // Thực tế nên hash mật khẩu
            new SqlParameter("@TrangThai", trangThai )
            };

            // Thực thi lệnh hiệu chỉnh dữ liệu bằng ExecuteNonQuery, trả về số dòng bị ảnh hưởng
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

            // Nếu số dòng ảnh hưởng > 0 tức là số điện thoại có tồn tại và đã cập nhật thành công
            return rowsAffected > 0;
        }
    }

}
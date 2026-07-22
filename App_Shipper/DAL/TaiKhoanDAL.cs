using System.Data;
using App_Shipper.Data;
using App_Shipper.Models;
using Microsoft.Data.SqlClient;

namespace App_Shipper.DAL
{
    public class TaiKhoanDAL
    {
        public TaiKhoan GetTaiKhoanDTOById(string maTaiKhoan)
        {
            // Viết câu lệnh SELECT tường minh
            string query = @"SELECT MaTaiKhoan, TenDangNhap, MatKhau, HoTen, SoDienThoai, 
                                    VaiTro, HinhAnh, DiaChi, TrangThai, TrangThaiLamViec, GioiTinh 
                             FROM TAI_KHOAN 
                             WHERE MaTaiKhoan = @MaTaiKhoan AND VaiTro = 'shipper'";

            // Truyền tham số an toàn
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            try
            {
                // Gọi hàm Generic của DatabaseHelper, truyền khuôn <TaiKhoan> vào
                List<TaiKhoan> result = DatabaseHelper.ExecuteQuery<TaiKhoan>(query, parameters);

                // Nếu tìm thấy tài khoản thì trả về đối tượng đầu tiên, ngược lại trả về null
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // Có thể ghi log lỗi ở đây nếu cần
                throw new Exception("Lỗi tầng DAL khi lấy thông tin tài khoản: " + ex.Message);
            }
        }

        public string GetMaTaiKhoanByCredentials(string username, string password)
        {
            // Truy vấn chỉ lấy đúng cột MaTaiKhoan khi khớp cả user lẫn pass
            // Đồng thời check xem tài khoản đó có đang được phép hoạt động hay không (TrangThaiLamViec = 1)
            string query = @"SELECT MaTaiKhoan 
                     FROM TAI_KHOAN 
                     WHERE TenDangNhap = @TenDangNhap 
                       AND MatKhau = @MatKhau 
                       AND TrangThaiLamViec = 1
                       AND VaiTro = 'shipper'"
                       ;

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", username),
                new SqlParameter("@MatKhau", password) // Nếu sau này có mã hóa (MD5/SHA256) thì băm password trước khi truyền vào đây
            };

            try
            {
                // ExecuteScalar trả về kiểu 'object', ta ép nó về 'string'
                object result = DatabaseHelper.ExecuteScalar(query, parameters);

                // Nếu đúng user/pass thì result sẽ chứa MaTaiKhoan (Ví dụ: "NV001"), ngược lại trả về null
                return result?.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng DAL khi xác thực đăng nhập: " + ex.Message);
            }
        }

        public bool DangKyTaiKhoanShipper(TaiKhoan taiKhoan)
        {
            string query = @"INSERT INTO TAI_KHOAN (TenDangNhap, MatKhau, HoTen, SoDienThoai, DiaChi, GioiTinh, VaiTro)
                            VALUES
                                 (@TenDangNhap, 
                                 @MatKhau, 
                                 @HoTen, 
                                 @SoDienThoai, 
                                 @DiaChi, 
                                 @GioiTinh,
                                 @VaiTro
                                    ) 
                             ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", taiKhoan.TenDangNhap),
                new SqlParameter("@MatKhau", taiKhoan.MatKhau),
                new SqlParameter("@HoTen", taiKhoan.HoTen),
                // Xử lý giá trị có thể null (nếu để trống số điện thoại hoặc địa chỉ)
                new SqlParameter("@SoDienThoai", string.IsNullOrWhiteSpace(taiKhoan.SoDienThoai) ? DBNull.Value : (object)taiKhoan.SoDienThoai),
                new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(taiKhoan.DiaChi) ? DBNull.Value : (object)taiKhoan.DiaChi),
                new SqlParameter("@GioiTinh", string.IsNullOrWhiteSpace(taiKhoan.GioiTinh) ? DBNull.Value : (object)taiKhoan.GioiTinh),
                new SqlParameter("@VaiTro", "client"),
            };

            try
            {
                // Gọi ExecuteNonQuery từ DatabaseHelper của bạn
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Nếu số dòng bị ảnh hưởng lớn hơn 0 nghĩa là đăng ký thành công
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng DAL khi đăng ký tài khoản: " + ex.Message);
            }
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
        public bool KiemTraTrungTenDangNhapCoLoaiTru(string tenDangNhap, string maTaiKhoan)
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
        public bool UpdateTaiKhoanDTO(TaiKhoan taiKhoan)
        {
            string query = @"UPDATE TAI_KHOAN 
                             SET TenDangNhap = @TenDangNhap, 
                                 MatKhau = @MatKhau, 
                                 HoTen = @HoTen, 
                                 SoDienThoai = @SoDienThoai, 
                                 DiaChi = @DiaChi, 
                                 GioiTinh = @GioiTinh, 
                                 HinhAnh = @HinhAnh
                             WHERE MaTaiKhoan = @MaTaiKhoan";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDangNhap", taiKhoan.TenDangNhap),
                new SqlParameter("@MatKhau", taiKhoan.MatKhau),
                new SqlParameter("@HoTen", taiKhoan.HoTen),
                // Xử lý giá trị có thể null (nếu để trống số điện thoại hoặc địa chỉ)
                new SqlParameter("@SoDienThoai", string.IsNullOrWhiteSpace(taiKhoan.SoDienThoai) ? DBNull.Value : (object)taiKhoan.SoDienThoai),
                new SqlParameter("@DiaChi", string.IsNullOrWhiteSpace(taiKhoan.DiaChi) ? DBNull.Value : (object)taiKhoan.DiaChi),
                new SqlParameter("@GioiTinh", string.IsNullOrWhiteSpace(taiKhoan.GioiTinh) ? DBNull.Value : (object)taiKhoan.GioiTinh),
                new SqlParameter("@HinhAnh", string.IsNullOrWhiteSpace(taiKhoan.HinhAnh) ? DBNull.Value : (object)taiKhoan.HinhAnh),
                new SqlParameter("@MaTaiKhoan", taiKhoan.MaTaiKhoan)
            };

            try
            {
                // Gọi ExecuteNonQuery từ DatabaseHelper của bạn
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

                // Nếu số dòng bị ảnh hưởng lớn hơn 0 nghĩa là cập nhật thành công
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng DAL khi cập nhật tài khoản: " + ex.Message);
            }
        }

        public bool UpdateTrangThaiLamViec(string maTaiKhoan)
        {
            string query = "UPDATE TAI_KHOAN SET TrangThaiLamViec = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan";

            // Dùng kiểu rút gọn đơn giản, ngắn gọn
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", false),
                new SqlParameter("@MaTaiKhoan", maTaiKhoan)
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tầng DAL khi cập nhật trạng thái: " + ex.Message);
            }
        }

        public bool CapNhatMatKhauTheoSDT(string soDienThoai, string matKhauMoi)
        {
            // Sử dụng lệnh UPDATE trực tiếp theo số điện thoại
            string query = "UPDATE TAI_KHOAN SET MatKhau = @MatKhauMoi WHERE SoDienThoai = @SoDienThoai AND VaiTro = 'client'";

            // Sử dụng syntax mảng cố định SqlParameter[] đồng bộ với mẫu của bạn
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@MatKhauMoi", matKhauMoi ), // Thực tế nên hash mật khẩu
            new SqlParameter("@SoDienThoai", soDienThoai.Trim() )
            };

            // Thực thi lệnh hiệu chỉnh dữ liệu bằng ExecuteNonQuery, trả về số dòng bị ảnh hưởng
            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);

            // Nếu số dòng ảnh hưởng > 0 tức là số điện thoại có tồn tại và đã cập nhật thành công
            return rowsAffected > 0;
        }

        public bool KichHoatTrangThaiTaiKhoan(string maTaiKhoan, string trangThai)
        {
            // Sử dụng lệnh UPDATE trực tiếp theo số điện thoại
            string query = "UPDATE TAI_KHOAN SET TrangThai = @TrangThai WHERE MaTaiKhoan = @MaTaiKhoan AND VaiTro = 'shipper'";

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
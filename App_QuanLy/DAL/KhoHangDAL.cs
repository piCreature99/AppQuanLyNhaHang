using System;
using System.Data;
using App_QuanLy.Models;
using App_QuanLy.Data;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.DAL
{
    public class KhoHangDAL
    {
        // TÍNH NĂNG 1: Lọc và Tìm kiếm nguyên liệu đầy đủ các cột (Lệnh SELECT + LIKE)
        public DataTable LayDanhSachNguyenLieu(string tuKhoa)
        {
            // Sử dụng CASE WHEN để tính toán Trạng Thái trực tiếp dựa trên Số lượng tồn và Tồn tối thiểu
            string query = @"
                SELECT 
                    MaNguyenLieu, 
                    TenNguyenLieu, 
                    SoLuongTon, 
                    DonViTinh, 
                    GiaNhapGanNhat, 
                    TonToiThieu,
                    TrangThai AS TrangThaiKinhDoanh,
                    CASE 
                        WHEN SoLuongTon <= 0 THEN N'Hết hàng'
                        WHEN SoLuongTon <= TonToiThieu THEN N'Sắp hết hàng'
                        ELSE N'Còn hàng'
                    END AS TrangThai
                FROM NGUYEN_LIEU 
                WHERE TenNguyenLieu COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @TuKhoa";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuKhoa", SqlDbType.NVarChar) { Value = "%" + tuKhoa + "%" }
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // TÍNH NĂNG 2: Thêm nguyên liệu mới đầy đủ các cột (Lệnh INSERT INTO)
        public bool ThemNguyenLieu(NguyenLieu nl)
        {
            // Bổ sung các cột còn thiếu vào câu lệnh INSERT
            string query = "INSERT INTO NGUYEN_LIEU (TenNguyenLieu, SoLuongTon, DonViTinh, GiaNhapGanNhat, TonToiThieu) " +
                           "VALUES (@Ten, @SoLuong, @DonVi, @GiaNhap, @TonMin)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ten", nl.TenNguyenLieu),
                new SqlParameter("@SoLuong", nl.SoLuongTon), // Đổi sang Decimal để khớp với số thực ở Form
                new SqlParameter("@DonVi", nl.DonViTinh),
                new SqlParameter("@GiaNhap", nl.GiaNhapGanNhat),
                new SqlParameter("@TonMin", nl.TonToiThieu),
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // TÍNH NĂNG 3: Cập nhật nguyên liệu đầy đủ các cột (Lệnh UPDATE)
        public bool CapNhatNguyenLieu(NguyenLieu nl)
        {
            // Bổ sung các cột còn thiếu vào câu lệnh UPDATE
            string query = "UPDATE NGUYEN_LIEU SET TenNguyenLieu = @Ten, SoLuongTon = @SoLuong, DonViTinh = @DonVi, " +
                           "GiaNhapGanNhat = @GiaNhap, TonToiThieu = @TonMin WHERE MaNguyenLieu = @Ma";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ma", nl.MaNguyenLieu),
                new SqlParameter("@Ten", nl.TenNguyenLieu),
                new SqlParameter("@SoLuong",nl.SoLuongTon),
                new SqlParameter("@DonVi", nl.DonViTinh),
                new SqlParameter("@GiaNhap",nl.GiaNhapGanNhat), 
                new SqlParameter("@TonMin",nl.TonToiThieu),     
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // TÍNH NĂNG 4: Xóa nguyên liệu (Lệnh DELETE)
        public bool XoaNguyenLieu(int maNguyenLieu)
        {
            string query = "DELETE FROM NGUYEN_LIEU WHERE MaNguyenLieu = @Ma";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maNguyenLieu }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool XoaNguyenLieuVatLy(int maNguyenLieu)
        {
            using (SqlConnection conn = new SqlConnection(SQLServerDbContext.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {

                        // 2. Xóa món ăn ở bảng MON_AN
                        string queryDeleteNguyenLieu = "DELETE FROM NGUYEN_LIEU WHERE MaNguyenLieu = @MaNguyenLieu";
                        using (SqlCommand cmd = new SqlCommand(queryDeleteNguyenLieu, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        trans.Rollback();

                        // Mã lỗi 547 là lỗi Foreign Key Constraint trong SQL Server
                        if (ex.Number == 547)
                        {
                            throw new Exception("FOREIGN_KEY_CONFLICT");
                        }
                        throw ex;
                    }
                }
            }
        }

        // 1. Hàm kiểm tra trùng tên khi THÊM MỚI (Chỉ cần truyền Tên nguyên liệu)
        public bool KiemTraTrungTenNguyenLieu(string tenNguyenLieu)
        {
            string query = "SELECT COUNT(*) FROM NGUYEN_LIEU WHERE TenNguyenLieu = @TenNguyenLieu";

            SqlParameter[] p = {
                new SqlParameter("@TenNguyenLieu", tenNguyenLieu.Trim())
             };

            // ExecuteScalar trả về kiểu object, cần ép về kiểu int
            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p));

            return count > 0; // Trả về true nếu đã tồn tại tên này
        }

        // 2. Hàm kiểm tra trùng tên khi CẬP NHẬT (Truyền cả Tên và Mã để né chính nó ra)
        public bool KiemTraTrungTenNguyenLieu(string tenNguyenLieu, int maNguyenLieu)
        {
            // Tìm xem có thằng NÀO KHÁC (MaNguyenLieu != @MaNguyenLieu) mà lại trùng cái tên này không
            string query = "SELECT COUNT(*) FROM NGUYEN_LIEU WHERE TenNguyenLieu = @TenNguyenLieu AND MaNguyenLieu != @MaNguyenLieu";

            SqlParameter[] p = {
                new SqlParameter("@TenNguyenLieu", tenNguyenLieu.Trim()),
                new SqlParameter("@MaNguyenLieu", maNguyenLieu)
             };

            int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p));

            return count > 0; // Trả về true nếu tên này đã bị một nguyên liệu khác chiếm mất
        }

        public bool CapNhatNgungKinhDoanh(int maNguyenLieu, bool trangThai)
        {
            string query = "UPDATE NGUYEN_LIEU SET TrangThai = @TrangThai WHERE MaNguyenLieu = @MaNguyenLieu";
            SqlParameter[] p = { new SqlParameter("@MaNguyenLieu", maNguyenLieu),
                new SqlParameter("@TrangThai", trangThai) };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0; // Trả về true nếu update thành công
        }

        public DataTable LayDanhSachNguyenLieuDeGoiY()
        {
            // Lấy thêm DonViTinh để khi chọn nguyên liệu, ta biết đơn vị của nó là gì (g, ml, kg...)
            string query = "SELECT MaNguyenLieu, TenNguyenLieu, DonViTinh FROM NGUYEN_LIEU";
            return DatabaseHelper.ExecuteQuery(query);
        }


    }
}
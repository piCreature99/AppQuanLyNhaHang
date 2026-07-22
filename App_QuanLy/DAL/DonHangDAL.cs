using System;
using System.Data;
using App_QuanLy.Data;
using App_QuanLy.DTOS;
using App_QuanLy.Models;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.DAL
{
    public class DonHangDAL
    {
        // 1. DÙNG CHO POLLING: Lấy các đơn hàng theo trạng thái cụ thể
        // Phục vụ App Admin (quét đơn mới) và App Shipper (quét đơn chờ giao)
        public DataTable LayDonHangTheoTrangThai(string trangThai)
        {
            string query = @"SELECT DH.MaDonHang, TK.HoTen AS TenKhachHang, DH.NgayDat, DH.TongTien, DH.DiaChiGiaoHang 
                            FROM DON_HANG DH 
                            JOIN TAI_KHOAN TK ON DH.MaKhachHang = TK.MaTaiKhoan
                            WHERE DH.TrangThaiDonHang = @TrangThai
                            ORDER BY DH.NgayDat DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TrangThai", SqlDbType.NVarChar) { Value = trangThai }
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // 2. CHỦ QUÁN DUYỆT ĐƠN: Cập nhật trạng thái đơn hàng (Dùng VARCHAR(36) cho ID)
        public bool CapNhatTrangThaiDon(string maDonHang, string trangThaiMoi, string maShipper = null)
        {
            string query = @"UPDATE DON_HANG 
                            SET TrangThaiDonHang = @TrangThai, MaShipper = @MaShipper 
                            WHERE MaDonHang = @MaDonHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDonHang", SqlDbType.VarChar, 36) { Value = maDonHang },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar) { Value = trangThaiMoi },
                new SqlParameter("@MaShipper", SqlDbType.VarChar, 36) { Value = (object)maShipper ?? DBNull.Value }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // 3. THUẬT TOÁN: Trừ kho nguyên liệu tự động khi duyệt đơn
        // Hàm này lấy danh sách định lượng cần hao hụt của tất cả món ăn nằm trong 1 đơn hàng
        public DataTable LayDinhLuongNguyenLieuCuaDonHang(string maDonHang)
        {
            string query = @"SELECT CT.MaNguyenLieu, (CT.SoLuongHaoHut * CTDH.SoLuong) AS TongHaoHut
                            FROM CHI_TIET_DON_HANG CTDH
                            JOIN CONG_THUC CT ON CTDH.MaMonAn = CT.MaMonAn
                            WHERE CTDH.MaDonHang = @MaDonHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDonHang", SqlDbType.VarChar, 36) { Value = maDonHang }
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // 4. Trực tiếp trừ số lượng tồn trong kho
        public bool TruKhoNguyenLieu(int maNguyenLieu, decimal soLuongTru)
        {
            string query = @"UPDATE NGUYEN_LIEU 
                            SET SoLuongTon = SoLuongTon - @SoLuongTru 
                            WHERE MaNguyenLieu = @MaNguyenLieu";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNguyenLieu", SqlDbType.Int) { Value = maNguyenLieu },
                new SqlParameter("@SoLuongTru", SqlDbType.Decimal) { Value = soLuongTru }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Lấy trọn gói thông tin hành chính, danh sách món và định lượng kho của một đơn hàng
        /// Sau đó tự động gom nhóm, đóng gói thành cấu trúc phân cấp DonHangDTO
        /// </summary>
        /// <summary>
        /// Lấy danh sách đơn hàng phân cấp, gom thành một Map tổng dựa trên bộ lọc trạng thái hoặc mã đơn.
        /// Nếu tham số để trống hoặc null, hệ thống tự động lấy TẤT CẢ các đơn hàng.
        /// </summary>
        public Dictionary<string, DonHangDTO> LayDanhSachDonHangGomMap(string maDonHang, string trangThaiDonHang)
        {
            // Khởi tạo Map tổng: Key là MaDonHang, Value là Object DonHangDTO tương ứng
            Dictionary<string, DonHangDTO> mapTongDonHang = new Dictionary<string, DonHangDTO>();

            // 1. Câu truy vấn gốc
            string query = @"
            SELECT 
                dh.MaDonHang, dh.NgayDat, dh.TongTien, dh.DiaChiGiaoHang, dh.TrangThaiDonHang,
                dh.MaKhachHang, kh.HoTen AS TenKhachHang, kh.HinhAnh as HinhAnhKhach, kh.SoDienThoai AS SoDienThoaiKhach,
                dh.MaShipper, sp.HoTen AS TenShipper, sp.HinhAnh as HinhAnhShipper, sp.SoDienThoai as SoDienThoaiShipper,
                ctdh.MaMonAn, ma.TenMonAn, ctdh.SoLuong AS SoLuongDat, ctdh.GiaBan,
                ct.MaNguyenLieu, nl.TenNguyenLieu, ct.SoLuongHaoHut, nl.SoLuongTon, nl.DonViTinh
            FROM DON_HANG dh
            LEFT JOIN TAI_KHOAN kh ON dh.MaKhachHang = kh.MaTaiKhoan
            LEFT JOIN TAI_KHOAN sp ON dh.MaShipper = sp.MaTaiKhoan
            JOIN CHI_TIET_DON_HANG ctdh ON dh.MaDonHang = ctdh.MaDonHang
            JOIN MON_AN ma ON ctdh.MaMonAn = ma.MaMonAn
            LEFT JOIN CONG_THUC ct ON ma.MaMonAn = ct.MaMonAn
            LEFT JOIN NGUYEN_LIEU nl ON ct.MaNguyenLieu = nl.MaNguyenLieu
            WHERE 1 = 1"; // Sử dụng mệnh đề 1=1 để nối điều kiện động an toàn

            // 2. Xử lý điều kiện lọc động
            List<SqlParameter> paramList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(maDonHang))
            {
                query += " AND dh.MaDonHang LIKE @MaDonHang";
                paramList.Add(new SqlParameter("@MaDonHang", SqlDbType.VarChar) { Value = "%" + maDonHang  + "%"});
            }

            if (!string.IsNullOrEmpty(trangThaiDonHang) && trangThaiDonHang != "Tất cả")
            {
                query += " AND dh.TrangThaiDonHang = @TrangThaiDonHang";
                paramList.Add(new SqlParameter("@TrangThaiDonHang", SqlDbType.NVarChar) { Value = trangThaiDonHang });
            }

            // Sắp xếp đơn hàng mới nhất lên trên đầu
            query += " ORDER BY dh.NgayDat DESC";

            // 3. Thực thi câu lệnh qua DatabaseHelper
            DataTable dtRaw = DatabaseHelper.ExecuteQuery(query, paramList.ToArray());

            if (dtRaw == null || dtRaw.Rows.Count == 0) return mapTongDonHang;

            // 4. Duyệt bảng phẳng để đưa vào cấu trúc Map tổng lồng Map con
            foreach (DataRow row in dtRaw.Rows)
            {
                string currentMaDon = row["MaDonHang"].ToString();

                // LƯU Ý: Kiểm tra nếu đơn hàng này chưa tồn tại trong Map tổng thì tạo mới node đơn hàng cha
                if (!mapTongDonHang.ContainsKey(currentMaDon))
                {
                    DonHangDTO donHang = new DonHangDTO
                    {
                        MaDonHang = currentMaDon,
                        TrangThaiDonHang = row["TrangThaiDonHang"].ToString(),
                        NgayDat = Convert.ToDateTime(row["NgayDat"]),
                        TongTien = Convert.ToDecimal(row["TongTien"]),
                        MaKhachHang = row["MaKhachHang"].ToString(),
                        TenKhachHang = row["TenKhachHang"].ToString(),
                        HinhAnhKhach = row["HinhAnhKhach"] != DBNull.Value ? row["HinhAnhKhach"].ToString() : "",
                        SoDienThoaiKhach = row["SoDienThoaiKhach"].ToString(),
                        DiaChiGiaoHang = row["DiaChiGiaoHang"].ToString(),
                        MaShipper = row["MaShipper"] != DBNull.Value ? row["MaShipper"].ToString() : "",
                        TenShipper = row["TenShipper"].ToString(),
                        SoDienThoaiShipper = row["SoDienThoaiShipper"] != DBNull.Value ? row["SoDienThoaiShipper"].ToString() : "",
                        HinhAnhShipper = row["HinhAnhShipper"] != DBNull.Value ? row["HinhAnhShipper"].ToString() : ""
                    };

                    mapTongDonHang.Add(currentMaDon, donHang);
                }

                // Lấy ra tham chiếu của đơn hàng hiện tại trong Map để nhét tiếp Món ăn và Nguyên liệu
                DonHangDTO donHangHienTai = mapTongDonHang[currentMaDon];
                int maMon = Convert.ToInt32(row["MaMonAn"]);

                // Thêm món ăn vào MapMonAn của đơn hàng hiện tại nếu chưa có
                if (!donHangHienTai.MapMonAn.ContainsKey(maMon))
                {
                    donHangHienTai.MapMonAn[maMon] = new ChiTietMonAnDTO
                    {
                        MaMonAn = maMon,
                        TenMonAn = row["TenMonAn"].ToString(),
                        SoLuongDat = Convert.ToInt32(row["SoLuongDat"]),
                        GiaBan = Convert.ToDecimal(row["GiaBan"])
                    };
                }

                // Thêm định lượng nguyên liệu vào món ăn tương ứng
                if (row["MaNguyenLieu"] != DBNull.Value)
                {
                    int maNL = Convert.ToInt32(row["MaNguyenLieu"]);
                    int soLuongDatMon = donHangHienTai.MapMonAn[maMon].SoLuongDat;
                    decimal haoHut = Convert.ToDecimal(row["SoLuongHaoHut"]);

                    if (!donHangHienTai.MapMonAn[maMon].MapNguyenLieu.ContainsKey(maNL))
                    {
                        donHangHienTai.MapMonAn[maMon].MapNguyenLieu[maNL] = new NguyenLieuTieuHao
                        {
                            MaNguyenLieu = maNL,
                            TenNguyenLieu = row["TenNguyenLieu"].ToString(),
                            TongNguyenLieuCan = soLuongDatMon * haoHut,
                            TonKhoHienTai = Convert.ToDecimal(row["SoLuongTon"]),
                            DonViTinh = row["DonViTinh"].ToString()
                        };
                    }
                }
            }

            return mapTongDonHang;
        }

        public bool TruNguyenLieuKhiHoanThanh(string maDonHang)
        {
            using (SqlConnection conn = new SqlConnection(SQLServerDbContext.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Chạy lệnh UPDATE giảm số lượng tồn kho dựa theo định lượng hao hụt trong CONG_THUC
                        string queryUpdateKho = @"
                    UPDATE NL
                    SET NL.SoLuongTon = NL.SoLuongTon - (CT_DON.SoLuong * CT.SoLuongHaoHut)
                    FROM NGUYEN_LIEU NL
                    INNER JOIN CONG_THUC CT ON NL.MaNguyenLieu = CT.MaNguyenLieu
                    INNER JOIN CHI_TIET_DON_HANG CT_DON ON CT.MaMonAn = CT_DON.MaMonAn
                    WHERE CT_DON.MaDonHang = @MaDonHang";

                        using (SqlCommand cmd = new SqlCommand(queryUpdateKho, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Cập nhật trạng thái đơn hàng thành 'Đã nấu xong'
                        string queryUpdateTrangThai = "UPDATE DON_HANG SET TrangThaiDonHang = N'Đã nấu xong' WHERE MaDonHang = @MaDonHang";
                        using (SqlCommand cmd = new SqlCommand(queryUpdateTrangThai, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        // Hợp lệ toàn bộ -> Commit xác nhận lưu vào DB
                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Có bất kỳ lỗi gì phát sinh -> Rollback trả lại nguyên trạng kho cho DB
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public bool HoanLaiNguyenLieuKhiHuyDon(string maDonHang)
        {
            using (SqlConnection conn = new SqlConnection(SQLServerDbContext.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // Dấu cộng (+) để hoàn lại số lượng vào kho
                        string queryHoanKho = @"
                    UPDATE NL
                    SET NL.SoLuongTon = NL.SoLuongTon + (CT_DON.SoLuong * CT.SoLuongHaoHut)
                    FROM NGUYEN_LIEU NL
                    INNER JOIN CONG_THUC CT ON NL.MaNguyenLieu = CT.MaNguyenLieu
                    INNER JOIN CHI_TIET_DON_HANG CT_DON ON CT.MaMonAn = CT_DON.MaMonAn
                    WHERE CT_DON.MaDonHang = @MaDonHang";

                        using (SqlCommand cmd = new SqlCommand(queryHoanKho, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaDonHang", maDonHang);
                            cmd.ExecuteNonQuery();
                        }

                        trans.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public bool CapNhatTrangThaiDonHang(string maDonHang, string maShipper, string trangThaiMoi)
        {
            

            string query = @"UPDATE DON_HANG 
                     SET TrangThaiDonHang = @TrangThaiMoi, MaShipper = @MaShipper 
                     WHERE MaDonHang = @MaDonHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDonHang", SqlDbType.VarChar) { Value = maDonHang },
                new SqlParameter("@TrangThaiMoi", SqlDbType.NVarChar) { Value = trangThaiMoi },
                new SqlParameter("@MaShipper", SqlDbType.VarChar) { Value = string.IsNullOrEmpty(maShipper) ? DBNull.Value : maShipper}
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public DataTable PollTrangThaiDonHangMoiNhat()
        {
            // TỐI ƯU: Chỉ lấy đúng 2 cột quan trọng và giới hạn trong ngày hôm nay để tránh nặng DB
            string query = @"SELECT MaDonHang, TrangThaiDonHang 
                             FROM DON_HANG 
                             WHERE 1=1";
                             //WHERE NgayDat >= CAST(GETDATE() AS DATE)";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public bool KiemTraHoaDonMoiNhat()
        {
            string query = "SELECT COUNT(*) FROM DON_HANG";
            DatabaseHelper.ExecuteNonQuery(query);
            return true;
        }
        public int LayTongSoHoaDon()
        {
            string query = "SELECT COUNT(*) FROM DON_HANG";
            object result = DatabaseHelper.ExecuteScalar(query);
            return Convert.ToInt32(result);
        }
    }
}
using System.Data;
using App_QuanLy.Data;
using App_QuanLy.Models;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.DAL
{
    public class MonAnDAL
    {
        // Lấy danh sách món ăn kèm tên danh mục (JOIN hiển thị GUI)
        public DataTable LayDanhSachMonAn(string tuKhoa, int maDanhMuc)
        {
            string query = @"SELECT MA.MaMonAn, MA.TenMonAn, MA.DonGia, MA.HinhAnh, 
                            DM.TenDanhMuc, MA.MaDanhMuc, DM.CanNL, MA.TrangThaiKinhDoanh
                            FROM MON_AN MA                    
                            LEFT JOIN DANH_MUC DM ON MA.MaDanhMuc = DM.MaDanhMuc                    
                            WHERE MA.TenMonAn COLLATE SQL_Latin1_General_CP1_CI_AI LIKE @TuKhoa AND (@MaDanhMuc = 0 OR DM.MaDanhMuc = @MaDanhMuc)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TuKhoa", SqlDbType.NVarChar) { Value = "%" + tuKhoa + "%" },
                new SqlParameter("@MaDanhMuc", SqlDbType.Int) { Value = maDanhMuc }
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public bool ThemDM(string tenDM)
        {
            string query = @"INSERT INTO DANH_MUC(TenDanhMuc) 
                           VALUES
                           (@TenDanhMuc)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDanhMuc", tenDM)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public bool XoaDM(int maDM)
        {
            string query = @"DELETE FROM  DANH_MUC WHERE MaDanhMuc = @MaDanhMuc";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDanhMuc", maDM)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Thêm hàm lấy toàn bộ danh mục để nạp vào ComboBox
        public DataTable LayDanhSachDanhMuc()
        {
            // Lấy đầy đủ Ma, Ten và thuộc tính CanNL để lát nữa phân loại ở GUI
            string query = "SELECT MaDanhMuc, TenDanhMuc, CanNL FROM DANH_MUC";
            return DatabaseHelper.ExecuteQuery(query, null);
        }

        // Lấy chi tiết công thức của một món ăn (JOIN sang bảng NGUYEN_LIEU để lấy tên hiển thị
        public DataTable LayCongThucTheoMon(int maMonAn)
        {
            string query = @"SELECT CT.MaCongThuc, CT.MaNguyenLieu, NL.TenNguyenLieu, CT.SoLuongHaoHut, NL.DonViTinh
                            FROM CONG_THUC CT
                            JOIN NGUYEN_LIEU NL ON CT.MaNguyenLieu = NL.MaNguyenLieu
                            WHERE CT.MaMonAn = @MaMonAn";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMonAn", SqlDbType.Int) {Value = maMonAn}
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // Thêm món ăn mới (Dùng DonGia kiểu decimal)
        public bool ThemMonAn(MonAn ma)
        {
            string query = @"INSERT INTO MON_AN (TenMonAn, DonGia, HinhAnh, MaDanhMuc) 
                            VALUES (@Ten, @Gia, @HinhAnh, @MaDM)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ten", SqlDbType.NVarChar) { Value = ma.TenMonAn },
                new SqlParameter("@Gia", SqlDbType.Decimal) { Value = ma.DonGia },
                new SqlParameter("@HinhAnh", SqlDbType.VarChar) { Value = (object)ma.HinhAnh ?? System.DBNull.Value },
                new SqlParameter("@MaDM", SqlDbType.Int) { Value = (object)ma.MaDanhMuc ?? System.DBNull.Value }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Viết một hàm kiểm tra nhanh trong MonAnDAL hoặc BLL
        public bool KiemTraTrungTenMon(string tenMonAn)
        {
            string query = "SELECT COUNT(*) FROM MON_AN WHERE TenMonAn = @Ten";
            SqlParameter[] p = { new SqlParameter("@Ten", tenMonAn) };
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(query, p)) > 0;
        }

        // Thêm món ăn mới kèm danh sách công thức (Sử dụng Transaction)
        public bool ThemMonAnKemCongThuc(MonAn ma, DataTable dtCongThuc)
        {
            // Lấy chuỗi kết nối từ DatabaseHelper của bạn (Giả sử DatabaseHelper có thuộc tính ConnectionString)
            // Nếu không có, bạn thay bằng chuỗi kết nối chuẩn của dự án bạn nhé
            string connectionString = SQLServerDbContext.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Chèn thông tin món ăn vào bảng MON_AN và LẤY RA MaMonAn vừa tự động sinh (IDENTITY)
                    string queryMonAn = @"INSERT INTO MON_AN (TenMonAn, DonGia, HinhAnh, MaDanhMuc) 
                                  VALUES (@Ten, @Gia, @HinhAnh, @MaDM);
                                  SELECT SCOPE_IDENTITY();"; // Dòng này dùng để lấy Id vừa sinh

                    SqlCommand cmdMonAn = new SqlCommand(queryMonAn, conn, trans);
                    cmdMonAn.Parameters.Add(new SqlParameter("@Ten", SqlDbType.NVarChar) { Value = ma.TenMonAn });
                    cmdMonAn.Parameters.Add(new SqlParameter("@Gia", SqlDbType.Decimal) { Value = ma.DonGia });
                    cmdMonAn.Parameters.Add(new SqlParameter("@HinhAnh", SqlDbType.VarChar) { Value = (object)ma.HinhAnh ?? DBNull.Value });
                    cmdMonAn.Parameters.Add(new SqlParameter("@MaDM", SqlDbType.Int) { Value = (object)ma.MaDanhMuc ?? DBNull.Value });

                    // Lấy MaMonAn vừa tạo
                    int maMonMoi = Convert.ToInt32(cmdMonAn.ExecuteScalar());

                    // 2. Duyệt qua danh sách công thức tạm thời từ GUI gửi xuống và chèn vào bảng CONG_THUC
                    if (dtCongThuc != null && dtCongThuc.Rows.Count > 0)
                    {
                        string queryCongThuc = @"INSERT INTO CONG_THUC (TenCongThuc, MaMonAn, MaNguyenLieu, SoLuongHaoHut) 
                                         VALUES (@TenCT, @MaMon, @MaNL, @SoLuong)";

                        foreach (DataRow row in dtCongThuc.Rows)
                        {
                            // Bỏ qua nếu dòng đó đánh dấu bị xóa tạm thời trên giao diện
                            if (row.RowState == DataRowState.Deleted) continue;

                            SqlCommand cmdCT = new SqlCommand(queryCongThuc, conn, trans);
                            cmdCT.Parameters.Add(new SqlParameter("@TenCT", SqlDbType.NVarChar) { Value = "Công thức " + ma.TenMonAn });
                            cmdCT.Parameters.Add(new SqlParameter("@MaMon", SqlDbType.Int) { Value = maMonMoi });
                            cmdCT.Parameters.Add(new SqlParameter("@MaNL", SqlDbType.Int) { Value = row["MaNguyenLieu"] });
                            cmdCT.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Decimal) { Value = row["SoLuongHaoHut"] });

                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    // Nếu mọi thứ chạy mượt mà không lỗi -> Xác nhận lưu vĩnh viễn vào DB
                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // Nếu có bất kỳ lỗi gì (ví dụ: trùng mã, sai kiểu dữ liệu) -> Hủy bỏ toàn bộ dữ liệu vừa nạp ở cả 2 bảng
                    trans.Rollback();
                    return false;
                }
            }
        }

        // Cập nhật món ăn
        public bool CapNhatMonAn(MonAn ma)
        {
            string query = @"UPDATE MON_AN 
                            SET TenMonAn = @Ten, DonGia = @Gia, HinhAnh = @HinhAnh, MaDanhMuc = @MaDM 
                            WHERE MaMonAn = @Ma";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ma", SqlDbType.Int) { Value = ma.MaMonAn },
                new SqlParameter("@Ten", SqlDbType.NVarChar) { Value = ma.TenMonAn },
                new SqlParameter("@Gia", SqlDbType.Decimal) { Value = ma.DonGia },
                new SqlParameter("@HinhAnh", SqlDbType.VarChar) { Value = (object)ma.HinhAnh ?? System.DBNull.Value },
                new SqlParameter("@MaDM", SqlDbType.Int) { Value = (object)ma.MaDanhMuc ?? System.DBNull.Value }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa món ăn
        // Hàm xóa cứng (Xóa hoàn toàn khỏi DB)
        public bool XoaMonAnVatLy(int maMonAn)
        {
            using (SqlConnection conn = new SqlConnection(SQLServerDbContext.GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Xóa toàn bộ công thức liên quan đến món ăn trước để tránh dính khóa ngoại chính nó
                        string queryDeleteCongThuc = "DELETE FROM CONG_THUC WHERE MaMonAn = @MaMonAn";
                        using (SqlCommand cmd = new SqlCommand(queryDeleteCongThuc, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaMonAn", maMonAn);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Xóa món ăn ở bảng MON_AN
                        string queryDeleteMonAn = "DELETE FROM MON_AN WHERE MaMonAn = @MaMonAn";
                        using (SqlCommand cmd = new SqlCommand(queryDeleteMonAn, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaMonAn", maMonAn);
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

        // Hàm cập nhật trạng thái ngừng kinh doanh (Xóa mềm)
        public bool CapNhatNgungKinhDoanh(int maMonAn, bool trangThai)
        {
            // Giả sử bảng MON_AN của bạn có cột TrangThai (1: Đang bán, 0: Ngừng bán)
            string query = "UPDATE MON_AN SET TrangThaiKinhDoanh = @TrangThaiKinhDoanh WHERE MaMonAn = @MaMonAn";
            SqlParameter[] p = { new SqlParameter("@MaMonAn", maMonAn), new SqlParameter("@TrangThaiKinhDoanh", trangThai) };
            return DatabaseHelper.ExecuteNonQuery(query, p) > 0; // Trả về true nếu update thành công
        }
        public bool KiemTraTrungTenDM(string tenDM, int? maDM)
        {
            string query = @"SELECT COUNT(*) 
                            FROM DANH_MUC 
                            WHERE LOWER(TenDanhMuc) = LOWER(@TenDanhMuc) AND (@MaDanhMuc IS NULL OR @MaDanhMuc = '') 
                            OR 
                            (MaDanhMuc = @MaDanhMuc)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDanhMuc", SqlDbType.NVarChar) { Value = tenDM.Trim() },
                new SqlParameter("@MaDanhMuc", SqlDbType.Int) { Value = maDM == null ? DBNull.Value : (object)maDM}
            };

            // Thực thi và bốc ra ô đầu tiên của kết quả (COUNT)
            object result = DatabaseHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                int count = Convert.ToInt32(result);
                return count > 0; // Trả về true nếu count > 0 (bị trùng)
            }

            return false;
        }
        public bool KiemTraTrungTen(string tenMonAn, int maMonAn)
        {
            // Nếu maMonAn = 0: Check trùng toàn bảng
            // Nếu maMonAn > 0: Check trùng với các dòng khác, bỏ qua dòng của chính nó
            string query = @"SELECT COUNT(*) 
                            FROM MON_AN 
                            WHERE LOWER(TenMonAn) = LOWER(@TenMonAn) AND MaMonAn != @MaMonAn";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenMonAn", SqlDbType.NVarChar) { Value = tenMonAn.Trim() },
                new SqlParameter("@MaMonAn", SqlDbType.Int) { Value = maMonAn }
            };

            // Thực thi và bốc ra ô đầu tiên của kết quả (COUNT)
            object result = DatabaseHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                int count = Convert.ToInt32(result);
                return count > 0; // Trả về true nếu count > 0 (bị trùng)
            }

            return false;
        }

        public bool CapNhatDanhMuc(string tenDanhMuc, int maDanhMuc)
        {
            string query = @"UPDATE DANH_MUC SET TenDanhMuc = @TenDanhMuc WHERE MaDanhMuc = @MaDanhMuc";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDanhMuc", tenDanhMuc),
                new SqlParameter("@MaDanhMuc", maDanhMuc)
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool KiemTraTonTai(int maDanhMuc)
        {
            string query = "SELECT COUNT(*) FROM DANH_MUC WHERE MaDanhMuc = @MaDanhMuc";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDanhMuc", SqlDbType.Int) { Value = maDanhMuc }
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                int count = Convert.ToInt32(result);
                return count > 0; // Trả về true nếu tìm thấy danh mục
            }

            return false;
        }

        // Cập nhật món ăn và làm sạch - ghi lại công thức (Clear & Insert)
        public bool CapNhatMonAnKemCongThuc(MonAn ma, DataTable dtCongThuc)
        {
            string connectionString = SQLServerDbContext.GetConnectionString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    // 1. Cập nhật thông tin cơ bản của món ăn
                    string queryMonAn = @"UPDATE MON_AN 
                                  SET TenMonAn = @Ten, DonGia = @Gia, HinhAnh = @HinhAnh, MaDanhMuc = @MaDM 
                                  WHERE MaMonAn = @Ma";

                    SqlCommand cmdMonAn = new SqlCommand(queryMonAn, conn, trans);
                    cmdMonAn.Parameters.Add(new SqlParameter("@Ma", SqlDbType.Int) { Value = ma.MaMonAn });
                    cmdMonAn.Parameters.Add(new SqlParameter("@Ten", SqlDbType.NVarChar) { Value = ma.TenMonAn });
                    cmdMonAn.Parameters.Add(new SqlParameter("@Gia", SqlDbType.Decimal) { Value = ma.DonGia });
                    cmdMonAn.Parameters.Add(new SqlParameter("@HinhAnh", SqlDbType.VarChar) { Value = (object)ma.HinhAnh ?? DBNull.Value });
                    cmdMonAn.Parameters.Add(new SqlParameter("@MaDM", SqlDbType.Int) { Value = (object)ma.MaDanhMuc ?? DBNull.Value });

                    cmdMonAn.ExecuteNonQuery();

                    // 2. Áp dụng chiến lược Clear: Xóa sạch công thức cũ của món ăn này
                    string queryXoaCongThucCu = "DELETE FROM CONG_THUC WHERE MaMonAn = @MaMon";
                    SqlCommand cmdXoa = new SqlCommand(queryXoaCongThucCu, conn, trans);
                    cmdXoa.Parameters.Add(new SqlParameter("@MaMon", SqlDbType.Int) { Value = ma.MaMonAn });
                    cmdXoa.ExecuteNonQuery();

                    // 3. Áp dụng chiến lược Insert: Duyệt DataTable mới và chèn lại từ đầu
                    if (dtCongThuc != null && dtCongThuc.Rows.Count > 0)
                    {
                        string queryInsertCT = @"INSERT INTO CONG_THUC (TenCongThuc, MaMonAn, MaNguyenLieu, SoLuongHaoHut) 
                                         VALUES (@TenCT, @MaMon, @MaNL, @SoLuong)";

                        foreach (DataRow row in dtCongThuc.Rows)
                        {
                            if (row.RowState == DataRowState.Deleted) continue;

                            SqlCommand cmdCT = new SqlCommand(queryInsertCT, conn, trans);
                            cmdCT.Parameters.Add(new SqlParameter("@TenCT", SqlDbType.NVarChar) { Value = "Công thức " + ma.TenMonAn });
                            cmdCT.Parameters.Add(new SqlParameter("@MaMon", SqlDbType.Int) { Value = ma.MaMonAn });
                            cmdCT.Parameters.Add(new SqlParameter("@MaNL", SqlDbType.Int) { Value = row["MaNguyenLieu"] });
                            cmdCT.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Decimal) { Value = row["SoLuongHaoHut"] });

                            cmdCT.ExecuteNonQuery();
                        }
                    }

                    trans.Commit();
                    return true;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    return false;
                }
            }
        }
    }
}
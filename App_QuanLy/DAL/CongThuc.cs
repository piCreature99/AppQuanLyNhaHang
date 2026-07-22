using System.Data;
using App_QuanLy.Data;
using App_QuanLy.Models;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.DAL
{
    public class CongThucDAL
    {
        // Lấy chi tiết các nguyên liệu cấu thành 1 món ăn cụ thể
        public DataTable LayCongThucTheoMon(int maMonAn)
        {
            string query = @"SELECT CT.MaCongThuc, CT.TenCongThuc, NL.TenNguyenLieu, CT.SoLuongHaoHut, NL.DonViTinh, CT.MaNguyenLieu
                            FROM CONG_THUC CT
                            JOIN NGUYEN_LIEU NL ON CT.MaNguyenLieu = NL.MaNguyenLieu
                            WHERE CT.MaMonAn = @MaMonAn";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaMonAn", SqlDbType.Int) { Value = maMonAn }
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // Khai báo công thức (Dùng DECIMAL(18,3) cho hao hụt)
        public bool ThemCongThuc(CongThuc ct)
        {
            string query = @"INSERT INTO CONG_THUC (TenCongThuc, MaMonAn, MaNguyenLieu, SoLuongHaoHut) 
                            VALUES (@Ten, @MaMA, @MaNL, @HaoHut)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ten", SqlDbType.NVarChar) { Value = ct.TenCongThuc },
                new SqlParameter("@MaMA", SqlDbType.Int) { Value = ct.MaMonAn },
                new SqlParameter("@MaNL", SqlDbType.Int) { Value = ct.MaNguyenLieu },
                new SqlParameter("@HaoHut", SqlDbType.Decimal) { Value = ct.SoLuongHaoHut }
            };

            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        // Xóa một thành phần nguyên liệu ra khỏi công thức món
        public bool XoaThandPhanCongThuc(int maCongThuc)
        {
            string query = "DELETE FROM CONG_THUC WHERE MaCongThuc = @Ma";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Ma", SqlDbType.Int) { Value = maCongThuc }
            };
            return DatabaseHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}
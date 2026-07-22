using System.Data;
using App_Shipper.Data;
using App_Shipper.DTOS;
using App_Shipper.Models;
using Microsoft.Data.SqlClient;

namespace App_Shipper.DAL
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

        public List<DanhMucDTO> LayDanhSachThucDonGop()
        {
            // 1. Câu lệnh SQL JOIN lấy đầy đủ các trường của danh mục và món ăn
            string query = @"
                SELECT 
                    d.MaDanhMuc, d.TenDanhMuc, d.CanNL,
                    m.MaMonAn, m.TenMonAn, m.DonGia, m.HinhAnh, m.TrangThaiKinhDoanh
                FROM DANH_MUC d
                INNER JOIN MON_AN m ON d.MaDanhMuc = m.MaDanhMuc
                WHERE m.TrangThaiKinhDoanh = 1"; // Chỉ lấy những món đang kinh doanh

            // 2. Gọi Helper để hứng dữ liệu vào danh sách phẳng hoàn toàn
            List<MonAnFlattenerDTO> dsPhang = DatabaseHelper.ExecuteQuery<MonAnFlattenerDTO>(query);

            // Đây là List kết quả cuối cùng chứa cấu trúc phân cấp chuẩn
            List<DanhMucDTO> dsThucDonKetQua = new List<DanhMucDTO>();

            if (dsPhang == null || dsPhang.Count == 0) return dsThucDonKetQua;

            // Dictionary dùng để "vừa duyệt vừa gom nhóm" Danh Mục theo MaDanhMuc
            Dictionary<int, DanhMucDTO> dicDanhMuc = new Dictionary<int, DanhMucDTO>();

            // 3. VÒNG LẶP DUYỆT BẢNG PHẲNG ĐỂ GỘP LẠI THEO ĐÚNG TƯ DUY OOP (Y hệt cách làm của đơn hàng)
            foreach (var dong in dsPhang)
            {
                // Kiểm tra xem danh mục này đã được khởi tạo trong Dictionary chưa
                if (!dicDanhMuc.ContainsKey(dong.MaDanhMuc))
                {
                    // Lần đầu tiên thấy mã danh mục này -> Khởi tạo Object cha (DanhMucDTO)
                    DanhMucDTO danhMucMoi = new DanhMucDTO
                    {
                        MaDanhMuc = dong.MaDanhMuc,
                        TenDanhMuc = dong.TenDanhMuc,
                        CanNL = dong.CanNL,
                        DanhSachMonAn = new List<MonAnDTO>() // Khởi tạo list rỗng sẵn sàng hứng con
                    };

                    dicDanhMuc.Add(dong.MaDanhMuc, danhMucMoi);
                    dsThucDonKetQua.Add(danhMucMoi); // Thêm luôn vào danh sách kết quả trả về
                }

                // Danh mục cha chắc chắn đã tồn tại (hoặc mới tạo, hoặc lấy lại từ Dictionary)
                DanhMucDTO danhMucCha = dicDanhMuc[dong.MaDanhMuc];

                // Khởi tạo đối tượng con (MonAnDTO)
                MonAnDTO monAnCon = new MonAnDTO
                {
                    MaMonAn = dong.MaMonAn,
                    TenMonAn = dong.TenMonAn,
                    DonGia = dong.DonGia,
                    HinhAnh = dong.HinhAnh,
                    MaDanhMuc = dong.MaDanhMuc,
                    TrangThaiKinhDoanh = dong.TrangThaiKinhDoanh
                };

                // Gộp phần tử con này vào danh sách của danh mục cha tương ứng
                danhMucCha.DanhSachMonAn.Add(monAnCon);
            }

            return dsThucDonKetQua;
        }
    }
}
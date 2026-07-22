using System;
using System.Data;
using App_NguoiDung.Data;
using App_NguoiDung.DTOS;

//using App_NguoiDung.DTOS;
using App_NguoiDung.Models;
using Microsoft.Data.SqlClient;

namespace App_NguoiDung.DAL
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

        public DataTable PollTrangThaiDonHangMoiNhat(string maNguoiDung)
        {
            string query = @"SELECT dh.MaDonHang as MaDonHang, sp.HoTen as TenShipper, dh.TrangThaiDonHang as TrangThaiDonHang
                             FROM DON_HANG dh
                             LEFT JOIN TAI_KHOAN sp ON dh.MaShipper = sp.MaTaiKhoan
                             WHERE dh.maKhachHang = @maKhachHang";
            //WHERE NgayDat >= CAST(GETDATE() AS DATE)";
            SqlParameter[] parameters = new SqlParameter[]
            {
              new  SqlParameter("@maKhachHang", maNguoiDung)
            };
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public DataTable LayTongSoHoaDon()
        {
            string query = "SELECT COUNT(*) FROM DON_HANG";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public string LayTrangThaiDonHang(string maDonHang)
        {
            string query = @"SELECT TrangThaiDonHang FROM DON_HANG WHERE MaDonHang = @MaDonHang";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDonHang", maDonHang)
            };

            // Thực thi và ép kiểu an toàn bằng 'as string'
            object result = DatabaseHelper.ExecuteScalar(query, parameters);

            // Nếu kết quả là null hoặc DBNull, trả về chuỗi rỗng "" (hoặc "Không xác định") để tầng BLL không bị lỗi
            return (result == null || result == DBNull.Value) ? string.Empty : result.ToString();
        }

        public bool HuyDonHang(string maDonHang)
        {
            string query = @"UPDATE DON_HANG SET TrangThaiDonHang = @TrangThaiDonHang WHERE MaDonHang = @MaDonHang";

            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@TrangThaiDonHang", "Đã hủy"),
                new SqlParameter("@MaDonHang", maDonHang)
            };

            return DatabaseHelper.ExecuteNonQuery (query, parameter) > 0;
        }

        public List<DonHangDTO> LayDanhSachDonHangGop(string maKhachHang)
        {
            // 1. Câu lệnh SQL JOIN lấy đầy đủ tất cả các trường ứng với DonHangFlattenerDTO
            string query = @"
                SELECT 
                    dh.MaDonHang, dh.MaKhachHang, dh.MaShipper, kh.HoTen as TenKhachHang, kh.SoDienThoai as SDTKhachHang, kh.DiaChi as DiaChiKhachHang, kh.HinhAnh as HinhAnhKhachHang, sp.HoTen as TenShipper, sp.SoDienThoai as SDTShipper, sp.HinhAnh as HinhAnhShipper, dh.NgayDat, dh.TongTien, dh.DiaChiGiaoHang, dh.TrangThaiDonHang,
                    ct.MaChiTiet, ct.MaMonAn, ct.SoLuong, ct.GiaBan,
                    m.TenMonAn, m.DonGia, m.HinhAnh, m.MaDanhMuc, m.TrangThaiKinhDoanh
                FROM DON_HANG dh
                INNER JOIN CHI_TIET_DON_HANG ct ON dh.MaDonHang = ct.MaDonHang
                INNER JOIN MON_AN m ON ct.MaMonAn = m.MaMonAn
                INNER JOIN TAI_KHOAN kh ON kh.MaTaiKhoan = dh.MaKhachHang
                LEFT JOIN TAI_KHOAN sp ON sp.MaTaiKhoan = dh.MaShipper
                WHERE MaKhachHang = @MaKhachHang
                ORDER BY dh.NgayDat DESC"; // Sắp xếp theo ngày đặt mới nhất

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaKhachHang", maKhachHang),
            };
            // 2. Gọi Helper để hứng dữ liệu vào danh sách phẳng hoàn toàn
            List<DonHangFlattenerDTO> dsPhang = DatabaseHelper.ExecuteQuery<DonHangFlattenerDTO>(query, parameters);

            // Đây là List kết quả cuối cùng chứa cấu trúc phân cấp chuẩn
            List<DonHangDTO> dsDonHangKetQua = new List<DonHangDTO>();

            if (dsPhang == null || dsPhang.Count == 0) return dsDonHangKetQua;

            // Dictionary dùng để "vừa duyệt vừa gom nhóm" Đơn Hàng theo MaDonHang, tránh bị lặp thông tin chung
            Dictionary<string, DonHangDTO> dicDonHang = new Dictionary<string, DonHangDTO>();

            // 3. VÒNG LẶP DUYỆT BẢNG PHẲNG ĐỂ GỘP LẠI THEO ĐÚNG TƯ DUY OOP
            foreach (var dong in dsPhang)
            {
                // Kiểm tra xem đơn hàng này đã được khởi tạo trong Dictionary chưa
                if (!dicDonHang.ContainsKey(dong.MaDonHang))
                {
                    // Lần đầu tiên thấy mã đơn hàng này -> Khởi tạo Object cha (DonHangDTO)
                    DonHangDTO donHangMoi = new DonHangDTO
                    {
                        MaDonHang = dong.MaDonHang,
                        MaKhachHang = dong.MaKhachHang,
                        MaShipper = dong.MaShipper,
                        TenKhachHang = dong.TenKhachHang,
                        SDTKhachHang = dong.SDTKhachHang,
                        DiaChiKhachHang = dong.DiaChiKhachHang,
                        HinhAnhKhachHang = dong.HinhAnhKhachHang,
                        TenShipper = dong.TenShipper,
                        SDTShipper = dong.SDTShipper,
                        HinhAnhShipper = dong.HinhAnhShipper,
                        NgayDat = dong.NgayDat,
                        TongTien = dong.TongTien,
                        DiaChiGiaoHang = dong.DiaChiGiaoHang,
                        TrangThaiDonHang = dong.TrangThaiDonHang,
                        DanhSachChiTiet = new List<ChiTietDonHangDTO>() // Khởi tạo list rỗng sẵn sàng hứng con
                    };

                    dicDonHang.Add(dong.MaDonHang, donHangMoi);
                    dsDonHangKetQua.Add(donHangMoi); // Thêm luôn vào danh sách kết quả trả về
                }

                // Đơn hàng cha chắc chắn đã tồn tại (hoặc mới tạo, hoặc lấy lại từ Dictionary)
                DonHangDTO donHangCha = dicDonHang[dong.MaDonHang];

                

                MonAnDTO monAnCon = new MonAnDTO
                {
                    MaMonAn = dong.MaMonAn,
                    TenMonAn = dong.TenMonAn,
                    DonGia = dong.DonGia,
                    HinhAnh = dong.HinhAnh,
                    MaDanhMuc = dong.MaDanhMuc,
                    TrangThaiKinhDoanh = dong.TrangThaiKinhDoanh
                };

                // Khởi tạo lớp con thứ nhất: ChiTietDonHangDTO
                ChiTietDonHangDTO chiTietCon = new ChiTietDonHangDTO
                {
                    MaChiTiet = dong.MaChiTiet,
                    MaDonHang = dong.MaDonHang,
                    MaMonAn = dong.MaMonAn,
                    TenMonAn = dong.TenMonAn,
                    SoLuong = dong.SoLuong,
                    GiaBan = dong.GiaBan,

                    // Gán trực tiếp đối tượng món ăn vừa tạo ở trên vào đây
                    MonAn = monAnCon
                };

                // Gộp phần tử con này vào danh sách của đơn hàng cha tương ứng
                donHangCha.DanhSachChiTiet.Add(chiTietCon);
            }

            return dsDonHangKetQua;
        }

        public bool TaoDonHangVaChiTiet(string maDonHang, string maKhachHang, decimal tongTien, string diaChiGiaoHang, List<ChiTietGioHangDTO> danhSachMonAn)
        {
            // Danh sách chứa các lệnh SQL chạy kèm trong một Transaction
            var commands = new List<Tuple<string, SqlParameter[]>>();

            // 1. Chuẩn bị câu lệnh và bộ tham số tối giản cho DON_HANG
            string sqlDonHang = @"
                INSERT INTO DON_HANG (MaDonHang, MaKhachHang, NgayDat, TongTien, DiaChiGiaoHang, TrangThaiDonHang) 
                VALUES (@MaDonHang, @MaKhachHang, @NgayDat, @TongTien, @DiaChiGiaoHang, @TrangThaiDonHang)";

            SqlParameter[] paramDonHang = new SqlParameter[]
            {
                new SqlParameter("@MaDonHang", maDonHang),
                new SqlParameter("@MaKhachHang", (object)maKhachHang ?? DBNull.Value),
                new SqlParameter("@NgayDat", DateTime.Now),
                new SqlParameter("@TongTien", tongTien),
                new SqlParameter("@DiaChiGiaoHang", (object)diaChiGiaoHang ?? DBNull.Value),
                new SqlParameter("@TrangThaiDonHang", "Chờ duyệt") // Trạng thái mặc định ban đầu
            };

            commands.Add(new Tuple<string, SqlParameter[]>(sqlDonHang, paramDonHang));

            // 2. Chuẩn bị các câu lệnh và bộ tham số tối giản cho từng món trong CHI_TIET_DON_HANG
            foreach (var item in danhSachMonAn)
            {
                string sqlChiTiet = @"
                    INSERT INTO CHI_TIET_DON_HANG (MaDonHang, MaMonAn, SoLuong, GiaBan) 
                    VALUES (@MaDonHang, @MaMonAn, @SoLuong, @GiaBan)";

                SqlParameter[] paramChiTiet = new SqlParameter[]
                {
                    new SqlParameter("@MaDonHang", maDonHang),
                    new SqlParameter("@MaMonAn", item.MaMonAn),
                    new SqlParameter("@SoLuong", item.SoLuong),
                    new SqlParameter("@GiaBan", item.DonGia)
                };

                commands.Add(new Tuple<string, SqlParameter[]>(sqlChiTiet, paramChiTiet));
            }

            // 3. Thực thi transaction đồng thời
            return DatabaseHelper.ExecuteTransaction(commands);
        }
    }
}
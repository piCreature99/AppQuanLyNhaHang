using System;
using System.Data;
using System.Text;
using App_NguoiDung.DAL;
using App_NguoiDung.DTOS;
//using App_NguoiDung.DTOS;

namespace App_NguoiDung.BLL
{
    public class DonHangBLL
    {
        private DonHangDAL _donHangDAL = new DonHangDAL();

        // PHỤC VỤ TIMER POLLING TRÊN UI (Mỗi 5 giây gọi hàm này một lần)
        public DataTable DocDonHangMoi()
        {
            // Trả về danh sách đơn 'ChoXacNhan' để Admin nổ thông báo lên màn hình WinForms
            return _donHangDAL.LayDonHangTheoTrangThai("ChoXacNhan");
        }


        public Dictionary<string, Tuple<string, string>> PollTrangThaiDonHangMoiNhat(string maNguoiDung)
        {
            // 1. Gọi xuống DAL để bốc dữ liệu thô từ SQL Server lên
            DataTable dt = _donHangDAL.PollTrangThaiDonHangMoiNhat(maNguoiDung);

            // 2. Khởi tạo một Dictionary rỗng để hứng dữ liệu chuyển đổi
            Dictionary<string, Tuple<string, string>> dictTrangThai = new Dictionary<string, Tuple<string, string>>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string maDonHang = row["MaDonHang"].ToString();
                    string trangThai = row["TrangThaiDonHang"] != DBNull.Value ? row["TrangThaiDonHang"].ToString() : "";
                    string tenShipper = row["TenShipper"] != DBNull.Value ? row["TenShipper"].ToString() : "";
                    // Đưa vào dictionary (Kiểm tra tránh trùng lặp Key an toàn)
                    if (!dictTrangThai.ContainsKey(maDonHang))
                    {
                        dictTrangThai.Add(maDonHang, Tuple.Create(trangThai, tenShipper));
                    }
                }
            }

          
            return dictTrangThai;
        }

        public List<DonHangDTO> LayDanhSachDonHangGop(string maKhachHang)
        {
            if (string.IsNullOrEmpty(maKhachHang))
                throw new ArgumentNullException("Mã khách hàng không hợp lệ");


            // 2. Gọi tầng DAL để lấy danh sách đã được gom nhóm sẵn sàng
            List<DonHangDTO> danhSach = _donHangDAL.LayDanhSachDonHangGop(maKhachHang);

            // 3. Trả kết quả về cho tầng UI
            return danhSach;
        }

        public DataTable LayTongSoHoaDon()
        {
            return _donHangDAL.LayTongSoHoaDon();
        }

        public bool HuyDonHang(string maDonHang)
        {
            if (string.IsNullOrEmpty(maDonHang))
                throw new ArgumentNullException("Vui lòng chọn một hóa đơn từ bảng");

            string trangThai = _donHangDAL.LayTrangThaiDonHang(maDonHang);

            if (string.IsNullOrEmpty(trangThai))
                throw new ArgumentException("Không xác định được trạng thái của đơn hàng");

            if(trangThai != "Chờ duyệt")
            {
                throw new ArgumentException("Không thể hủy hóa đơn, trạng thái không hợp lệ");
            }


            return _donHangDAL.HuyDonHang(maDonHang);
        }

        public bool DatHangOffline(string maDonHang, string maKhachHang, string diaChiGiaoHang, List<ChiTietGioHangDTO> gioHang)
        {
            // 1. Kiểm tra mã hóa đơn hợp lệ
            if (string.IsNullOrEmpty(maDonHang))
                throw new ArgumentNullException("Mã hóa đơn không hợp lệ hoặc không tự sinh được");

            // 2. Kiểm tra thông tin địa chỉ
            if (string.IsNullOrWhiteSpace(diaChiGiaoHang))
                throw new ArgumentException("Vui lòng nhập địa chỉ giao hàng");

            // 3. Kiểm tra giỏ hàng rỗng
            if (gioHang == null || gioHang.Count == 0)
                throw new ArgumentException("Giỏ hàng đang trống, không thể tiến hành đặt hàng");

            // 4. Tính toán tổng tiền thực tế
            decimal tongTien = 0;
            foreach (var item in gioHang)
            {
                if (item.SoLuong <= 0)
                    throw new ArgumentException($"Số lượng của món '{item.TenMon}' phải lớn hơn 0");

                tongTien += item.ThanhTien;
            }

            if (tongTien <= 0)
                throw new ArgumentException("Tổng trị giá đơn hàng phải lớn hơn 0");

            // 5. Gọi tầng DAL thực thi ghi xuống DB
            return _donHangDAL.TaoDonHangVaChiTiet(maDonHang, maKhachHang, tongTien, diaChiGiaoHang, gioHang);
        }
    }
}
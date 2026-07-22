using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_NguoiDung.DTOS
{
    public class DonHangFlattenerDTO
    {
        // 1. Đầy đủ các cột của bảng DON_HANG
        public string MaDonHang { get; set; }
        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string SDTKhachHang { get; set; }
        public string DiaChiKhachHang { get; set; }
        public string HinhAnhKhachHang { get; set; }
        public string MaShipper { get; set; }
        public string TenShipper { get; set; }
        public string SDTShipper { get; set; }
        public string HinhAnhShipper { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string TrangThaiDonHang { get; set; }

        // 2. Đầy đủ các cột của bảng CHI_TIET_DON_HANG
        public int MaChiTiet { get; set; }
        public int MaMonAn { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }

        // 3. Đầy đủ các cột của bảng MON_AN (Phần bị thiếu lúc nãy)
        public string TenMonAn { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        public int MaDanhMuc { get; set; }
        public bool TrangThaiKinhDoanh { get; set; }
    }
}

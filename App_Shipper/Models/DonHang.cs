using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class DonHang
    {
        public string MaDonHang { get; set; } // Khóa chính (Chuỗi)
        public string MaKhachHang { get; set; }
        public string MaShipper { get; set; }
        public DateTime NgayDat { get; set; }
        public decimal TongTien { get; set; }
        public string DiaChiGiaoHang { get; set; }
        public string TrangThaiDonHang { get; set; } // 'ChoXacNhan', 'DangChuanBi', 'DangGiao', 'DaGiao', 'Huy'
    }
}

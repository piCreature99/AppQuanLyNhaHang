using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class TaiKhoan
    {
        public string MaTaiKhoan { get; set; } // Khóa chính
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; } // 'admin', 'client', 'shipper'
        public string HinhAnh { get; set; } = "";
        public string TrangThai { get; set; }
        public bool TrangThaiLamViec { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }

    }
}

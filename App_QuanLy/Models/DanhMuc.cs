using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class DanhMuc
    {
        public string MaTaiKhoan { get; set; } // Khóa chính
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; } // 'admin', 'client', 'shipper'
    }
}

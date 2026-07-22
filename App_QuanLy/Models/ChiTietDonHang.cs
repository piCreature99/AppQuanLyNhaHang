using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class ChiTietDonHang
    {
        public int MaChiTiet { get; set; } // IDENTITY
        public string MaDonHang { get; set; }
        public int MaMonAn { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaBan { get; set; }
    }
}

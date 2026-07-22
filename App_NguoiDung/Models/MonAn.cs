using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class MonAn
    {
        public int MaMonAn { get; set; } // IDENTITY
        public string TenMonAn { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }

        public bool TrangThaiKinhDoanh { get; set; }
        public int? MaDanhMuc { get; set; } // Cho phép null nếu món ăn chưa xếp mục
    }
}

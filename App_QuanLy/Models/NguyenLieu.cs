using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class NguyenLieu
    {
        public int MaNguyenLieu { get; set; } // IDENTITY
        public string TenNguyenLieu { get; set; }
        public decimal SoLuongTon { get; set; } // Đổi sang decimal
        public string DonViTinh { get; set; }
        public decimal? GiaNhapGanNhat { get; set; } // Nullable decimal
        public decimal TonToiThieu { get; set; }
        public string TrangThai { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace App_QuanLy.Models
{
    public class CongThuc
    {
        public int MaCongThuc { get; set; } // IDENTITY
        public string TenCongThuc { get; set; }
        public int MaMonAn { get; set; }
        public int MaNguyenLieu { get; set; }
        public decimal SoLuongHaoHut { get; set; } // Định lượng hao hụt khi làm món
    }
}

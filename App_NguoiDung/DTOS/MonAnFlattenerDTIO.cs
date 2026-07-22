using System;

namespace App_NguoiDung.DTOS
{
    public class MonAnFlattenerDTO
    {
        // --- Cột của bảng DANH_MUC ---
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public bool CanNL { get; set; } // Cần nguyên liệu

        // --- Cột của bảng MON_AN ---
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public decimal DonGia { get; set; }
        public string HinhAnh { get; set; }
        public bool TrangThaiKinhDoanh { get; set; }
    }
}
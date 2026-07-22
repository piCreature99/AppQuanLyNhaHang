using System;
using System.Collections.Generic;
using System.Text;

namespace App_Shipper.DTOS
{
    public class ChiTietGioHangDTO
    {
        public int MaMonAn { get; set; }
        public string TenMon { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        // Thành tiền tự động tính dựa trên Số lượng và Đơn giá
        public decimal ThanhTien => SoLuong * DonGia;
    }
}

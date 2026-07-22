using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_NguoiDung.DTOS
{
    public class ChiTietDonHangDTO
    {
        [Browsable(false)]
        public int MaChiTiet { get; set; }

        [Browsable(false)]
        public string MaDonHang { get; set; }

        [Browsable(false)]
        public int MaMonAn { get; set; }

        [DisplayName("Tên Món Ăn")]
        public string TenMonAn { get; set; }

        [DisplayName("Số Lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Giá Bán")]
        public decimal GiaBan { get; set; }

        [DisplayName("Thành tiền")]
        public decimal ThanhTien => SoLuong * GiaBan;
        // Ẩn đối tượng lồng bên trong để DataGridView không hiển thị nhầm dữ liệu hệ thống
        [Browsable(false)]
        public MonAnDTO MonAn { get; set; }

    }
}

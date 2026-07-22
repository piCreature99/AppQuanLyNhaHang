using System;
using System.Collections.Generic;
using System.Text;

namespace App_NguoiDung.DTOS
{
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public bool CanNL { get; set; }
        public List<MonAnDTO> DanhSachMonAn { get; set; } = new List<MonAnDTO>();
    }
}

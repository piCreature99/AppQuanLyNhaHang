using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_Shipper.Models
{
    public class TaiKhoan
    {
        public string MaTaiKhoan { get; set; } // Khóa chính
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string VaiTro { get; set; } // 'admin', 'client', 'shipper'
        private string _hinhAnh = "";
        public string HinhAnh {
            get { return _hinhAnh; }
            set
            {
                // 1. Kiểm tra an toàn: Nếu dữ liệu trống thì gán mặc định, không cắt để tránh lỗi crash
                if (string.IsNullOrEmpty(value))
                {
                    _hinhAnh = "";
                    return;
                }

                // 2. Kiểm tra xem chuỗi truyền vào có chứa đường dẫn thư mục hay không
                // Nếu có dấu \ hoặc / thì mới dùng Path.GetFileName để cắt lấy tên file sạch
                if (value.Contains("\\") || value.Contains("/"))
                {
                    _hinhAnh = Path.GetFileName(value);
                }
                else
                {
                    // Nếu chuỗi truyền vào vốn đã là tên file sạch (ví dụ: "img.jpg"), giữ nguyên luôn
                    _hinhAnh = value;
                }
            }
        }
        public string TrangThai { get; set; }
        public bool TrangThaiLamViec { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }

    }
}

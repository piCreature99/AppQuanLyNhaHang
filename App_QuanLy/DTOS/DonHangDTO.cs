using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_QuanLy.DTOS
{
    // Class chính (Root)
    public class DonHangDTO
    {
        public string MaDonHang { get; set; }

        public string MaDonHienThi
        {
            get
            {
                if (string.IsNullOrEmpty(MaDonHang)) return "";
                // Ví dụ: DH20260714-151020-9999 (Tách ra bằng dấu gạch ngang)
                if (MaDonHang.Length >= 10)
                {
                    return MaDonHang.Insert(10, " ");
                }
                return MaDonHang;
            }
        }
        public string TrangThaiDonHang { get; set; }

        public string MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public string HinhAnhKhach { get; set; }
        public string SoDienThoaiKhach { get; set; }
        public string DiaChiGiaoHang { get; set; }

        public string MaShipper { get; set; }
        public string TenShipper { get; set; }
        public string SoDienThoaiShipper { get; set; }

        private string _hinhAnhShipper;
        [Browsable(false)]
        public string HinhAnhShipper
        {
            get { return _hinhAnhShipper; }
            set
            {
                // 1. Kiểm tra an toàn: Nếu dữ liệu trống thì gán mặc định, không cắt để tránh lỗi crash
                if (string.IsNullOrEmpty(value))
                {
                    _hinhAnhShipper = "";
                    return;
                }

                // 2. Kiểm tra xem chuỗi truyền vào có chứa đường dẫn thư mục hay không
                // Nếu có dấu \ hoặc / thì mới dùng Path.GetFileName để cắt lấy tên file sạch
                if (value.Contains("\\") || value.Contains("/"))
                {
                    _hinhAnhShipper = Path.GetFileName(value);
                }
                else
                {
                    // Nếu chuỗi truyền vào vốn đã là tên file sạch (ví dụ: "img.jpg"), giữ nguyên luôn
                    _hinhAnhShipper = value;
                }
            }
        }

        public DateTime NgayDat { get; set; }

        public decimal TongTien { get; set; }
        public Dictionary<int, ChiTietMonAnDTO> MapMonAn { get; set; } = new Dictionary<int, ChiTietMonAnDTO>();
    }

    // Bạn có thể viết các class vệ tinh ngay bên dưới trong cùng một file cho tiện quản lý
    public class ChiTietMonAnDTO
    {
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public int SoLuongDat { get; set; }
        public decimal GiaBan { get; set; }
        public decimal ThanhTien => SoLuongDat * GiaBan;
        public Dictionary<int, NguyenLieuTieuHao> MapNguyenLieu { get; set; } = new Dictionary<int, NguyenLieuTieuHao>();
    }

    public class NguyenLieuTieuHao
    {
        public int MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public decimal TongNguyenLieuCan { get; set; }
        public decimal TonKhoHienTai { get; set; }
        public string DonViTinh { get; set; }
    }
}

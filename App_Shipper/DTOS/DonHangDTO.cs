using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App_Shipper.DTOS
{
    public class DonHangDTO
    {
        [Browsable(false)]
        // --- Thông tin chung của đơn hàng (Không bị lặp) ---
        public string MaDonHang { get; set; }

        [DisplayName("Mã Đơn")]
        public string MaDonHangHienThi
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

        [Browsable(false)]
        public string MaKhachHang { get; set; }

        [Browsable(false)]
        public string MaShipper { get; set; }

        [DisplayName("Ten Khách Hàng")]
        public string TenKhachHang { get; set; }

        [Browsable(false)]
        public string SDTKhachHang { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChiKhachHang { get; set; }

        [Browsable(false)]
        public string HinhAnhKhachHang{ get; set; }

        [Browsable(false)]
        public string TenShipper { get; set; }

        [Browsable(false)]
        public string SDTShipper { get; set; }

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

        [DisplayName("Ngày Đặt Hàng")]
        public DateTime NgayDat { get; set; }

        [DisplayName("Tổng Tiền Thu")]
        public decimal TongTien { get; set; }

        [Browsable(false)]
        public string DiaChiGiaoHang { get; set; }

        [DisplayName("Trạng Thái Đơn Hàng")]
        public string TrangThaiDonHang { get; set; }

        // Thẻ Browsable(false) giúp ẩn luôn cột này trên DataGridView, không sợ bị hiện ô trống lỗi
        [Browsable(false)]
        public List<ChiTietDonHangDTO> DanhSachChiTiet { get; set; } // 'ChoXacNhan', 'DangChuanBi', 'DangGiao', 'DaGiao', 'Huy'

        // --- Danh sách các món ăn chi tiết thuộc đơn hàng này ---
        // Khởi tạo sẵn new List để tránh lỗi NullReferenceException khi dùng
    }
}

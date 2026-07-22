using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using App_Shipper.Helpers;

namespace App_Shipper.DTOS
{
    public class MonAnDTO
    {
        private HelperFunctions _helperFuncs;

        [Browsable(false)]
        public int MaMonAn { get; set; }

        [DisplayName("Tên Món Ăn")]
        public string TenMonAn { get; set; }

        [DisplayName("Đơn Giá")]
        public decimal DonGia { get; set; }

        //[DisplayName("Link Ảnh")]
        [Browsable(false)]
        public string HinhAnh { get; set; }

        [Browsable(false)]
        public int MaDanhMuc { get; set; }

        [Browsable(false)]
        public bool TrangThaiKinhDoanh { get; set; }

        private Image _hinhAnhHienThi = null;

        [DisplayName(" ")]
        public Image HinhAnhHienThi
        {
            get
            {
                if (_hinhAnhHienThi != null)
                {
                    return _hinhAnhHienThi;
                }

                _helperFuncs = new HelperFunctions();

                string tenHinhAnh = HinhAnh;
                if (!string.IsNullOrEmpty(tenHinhAnh) && tenHinhAnh.Contains("\\") || tenHinhAnh.Contains("/"))
                {
                    tenHinhAnh = Path.GetFileName(tenHinhAnh);
                }

                string duongDanDich = Path.Combine(Application.StartupPath, "Images\\FoodItems");
                string duongDanAnh = Path.Combine(duongDanDich, tenHinhAnh == null ? "" : tenHinhAnh);
                string duongDanAnhDefault = Path.Combine(duongDanDich, "default.png");

                if (!Directory.Exists(duongDanDich))
                {
                    Directory.CreateDirectory(duongDanDich);
                }

                // Nếu chưa nạp ảnh và có tên file, tiến hành nạp ngầm qua Stream
                if ((_hinhAnhHienThi == null && string.IsNullOrEmpty(tenHinhAnh)) || !File.Exists(duongDanAnh))
                {
                    try
                    {
                        //System.Diagnostics.Debug.WriteLine(!File.Exists(duongDanAnh));
                        _hinhAnhHienThi = _helperFuncs.LoadImage(duongDanAnhDefault);
                    }
                    catch
                    {
                        _hinhAnhHienThi = null; // Phòng hờ file default.png bị lỗi vật lý
                    }
                }
                else
                {

                    try
                    {
                        _hinhAnhHienThi = _helperFuncs.LoadImage(duongDanAnh);
                    }
                    catch
                    {
                        // Nếu file ảnh món ăn bị lỗi định dạng, tự động fallback về ảnh default luôn cho đẹp UI
                        try
                        {
                            _hinhAnhHienThi = _helperFuncs.LoadImage(duongDanAnhDefault);
                        }
                        catch
                        {
                            _hinhAnhHienThi = null;
                        }
                    }
                }
                return _hinhAnhHienThi;
            }
            set
            {
                // Nếu ảnh cũ đang tồn tại và giá trị mới gán vào khác ảnh cũ (hoặc gán bằng null)
                if (_hinhAnhHienThi != null && _hinhAnhHienThi != value)
                {
                    _hinhAnhHienThi.Dispose(); // Tự động hủy vùng nhớ RAM của ảnh cũ ngay lập tức
                    _hinhAnhHienThi = null;
                }
                _hinhAnhHienThi = value;
            }
        }
    }
}

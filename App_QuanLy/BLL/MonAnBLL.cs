using System.Data;
using System.Text.RegularExpressions;
using App_QuanLy.DAL;
using App_QuanLy.Data;
using App_QuanLy.Models;

namespace App_QuanLy.BLL
{
    public class MonAnBLL
    {
        private MonAnDAL _monAnDAL = new MonAnDAL();

        public DataTable LayDanhSachMonAn(string tuKhoa, int danhMuc)
        {
            // Nếu từ khóa null, chuyển thành chuỗi rỗng để tránh lỗi SQL LIKE
            return _monAnDAL.LayDanhSachMonAn(tuKhoa ?? "", danhMuc);
        }

        public DataTable LayCongThucTheoMon(int maMonAn)
        {
            return _monAnDAL.LayCongThucTheoMon(maMonAn);
        }

        public string ThemMonAn(MonAn ma, DataTable dtCongThuc, bool canNguyenLieu)
        {

            // 1. Ràng buộc về Tên món ăn
            if (string.IsNullOrWhiteSpace(ma.TenMonAn))
            {
                return "Tên món ăn không được để trống!";
            }
            string patternNguyenLieu = @"^[\p{L}\d\s_.,()\[\]-]+$";
            if (!Regex.IsMatch(ma.TenMonAn, patternNguyenLieu))
            {
                return "Tên Món ăn không được chứa các ký tự đặc biệt (như <, >, ;,...)!";
            }
            if (ma.TenMonAn.Length > 100 || ma.TenMonAn.Length < 3)
            {
                return "Tên món ăn không nhỏ hơn 3 hoặc vượt quá 100 ký tự!";
            }
            ma.TenMonAn = Regex.Replace(ma.TenMonAn, @"\s+", " ");
            // 2. Ràng buộc về Đơn giá
            if (ma.DonGia <= 0)
            {
                return "Đơn giá món ăn phải lớn hơn 0!";
            }
            // 3. Ràng buộc về Danh mục (Khóa ngoại)
            if (ma.MaDanhMuc.HasValue)
            {
                // Kiểm tra xem mã danh mục có tồn tại thật không
                if (!_monAnDAL.KiemTraTonTai(ma.MaDanhMuc.Value))
                {
                    return "Danh mục được chọn không hợp lệ hoặc đã bị xóa!";
                }
            }
            // Kiểm tra các điều kiện nghiệp vụ (Validation)
            if(_monAnDAL.KiemTraTrungTenMon(ma.TenMonAn))
                return "Tên món ăn đã tồn tại trong hệ thống!"; ;
            // 4. Ràng buộc về Công thức (Logic phối hợp từ thuộc tính CanNL của danh mục)
            if (canNguyenLieu)
            {
                if (dtCongThuc == null || dtCongThuc.Rows.Count == 0)
                {
                    return "Món ăn thuộc danh mục này bắt buộc phải cấu hình ít nhất 1 nguyên liệu!";
                }
            }

            bool ketQua = _monAnDAL.ThemMonAnKemCongThuc(ma, dtCongThuc);
            return ketQua ? "Thành công" : "Có lỗi xảy ra khi thêm món ăn vào hệ thống!";
        }

        public string CapNhatMonAn(MonAn ma, DataTable dtCongThuc, bool canNguyenLieu)
        {
            if (ma.MaMonAn <= 0)
                return "Mã món ăn không hợp lệ để cập nhật!";
            // 1. Ràng buộc về Tên món ăn
            if (string.IsNullOrWhiteSpace(ma.TenMonAn))
            {
                return "Tên món ăn không được để trống!";
            }
            string patternNguyenLieu = @"^[\p{L}\d\s_.,()\[\]-]+$";
            if (!Regex.IsMatch(ma.TenMonAn, patternNguyenLieu))
            {
                return "Tên Món ăn không được chứa các ký tự đặc biệt (như <, >, ;,...)!";
            }
            if (ma.TenMonAn.Length > 100 || ma.TenMonAn.Length < 3)
            {
                return "Tên món ăn không nhỏ hơn 3 hoặc vượt quá 100 ký tự!";
            }
            ma.TenMonAn = Regex.Replace(ma.TenMonAn, @"\s+", " ");
            // 2. Ràng buộc về Đơn giá
            if (ma.DonGia <= 0)
            {
                return "Đơn giá món ăn phải lớn hơn 0!";
            }
            // 3. Ràng buộc về Danh mục (Khóa ngoại)
            if (ma.MaDanhMuc.HasValue)
            {
                // Kiểm tra xem mã danh mục có tồn tại thật không
                if (!_monAnDAL.KiemTraTonTai(ma.MaDanhMuc.Value))
                {
                    return "Danh mục được chọn không hợp lệ hoặc đã bị xóa!";
                }
            }

            // Check trùng tên nhưng phải loại trừ chính món ăn đang sửa (dựa vào MaMonAn)
            if (_monAnDAL.KiemTraTrungTen(ma.TenMonAn, ma.MaMonAn))
            {
                return "Tên món ăn đã bị trùng với một món khác trong hệ thống!";
            }

            // Ràng buộc về Công thức (Logic phối hợp từ thuộc tính CanNL của danh mục)
            if (canNguyenLieu)
            {
                if (dtCongThuc == null || dtCongThuc.Rows.Count == 0)
                {
                    return "Món ăn thuộc danh mục này bắt buộc phải cấu hình ít nhất 1 nguyên liệu!";
                }
            }

            bool ketQua = _monAnDAL.CapNhatMonAnKemCongThuc(ma, dtCongThuc);
            return ketQua ? "Thành công" : "Có lỗi xảy ra khi cập nhật món ăn!";
        }

        public string XoaMonAn(int maMonAn)
        {
            try
            {
                bool ketQua = _monAnDAL.XoaMonAnVatLy(maMonAn);
                return ketQua ? "XOA_THANH_CONG" : "XOA_THAT_BAI";
            }
            catch (Exception ex)
            {
                // Bắt đúng chuỗi ký hiệu lỗi khóa ngoại từ DAL ném lên
                if (ex.Message == "FOREIGN_KEY_CONFLICT")
                {
                    return "YEU_CAU_CHUYEN_TRANG_THAI";
                }
                return "Lỗi hệ thống: " + ex.Message;
            }
        }
        public bool ThemDM(string tenDM, int? maDM = null)
        {
            if (!string.IsNullOrEmpty(tenDM))
            tenDM = tenDM.Trim();
            if (string.IsNullOrEmpty(tenDM))
                throw new ArgumentException("Tên danh muc không được trống");
            if (_monAnDAL.KiemTraTrungTenDM(tenDM, maDM))
                throw new ArgumentException("Tên danh mục đã tồn tại trong hệ thống");
            return _monAnDAL.ThemDM(tenDM);
        }

        public bool XoaDM(int maDM)
        {
            return _monAnDAL.XoaDM(maDM);
        }
        public bool ChuyenTrangThaiNgungKinhDoanh(int maMonAn, bool trangThai)
        {
            return _monAnDAL.CapNhatNgungKinhDoanh(maMonAn, trangThai);
        }
        public bool CapNhatDM(string tenDM, int maDM)
        {
            if (!string.IsNullOrEmpty(tenDM))
                tenDM = tenDM.Trim();
            if (string.IsNullOrEmpty(tenDM))
                throw new ArgumentException("Tên danh muc không được trống");
            if (_monAnDAL.KiemTraTrungTenDM(tenDM, maDM))
                throw new ArgumentException("Tên danh mục đã tồn tại trong hệ thống");
            return _monAnDAL.CapNhatDanhMuc(tenDM, maDM);
        }
        public DataTable LayDanhSachDanhMuc()
        {
            return _monAnDAL.LayDanhSachDanhMuc();
        }
    }
}
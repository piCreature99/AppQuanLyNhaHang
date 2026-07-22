using System;
using System.Data;
using App_QuanLy.DAL;
using App_QuanLy.Helpers;
using App_QuanLy.Models;

namespace App_QuanLy.BLL
{
    public class KhoHangBLL
    {
        private KhoHangDAL _khoHangDAL = new KhoHangDAL();
        private HelperFunctions _helperFuncs = new HelperFunctions();

        // Xử lý logic khi lấy danh sách (nếu từ khóa trống thì mặc định truyền chuỗi rỗng để lấy hết)
        public DataTable LayDanhSach(string tuKhoa)
        {
            return _khoHangDAL.LayDanhSachNguyenLieu(tuKhoa ?? "");
        }

        // Xử lý logic và Validate trước khi thêm mới
        public bool ThemNguyenLieu(NguyenLieu nl)
        {
            _helperFuncs.RangBuocNghiepVuThemNguyenLieu(nl);

            // Nếu mọi logic đã đúng, gọi xuống DAL để lưu
            bool kq = _khoHangDAL.ThemNguyenLieu(nl);
            return kq;
        }

        // Xử lý logic và Validate trước khi cập nhật
        public bool CapNhatNguyenLieu(NguyenLieu nl)
        {
            _helperFuncs.RangBuocNghiepVuSuaNguyenLieu(nl);

            bool kq = _khoHangDAL.CapNhatNguyenLieu(nl);
            return kq;
        }

        // Xử lý logic trước khi xóa
        public bool XoaNguyenLieuNoCatch(int ma)
        {
            if (ma <= 0) return false;
            return _khoHangDAL.XoaNguyenLieu(ma);
        }

        public string XoaNguyenLieu(int maNguyenLieu)
        {
            try
            {
                bool ketQua = _khoHangDAL.XoaNguyenLieuVatLy(maNguyenLieu);
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

        public bool ChuyenTrangThaiNgungKinhDoanh(int maNguyenLieu, bool trangThai)
        {
            return _khoHangDAL.CapNhatNgungKinhDoanh(maNguyenLieu, trangThai);
        }

        public DataTable LayDanhSachNguyenLieuDeGoiY()
        {
            return _khoHangDAL.LayDanhSachNguyenLieuDeGoiY();
        }


    }
}
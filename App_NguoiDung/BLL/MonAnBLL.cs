using System.Data;
using App_NguoiDung.DAL;
using App_NguoiDung.Data;
using App_NguoiDung.DTOS;
using App_NguoiDung.Models;

namespace App_NguoiDung.BLL
{
    public class MonAnBLL
    {
        private MonAnDAL _monAnDAL = new MonAnDAL();

        public DataTable LayDanhSachMonAn(string tuKhoa, int danhMuc)
        {
            // Nếu từ khóa null, chuyển thành chuỗi rỗng để tránh lỗi SQL LIKE
            return _monAnDAL.LayDanhSachMonAn(tuKhoa ?? "", danhMuc);
        }

        public DataTable LayDanhSachDanhMuc()
        {
            return _monAnDAL.LayDanhSachDanhMuc();
        }

        public List<DanhMucDTO> LayToanBoThucDon()
        {
            // Do logic lọc trạng thái kinh doanh và gộp nhóm tối ưu đã được xử lý ở DAL, 
            // BLL chỉ đóng vai trò trung chuyển dữ liệu và xử lý các ràng buộc nghiệp vụ nếu phát sinh sau này.
            return _monAnDAL.LayDanhSachThucDonGop();
        }
    }
}
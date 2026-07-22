using System.Data;
using App_QuanLy.DAL;
using App_QuanLy.Models;

namespace App_QuanLy.BLL
{
    public class CongThucBLL
    {
        private CongThucDAL _congThucDAL = new CongThucDAL();

        public DataTable LayCongThucMon(int maMonAn)
        {
            return _congThucDAL.LayCongThucTheoMon(maMonAn);
        }

        public string ThemThanhPhan(CongThuc ct)
        {
            // Nghiệp vụ định lượng định mức không cho phép giá trị âm hoặc bằng 0
            if (ct.SoLuongHaoHut <= 0)
                return "Định mức nguyên liệu tiêu hao phải lớn hơn 0!";

            return _congThucDAL.ThemCongThuc(ct) ? "SUCCESS" : "Lỗi không thể lưu công thức!";
        }

        public bool XoaThanhPhan(int maCongThuc)
        {
            return _congThucDAL.XoaThandPhanCongThuc(maCongThuc);
        }
    }
}
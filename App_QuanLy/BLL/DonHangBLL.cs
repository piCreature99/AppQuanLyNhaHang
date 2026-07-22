using System;
using System.Data;
using System.Text;
using App_QuanLy.DAL;
using App_QuanLy.DTOS;

namespace App_QuanLy.BLL
{
    public class DonHangBLL
    {
        private DonHangDAL _donHangDAL = new DonHangDAL();

        // PHỤC VỤ TIMER POLLING TRÊN UI (Mỗi 5 giây gọi hàm này một lần)
        public DataTable DocDonHangMoi()
        {
            // Trả về danh sách đơn 'ChoXacNhan' để Admin nổ thông báo lên màn hình WinForms
            return _donHangDAL.LayDonHangTheoTrangThai("ChoXacNhan");
        }

        // NGHIỆP VỤ DUYỆT ĐƠN HÀNG + TỰ ĐỘNG TRỪ KHO
        public string DuyetVaChuyenXuongBep(string maDonHang)
        {
            if (string.IsNullOrEmpty(maDonHang))
                return "Mã đơn hàng không hợp lệ!";

            // Bước 1: Lấy thông tin định lượng cần dùng cho đơn hàng này từ công thức
            DataTable dtDinhLuong = _donHangDAL.LayDinhLuongNguyenLieuCuaDonHang(maDonHang);

            // Bước 2: Thực hiện trừ kho tự động cho từng nguyên liệu cấu thành món ăn
            foreach (DataRow row in dtDinhLuong.Rows)
            {
                int maNL = Convert.ToInt32(row["MaNguyenLieu"]);
                decimal tongHaoHut = Convert.ToDecimal(row["TongHaoHut"]);

                // Ra lệnh cho DAL cập nhật lại bảng NGUYEN_LIEU
                _donHangDAL.TruKhoNguyenLieu(maNL, tongHaoHut);
            }

            // Bước 3: Đổi trạng thái đơn hàng sang 'DangChuanBi'
            bool capNhatDon = _donHangDAL.CapNhatTrangThaiDon(maDonHang, "DangChuanBi");

            if (capNhatDon)
                return "SUCCESS";
            else
                return "Lỗi! Không thể cập nhật trạng thái đơn hàng.";
        }

        // NGHIỆP VỤ ĐIỀU PHỐI SHIPPER (Khi chuyển sang trạng thái 'DangGiao')
        public bool GiaoDonChoShipper(string maDonHang, string maShipper)
        {
            if (string.IsNullOrEmpty(maDonHang) || string.IsNullOrEmpty(maShipper))
                return false;

            return _donHangDAL.CapNhatTrangThaiDon(maDonHang, "DangGiao", maShipper);
        }

        /// <summary>
        /// Lấy thông tin đơn hàng đã được phân cấp thành cấu trúc Map từ DAL
        /// </summary>
        public Dictionary<string, DonHangDTO> GetMapTongDonHang(string maDonHang, string trangThaiView)
        {
            string trangThaiDB = (trangThaiView == "Tất cả") ? "" : trangThaiView;

            return _donHangDAL.LayDanhSachDonHangGomMap(maDonHang, trangThaiDB);
        }

        public bool KiemTraDuNguyenLieu(DonHangDTO donHang, out string thongBaoThieu)
        {
            thongBaoThieu = "";

            // Dài hạn: Dùng Dictionary để gom nhóm nếu đơn hàng có nhiều món dùng chung nguyên liệu
            Dictionary<int, NguyenLieuTieuHao> listGomGiaTri = new Dictionary<int, NguyenLieuTieuHao>();

            foreach (var monAnPair in donHang.MapMonAn)
            {
                ChiTietMonAnDTO monAn = monAnPair.Value;

                foreach (var nguyenLieuPair in monAn.MapNguyenLieu)
                {
                    NguyenLieuTieuHao nl = nguyenLieuPair.Value;

                    if (listGomGiaTri.ContainsKey(nl.MaNguyenLieu))
                    {
                        // Nếu nguyên liệu đã tồn tại trong danh sách gom, cộng dồn lượng cần tiêu hao thêm
                        listGomGiaTri[nl.MaNguyenLieu].TongNguyenLieuCan += nl.TongNguyenLieuCan;
                    }
                    else
                    {
                        // Nếu chưa có, sao chép đối tượng nguyên liệu qua để theo dõi tổng
                        listGomGiaTri[nl.MaNguyenLieu] = new NguyenLieuTieuHao
                        {
                            MaNguyenLieu = nl.MaNguyenLieu,
                            TenNguyenLieu = nl.TenNguyenLieu,
                            TongNguyenLieuCan = nl.TongNguyenLieuCan,
                            TonKhoHienTai = nl.TonKhoHienTai,
                            DonViTinh = nl.DonViTinh
                        };
                    }
                }
            }

            // Duyệt danh sách đã gom để so sánh đối chiếu lượng Cần dùng với Tồn kho thực tế
            StringBuilder sbLoi = new StringBuilder();
            bool laDuHang = true;

            foreach (var itemPair in listGomGiaTri)
            {
                NguyenLieuTieuHao item = itemPair.Value;
                //MessageBox.Show(item.TongNguyenLieuCan.ToString());
                //MessageBox.Show(item.TonKhoHienTai.ToString());

                // Nếu lượng cần vượt quá lượng tồn kho hiện tại => Bị hụt kho!
                if (item.TongNguyenLieuCan > item.TonKhoHienTai)
                {
                    laDuHang = false;
                    decimal luongThieu = item.TongNguyenLieuCan - item.TonKhoHienTai;

                    sbLoi.AppendLine($"- {item.TenNguyenLieu}: Cần {item.TongNguyenLieuCan} {item.DonViTinh} | Kho có: {item.TonKhoHienTai} {item.DonViTinh} (Thiếu: {luongThieu} {item.DonViTinh})");
                }
            }

            // Nếu phát hiện thiếu hụt, soạn văn bản thông báo chi tiết gửi ngược lên GUI
            if (!laDuHang)
            {
                thongBaoThieu = $"Đơn hàng #{donHang.MaDonHang} KHÔNG ĐỦ NGUYÊN LIỆU!\n" +
                                 "Chi tiết lượng thiếu hụt trong kho:\n\n" + sbLoi.ToString();
            }

            return laDuHang;
        }

        public bool XulyHoanThanhMonAn(string maDonHang)
        {
            // Gọi thẳng xuống DAL thực thi chuỗi Transaction trừ kho và đổi trạng thái
            return _donHangDAL.TruNguyenLieuKhiHoanThanh(maDonHang);
        }

        public bool XulyHoanKhoKhiHuy(string maDonHang)
        {
            return _donHangDAL.HoanLaiNguyenLieuKhiHuyDon(maDonHang);
        }
        public bool UpdateTrangThaiDonHang(string maDonHang, string trangThaiMoi, string maShipper = "")
        {
            if (string.IsNullOrEmpty(maDonHang) || string.IsNullOrEmpty(trangThaiMoi))
                return false;

            return _donHangDAL.CapNhatTrangThaiDonHang(maDonHang, maShipper, trangThaiMoi);
        }

        public Dictionary<string, string> PollTrangThaiDonHangMoiNhat()
        {
            // 1. Gọi xuống DAL để bốc dữ liệu thô từ SQL Server lên
            DataTable dt = _donHangDAL.PollTrangThaiDonHangMoiNhat();

            // 2. Khởi tạo một Dictionary rỗng để hứng dữ liệu chuyển đổi
            Dictionary<string, string> dictTrangThai = new Dictionary<string, string>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string maDonHang = row["MaDonHang"].ToString();
                    string trangThai = row["TrangThaiDonHang"] != DBNull.Value ? row["TrangThaiDonHang"].ToString() : "";

                    // Đưa vào dictionary (Kiểm tra tránh trùng lặp Key an toàn)
                    if (!dictTrangThai.ContainsKey(maDonHang))
                    {
                        dictTrangThai.Add(maDonHang, trangThai);
                    }
                }
            }

          
            return dictTrangThai;
        }

        public bool KiemTraHoaDonMoi()
        {
            return _donHangDAL.KiemTraHoaDonMoiNhat();
        }
        public int LayTongSoHoaDon()
        {
            return _donHangDAL.LayTongSoHoaDon();
        }


    }
}
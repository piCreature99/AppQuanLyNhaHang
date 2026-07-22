using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using App_QuanLy.DAL;
using App_QuanLy.Models;

namespace App_QuanLy.Helpers
{
    public class HelperFunctions
    {
        private TaiKhoanDAL _taiKhoanDAL = new TaiKhoanDAL();
        private KhoHangDAL _khoHangDAL = new KhoHangDAL();

        public Image LoadImage(string path)
        {
            if (!File.Exists(path)) return null;

            try
            {
                byte[] imageBytes = File.ReadAllBytes(path);
                MemoryStream ms = new MemoryStream(imageBytes);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }
        public static void ApplyRoundedButtons(Control.ControlCollection controls, int radius)
        {
            //MessageBox.Show("Hello");
            foreach (Control c in controls)
            {
                // Nếu control là Button, áp dụng bo góc
                if (c is Button)
                {
                    //MessageBox.Show("Tìm thấy nút: " + c.Name);
                    Button btn = (Button)c;
                    btn.FlatStyle = FlatStyle.Flat; // Bắt buộc cho FlatStyle
                    btn.FlatAppearance.BorderSize = 0; // Xóa viền cũ

                    // Vẽ đường cong
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(0, 0, radius, radius, 180, 90);
                    path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
                    path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
                    path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
                    btn.Region = new Region(path);
                }

                // Nếu control chứa các control khác (như Panel, GroupBox), gọi đệ quy tiếp
                if (c.HasChildren)
                {
                    ApplyRoundedButtons(c.Controls, radius);
                }
            }
        }

        public void DefaultImgInitializer()
        {
            string thuMucImagesShipper = Path.Combine(Application.StartupPath, "Images\\shipper");
            if (!Directory.Exists(thuMucImagesShipper))
            {
                Directory.CreateDirectory(thuMucImagesShipper);
            }
            string duongDanAnhShipper = Path.Combine(thuMucImagesShipper, "default.png");
            if (!File.Exists(duongDanAnhShipper))
            {
                TaoAnhChuMacDinh(duongDanAnhShipper);
            }

            string thuMucImagesNguoiDung = Path.Combine(Application.StartupPath, "Images\\client");
            if (!Directory.Exists(thuMucImagesNguoiDung))
            {
                Directory.CreateDirectory(thuMucImagesNguoiDung);
            }
            string duongDanAnhNguoiDung = Path.Combine(thuMucImagesNguoiDung, "default.png");
            if (!File.Exists(duongDanAnhNguoiDung))
            {
                TaoAnhChuMacDinh(duongDanAnhNguoiDung);
            }

            string thuMucImagesAdmin = Path.Combine(Application.StartupPath, "Images\\admin");
            if (!Directory.Exists(thuMucImagesAdmin))
            {
                Directory.CreateDirectory(thuMucImagesAdmin);
            }
            string duongDanAnhAdmin = Path.Combine(thuMucImagesAdmin, "default.png");
            if (!File.Exists(duongDanAnhAdmin))
            {
                TaoAnhChuMacDinh(duongDanAnhAdmin);
            }

            string thuMucImagesMonAn = Path.Combine(Application.StartupPath, "Images\\FoodItems");
            if (!Directory.Exists(thuMucImagesMonAn))
            {
                Directory.CreateDirectory(thuMucImagesMonAn);
            }
            string duongDanAnhMonAn = Path.Combine(thuMucImagesMonAn, "default.png");
            if (!File.Exists(duongDanAnhMonAn))
            {
                TaoAnhChuMacDinh(duongDanAnhMonAn);
            }
        }

        public string KhoiTaoFolderDichTheoVaiTro(string vaiTro)
        {
            return Path.Combine(Application.StartupPath, $"Images\\{vaiTro}");
        }

        private void TaoAnhChuMacDinh(string duongDanLuuFile)
        {
            // 1. Tạo khung ảnh kích thước 300x300
            using (Bitmap bmp = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Màu nền xám đậm
                    g.Clear(Color.FromArgb(64, 0, 0));

                    // 2. Cấu hình chữ muốn viết lên ảnh
                    string text = "NO IMAGE";
                    Font font = new Font("Arial", 16, FontStyle.Bold);
                    Brush brush = Brushes.White;

                    // 3. Căn chữ nằm chính giữa bức ảnh
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    // 4. Vẽ chữ lên bề mặt bức ảnh
                    // RectangleF(0, 0, 300, 300) nghĩa là căn chữ theo toàn bộ khung ảnh
                    g.DrawString(text, font, brush, new RectangleF(0, 0, 300, 300), sf);
                }

                // 5. Lưu lại thành file vật lý
                bmp.Save(duongDanLuuFile, ImageFormat.Png);
            }
        }

        // Ràng buộc nghiệp vụ đăng ký tài khoản
        public void RangBuocNghiepVuThongTinDangKy(TaiKhoan taiKhoan)
        {

            // RÀNG BUỘC SỰ TỒN TẠI (Kiểm tra null/rỗng cơ bản)
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan), "Dữ liệu tài khoản không được để null!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.HoTen))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.DiaChi))
            {
                throw new ArgumentException("Địa chỉ giao hàng không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.SoDienThoai))
            {
                throw new ArgumentException("Số điện thoại không được để trống!");
            }
            // CHUẨN HÓA DỮ LIỆU
            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.ToLower();
            taiKhoan.HoTen = taiKhoan.HoTen.Trim();
            taiKhoan.SoDienThoai = taiKhoan.SoDienThoai?.Trim();
            taiKhoan.DiaChi = taiKhoan.DiaChi.Trim();
            // RÀNG BUỘC ĐỘ DÀI
            if (taiKhoan.TenDangNhap.Length < 4 || taiKhoan.TenDangNhap.Length > 50)
            {
                throw new ArgumentException("Tên đăng nhập phải chứa từ 4 đến 50 ký tự!");
            }
            if (taiKhoan.MatKhau.Length < 6 || taiKhoan.MatKhau.Length > 255)
            {
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            }
            if (taiKhoan.HoTen.Length < 2 || taiKhoan.HoTen.Length > 100)
            {
                throw new ArgumentException("Họ tên phải chứa từ 2 đến 100 ký tự!");
            }
            if (taiKhoan.DiaChi.Length < 10 || taiKhoan.DiaChi.Length > 255)
            {
                throw new ArgumentException("Địa chỉ phải có từ 10 đến 255 ký tự!");
            }
            // RÀNG BUỘC ĐỊNH DẠNG VÀ CHẶN KHOẢNG TRẮNG Ở GIỮA (Dùng Regex như hàm đăng ký)
            // Regex này tự động cấm luôn khoảng trắng ở giữa tên đăng nhập
            string pattern = @"^[\p{L}\d\s\-_.]+$";
            if (!Regex.IsMatch(taiKhoan.HoTen, pattern))
            {
                throw new ArgumentException("Họ tên không được chứa ký tự đặc biệt!");
            }
            // Chặn ký tự không hợp lệ
            if (!Regex.IsMatch(taiKhoan.TenDangNhap, "^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Tên đăng nhập chứa ký tự không hợp lệ (chỉ chấp nhận chữ cái không dấu, số và dấu '_')!");
            }
            // Chặn khoản trắng trong mật khẩu
            if (taiKhoan.MatKhau.Contains(" "))
            {
                throw new ArgumentException("Mật khẩu không được chứa khoảng trắng!");
            }
            // Định dạng số điện thoại chuẩn Việt Nam (nếu người dùng có điền)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai) && !Regex.IsMatch(taiKhoan.SoDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
            {
                throw new ArgumentException("Số điện thoại không đúng định dạng hợp lệ!");
            }
            if (Regex.IsMatch(taiKhoan.DiaChi, @"^(.)\1+$"))
            {
                throw new ArgumentException("Địa chỉ không hợp lệ, vui lòng không nhập ký tự lặp lại vô nghĩa!");
            }
            if (!Regex.IsMatch(taiKhoan.DiaChi, @"[\p{L}]"))
            {
                throw new ArgumentException("Địa chỉ giao hàng phải chứa các chữ cái tên đường, tên phường/xã!");
            }
            // Kiểm tra trùng Số điện thoại chủ động (tất cả số điện thoại)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai))
            {
                if (_taiKhoanDAL.KiemTraTrungSoDienThoai(taiKhoan.SoDienThoai))
                    throw new ArgumentException("Số điện thoại này đã tồn tại trong hệ thống!");
            }
            // Kiểm tra trùng tên đăng nhập (tất cả tên)
            if (_taiKhoanDAL.KiemTraTrungTenDangNhap(taiKhoan.TenDangNhap))
                throw new ArgumentException("Tên đăng nhập này đã được sử dụng!");

        }
        // Ràng buộc nghiệp vụ cập nhật tài khoản
        public void RangBuocNghiepVuThongTinCapNhat(TaiKhoan taiKhoan)
        {

            // RÀNG BUỘC SỰ TỒN TẠI (Kiểm tra null/rỗng cơ bản)
            if (taiKhoan == null)
            {
                throw new ArgumentNullException(nameof(taiKhoan), "Dữ liệu tài khoản không được để null!");
            }
            if (string.IsNullOrEmpty(taiKhoan.MaTaiKhoan))
            {
                throw new ArgumentException("Mã tài khoản không hợp lệ");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.TenDangNhap))
            {
                throw new ArgumentException("Tên đăng nhập không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.MatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.HoTen))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.DiaChi))
            {
                throw new ArgumentException("Địa chỉ giao hàng không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(taiKhoan.SoDienThoai))
            {
                throw new ArgumentException("Số điện thoại không được để trống!");
            }
            // CHUẨN HÓA DỮ LIỆU
            taiKhoan.TenDangNhap = taiKhoan.TenDangNhap.ToLower();
            taiKhoan.HoTen = taiKhoan.HoTen.Trim();
            taiKhoan.SoDienThoai = taiKhoan.SoDienThoai?.Trim() ?? "";
            taiKhoan.DiaChi = taiKhoan.DiaChi.Trim();
            // RÀNG BUỘC ĐỘ DÀI
            if (taiKhoan.TenDangNhap.Length < 4 || taiKhoan.TenDangNhap.Length > 50)
            {
                throw new ArgumentException("Tên đăng nhập phải chứa từ 4 đến 50 ký tự!");
            }
            if (taiKhoan.MatKhau.Length < 6 || taiKhoan.MatKhau.Length > 255)
            {
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            }
            if (taiKhoan.HoTen.Length < 2 || taiKhoan.HoTen.Length > 100)
            {
                throw new ArgumentException("Họ tên phải chứa từ 2 đến 100 ký tự!");
            }
            if (taiKhoan.DiaChi.Length < 10 || taiKhoan.DiaChi.Length > 255)
            {
                throw new ArgumentException("Địa chỉ phải có từ 10 đến 255 ký tự!");
            }
            // RÀNG BUỘC ĐỊNH DẠNG VÀ CHẶN KHOẢNG TRẮNG Ở GIỮA (Dùng Regex như hàm đăng ký)
            // Regex này tự động cấm luôn khoảng trắng ở giữa tên đăng nhập
            string pattern = @"^[\p{L}\d\s\-_.]+$";
            if (!Regex.IsMatch(taiKhoan.HoTen, pattern))
            {
                throw new ArgumentException("Họ tên không được chứa ký tự đặc biệt!");
            }
            // Chặn ký tự không hợp lệ
            if (!Regex.IsMatch(taiKhoan.TenDangNhap, "^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Tên đăng nhập chứa ký tự không hợp lệ (chỉ chấp nhận chữ cái không dấu, số và dấu '_')!");
            }
            // Chặn khoản trắng trong mật khẩu
            if (taiKhoan.MatKhau.Contains(" "))
            {
                throw new ArgumentException("Mật khẩu không được chứa khoảng trắng!");
            }
            if (Regex.IsMatch(taiKhoan.DiaChi, @"^(.)\1+$"))
            {
                throw new ArgumentException("Địa chỉ không hợp lệ, vui lòng không nhập ký tự lặp lại vô nghĩa!");
            }
            if (!Regex.IsMatch(taiKhoan.DiaChi, @"[\p{L}]"))
            {
                throw new ArgumentException("Địa chỉ giao hàng phải chứa các chữ cái tên đường, tên phường/xã!");
            }
            // Định dạng số điện thoại chuẩn Việt Nam (nếu người dùng có điền)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai) && !Regex.IsMatch(taiKhoan.SoDienThoai, @"^(0[3|5|7|8|9])([0-9]{8})$"))
            {
                throw new ArgumentException("Số điện thoại không đúng định dạng hợp lệ!");
            }
            // Kiểm tra trùng Số điện thoại chủ động (tất cả số điện thoại)
            if (!string.IsNullOrEmpty(taiKhoan.SoDienThoai))
            {
                if (_taiKhoanDAL.KiemTraTrungSoDienThoai(taiKhoan.SoDienThoai, taiKhoan.MaTaiKhoan))
                    throw new ArgumentException("Số điện thoại này đã tồn tại trong hệ thống!");
            }
            // Kiểm tra trùng tên đăng nhập (tất cả tên)
            if (_taiKhoanDAL.KiemTraTrungTenDangNhap(taiKhoan.TenDangNhap, taiKhoan.MaTaiKhoan))
                throw new ArgumentException("Tên đăng nhập này đã được sử dụng!");

        }
        // Ràng buộc nghiệp vụ lấy lại mật khẩu
        public void RangBuocNghiepVuLayLaiMatKhau(string soDienThoai, string matKhauMoi, string nhapLaiMatKhau)
        {
            // RÀNG BUỘC SỰ TỒN TẠI (Kiểm tra null/rỗng cơ bản)
            if (string.IsNullOrWhiteSpace(soDienThoai))
            {
                throw new ArgumentException("SĐT không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(matKhauMoi) || string.IsNullOrWhiteSpace(nhapLaiMatKhau))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }
            // CHUẨN HÓA DỮ LIỆU
            soDienThoai = soDienThoai.Trim();
            // RÀNG BUỘC ĐỘ DÀI
            if (matKhauMoi.Length < 6 || matKhauMoi.Length > 255)
            {
                throw new ArgumentException("Mật khẩu phải chứa từ 6 đến 255 ký tự!");
            }
            if (matKhauMoi != nhapLaiMatKhau)
            {
                throw new ArgumentException("Mật khẩu nhập lại không khớp");
            }
            // Chặn khoản trắng trong mật khẩu
            if (matKhauMoi.Contains(" "))
            {
                throw new ArgumentException("Mật khẩu không được chứa khoảng trắng!");
            }
        }

        // Ràng buộc nghiệp vụ thêm nguyên liệu
        public void RangBuocNghiepVuThemNguyenLieu(NguyenLieu nguyenLieu)
        {
            // 1. Kiểm tra tồn tại
            if (nguyenLieu == null)
            {
                throw new ArgumentNullException(nameof(nguyenLieu), "Dữ liệu nguyên liệu không được để null!");
            }
            if (string.IsNullOrWhiteSpace(nguyenLieu.TenNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(nguyenLieu.DonViTinh))
            {
                throw new ArgumentException("Đơn vị tính không được để trống!");
            }

            // 2. Chuẩn hóa dữ liệu (Trim và xử lý khoảng trắng thừa ở giữa)
            nguyenLieu.TenNguyenLieu = nguyenLieu.TenNguyenLieu.Trim();
            nguyenLieu.DonViTinh = nguyenLieu.DonViTinh.Trim();
            // Biến "Thịt   bò   Mỹ" thành "Thịt bò Mỹ"
            nguyenLieu.TenNguyenLieu = Regex.Replace(nguyenLieu.TenNguyenLieu, @"\s+", " ");
            nguyenLieu.DonViTinh = Regex.Replace(nguyenLieu.DonViTinh, @"\s+", " ");

            // 3. Ràng buộc độ dài
            if (nguyenLieu.TenNguyenLieu.Length < 3 || nguyenLieu.TenNguyenLieu.Length > 100)
            {
                throw new ArgumentException("Tên nguyên liệu phải chứa từ 3 đến 100 ký tự!");
            }
            if (nguyenLieu.DonViTinh.Length < 1 || nguyenLieu.TenNguyenLieu.Length > 20)
            {
                throw new ArgumentException("Đơn vị tính phải chứa từ 1 đến 20 ký tự!");
            }

            // 4. Kiểm tra ký tự hợp lệ (Cho phép dấu ngoặc, gạch ngang, dấu chấm, dấu phẩy)
            string patternNguyenLieu = @"^[\p{L}\d\s_.,()\[\]-]+$";
            string patternDVT = @"^[\p{L}\s]+$";
            if (!Regex.IsMatch(nguyenLieu.TenNguyenLieu, patternNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu không được chứa các ký tự đặc biệt (như <, >, ;,...)!");
            }
            if (!Regex.IsMatch(nguyenLieu.DonViTinh, patternDVT))
            {
                throw new ArgumentException("Đơn vị tính không được chứa các ký tự đặc biệt (như <, >, ;,...)!");
            }

            // 5. Kiểm tra trùng tên dưới Database (DAL)
            // Giả sử đây là hàm thêm mới (Nếu là hàm sửa thì cần truyền thêm Mã để né chính nó ra)
            if (_khoHangDAL.KiemTraTrungTenNguyenLieu(nguyenLieu.TenNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu này đã tồn tại trong kho!");
            }
        }
        // Ràng buộc nghiệp vụ sửa nguyên liệu
        public void RangBuocNghiepVuSuaNguyenLieu(NguyenLieu nguyenLieu)
        {
            // 1. Kiểm tra tồn tại
            if (nguyenLieu == null)
            {
                throw new ArgumentNullException(nameof(nguyenLieu), "Dữ liệu nguyên liệu không được để null!");
            }
            if (string.IsNullOrWhiteSpace(nguyenLieu.TenNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(nguyenLieu.DonViTinh))
            {
                throw new ArgumentException("Đơn vị tính không được để trống!");
            }

            // 2. Chuẩn hóa dữ liệu (Trim và xử lý khoảng trắng thừa ở giữa)
            nguyenLieu.TenNguyenLieu = nguyenLieu.TenNguyenLieu.Trim();
            nguyenLieu.DonViTinh = nguyenLieu.DonViTinh.Trim();
            // Biến "Thịt   bò   Mỹ" thành "Thịt bò Mỹ"
            nguyenLieu.TenNguyenLieu = Regex.Replace(nguyenLieu.TenNguyenLieu, @"\s+", " ");
            nguyenLieu.DonViTinh = Regex.Replace(nguyenLieu.DonViTinh, @"\s+", " ");

            // 3. Ràng buộc độ dài
            if (nguyenLieu.TenNguyenLieu.Length < 3 || nguyenLieu.TenNguyenLieu.Length > 100)
            {
                throw new ArgumentException("Tên nguyên liệu phải chứa từ 3 đến 100 ký tự!");
            }
            if (nguyenLieu.DonViTinh.Length < 1 || nguyenLieu.TenNguyenLieu.Length > 20)
            {
                throw new ArgumentException("Đơn vị tính phải chứa từ 1 đến 20 ký tự!");
            }

            // 4. Kiểm tra ký tự hợp lệ (Cho phép dấu ngoặc, gạch ngang, dấu chấm, dấu phẩy)
            string patternNguyenLieu = @"^[\p{L}\d\s_.,()\[\]-]+$";
            string patternDVT = @"^[\p{L}\s]+$";
            if (!Regex.IsMatch(nguyenLieu.TenNguyenLieu, patternNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu không được chứa các ký tự đặc biệt (như <, >, ;,...)!");
            }
            if (!Regex.IsMatch(nguyenLieu.DonViTinh, patternDVT))
            {
                throw new ArgumentException("Đơn vị tính không được chứa các ký tự đặc biệt (như <, >, ;,...)!");
            }

            // 5. Kiểm tra trùng tên dưới Database (DAL)
            // Giả sử đây là hàm thêm mới (Nếu là hàm sửa thì cần truyền thêm Mã để né chính nó ra)
            if (_khoHangDAL.KiemTraTrungTenNguyenLieu(nguyenLieu.TenNguyenLieu, nguyenLieu.MaNguyenLieu))
            {
                throw new ArgumentException("Tên nguyên liệu này đã tồn tại trong kho!");
            }
        }
    }
}

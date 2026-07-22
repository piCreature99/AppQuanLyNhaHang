using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using App_QuanLy.BLL;
using App_QuanLy.DTOS;
using App_QuanLy.Helpers;
using App_QuanLy.Services;

namespace App_QuanLy.Views
{
    public partial class FormQuanLyHoaDon : Form
    {
        // Bộ nhớ đệm lưu trữ danh sách hóa đơn phân cấp trên RAM
        private Dictionary<string, DonHangDTO> _cacheMapDonHang = new Dictionary<string, DonHangDTO>();
        private DonHangBLL _donHangBLL = new DonHangBLL();
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private DonHangDTO _donHangDangChon = null;
        private DonHangPollingService _pollingService;
        private string _trangThaiDonHienTai = "";
        private int _soLuongDonHienTai = 0;
        private string _vaiTroDangChon;
        private HelperFunctions _helperFuncs = new HelperFunctions();

        private string _tenAnhKhachHangDangChon;
        private string _duongDanAnhKhachHangDangChon;
        private string _tenAnhShipperDangChon;
        private string _duongDanAnhShipperDangChon;

        public FormQuanLyHoaDon()
        {
            InitializeComponent();
        }

        private void FormQuanLyHoaDon_Load(object sender, EventArgs e)
        {
            _helperFuncs.DefaultImgInitializer();
            NapAnhDaiDienKhachHang("default.png", "client");
            NapAnhDaiDienShipper("default.png", "shipper");
            LoadDuLieuLenForm(cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả", txtSearch.Text.Trim());
            ThietLapTrangThaiNut();
            LoadDanhSachShipperLenCombo();

            // Khởi động dịch vụ Polling
            _pollingService = new DonHangPollingService();
            CaiDatComboBoxTrangThai();

            // Đăng ký nhận sự kiện
            _pollingService.OnShipperDataReceived += PollingService_OnShipperDataReceived;
            _pollingService.OnOrderStatusChanged += PollingService_OnOrderStatusChanged;
            _pollingService.OnOrderCountChanged += PollingService_OnOrderCountChanged;

            _pollingService.Start(2000); // Chạy quét 2 giây/lần
        }

        // XỬ LÝ ĐỒNG BỘ SHIPPER COMBOBOX
        private void PollingService_OnShipperDataReceived(DataTable dtShipperMoi)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(new Action(() =>
            {
                DataTable dtHienTai = cboShipper.DataSource as DataTable;

                // 1. Trường hợp lần đầu nạp Form (chưa có DataSource)
                if (dtHienTai == null)
                {
                    if (dtShipperMoi == null) return;

                    // Tạo dòng mặc định
                    DataRow drMacDinh = dtShipperMoi.NewRow();
                    drMacDinh["MaTaiKhoan"] = "";
                    drMacDinh["HoTen"] = "-- Chọn shipper --";
                    dtShipperMoi.Rows.InsertAt(drMacDinh, 0); // Chèn vào vị trí đầu tiên (index 0)

                    cboShipper.DataSource = dtShipperMoi;
                    cboShipper.DisplayMember = "HoTen";
                    cboShipper.ValueMember = "MaTaiKhoan";
                    cboShipper.SelectedIndex = 0;
                    return;
                }

                // 2. Trường hợp Polling cập nhật ngầm (so sánh số lượng dòng)
                // Số dòng thực tế = Số shipper + 1 dòng "-- Chọn shipper --"
                int soLuongShipperCu = dtHienTai.Rows.Count - 1;
                int soLuongShipperMoi = dtShipperMoi != null ? dtShipperMoi.Rows.Count : 0;

                if (soLuongShipperMoi != soLuongShipperCu)
                {
                    dtHienTai.BeginLoadData();
                    dtHienTai.Clear(); // Xóa sạch dữ liệu cũ

                    // TẠO LẠI DÒNG MẶC ĐỊNH LÀM HÀNG ĐẦU TIÊN
                    DataRow drMacDinh = dtHienTai.NewRow();
                    drMacDinh["MaTaiKhoan"] = "";
                    drMacDinh["HoTen"] = "-- Chọn shipper --";
                    dtHienTai.Rows.Add(drMacDinh); // Thêm dòng mặc định vào vị trí 0

                    // IMPORT TIẾP CÁC SHIPPER MỚI VÀO PHÍA SAU
                    if (dtShipperMoi != null)
                    {
                        foreach (DataRow row in dtShipperMoi.Rows)
                        {
                            // Lưu ý: Nếu dtShipperMoi đã lỡ có dòng "-- Chọn shipper --" từ trước thì bỏ qua, 
                            // chỉ import những dòng shipper thật (có MaTaiKhoan khác rỗng)
                            if (row["MaTaiKhoan"] != null && !string.IsNullOrEmpty(row["MaTaiKhoan"].ToString()))
                            {
                                dtHienTai.ImportRow(row);
                            }
                        }
                    }

                    dtHienTai.EndLoadData();
                    // Việc chỉ Clear() và ImportRow() vào DataTable cũ giúp ComboBox giữ nguyên trạng thái DropDown!
                }
            }));
        }

        // XỬ LÝ CẬP NHẬT TRẠNG THÁI ĐƠN HÀNG TỨC THÌ (KHÔNG LOAD LẠI BẢNG)
        private void PollingService_OnOrderStatusChanged(Dictionary<string, string> dictTrangThaiMoi)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;


            this.Invoke(new Action(() =>
            {
                bool coThayDoi = false;

                foreach (var item in dictTrangThaiMoi)
                {
                    string maDon = item.Key;
                    string trangThaiMoiTuDB = item.Value;
                    //MessageBox.Show(maDon);

                    // Đối chiếu với bộ nhớ Cache RAM hiện tại toàn Form
                    if (_cacheMapDonHang.ContainsKey(maDon))
                    {
                        // Nếu trạng thái trong DB khác với trạng thái app đang giữ trên RAM -> Có sự thay đổi từ bên ngoài!
                        if (_cacheMapDonHang[maDon].TrangThaiDonHang != trangThaiMoiTuDB)
                        {
                            // Cập nhật ô trạng thái trực tiếp trên RAM ô nhớ
                            _cacheMapDonHang[maDon].TrangThaiDonHang = trangThaiMoiTuDB;
                            coThayDoi = true;

                            // Nếu đơn này xui rủi lại trúng ngay đơn người dùng đang click Xem chi tiết
                            if (_donHangDangChon != null && _donHangDangChon.MaDonHang == maDon)
                            {
                                // Cập nhật luôn trạng thái các nút bấm/ô chữ chi tiết ngay lập tức
                                CapNhatTrangThaiNutBam(trangThaiMoiTuDB);
                            }
                        }
                    }
                }

                // ĐÂY LÀ CHÌA KHÓA: Nếu phát hiện có đơn đổi trạng thái, chỉ ra lệnh vẽ lại chữ mới
                // Tuyệt đối không gán lại DataSource, không bị giật lag hay mất vị trí cuộn chuột của người dùng!
                if (coThayDoi)
                {
                    dgvDonHang.Refresh();
                }
            }));
        }

        private void PollingService_OnOrderCountChanged(int soLuongDonMoi)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(new Action(() =>
            {
                //MessageBox.Show(soLuongDonMoi.ToString() + _soLuongDonHienTai.ToString());
                if (soLuongDonMoi != _soLuongDonHienTai)
                {
                    _soLuongDonHienTai = soLuongDonMoi;
                    LoadDuLieuLenForm(cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả", txtSearch.Text.Trim());
                }

            }));
        }

        // Khi đóng Form nhớ tắt Polling để không bị chạy ngầm hao tổn tài nguyên máy
        private void FrmQuanLyDonHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pollingService != null)
            {
                _pollingService.Stop();
            }
        }

        private void LoadDuLieuLenForm(string trangThaiLoc, string tuKhoaTimKiem)
        {
            // 1. Tải toàn bộ cục Map phân cấp từ Database lên RAM (chỉ chạy 1 lần khi load/thay đổi bộ lọc)
            // Để trống ô tìm kiếm ở DAL để bốc toàn bộ danh sách, việc tìm kiếm từ khóa ta có thể lọc luôn trên RAM hoặc để DAL lo
            _cacheMapDonHang = _donHangBLL.GetMapTongDonHang(tuKhoaTimKiem, trangThaiLoc);

            dgvChiTietDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvChiTietDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgvChiTietDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTietDonHang.DefaultCellStyle.Font = new Font("Segoe UI", 7, FontStyle.Regular); ;
            dgvChiTietDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgvChiTietDonHang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChiTietDonHang.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgvChiTietDonHang.DefaultCellStyle.ForeColor = Color.White;
            dgvChiTietDonHang.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgvChiTietDonHang.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvChiTietDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgvChiTietDonHang.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvChiTietDonHang.EnableHeadersVisualStyles = false;
            dgvChiTietDonHang.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgvChiTietDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvChiTietDonHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChiTietDonHang.ColumnHeadersHeight = 50;
            dgvChiTietDonHang.AllowUserToResizeColumns = false;
            dgvChiTietDonHang.AllowUserToResizeRows = false;
            dgvChiTietDonHang.AllowUserToOrderColumns = false;
            dgvChiTietDonHang.ReadOnly = true;
            dgvChiTietDonHang.MultiSelect = false;
            dgvChiTietDonHang.RowTemplate.Height = 50;
            dgvChiTietDonHang.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvChiTietDonHang.ReadOnly = true;
            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            dgvChiTietDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chỉ cho phép chọn 1 hàng duy nhất tại một thời điểm (Tránh việc giữ Ctrl chọn nhiều hàng bừa bãi)
            dgvChiTietDonHang.MultiSelect = false;

            // 2. Đổ dữ liệu tổng quan lên dgvDonHang chính để hiển thị dòng
            // Chúng ta tạo ra một list phẳng rút gọn từ .Values của cục Map để gán vào DataSource
            dgvDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgvDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgvDonHang.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDonHang.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgvDonHang.DefaultCellStyle.ForeColor = Color.White;
            dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDonHang.EnableHeadersVisualStyles = false;
            dgvDonHang.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDonHang.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvDonHang.ColumnHeadersHeight = 50;
            dgvDonHang.AllowUserToResizeColumns = false;
            dgvDonHang.AllowUserToResizeRows = false;
            dgvDonHang.AllowUserToOrderColumns = false;
            dgvDonHang.ReadOnly = true;
            dgvDonHang.MultiSelect = false;
            dgvDonHang.RowTemplate.Height = 60;
            dgvDonHang.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvDonHang.ReadOnly = true;
            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chỉ cho phép chọn 1 hàng duy nhất tại một thời điểm (Tránh việc giữ Ctrl chọn nhiều hàng bừa bãi)
            dgvDonHang.MultiSelect = false;

            dgvDonHang.DataSource = new List<DonHangDTO>(_cacheMapDonHang.Values);

            // Ẩn bớt các cột chi tiết sâu trên dgvDonHang chính (Chỉ giữ lại thông tin hành chính đơn)
            AnCotKhongCanThietTrenGridChinh();

        }

        private void CaiDatComboBoxTrangThai()
        {
            cboFilterTrangThai.Items.Clear();
            cboFilterTrangThai.Items.Add("Tất cả");
            cboFilterTrangThai.Items.Add("Chờ duyệt");
            cboFilterTrangThai.Items.Add("Đang nấu");
            cboFilterTrangThai.Items.Add("Đã nấu xong");
            cboFilterTrangThai.Items.Add("Đang giao");
            cboFilterTrangThai.Items.Add("Đã giao");
            cboFilterTrangThai.Items.Add("Đã hủy");
            cboFilterTrangThai.SelectedIndex = 0;
        }

        //private void DefaultImgInitializer()
        //{
        //    string thuMucImages = Path.Combine(Application.StartupPath, "Images");
        //    if (!Directory.Exists(thuMucImages))
        //    {
        //        Directory.CreateDirectory(thuMucImages);
        //    }

        //    _selectedImagePath = Path.Combine(thuMucImages, "default.png");

        //    // Nếu chưa có file default.png, code sẽ tự vẽ ra file đó luôn!
        //    if (!File.Exists(_selectedImagePath))
        //    {
        //        TaoAnhChuMacDinh(_selectedImagePath);
        //    }
        //}

        private void TaoAnhChuMacDinh(string duongDanLuuFile)
        {
            // 1. Tạo khung ảnh kích thước 300x300
            using (Bitmap bmp = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Màu nền xám đậm
                    g.Clear(Color.FromArgb(230, 230, 230));

                    // 2. Cấu hình chữ muốn viết lên ảnh
                    string text = "NO IMAGE";
                    Font font = new Font("Arial", 16, FontStyle.Bold);
                    Brush brush = Brushes.Gray;

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

        private void XuLyAnhShipper(string selectedImage)
        {
            string duongDanDichFull = "";
            // Tạo thư mục "Images" nếu chưa có
            string folderDich = Path.Combine(Application.StartupPath, "Images");
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }

            duongDanDichFull = Path.Combine(Application.StartupPath, selectedImage);
            //MessageBox.Show(duongDanDichFull);
            if (!File.Exists(duongDanDichFull))
            {
                duongDanDichFull = Path.Combine(folderDich, "default.png");
            }
            if (!File.Exists(duongDanDichFull))
            {
                TaoAnhChuMacDinh(duongDanDichFull);
            }

            try
            {
                // Đọc toàn bộ file ảnh thành mảng byte
                byte[] imageBytes = File.ReadAllBytes(duongDanDichFull);

                // Chuyển mảng byte thành Stream nằm trên RAM
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // Tạo đối tượng ảnh từ RAM để gán vào DataTable
                    // File vật lý dưới ổ đĩa lúc này đã được giải phóng hoàn toàn!
                    shipperAvatar.Image = Image.FromStream(ms);
                }
            }
            catch
            {
                shipperAvatar.Image = null; // Ảnh lỗi hoặc không đọc được
            }


        }

        private void LoadDanhSachShipperLenCombo()
        {
            // Gọi BLL lấy DataTable danh sách shipper
            DataTable dtShipper = _taiKhoanBLL.PollDanhSachShipperSS();

            if (dtShipper != null)
            {
                DataRow dr = dtShipper.NewRow();
                dr["MaTaiKhoan"] = "";
                dr["HoTen"] = "-- Chọn shipper --";

                dtShipper.Rows.InsertAt(dr, 0);

                cboShipper.DataSource = dtShipper;
                cboShipper.DisplayMember = "HoTen";
                cboShipper.ValueMember = "MaTaiKhoan";

                cboShipper.SelectedIndex = 0;
            }
            // Đổ dữ liệu vào ComboBox
        }
        private void ThietLapTrangThaiNut()
        {
            btnDuyetDon.Enabled = false;
            btnXongMon.Enabled = false;
            btnGiaoViec.Enabled = false;
            btnInHoaDon.Enabled = false;
            btnHuyDon.Enabled = false;
        }
        private void AnCotKhongCanThietTrenGridChiTiet()
        {
            // Kiểm tra để tránh lỗi NullReference nếu lưới chưa kịp tạo cột
            if (dgvChiTietDonHang.Columns.Count == 0) return;

            // -----------------------------------------------------------------
            // 1. ẨN CÁC CỘT CẤU TRÚC PHỨC TẠP VÀ ID NỘI BỘ
            // -----------------------------------------------------------------
            if (dgvChiTietDonHang.Columns.Contains("MapNguyenLieu"))
                dgvChiTietDonHang.Columns["MapNguyenLieu"].Visible = false;

            if (dgvChiTietDonHang.Columns.Contains("MaMonAn"))
                dgvChiTietDonHang.Columns["MaMonAn"].Visible = false;


            // -----------------------------------------------------------------
            // 2. ĐỔI TIÊU ĐỀ TIẾNG VIỆT VÀ ĐỊNH DẠNG CỘT HIỂN THỊ
            // -----------------------------------------------------------------
            if (dgvChiTietDonHang.Columns.Contains("TenMonAn"))
            {
                dgvChiTietDonHang.Columns["TenMonAn"].HeaderText = "Tên Món Ăn";
                // Cho tên món ăn tự giãn rộng hết cỡ để dễ đọc
                //dgvChiTietDonHang.Columns["TenMonAn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvChiTietDonHang.Columns.Contains("SoLuongDat"))
            {
                dgvChiTietDonHang.Columns["SoLuongDat"].HeaderText = "Số Lượng";
                dgvChiTietDonHang.Columns["SoLuongDat"].Width = 50;
                dgvChiTietDonHang.Columns["SoLuongDat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvChiTietDonHang.Columns.Contains("GiaBan"))
            {
                dgvChiTietDonHang.Columns["GiaBan"].HeaderText = "Đơn Giá";
                dgvChiTietDonHang.Columns["GiaBan"].Width = 110;
                // Định dạng tiền tệ hiển thị phân tách hàng nghìn (Ví dụ: 45,000)
                dgvChiTietDonHang.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
                dgvChiTietDonHang.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvChiTietDonHang.Columns.Contains("ThanhTien"))
            {
                dgvChiTietDonHang.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvChiTietDonHang.Columns["ThanhTien"].Width = 50;
                dgvChiTietDonHang.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                dgvChiTietDonHang.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTietDonHang.Columns["ThanhTien"].DisplayIndex = 2;
            }
        }
        private void AnCotKhongCanThietTrenGridChinh()
        {
            // Kiểm tra xem DataGridView đã có dữ liệu chưa để tránh lỗi NullReference
            if (dgvDonHang.Columns.Count == 0) return;

            // -----------------------------------------------------------------
            // 1. ẨN CÁC CỘT DỮ LIỆU ĐỊNH LƯỢNG VÀ CẤU TRÚC PHỨC TẠP (Ẩn trên RAM)
            // -----------------------------------------------------------------
            if (dgvDonHang.Columns.Contains("MapMonAn"))
                dgvDonHang.Columns["MapMonAn"].Visible = false;

            // Ẩn bớt các ID nội bộ không cần cho khách hàng nhìn thấy
            if (dgvDonHang.Columns.Contains("MaDonHang")) dgvDonHang.Columns["MaDonHang"].Visible = false;
            if (dgvDonHang.Columns.Contains("MaKhachHang")) dgvDonHang.Columns["MaKhachHang"].Visible = false;
            if (dgvDonHang.Columns.Contains("MaShipper")) dgvDonHang.Columns["MaShipper"].Visible = false;
            if (dgvDonHang.Columns.Contains("HinhAnhKhach")) dgvDonHang.Columns["HinhAnhKhach"].Visible = false;
            if (dgvDonHang.Columns.Contains("SoDienThoaiShipper")) dgvDonHang.Columns["SoDienThoaiShipper"].Visible = false;
            if (dgvDonHang.Columns.Contains("HinhAnhSHipper")) dgvDonHang.Columns["HinhAnhShipper"].Visible = false;


            // -----------------------------------------------------------------
            // 2. ĐỔI TÊN TIÊU ĐỀ TIẾNG VIỆT CHO CÁC CỘT HIỂN THỊ CHÍNH
            // -----------------------------------------------------------------
            if (dgvDonHang.Columns.Contains("MaDonHienThi"))
            {
                dgvDonHang.Columns["MaDonHienThi"].HeaderText = "Mã Đơn Hàng";
                dgvDonHang.Columns["MaDonHienThi"].Width = 120;
            }

            if (dgvDonHang.Columns.Contains("TenKhachHang"))
            {
                dgvDonHang.Columns["TenKhachHang"].HeaderText = "Khách Hàng";
                dgvDonHang.Columns["TenKhachHang"].Width = 150;
            }

            if (dgvDonHang.Columns.Contains("SoDienThoaiKhach"))
            {
                dgvDonHang.Columns["SoDienThoaiKhach"].HeaderText = "Số Điện Thoại";
                dgvDonHang.Columns["SoDienThoaiKhach"].Width = 110;
            }

            if (dgvDonHang.Columns.Contains("NgayDat"))
            {
                dgvDonHang.Columns["NgayDat"].HeaderText = "Ngày Đặt";
                dgvDonHang.Columns["NgayDat"].Width = 140;
                // Định dạng lại ngày giờ hiển thị cho dễ nhìn (Ngày/Tháng/Năm Giờ:Phút)
                dgvDonHang.Columns["NgayDat"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            if (dgvDonHang.Columns.Contains("TongTien"))
            {
                dgvDonHang.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvDonHang.Columns["TongTien"].Width = 100;
                // Định dạng tiền tệ VND (Ví dụ: 110,000)
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvDonHang.Columns.Contains("TrangThaiDonHang"))
            {
                dgvDonHang.Columns["TrangThaiDonHang"].HeaderText = "Trạng Thái";
                dgvDonHang.Columns["TrangThaiDonHang"].Width = 110;
                dgvDonHang.Columns["TrangThaiDonHang"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvDonHang.Columns.Contains("TenShipper"))
            {
                dgvDonHang.Columns["TenShipper"].HeaderText = "Shipper Giao";
                dgvDonHang.Columns["TenShipper"].Width = 160;
            }

            if (dgvDonHang.Columns.Contains("DiaChiGiaoHang"))
            {
                dgvDonHang.Columns["DiaChiGiaoHang"].HeaderText = "Địa Chỉ Giao";
                // Cột địa chỉ thường dài, ta cho nó tự động co giãn chiếm hết không gian còn lại
                //dgvDonHang.Columns["DiaChiGiaoHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            dgvDonHang.Columns["TongTien"].DisplayIndex = 2;
            dgvDonHang.Columns["TenShipper"].DisplayIndex = 3;
        }

        private void NapAnhDaiDienKhachHang(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (khachAvatar.Image != null)
                {
                    khachAvatar.Image.Dispose();
                    khachAvatar.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    khachAvatar.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhKhachHangDangChon = tenAnh;
                    _duongDanAnhKhachHangDangChon = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    khachAvatar.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                khachAvatar.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }

        private void NapAnhDaiDienShipper(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (shipperAvatar.Image != null)
                {
                    shipperAvatar.Image.Dispose();
                    shipperAvatar.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    shipperAvatar.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhShipperDangChon = tenAnh;
                    _duongDanAnhShipperDangChon = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    shipperAvatar.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                shipperAvatar.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }
        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy mã đơn hàng từ dòng click
            string maDonHang = dgvDonHang.Rows[e.RowIndex].Cells["MaDonHang"].Value.ToString();

            // CHỈ ĐƠN GIẢN LÀ TRUY XUẤT TỪ MAP TRÊN RAM - KHÔNG GỌI DATABASE!
            if (_cacheMapDonHang.ContainsKey(maDonHang))
            {
                _donHangDangChon = _cacheMapDonHang[maDonHang];

                _trangThaiDonHienTai = _donHangDangChon.TrangThaiDonHang;
                // Đổ thông tin hành chính của Khách và Shipper lên TextBox
                txtTenKhachHang.Text = _donHangDangChon.TenKhachHang;
                txtSoDienThoai.Text = _donHangDangChon.SoDienThoaiKhach;
                txtDiaChiGiao.Text = _donHangDangChon.DiaChiGiaoHang;
                txtSoDienThoaiShipper.Text = _donHangDangChon.SoDienThoaiShipper;
                if (string.IsNullOrEmpty(_donHangDangChon.MaShipper))
                {
                    cboShipper.SelectedValue = "";
                }
                else
                {
                    // 1. Kiểm tra xem Mã Shipper của đơn này có tồn tại trong danh sách Cbo (đang rảnh) hay không
                    cboShipper.SelectedValue = _donHangDangChon.MaShipper;

                    // 2. Nếu SelectedValue bị null (Nghĩa là Shipper này ĐANG NGHỈ nên cbo không có data)
                    if (cboShipper.SelectedValue == "" || cboShipper.SelectedValue.ToString() != _donHangDangChon.MaShipper)
                    {
                        // Ép hiển thị tên Shipper đó lên giao diện để Quản lý vẫn đọc được
                        cboShipper.Text = _donHangDangChon.TenShipper + " (Đang nghỉ/Bận)";
                    }
                }
                NapAnhDaiDienKhachHang(_donHangDangChon.HinhAnhKhach, "client");
                //MessageBox.Show(_donHangDangChon.HinhAnhShipper);
                //MessageBox.Show(_donHangDangChon.TenShipper);
                NapAnhDaiDienShipper(_donHangDangChon.HinhAnhShipper, "shipper");
                // Đổ danh sách món ăn lên lưới chi tiết từ map con
                dgvChiTietDonHang.DataSource = new List<ChiTietMonAnDTO>(_donHangDangChon.MapMonAn.Values);

                AnCotKhongCanThietTrenGridChiTiet();
                // Chạy vòng lặp tính toán ngầm định lượng nguyên liệu (Đã có sẵn trên RAM)
                //KiemTraKhoNgam(donHangDuocChon);

                // Cập nhật trạng thái các nút chức năng dựa trên đơn hàng này
                CapNhatTrangThaiNutBam(_donHangDangChon.TrangThaiDonHang);
            }
        }

        /// <summary>
        /// Hàm hỗ trợ định dạng lại các cột hiển thị trên lưới chi tiết món ăn
        /// </summary>
        private void DinhDangLuoiChiTiet()
        {
            if (dgvChiTietDonHang.Columns["MaMonAn"] != null) dgvChiTietDonHang.Columns["MaMonAn"].Visible = false;
            if (dgvChiTietDonHang.Columns["MapNguyenLieu"] != null) dgvChiTietDonHang.Columns["MapNguyenLieu"].Visible = false;

            if (dgvChiTietDonHang.Columns["TenMonAn"] != null) dgvChiTietDonHang.Columns["TenMonAn"].HeaderText = "Tên món ăn";
            if (dgvChiTietDonHang.Columns["SoLuongDat"] != null) dgvChiTietDonHang.Columns["SoLuongDat"].HeaderText = "SL";
            if (dgvChiTietDonHang.Columns["GiaBan"] != null) dgvChiTietDonHang.Columns["GiaBan"].HeaderText = "Giá bán";
            if (dgvChiTietDonHang.Columns["ThanhTien"] != null) dgvChiTietDonHang.Columns["ThanhTien"].HeaderText = "Thành tiền";

            // Định dạng tiền tệ cho dễ nhìn
            if (dgvChiTietDonHang.Columns["GiaBan"] != null) dgvChiTietDonHang.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            if (dgvChiTietDonHang.Columns["ThanhTien"] != null) dgvChiTietDonHang.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
        }

        private void CapNhatTrangThaiNutBam(string trangThaiDonHang)
        {
            switch (trangThaiDonHang)
            {
                case "Chờ duyệt":
                    btnDuyetDon.Enabled = true;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = false;
                    btnInHoaDon.Enabled = false;
                    btnHuyDon.Enabled = true; // Luôn bật phòng sự cố
                    break;

                case "Đang nấu":
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = true;
                    btnGiaoViec.Enabled = false;
                    btnInHoaDon.Enabled = true; // Bấm nấu xong hoặc đang nấu được phép in hóa đơn
                    btnHuyDon.Enabled = true;
                    break;

                case "Đã nấu xong":
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = true; // Bật nút để Shipper hoàn thành đơn hoặc điều phối tiếp
                    btnInHoaDon.Enabled = true;
                    btnHuyDon.Enabled = true;
                    break;
                case "Gặp sự cố":
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = true; // Bật nút để Shipper hoàn thành đơn hoặc điều phối tiếp
                    btnInHoaDon.Enabled = true;
                    btnHuyDon.Enabled = true;
                    break;
                case "Đang giao":
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = false; // Bật nút để Shipper hoàn thành đơn hoặc điều phối tiếp
                    btnInHoaDon.Enabled = true;
                    btnHuyDon.Enabled = true;
                    break;

                case "Đã giao":
                case "Đã hủy":
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = false;
                    btnInHoaDon.Enabled = true; // Vẫn cho phép in lại hóa đơn cũ
                    btnHuyDon.Enabled = false; // Đã kết thúc quy trình không cho hủy nữa
                    break;

                default:
                    // Trường hợp không chọn đơn nào hoặc trạng thái lạ
                    btnDuyetDon.Enabled = false;
                    btnXongMon.Enabled = false;
                    btnGiaoViec.Enabled = false;
                    btnInHoaDon.Enabled = false;
                    btnHuyDon.Enabled = false;
                    break;
            }
        }
        // Lấy mã đơn hàng đang được chọn trên giao diện
        private string GetMaDonHangDangChon()
        {
            if (dgvDonHang.CurrentRow == null || dgvDonHang.CurrentRow.Index < 0) return null;
            return dgvDonHang.CurrentRow.Cells["MaDonHang"].Value.ToString();
        }

        // Hàm bổ trợ cập nhật nhanh trạng thái đơn hàng trên giao diện và Cache RAM
        private void ThucHienDoiTrangThai(string trangThaiMoi, string maShipper = "")
        {
            string maDonHang = GetMaDonHangDangChon();
            string tenShipper = "";
            if (string.IsNullOrEmpty(maDonHang))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //MessageBox.Show(maShipper);
            // Gọi qua BLL để cập nhật xuống CSDL
            bool bieuQuyet = _donHangBLL.UpdateTrangThaiDonHang(maDonHang, trangThaiMoi, maShipper);

            if (bieuQuyet)
            {
                if (cboShipper.SelectedIndex != null)
                {
                    // Ép kiểu dòng đang chọn về DataRowView để đọc các cột ẩn
                    DataRowView rowSelected = cboShipper.SelectedItem as DataRowView;

                    if (rowSelected != null)
                    {
                        // Lấy giá trị cột trạng thái (tên cột phải khớp chính xác với SQL
                        tenShipper = rowSelected["HoTen"] != DBNull.Value ? rowSelected["HoTen"].ToString().Trim() : "";

                    }
                }
                // Cập nhật trực tiếp trên Cache RAM
                if (_cacheMapDonHang.ContainsKey(maDonHang))
                {
                    _cacheMapDonHang[maDonHang].TrangThaiDonHang = trangThaiMoi;
                    if (cboShipper.SelectedValue != "")
                    _cacheMapDonHang[maDonHang].TenShipper = tenShipper;

                }

                // Làm mới lại hiển thị của DataGridView chính
                dgvDonHang.Refresh();

                // Cập nhật lại hiệu lực các nút bấm ngay lập tức
                CapNhatTrangThaiNutBam(trangThaiMoi);

                MessageBox.Show($"Đã chuyển đơn hàng sang trạng thái: {trangThaiMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật trạng thái thất bại. Vui lòng kiểm tra lại kết nối!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 1. NÚT DUYỆT ĐƠN
        private void btnDuyetDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;

            DonHangDTO donHangDangChon = dgvDonHang.CurrentRow.DataBoundItem as DonHangDTO;
            if (donHangDangChon == null) return;

            // 1. Kiểm tra kho trên RAM
            string chuoiLoiKho = "";
            bool duNguyenLieu = _donHangBLL.KiemTraDuNguyenLieu(donHangDangChon, out chuoiLoiKho);

            if (!duNguyenLieu)
            {
                MessageBox.Show(chuoiLoiKho, "Chặn duyệt đơn - Thiếu vật tư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Đủ đồ -> Trừ kho dưới DB
            try
            {
                bool thucHienTruKho = _donHangBLL.XulyHoanThanhMonAn(donHangDangChon.MaDonHang);

                if (thucHienTruKho)
                {
                    // 3. Đổi trạng thái đơn hàng sang Đang nấu
                    ThucHienDoiTrangThai("Đang nấu");

                    // Hàm này sẽ chạy lại GetMapTongDonHang() để DAL kéo toàn bộ TonKhoHienTai mới nhất từ DB lên RAM.
                    LoadDuLieuLenForm(cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả", txtSearch.Text.Trim());

                    MessageBox.Show("Duyệt đơn thành công! Nguyên liệu đã được cập nhật lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống khi trừ kho: {ex.Message}", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. NÚT NẤU XONG
        private void btnNauXong_Click(object sender, EventArgs e)
        {
            ThucHienDoiTrangThai("Đã nấu xong");
        }

        // 3. NÚT GIAO VIỆC (HOÀN THÀNH ĐƠN)
        private void btnGiaoViec_Click(object sender, EventArgs e)
        {
            // 1. Lấy giá trị an toàn, nếu chọn dòng trống ("--- Chọn Shipper ---") thì maShipper sẽ là chuỗi rỗng ""
            string maShipper = (cboShipper.SelectedValue == null || cboShipper.SelectedValue == DBNull.Value)
                ? ""
                : cboShipper.SelectedValue.ToString().Trim();

            // 2. Kiểm tra điều kiện chặn: Nếu trạng thái hiện tại là "Đang nấu" (hoặc "Đã nấu xong") 
            // và người dùng chưa chọn Shipper cụ thể thì dừng lại cảnh báo.
            if (string.IsNullOrEmpty(maShipper))
            {
                MessageBox.Show("Vui lòng chọn một Shipper cụ thể từ danh sách để giao việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboShipper.SelectedIndex != null)
            {
                // Ép kiểu dòng đang chọn về DataRowView để đọc các cột ẩn
                DataRowView rowSelected = cboShipper.SelectedItem as DataRowView;

                if (rowSelected != null)
                {
                    // Lấy giá trị cột trạng thái (tên cột phải khớp chính xác với SQL
                    string trangThai = rowSelected["TrangThai"] != DBNull.Value ? rowSelected["TrangThai"].ToString().Trim() : "";

                    // Thực hiện kiểm tra chặn
                    if (trangThai == "Đang nghỉ" || trangThai == "Nghỉ việc")
                    {
                        MessageBox.Show("Shipper này hiện tại đang nghỉ hoặc ngừng hoạt động, không thể giao đơn hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
            }

            // 4. Nếu vượt qua hết tất cả các tầng kiểm tra an toàn -> Tiến hành giao việc
            ThucHienDoiTrangThai("Đang giao", maShipper);

        }

        // 4. NÚT HỦY ĐƠN (Bật suốt quá trình)
        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow == null) return;

            DonHangDTO donHangDangChon = dgvDonHang.CurrentRow.DataBoundItem as DonHangDTO;
            if (donHangDangChon == null) return;

            // 1. Hỏi xác nhận hủy đơn tổng thể trước
            DialogResult resultHuy = MessageBox.Show(
                $"Bạn có chắc chắn muốn HỦY đơn hàng #{donHangDangChon.MaDonHang} không?",
                "Xác nhận hủy đơn",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultHuy != DialogResult.Yes) return; // Người dùng bấm No -> Dừng lại, không làm gì cả

            // Đặt biến kiểm tra xem có cần chạy lệnh hoàn kho dưới DB không
            bool canHoanKho = false;

            // 2. Kiểm tra điều kiện: Chỉ những đơn đã duyệt (Đang nấu, Đã nấu xong) mới cần hỏi hoàn kho
            // Nếu đơn mới ở trạng thái "Chờ duyệt" (chưa bị trừ kho bao giờ) thì bỏ qua bước này.
            if (donHangDangChon.TrangThaiDonHang == "Đang nấu" || donHangDangChon.TrangThaiDonHang == "Đã nấu xong" || donHangDangChon.TrangThaiDonHang == "Đang giao")
            {
                DialogResult resultHoanKho = MessageBox.Show(
                    "Nguyên liệu của đơn hàng này có thể tái sử dụng (hoàn trả lại vào kho) không?\n\n" +
                    "Chọn YES: Nếu bếp chưa làm hoặc đồ còn nguyên (Hệ thống sẽ CỘNG LẠI vào kho).\n" +
                    "Chọn NO: Nếu đồ ăn đã chế biến hỏng/vứt bỏ (Hệ thống giữ nguyên kho, tính vào hao hụt).",
                    "Xác lý lý thuyết kho khi hủy",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resultHoanKho == DialogResult.Yes)
                {
                    canHoanKho = true;
                }
            }

            // 3. Thực thi nghiệp vụ xuống dưới Database
            try
            {
                bool thucHienThanhCong = true;

                if (canHoanKho)
                {
                    // Gọi BLL xuống DAL thực hiện Transaction dấu (+) để hoàn nguyên liệu
                    thucHienThanhCong = _donHangBLL.XulyHoanKhoKhiHuy(donHangDangChon.MaDonHang);
                }

                if (thucHienThanhCong)
                {
                    // Thay đổi trạng thái đơn hàng thành "Đã hủy" theo hàm gốc của bạn
                    ThucHienDoiTrangThai("Đã hủy");

                    // Tải lại lưới để cập nhật số lượng tồn kho mới nhất lên RAM và giao diện
                    LoadDuLieuLenForm(cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả", txtSearch.Text.Trim());

                    MessageBox.Show("Đã hủy đơn hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi phát sinh khi hủy đơn: {ex.Message}", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 5. NÚT IN HÓA ĐƠN
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            string maDonHang = GetMaDonHangDangChon();
            if (string.IsNullOrEmpty(maDonHang) || !_cacheMapDonHang.ContainsKey(maDonHang)) return;

            DonHangDTO donHang = _cacheMapDonHang[maDonHang];

            // Test nhanh cấu trúc in hóa đơn ra màn hình console hoặc MessageBox trước khi xuất file
            string hoaDonText = $"--- HÓA ĐƠN BÁN HÀNG ---\nMã Đơn: {donHang.MaDonHang}\nKhách Hàng: {donHang.TenKhachHang}\nTổng tiền: {donHang.TongTien:#,##0} VND\n----------------------";
            MessageBox.Show(hoaDonText, "In Hóa Đơn Mẫu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSoDienThoaiShipper_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboShipper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboShipper.SelectedItem == null) return;

            DataRowView rowSelected = cboShipper.SelectedItem as DataRowView;

            if (rowSelected != null)
            {
                // KIỂM TRA AN TOÀN: Nếu người dùng chọn trúng dòng mặc định đầu tiên thì xóa thông tin cũ và dừng lại
                if (rowSelected["MaTaiKhoan"] == DBNull.Value)
                {
                    if (shipperAvatar.Image != null)
                    {
                        shipperAvatar.Image.Dispose(); shipperAvatar.Image = null;
                    }
                    NapAnhDaiDienShipper("default.png", "shipper");
                    return;
                }

                // Kiểm tra an toàn xem các cột này có tồn tại trong DataTable hiện tại không trước khi bốc dữ liệu
                if (rowSelected.Row.Table.Columns.Contains("HinhAnh") && rowSelected.Row.Table.Columns.Contains("SoDienThoai"))
                {
                    string tenFileAnh = rowSelected["HinhAnh"] != DBNull.Value ? rowSelected["HinhAnh"].ToString() : "";
                    string SDT = rowSelected["SoDienThoai"] != DBNull.Value ? rowSelected["SoDienThoai"].ToString() : "";

                    NapAnhDaiDienShipper(tenFileAnh, "shipper");
                    txtSoDienThoaiShipper.Text = SDT;
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            LoadDuLieuLenForm(cboFilterTrangThai.Text, txtSearch.Text);
        }

        private void cboFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieuLenForm(cboFilterTrangThai.Text, txtSearch.Text);
        }

        private void ClearForm()
        {
            txtSearch.Clear();
            txtTenKhachHang.Clear();
            txtSoDienThoai.Clear();
            txtDiaChiGiao.Clear();
            txtSoDienThoaiShipper.Clear();
            cboFilterTrangThai.SelectedIndex = 0;
            cboShipper.SelectedIndex = 0;
            if (khachAvatar.Image != null)
            {
                // 1. Giải phóng file ảnh đang chiếm dụng trong bộ nhớ RAM
                khachAvatar.Image.Dispose();

                // 2. Bỏ gán hoàn toàn để PictureBox quay về trạng thái trống rỗng
                khachAvatar.Image = null;
            }
            if (shipperAvatar.Image != null)
            {
                // 1. Giải phóng file ảnh đang chiếm dụng trong bộ nhớ RAM
                shipperAvatar.Image.Dispose();

                // 2. Bỏ gán hoàn toàn để PictureBox quay về trạng thái trống rỗng
                shipperAvatar.Image = null;
            }
            ThietLapTrangThaiNut();
            LoadDuLieuLenForm(cboFilterTrangThai.SelectedItem?.ToString() ?? "Tất cả", txtSearch.Text.Trim());
            dgvChiTietDonHang.DataSource = null;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}

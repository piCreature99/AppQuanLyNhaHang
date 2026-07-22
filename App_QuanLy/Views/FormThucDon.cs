using System;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using App_QuanLy.BLL;
using App_QuanLy.DAL;
using App_QuanLy.Helpers;
using App_QuanLy.Models;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.Views
{
    public partial class FormThucDon : Form
    {
        private KhoHangBLL _khoHangBLL = new KhoHangBLL();
        private MonAnBLL _monAnBLL = new MonAnBLL();
        private int _selectedDishId = 0;
        private int _selectedCatId = 0;
        private string _selectedImagePath = "template.jpg"; // Biến tạm lưu đường dẫn ảnh được chọn
        private string _selectedRelativeImagePath = ""; // Biến tạm lưu đường dẫn ảnh được chọn

        // DataTable trạng thái tạm thời giữ danh sách công thức đang hiển thị bên trái
        private DataTable _dtCongThucState;
        private DataTable _dtMonAnState;
        private HelperFunctions _helperFuncs = new HelperFunctions();

        private string _tenAnhDangChon;
        private string _duongDanAnhDangChonDeCopy;

        public FormThucDon()
        {
            InitializeComponent();
        }

        private void FrmThucDon_Load(object sender, EventArgs e)
        {
            _helperFuncs.DefaultImgInitializer();

            // Ngăn chặn sửa trực tiếp trên tất cả các ô của GridView
            dgvThucDon.ReadOnly = true;

            // (Tùy chọn thêm) Ngăn người dùng nhấn nút Delete trên bàn phím để xóa dòng bừa bãi
            dgvThucDon.AllowUserToDeleteRows = false;

            // (Tùy chọn thêm) Ngăn người dùng tự bấm chuột vào dòng trống cuối cùng để thêm dòng bừa bãi
            dgvThucDon.AllowUserToAddRows = false;

            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            dgvThucDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chỉ cho phép chọn 1 hàng duy nhất tại một thời điểm (Tránh việc giữ Ctrl chọn nhiều hàng bừa bãi)
            dgvThucDon.MultiSelect = false;

            NapDanhMucVaoStatusComboBox();
            HienThiDanhSachThucDon("");
            LoadDropdownNguyenLieu();
            KhoiTaoCauTrucBangCongThucTam();
            NapDanhMucVaoComboBox();
            NapAnhDaiDien();
        }

        // Hàm nạp danh mục vào ComboBox (Gọi hàm này tại sự kiện Form_Load)
        private void NapDanhMucVaoComboBox()
        {
            try
            {
                // Gọi xuống BLL hoặc DAL tùy cấu trúc ba lớp của bạn
                DataTable dtDanhMuc = _monAnBLL.LayDanhSachDanhMuc();

                cboCat.ValueMember = "MaDanhMuc";   // Giá trị ngầm bên dưới là Mã danh mục
                cboCat.DisplayMember = "TenDanhMuc"; // Hiển thị tên danh mục cho người dùng thấy
                cboCat.DataSource = dtDanhMuc;

                //Đặt SelectedIndex = -1 nếu muốn ban đầu cbo trống không tự chọn dòng đầu
                cboCat.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void NapDanhMucVaoStatusComboBox()
        {
            try
            {
                // Gọi xuống BLL hoặc DAL tùy cấu trúc ba lớp của bạn
                DataTable dtDanhMuc = _monAnBLL.LayDanhSachDanhMuc();

                DataRow rowAll = dtDanhMuc.NewRow();
                rowAll["MaDanhMuc"] = 0; // Gán giá trị 0 làm giá trị đặc biệt
                rowAll["TenDanhMuc"] = "Tất cả danh mục";
                rowAll["CanNL"] = false;

                dtDanhMuc.Rows.InsertAt(rowAll, 0);

                CboStatus.DataSource = dtDanhMuc;
                CboStatus.DisplayMember = "TenDanhMuc"; // Hiển thị tên danh mục cho người dùng thấy
                CboStatus.ValueMember = "MaDanhMuc";   // Giá trị ngầm bên dưới là Mã danh mục

                //Đặt SelectedIndex = -1 nếu muốn ban đầu cbo trống không tự chọn dòng đầu
                CboStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Image NapAnhDaiDienBang(string tenAnh, string vaiTro)
        {
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    return _helperFuncs.LoadImage(fullPath);
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    return _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                return _helperFuncs.LoadImage(defaultPath);
            }
        }
        private void NapAnhDaiDien(string tenAnh = "default.png")
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro("FoodItems");
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (dishAvatar.Image != null)
                {
                    dishAvatar.Image.Dispose();
                    dishAvatar.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    dishAvatar.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhDangChon = tenAnh;
                    _duongDanAnhDangChonDeCopy = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    dishAvatar.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                dishAvatar.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }

        private void btnAddDish_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra nhanh dữ liệu đầu vào ở giao diện trước khi đẩy xuống BLL
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                return;
            }

            string folderDich = Path.Combine(Application.StartupPath, "Images\\FoodItems");
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }
            string tenFileAnh = BoDauTiengViet(TxtName.Text.Trim()) + Path.GetExtension(_duongDanAnhDangChonDeCopy);
            string duongDanAnhMonAn = Path.Combine(folderDich, tenFileAnh);
            // 2. Tạo đối tượng món ăn mới (Không gán MaMonAn vì DB tự tăng)
            MonAn monAnMoi = new MonAn
            {
                TenMonAn = TxtName.Text.Trim(),
                DonGia = nudDonGia.Value,
                HinhAnh = tenFileAnh,
                MaDanhMuc = Convert.ToInt32(cboCat.SelectedValue) // Có thể thay bằng cboDanhMuc.SelectedValue nếu có
            };
            bool canNguyenLieu = true; // Mặc định là cần nguyên liệu cho an toàn

            if (cboCat.SelectedItem is DataRowView dongDuocChon)
            {
                // Ép kiểu cột CanNL từ DataRowView sang bool
                canNguyenLieu = Convert.ToBoolean(dongDuocChon["CanNL"]);
            }
            else
            {
                // Trường hợp món ăn không thuộc danh mục nào (MaDanhMuc = null)
                // Nghiệp vụ tùy bạn quyết định: Ở đây tôi mặc định không thuộc danh mục thì KHÔNG CẦN nguyên liệu
                canNguyenLieu = false;
            }
            // 3. Gọi BLL xử lý thêm mới kèm bảng công thức tạm thời
            string thongBao = _monAnBLL.ThemMonAn(monAnMoi, _dtCongThucState, canNguyenLieu);

            if (thongBao == "Thành công")
            {


                try
                {
                    // BẢO VỆ: Chỉ copy nếu file nguồn KHÁC file đích (Tránh lỗi tự ghi đè chính mình)
                    // Cần dùng Path.GetFullPath để chuẩn hóa chuỗi đường dẫn trước khi so sánh
                    if (_duongDanAnhDangChonDeCopy != duongDanAnhMonAn)
                    {
                        //MessageBox.Show(_duongDanAnhDangChonDeCopy + " " + duongDanAnhMonAn);
                        File.Copy(_duongDanAnhDangChonDeCopy, duongDanAnhMonAn, true);
                    }
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Lỗi ghi đè ảnh: " + ioEx.Message);
                } // true: cho phép ghi đè nếu trùng

                MessageBox.Show("Thêm món ăn mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HienThiDanhSachThucDon(""); // Tải lại lưới bên phải để thấy món mới
                ClearForm();        // Xóa sạch các ô nhập liệu để sẵn sàng cho tác vụ tiếp theo
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi xử lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuLyDuongDanAnh(out string duongDanDichFull, out string luuDuongDanAnhDB)
        {
            // Khởi tạo giá trị mặc định cho 2 tham số đầu ra
            duongDanDichFull = "";
            luuDuongDanAnhDB = "";

            // Truy cập trực tiếp biến toàn cục _selectedImagePath của Form
            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                // 1. Tạo thư mục "Images" nếu chưa có
                string folderDich = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderDich))
                {
                    Directory.CreateDirectory(folderDich);
                }

                // 2. Tạo tên file sạch dựa trực tiếp vào TxtName của Form
                string tenFileAnh = BoDauTiengViet(TxtName.Text.Trim()) + Path.GetExtension(_selectedImagePath);

                // 3. Gán giá trị thực tế cho 2 tham số out
                duongDanDichFull = Path.Combine(folderDich, tenFileAnh);
                luuDuongDanAnhDB = Path.Combine("Images", tenFileAnh);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn món ăn nào từ lưới chưa
            // Giả sử _selectedDishId của bạn kiểu int (mặc định = 0) hoặc int? (mặc định = null)
            if (_selectedDishId == 0)
            {
                MessageBox.Show("Vui lòng chọn một món ăn từ danh sách bên phải trước khi bấm cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Tên món ăn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtName.Focus();
                return;
            }

            string folderDich = Path.Combine(Application.StartupPath, "Images\\FoodItems");
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }
            string tenFileAnh = BoDauTiengViet(TxtName.Text.Trim()) + Path.GetExtension(_duongDanAnhDangChonDeCopy);
            string duongDanAnhNguoiDung = Path.Combine(folderDich, tenFileAnh);

            // 2. Tạo đối tượng món ăn với ID lấy từ biến State đã lưu trước đó
            MonAn monAnCapNhat = new MonAn
            {
                MaMonAn = Convert.ToInt32(_selectedDishId), // Sử dụng chính xác State ID của bạn
                TenMonAn = TxtName.Text,
                DonGia = nudDonGia.Value,
                HinhAnh = tenFileAnh,
                MaDanhMuc = Convert.ToInt32(cboCat.SelectedValue)
            };

            bool canNguyenLieu = true; // Mặc định là cần nguyên liệu cho an toàn

            if (cboCat.SelectedItem is DataRowView dongDuocChon)
            {
                // Ép kiểu cột CanNL từ DataRowView sang bool
                canNguyenLieu = Convert.ToBoolean(dongDuocChon["CanNL"]);
            }
            else
            {
                // Trường hợp món ăn không thuộc danh mục nào (MaDanhMuc = null)
                // Nghiệp vụ tùy bạn quyết định: Ở đây tôi mặc định không thuộc danh mục thì KHÔNG CẦN nguyên liệu
                canNguyenLieu = false;
            }

            // 3. Gọi BLL xử lý cập nhật
            string thongBao = _monAnBLL.CapNhatMonAn(monAnCapNhat, _dtCongThucState, canNguyenLieu);

            if (thongBao == "Thành công")
            {


                try
                {
                    // BẢO VỆ: Chỉ copy nếu file nguồn KHÁC file đích (Tránh lỗi tự ghi đè chính mình)
                    // Cần dùng Path.GetFullPath để chuẩn hóa chuỗi đường dẫn trước khi so sánh
                    if (_duongDanAnhDangChonDeCopy != duongDanAnhNguoiDung) File.Copy(_duongDanAnhDangChonDeCopy, duongDanAnhNguoiDung, true);
                }
                catch (IOException ioEx)
                {
                    MessageBox.Show("Lỗi ghi đè ảnh: " + ioEx.Message);
                } // true: cho phép ghi đè nếu trùng

                MessageBox.Show("Cập nhật thông tin món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HienThiDanhSachThucDon(""); // Tải lại lưới
                ClearForm();        // Xóa sạch các ô nhập liệu và reset luôn state ID
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi xử lý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HienThiDanhSachThucDon(string tuKhoa)
        {
            dgvThucDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvThucDon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgvThucDon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvThucDon.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgvThucDon.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvThucDon.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgvThucDon.DefaultCellStyle.ForeColor = Color.White;
            dgvThucDon.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgvThucDon.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvThucDon.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgvThucDon.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvThucDon.EnableHeadersVisualStyles = false;
            dgvThucDon.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgvThucDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvThucDon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvThucDon.ColumnHeadersHeight = 50;
            dgvThucDon.AllowUserToResizeColumns = false;
            dgvThucDon.AllowUserToResizeRows = false;
            dgvThucDon.AllowUserToOrderColumns = false;
            dgvThucDon.ReadOnly = true;
            dgvThucDon.MultiSelect = false;
            dgvThucDon.RowTemplate.Height = 60;
            dgvThucDon.DefaultCellStyle.WrapMode = DataGridViewTriState.True;



            dgvCongThuc.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvCongThuc.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgvCongThuc.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCongThuc.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgvCongThuc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCongThuc.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgvCongThuc.DefaultCellStyle.ForeColor = Color.White;
            dgvCongThuc.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgvCongThuc.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCongThuc.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgvCongThuc.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvCongThuc.EnableHeadersVisualStyles = false;
            dgvCongThuc.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgvCongThuc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvCongThuc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCongThuc.ColumnHeadersHeight = 50;
            dgvCongThuc.AllowUserToResizeColumns = false;
            dgvCongThuc.AllowUserToResizeRows = false;
            dgvCongThuc.AllowUserToOrderColumns = false;
            //dgvCongThuc.ReadOnly = true;
            dgvCongThuc.MultiSelect = false;
            dgvCongThuc.RowTemplate.Height = 40;
            dgvCongThuc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvCongThuc.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            try
            {
                int maDanhMuc = 0; // Mặc định là 0 (Tất cả) nếu chưa lấy được giá trị hợp lệ

                if (CboStatus.SelectedValue != null)
                {
                    // Nếu WinForms chưa xử lý xong ValueMember, SelectedValue sẽ là DataRowView
                    if (CboStatus.SelectedValue is DataRowView rowView)
                    {
                        // Bốc trực tiếp giá trị từ cột mã danh mục trong DataRowView ra
                        maDanhMuc = Convert.ToInt32(rowView["MaDanhMuc"]);
                    }
                    else if (CboStatus.SelectedValue is int)
                    {
                        // Nếu đã là kiểu int chuẩn chỉ
                        maDanhMuc = (int)CboStatus.SelectedValue;
                    }
                    else
                    {
                        // Trường hợp nó trả ra dạng chuỗi hoặc kiểu số khác (ví dụ: short, long)
                        int.TryParse(CboStatus.SelectedValue.ToString(), out maDanhMuc);
                    }
                }
                GiaiPhongAnhChoDataTable();
                _dtMonAnState = _monAnBLL.LayDanhSachMonAn(tuKhoa, maDanhMuc);
                DataTable dt = _dtMonAnState;

                dt.Columns.Add("AnhDaiDienThucTe", typeof(Image));

                foreach (DataRow row in dt.Rows)
                {
                    if (row["HinhAnh"] != DBNull.Value && row["HinhAnh"] != null)
                    {
                        string relativePath = row["HinhAnh"].ToString().Trim();
                        row["AnhDaiDienThucTe"] = NapAnhDaiDienBang(relativePath, "FoodItems");
                    }
                }
                //_dtShipperState = dt;
                dgvThucDon.DataSource = dt;

                if (dgvThucDon.Columns.Count > 0)
                {
                    dgvThucDon.Columns["MaMonAn"].Visible = false;
                    dgvThucDon.Columns["TenMonAn"].HeaderText = "Tên Món Ăn";
                    dgvThucDon.Columns["TenMonAn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvThucDon.Columns["DonGia"].HeaderText = "Đơn Giá";
                    dgvThucDon.Columns["HinhAnh"].HeaderText = "Hình Ảnh";
                    dgvThucDon.Columns["TenDanhMuc"].HeaderText = "Tên Danh Mục";
                    dgvThucDon.Columns["MaDanhMuc"].Visible = false;
                    dgvThucDon.Columns["CanNL"].HeaderText = "Cần NL";
                    dgvThucDon.Columns["TrangThaiKinhDoanh"].HeaderText = "Kinh Doanh";
                }

                if (dgvThucDon.Columns.Contains("HinhAnh"))
                    dgvThucDon.Columns["HinhAnh"].Visible = false; // Ẩn cột text đi

                if (dgvThucDon.Columns.Contains("AnhDaiDienThucTe"))
                {
                    // Ép cột ảnh tự co dãn vừa vặn
                    ((DataGridViewImageColumn)dgvThucDon.Columns["AnhDaiDienThucTe"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgvThucDon.Columns["AnhDaiDienThucTe"].HeaderText = "Ảnh Đại Diện";
                    dgvThucDon.Columns["AnhDaiDienThucTe"].DisplayIndex = 2; // Đẩy lên vị trí thứ 3 trong bảng
                }

                // Tăng chiều cao dòng cho đẹp
                dgvThucDon.RowTemplate.Height = 70;
                foreach (DataGridViewRow r in dgvThucDon.Rows) r.Height = 70;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách shipper: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GiaiPhongAnhChoDataTable()
        {
            if (_dtMonAnState != null)
            {
                // Gỡ DataSource của DataGridView ra trước để tránh xung đột giao diện khi đang hủy ảnh
                dgvThucDon.DataSource = null;

                if (_dtMonAnState.Columns.Contains("AnhDaiDienThucTe"))
                {
                    foreach (DataRow row in _dtMonAnState.Rows)
                    {
                        if (row["AnhDaiDienThucTe"] is Image imgCu)
                        {
                            imgCu.Dispose(); // Giải phóng tài nguyên và mở khóa file ảnh trên đĩa
                        }
                    }
                }

                _dtMonAnState.Dispose(); // Giải phóng bản thân DataTable cũ
                _dtMonAnState = null;
            }
        }
        private void LoadDropdownNguyenLieu()
        {
            // Đổ danh sách nguyên liệu vào ô Dropdown tìm kiếm nhanh
            DataTable dtNL = _khoHangBLL.LayDanhSachNguyenLieuDeGoiY();
            cboIngredients.DataSource = dtNL;
            cboIngredients.DisplayMember = "TenNguyenLieu";
            cboIngredients.ValueMember = "MaNguyenLieu";

            cboIngredients.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboIngredients.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIngredients.DropDownStyle = ComboBoxStyle.DropDown;

            cboIngredients.SelectedIndex = -1; // Để trống ban đầu
        }

        private void dgvCongThuc_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Ngăn không cho hộp thoại lỗi mặc định của hệ thống xuất hiện
            e.ThrowException = false;

            // Hiển thị thông báo lỗi tùy biến theo ý mình
            MessageBox.Show("Dữ liệu nhập vào không đúng định dạng của cột này!", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);

            // Đưa ô đó quay về trạng thái chỉnh sửa để người dùng nhập lại
            e.Cancel = true;
        }

        private void dgvCongThuc_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Lấy tên cột đang bị tác động
            string columnName = dgvCongThuc.Columns[e.ColumnIndex].Name;

            // 1. KIỂM TRA CHO CỘT SỐ LƯỢNG HAO HỤT (Hoặc các cột số nói chung)
            if (columnName == "SoLuongHaoHut")
            {
                string value = e.FormattedValue.ToString().Trim();

                // Kiểm tra trống
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Số lượng hao hụt không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Chặn không cho rời khỏi ô
                    return;
                }

                // Kiểm tra sai kiểu dữ liệu (Phải là số thực/số thập phân và không được âm)
                if (!decimal.TryParse(value, out decimal result) || result < 0)
                {
                    MessageBox.Show("Vui lòng nhập một số thập phân lớn hơn hoặc bằng 0!", "Lỗi kiểu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true; // Giữ chân người dùng lại ô này
                    return;
                }
            }

            // 2. KIỂM TRA CHO CÁC CỘT BẮT BUỘC NHẬP CHỮ (Nếu có, ví dụ như TenNguyenLieu)
            if (columnName == "TenNguyenLieu")
            {
                if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    MessageBox.Show("Tên nguyên liệu không được để trống!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void KhoiTaoCauTrucBangCongThucTam()
        {
            // Tạo một khung bảng trắng có các cột khớp với dgvCongThuc để hứng dữ liệu tạm
            _dtCongThucState = new DataTable();
            _dtCongThucState.Columns.Add("MaCongThuc", typeof(int));
            _dtCongThucState.Columns.Add("MaNguyenLieu", typeof(int));
            _dtCongThucState.Columns.Add("TenNguyenLieu", typeof(string));
            _dtCongThucState.Columns.Add("SoLuongHaoHut", typeof(decimal));
            _dtCongThucState.Columns.Add("DonViTinh", typeof(string));

            dgvCongThuc.DataSource = _dtCongThucState;

            // Định cấu hình ẩn cột ID không cần thiết đối với người dùng
            if (dgvCongThuc.Columns["MaCongThuc"] != null) dgvCongThuc.Columns["MaCongThuc"].Visible = false;
            if (dgvCongThuc.Columns["MaNguyenLieu"] != null) dgvCongThuc.Columns["MaNguyenLieu"].Visible = false;
            if (dgvCongThuc.Columns.Count > 0)
            {
                dgvCongThuc.Columns["TenNguyenLieu"].HeaderText = "Tên NL";
                dgvCongThuc.Columns["SoLuongHaoHut"].HeaderText = "Số Lượng";
                dgvCongThuc.Columns["SoLuongHaoHut"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvCongThuc.Columns["DonViTinh"].HeaderText = "ĐVT";

            }
            // 1. Cho phép DataGridView được quyền chỉnh sửa nói chung
            dgvCongThuc.ReadOnly = false;
            // (Tùy chọn thêm) Ngăn người dùng nhấn nút Delete trên bàn phím để xóa dòng bừa bãi
            dgvCongThuc.AllowUserToDeleteRows = false;
            // (Tùy chọn thêm) Ngăn người dùng tự bấm chuột vào dòng trống cuối cùng để thêm dòng bừa bãi
            dgvCongThuc.AllowUserToAddRows = false;
            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            dgvCongThuc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 2. Khóa tất cả các cột lại để an toàn
            foreach (DataGridViewColumn col in dgvCongThuc.Columns)
            {
                col.ReadOnly = true;
            }

            // 3. Chỉ mở khóa DUY NHẤT cột Số lượng hao hụt để người dùng gõ số
            if (dgvCongThuc.Columns["SoLuongHaoHut"] != null)
            {
                dgvCongThuc.Columns["SoLuongHaoHut"].ReadOnly = false;
            }
        }

        // Sự kiện nút "Chọn ảnh"
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _duongDanAnhDangChonDeCopy = ofd.FileName; // Lấy đường dẫn file ảnh
                    if (dishAvatar.Image != null)
                    {
                        dishAvatar.Image.Dispose();
                        dishAvatar.Image = null;
                    }
                    dishAvatar.Image = _helperFuncs.LoadImage(_duongDanAnhDangChonDeCopy);
                }
            }
        }

        public static string BoDauTiengViet(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            // 1. Xử lý khoảng trắng thừa ở hai đầu ngay từ đầu vào
            text = text.Trim();

            // 2. Chuẩn hóa chuỗi về FormD để tách chữ và dấu
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // 3. Đưa về FormC và sửa chính xác chữ Đ/đ thành D/d
            string result = sb.ToString().Normalize(NormalizationForm.FormC);
            result = result.Replace('đ', 'd').Replace('Đ', 'D');
            result = result.Replace(" ", "");
            return result;
        }

        private string luuDuongDanAnh()
        {
            string luuDuongDanAnhDB = "";

            if (!string.IsNullOrEmpty(_selectedImagePath))
            {
                // 1. Tạo thư mục tên là "Images" nằm ngay nơi chạy file .exe của App nếu chưa có
                string folderDich = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(folderDich))
                {
                    Directory.CreateDirectory(folderDich);
                }
                //MessageBox.Show(BoDauTiengViet(TxtName.Text));
                // 2. Tạo tên file mới để tránh trùng lặp (dùng Tên đăng nhập làm tên file ảnh)
                string tenFileAnh = BoDauTiengViet(TxtName.Text) + Path.GetExtension(_selectedImagePath); // Ví dụ: shipper01.jpg
                string duongDanDich = Path.Combine(folderDich, tenFileAnh);

                // 3. Thực hiện copy file ảnh từ ổ đĩa của Admin vào thư mục App
                if (File.Exists(duongDanDich))
                {
                    // 1. Nếu PictureBox đang hiển thị ảnh, hãy gỡ nó ra và giải phóng bộ nhớ
                    if (dishAvatar.Image != null)
                    {
                        dishAvatar.Image.Dispose(); // Giải phóng file ảnh đang bị khóa ngầm
                        dishAvatar.Image = null;    // Trả về trống để hủy liên kết hoàn toàn
                    }

                    // 2. Chạy dọn rác hệ thống (Garbage Collector) để ép Windows nhả file ra ngay lập tức
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                File.Copy(_selectedImagePath, duongDanDich, true); // true: cho phép ghi đè nếu trùng

                // 4. CHỈ LƯU ĐƯỜNG DẪN TƯƠNG ĐỐI VÀO DATABASE
                luuDuongDanAnhDB = Path.Combine("Images", tenFileAnh);
            }

            return luuDuongDanAnhDB;
        }

        // Nút bấm Thêm Nguyên Liệu từ Dropdown vào công thức tạm thời
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Chặn trường hợp user gõ chữ bậy bạ không có trong danh sách gợi ý
            if (cboIngredients.SelectedIndex == -1 || cboIngredients.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một nguyên liệu hợp lệ từ danh sách gợi ý!", "Thông báo");
                return;
            }

            if (cboIngredients.SelectedValue == null) return;

            int maNL = Convert.ToInt32(cboIngredients.SelectedValue);
            string tenNL = cboIngredients.Text;
            decimal soLuongNap = 1.000m; // Mặc định mỗi lần thêm mới là 1 đơn vị định lượng

            // Lấy Đơn vị tính từ thuộc tính dữ liệu đang chọn trong ComboBox
            DataRowView rowChon = (DataRowView)cboIngredients.SelectedItem;
            string dvt = rowChon["DonViTinh"].ToString();

            // Kiểm tra trùng nguyên liệu trong lưới tạm
            bool trungNL = false;
            foreach (DataRow r in _dtCongThucState.Rows)
            {
                if (Convert.ToInt32(r["MaNguyenLieu"]) == maNL)
                {
                    r["SoLuongHaoHut"] = Convert.ToDecimal(r["SoLuongHaoHut"]) + soLuongNap; // Cộng dồn số lượng
                    trungNL = true;
                    break;
                }
            }

            if (!trungNL)
            {
                DataRow newRow = _dtCongThucState.NewRow();
                newRow["MaNguyenLieu"] = maNL;
                newRow["TenNguyenLieu"] = tenNL;
                newRow["SoLuongHaoHut"] = soLuongNap;
                newRow["DonViTinh"] = dvt;
                _dtCongThucState.Rows.Add(newRow);
            }
        }

        // 1. Sự kiện khi click chọn 1 món ăn từ DataGridView bên phải
        private void dgvThucDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvThucDon.Rows[e.RowIndex];

            // Đổ thông tin cơ bản lên các ô nhập liệu bên trái
            //txtMaMon.Text = r.Cells["MaMonAn"].Value.ToString();
            TxtName.Text = r.Cells["TenMonAn"].Value.ToString();
            nudDonGia.Value = Convert.ToDecimal(r.Cells["DonGia"].Value);
            string relativePath = r.Cells["HinhAnh"].Value?.ToString();
            NapAnhDaiDien(relativePath);
            //string fullPath = Path.Combine(Application.StartupPath, relativePath);

            // txtHinhAnh.Text = r.Cells["HinhAnh"].Value?.ToString();
            // cboDanhMuc.SelectedValue = r.Cells["MaDanhMuc"].Value; // Nếu có dropdown danh mục

            // Tải danh sách công thức của món ăn này vào bảng tạm
            _selectedDishId = Convert.ToInt32(r.Cells["MaMonAn"].Value.ToString());
            DataTable dtGoc = _monAnBLL.LayCongThucTheoMon(_selectedDishId);

            bool dangHoatDong = Convert.ToBoolean(r.Cells["TrangThaiKinhDoanh"].Value);

            // THAY ĐỔI GIAO DIỆN NÚT BẤM LINH HOẠT
            if (dangHoatDong)
            {
                btnActivation.Text = "Ngưng Kinh doanh";
                btnActivation.BackColor = Color.Tomato; // Màu đỏ cảnh báo
            }
            else
            {
                btnActivation.Text = "Kích hoạt lại";
                btnActivation.BackColor = Color.MediumSeaGreen; // Màu xanh mở khóa
            }

            // Chỉ cần gán MaDanhMuc vào SelectedValue, ComboBox sẽ tự nhận diện tên hiển thị
            if (r.Cells["MaDanhMuc"].Value != DBNull.Value)
            {
                cboCat.SelectedValue = r.Cells["MaDanhMuc"].Value;
            }
            else
            {
                cboCat.SelectedIndex = -1; // Nếu món ăn chưa có danh mục
            }

            _dtCongThucState.Rows.Clear();
            foreach (DataRow rowGoc in dtGoc.Rows)
            {
                _dtCongThucState.ImportRow(rowGoc); // Copy nguyên trạng dữ liệu vào bộ nhớ tạm
            }
        }

        private void cboIngredients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ClearForm()
        {
            _helperFuncs.DefaultImgInitializer();
            TxtName.Clear();
            nudDonGia.Value = 0;
            _dtCongThucState.Rows.Clear();
            _selectedImagePath = "";
            TxtName.Focus();
            NapAnhDaiDien();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn món ăn nào chưa
            if (_selectedDishId == 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận trước khi thực hiện hành động xóa
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa món ăn đang chọn không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            int maMonAn = Convert.ToInt32(_selectedDishId);

            // 2. Gọi hàm xóa từ BLL
            string ketQua = _monAnBLL.XoaMonAn(maMonAn);

            if (ketQua == "XOA_THANH_CONG")
            {
                MessageBox.Show("Đã xóa hoàn toàn món ăn và công thức thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                HienThiDanhSachThucDon(""); // Tải lại danh sách bảng
                ClearForm();        // Xóa trắng form nhập liệu
            }
            else if (ketQua == "YEU_CAU_CHUYEN_TRANG_THAI")
            {
                // TRƯỜNG HỢP ĐẶC BIỆT: Phát hiện có giao dịch hóa đơn liên quan
                DialogResult luaChon = MessageBox.Show(
                    "Món ăn này đã có lịch sử giao dịch (hóa đơn) trong hệ thống nên không thể xóa bỏ hoàn toàn.\n\n" +
                    "Bạn có muốn chuyển trạng thái món ăn này sang [Ngừng Kinh Doanh] để ẩn khỏi thực đơn bán hàng không?",
                    "Món ăn đã có dữ liệu ràng buộc",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (luaChon == DialogResult.Yes)
                {
                    bool ok = _monAnBLL.ChuyenTrangThaiNgungKinhDoanh(maMonAn, false);
                    if (ok)
                    {
                        MessageBox.Show("Đã chuyển trạng thái món ăn sang Ngừng Kinh Doanh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        HienThiDanhSachThucDon("");
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Chuyển trạng thái thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng có thực sự đang chọn một danh mục hợp lệ không
            if (cboCat.SelectedIndex != -1 && cboCat.SelectedItem is DataRowView danhMucDangChon)
            {
                _selectedCatId = Convert.ToInt32(cboCat.SelectedValue);
                // Bốc giá trị bit CanNL từ danh mục được chọn ra
                bool canNguyenLieu = Convert.ToBoolean(danhMucDangChon["CanNL"]);

                // 2. Nếu không cần nguyên liệu (canNguyenLieu == false)
                if (!canNguyenLieu)
                {
                    // Tự động làm trống: Xóa sạch các dòng đang có trên DataTable trạng thái của bảng công thức
                    if (_dtCongThucState != null)
                    {
                        _dtCongThucState.Rows.Clear();
                    }

                    // Tắt nút thêm nl: Khóa nút thêm nguyên liệu lại
                    BtnAdd.Enabled = false;
                    btnDeleteIngredient.Enabled = false;
                    // Chặn tương tác bảng: Chuyển bảng sang chế độ chỉ đọc hoặc khóa hẳn tương tác
                    dgvCongThuc.Enabled = false;            // Đóng băng toàn bộ bảng (mờ đi để biểu thị không dùng)

                    cboIngredients.Enabled = false;
                }
                else
                {
                    // 3. Nếu CẦN nguyên liệu (canNguyenLieu == true) -> MỞ LẠI TẤT CẢ
                    BtnAdd.Enabled = true;
                    btnDeleteIngredient.Enabled = true;

                    dgvCongThuc.Enabled = true;
                    //dgvCongThuc.ReadOnly = false;
                    cboIngredients.Enabled = true;

                    //lblGợiÝCôngThức.Text = "* Vui lòng thiết lập công thức định lượng cho món ăn.";
                    //lblGợiÝCôngThức.ForeColor = Color.Red;
                }
            }
            else
            {
                // Trường hợp cboDanhMuc bị xóa trống (SelectedIndex = -1), bạn có thể chọn mở hoặc khóa tùy ý
                //btnThemNguyenLieu.Enabled = true;
                btnDeleteIngredient.Enabled = false;
                dgvCongThuc.Enabled = false;
                cboIngredients.Enabled = false;
            }
        }

        private void btnDeleteIngredient_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên DataGridView chưa
            if (dgvCongThuc.CurrentRow != null && dgvCongThuc.CurrentRow.Index >= 0)
            {
                // Hỏi xác nhận trước khi xóa (Tăng trải nghiệm UX)
                string tenNL = dgvCongThuc.CurrentRow.Cells["TenNguyenLieu"].Value?.ToString();
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nguyên liệu '{tenNL}' ra khỏi công thức món ăn này không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    // 2. Lấy vị trí index của dòng đang được chọn trên lưới
                    int rowIndex = dgvCongThuc.CurrentRow.Index;

                    // 3. Xóa dòng tương ứng trong DataTable trạng thái (_dtCongThucState)
                    if (_dtCongThucState != null && _dtCongThucState.Rows.Count > rowIndex)
                    {
                        _dtCongThucState.Rows[rowIndex].Delete();

                        // Chấp nhận thay đổi để DataTable cập nhật lại trạng thái
                        _dtCongThucState.AcceptChanges();

                        MessageBox.Show("Đã xóa nguyên liệu khỏi công thức tạm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng nguyên liệu trong bảng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            HienThiDanhSachThucDon(TxtSearch.Text.Trim());
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtSearch.Clear();
        }

        private void CboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            HienThiDanhSachThucDon(TxtSearch.Text.Trim());
        }

        private void btnActivation_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn món nào nào chưa
                if (_selectedDishId == -1)
                {
                    MessageBox.Show("Vui lòng chọn một Nguyên liệu từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRowView currentRowView = (DataRowView)dgvThucDon.CurrentRow.DataBoundItem;
                DataRow row = currentRowView.Row;

                // Lấy trạng thái hiện tại để đảo ngược nó (Toggle)
                bool trangThaiHienTai = Convert.ToBoolean(row["TrangThaiKinhDoanh"]);
                bool trangThaiMoi = !trangThaiHienTai;

                string action = trangThaiMoi ? "kích hoạt lại" : "ngưng kinh doanh";

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn {action} nguyên liệu [{_selectedDishId}]?",
                    "Xác nhận thay đổi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 3. Gọi BLL thực thi ngưng hoạt động (Xóa mềm / Update trạng thái)
                    if (_monAnBLL.ChuyenTrangThaiNgungKinhDoanh(_selectedDishId, trangThaiMoi))
                    {
                        HienThiDanhSachThucDon("");

                        MessageBox.Show($"Đã {action} nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật trạng thái! Nguyên liệu có thể không tồn tại.", "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            // --- KHỐI BẮT LỖI ĐẶC THÙ TỪ SQL SERVER ---
            catch (SqlException sqlEx)
            {
                // Các lỗi SQL khác (Mất kết nối, sai cú pháp...)
                MessageBox.Show($"Đã xảy ra lỗi từ Cơ sở dữ liệu: {sqlEx.Message} (Mã lỗi: {sqlEx.Number})", "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // --- KHỐI BẮT CÁC LỖI HỆ THỐNG KHÁC (Ứng dụng C#) ---
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống không mong muốn:\n{ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            if (dishAvatar.Image != null)
            {
                dishAvatar.Image.Dispose();
                dishAvatar.Image = null;
            }


            string folderPath = Path.Combine(Application.StartupPath, "Images", "FoodItems");
            string defaultPath = Path.Combine(folderPath, "default.png");
            _tenAnhDangChon = "default.png";
            _duongDanAnhDangChonDeCopy = defaultPath;

            dishAvatar.Image = _helperFuncs.LoadImage(defaultPath);
        }

        private void btnThemDanhMuc_Click(object sender, EventArgs e)
        {
            try
            {
                if (_monAnBLL.ThemDM(cboCat.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Thêm danh mục thành công!");
                }
                else
                {
                    MessageBox.Show("Không thể thêm danh mục");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void xoaDanhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn danh mục nào chưa
            if (cboCat.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một danh mục để xóa!");
                return;
            }

            string tenDanhMuc = cboCat.Text;
            int maDanhMuc = (int)cboCat.SelectedValue;

            // 2. Xác nhận xóa
            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa danh mục '{tenDanhMuc}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (dr == DialogResult.Yes)
            {
                // 3. Thực thi câu lệnh SQL xóa trong BLL
                try
                {


                    if (_monAnBLL.XoaDM(Convert.ToInt32(cboCat.SelectedValue)))
                    {
                        MessageBox.Show("Đã xóa danh mục thành công!");
                        NapDanhMucVaoComboBox();
                        // 4. Load lại ComboBox sau khi xóa
                        //LoadDanhSachDanhMuc();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa vì danh mục này đang chứa dữ liệu!");
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa vì danh mục này đã có món ăn!", "Lỗi Ràng Buộc Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmsDanhMuc_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void themDanhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDanhMucMoi = cboCat.Text.Trim();

                if (string.IsNullOrEmpty(tenDanhMucMoi))
                {
                    MessageBox.Show("Vui long nhập tên danh mục!");
                    return;
                }

                if (_monAnBLL.ThemDM(tenDanhMucMoi))
                {
                    MessageBox.Show("Thêm danh mục thành công!");
                    NapDanhMucVaoComboBox();
                }
                else
                {
                    MessageBox.Show("Không thể thêm danh mục");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void suaDanhMucToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDanhMucMoi = cboCat.Text.Trim();
                //MessageBox.Show(tenDanhMucMoi + _selectedCatId);

                if (string.IsNullOrEmpty(tenDanhMucMoi))
                {
                    MessageBox.Show("Vui lòng chọn danh mục!");
                    return;
                }

                if (_monAnBLL.CapNhatDM(tenDanhMucMoi, _selectedCatId))
                {
                    MessageBox.Show("Sửa danh mục thành công!");
                    NapDanhMucVaoComboBox();
                }
                else
                {
                    MessageBox.Show("Sửa danh mục không thành công");
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
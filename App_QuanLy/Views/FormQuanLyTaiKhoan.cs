using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using App_QuanLy.BLL;
using App_QuanLy.Helpers;
using App_QuanLy.Models;
using App_QuanLy.Services;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.Views
{
    public partial class FormQuanLyTaiKhoan : Form
    {
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private string _selectedImagePath = ""; // Biến tạm lưu đường dẫn ảnh được chọn
        private string _selectedRelativeImagePath = ""; // Biến tạm lưu đường dẫn ảnh được chọn
        private string _selectedShipperID = "";
        private string _selectedGender = "";
        private HelperFunctions _helperFuncs = new HelperFunctions();
        private string _selectedRole = "";
        private string _baseSelectedRole = "";

        private AccountPollingService _accountPollingService;
        private DataTable _dtShipperState; // Nơi lưu trữ "State" dữ liệu hiện tại của bảng

        private string _tenAnhDangChon;
        private string _duongDanAnhDangChonDeCopy;
        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
            FormQuanLyTaiKhoan_Load();
        }

        private void FormQuanLyTaiKhoan_Load()
        {

            _helperFuncs.DefaultImgInitializer();
            NapAnhDaiDien("default.png");
            // Ngăn chặn sửa trực tiếp trên tất cả các ô của GridView
            dgvTaiKhoan.ReadOnly = true;

            // (Tùy chọn thêm) Ngăn người dùng nhấn nút Delete trên bàn phím để xóa dòng bừa bãi
            dgvTaiKhoan.AllowUserToDeleteRows = false;

            // (Tùy chọn thêm) Ngăn người dùng tự bấm chuột vào dòng trống cuối cùng để thêm dòng bừa bãi
            dgvTaiKhoan.AllowUserToAddRows = false;

            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chỉ cho phép chọn 1 hàng duy nhất tại một thời điểm (Tránh việc giữ Ctrl chọn nhiều hàng bừa bãi)
            dgvTaiKhoan.MultiSelect = false;
            txtPassword.UseSystemPasswordChar = true;

            CaiDatComboBoxTrangThai();
            NapComboboxVaiTro();
            NapComboboxLocVaiTro();
            // 1. Tải dữ liệu lần đầu tiên (Initial Render)
            LoadDanhSachShipper();

            // 2. Khởi tạo Service Polling và đăng ký lắng nghe sự kiện
            _accountPollingService = new AccountPollingService();
            _accountPollingService.OnShipperDataReceived += Shippers_OnDataReceived;

            // 3. Bắt đầu kích hoạt Polling quét 2s/lần
            _accountPollingService.Start(2000);

        }



        // Hàm xử lý khi Service gửi dữ liệu mới về từ luồng ngầm
        private void Shippers_OnDataReceived(DataTable dtMoi)
        {
            // Vì dữ liệu được lấy từ luồng ngầm (Background Thread),
            // ta bắt buộc phải dùng Invoke để can thiệp an toàn vào giao diện (UI Thread)
            if (this.IsDisposed || this.Disposing) return;

            this.Invoke(new Action(() =>
            {
                CapNhatGiaoDienShipperNgam(dtMoi);
            }));
        }

        // THUẬT TOÁN ĐỒNG BỘ
        private void CapNhatGiaoDienShipperNgam(DataTable dtMoi)
        {
            if (_dtShipperState == null || dgvTaiKhoan.DataSource == null) return;

            // Quét qua từng dòng dữ liệu mới vừa lấy dưới DB lên
            foreach (DataRow rowMoi in dtMoi.Rows)
            {
                string maShipper = rowMoi["MaTaiKhoan"].ToString().Trim();

                // Tìm dòng tương ứng dựa vào MaTaiKhoan trong "State" hiện tại của bảng
                DataRow[] dongHienTai = _dtShipperState.Select($"MaTaiKhoan = '{maShipper}'");

                if (dongHienTai.Length > 0)
                {
                    DataRow currentStateRow = dongHienTai[0];

                    // 1. Kiểm tra biến đọng cột TrangThai (Đang bật app / Tắt app)
                    string statusMoi = Convert.ToString(rowMoi["TrangThai"]);
                    string statusCu = Convert.ToString(currentStateRow["TrangThai"]);

                    if (statusMoi != statusCu)
                    {
                        currentStateRow["TrangThai"] = statusMoi; // Cập nhật ô ngay tại chỗ

                    }

                    // 2. Kiểm tra biến động cột TrangThaiLamViec
                    bool tTLamViecMoi = Convert.ToBoolean(rowMoi["TrangThaiLamViec"]);
                    bool tTLamViecCu = Convert.ToBoolean(currentStateRow["TrangThaiLamViec"]);

                    if (tTLamViecMoi != tTLamViecCu)
                    {
                        currentStateRow["TrangThaiLamViec"] = tTLamViecMoi; // Cập nhật ô ngay tại chỗ
                    }
                }
            }
        }
        // QUAN TRỌNG: Phải tắt Polling khi tắt Form để giải phòng luồng
        private void FormQuanLyTaiKhoan_FormClosing(object sender, FormClosedEventArgs e)
        {
            if (_accountPollingService != null)
            {
                _accountPollingService.Stop();
            }
            GiaiPhongAnhChoDataTable();
        }

        private void dgvShipper_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem có đúng là cột mật khẩu hay không (thay "MatKhau" bằng tên cột thực tế của bạn)
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "MatKhau" && e.Value != null)
            {
                // Biến chuỗi hiển thị thành một chuỗi các ký tự mật khẩu (ví dụ: ●)
                e.Value = new string('●', e.Value.ToString().Length);

                // Báo cho WinForms biết đã định dạng xong, hãy vẽ chuỗi ký tự ẩn này lên màn hình
                e.FormattingApplied = true;
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
        private void NapAnhDaiDien(string tenAnh)
        {
            //MessageBox.Show(tenAnh);
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh

            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(_baseSelectedRole);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (picAvatar.Image != null)
                {
                    picAvatar.Image.Dispose();
                    picAvatar.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    picAvatar.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhDangChon = tenAnh;
                    _duongDanAnhDangChonDeCopy = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    picAvatar.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                picAvatar.Image = _helperFuncs.LoadImage(defaultPath);
            }


        }

        private void dgvShipper_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];


                _selectedShipperID = row.Cells["MaTaiKhoan"].Value?.ToString();
                //MessageBox.Show(row.Cells["MaTaiKhoan"].Value?.ToString());

                // Đọc trạng thái hiện tại từ dòng dữ liệu
                bool dangHoatDong = Convert.ToBoolean(row.Cells["TrangThaiLamViec"].Value);

                // THAY ĐỔI GIAO DIỆN NÚT BẤM LINH HOẠT
                if (dangHoatDong)
                {
                    btnDelete.Text = "Ngưng hoạt động";
                    btnDelete.BackColor = Color.Tomato; // Màu đỏ cảnh báo
                }
                else
                {
                    btnDelete.Text = "Kích hoạt lại";
                    btnDelete.BackColor = Color.MediumSeaGreen; // Màu xanh mở khóa
                }

                txtUsername.Text = row.Cells["TenDangNhap"].Value?.ToString();
                txtPassword.Text = row.Cells["MatKhau"].Value?.ToString();
                txtFullName.Text = row.Cells["HoTen"].Value?.ToString();
                txtPhone.Text = row.Cells["SoDienThoai"].Value?.ToString();
                CboGender.Text = row.Cells["GioiTinh"].Value?.ToString();
                txtAddress.Text = row.Cells["DiaChi"].Value?.ToString();
                txtStatus.Text = row.Cells["TrangThai"].Value?.ToString();
                cboVaiTro.SelectedValue = _selectedRole = _baseSelectedRole = row.Cells["VaiTro"].Value?.ToString();
                //MessageBox.Show(row.Cells["HinhAnh"].Value?.ToString());
                NapAnhDaiDien(row.Cells["HinhAnh"].Value?.ToString());
                //CboStatus.SelectedItem = row.Cells["TrangThai"].Value?.ToString() ?? "Còn hàng";
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
                    if (picAvatar.Image != null)
                    {
                        picAvatar.Image.Dispose();
                        picAvatar.Image = null;
                    }
                    picAvatar.Image = _helperFuncs.LoadImage(_duongDanAnhDangChonDeCopy);
                }
            }
        }

        // Hàm nạp dữ liệu vào DataGridView
        private void LoadDanhSachShipper(string tuKhoa = "", string vaiTroFilter = "")
        {
            //MessageBox.Show(tuKhoa + " " + vaiTroFilter);
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTaiKhoan.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgvTaiKhoan.DefaultCellStyle.ForeColor = Color.White;
            dgvTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgvTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgvTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTaiKhoan.EnableHeadersVisualStyles = false;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTaiKhoan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.ColumnHeadersHeight = 50;
            dgvTaiKhoan.AllowUserToResizeColumns = false;
            dgvTaiKhoan.AllowUserToResizeRows = false;
            dgvTaiKhoan.AllowUserToOrderColumns = false;
            dgvTaiKhoan.ReadOnly = true;
            dgvTaiKhoan.MultiSelect = false;
            dgvTaiKhoan.RowTemplate.Height = 60;
            dgvTaiKhoan.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            try
            {
                GiaiPhongAnhChoDataTable();

                DataTable dt = _taiKhoanBLL.LayDanhSachShipper(tuKhoa, vaiTroFilter);

                dt.Columns.Add("AnhDaiDienThucTe", typeof(Image));

                foreach (DataRow row in dt.Rows)
                {
                    string tenAnh = row["HinhAnh"].ToString().Trim();
                    string vaiTro = row["VaiTro"].ToString().Trim();
                    //MessageBox.Show(vaiTro);
                    if (!string.IsNullOrEmpty(vaiTro))
                    {
                        row["AnhDaiDienThucTe"] = NapAnhDaiDienBang(tenAnh, vaiTro);
                    }
                }
                _dtShipperState = dt;
                dgvTaiKhoan.DataSource = dt;

                if (dgvTaiKhoan.Columns.Contains("HinhAnh"))
                    dgvTaiKhoan.Columns["HinhAnh"].Visible = false; // Ẩn cột text đi
                if (dgvTaiKhoan.Columns.Contains("GioiTinh"))
                    dgvTaiKhoan.Columns["GioiTinh"].Visible = false;

                if (dgvTaiKhoan.Columns.Contains("AnhDaiDienThucTe"))
                {
                    // Ép cột ảnh tự co dãn vừa vặn
                    ((DataGridViewImageColumn)dgvTaiKhoan.Columns["AnhDaiDienThucTe"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dgvTaiKhoan.Columns["AnhDaiDienThucTe"].HeaderText = "Ảnh Đại Diện";
                    dgvTaiKhoan.Columns["AnhDaiDienThucTe"].DisplayIndex = 2; // Đẩy lên vị trí thứ 3 trong bảng
                    dgvTaiKhoan.Columns["AnhDaiDienThucTe"].Width = 90;
                }

                // Tăng chiều cao dòng cho đẹp
                dgvTaiKhoan.RowTemplate.Height = 70;
                foreach (DataGridViewRow r in dgvTaiKhoan.Rows) r.Height = 70;

                dgvTaiKhoan.Columns["MaTaiKhoan"].HeaderText = "Mã Shipper";
                dgvTaiKhoan.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                dgvTaiKhoan.Columns["MatKhau"].Visible = false;
                dgvTaiKhoan.Columns["Hoten"].HeaderText = "Họ và tên";
                dgvTaiKhoan.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvTaiKhoan.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dgvTaiKhoan.Columns["HinhAnh"].HeaderText = "Ảnh";
                dgvTaiKhoan.Columns["VaiTro"].HeaderText = "Vai Trò";
                dgvTaiKhoan.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgvTaiKhoan.Columns["TrangThaiLamViec"].HeaderText = "Đang Hoạt Động";
                dgvTaiKhoan.Columns["TrangThaiLamViec"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách shipper: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GiaiPhongAnhChoDataTable()
        {
            if (_dtShipperState != null)
            {
                // Gỡ DataSource của DataGridView ra trước để tránh xung đột giao diện khi đang hủy ảnh
                dgvTaiKhoan.DataSource = null;

                if (_dtShipperState.Columns.Contains("AnhDaiDienThucTe"))
                {
                    foreach (DataRow row in _dtShipperState.Rows)
                    {
                        if (row["AnhDaiDienThucTe"] is Image imgCu)
                        {
                            imgCu.Dispose(); // Giải phóng tài nguyên và mở khóa file ảnh trên đĩa
                        }
                    }
                }

                _dtShipperState.Dispose(); // Giải phóng bản thân DataTable cũ
                _dtShipperState = null;
            }
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

                // 2. Tạo tên file mới để tránh trùng lặp (dùng Tên đăng nhập làm tên file ảnh)
                string tenFileAnh = txtUsername.Text.Trim() + Path.GetExtension(_selectedImagePath); // Ví dụ: shipper01.jpg
                string duongDanDich = Path.Combine(folderDich, tenFileAnh);

                // 3. Thực hiện copy file ảnh từ ổ đĩa của Admin vào thư mục App
                File.Copy(_selectedImagePath, duongDanDich, true); // true: cho phép ghi đè nếu trùng

                // 4. CHỈ LƯU ĐƯỜNG DẪN TƯƠNG ĐỐI VÀO DATABASE
                luuDuongDanAnhDB = Path.Combine("Images", tenFileAnh);
            }

            return luuDuongDanAnhDB;
        }
        // Sự kiện nút "Thêm Shipper"
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                _helperFuncs.DefaultImgInitializer();

                string folderDich = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(_selectedRole);
                if (!Directory.Exists(folderDich))
                {
                    Directory.CreateDirectory(folderDich);
                }
                string tenFileAnh = txtUsername.Text.Trim() + Path.GetExtension(_duongDanAnhDangChonDeCopy);
                string duongDanAnhNguoiDung = Path.Combine(folderDich, tenFileAnh);
                // Đóng gói vào Model để gửi xuống BLL
                TaiKhoan shipperMoi = new TaiKhoan()
                {
                    TenDangNhap = txtUsername.Text,
                    MatKhau = txtPassword.Text,
                    HoTen = txtFullName.Text,
                    SoDienThoai = txtPhone.Text,
                    DiaChi = txtAddress.Text,
                    HinhAnh = tenFileAnh,
                    TrangThai = "Đang nghỉ",
                    GioiTinh = CboGender.SelectedItem?.ToString() ?? "",
                    VaiTro = _selectedRole
                };

                // Gọi BLL thực thi xử lý nghiệp vụ
                if (_taiKhoanBLL.ThemTaiKhoan(shipperMoi))
                {
                    try
                    {
                        // BẢO VỆ: Chỉ copy nếu file nguồn KHÁC file đích (Tránh lỗi tự ghi đè chính mình)
                        // Cần dùng Path.GetFullPath để chuẩn hóa chuỗi đường dẫn trước khi so sánh
                        if (_duongDanAnhDangChonDeCopy != duongDanAnhNguoiDung) File.Copy(_duongDanAnhDangChonDeCopy, duongDanAnhNguoiDung, true);
                        LoadDanhSachShipper();
                    }
                    catch (IOException ioEx)
                    {
                        MessageBox.Show("Lỗi ghi đè ảnh: " + ioEx.Message);
                    } // true: cho phép ghi đè nếu trùng
                    MessageBox.Show("Tạo tài khoản cho Shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
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

        private void NapComboboxVaiTro()
        {
            // 1. Tạo danh sách dữ liệu
            var danhSachVaiTro = new[]
            {
                new { TenHienThi = "Khách hàng", GiaTri = "client" },
                new { TenHienThi = "Nhân viên giao hàng", GiaTri = "shipper" },
                new { TenHienThi = "Quản lý", GiaTri = "admin" },
            };

            // 2. Gán vào DataSource của ComboBox
            cboVaiTro.DataSource = danhSachVaiTro;

            // 3. Cấu hình thuộc tính hiển thị và thuộc tính giá trị
            cboVaiTro.DisplayMember = "TenHienThi"; // Chữ hiển thị lên màn hình cho người dùng đọc
            cboVaiTro.ValueMember = "GiaTri";       // Giá trị thực tế chạy ngầm dưới code để lưu DB
        }

        private void NapComboboxLocVaiTro()
        {
            cboFilterVaiTro.SelectedIndexChanged -= cboFilterVaiTro_SelectedIndexChanged;
            // 1. Tạo danh sách dữ liệu
            var danhSachVaiTro = new[]
            {
                new { TenHienThi = "Tất cả", GiaTri = "" },
                new { TenHienThi = "Khách hàng", GiaTri = "client" },
                new { TenHienThi = "Nhân viên giao hàng", GiaTri = "shipper" },
                new { TenHienThi = "Quản lý", GiaTri = "admin" },
            };

            // 2. Gán vào DataSource của ComboBox
            cboFilterVaiTro.DataSource = danhSachVaiTro;

            // 3. Cấu hình thuộc tính hiển thị và thuộc tính giá trị
            cboFilterVaiTro.DisplayMember = "TenHienThi"; // Chữ hiển thị lên màn hình cho người dùng đọc
            cboFilterVaiTro.ValueMember = "GiaTri";       // Giá trị thực tế chạy ngầm dưới code để lưu DB
            cboFilterVaiTro.SelectedIndexChanged += cboFilterVaiTro_SelectedIndexChanged;
        }

        private void CaiDatComboBoxTrangThai()
        {
            CboGender.Items.Clear();
            CboGender.Items.Add("");
            CboGender.Items.Add("Nam");
            CboGender.Items.Add("Nữ");
            CboGender.SelectedIndex = 0;
        }

        private void CboGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedGender = CboGender.SelectedItem.ToString();
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            _selectedImagePath = "";
            txtStatus.Clear();
            CboGender.SelectedIndex = 0;
            txtUsername.Focus();

            if (picAvatar.Image != null)
            {
                picAvatar.Image.Dispose();
                picAvatar.Image = null;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn shipper nào trên bảng chưa
            if (string.IsNullOrEmpty(_selectedShipperID))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản từ bảng dữ liệu trước khi sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_selectedRole))
            {
                MessageBox.Show("Vài trò không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(_baseSelectedRole))
            {
                MessageBox.Show("Vài trò gốc không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _helperFuncs.DefaultImgInitializer();

            string folderDich = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(_selectedRole);
            if (!Directory.Exists(folderDich))
            {
                Directory.CreateDirectory(folderDich);
            }
            string tenFileAnh = txtUsername.Text.Trim() + Path.GetExtension(_duongDanAnhDangChonDeCopy);
            string duongDanAnhNguoiDung = Path.Combine(folderDich, tenFileAnh);

            try
            {
                DataRowView currentRowView = (DataRowView)dgvTaiKhoan.CurrentRow.DataBoundItem;
                DataRow row = currentRowView.Row;

                // Lấy trạng thái hiện tại để đảo ngược nó (Toggle)
                bool trangThaiHienTai = Convert.ToBoolean(row["TrangThaiLamViec"]);

                if (_baseSelectedRole == "admin" && _selectedRole != "admin" && trangThaiHienTai)
                {
                    int adminCount = _taiKhoanBLL.LaySoLuongTaiKhoanAdmin();
                    if (adminCount <= 1) throw new ArgumentException("Phải có ít nhất một tài khoản quản lý (admin)");
                }
                // Đóng gói vào Model để gửi xuống BLL
                TaiKhoan thongTin = new TaiKhoan()
                {
                    MaTaiKhoan = _selectedShipperID,
                    TenDangNhap = txtUsername.Text,
                    MatKhau = txtPassword.Text,
                    HoTen = txtFullName.Text,
                    SoDienThoai = txtPhone.Text,
                    DiaChi = txtAddress.Text,
                    HinhAnh = tenFileAnh, // Đường dẫn tương đối sạch sẽ, máy nào cũng chạy được
                    TrangThai = "Đang nghỉ",
                    GioiTinh = CboGender.SelectedItem?.ToString().Trim() ?? "",
                    VaiTro = _selectedRole
                };
                //MessageBox.Show(_selectedShipperID.Trim());
                //MessageBox.Show(txtPhone.Text.Trim());
                // Gọi BLL thực thi xử lý nghiệp vụ
                if (_taiKhoanBLL.SuaTaiKhoan(thongTin))
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
                    MessageBox.Show("Sửa thông tin tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachShipper();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Lỗi cập nhật");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn shipper nào chưa
                if (string.IsNullOrEmpty(_selectedShipperID))
                {
                    MessageBox.Show("Vui lòng chọn một tài khoản từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRowView currentRowView = (DataRowView)dgvTaiKhoan.CurrentRow.DataBoundItem;
                DataRow row = currentRowView.Row;

                // Lấy trạng thái hiện tại để đảo ngược nó (Toggle)
                bool trangThaiHienTai = Convert.ToBoolean(row["TrangThaiLamViec"]);
                bool trangThaiMoi = !trangThaiHienTai;

                string action = trangThaiMoi ? "kích hoạt lại" : "ngưng hoạt động";

                if (_baseSelectedRole == "admin" && trangThaiHienTai)
                {
                    int adminCount = _taiKhoanBLL.LaySoLuongTaiKhoanAdmin();
                    MessageBox.Show(adminCount.ToString());
                    if (adminCount <= 1) throw new ArgumentException("Phải có ít nhất một tài khoản quản lý (admin)");
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn {action} tài khoản [{_selectedShipperID}]?",
                    "Xác nhận thay đổi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 3. Gọi BLL thực thi ngưng hoạt động (Xóa mềm / Update trạng thái)
                    if (_taiKhoanBLL.CapNhatTrangThaiHoatDong(_selectedShipperID, trangThaiMoi))
                    {
                        LoadDanhSachShipper();

                        MessageBox.Show($"Đã {action} khoản Shipper thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        _selectedShipperID = "";
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật trạng thái! Tài khoản có thể không tồn tại.", "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachShipper(txtSearch.Text.ToString().Trim(), cboFilterVaiTro.SelectedValue.ToString().Trim());
        }

        private void chkPassShow_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkPassShow.Checked;
        }

        private void chkPassShow_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu check vào ô "Hiện mật khẩu" -> Tắt cơ chế ẩn đi và ngược lại
            txtPassword.UseSystemPasswordChar = !chkPassShow.Checked;
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            if (picAvatar.Image != null)
            {
                picAvatar.Image.Dispose();
                picAvatar.Image = null;
            }


            string folderPath = Path.Combine(Application.StartupPath, "Images", _baseSelectedRole);
            string defaultPath = Path.Combine(folderPath, "default.png");
            _tenAnhDangChon = "default.png";
            _duongDanAnhDangChonDeCopy = defaultPath;

            picAvatar.Image = _helperFuncs.LoadImage(defaultPath);
        }

        private void cboVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedRole = cboVaiTro.SelectedValue?.ToString();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void cboFilterVaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDanhSachShipper(txtSearch.Text.Trim(), cboFilterVaiTro.SelectedValue?.ToString().Trim());
        }

        private void btnXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn món nào nào chưa
                if (string.IsNullOrEmpty(_selectedShipperID)){
                    MessageBox.Show("Vui lòng chọn một Tài khoản từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vĩnh viễn tài khoản này?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 3. Gọi BLL thực thi ngưng hoạt động
                    if (_taiKhoanBLL.XoaTaiKhoan(_selectedShipperID, _baseSelectedRole))
                    {

                        MessageBox.Show($"Đã xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadDanhSachShipper();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản! Tài khoản có thể không tồn tại.", "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            // --- KHỐI BẮT LỖI ĐẶC THÙ TỪ SQL SERVER ---
            catch (SqlException sqlEx)
            {
                // Các lỗi SQL khác (Mất kết nối, sai cú pháp...)
                if (sqlEx.Number == 547)
                {
                    MessageBox.Show("Không thể xóa vì tài khoản này đã có hóa đơn liên quan!", "Lỗi Ràng Buộc Dữ Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Đã xảy ra lỗi từ Cơ sở dữ liệu: {sqlEx.Message} (Mã lỗi: {sqlEx.Number})", "Lỗi SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            // --- KHỐI BẮT CÁC LỖI HỆ THỐNG KHÁC (Ứng dụng C#) ---
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi xóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi hệ thống không mong muốn:\n{ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

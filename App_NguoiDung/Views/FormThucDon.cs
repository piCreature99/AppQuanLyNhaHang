using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_NguoiDung.BLL;
using App_NguoiDung.DTOS;
using App_NguoiDung.Helpers;
using App_NguoiDung.Models;

namespace App_NguoiDung.Views
{
    public partial class FormThucDon : Form
    {
        private readonly MonAnBLL _monAnBLL = new MonAnBLL();
        private readonly DonHangBLL _donHangBLL = new DonHangBLL();
        private static readonly Random _random = new Random();

        private List<DanhMucDTO> _dsDanhMuc = new List<DanhMucDTO>();
        private BindingList<ChiTietGioHangDTO> _gioHang = new BindingList<ChiTietGioHangDTO>();
        private HelperFunctions _helperFuncs = new HelperFunctions();
        private List<MonAnDTO> _dsTatCaMonAn;
        private FormMaster _master;
        private TaiKhoan _taiKhoanNguoiDung;

        private string _tenAnhDangChon;
        private string _duongDanAnhDangChonDeCopy;
        public FormThucDon(FormMaster master)
        {
            InitializeComponent();
            _master = master;
            _taiKhoanNguoiDung = master.TaiKhoanHienTai;
        }

        private void FormThucDon_Load(object sender, EventArgs e)
        {
            _helperFuncs.DefaultImgInitializer();
            NapAnhDaiDien("default.png", "FoodItems");
            FormatDgvCoBan(dgvThucDon);
            dgvThucDon.ColumnHeadersHeight = 60;
            dgvThucDon.RowTemplate.Height = 180;
            FormatDgvCoBan(dgvGioHang);
            dgvGioHang.ColumnHeadersHeight = 50;
            dgvGioHang.RowTemplate.Height = 50;

            dgvThucDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            dgvThucDon.DefaultCellStyle.Font = new Font("Segoe UI", 20, FontStyle.Regular);

            dgvGioHang.DataSource = _gioHang;

            _dsDanhMuc = _monAnBLL.LayToanBoThucDon();
            _dsTatCaMonAn = _dsDanhMuc
                        .SelectMany(dm => dm.DanhSachMonAn)
                        .ToList();

            CaiDatComboBoxDanhMuc();
            TaiDanhSachThucDon(_dsTatCaMonAn);
            DinhDangLuoiGioHang();
            CapNhatTongTien();
        }

        private void NapAnhDaiDien(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (pbAnhMonAn.Image != null)
                {
                    pbAnhMonAn.Image.Dispose();
                    pbAnhMonAn.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    pbAnhMonAn.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhDangChon = tenAnh;
                    _duongDanAnhDangChonDeCopy = fullPath;
                    //MessageBox.Show(tenAnh);
                    //MessageBox.Show(_duongDanAnhDangChonDeCopy);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    pbAnhMonAn.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                pbAnhMonAn.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }

        // Hàm sinh mã hóa đơn tự động đặt ở GUI
        private string SinhMaHoaDon()
        {
            // Chuỗi ngày giờ: DH20260714151020 (Độ dài 16 ký tự)
            string thoiGian = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Sinh thêm 4 số ngẫu nhiên từ 1000 đến 9999 (Độ dài 4 ký tự)
            string ngauNhien = _random.Next(1000, 10000).ToString();

            // Tổng chiều dài chuỗi khoảng 20 ký tự (Nằm gọn trong giới hạn VARCHAR(36) của bạn)
            return thoiGian + ngauNhien;
        }

        private void DinhDangLuoiGioHang()
        {
            // 1. Ẩn cột Mã món ăn để đảm bảo thẩm mỹ nhưng vẫn giữ lại chạy logic
            if (dgvGioHang.Columns.Contains("MaMonAn"))
            {
                dgvGioHang.Columns["MaMonAn"].Visible = false;
            }

            // 2. Định dạng tiêu đề hiển thị và định dạng hiển thị tiền tệ cho các cột số
            if (dgvGioHang.Columns.Contains("TenMon"))
            {
                dgvGioHang.Columns["TenMon"].HeaderText = "Tên Món Ăn";
                dgvGioHang.Columns["TenMon"].Width = 150;
            }

            if (dgvGioHang.Columns.Contains("SoLuong"))
            {
                dgvGioHang.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvGioHang.Columns["SoLuong"].Width = 80;
            }

            if (dgvGioHang.Columns.Contains("DonGia"))
            {
                dgvGioHang.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvGioHang.Columns["DonGia"].DefaultCellStyle.Format = "N0"; // Format giống dạng 50,000
                dgvGioHang.Columns["DonGia"].Width = 100;
            }

            if (dgvGioHang.Columns.Contains("ThanhTien"))
            {
                dgvGioHang.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvGioHang.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; // Format giống dạng 150,000
                dgvGioHang.Columns["ThanhTien"].Width = 120;
            }

            // Đặt chế độ tự co giãn cột cho vừa vặn khung lưới
            dgvGioHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CapNhatTongTien()
        {
            // Dùng LINQ tính tổng tất cả Thành tiền đang có trong BindingList giỏ hàng
            decimal tongTien = _gioHang.Sum(item => item.ThanhTien);

            // Gán vào TextBox chỉ hiển thị và format dạng N0 (ví dụ: 150,000)
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã thực sự chọn món nào trên thực đơn dgvThucDon chưa
            if (dgvThucDon.CurrentRow == null || dgvThucDon.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một món ăn từ thực đơn trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Lấy số lượng thực tế từ NumericUpDown nudSoLuong
            int soLuongThem = (int)nudSoLuong.Value;

            // Phòng hờ các lỗi ngoài ý muốn khiến số lượng bị <= 0
            if (soLuongThem <= 0)
            {
                MessageBox.Show("Số lượng thêm vào giỏ hàng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Lấy đối tượng MonAnDTO đang được chọn từ dòng hiện tại trên dgvThucDon
            if (dgvThucDon.CurrentRow.DataBoundItem is MonAnDTO monDuocChon)
            {
                // 4. Kiểm tra xem món ăn này đã có sẵn trong giỏ hàng offline chưa
                var monTrongGio = _gioHang.FirstOrDefault(item => item.MaMonAn == monDuocChon.MaMonAn);

                if (monTrongGio != null)
                {
                    // Trường hợp đã có: Cộng dồn số lượng vừa chọn vào số lượng hiện tại
                    monTrongGio.SoLuong += soLuongThem;

                    // Ép dgvGioHang vẽ lại dòng này để cập nhật giá trị hiển thị mới (Số lượng, Thành tiền)
                    _gioHang.ResetBindings();
                }
                else
                {
                    // Trường hợp chưa có: Tạo mới một dòng ChiTietGioHangDTO
                    ChiTietGioHangDTO dongMoi = new ChiTietGioHangDTO
                    {
                        MaMonAn = monDuocChon.MaMonAn,
                        TenMon = monDuocChon.TenMonAn,
                        DonGia = monDuocChon.DonGia,
                        SoLuong = soLuongThem // Thiết lập số lượng bằng giá trị lấy từ nudSoLuong
                    };

                    // Thêm trực tiếp vào BindingList (Grid sẽ tự vẽ thêm 1 dòng mới ngay lập tức)
                    _gioHang.Add(dongMoi);
                }

                // 5. Cập nhật lại tổng tiền trên txtTongTien
                CapNhatTongTien();

                // 6. Đưa nudSoLuong về lại giá trị 1 để sẵn sàng cho lần chọn tiếp theo
                nudSoLuong.Value = 1;
            }
        }

        private void btnXoaKhoiGio_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên dgvGioHang chưa
            if (dgvGioHang.CurrentRow == null || dgvGioHang.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một món ăn trong giỏ hàng để tiến hành xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Hỏi xác nhận lại trước khi xóa để tránh người dùng click nhầm (UX tốt hơn)
            var dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa món này khỏi giỏ hàng không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                // 3. Lấy đối tượng ChiTietGioHangDTO gắn liền với dòng được chọn
                if (dgvGioHang.CurrentRow.DataBoundItem is ChiTietGioHangDTO monCanXoa)
                {
                    // 4. Xóa khỏi BindingList (Lưới dgvGioHang sẽ tự xóa dòng hiển thị tương ứng)
                    _gioHang.Remove(monCanXoa);

                    // 5. Tính toán lại tổng tiền của giỏ sau khi đã xóa bớt
                    CapNhatTongTien();
                }
            }
        }

        private void CaiDatComboBoxDanhMuc()
        {
            // Tạo một danh sách mới để đổ lên Combo, tránh chỉnh sửa trực tiếp vào danh sách gốc _dsDanhMuc
            List<DanhMucDTO> dsCombo = new List<DanhMucDTO>(_dsDanhMuc);

            // Chèn danh mục "Tất cả" vào đầu danh sách với ID đặc biệt là -1
            dsCombo.Insert(0, new DanhMucDTO
            {
                MaDanhMuc = -1,
                TenDanhMuc = "Tất cả"
            });

            cboDanhMuc.DataSource = null;
            cboDanhMuc.DataSource = dsCombo;
            cboDanhMuc.DisplayMember = "TenDanhMuc"; // Hiển thị tên danh mục cho người dùng thấy
            cboDanhMuc.ValueMember = "MaDanhMuc";     // Giữ ngầm mã danh mục để xử lý code

            cboDanhMuc.SelectedIndex = 0; // Mặc định chọn "Tất cả" khi vừa mở Form
        }

        private void FormatDgvCoBan(DataGridView dgv)
        {
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ColumnHeadersHeight = 50;
            dgv.RowTemplate.Height = 50;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void GiaiPhongAnhTrenGrid()
        {
            // 1. Gỡ ảnh trên PictureBox ra trước (trả về null)
            NapAnhDaiDien("default.png", "FoodItems");

            // 2. Sau đó mới chạy logic giải phóng List dưới RAM như cũ
            var dsHienTai = dgvThucDon.DataSource as List<MonAnDTO>;
            dgvThucDon.DataSource = null;

            if (dsHienTai != null)
            {
                foreach (var monAn in dsHienTai)
                {
                    monAn.HinhAnhHienThi = null;
                }
            }
        }

        private void LamMoi()
        {
            txtTenMonAn.Clear();
            txtDonGia.Clear();
            txtSearch.Clear();
            cboDanhMuc.SelectedIndex = 0;
            TaiDanhSachThucDon(_dsTatCaMonAn);
            // 1. Gán ô hiện tại bằng null (Bắt buộc để CurrentRow trả về null)
            dgvThucDon.CurrentCell = null;
            // 2. Xóa vệt màu xanh đang highlight trên dòng (Giúp giao diện sạch sẽ)
            dgvThucDon.ClearSelection();
        }

        private void TaiDanhSachThucDon(List<MonAnDTO> danhSachMon)
        {
            // 1. Ngắt liên kết lưới và dọn dẹp RAM ảnh cũ (như logic chúng ta đã bàn)
            GiaiPhongAnhTrenGrid();

            dgvThucDon.DataSource = null; // Xóa sạch nguồn cũ để tránh lỗi cache cột

            if (danhSachMon != null && danhSachMon.Count > 0)
            {
                dgvThucDon.DataSource = danhSachMon;

                // Tự động kéo giãn cột Tên món ăn cho đẹp giao diện
                if (dgvThucDon.Columns.Contains("TenMonAn"))
                {
                    dgvThucDon.Columns["TenMonAn"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                // Định dạng tiền tệ cho cột Đơn giá (Ví dụ: 45000 -> 45,000)
                if (dgvThucDon.Columns.Contains("DonGia"))
                {
                    dgvThucDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                    dgvThucDon.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvThucDon.Columns["DonGia"].Width = 200;

                }

                // Ẩn cột MaDanhMuc không cần thiết hiển thị với người dùng
                if (dgvThucDon.Columns.Contains("MaDanhMuc"))
                {
                    dgvThucDon.Columns["MaDanhMuc"].Visible = false;
                }

                DinhDangCotAnhTrenLuoi();
            }
        }

        private void DinhDangCotAnhTrenLuoi()
        {
            // 1. Kiểm tra xem lưới đã tự động tạo ra cột "HinhAnhHienThi" chưa
            if (dgvThucDon.Columns.Contains("HinhAnhHienThi"))
            {
                var colAnh = (DataGridViewImageColumn)dgvThucDon.Columns["HinhAnhHienThi"];

                // CHÌA KHÓA Ở ĐÂY: Chuyển chế độ hiển thị thành Zoom để ảnh tự động co giãn 
                // vừa vặn với kích thước ô mà không bị méo, không bị vỡ hình
                colAnh.ImageLayout = DataGridViewImageCellLayout.Zoom;

                colAnh.Width = 180; // Bạn có thể đặt độ rộng của cột ảnh là 80px hoặc 100px tùy ý

                // Di chuyển cột hình ảnh lên đầu tiên (bên trái cùng)
                colAnh.DisplayIndex = 0;
            }
        }

        private void ThucHienLocThucDon()
        {

            // Nếu danh sách gốc trên RAM trống thì không làm gì cả
            if (_dsTatCaMonAn == null) return;

            // 1. Lấy dữ liệu từ bộ lọc txtSearch
            string tuKhoa = txtSearch.Text.Trim().ToLower();

            // 2. Lấy dữ liệu từ bộ lọc cboDanhMuc
            // Giả sử mã danh mục mặc định "Tất cả" là 0 hoặc -1, hoặc cbo chưa chọn gì
            int maDanhMucDuocChon = 0;
            if (cboDanhMuc.SelectedValue != null)
            {
                int.TryParse(cboDanhMuc.SelectedValue.ToString(), out maDanhMucDuocChon);
            }

            // 3. Thực hiện lọc đồng thời bằng LINQ
            List<MonAnDTO> danhSachDaLoc = _dsTatCaMonAn
                .Where(mon =>
                    // Điều kiện 1: Lọc theo Danh mục (Nếu chọn "Tất cả" hoặc mã khớp thì lấy)
                    (maDanhMucDuocChon <= 0 || mon.MaDanhMuc == maDanhMucDuocChon)
                    &&
                    // Điều kiện 2: Lọc theo Từ khóa tìm kiếm
                    (string.IsNullOrEmpty(tuKhoa)
                     || mon.TenMonAn.ToLower().Contains(tuKhoa)
                    )
                )
                .ToList();

            // 4. Đổ dữ liệu đã lọc qua hàm cập nhật (có tự động giải phóng RAM ảnh cũ)
            TaiDanhSachThucDon(danhSachDaLoc);
        }

        private void cboDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLocThucDon();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ThucHienLocThucDon();
        }

        private void dgvThucDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy đối tượng MonAnDTO được liên kết với dòng này
            if (dgvThucDon.Rows[e.RowIndex].DataBoundItem is MonAnDTO monAn)
            {
                txtTenMonAn.Text = monAn.TenMonAn;

                // Bạn có thể format đơn giá dạng số cho đẹp (Ví dụ: 50000 -> 50,000)
                txtDonGia.Text = monAn.DonGia.ToString("N0");

                pbAnhMonAn.Image = monAn.HinhAnhHienThi;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDatHang_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra giỏ hàng trước khi xử lý (Bảo vệ giao diện sớm)
                if (_gioHang == null || _gioHang.Count == 0)
                {
                    MessageBox.Show("Giỏ hàng của bạn đang trống! Hãy thêm món ăn trước khi đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string diaChi = _taiKhoanNguoiDung.DiaChi;

                // 2. Lấy thông tin địa chỉ giao hàng trực tiếp từ ô nhập liệu trên giao diện (Cho phép người dùng tùy ý chỉnh sửa)
                string diaChiGiao = diaChi.Trim();
                if (string.IsNullOrEmpty(diaChiGiao))
                {
                    MessageBox.Show("Vui lòng điền địa chỉ nhận đồ ăn trước khi đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Xác nhận lại với khách hàng trước khi chạy logic nặng
                var xacNhan = MessageBox.Show($"Bạn có chắc chắn muốn tiến hành đặt đơn hàng này đến địa chỉ {diaChi} không?", "Xác nhận đặt hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (xacNhan == DialogResult.No) return;

                // 4. Tự sinh mã hóa đơn ngay trên GUI
                string maHDMoi = SinhMaHoaDon();

                // Convert BindingList sang List thông thường để truyền xuống BLL xử lý
                List<ChiTietGioHangDTO> dsChiTiet = _gioHang.ToList();

                // Lấy mã tài khoản từ DTO người dùng (Phòng hờ trường hợp đối tượng null)
                string maKH = _taiKhoanNguoiDung != null ? _taiKhoanNguoiDung.MaTaiKhoan : null;

                // 5. Gọi tầng BLL để thực thi lưu trữ (BLL sẽ tự động Validate dữ liệu và ném Exception nếu có lỗi)
                if (_donHangBLL.DatHangOffline(maHDMoi, maKH, diaChiGiao, dsChiTiet))
                {
                    // Nếu đặt hàng thành công và ghi nhận xuống DB mượt mà
                    MessageBox.Show($"Đặt hàng thành công!\nMã đơn hàng của bạn là: {maHDMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Dọn dẹp giỏ hàng offline và reset giao diện
                    LamMoiGioHangSauKhiDat();
                }
                else
                {
                    // Phòng hờ trường hợp hàm trả về false nhưng không ném lỗi
                    MessageBox.Show("Đặt hàng thất bại do lỗi hệ thống không xác định.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Toàn bộ lỗi từ BLL (nhập thiếu, sai logic) hay lỗi Database (Rollback) 
                // đều được túm gọn an toàn tại đây để hiển thị trực quan cho người dùng.
                MessageBox.Show(ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Hàm bổ trợ dọn dẹp giỏ hàng sau khi lưu DB thành công
        private void LamMoiGioHangSauKhiDat()
        {
            _gioHang.Clear(); // Xóa sạch BindingList (Lưới tự động xóa dòng hiển thị)
            txtTongTien.Text = "0"; // Đưa tổng tiền về 0
            nudSoLuong.Value = 1; // Reset số lượng món ăn về 1

        }
    }
}

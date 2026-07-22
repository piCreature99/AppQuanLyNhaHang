using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_Shipper.BLL;
using App_Shipper.DAL;
using App_Shipper.DTOS;
using App_Shipper.Helpers;
using App_Shipper.Services;

namespace App_Shipper.Views
{
    public partial class FormQuanLyHoaDon : Form
    {
        private BindingList<DonHangDTO> _dsDonHang;
        private readonly DonHangBLL _donHangBLL = new DonHangBLL();
        private FormMaster _master;
        private string _idShipper;
        private HelperFunctions _helperFuncs = new HelperFunctions();
        private DonHangPollingService _pollingService;
        private DonHangDTO _donHangDangChon;

        private string _tenAnhKhachHangDangChon;
        private string _duongDanAnhKhachHangDangChon;
        private string _tenAnhShipperDangChon;
        private string _duongDanAnhShipperDangChon;

        private int _soLuongDonHienTai;

        public FormQuanLyHoaDon(FormMaster master)
        {
            InitializeComponent();
            _master = master;
        }

        public void FormShipperHoaDon_Load(object sender, EventArgs e)
        {
            btnBaoSuCo.Enabled = false;
            btnDaGiao.Enabled = false;

            _helperFuncs.DefaultImgInitializer();
            NapAnhDaiDienKhachHang("default.png", "client");
            NapAnhDaiDienShipper("default.png", "shipper");
            _idShipper = _master._idShipper;

            btnDaGiao.Enabled = false;

            FormatDgvCoBan(dgvHoaDon);
            FormatDgvCoBan(dgvChiTietHoaDon);

            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular); ;

            dgvChiTietHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            dgvChiTietHoaDon.DefaultCellStyle.Font = new Font("Segoe UI", 7, FontStyle.Regular); ;

            CaiDatComboBoxTrangThai();
            LaySoLuongHoaDon();
            TaiDanhSachDonHang();

            // Khởi động dịch vụ Polling
            _pollingService = new DonHangPollingService();

            // Đăng ký nhận sự kiện
            _pollingService.OnOrderStatusChanged += PollingService_OnOrderStatusChanged;
            _pollingService.OnOrderCountChanged += PollingService_OnOrderCountChanged;

            _pollingService.Start(_master._idShipper, 2000); // Chạy quét 2 giây/lần
        }

        private void PollingService_OnOrderCountChanged(int soLuongDonMoi)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(new Action(() =>
            {
                //MessageBox.Show(soLuongDonMoi.ToString() + _soLuongDonHienTai.ToString());
                if (soLuongDonMoi != _soLuongDonHienTai)
                {
                    TaiDanhSachDonHang();
                    _soLuongDonHienTai = soLuongDonMoi;
                }

            }));
        }

        private void PollingService_OnOrderStatusChanged(Dictionary<string, Tuple<string, string>> dictTrangThaiMoi)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(new Action(() =>
            {
                bool coThayDoi = false;

                foreach (var item in dictTrangThaiMoi)
                {
                    string maDon = item.Key;

                    Tuple<string, string> thongTinMoi = item.Value;

                    string trangThaiMoiTuDB = thongTinMoi.Item1;
                    string tenShipperMoiTuDB = thongTinMoi.Item2;
                    //MessageBox.Show(maDon);

                    // Đối chiếu với bộ nhớ Cache RAM hiện tại toàn Form
                    if (_dsDonHang.Any(dh => dh.MaDonHang == maDon))
                    {
                        // 1. Tìm đơn hàng trong List theo mã đơn
                        var donHang = _dsDonHang.FirstOrDefault(dh => dh.MaDonHang == maDon);
                        // Nếu trạng thái trong DB khác với trạng thái app đang giữ trên RAM -> Có sự thay đổi từ bên ngoài!
                        if (donHang != null)
                        {
                            if (donHang.TrangThaiDonHang != trangThaiMoiTuDB)
                            {
                                // Cập nhật ô trạng thái trực tiếp trên RAM ô nhớ
                                donHang.TrangThaiDonHang = trangThaiMoiTuDB;
                            }

                            if (donHang.TenShipper != tenShipperMoiTuDB)
                            {
                                donHang.TenShipper = tenShipperMoiTuDB;
                            }

                            coThayDoi = true;
                        }

                        // Nếu tên shipper trong DB khác với trạng thái app đang giữ trên RAM
                    }
                }

                // ĐÂY LÀ CHÌA KHÓA: Nếu phát hiện có đơn đổi trạng thái, chỉ ra lệnh vẽ lại chữ mới
                // Tuyệt đối không gán lại DataSource, không bị giật lag hay mất vị trí cuộn chuột của người dùng!
                if (coThayDoi)
                {
                    dgvHoaDon.Refresh();
                }
            }));
        }

        // Khi đóng Form nhớ tắt Polling để không bị chạy ngầm hao tổn tài nguyên máy
        private void FrmShipperDonHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_pollingService != null)
            {
                _pollingService.Stop();
            }
        }

        public void FormShipperHoaDon_FormClosed(object sender, EventArgs e)
        {
            pbKhachHang.Image?.Dispose();
            pbShipper.Image?.Dispose();
        }

        private void LaySoLuongHoaDon()
        {
            try
            {
                _soLuongDonHienTai = _donHangBLL.LayTongSoHoaDon(_master._idShipper);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lấy số lượng hóa đơn thất bại");
            }
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

        private void TaiDanhSachDonHang()
        {
            _dsDonHang = new BindingList<DonHangDTO>(_donHangBLL.LayDanhSachDonHangGop(_idShipper));

            dgvHoaDon.DataSource = null;
            if (_dsDonHang != null && _dsDonHang.Count > 0)
            {
                dgvHoaDon.DataSource = _dsDonHang;
                if (dgvHoaDon.Columns.Contains("DiaChiKhachHang"))
                {
                    dgvHoaDon.Columns["DiaChiKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dgvHoaDon.Columns.Contains("TongTien"))
                {
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                }
            }
        }

        private void NapAnhDaiDienKhachHang(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (pbKhachHang.Image != null)
                {
                    pbKhachHang.Image.Dispose();
                    pbKhachHang.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    pbKhachHang.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhKhachHangDangChon = tenAnh;
                    _duongDanAnhKhachHangDangChon = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    pbKhachHang.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                pbKhachHang.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }
        private void NapAnhDaiDienShipper(string tenAnh, string vaiTro)
        {
            // Xử lý nạp ảnh đại diện nếu có tên file ảnh
            string folderPath = _helperFuncs.KhoiTaoFolderDichTheoVaiTro(vaiTro);
            string defaultPath = Path.Combine(folderPath, "default.png");
            if (!string.IsNullOrEmpty(tenAnh))
            {
                if (pbShipper.Image != null)
                {
                    pbShipper.Image.Dispose();
                    pbShipper.Image = null;
                }

                string fullPath = Path.Combine(folderPath, tenAnh);

                if (File.Exists(fullPath))
                {
                    // Nạp ảnh gián tiếp qua MemoryStream để tránh chiếm dụng (khóa) file ảnh trên ổ đĩa

                    pbShipper.Image = _helperFuncs.LoadImage(fullPath);
                    _tenAnhShipperDangChon = tenAnh;
                    _duongDanAnhShipperDangChon = fullPath;
                    //MessageBox.Show(defaultPath);
                }
                else
                {
                    //MessageBox.Show(defaultPath);
                    pbShipper.Image = _helperFuncs.LoadImage(defaultPath); // Hoặc gán một ảnh mặc định "no-avatar.png"
                }
            }
            else
            {
                pbShipper.Image = _helperFuncs.LoadImage(defaultPath);
            }
        }
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có đang chọn dòng hợp lệ nào không
            if (dgvHoaDon.CurrentRow != null && dgvHoaDon.CurrentRow.DataBoundItem != null)
            {
                // 1. Lấy ra chính xác đối tượng Đơn hàng thuộc dòng đang được chọn (Ép kiểu về DonHangDTO)
                _donHangDangChon = (DonHangDTO)dgvHoaDon.CurrentRow.DataBoundItem;

                // Nạp thông tin khách và shipper
                txtTenKhachHang.Text = _donHangDangChon.TenKhachHang;
                txtSoDienThoaiKhachHang.Text = _donHangDangChon.SDTKhachHang;
                txtDiaChiKhachHang.Text = _donHangDangChon.DiaChiKhachHang;
                NapAnhDaiDienKhachHang(_donHangDangChon.HinhAnhKhachHang, "client");

                txtTenShipper.Text = _donHangDangChon.TenShipper;
                txtSoDienThoaiShipper.Text = _donHangDangChon.SDTShipper;
                NapAnhDaiDienShipper(_donHangDangChon.HinhAnhShipper, "shipper");

                btnBaoSuCo.Enabled = btnDaGiao.Enabled = _donHangDangChon.TrangThaiDonHang != "Đang giao" ? false : true;
                

                // 2. Nạp thẳng DanhSachChiTiet của đơn hàng này vào dgvChiTietHoaDon
                dgvChiTietHoaDon.DataSource = null; // Reset lưới chi tiết trước khi nạp mới

                if (_donHangDangChon.DanhSachChiTiet != null && _donHangDangChon.DanhSachChiTiet.Count > 0)
                {
                    dgvChiTietHoaDon.DataSource = _donHangDangChon.DanhSachChiTiet;
                    if (dgvChiTietHoaDon.Columns.Contains("SoLuong"))
                    {
                        dgvChiTietHoaDon.Columns["SoLuong"].Width = 50;
                    }
                    if (dgvChiTietHoaDon.Columns.Contains("GiaBan"))
                    {
                        dgvChiTietHoaDon.Columns["GiaBan"].Width = 60;
                        dgvChiTietHoaDon.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";

                    }
                    if (dgvChiTietHoaDon.Columns.Contains("ThanhTien"))
                    {
                        dgvChiTietHoaDon.Columns["ThanhTien"].Width = 60;
                        dgvChiTietHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
                    }
                }
            }
            else
            {
                // Nếu không có dòng nào được chọn, làm sạch lưới chi tiết
                dgvChiTietHoaDon.DataSource = null;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtSearch.Text.Trim().ToLower();

            // 1. Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách gốc
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dgvHoaDon.DataSource = null;
                dgvHoaDon.DataSource = _dsDonHang; // _dsDonHang là biến toàn cục chứa List gốc
                if (dgvHoaDon.Columns.Contains("DiaChiKhachHang"))
                {
                    dgvHoaDon.Columns["DiaChiKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                return;
            }

            // 2. Dùng LINQ để lọc các đơn hàng có chứa từ khóa (Tìm kiếm tương đối)
            List<DonHangDTO> danhSachLoc = _dsDonHang
                .Where(dh => dh.MaDonHang != null && dh.MaDonHang.ToLower().Contains(tuKhoa))
                .ToList();

            // 3. Cập nhật lại lưới hiển thị
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = danhSachLoc;
            if (dgvHoaDon.Columns.Contains("DiaChiKhachHang"))
            {
                dgvHoaDon.Columns["DiaChiKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvHoaDon.Columns.Contains("TongTien"))
            {
                dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }
        }

        private void cboFilterTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string trangThaiChon = cboFilterTrangThai.Text;

            // 1. Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách gốc
            if (trangThaiChon == "Tất cả")
            {
                dgvHoaDon.DataSource = null;
                dgvHoaDon.DataSource = _dsDonHang; // _dsDonHang là biến toàn cục chứa List gốc
                if (dgvHoaDon.Columns.Contains("DiaChiKhachHang"))
                {
                    dgvHoaDon.Columns["DiaChiKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (dgvHoaDon.Columns.Contains("TongTien"))
                {
                    dgvHoaDon.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
                }
                return;
            }

            // 2. Dùng LINQ để lọc các đơn hàng có chứa từ khóa (Tìm kiếm tương đối)
            List<DonHangDTO> danhSachLoc = _dsDonHang
                .Where(dh => dh.TrangThaiDonHang != null && dh.TrangThaiDonHang.Contains(trangThaiChon))
                .ToList();

            // 3. Cập nhật lại lưới hiển thị
            dgvHoaDon.DataSource = null;
            dgvHoaDon.DataSource = danhSachLoc;
            if (dgvHoaDon.Columns.Contains("DiaChiKhachHang"))
            {
                dgvHoaDon.Columns["DiaChiKhachHang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void LamMoi()
        {
            pbKhachHang.Image?.Dispose();
            pbShipper.Image?.Dispose();
            pbKhachHang.Image = null;
            pbShipper.Image = null;

            txtDiaChiKhachHang.Clear();
            txtSearch.Clear();
            txtSoDienThoaiKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtTenShipper.Clear();
            txtSoDienThoaiShipper.Clear();
            cboFilterTrangThai.SelectedIndex = 0;

            btnBaoSuCo.Enabled = false;
            btnDaGiao.Enabled = false;

            dgvChiTietHoaDon.DataSource = null;
            _donHangDangChon = null;
        }

        private void btnDaGiao_Click(object sender, EventArgs e)
        {
            if (_donHangDangChon == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn từ bảng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string tieuDe = "XÁC NHẬN GIAO ĐƠN HÀNG";
            string noiDungCanhBao = $"- Xác nhận đã giao đến đị chỉa {_donHangDangChon.DiaChiGiaoHang}?";

            // 2. Hiển thị hộp thoại Yes/No
            DialogResult luaChon = MessageBox.Show(noiDungCanhBao, tieuDe, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (luaChon == DialogResult.No) return;

            try
            {
                bool ketQua = _donHangBLL.GiaoDonHang(_donHangDangChon.MaDonHang);

                if (ketQua)
                {
                    MessageBox.Show("Cập nhật đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachDonHang();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBaoSuCo_Click(object sender, EventArgs e)
        {
            string tieuDe = "XÁC NHẬN BÁO SỰ CỐ CHO QUẢN LÝ";
            string noiDungCanhBao = "Bạn có chắc chắn muốn gửi thông báo này không?";

            // 2. Hiển thị hộp thoại Yes/No
            DialogResult luaChon = MessageBox.Show(noiDungCanhBao, tieuDe, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (luaChon == DialogResult.No) return;
            try
            {
                bool ketQua = _donHangBLL.BaoSuCo(_donHangDangChon.MaDonHang);

                if (ketQua)
                {
                    MessageBox.Show("Đã thông báo với quản lý thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachDonHang();
                    LamMoi();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

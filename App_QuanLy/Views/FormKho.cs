using System;
using System.Data;
using System.Windows.Forms;
using App_QuanLy.BLL;
using App_QuanLy.Models;
using App_QuanLy.Helpers;
using Microsoft.Data.SqlClient;

namespace App_QuanLy.Views
{
    public partial class FormKho : Form
    {
        private KhoHangBLL _khoHangBLL = new KhoHangBLL();
        private int _maNguyenLieuDangChon = -1;

        public FormKho()
        {
            InitializeComponent();
        }

        private void FrmKho_Load(object sender, EventArgs e)
        {
            // Ngăn chặn sửa trực tiếp trên tất cả các ô của GridView
            DgvStorage.ReadOnly = true;

            // (Tùy chọn thêm) Ngăn người dùng nhấn nút Delete trên bàn phím để xóa dòng bừa bãi
            DgvStorage.AllowUserToDeleteRows = false;

            // (Tùy chọn thêm) Ngăn người dùng tự bấm chuột vào dòng trống cuối cùng để thêm dòng bừa bãi
            DgvStorage.AllowUserToAddRows = false;

            // Đổi chế độ chọn từ 1 ô sang chọn TOÀN BỘ HÀNG
            DgvStorage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chỉ cho phép chọn 1 hàng duy nhất tại một thời điểm (Tránh việc giữ Ctrl chọn nhiều hàng bừa bãi)
            DgvStorage.MultiSelect = false;

            LoadDanhSachNguyenLieu();
            CaiDatComboBoxTrangThai();
        }

        private void FrmKho_Shown(object sender, EventArgs e)
        {
            //HelperFunctions.ApplyRoundedButtons(panelMain.Controls, 10);
        }

        private void LoadDanhSachNguyenLieu(string tuKhoa = "")
        {
            try
            {
                DgvStorage.DataSource = _khoHangBLL.LayDanhSach(tuKhoa);
                // Thay đổi tiêu đề hiển thị (HeaderText) cho từng cột
                DgvStorage.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                DgvStorage.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 17, 23); // Màu rượu vang đậm
                DgvStorage.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                DgvStorage.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None; // Xóa viền giữa các cột
                DgvStorage.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DgvStorage.DefaultCellStyle.BackColor = Color.FromArgb(65, 29, 34); // Màu rượu vang nhạt hơn
                DgvStorage.DefaultCellStyle.ForeColor = Color.White;
                DgvStorage.DefaultCellStyle.SelectionBackColor = Color.Black; // Màu khi chọn dòng
                DgvStorage.DefaultCellStyle.SelectionForeColor = Color.White;
                DgvStorage.DefaultCellStyle.SelectionBackColor = Color.FromArgb(100, 20, 20); // Một màu đỏ rượu vang đậm hơn bình thường
                DgvStorage.DefaultCellStyle.SelectionForeColor = Color.White;
                DgvStorage.EnableHeadersVisualStyles = false;
                DgvStorage.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(120, 0, 0);
                DgvStorage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                DgvStorage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DgvStorage.ColumnHeadersHeight = 50;
                DgvStorage.AllowUserToResizeColumns = false;
                DgvStorage.AllowUserToResizeRows = false;
                DgvStorage.AllowUserToOrderColumns = false;
                DgvStorage.ReadOnly = true;
                DgvStorage.MultiSelect = false;
                DgvStorage.RowTemplate.Height = 40;
                //DgvStorage.ScrollBars = ScrollBars.None;
                DgvStorage.Columns["MaNguyenLieu"].HeaderText = "Mã Nguyên Liệu";
                DgvStorage.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
                DgvStorage.Columns["TenNguyenLieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DgvStorage.Columns["SoLuongTon"].HeaderText = "Số Lượng Tồn";
                DgvStorage.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
                DgvStorage.Columns["GiaNhapGanNhat"].HeaderText = "Giá Nhập Gần Nhất";
                DgvStorage.Columns["TonToiThieu"].HeaderText = "Tồn Tối Thiểu";
                DgvStorage.Columns["TrangThai"].HeaderText = "Trạng Thái"; // Cột tính toán động từ SQL
                DgvStorage.Columns["TrangThaiKinhDoanh"].HeaderText = "Kinh Doanh"; // Cột tính toán động từ SQL
                DgvStorage.Columns["TrangThaiKinhDoanh"].Width = 60;

                if (DgvStorage.Columns["MaNguyenLieu"] != null)
                    DgvStorage.Columns["MaNguyenLieu"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối database: " + ex.Message, "Lỗi Hệ Thống");
            }
        }

        private void CaiDatComboBoxTrangThai()
        {
            CboStatus.Items.Clear();
            CboStatus.Items.Add("Tất cả");
            CboStatus.Items.Add("Còn hàng");
            CboStatus.Items.Add("Sắp hết hàng");
            CboStatus.Items.Add("Hết hàng");
            CboStatus.SelectedIndex = 0;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDanhSachNguyenLieu(TxtSearch.Text.Trim());
        }

        private void DgvStorage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DgvStorage.Rows[e.RowIndex];

                _maNguyenLieuDangChon = Convert.ToInt32(row.Cells["MaNguyenLieu"].Value);


                bool dangHoatDong = Convert.ToBoolean(row.Cells["TrangThaiKinhDoanh"].Value);

                // THAY ĐỔI GIAO DIỆN NÚT BẤM LINH HOẠT
                if (dangHoatDong)
                {
                    btnActivate.Text = "Ngưng Kinh doanh";
                    btnActivate.BackColor = Color.Tomato; // Màu đỏ cảnh báo
                }
                else
                {
                    btnActivate.Text = "Kích hoạt lại";
                    btnActivate.BackColor = Color.MediumSeaGreen; // Màu xanh mở khóa
                }

                //TxtId.Text = _maNguyenLieuDangChon.ToString();
                TxtName.Text = row.Cells["TenNguyenLieu"].Value?.ToString();
                nudSoLuongTon.Value = Convert.ToInt32(row.Cells["SoLuongTon"].Value);
                TxtUnit.Text = row.Cells["DonViTinh"].Value?.ToString();
                nudDonGia.Value = Convert.ToDecimal(row.Cells["GiaNhapGanNhat"].Value);
                nudTonToiThieu.Value = Convert.ToDecimal(row.Cells["TonToiThieu"].Value);
                //CboStatus.SelectedItem = row.Cells["TrangThai"].Value?.ToString() ?? "Còn hàng";
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                NguyenLieu nl = new NguyenLieu()
                {
                    TenNguyenLieu = TxtName.Text.Trim(),
                    SoLuongTon = nudSoLuongTon.Value,
                    DonViTinh = TxtUnit.Text.Trim(),
                    GiaNhapGanNhat = nudDonGia.Value,
                    TonToiThieu = nudSoLuongTon.Value,
                    TrangThai = CboStatus.SelectedItem?.ToString() ?? "Còn hàng"
                };

                bool ketQua = _khoHangBLL.ThemNguyenLieu(nl);
                if (ketQua)
                {
                    MessageBox.Show("Thêm nguyên liệu thành công!");
                    LoadDanhSachNguyenLieu();
                    XoaTrangForm();
                }
                else MessageBox.Show("Them nguyên liệu thất bại");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu: ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng số: " + ex.Message);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_maNguyenLieuDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!");
                return;
            }

            try
            {
                NguyenLieu nl = new NguyenLieu()
                {
                    MaNguyenLieu = _maNguyenLieuDangChon,
                    TenNguyenLieu = TxtName.Text.Trim(),
                    SoLuongTon = nudSoLuongTon.Value,
                    DonViTinh = TxtUnit.Text.Trim(),
                    GiaNhapGanNhat = nudDonGia.Value,
                    TonToiThieu = nudTonToiThieu.Value,
                    TrangThai = CboStatus.SelectedItem.ToString()
                };

                bool ketQua = _khoHangBLL.CapNhatNguyenLieu(nl);
                if (ketQua)
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadDanhSachNguyenLieu();
                    XoaTrangForm();
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng dữ liệu: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_maNguyenLieuDangChon == -1) return;

            // Hỏi xác nhận trước khi thực hiện hành động xóa
            DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa nguyên liệu đang chọn không?",
                                              "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            int maMonAn = Convert.ToInt32(_maNguyenLieuDangChon);

            // 2. Gọi hàm xóa từ BLL
            string ketQua = _khoHangBLL.XoaNguyenLieu(maMonAn);

            if (ketQua == "XOA_THANH_CONG")
            {
                MessageBox.Show("Đã xóa hoàn toàn nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachNguyenLieu();
                XoaTrangForm();        // Xóa trắng form nhập liệu
            }
            else if (ketQua == "YEU_CAU_CHUYEN_TRANG_THAI")
            {
                // TRƯỜNG HỢP ĐẶC BIỆT: Phát hiện có giao dịch hóa đơn liên quan
                DialogResult luaChon = MessageBox.Show(
                    "Nguyên liệu này này đã có lịch sử giao dịch (hóa đơn) trong hệ thống nên không thể xóa bỏ hoàn toàn.\n\n" +
                    "Bạn có muốn chuyển trạng thái nguyên liệu này sang [Ngừng Kinh Doanh] để ẩn khỏi thực đơn bán hàng không?",
                    "Nguyên liệu đã có dữ liệu ràng buộc",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (luaChon == DialogResult.Yes)
                {
                    bool ok = _khoHangBLL.ChuyenTrangThaiNgungKinhDoanh(maMonAn, false);
                    if (ok)
                    {
                        MessageBox.Show("Đã chuyển trạng thái nguyên liệu sang Ngừng Kinh Doanh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachNguyenLieu();
                        XoaTrangForm();

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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            XoaTrangForm();
        }

        private void XoaTrangForm()
        {
            _maNguyenLieuDangChon = -1;
            //TxtId.Clear();
            TxtName.Clear();
            nudDonGia.Value = 0;
            TxtUnit.Clear();
            nudTonToiThieu.Value = 0;
            nudSoLuongTon.Value = 0;
            if (CboStatus.Items.Count > 0) CboStatus.SelectedIndex = 0;
        }

        private void CboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem DataGridView đã có dữ liệu (DataTable) chưa, nếu chưa thì bỏ qua
            if (DgvStorage.DataSource == null) return;

            // Ép kiểu DataSource về DataTable (nếu trước đó bạn gán bằng DataTable)
            DataTable dt = DgvStorage.DataSource as DataTable;

            // Nếu trước đó bạn đi qua BindingSource thì dùng dòng này:
            // DataTable dt = (DgvStorage.DataSource as BindingSource)?.DataSource as DataTable;

            if (dt != null)
            {
                string trangThaiChon = CboStatus.SelectedItem.ToString();

                // 2. Nếu chọn "Tất cả" hoặc không chọn gì -> Hủy bộ lọc, hiển thị toàn bộ
                if (trangThaiChon == "Tất cả" || string.IsNullOrEmpty(trangThaiChon))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    // 3. Lọc theo giá trị chính xác của cột [TrangThai] được sinh động từ SQL
                    dt.DefaultView.RowFilter = string.Format("TrangThai = '{0}'", trangThaiChon);
                }
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem người dùng đã chọn shipper nào chưa
                if (_maNguyenLieuDangChon == -1)
                {
                    MessageBox.Show("Vui lòng chọn một Nguyên liệu từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataRowView currentRowView = (DataRowView)DgvStorage.CurrentRow.DataBoundItem;
                DataRow row = currentRowView.Row;

                // Lấy trạng thái hiện tại để đảo ngược nó (Toggle)
                bool trangThaiHienTai = Convert.ToBoolean(row["TrangThaiKinhDoanh"]);
                bool trangThaiMoi = !trangThaiHienTai;

                string action = trangThaiMoi ? "kích hoạt lại" : "ngưng kinh doanh";

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn {action} nguyên liệu [{_maNguyenLieuDangChon}]?",
                    "Xác nhận thay đổi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    // 3. Gọi BLL thực thi ngưng hoạt động (Xóa mềm / Update trạng thái)
                    if (_khoHangBLL.ChuyenTrangThaiNgungKinhDoanh(_maNguyenLieuDangChon, trangThaiMoi))
                    {
                        LoadDanhSachNguyenLieu();

                        MessageBox.Show($"Đã {action} nguyên liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        XoaTrangForm();
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

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void nudSoLuongTon_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
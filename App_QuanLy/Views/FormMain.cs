using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using App_QuanLy.BLL;

namespace App_QuanLy.Views
{
    public partial class FormMain : Form
    {
        private Form activeForm = null;
        // Cấu hình tỷ lệ thiết kế chuẩn ban đầu (1024 x 768)
        private const float TARGET_ASPECT_RATIO = 1280f / 720f;
        private const int MIN_WIDTH = 1296;
        private const int MIN_HEIGHT = 759;
        private FormMaster _master;
        private System.Windows.Forms.Timer pollingTimer;
        private bool hasNewInvoice = false; // Trạng thái kiểm tra xem có hóa đơn mới không
        private bool isHoaDonSelected = false; // Trạng thái xem người dùng có đang ở màn hình Hóa đơn không
        private DonHangBLL _donHangBLL = new DonHangBLL();
        private TaiKhoanBLL _taiKhoanBLL = new TaiKhoanBLL();
        private int currentInvoiceCount = 0;
        public FormMain(FormMaster master)
        {
            InitializeComponent();
            _master = master;
            int result = _donHangBLL.LayTongSoHoaDon();

                currentInvoiceCount = result;

            // Cấu hình Timer cho việc Polling
            pollingTimer = new System.Windows.Forms.Timer();
            pollingTimer.Interval = 5000; // Cứ mỗi 5 giây (5000ms) kiểm tra 1 lần, bạn có thể chỉnh lại số này
            pollingTimer.Tick += PollingTimer_Tick;
            pollingTimer.Start(); // Bắt đầu chạy ngầm
        }

        private void PollingTimer_Tick(object sender, EventArgs e)
        {

            int newCount = 0;
            int result = _donHangBLL.LayTongSoHoaDon();

                // Lấy ô ở dòng 0, cột 0 và chuyển thành kiểu int
                newCount = result;

            //MessageBox.Show(newCount.ToString() + currentInvoiceCount.ToString());
            // Nếu người dùng ĐANG mở màn hình hóa đơn rồi thì không cần hiện dấu chấm than nữa
            if (isHoaDonSelected) {
                currentInvoiceCount = newCount;
            }
            else if (newCount > currentInvoiceCount)
            {
                hasNewInvoice = true;
                // Đổi ảnh sang icon có dấu chấm than (Ví dụ ảnh đặt tên là: pbhoadon_alert.png)
                string alertPath = Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbhoadon_alert.png");
                if (File.Exists(alertPath))
                {
                    // Tránh việc nạp lại ảnh liên tục gây giật lag giao diện
                    if (pbHoaDon.Image != null) pbHoaDon.Image.Dispose();
                    pbHoaDon.Image = Image.FromFile(alertPath);
                }
            }
        }

        // Hàm xử lý khi nhấn vào bất kỳ nút nào
        private void OnMenuButtonClicked(object sender, EventArgs e)
        {
            // 1. Reset tất cả về trạng thái "chưa chọn" (Ảnh bình thường)
            ResetAllButtons();

            // 2. Chuyển nút vừa nhấn sang ảnh "đã chọn"
            PictureBox clickedPb = sender as PictureBox;
            if (clickedPb == null) return;

            // Thay đổi ảnh dựa trên tên nút
            // Lưu ý: Bạn cần đảm bảo các file ảnh 'selected' đã có trong thư mục Images
            string imgPath = Path.Combine(Application.StartupPath, "Images/Buttons/Storage", clickedPb.Name.ToLower() + "_selected.png");
            //MessageBox.Show(imgPath);

            if (File.Exists(imgPath))
            {
                clickedPb.Image = Image.FromFile(imgPath);
            }
        }
        private void OnHoaDonButtonClicked(object sender, EventArgs e)
        {
            // 1. Reset tất cả về trạng thái "chưa chọn" (Ảnh bình thường)
            if (pbTaiKhoan.Image != null) pbTaiKhoan.Image.Dispose();
            if (pbKho.Image != null) pbKho.Image.Dispose();
            if (pbThucDon.Image != null) pbThucDon.Image.Dispose();
            if (pbHoaDon.Image != null) pbHoaDon.Image.Dispose();

            pbTaiKhoan.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbtaikhoan_normal.png"));
            pbKho.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbkho_normal.png")); // Lưu ý logic ảnh ở đây
            pbThucDon.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbthucdon_normal.png"));

            // 2. Chuyển nút vừa nhấn sang ảnh "đã chọn"
            PictureBox clickedPb = sender as PictureBox;
            if (clickedPb == null) return;

            // Thay đổi ảnh dựa trên tên nút
            // Lưu ý: Bạn cần đảm bảo các file ảnh 'selected' đã có trong thư mục Images
            string imgPath = Path.Combine(Application.StartupPath, "Images/Buttons/Storage", clickedPb.Name.ToLower() + "_selected.png");
            //MessageBox.Show(imgPath);

            if (File.Exists(imgPath))
            {
                clickedPb.Image = Image.FromFile(imgPath);
            }
        }

        // Khi click vào PictureBox Kho
        private void pbKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormKho());
            OnMenuButtonClicked(sender, e);
        }

        // Khi click vào PictureBox Hóa đơn
        private void pbHoaDon_Click(object sender, EventArgs e)
        {
            hasNewInvoice = false;
            isHoaDonSelected = true;

            OpenChildForm(new FormQuanLyHoaDon());
            OnHoaDonButtonClicked(sender, e);
        }

        // Khi click vào PictureBox Tài khoản
        private void pbTaiKhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyTaiKhoan());
            OnMenuButtonClicked(sender, e);
        }

        // Khi click vào PictureBox Thực đơn
        private void pbThucDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormThucDon());
            OnMenuButtonClicked(sender, e);
        }

        private void ResetAllButtons()
        {
            isHoaDonSelected = false;
            // Đưa tất cả về ảnh "normal"
            // Giải phóng bộ nhớ các ảnh cũ trước khi nạp ảnh mới (Tránh ngốn RAM app)
            if (pbTaiKhoan.Image != null) pbTaiKhoan.Image.Dispose();
            if (pbKho.Image != null) pbKho.Image.Dispose();
            if (pbThucDon.Image != null) pbThucDon.Image.Dispose();

            pbTaiKhoan.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbtaikhoan_normal.png"));
            pbKho.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbkho_normal.png")); // Lưu ý logic ảnh ở đây
            pbThucDon.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbthucdon_normal.png"));

            // RIÊNG NÚT HÓA ĐƠN:
            // Nếu có hóa đơn mới mà người dùng chưa thèm xem, thì khi reset các nút khác, 
            // nút Hóa đơn vẫn phải giữ nguyên ảnh dấu chấm chỉ (alert), ngược lại mới về normal.
            if (pbHoaDon.Image != null) pbHoaDon.Image.Dispose();

            if (hasNewInvoice)
            {
                pbHoaDon.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbhoadon_alert.png"));
            }
            else
            {
                pbHoaDon.Image = Image.FromFile(Path.Combine(Application.StartupPath, "Images/Buttons/Storage", "pbhoadon_normal.png"));
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult luaChon = MessageBox.Show(
                    "Quay trở lại đăng nhập?",
                    "Kết thúc phiên đăng nhập này?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

            if(luaChon == DialogResult.Yes)
            {
                if (_master.idAdmin != null)
                {
                    _taiKhoanBLL.KichHoatTrangThaiTaiKhoan(_master.idAdmin, "Đang nghỉ");
                }
                _master.HienThiFormDangNhap();
            }
        }

        private void OpenChildForm(Form childForm)
        {
            // 1. Ẩn Panel ngay lập tức để người dùng không thấy cảnh "vẽ từng phần"
            var oldSize = panelMain.Size;
            panelMain.Size = new Size(0, 0);


            // 2. Thực hiện các thao tác load form (như cũ)
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelMain.Controls.Clear();
            Panel content = childForm.Controls.Find("panelMain", true).FirstOrDefault() as Panel;

            if (content != null)
            {
                content.Location = new Point(0, 0);
                //MessageBox.Show("Hello");
                panelMain.Controls.Add(content); // Chỉ thêm cái Panel vào
            }
            else
            {
                panelMain.Controls.Add(childForm); // Nếu không tìm thấy thì thêm cả cái Form
            }

            // 3. Đợi form con thực sự load xong (sự kiện Load của form con)
            childForm.Show();

            // Sau khi xong
            panelMain.Size = oldSize;
        }

        
    }
}

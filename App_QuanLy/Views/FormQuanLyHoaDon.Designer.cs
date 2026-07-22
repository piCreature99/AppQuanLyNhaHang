namespace App_QuanLy.Views
{
    partial class FormQuanLyHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuanLyHoaDon));
            khachAvatar = new PictureBox();
            shipperAvatar = new PictureBox();
            cboShipper = new ComboBox();
            txtSoDienThoaiShipper = new TextBox();
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            dgvDonHang = new DataGridView();
            panel3 = new Panel();
            label7 = new Label();
            panel1 = new Panel();
            cboFilterTrangThai = new ComboBox();
            panel4 = new Panel();
            txtSearch = new TextBox();
            btnLamMoi = new Button();
            panel2 = new Panel();
            panel14 = new Panel();
            panel13 = new Panel();
            panel12 = new Panel();
            dgvChiTietDonHang = new DataGridView();
            btnInHoaDon = new Button();
            btnGiaoViec = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            panel10 = new Panel();
            panel9 = new Panel();
            panel8 = new Panel();
            txtDiaChiGiao = new TextBox();
            panel7 = new Panel();
            txtSoDienThoai = new TextBox();
            panel6 = new Panel();
            txtTenKhachHang = new TextBox();
            label11 = new Label();
            btnDuyetDon = new Button();
            label12 = new Label();
            btnXongMon = new Button();
            btnHuyDon = new Button();
            ((System.ComponentModel.ISupportInitialize)khachAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)shipperAvatar).BeginInit();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel14.SuspendLayout();
            panel13.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChiTietDonHang).BeginInit();
            panel10.SuspendLayout();
            panel9.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // khachAvatar
            // 
            khachAvatar.Location = new Point(12, 12);
            khachAvatar.Margin = new Padding(3, 2, 3, 2);
            khachAvatar.Name = "khachAvatar";
            khachAvatar.Size = new Size(87, 86);
            khachAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            khachAvatar.TabIndex = 13;
            khachAvatar.TabStop = false;
            // 
            // shipperAvatar
            // 
            shipperAvatar.Location = new Point(12, 12);
            shipperAvatar.Margin = new Padding(3, 2, 3, 2);
            shipperAvatar.Name = "shipperAvatar";
            shipperAvatar.Size = new Size(87, 86);
            shipperAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            shipperAvatar.TabIndex = 19;
            shipperAvatar.TabStop = false;
            // 
            // cboShipper
            // 
            cboShipper.FlatStyle = FlatStyle.Flat;
            cboShipper.FormattingEnabled = true;
            cboShipper.Location = new Point(10, 5);
            cboShipper.Margin = new Padding(3, 2, 3, 2);
            cboShipper.Name = "cboShipper";
            cboShipper.Size = new Size(173, 23);
            cboShipper.TabIndex = 20;
            cboShipper.SelectedIndexChanged += cboShipper_SelectedIndexChanged;
            // 
            // txtSoDienThoaiShipper
            // 
            txtSoDienThoaiShipper.BorderStyle = BorderStyle.None;
            txtSoDienThoaiShipper.Location = new Point(6, 5);
            txtSoDienThoaiShipper.Margin = new Padding(3, 2, 3, 2);
            txtSoDienThoaiShipper.Name = "txtSoDienThoaiShipper";
            txtSoDienThoaiShipper.ReadOnly = true;
            txtSoDienThoaiShipper.Size = new Size(177, 16);
            txtSoDienThoaiShipper.TabIndex = 22;
            txtSoDienThoaiShipper.TextChanged += txtSoDienThoaiShipper_TextChanged;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel5);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel2);
            panelMain.Location = new Point(23, 211);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 24;
            // 
            // panel5
            // 
            panel5.BackgroundImage = Properties.Resources.right_panel;
            panel5.Controls.Add(panel11);
            panel5.Location = new Point(349, 83);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(10);
            panel5.Size = new Size(907, 490);
            panel5.TabIndex = 26;
            // 
            // panel11
            // 
            panel11.BackgroundImage = (Image)resources.GetObject("panel11.BackgroundImage");
            panel11.BackgroundImageLayout = ImageLayout.Stretch;
            panel11.Controls.Add(dgvDonHang);
            panel11.Location = new Point(15, 13);
            panel11.Name = "panel11";
            panel11.Size = new Size(880, 432);
            panel11.TabIndex = 1;
            // 
            // dgvDonHang
            // 
            dgvDonHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDonHang.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvDonHang.BorderStyle = BorderStyle.None;
            dgvDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDonHang.EnableHeadersVisualStyles = false;
            dgvDonHang.GridColor = Color.Maroon;
            dgvDonHang.Location = new Point(10, 10);
            dgvDonHang.Margin = new Padding(10);
            dgvDonHang.Name = "dgvDonHang";
            dgvDonHang.RowHeadersVisible = false;
            dgvDonHang.RowHeadersWidth = 51;
            dgvDonHang.Size = new Size(860, 412);
            dgvDonHang.TabIndex = 0;
            dgvDonHang.CellClick += dgvDonHang_CellClick;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.None;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(btnLamMoi);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(12);
            panel3.Size = new Size(1256, 74);
            panel3.TabIndex = 25;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label7.ForeColor = SystemColors.ControlLight;
            label7.Location = new Point(3, 25);
            label7.Name = "label7";
            label7.Size = new Size(289, 37);
            label7.TabIndex = 25;
            label7.Text = "QUẢN LÝ ĐƠN HÀNG";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(cboFilterTrangThai);
            panel1.Location = new Point(1085, 12);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(159, 50);
            panel1.TabIndex = 23;
            // 
            // cboFilterTrangThai
            // 
            cboFilterTrangThai.BackColor = Color.White;
            cboFilterTrangThai.FlatStyle = FlatStyle.Flat;
            cboFilterTrangThai.Font = new Font("Segoe UI", 12F);
            cboFilterTrangThai.ForeColor = Color.Black;
            cboFilterTrangThai.FormattingEnabled = true;
            cboFilterTrangThai.Location = new Point(14, 13);
            cboFilterTrangThai.Margin = new Padding(3, 2, 3, 2);
            cboFilterTrangThai.Name = "cboFilterTrangThai";
            cboFilterTrangThai.Size = new Size(135, 29);
            cboFilterTrangThai.TabIndex = 8;
            cboFilterTrangThai.SelectedIndexChanged += cboFilterTrangThai_SelectedIndexChanged;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel4.BackgroundImage = (Image)resources.GetObject("panel4.BackgroundImage");
            panel4.Controls.Add(txtSearch);
            panel4.Location = new Point(775, 12);
            panel4.Margin = new Padding(0, 0, 10, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(300, 50);
            panel4.TabIndex = 24;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.White;
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Location = new Point(52, 19);
            txtSearch.Margin = new Padding(3, 2, 3, 2);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Tìm kiếm...";
            txtSearch.Size = new Size(242, 16);
            txtSearch.TabIndex = 7;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Transparent;
            btnLamMoi.BackgroundImage = (Image)resources.GetObject("btnLamMoi.BackgroundImage");
            btnLamMoi.FlatAppearance.BorderColor = Color.Firebrick;
            btnLamMoi.FlatAppearance.BorderSize = 0;
            btnLamMoi.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnLamMoi.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 10F);
            btnLamMoi.ForeColor = SystemColors.ButtonFace;
            btnLamMoi.Location = new Point(614, 12);
            btnLamMoi.Margin = new Padding(0, 0, 10, 0);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(151, 50);
            btnLamMoi.TabIndex = 12;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.left_panel;
            panel2.Controls.Add(panel14);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(panel12);
            panel2.Controls.Add(btnInHoaDon);
            panel2.Controls.Add(btnGiaoViec);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(panel10);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(btnDuyetDon);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(btnXongMon);
            panel2.Controls.Add(btnHuyDon);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // panel14
            // 
            panel14.BackgroundImage = (Image)resources.GetObject("panel14.BackgroundImage");
            panel14.BackgroundImageLayout = ImageLayout.Stretch;
            panel14.Controls.Add(shipperAvatar);
            panel14.Location = new Point(214, 369);
            panel14.Name = "panel14";
            panel14.Size = new Size(110, 110);
            panel14.TabIndex = 26;
            // 
            // panel13
            // 
            panel13.BackgroundImage = (Image)resources.GetObject("panel13.BackgroundImage");
            panel13.BackgroundImageLayout = ImageLayout.Stretch;
            panel13.Controls.Add(khachAvatar);
            panel13.Location = new Point(214, 32);
            panel13.Name = "panel13";
            panel13.Size = new Size(110, 110);
            panel13.TabIndex = 25;
            // 
            // panel12
            // 
            panel12.BackgroundImage = (Image)resources.GetObject("panel12.BackgroundImage");
            panel12.BackgroundImageLayout = ImageLayout.Stretch;
            panel12.Controls.Add(dgvChiTietDonHang);
            panel12.Location = new Point(16, 181);
            panel12.Name = "panel12";
            panel12.Size = new Size(222, 167);
            panel12.TabIndex = 2;
            // 
            // dgvChiTietDonHang
            // 
            dgvChiTietDonHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTietDonHang.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvChiTietDonHang.BorderStyle = BorderStyle.None;
            dgvChiTietDonHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTietDonHang.EnableHeadersVisualStyles = false;
            dgvChiTietDonHang.GridColor = Color.Maroon;
            dgvChiTietDonHang.Location = new Point(10, 10);
            dgvChiTietDonHang.Margin = new Padding(10);
            dgvChiTietDonHang.Name = "dgvChiTietDonHang";
            dgvChiTietDonHang.RowHeadersVisible = false;
            dgvChiTietDonHang.RowHeadersWidth = 51;
            dgvChiTietDonHang.Size = new Size(202, 147);
            dgvChiTietDonHang.TabIndex = 0;
            // 
            // btnInHoaDon
            // 
            btnInHoaDon.BackgroundImage = Properties.Resources.clear_btn;
            btnInHoaDon.BackgroundImageLayout = ImageLayout.Stretch;
            btnInHoaDon.FlatAppearance.BorderSize = 0;
            btnInHoaDon.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnInHoaDon.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnInHoaDon.FlatStyle = FlatStyle.Flat;
            btnInHoaDon.ForeColor = SystemColors.ControlLight;
            btnInHoaDon.Location = new Point(244, 318);
            btnInHoaDon.Margin = new Padding(3, 2, 3, 2);
            btnInHoaDon.Name = "btnInHoaDon";
            btnInHoaDon.Size = new Size(80, 30);
            btnInHoaDon.TabIndex = 32;
            btnInHoaDon.Text = "In hóa đơn";
            btnInHoaDon.UseVisualStyleBackColor = true;
            // 
            // btnGiaoViec
            // 
            btnGiaoViec.BackgroundImage = Properties.Resources.clear_btn;
            btnGiaoViec.BackgroundImageLayout = ImageLayout.Stretch;
            btnGiaoViec.FlatAppearance.BorderSize = 0;
            btnGiaoViec.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnGiaoViec.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnGiaoViec.FlatStyle = FlatStyle.Flat;
            btnGiaoViec.ForeColor = SystemColors.ControlLight;
            btnGiaoViec.Location = new Point(244, 250);
            btnGiaoViec.Margin = new Padding(3, 2, 3, 2);
            btnGiaoViec.Name = "btnGiaoViec";
            btnGiaoViec.Size = new Size(80, 30);
            btnGiaoViec.TabIndex = 31;
            btnGiaoViec.Text = "Giao Việc";
            btnGiaoViec.UseVisualStyleBackColor = true;
            btnGiaoViec.Click += btnGiaoViec_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label8.ForeColor = SystemColors.ControlLight;
            label8.Location = new Point(16, 402);
            label8.Name = "label8";
            label8.Size = new Size(35, 19);
            label8.TabIndex = 30;
            label8.Text = "SĐT";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label9.ForeColor = SystemColors.ControlLight;
            label9.Location = new Point(16, 347);
            label9.Name = "label9";
            label9.Size = new Size(61, 19);
            label9.TabIndex = 29;
            label9.Text = "Shipper";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label10.ForeColor = SystemColors.ControlLight;
            label10.Location = new Point(9, 123);
            label10.Name = "label10";
            label10.Size = new Size(54, 19);
            label10.TabIndex = 28;
            label10.Text = "Địa chỉ";
            // 
            // panel10
            // 
            panel10.BackgroundImage = Properties.Resources.text_box;
            panel10.Controls.Add(txtSoDienThoaiShipper);
            panel10.Location = new Point(16, 424);
            panel10.Name = "panel10";
            panel10.Size = new Size(189, 30);
            panel10.TabIndex = 27;
            // 
            // panel9
            // 
            panel9.BackgroundImage = Properties.Resources.text_box;
            panel9.Controls.Add(cboShipper);
            panel9.Location = new Point(16, 369);
            panel9.Name = "panel9";
            panel9.Size = new Size(189, 30);
            panel9.TabIndex = 26;
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(txtDiaChiGiao);
            panel8.Location = new Point(16, 145);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 25;
            // 
            // txtDiaChiGiao
            // 
            txtDiaChiGiao.BorderStyle = BorderStyle.None;
            txtDiaChiGiao.Location = new Point(6, 5);
            txtDiaChiGiao.Margin = new Padding(3, 2, 3, 2);
            txtDiaChiGiao.Name = "txtDiaChiGiao";
            txtDiaChiGiao.ReadOnly = true;
            txtDiaChiGiao.Size = new Size(177, 16);
            txtDiaChiGiao.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.Controls.Add(txtSoDienThoai);
            panel7.Location = new Point(16, 90);
            panel7.Name = "panel7";
            panel7.Size = new Size(189, 30);
            panel7.TabIndex = 24;
            // 
            // txtSoDienThoai
            // 
            txtSoDienThoai.BorderStyle = BorderStyle.None;
            txtSoDienThoai.Location = new Point(6, 5);
            txtSoDienThoai.Margin = new Padding(3, 2, 3, 2);
            txtSoDienThoai.Name = "txtSoDienThoai";
            txtSoDienThoai.ReadOnly = true;
            txtSoDienThoai.Size = new Size(177, 16);
            txtSoDienThoai.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtTenKhachHang);
            panel6.Location = new Point(16, 35);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 23;
            // 
            // txtTenKhachHang
            // 
            txtTenKhachHang.BorderStyle = BorderStyle.None;
            txtTenKhachHang.Location = new Point(6, 5);
            txtTenKhachHang.Margin = new Padding(3, 2, 3, 2);
            txtTenKhachHang.Name = "txtTenKhachHang";
            txtTenKhachHang.ReadOnly = true;
            txtTenKhachHang.Size = new Size(177, 16);
            txtTenKhachHang.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label11.ForeColor = SystemColors.ControlLight;
            label11.Location = new Point(16, 13);
            label11.Name = "label11";
            label11.Size = new Size(75, 19);
            label11.TabIndex = 14;
            label11.Text = "Tên khách";
            // 
            // btnDuyetDon
            // 
            btnDuyetDon.BackgroundImage = Properties.Resources.clear_btn;
            btnDuyetDon.BackgroundImageLayout = ImageLayout.Stretch;
            btnDuyetDon.FlatAppearance.BorderSize = 0;
            btnDuyetDon.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDuyetDon.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDuyetDon.FlatStyle = FlatStyle.Flat;
            btnDuyetDon.ForeColor = SystemColors.ControlLight;
            btnDuyetDon.Location = new Point(244, 182);
            btnDuyetDon.Margin = new Padding(3, 2, 3, 2);
            btnDuyetDon.Name = "btnDuyetDon";
            btnDuyetDon.Size = new Size(80, 30);
            btnDuyetDon.TabIndex = 9;
            btnDuyetDon.Text = "Duyệt Đơn";
            btnDuyetDon.UseVisualStyleBackColor = true;
            btnDuyetDon.Click += btnDuyetDon_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label12.ForeColor = SystemColors.ControlLight;
            label12.Location = new Point(9, 68);
            label12.Name = "label12";
            label12.Size = new Size(35, 19);
            label12.TabIndex = 15;
            label12.Text = "SĐT";
            // 
            // btnXongMon
            // 
            btnXongMon.BackgroundImage = Properties.Resources.clear_btn;
            btnXongMon.BackgroundImageLayout = ImageLayout.Stretch;
            btnXongMon.FlatAppearance.BorderSize = 0;
            btnXongMon.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXongMon.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXongMon.FlatStyle = FlatStyle.Flat;
            btnXongMon.ForeColor = SystemColors.ControlLight;
            btnXongMon.Location = new Point(244, 216);
            btnXongMon.Margin = new Padding(3, 2, 3, 2);
            btnXongMon.Name = "btnXongMon";
            btnXongMon.Size = new Size(80, 30);
            btnXongMon.TabIndex = 10;
            btnXongMon.Text = "Nấu xong";
            btnXongMon.UseVisualStyleBackColor = true;
            btnXongMon.Click += btnNauXong_Click;
            // 
            // btnHuyDon
            // 
            btnHuyDon.BackgroundImage = Properties.Resources.clear_btn;
            btnHuyDon.BackgroundImageLayout = ImageLayout.Stretch;
            btnHuyDon.FlatAppearance.BorderSize = 0;
            btnHuyDon.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnHuyDon.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnHuyDon.FlatStyle = FlatStyle.Flat;
            btnHuyDon.ForeColor = SystemColors.ControlLight;
            btnHuyDon.Location = new Point(244, 284);
            btnHuyDon.Margin = new Padding(3, 2, 3, 2);
            btnHuyDon.Name = "btnHuyDon";
            btnHuyDon.Size = new Size(80, 30);
            btnHuyDon.TabIndex = 11;
            btnHuyDon.Text = "Hủy đơn";
            btnHuyDon.UseVisualStyleBackColor = true;
            btnHuyDon.Click += btnHuyDon_Click;
            // 
            // FormQuanLyHoaDon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1299, 793);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(898, 586);
            Name = "FormQuanLyHoaDon";
            Text = "FormQuanLyHoaDon";
            Load += FormQuanLyHoaDon_Load;
            ((System.ComponentModel.ISupportInitialize)khachAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)shipperAvatar).EndInit();
            panelMain.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDonHang).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel14.ResumeLayout(false);
            panel13.ResumeLayout(false);
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChiTietDonHang).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel9.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvDonHang;
        private TextBox txtSearch;
        private ComboBox cboFilterTrangThai;
        private Button btnLamMoi;
        private TextBox txtTenKhachHang;
        private TextBox txtDiaChiGiao;
        private TextBox txtSoDienThoai;
        private PictureBox khachAvatar;
        private Button btnDuyetDon;
        private Button btnXongMon;
        private Button btnHuyDon;
        private Button btnInHoaDon;
        private Button btnGiaoViec;
        private PictureBox shipperAvatar;
        private ComboBox cboShipper;
        private TextBox txtSoDienThoaiShipper;
        public Panel panelMain;
        private Panel panel5;
        private Panel panel11;
        private Panel panel3;
        private Label label7;
        private Panel panel1;
        private Panel panel4;
        private Panel panel2;
        private Label label8;
        private Label label9;
        private Label label10;
        private Panel panel10;
        private TextBox TxtMinStock;
        private Panel panel9;
        private Panel panel8;
        private Panel panel7;
        private TextBox TxtQuantity;
        private Panel panel6;
        private Label label11;
        private Button BtnAdd;
        private Label label12;
        private Button BtnEdit;
        private Button BtnDelete;
        private Button button1;
        private Panel panel12;
        private DataGridView dgvChiTietDonHang;
        private Panel panel13;
        private Panel panel14;
    }
}
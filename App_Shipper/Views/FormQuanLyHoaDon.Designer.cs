namespace App_Shipper.Views
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
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            dgvHoaDon = new DataGridView();
            panel3 = new Panel();
            label7 = new Label();
            panel1 = new Panel();
            cboFilterTrangThai = new ComboBox();
            panel4 = new Panel();
            txtSearch = new TextBox();
            btnLamMoi = new Button();
            panel2 = new Panel();
            btnBaoSuCo = new Button();
            panel14 = new Panel();
            pbShipper = new PictureBox();
            panel13 = new Panel();
            pbKhachHang = new PictureBox();
            panel12 = new Panel();
            dgvChiTietHoaDon = new DataGridView();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            panel10 = new Panel();
            txtSoDienThoaiShipper = new TextBox();
            panel9 = new Panel();
            txtTenShipper = new TextBox();
            panel8 = new Panel();
            txtDiaChiKhachHang = new TextBox();
            panel7 = new Panel();
            txtSoDienThoaiKhachHang = new TextBox();
            panel6 = new Panel();
            txtTenKhachHang = new TextBox();
            label11 = new Label();
            label12 = new Label();
            btnDaGiao = new Button();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbShipper).BeginInit();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbKhachHang).BeginInit();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChiTietHoaDon).BeginInit();
            panel10.SuspendLayout();
            panel9.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel5);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel2);
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 25;
            // 
            // panel5
            // 
            panel5.BackgroundImage = (Image)resources.GetObject("panel5.BackgroundImage");
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
            panel11.Controls.Add(dgvHoaDon);
            panel11.Location = new Point(15, 13);
            panel11.Name = "panel11";
            panel11.Size = new Size(880, 432);
            panel11.TabIndex = 1;
            // 
            // dgvHoaDon
            // 
            dgvHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHoaDon.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvHoaDon.BorderStyle = BorderStyle.None;
            dgvHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaDon.EnableHeadersVisualStyles = false;
            dgvHoaDon.GridColor = Color.Maroon;
            dgvHoaDon.Location = new Point(10, 10);
            dgvHoaDon.Margin = new Padding(10);
            dgvHoaDon.Name = "dgvHoaDon";
            dgvHoaDon.RowHeadersVisible = false;
            dgvHoaDon.RowHeadersWidth = 51;
            dgvHoaDon.Size = new Size(860, 412);
            dgvHoaDon.TabIndex = 0;
            dgvHoaDon.CellClick += dgvHoaDon_CellClick;
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
            label7.Location = new Point(12, 25);
            label7.Name = "label7";
            label7.Size = new Size(427, 37);
            label7.TabIndex = 25;
            label7.Text = "THEO DÕI ĐƠN HÀNG CỦA BẠN";
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
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(btnBaoSuCo);
            panel2.Controls.Add(panel14);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(panel12);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(panel10);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(btnDaGiao);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // btnBaoSuCo
            // 
            btnBaoSuCo.BackgroundImage = (Image)resources.GetObject("btnBaoSuCo.BackgroundImage");
            btnBaoSuCo.BackgroundImageLayout = ImageLayout.Stretch;
            btnBaoSuCo.FlatAppearance.BorderSize = 0;
            btnBaoSuCo.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnBaoSuCo.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnBaoSuCo.FlatStyle = FlatStyle.Flat;
            btnBaoSuCo.Font = new Font("Segoe UI", 9F);
            btnBaoSuCo.ForeColor = SystemColors.ControlLight;
            btnBaoSuCo.Location = new Point(226, 162);
            btnBaoSuCo.Margin = new Padding(3, 2, 3, 2);
            btnBaoSuCo.Name = "btnBaoSuCo";
            btnBaoSuCo.Size = new Size(87, 30);
            btnBaoSuCo.TabIndex = 31;
            btnBaoSuCo.Text = "Báo sự cố";
            btnBaoSuCo.UseVisualStyleBackColor = true;
            btnBaoSuCo.Click += btnBaoSuCo_Click;
            // 
            // panel14
            // 
            panel14.BackgroundImage = (Image)resources.GetObject("panel14.BackgroundImage");
            panel14.BackgroundImageLayout = ImageLayout.Stretch;
            panel14.Controls.Add(pbShipper);
            panel14.Location = new Point(214, 369);
            panel14.Name = "panel14";
            panel14.Size = new Size(110, 110);
            panel14.TabIndex = 26;
            // 
            // pbShipper
            // 
            pbShipper.Location = new Point(12, 12);
            pbShipper.Margin = new Padding(3, 2, 3, 2);
            pbShipper.Name = "pbShipper";
            pbShipper.Size = new Size(87, 86);
            pbShipper.SizeMode = PictureBoxSizeMode.Zoom;
            pbShipper.TabIndex = 19;
            pbShipper.TabStop = false;
            // 
            // panel13
            // 
            panel13.BackgroundImage = (Image)resources.GetObject("panel13.BackgroundImage");
            panel13.BackgroundImageLayout = ImageLayout.Stretch;
            panel13.Controls.Add(pbKhachHang);
            panel13.Location = new Point(214, 13);
            panel13.Name = "panel13";
            panel13.Size = new Size(110, 110);
            panel13.TabIndex = 25;
            // 
            // pbKhachHang
            // 
            pbKhachHang.Location = new Point(12, 12);
            pbKhachHang.Margin = new Padding(3, 2, 3, 2);
            pbKhachHang.Name = "pbKhachHang";
            pbKhachHang.Size = new Size(87, 86);
            pbKhachHang.SizeMode = PictureBoxSizeMode.Zoom;
            pbKhachHang.TabIndex = 13;
            pbKhachHang.TabStop = false;
            // 
            // panel12
            // 
            panel12.BackgroundImage = (Image)resources.GetObject("panel12.BackgroundImage");
            panel12.BackgroundImageLayout = ImageLayout.Stretch;
            panel12.Controls.Add(dgvChiTietHoaDon);
            panel12.Location = new Point(16, 196);
            panel12.Name = "panel12";
            panel12.Size = new Size(308, 167);
            panel12.TabIndex = 2;
            // 
            // dgvChiTietHoaDon
            // 
            dgvChiTietHoaDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvChiTietHoaDon.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvChiTietHoaDon.BorderStyle = BorderStyle.None;
            dgvChiTietHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChiTietHoaDon.EnableHeadersVisualStyles = false;
            dgvChiTietHoaDon.GridColor = Color.Maroon;
            dgvChiTietHoaDon.Location = new Point(10, 10);
            dgvChiTietHoaDon.Margin = new Padding(10);
            dgvChiTietHoaDon.Name = "dgvChiTietHoaDon";
            dgvChiTietHoaDon.RowHeadersVisible = false;
            dgvChiTietHoaDon.RowHeadersWidth = 51;
            dgvChiTietHoaDon.Size = new Size(288, 147);
            dgvChiTietHoaDon.TabIndex = 0;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label8.ForeColor = SystemColors.ControlLight;
            label8.Location = new Point(16, 424);
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
            label9.Location = new Point(16, 369);
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
            panel10.BackgroundImage = (Image)resources.GetObject("panel10.BackgroundImage");
            panel10.Controls.Add(txtSoDienThoaiShipper);
            panel10.Location = new Point(16, 446);
            panel10.Name = "panel10";
            panel10.Size = new Size(189, 30);
            panel10.TabIndex = 27;
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
            // 
            // panel9
            // 
            panel9.BackgroundImage = (Image)resources.GetObject("panel9.BackgroundImage");
            panel9.Controls.Add(txtTenShipper);
            panel9.Location = new Point(16, 391);
            panel9.Name = "panel9";
            panel9.Size = new Size(189, 30);
            panel9.TabIndex = 26;
            // 
            // txtTenShipper
            // 
            txtTenShipper.BorderStyle = BorderStyle.None;
            txtTenShipper.Location = new Point(6, 6);
            txtTenShipper.Margin = new Padding(3, 2, 3, 2);
            txtTenShipper.Name = "txtTenShipper";
            txtTenShipper.ReadOnly = true;
            txtTenShipper.Size = new Size(177, 16);
            txtTenShipper.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(txtDiaChiKhachHang);
            panel8.Location = new Point(16, 145);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 25;
            // 
            // txtDiaChiKhachHang
            // 
            txtDiaChiKhachHang.BorderStyle = BorderStyle.None;
            txtDiaChiKhachHang.Location = new Point(6, 5);
            txtDiaChiKhachHang.Margin = new Padding(3, 2, 3, 2);
            txtDiaChiKhachHang.Name = "txtDiaChiKhachHang";
            txtDiaChiKhachHang.ReadOnly = true;
            txtDiaChiKhachHang.Size = new Size(177, 16);
            txtDiaChiKhachHang.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.Controls.Add(txtSoDienThoaiKhachHang);
            panel7.Location = new Point(16, 90);
            panel7.Name = "panel7";
            panel7.Size = new Size(189, 30);
            panel7.TabIndex = 24;
            // 
            // txtSoDienThoaiKhachHang
            // 
            txtSoDienThoaiKhachHang.BorderStyle = BorderStyle.None;
            txtSoDienThoaiKhachHang.Location = new Point(6, 5);
            txtSoDienThoaiKhachHang.Margin = new Padding(3, 2, 3, 2);
            txtSoDienThoaiKhachHang.Name = "txtSoDienThoaiKhachHang";
            txtSoDienThoaiKhachHang.ReadOnly = true;
            txtSoDienThoaiKhachHang.Size = new Size(177, 16);
            txtSoDienThoaiKhachHang.TabIndex = 3;
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
            // btnDaGiao
            // 
            btnDaGiao.BackgroundImage = (Image)resources.GetObject("btnDaGiao.BackgroundImage");
            btnDaGiao.BackgroundImageLayout = ImageLayout.Stretch;
            btnDaGiao.FlatAppearance.BorderSize = 0;
            btnDaGiao.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDaGiao.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDaGiao.FlatStyle = FlatStyle.Flat;
            btnDaGiao.ForeColor = SystemColors.ControlLight;
            btnDaGiao.Location = new Point(226, 128);
            btnDaGiao.Margin = new Padding(3, 2, 3, 2);
            btnDaGiao.Name = "btnDaGiao";
            btnDaGiao.Size = new Size(87, 30);
            btnDaGiao.TabIndex = 11;
            btnDaGiao.Text = "Đã Giao";
            btnDaGiao.UseVisualStyleBackColor = true;
            btnDaGiao.Click += btnDaGiao_Click;
            // 
            // FormQuanLyHoaDon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1259, 714);
            Controls.Add(panelMain);
            Name = "FormQuanLyHoaDon";
            Text = "FormShipperHoaDon";
            Load += FormShipperHoaDon_Load;
            panelMain.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaDon).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbShipper).EndInit();
            panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbKhachHang).EndInit();
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChiTietHoaDon).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        public Panel panelMain;
        private Panel panel5;
        private Panel panel11;
        private DataGridView dgvHoaDon;
        private Panel panel3;
        private Label label7;
        private Panel panel1;
        private ComboBox cboFilterTrangThai;
        private Panel panel4;
        private TextBox txtSearch;
        private Button btnLamMoi;
        private Panel panel2;
        private Panel panel14;
        private PictureBox pbShipper;
        private Panel panel13;
        private PictureBox pbKhachHang;
        private Panel panel12;
        private DataGridView dgvChiTietHoaDon;
        private Label label8;
        private Label label9;
        private Label label10;
        private Panel panel10;
        private TextBox txtSoDienThoaiShipper;
        private Panel panel9;
        private Panel panel8;
        private TextBox txtDiaChiKhachHang;
        private Panel panel7;
        private TextBox txtSoDienThoaiKhachHang;
        private Panel panel6;
        private TextBox txtTenKhachHang;
        private Label label11;
        private Label label12;
        private Button btnDaGiao;
        private TextBox txtTenShipper;
        private Button btnBaoSuCo;
    }
}
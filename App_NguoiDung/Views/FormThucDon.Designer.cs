namespace App_NguoiDung.Views
{
    partial class FormThucDon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThucDon));
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            dgvThucDon = new DataGridView();
            panel3 = new Panel();
            label3 = new Label();
            panel1 = new Panel();
            cboDanhMuc = new ComboBox();
            panel4 = new Panel();
            txtSearch = new TextBox();
            btnLamMoi = new Button();
            panel2 = new Panel();
            panel9 = new Panel();
            txtTongTien = new TextBox();
            label4 = new Label();
            btnDatHang = new Button();
            label2 = new Label();
            panel12 = new Panel();
            dgvGioHang = new DataGridView();
            panel8 = new Panel();
            nudSoLuong = new NumericUpDown();
            label1 = new Label();
            panel13 = new Panel();
            pbAnhMonAn = new PictureBox();
            btnXoaKhoiGio = new Button();
            panel7 = new Panel();
            txtDonGia = new TextBox();
            panel6 = new Panel();
            txtTenMonAn = new TextBox();
            label11 = new Label();
            btnThemVaoGio = new Button();
            label12 = new Label();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThucDon).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel9.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbAnhMonAn).BeginInit();
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
            panelMain.Location = new Point(17, 83);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 33;
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
            panel11.Controls.Add(dgvThucDon);
            panel11.Location = new Point(15, 13);
            panel11.Name = "panel11";
            panel11.Size = new Size(880, 432);
            panel11.TabIndex = 1;
            // 
            // dgvThucDon
            // 
            dgvThucDon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvThucDon.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvThucDon.BorderStyle = BorderStyle.None;
            dgvThucDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvThucDon.EnableHeadersVisualStyles = false;
            dgvThucDon.GridColor = Color.Maroon;
            dgvThucDon.Location = new Point(10, 10);
            dgvThucDon.Margin = new Padding(10);
            dgvThucDon.Name = "dgvThucDon";
            dgvThucDon.RowHeadersVisible = false;
            dgvThucDon.RowHeadersWidth = 51;
            dgvThucDon.Size = new Size(860, 412);
            dgvThucDon.TabIndex = 0;
            dgvThucDon.CellClick += dgvThucDon_CellClick;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.None;
            panel3.Controls.Add(label3);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label3.ForeColor = SystemColors.ControlLight;
            label3.Location = new Point(3, 25);
            label3.Name = "label3";
            label3.Size = new Size(98, 37);
            label3.TabIndex = 25;
            label3.Text = "MENU";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(cboDanhMuc);
            panel1.Location = new Point(1085, 12);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(159, 50);
            panel1.TabIndex = 23;
            // 
            // cboDanhMuc
            // 
            cboDanhMuc.BackColor = Color.White;
            cboDanhMuc.FlatStyle = FlatStyle.Flat;
            cboDanhMuc.Font = new Font("Segoe UI", 12F);
            cboDanhMuc.ForeColor = Color.Black;
            cboDanhMuc.FormattingEnabled = true;
            cboDanhMuc.Location = new Point(14, 13);
            cboDanhMuc.Margin = new Padding(3, 2, 3, 2);
            cboDanhMuc.Name = "cboDanhMuc";
            cboDanhMuc.Size = new Size(135, 29);
            cboDanhMuc.TabIndex = 8;
            cboDanhMuc.SelectedIndexChanged += cboDanhMuc_SelectedIndexChanged;
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
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(btnDatHang);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(panel12);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(btnXoaKhoiGio);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(btnThemVaoGio);
            panel2.Controls.Add(label12);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // panel9
            // 
            panel9.BackgroundImage = (Image)resources.GetObject("panel9.BackgroundImage");
            panel9.BackgroundImageLayout = ImageLayout.Stretch;
            panel9.Controls.Add(txtTongTien);
            panel9.Location = new Point(65, 451);
            panel9.Name = "panel9";
            panel9.Size = new Size(143, 30);
            panel9.TabIndex = 42;
            panel9.Paint += panel9_Paint;
            // 
            // txtTongTien
            // 
            txtTongTien.BorderStyle = BorderStyle.None;
            txtTongTien.Location = new Point(6, 5);
            txtTongTien.Margin = new Padding(3, 2, 3, 2);
            txtTongTien.Name = "txtTongTien";
            txtTongTien.ReadOnly = true;
            txtTongTien.Size = new Size(128, 16);
            txtTongTien.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label4.ForeColor = SystemColors.ControlLight;
            label4.Location = new Point(16, 453);
            label4.Name = "label4";
            label4.Size = new Size(43, 19);
            label4.TabIndex = 41;
            label4.Text = "Tổng";
            // 
            // btnDatHang
            // 
            btnDatHang.BackgroundImage = (Image)resources.GetObject("btnDatHang.BackgroundImage");
            btnDatHang.BackgroundImageLayout = ImageLayout.Stretch;
            btnDatHang.FlatAppearance.BorderSize = 0;
            btnDatHang.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDatHang.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDatHang.FlatStyle = FlatStyle.Flat;
            btnDatHang.ForeColor = SystemColors.ControlLight;
            btnDatHang.Location = new Point(214, 451);
            btnDatHang.Margin = new Padding(3, 2, 3, 2);
            btnDatHang.Name = "btnDatHang";
            btnDatHang.Size = new Size(114, 30);
            btnDatHang.TabIndex = 40;
            btnDatHang.Text = "Đặt Hàng";
            btnDatHang.UseVisualStyleBackColor = true;
            btnDatHang.Click += btnDatHang_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label2.ForeColor = SystemColors.ControlLight;
            label2.Location = new Point(11, 199);
            label2.Name = "label2";
            label2.Size = new Size(69, 19);
            label2.TabIndex = 39;
            label2.Text = "Giỏ hàng";
            // 
            // panel12
            // 
            panel12.BackgroundImage = (Image)resources.GetObject("panel12.BackgroundImage");
            panel12.BackgroundImageLayout = ImageLayout.Stretch;
            panel12.Controls.Add(dgvGioHang);
            panel12.Location = new Point(11, 221);
            panel12.Name = "panel12";
            panel12.Size = new Size(317, 224);
            panel12.TabIndex = 38;
            // 
            // dgvGioHang
            // 
            dgvGioHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGioHang.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvGioHang.BorderStyle = BorderStyle.None;
            dgvGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGioHang.EnableHeadersVisualStyles = false;
            dgvGioHang.GridColor = Color.Maroon;
            dgvGioHang.Location = new Point(10, 10);
            dgvGioHang.Margin = new Padding(10);
            dgvGioHang.Name = "dgvGioHang";
            dgvGioHang.RowHeadersVisible = false;
            dgvGioHang.RowHeadersWidth = 51;
            dgvGioHang.Size = new Size(297, 204);
            dgvGioHang.TabIndex = 0;
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(nudSoLuong);
            panel8.Location = new Point(16, 145);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 37;
            // 
            // nudSoLuong
            // 
            nudSoLuong.BorderStyle = BorderStyle.None;
            nudSoLuong.Location = new Point(11, 6);
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(172, 19);
            nudSoLuong.TabIndex = 35;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(9, 123);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 36;
            label1.Text = "Số lượng";
            // 
            // panel13
            // 
            panel13.BackgroundImage = (Image)resources.GetObject("panel13.BackgroundImage");
            panel13.BackgroundImageLayout = ImageLayout.Stretch;
            panel13.Controls.Add(pbAnhMonAn);
            panel13.Location = new Point(214, 13);
            panel13.Name = "panel13";
            panel13.Size = new Size(110, 110);
            panel13.TabIndex = 25;
            // 
            // pbAnhMonAn
            // 
            pbAnhMonAn.Location = new Point(11, 12);
            pbAnhMonAn.Margin = new Padding(3, 2, 3, 2);
            pbAnhMonAn.Name = "pbAnhMonAn";
            pbAnhMonAn.Size = new Size(89, 86);
            pbAnhMonAn.SizeMode = PictureBoxSizeMode.Zoom;
            pbAnhMonAn.TabIndex = 22;
            pbAnhMonAn.TabStop = false;
            // 
            // btnXoaKhoiGio
            // 
            btnXoaKhoiGio.BackgroundImage = (Image)resources.GetObject("btnXoaKhoiGio.BackgroundImage");
            btnXoaKhoiGio.BackgroundImageLayout = ImageLayout.Stretch;
            btnXoaKhoiGio.FlatAppearance.BorderSize = 0;
            btnXoaKhoiGio.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXoaKhoiGio.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXoaKhoiGio.FlatStyle = FlatStyle.Flat;
            btnXoaKhoiGio.ForeColor = SystemColors.ControlLight;
            btnXoaKhoiGio.Location = new Point(214, 162);
            btnXoaKhoiGio.Margin = new Padding(3, 2, 3, 2);
            btnXoaKhoiGio.Name = "btnXoaKhoiGio";
            btnXoaKhoiGio.Size = new Size(114, 30);
            btnXoaKhoiGio.TabIndex = 31;
            btnXoaKhoiGio.Text = "Xóa khỏi giỏ";
            btnXoaKhoiGio.UseVisualStyleBackColor = true;
            btnXoaKhoiGio.Click += btnXoaKhoiGio_Click;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.Controls.Add(txtDonGia);
            panel7.Location = new Point(16, 90);
            panel7.Name = "panel7";
            panel7.Size = new Size(189, 30);
            panel7.TabIndex = 24;
            // 
            // txtDonGia
            // 
            txtDonGia.BorderStyle = BorderStyle.None;
            txtDonGia.Location = new Point(6, 5);
            txtDonGia.Margin = new Padding(3, 2, 3, 2);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.ReadOnly = true;
            txtDonGia.Size = new Size(177, 16);
            txtDonGia.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtTenMonAn);
            panel6.Location = new Point(16, 35);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 23;
            // 
            // txtTenMonAn
            // 
            txtTenMonAn.BorderStyle = BorderStyle.None;
            txtTenMonAn.Location = new Point(6, 5);
            txtTenMonAn.Margin = new Padding(3, 2, 3, 2);
            txtTenMonAn.Name = "txtTenMonAn";
            txtTenMonAn.ReadOnly = true;
            txtTenMonAn.Size = new Size(177, 16);
            txtTenMonAn.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label11.ForeColor = SystemColors.ControlLight;
            label11.Location = new Point(11, 13);
            label11.Name = "label11";
            label11.Size = new Size(66, 19);
            label11.TabIndex = 14;
            label11.Text = "Tên món";
            // 
            // btnThemVaoGio
            // 
            btnThemVaoGio.BackgroundImage = (Image)resources.GetObject("btnThemVaoGio.BackgroundImage");
            btnThemVaoGio.BackgroundImageLayout = ImageLayout.Stretch;
            btnThemVaoGio.FlatAppearance.BorderSize = 0;
            btnThemVaoGio.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnThemVaoGio.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnThemVaoGio.FlatStyle = FlatStyle.Flat;
            btnThemVaoGio.ForeColor = SystemColors.ControlLight;
            btnThemVaoGio.Location = new Point(214, 128);
            btnThemVaoGio.Margin = new Padding(3, 2, 3, 2);
            btnThemVaoGio.Name = "btnThemVaoGio";
            btnThemVaoGio.Size = new Size(114, 30);
            btnThemVaoGio.TabIndex = 9;
            btnThemVaoGio.Text = "Thêm vào giỏ";
            btnThemVaoGio.UseVisualStyleBackColor = true;
            btnThemVaoGio.Click += btnThemVaoGio_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label12.ForeColor = SystemColors.ControlLight;
            label12.Location = new Point(9, 68);
            label12.Name = "label12";
            label12.Size = new Size(61, 19);
            label12.TabIndex = 15;
            label12.Text = "Đơn giá";
            // 
            // FormThucDon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 738);
            Controls.Add(panelMain);
            Name = "FormThucDon";
            Text = "FormThucDon";
            Load += FormThucDon_Load;
            panelMain.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvThucDon).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvGioHang).EndInit();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbAnhMonAn).EndInit();
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
        private DataGridView dgvThucDon;
        private Panel panel3;
        private Label label3;
        private Panel panel1;
        private ComboBox cboDanhMuc;
        private Panel panel4;
        private TextBox txtSearch;
        private Button btnLamMoi;
        private Panel panel2;
        private Panel panel13;
        private PictureBox pbAnhMonAn;
        private Button btnXoaKhoiGio;
        private Panel panel7;
        private TextBox txtDonGia;
        private Panel panel6;
        private TextBox txtTenMonAn;
        private Label label11;
        private Button btnThemVaoGio;
        private Label label12;
        private NumericUpDown nudSoLuong;
        private Panel panel8;
        private Label label1;
        private Label label2;
        private Panel panel12;
        private DataGridView dgvGioHang;
        private Button btnDatHang;
        private Panel panel9;
        private TextBox txtTongTien;
        private Label label4;
    }
}
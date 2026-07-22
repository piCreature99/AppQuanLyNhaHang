namespace App_QuanLy.Views
{
    partial class FormQuanLyTaiKhoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuanLyTaiKhoan));
            picAvatar = new PictureBox();
            CboGender = new ComboBox();
            chkPassShow = new CheckBox();
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            dgvTaiKhoan = new DataGridView();
            panel3 = new Panel();
            label8 = new Label();
            panel1 = new Panel();
            cboFilterVaiTro = new ComboBox();
            panel4 = new Panel();
            txtSearch = new TextBox();
            btnLamMoi = new Button();
            panel2 = new Panel();
            label15 = new Label();
            btnXoaAnh = new Button();
            panel15 = new Panel();
            cboVaiTro = new ComboBox();
            btnChooseImage = new Button();
            label14 = new Label();
            label10 = new Label();
            label9 = new Label();
            panel14 = new Panel();
            txtStatus = new TextBox();
            panel12 = new Panel();
            txtAddress = new TextBox();
            panel10 = new Panel();
            panel9 = new Panel();
            txtFullName = new TextBox();
            panel13 = new Panel();
            btnDelete = new Button();
            label11 = new Label();
            panel8 = new Panel();
            txtPhone = new TextBox();
            panel7 = new Panel();
            txtPassword = new TextBox();
            panel6 = new Panel();
            txtUsername = new TextBox();
            label12 = new Label();
            btnInsert = new Button();
            label13 = new Label();
            btnUpdate = new Button();
            btnXoaTaiKhoan = new Button();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel15.SuspendLayout();
            panel14.SuspendLayout();
            panel12.SuspendLayout();
            panel10.SuspendLayout();
            panel9.SuspendLayout();
            panel13.SuspendLayout();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // picAvatar
            // 
            picAvatar.Location = new Point(12, 12);
            picAvatar.Margin = new Padding(3, 2, 3, 2);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(88, 87);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 5;
            picAvatar.TabStop = false;
            // 
            // CboGender
            // 
            CboGender.FlatStyle = FlatStyle.Flat;
            CboGender.Font = new Font("Segoe UI", 7F);
            CboGender.FormattingEnabled = true;
            CboGender.Location = new Point(5, 5);
            CboGender.Margin = new Padding(3, 2, 3, 2);
            CboGender.Name = "CboGender";
            CboGender.Size = new Size(178, 20);
            CboGender.TabIndex = 12;
            CboGender.SelectedIndexChanged += CboGender_SelectedIndexChanged;
            // 
            // chkPassShow
            // 
            chkPassShow.AutoSize = true;
            chkPassShow.Location = new Point(303, 167);
            chkPassShow.Margin = new Padding(3, 2, 3, 2);
            chkPassShow.Name = "chkPassShow";
            chkPassShow.Size = new Size(15, 14);
            chkPassShow.TabIndex = 20;
            chkPassShow.UseVisualStyleBackColor = true;
            chkPassShow.CheckedChanged += chkPassShow_CheckedChanged;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel5);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel2);
            panelMain.Location = new Point(26, 168);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 25;
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
            panel11.Controls.Add(dgvTaiKhoan);
            panel11.Location = new Point(15, 13);
            panel11.Name = "panel11";
            panel11.Size = new Size(880, 432);
            panel11.TabIndex = 1;
            // 
            // dgvTaiKhoan
            // 
            dgvTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTaiKhoan.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvTaiKhoan.BorderStyle = BorderStyle.None;
            dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTaiKhoan.EnableHeadersVisualStyles = false;
            dgvTaiKhoan.GridColor = Color.Maroon;
            dgvTaiKhoan.Location = new Point(10, 10);
            dgvTaiKhoan.Margin = new Padding(10);
            dgvTaiKhoan.Name = "dgvTaiKhoan";
            dgvTaiKhoan.RowHeadersVisible = false;
            dgvTaiKhoan.RowHeadersWidth = 51;
            dgvTaiKhoan.Size = new Size(860, 412);
            dgvTaiKhoan.TabIndex = 0;
            dgvTaiKhoan.CellClick += dgvShipper_CellClick;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.None;
            panel3.Controls.Add(label8);
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
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label8.ForeColor = SystemColors.ControlLight;
            label8.Location = new Point(3, 25);
            label8.Name = "label8";
            label8.Size = new Size(288, 37);
            label8.TabIndex = 25;
            label8.Text = "QUẢN LÝ TÀI KHOẢN";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(cboFilterVaiTro);
            panel1.Location = new Point(1085, 12);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(159, 50);
            panel1.TabIndex = 23;
            // 
            // cboFilterVaiTro
            // 
            cboFilterVaiTro.BackColor = Color.White;
            cboFilterVaiTro.FlatStyle = FlatStyle.Flat;
            cboFilterVaiTro.Font = new Font("Segoe UI", 12F);
            cboFilterVaiTro.ForeColor = Color.Black;
            cboFilterVaiTro.FormattingEnabled = true;
            cboFilterVaiTro.Location = new Point(14, 13);
            cboFilterVaiTro.Margin = new Padding(3, 2, 3, 2);
            cboFilterVaiTro.Name = "cboFilterVaiTro";
            cboFilterVaiTro.Size = new Size(135, 29);
            cboFilterVaiTro.TabIndex = 8;
            cboFilterVaiTro.SelectedIndexChanged += cboFilterVaiTro_SelectedIndexChanged;
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
            panel2.Controls.Add(btnXoaTaiKhoan);
            panel2.Controls.Add(label15);
            panel2.Controls.Add(btnXoaAnh);
            panel2.Controls.Add(panel15);
            panel2.Controls.Add(btnChooseImage);
            panel2.Controls.Add(chkPassShow);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(panel14);
            panel2.Controls.Add(panel12);
            panel2.Controls.Add(panel10);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(btnDelete);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(btnInsert);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(btnUpdate);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label15.ForeColor = SystemColors.ControlLight;
            label15.Location = new Point(18, 344);
            label15.Name = "label15";
            label15.Size = new Size(55, 19);
            label15.TabIndex = 34;
            label15.Text = "Vai Trò";
            // 
            // btnXoaAnh
            // 
            btnXoaAnh.BackgroundImage = Properties.Resources.clear_btn;
            btnXoaAnh.BackgroundImageLayout = ImageLayout.Stretch;
            btnXoaAnh.FlatAppearance.BorderSize = 0;
            btnXoaAnh.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXoaAnh.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXoaAnh.FlatStyle = FlatStyle.Flat;
            btnXoaAnh.ForeColor = SystemColors.ControlLight;
            btnXoaAnh.Location = new Point(233, 89);
            btnXoaAnh.Margin = new Padding(3, 2, 3, 2);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(91, 30);
            btnXoaAnh.TabIndex = 36;
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.UseVisualStyleBackColor = true;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // panel15
            // 
            panel15.BackgroundImage = (Image)resources.GetObject("panel15.BackgroundImage");
            panel15.Controls.Add(cboVaiTro);
            panel15.Location = new Point(135, 344);
            panel15.Name = "panel15";
            panel15.Size = new Size(189, 30);
            panel15.TabIndex = 33;
            // 
            // cboVaiTro
            // 
            cboVaiTro.FlatStyle = FlatStyle.Flat;
            cboVaiTro.Font = new Font("Segoe UI", 7F);
            cboVaiTro.FormattingEnabled = true;
            cboVaiTro.Location = new Point(5, 5);
            cboVaiTro.Margin = new Padding(3, 2, 3, 2);
            cboVaiTro.Name = "cboVaiTro";
            cboVaiTro.Size = new Size(178, 20);
            cboVaiTro.TabIndex = 12;
            cboVaiTro.SelectedIndexChanged += cboVaiTro_SelectedIndexChanged;
            // 
            // btnChooseImage
            // 
            btnChooseImage.BackgroundImage = Properties.Resources.clear_btn;
            btnChooseImage.BackgroundImageLayout = ImageLayout.Stretch;
            btnChooseImage.FlatAppearance.BorderSize = 0;
            btnChooseImage.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnChooseImage.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnChooseImage.FlatStyle = FlatStyle.Flat;
            btnChooseImage.ForeColor = SystemColors.ControlLight;
            btnChooseImage.Location = new Point(132, 89);
            btnChooseImage.Margin = new Padding(3, 2, 3, 2);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(91, 30);
            btnChooseImage.TabIndex = 35;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label14.ForeColor = SystemColors.ControlLight;
            label14.Location = new Point(18, 308);
            label14.Name = "label14";
            label14.Size = new Size(76, 19);
            label14.TabIndex = 34;
            label14.Text = "Trạng thái";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label10.ForeColor = SystemColors.ControlLight;
            label10.Location = new Point(18, 272);
            label10.Name = "label10";
            label10.Size = new Size(54, 19);
            label10.TabIndex = 33;
            label10.Text = "Địa chỉ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label9.ForeColor = SystemColors.ControlLight;
            label9.Location = new Point(18, 236);
            label9.Name = "label9";
            label9.Size = new Size(65, 19);
            label9.TabIndex = 32;
            label9.Text = "Giới tính";
            // 
            // panel14
            // 
            panel14.BackgroundImage = (Image)resources.GetObject("panel14.BackgroundImage");
            panel14.Controls.Add(txtStatus);
            panel14.Location = new Point(135, 308);
            panel14.Name = "panel14";
            panel14.Size = new Size(189, 30);
            panel14.TabIndex = 27;
            // 
            // txtStatus
            // 
            txtStatus.BorderStyle = BorderStyle.None;
            txtStatus.Location = new Point(6, 5);
            txtStatus.Margin = new Padding(3, 2, 3, 2);
            txtStatus.Name = "txtStatus";
            txtStatus.ReadOnly = true;
            txtStatus.Size = new Size(177, 16);
            txtStatus.TabIndex = 4;
            // 
            // panel12
            // 
            panel12.BackgroundImage = (Image)resources.GetObject("panel12.BackgroundImage");
            panel12.Controls.Add(txtAddress);
            panel12.Location = new Point(135, 272);
            panel12.Name = "panel12";
            panel12.Size = new Size(189, 30);
            panel12.TabIndex = 27;
            // 
            // txtAddress
            // 
            txtAddress.BorderStyle = BorderStyle.None;
            txtAddress.Location = new Point(6, 5);
            txtAddress.Margin = new Padding(3, 2, 3, 2);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(177, 16);
            txtAddress.TabIndex = 4;
            // 
            // panel10
            // 
            panel10.BackgroundImage = (Image)resources.GetObject("panel10.BackgroundImage");
            panel10.Controls.Add(CboGender);
            panel10.Location = new Point(135, 236);
            panel10.Name = "panel10";
            panel10.Size = new Size(189, 30);
            panel10.TabIndex = 26;
            // 
            // panel9
            // 
            panel9.BackgroundImage = (Image)resources.GetObject("panel9.BackgroundImage");
            panel9.Controls.Add(txtFullName);
            panel9.Location = new Point(135, 20);
            panel9.Name = "panel9";
            panel9.Size = new Size(189, 30);
            panel9.TabIndex = 26;
            // 
            // txtFullName
            // 
            txtFullName.BorderStyle = BorderStyle.None;
            txtFullName.Location = new Point(6, 5);
            txtFullName.Margin = new Padding(3, 2, 3, 2);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(177, 16);
            txtFullName.TabIndex = 4;
            // 
            // panel13
            // 
            panel13.BackgroundImage = (Image)resources.GetObject("panel13.BackgroundImage");
            panel13.BackgroundImageLayout = ImageLayout.Stretch;
            panel13.Controls.Add(picAvatar);
            panel13.Location = new Point(14, 13);
            panel13.Name = "panel13";
            panel13.Size = new Size(110, 110);
            panel13.TabIndex = 25;
            // 
            // btnDelete
            // 
            btnDelete.BackgroundImage = Properties.Resources.delete_btn;
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDelete.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = SystemColors.ControlLight;
            btnDelete.Location = new Point(59, 405);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(211, 30);
            btnDelete.TabIndex = 31;
            btnDelete.Text = "Ngưng hoạt động";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label11.ForeColor = SystemColors.ControlLight;
            label11.Location = new Point(17, 200);
            label11.Name = "label11";
            label11.Size = new Size(35, 19);
            label11.TabIndex = 28;
            label11.Text = "SĐT";
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(txtPhone);
            panel8.Location = new Point(135, 200);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 25;
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.None;
            txtPhone.Location = new Point(6, 5);
            txtPhone.Margin = new Padding(3, 2, 3, 2);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(177, 16);
            txtPhone.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.BackgroundImageLayout = ImageLayout.Stretch;
            panel7.Controls.Add(txtPassword);
            panel7.Location = new Point(135, 164);
            panel7.Name = "panel7";
            panel7.Size = new Size(163, 30);
            panel7.TabIndex = 24;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(6, 5);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(150, 16);
            txtPassword.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtUsername);
            panel6.Location = new Point(135, 128);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 23;
            // 
            // txtUsername
            // 
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.Location = new Point(6, 5);
            txtUsername.Margin = new Padding(3, 2, 3, 2);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(177, 16);
            txtUsername.TabIndex = 2;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label12.ForeColor = SystemColors.ControlLight;
            label12.Location = new Point(15, 126);
            label12.Name = "label12";
            label12.Size = new Size(73, 19);
            label12.TabIndex = 14;
            label12.Text = "Tài khoản";
            // 
            // btnInsert
            // 
            btnInsert.BackgroundImage = Properties.Resources.clear_btn;
            btnInsert.BackgroundImageLayout = ImageLayout.Stretch;
            btnInsert.FlatAppearance.BorderSize = 0;
            btnInsert.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnInsert.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnInsert.FlatStyle = FlatStyle.Flat;
            btnInsert.ForeColor = SystemColors.ControlLight;
            btnInsert.Location = new Point(132, 55);
            btnInsert.Margin = new Padding(3, 2, 3, 2);
            btnInsert.Name = "btnInsert";
            btnInsert.Size = new Size(91, 30);
            btnInsert.TabIndex = 9;
            btnInsert.Text = "Thêm";
            btnInsert.UseVisualStyleBackColor = true;
            btnInsert.Click += btnInsert_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label13.ForeColor = SystemColors.ControlLight;
            label13.Location = new Point(17, 164);
            label13.Name = "label13";
            label13.Size = new Size(71, 19);
            label13.TabIndex = 15;
            label13.Text = "Mật khẩu";
            // 
            // btnUpdate
            // 
            btnUpdate.BackgroundImage = Properties.Resources.clear_btn;
            btnUpdate.BackgroundImageLayout = ImageLayout.Stretch;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnUpdate.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = SystemColors.ControlLight;
            btnUpdate.Location = new Point(233, 55);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(91, 30);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnXoaTaiKhoan
            // 
            btnXoaTaiKhoan.BackgroundImage = Properties.Resources.delete_btn;
            btnXoaTaiKhoan.BackgroundImageLayout = ImageLayout.Stretch;
            btnXoaTaiKhoan.FlatAppearance.BorderSize = 0;
            btnXoaTaiKhoan.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXoaTaiKhoan.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXoaTaiKhoan.FlatStyle = FlatStyle.Flat;
            btnXoaTaiKhoan.ForeColor = SystemColors.ControlLight;
            btnXoaTaiKhoan.Location = new Point(59, 439);
            btnXoaTaiKhoan.Margin = new Padding(3, 2, 3, 2);
            btnXoaTaiKhoan.Name = "btnXoaTaiKhoan";
            btnXoaTaiKhoan.Size = new Size(211, 30);
            btnXoaTaiKhoan.TabIndex = 37;
            btnXoaTaiKhoan.Text = "Xóa tài khoản";
            btnXoaTaiKhoan.UseVisualStyleBackColor = true;
            btnXoaTaiKhoan.Click += btnXoaTaiKhoan_Click;
            // 
            // FormQuanLyTaiKhoan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1302, 750);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(898, 586);
            Name = "FormQuanLyTaiKhoan";
            Text = "FormQuanLyTaiKhoan";
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            panelMain.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel15.ResumeLayout(false);
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel10.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel13.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtUsernam;
        private TextBox txtPassword;
        private TextBox txtPhone;
        private TextBox txtFullName;
        private TextBox txtStatus;
        private PictureBox picAvatar;
        private Button btnChooseImage;
        private Button btnInsert;
        private Button btnDelete;
        private Button btnUpdate;
        private DataGridView dgvTaiKhoan;
        private ComboBox CboGender;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtSearch;
        private CheckBox chkPassShow;
        private TextBox txtAddress;
        private Label label7;
        public Panel panelMain;
        private Panel panel5;
        private Panel panel11;
        private Panel panel3;
        private Label label8;
        private Panel panel1;
        private ComboBox cboFilterVaiTro;
        private Panel panel4;
        private Button btnLamMoi;
        private Panel panel2;
        private Panel panel13;
        private PictureBox khachAvatar;
        private Label label11;
        private Panel panel8;
        private TextBox txtDiaChiGiao;
        private Panel panel7;
        private TextBox txtSoDienThoai;
        private Panel panel6;
        private TextBox txtUsername;
        private Label label12;
        private Button btnDuyetDon;
        private Label label13;
        private Panel panel10;
        private TextBox textBox3;
        private Panel panel9;
        private Panel panel14;
        private TextBox textBox5;
        private Panel panel12;
        private TextBox textBox4;
        private Button btnXoaAnh;
        private Label label14;
        private Label label10;
        private Label label9;
        private Label label15;
        private Panel panel15;
        private ComboBox cboVaiTro;
        private Button btnXoaTaiKhoan;
    }
}
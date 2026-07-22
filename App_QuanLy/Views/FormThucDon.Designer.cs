namespace App_QuanLy.Views
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormThucDon));
            dishAvatar = new PictureBox();
            cboIngredients = new ComboBox();
            cboCat = new ComboBox();
            cmsDanhMuc = new ContextMenuStrip(components);
            themDanhMucToolStripMenuItem = new ToolStripMenuItem();
            suaDanhMucToolStripMenuItem = new ToolStripMenuItem();
            xoaDanhMucToolStripMenuItem = new ToolStripMenuItem();
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            dgvThucDon = new DataGridView();
            panel3 = new Panel();
            label3 = new Label();
            panel1 = new Panel();
            CboStatus = new ComboBox();
            panel4 = new Panel();
            TxtSearch = new TextBox();
            btnReset = new Button();
            panel2 = new Panel();
            btnActivation = new Button();
            btnXoaAnh = new Button();
            panel9 = new Panel();
            btnDeleteIngredient = new Button();
            panel13 = new Panel();
            panel12 = new Panel();
            dgvCongThuc = new DataGridView();
            btnUpdate = new Button();
            BtnDelete = new Button();
            label10 = new Label();
            panel8 = new Panel();
            panel7 = new Panel();
            nudDonGia = new NumericUpDown();
            panel6 = new Panel();
            TxtName = new TextBox();
            label11 = new Label();
            btnAddDish = new Button();
            label12 = new Label();
            btnChooseImage = new Button();
            BtnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)dishAvatar).BeginInit();
            cmsDanhMuc.SuspendLayout();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvThucDon).BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel9.SuspendLayout();
            panel13.SuspendLayout();
            panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCongThuc).BeginInit();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDonGia).BeginInit();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // dishAvatar
            // 
            dishAvatar.Location = new Point(11, 12);
            dishAvatar.Margin = new Padding(3, 2, 3, 2);
            dishAvatar.Name = "dishAvatar";
            dishAvatar.Size = new Size(89, 86);
            dishAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            dishAvatar.TabIndex = 22;
            dishAvatar.TabStop = false;
            // 
            // cboIngredients
            // 
            cboIngredients.FlatStyle = FlatStyle.Flat;
            cboIngredients.Font = new Font("Segoe UI", 8F);
            cboIngredients.FormattingEnabled = true;
            cboIngredients.Location = new Point(8, 5);
            cboIngredients.Margin = new Padding(3, 2, 3, 2);
            cboIngredients.Name = "cboIngredients";
            cboIngredients.Size = new Size(133, 21);
            cboIngredients.TabIndex = 25;
            cboIngredients.SelectedIndexChanged += cboIngredients_SelectedIndexChanged;
            // 
            // cboCat
            // 
            cboCat.ContextMenuStrip = cmsDanhMuc;
            cboCat.FlatStyle = FlatStyle.Flat;
            cboCat.Font = new Font("Segoe UI", 8F);
            cboCat.FormattingEnabled = true;
            cboCat.Location = new Point(8, 5);
            cboCat.Margin = new Padding(3, 2, 3, 2);
            cboCat.Name = "cboCat";
            cboCat.Size = new Size(177, 21);
            cboCat.TabIndex = 28;
            cboCat.SelectedIndexChanged += cboCat_SelectedIndexChanged;
            // 
            // cmsDanhMuc
            // 
            cmsDanhMuc.Items.AddRange(new ToolStripItem[] { themDanhMucToolStripMenuItem, suaDanhMucToolStripMenuItem, xoaDanhMucToolStripMenuItem });
            cmsDanhMuc.Name = "cmsDanhMuc";
            cmsDanhMuc.Size = new Size(166, 70);
            cmsDanhMuc.Opening += cmsDanhMuc_Opening;
            // 
            // themDanhMucToolStripMenuItem
            // 
            themDanhMucToolStripMenuItem.Name = "themDanhMucToolStripMenuItem";
            themDanhMucToolStripMenuItem.Size = new Size(165, 22);
            themDanhMucToolStripMenuItem.Text = "Thêm danh mục ";
            themDanhMucToolStripMenuItem.Click += themDanhMucToolStripMenuItem_Click;
            // 
            // suaDanhMucToolStripMenuItem
            // 
            suaDanhMucToolStripMenuItem.Name = "suaDanhMucToolStripMenuItem";
            suaDanhMucToolStripMenuItem.Size = new Size(165, 22);
            suaDanhMucToolStripMenuItem.Text = "Sửa danh mục ";
            suaDanhMucToolStripMenuItem.Click += suaDanhMucToolStripMenuItem_Click;
            // 
            // xoaDanhMucToolStripMenuItem
            // 
            xoaDanhMucToolStripMenuItem.Name = "xoaDanhMucToolStripMenuItem";
            xoaDanhMucToolStripMenuItem.Size = new Size(165, 22);
            xoaDanhMucToolStripMenuItem.Text = "Xóa danh mục ";
            xoaDanhMucToolStripMenuItem.Click += xoaDanhMucToolStripMenuItem_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel5);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel2);
            panelMain.Location = new Point(67, 244);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 32;
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
            panel3.Controls.Add(btnReset);
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
            label3.Location = new Point(15, 37);
            label3.Name = "label3";
            label3.Size = new Size(284, 37);
            label3.TabIndex = 25;
            label3.Text = "QUẢN LÝ THỰC ĐƠN";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(CboStatus);
            panel1.Location = new Point(1085, 12);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(159, 50);
            panel1.TabIndex = 23;
            // 
            // CboStatus
            // 
            CboStatus.BackColor = Color.White;
            CboStatus.FlatStyle = FlatStyle.Flat;
            CboStatus.Font = new Font("Segoe UI", 12F);
            CboStatus.ForeColor = Color.Black;
            CboStatus.FormattingEnabled = true;
            CboStatus.Location = new Point(14, 13);
            CboStatus.Margin = new Padding(3, 2, 3, 2);
            CboStatus.Name = "CboStatus";
            CboStatus.Size = new Size(135, 29);
            CboStatus.TabIndex = 8;
            CboStatus.SelectedIndexChanged += CboStatus_SelectedIndexChanged;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel4.BackgroundImage = (Image)resources.GetObject("panel4.BackgroundImage");
            panel4.Controls.Add(TxtSearch);
            panel4.Location = new Point(775, 12);
            panel4.Margin = new Padding(0, 0, 10, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(300, 50);
            panel4.TabIndex = 24;
            // 
            // TxtSearch
            // 
            TxtSearch.BackColor = Color.White;
            TxtSearch.BorderStyle = BorderStyle.None;
            TxtSearch.Location = new Point(52, 19);
            TxtSearch.Margin = new Padding(3, 2, 3, 2);
            TxtSearch.Name = "TxtSearch";
            TxtSearch.PlaceholderText = "Tìm kiếm...";
            TxtSearch.Size = new Size(242, 16);
            TxtSearch.TabIndex = 7;
            TxtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.Transparent;
            btnReset.BackgroundImage = (Image)resources.GetObject("btnReset.BackgroundImage");
            btnReset.FlatAppearance.BorderColor = Color.Firebrick;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnReset.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F);
            btnReset.ForeColor = SystemColors.ButtonFace;
            btnReset.Location = new Point(614, 12);
            btnReset.Margin = new Padding(0, 0, 10, 0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(151, 50);
            btnReset.TabIndex = 12;
            btnReset.Text = "Làm mới";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.left_panel;
            panel2.Controls.Add(btnActivation);
            panel2.Controls.Add(btnXoaAnh);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(btnDeleteIngredient);
            panel2.Controls.Add(panel13);
            panel2.Controls.Add(panel12);
            panel2.Controls.Add(btnUpdate);
            panel2.Controls.Add(BtnDelete);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(btnAddDish);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(btnChooseImage);
            panel2.Controls.Add(BtnAdd);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // btnActivation
            // 
            btnActivation.BackgroundImage = Properties.Resources.delete_btn;
            btnActivation.BackgroundImageLayout = ImageLayout.Stretch;
            btnActivation.FlatAppearance.BorderSize = 0;
            btnActivation.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnActivation.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnActivation.FlatStyle = FlatStyle.Flat;
            btnActivation.ForeColor = SystemColors.ControlLight;
            btnActivation.Location = new Point(181, 244);
            btnActivation.Margin = new Padding(3, 2, 3, 2);
            btnActivation.Name = "btnActivation";
            btnActivation.Size = new Size(147, 30);
            btnActivation.TabIndex = 35;
            btnActivation.Text = "Ngưng kinh doanh";
            btnActivation.UseVisualStyleBackColor = true;
            btnActivation.Click += btnActivation_Click;
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
            btnXoaAnh.Location = new Point(238, 184);
            btnXoaAnh.Margin = new Padding(3, 2, 3, 2);
            btnXoaAnh.Name = "btnXoaAnh";
            btnXoaAnh.Size = new Size(80, 30);
            btnXoaAnh.TabIndex = 34;
            btnXoaAnh.Text = "Xóa ảnh";
            btnXoaAnh.UseVisualStyleBackColor = true;
            btnXoaAnh.Click += btnXoaAnh_Click;
            // 
            // panel9
            // 
            panel9.BackgroundImage = (Image)resources.GetObject("panel9.BackgroundImage");
            panel9.BackgroundImageLayout = ImageLayout.Stretch;
            panel9.Controls.Add(cboIngredients);
            panel9.Location = new Point(181, 279);
            panel9.Name = "panel9";
            panel9.Size = new Size(147, 30);
            panel9.TabIndex = 26;
            // 
            // btnDeleteIngredient
            // 
            btnDeleteIngredient.BackgroundImage = Properties.Resources.clear_btn;
            btnDeleteIngredient.BackgroundImageLayout = ImageLayout.Stretch;
            btnDeleteIngredient.FlatAppearance.BorderSize = 0;
            btnDeleteIngredient.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDeleteIngredient.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDeleteIngredient.FlatStyle = FlatStyle.Flat;
            btnDeleteIngredient.ForeColor = SystemColors.ControlLight;
            btnDeleteIngredient.Location = new Point(95, 280);
            btnDeleteIngredient.Margin = new Padding(3, 2, 3, 2);
            btnDeleteIngredient.Name = "btnDeleteIngredient";
            btnDeleteIngredient.Size = new Size(80, 30);
            btnDeleteIngredient.TabIndex = 33;
            btnDeleteIngredient.Text = "Xóa NL";
            btnDeleteIngredient.UseVisualStyleBackColor = true;
            btnDeleteIngredient.Click += btnDeleteIngredient_Click;
            // 
            // panel13
            // 
            panel13.BackgroundImage = (Image)resources.GetObject("panel13.BackgroundImage");
            panel13.BackgroundImageLayout = ImageLayout.Stretch;
            panel13.Controls.Add(dishAvatar);
            panel13.Location = new Point(214, 32);
            panel13.Name = "panel13";
            panel13.Size = new Size(110, 110);
            panel13.TabIndex = 25;
            // 
            // panel12
            // 
            panel12.BackgroundImage = (Image)resources.GetObject("panel12.BackgroundImage");
            panel12.BackgroundImageLayout = ImageLayout.Stretch;
            panel12.Controls.Add(dgvCongThuc);
            panel12.Location = new Point(10, 315);
            panel12.Name = "panel12";
            panel12.Size = new Size(318, 167);
            panel12.TabIndex = 2;
            // 
            // dgvCongThuc
            // 
            dgvCongThuc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCongThuc.BackgroundColor = Color.FromArgb(64, 0, 0);
            dgvCongThuc.BorderStyle = BorderStyle.None;
            dgvCongThuc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCongThuc.EnableHeadersVisualStyles = false;
            dgvCongThuc.GridColor = Color.Maroon;
            dgvCongThuc.Location = new Point(10, 10);
            dgvCongThuc.Margin = new Padding(10);
            dgvCongThuc.Name = "dgvCongThuc";
            dgvCongThuc.RowHeadersVisible = false;
            dgvCongThuc.RowHeadersWidth = 51;
            dgvCongThuc.Size = new Size(298, 146);
            dgvCongThuc.TabIndex = 0;
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
            btnUpdate.Location = new Point(97, 180);
            btnUpdate.Margin = new Padding(3, 2, 3, 2);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 32;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // BtnDelete
            // 
            BtnDelete.BackgroundImage = Properties.Resources.clear_btn;
            BtnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            BtnDelete.FlatAppearance.BorderSize = 0;
            BtnDelete.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnDelete.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnDelete.FlatStyle = FlatStyle.Flat;
            BtnDelete.ForeColor = SystemColors.ControlLight;
            BtnDelete.Location = new Point(11, 214);
            BtnDelete.Margin = new Padding(3, 2, 3, 2);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(80, 30);
            BtnDelete.TabIndex = 31;
            BtnDelete.Text = "Xóa";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label10.ForeColor = SystemColors.ControlLight;
            label10.Location = new Point(9, 123);
            label10.Name = "label10";
            label10.Size = new Size(75, 19);
            label10.TabIndex = 28;
            label10.Text = "Danh mục";
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(cboCat);
            panel8.Location = new Point(16, 145);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 25;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.Controls.Add(nudDonGia);
            panel7.Location = new Point(16, 90);
            panel7.Name = "panel7";
            panel7.Size = new Size(189, 30);
            panel7.TabIndex = 24;
            // 
            // nudDonGia
            // 
            nudDonGia.BorderStyle = BorderStyle.None;
            nudDonGia.Location = new Point(8, 4);
            nudDonGia.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            nudDonGia.Name = "nudDonGia";
            nudDonGia.Size = new Size(175, 19);
            nudDonGia.TabIndex = 36;
            // 
            // panel6
            // 
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(TxtName);
            panel6.Location = new Point(16, 35);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 23;
            // 
            // TxtName
            // 
            TxtName.BorderStyle = BorderStyle.None;
            TxtName.Location = new Point(6, 5);
            TxtName.Margin = new Padding(3, 2, 3, 2);
            TxtName.Name = "TxtName";
            TxtName.Size = new Size(177, 16);
            TxtName.TabIndex = 2;
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
            // btnAddDish
            // 
            btnAddDish.BackgroundImage = Properties.Resources.clear_btn;
            btnAddDish.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddDish.FlatAppearance.BorderSize = 0;
            btnAddDish.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnAddDish.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnAddDish.FlatStyle = FlatStyle.Flat;
            btnAddDish.ForeColor = SystemColors.ControlLight;
            btnAddDish.Location = new Point(11, 180);
            btnAddDish.Margin = new Padding(3, 2, 3, 2);
            btnAddDish.Name = "btnAddDish";
            btnAddDish.Size = new Size(80, 30);
            btnAddDish.TabIndex = 9;
            btnAddDish.Text = "Thêm món";
            btnAddDish.UseVisualStyleBackColor = true;
            btnAddDish.Click += btnAddDish_Click;
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
            // btnChooseImage
            // 
            btnChooseImage.BackgroundImage = Properties.Resources.clear_btn;
            btnChooseImage.BackgroundImageLayout = ImageLayout.Stretch;
            btnChooseImage.FlatAppearance.BorderSize = 0;
            btnChooseImage.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnChooseImage.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnChooseImage.FlatStyle = FlatStyle.Flat;
            btnChooseImage.ForeColor = SystemColors.ControlLight;
            btnChooseImage.Location = new Point(238, 150);
            btnChooseImage.Margin = new Padding(3, 2, 3, 2);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(80, 30);
            btnChooseImage.TabIndex = 10;
            btnChooseImage.Text = "Thêm ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            btnChooseImage.Click += btnChooseImage_Click;
            // 
            // BtnAdd
            // 
            BtnAdd.BackgroundImage = Properties.Resources.clear_btn;
            BtnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            BtnAdd.FlatAppearance.BorderSize = 0;
            BtnAdd.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnAdd.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnAdd.FlatStyle = FlatStyle.Flat;
            BtnAdd.ForeColor = SystemColors.ControlLight;
            BtnAdd.Location = new Point(9, 280);
            BtnAdd.Margin = new Padding(3, 2, 3, 2);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(80, 30);
            BtnAdd.TabIndex = 11;
            BtnAdd.Text = "Thêm NL";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += btnAdd_Click;
            // 
            // FormThucDon
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1384, 864);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(898, 586);
            Name = "FormThucDon";
            Text = "Quản Lý Kho Nguyên Liệu";
            Load += FrmThucDon_Load;
            ((System.ComponentModel.ISupportInitialize)dishAvatar).EndInit();
            cmsDanhMuc.ResumeLayout(false);
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
            panel13.ResumeLayout(false);
            panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCongThuc).EndInit();
            panel8.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudDonGia).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvThucDon;
        private TextBox TxtName;
        private TextBox TxtSearch;
        private ComboBox CboStatus;
        private Button BtnAdd;
        private Button btnUpdate;
        private Button BtnDelete;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox dishAvatar;
        private Button btnChooseImage;
        private DataGridView dgvCongThuc;
        private ComboBox cboIngredients;
        private Button btnAddDish;
        private Button btnReset;
        private ComboBox cboCat;
        private Button btnDeleteIngredient;
        private Button btnActivation;
        public Panel panelMain;
        private Panel panel5;
        private Panel panel11;
        private DataGridView dgvDonHang;
        private Panel panel3;
        private Label label3;
        private Panel panel1;
        private Panel panel4;
        private Button BtnClear;
        private Panel panel2;
        private Panel panel14;
        private PictureBox shipperAvatar;
        private Panel panel13;
        private Panel panel12;
        private Label label8;
        private Label label9;
        private Label label10;
        private Panel panel10;
        private TextBox txtSoDienThoaiShipper;
        private Panel panel9;
        private ComboBox cboShipper;
        private Panel panel8;
        private Panel panel7;
        private TextBox txtSoDienThoai;
        private Panel panel6;
        private Label label11;
        private Label label12;
        private Button btnHuyDon;
        private Button btnXoaAnh;
        private ContextMenuStrip cmsDanhMuc;
        private ToolStripMenuItem xoaDanhMucToolStripMenuItem;
        private ToolStripMenuItem themDanhMucToolStripMenuItem;
        private ToolStripMenuItem suaDanhMucToolStripMenuItem;
        private NumericUpDown nudDonGia;
    }
}
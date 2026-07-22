namespace App_QuanLy.Views
{
    partial class FormKho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKho));
            DgvStorage = new DataGridView();
            TxtName = new TextBox();
            BtnAdd = new Button();
            BtnEdit = new Button();
            BtnDelete = new Button();
            BtnClear = new Button();
            label2 = new Label();
            label3 = new Label();
            btnActivate = new Button();
            panelMain = new Panel();
            panel5 = new Panel();
            panel11 = new Panel();
            panel3 = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            CboStatus = new ComboBox();
            panel4 = new Panel();
            TxtSearch = new TextBox();
            panel2 = new Panel();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            panel10 = new Panel();
            nudTonToiThieu = new NumericUpDown();
            panel9 = new Panel();
            nudDonGia = new NumericUpDown();
            panel8 = new Panel();
            TxtUnit = new TextBox();
            panel7 = new Panel();
            nudSoLuongTon = new NumericUpDown();
            panel6 = new Panel();
            ((System.ComponentModel.ISupportInitialize)DgvStorage).BeginInit();
            panelMain.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudTonToiThieu).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDonGia).BeginInit();
            panel8.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // DgvStorage
            // 
            DgvStorage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvStorage.BackgroundColor = Color.FromArgb(64, 0, 0);
            DgvStorage.BorderStyle = BorderStyle.None;
            DgvStorage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgvStorage.EnableHeadersVisualStyles = false;
            DgvStorage.GridColor = Color.Maroon;
            DgvStorage.Location = new Point(10, 10);
            DgvStorage.Margin = new Padding(10);
            DgvStorage.Name = "DgvStorage";
            DgvStorage.RowHeadersVisible = false;
            DgvStorage.RowHeadersWidth = 51;
            DgvStorage.Size = new Size(860, 412);
            DgvStorage.TabIndex = 0;
            DgvStorage.CellClick += DgvStorage_CellClick;
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
            // BtnAdd
            // 
            BtnAdd.BackgroundImage = Properties.Resources.clear_btn;
            BtnAdd.BackgroundImageLayout = ImageLayout.Stretch;
            BtnAdd.FlatAppearance.BorderSize = 0;
            BtnAdd.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnAdd.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnAdd.FlatStyle = FlatStyle.Flat;
            BtnAdd.ForeColor = SystemColors.ControlLight;
            BtnAdd.Location = new Point(10, 232);
            BtnAdd.Margin = new Padding(3, 2, 3, 2);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(80, 30);
            BtnAdd.TabIndex = 9;
            BtnAdd.Text = "Thêm";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // BtnEdit
            // 
            BtnEdit.BackgroundImage = Properties.Resources.clear_btn;
            BtnEdit.BackgroundImageLayout = ImageLayout.Stretch;
            BtnEdit.FlatAppearance.BorderSize = 0;
            BtnEdit.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnEdit.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnEdit.FlatStyle = FlatStyle.Flat;
            BtnEdit.ForeColor = SystemColors.ControlLight;
            BtnEdit.Location = new Point(96, 232);
            BtnEdit.Margin = new Padding(3, 2, 3, 2);
            BtnEdit.Name = "BtnEdit";
            BtnEdit.Size = new Size(80, 30);
            BtnEdit.TabIndex = 10;
            BtnEdit.Text = "Sửa";
            BtnEdit.UseVisualStyleBackColor = true;
            BtnEdit.Click += BtnEdit_Click;
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
            BtnDelete.Location = new Point(182, 232);
            BtnDelete.Margin = new Padding(3, 2, 3, 2);
            BtnDelete.Name = "BtnDelete";
            BtnDelete.Size = new Size(80, 30);
            BtnDelete.TabIndex = 11;
            BtnDelete.Text = "Xóa";
            BtnDelete.UseVisualStyleBackColor = true;
            BtnDelete.Click += BtnDelete_Click;
            // 
            // BtnClear
            // 
            BtnClear.BackColor = Color.Transparent;
            BtnClear.BackgroundImage = (Image)resources.GetObject("BtnClear.BackgroundImage");
            BtnClear.FlatAppearance.BorderColor = Color.Firebrick;
            BtnClear.FlatAppearance.BorderSize = 0;
            BtnClear.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnClear.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnClear.FlatStyle = FlatStyle.Flat;
            BtnClear.Font = new Font("Segoe UI", 10F);
            BtnClear.ForeColor = SystemColors.ButtonFace;
            BtnClear.Location = new Point(614, 12);
            BtnClear.Margin = new Padding(0, 0, 10, 0);
            BtnClear.Name = "BtnClear";
            BtnClear.Size = new Size(151, 50);
            BtnClear.TabIndex = 12;
            BtnClear.Text = "Làm mới";
            BtnClear.UseVisualStyleBackColor = false;
            BtnClear.Click += BtnClear_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label2.ForeColor = SystemColors.ControlLight;
            label2.Location = new Point(10, 49);
            label2.Name = "label2";
            label2.Size = new Size(32, 19);
            label2.TabIndex = 14;
            label2.Text = "Tên";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label3.ForeColor = SystemColors.ControlLight;
            label3.Location = new Point(10, 85);
            label3.Name = "label3";
            label3.Size = new Size(95, 19);
            label3.TabIndex = 15;
            label3.Text = "Số lượng tồn";
            // 
            // btnActivate
            // 
            btnActivate.BackgroundImage = Properties.Resources.delete_btn;
            btnActivate.BackgroundImageLayout = ImageLayout.Stretch;
            btnActivate.FlatAppearance.BorderSize = 0;
            btnActivate.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnActivate.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnActivate.FlatStyle = FlatStyle.Flat;
            btnActivate.ForeColor = SystemColors.ControlLight;
            btnActivate.Location = new Point(10, 266);
            btnActivate.Margin = new Padding(3, 2, 3, 2);
            btnActivate.Name = "btnActivate";
            btnActivate.Size = new Size(170, 30);
            btnActivate.TabIndex = 22;
            btnActivate.Text = "Ngưng Hoạt Động";
            btnActivate.UseVisualStyleBackColor = true;
            btnActivate.Click += btnActivate_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel5);
            panelMain.Controls.Add(panel3);
            panelMain.Controls.Add(panel2);
            panelMain.Location = new Point(12, 135);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 22;
            panelMain.Paint += panelMain_Paint;
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
            panel11.Controls.Add(DgvStorage);
            panel11.Location = new Point(15, 13);
            panel11.Name = "panel11";
            panel11.Size = new Size(880, 432);
            panel11.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.BackgroundImageLayout = ImageLayout.None;
            panel3.Controls.Add(label1);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(BtnClear);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(12);
            panel3.Size = new Size(1256, 74);
            panel3.TabIndex = 25;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(10, 25);
            label1.Name = "label1";
            label1.Size = new Size(286, 37);
            label1.TabIndex = 25;
            label1.Text = "QUẢN LÝ KHO HÀNG";
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
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.left_panel;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(panel10);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(panel8);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(btnActivate);
            panel2.Controls.Add(BtnAdd);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(BtnEdit);
            panel2.Controls.Add(BtnDelete);
            panel2.Location = new Point(0, 83);
            panel2.Margin = new Padding(0, 10, 0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(339, 490);
            panel2.TabIndex = 24;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label6.ForeColor = SystemColors.ControlLight;
            label6.Location = new Point(10, 193);
            label6.Name = "label6";
            label6.Size = new Size(93, 19);
            label6.TabIndex = 30;
            label6.Text = "Tồn tối thiểu";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label5.ForeColor = SystemColors.ControlLight;
            label5.Location = new Point(10, 157);
            label5.Name = "label5";
            label5.Size = new Size(61, 19);
            label5.TabIndex = 29;
            label5.Text = "Đơn giá";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label4.ForeColor = SystemColors.ControlLight;
            label4.Location = new Point(10, 121);
            label4.Name = "label4";
            label4.Size = new Size(81, 19);
            label4.TabIndex = 28;
            label4.Text = "Đơn vị tính";
            // 
            // panel10
            // 
            panel10.BackgroundImage = Properties.Resources.text_box;
            panel10.Controls.Add(nudTonToiThieu);
            panel10.Location = new Point(135, 193);
            panel10.Name = "panel10";
            panel10.Size = new Size(189, 30);
            panel10.TabIndex = 27;
            // 
            // nudTonToiThieu
            // 
            nudTonToiThieu.BorderStyle = BorderStyle.None;
            nudTonToiThieu.Location = new Point(7, 6);
            nudTonToiThieu.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudTonToiThieu.Name = "nudTonToiThieu";
            nudTonToiThieu.Size = new Size(176, 19);
            nudTonToiThieu.TabIndex = 33;
            // 
            // panel9
            // 
            panel9.BackgroundImage = Properties.Resources.text_box;
            panel9.Controls.Add(nudDonGia);
            panel9.Location = new Point(135, 157);
            panel9.Name = "panel9";
            panel9.Size = new Size(189, 30);
            panel9.TabIndex = 26;
            // 
            // nudDonGia
            // 
            nudDonGia.BorderStyle = BorderStyle.None;
            nudDonGia.Location = new Point(7, 6);
            nudDonGia.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            nudDonGia.Name = "nudDonGia";
            nudDonGia.Size = new Size(176, 19);
            nudDonGia.TabIndex = 32;
            // 
            // panel8
            // 
            panel8.BackgroundImage = (Image)resources.GetObject("panel8.BackgroundImage");
            panel8.Controls.Add(TxtUnit);
            panel8.Location = new Point(135, 121);
            panel8.Name = "panel8";
            panel8.Size = new Size(189, 30);
            panel8.TabIndex = 25;
            // 
            // TxtUnit
            // 
            TxtUnit.BorderStyle = BorderStyle.None;
            TxtUnit.Location = new Point(6, 5);
            TxtUnit.Margin = new Padding(3, 2, 3, 2);
            TxtUnit.Name = "TxtUnit";
            TxtUnit.Size = new Size(177, 16);
            TxtUnit.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.BackgroundImage = (Image)resources.GetObject("panel7.BackgroundImage");
            panel7.Controls.Add(nudSoLuongTon);
            panel7.Location = new Point(135, 85);
            panel7.Name = "panel7";
            panel7.Size = new Size(189, 30);
            panel7.TabIndex = 24;
            // 
            // nudSoLuongTon
            // 
            nudSoLuongTon.BorderStyle = BorderStyle.None;
            nudSoLuongTon.Location = new Point(7, 5);
            nudSoLuongTon.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudSoLuongTon.Name = "nudSoLuongTon";
            nudSoLuongTon.Size = new Size(176, 19);
            nudSoLuongTon.TabIndex = 31;
            nudSoLuongTon.ValueChanged += nudSoLuongTon_ValueChanged;
            // 
            // panel6
            // 
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(TxtName);
            panel6.Location = new Point(135, 49);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 23;
            // 
            // FormKho
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(898, 586);
            Name = "FormKho";
            Text = "Quản Lý Kho Nguyên Liệu";
            Load += FrmKho_Load;
            Shown += FrmKho_Shown;
            ((System.ComponentModel.ISupportInitialize)DgvStorage).EndInit();
            panelMain.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel11.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudTonToiThieu).EndInit();
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudDonGia).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView DgvStorage;
        private TextBox TxtName;
        private Button BtnAdd;
        private Button BtnEdit;
        private Button BtnDelete;
        private Button BtnClear;
        private Label label2;
        private Label label3;
        private Button btnActivate;
        public Panel panelMain;
        private ComboBox CboStatus;
        private TextBox TxtSearch;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private Label label1;
        private Panel panel5;
        private Panel panel6;
        private Panel panel10;
        private Panel panel9;
        private Panel panel8;
        private Panel panel7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Panel panel11;
        private TextBox TxtUnit;
        private NumericUpDown nudSoLuongTon;
        private NumericUpDown nudTonToiThieu;
        private NumericUpDown nudDonGia;
    }
}
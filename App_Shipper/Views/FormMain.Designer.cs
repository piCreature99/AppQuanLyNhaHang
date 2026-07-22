namespace App_Shipper.Views
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            panelMainContainer = new Panel();
            panelMain = new Panel();
            panel3 = new Panel();
            btnDangXuat = new Button();
            btnCaiDat = new Button();
            btnThongBao = new Button();
            pbTaiKhoan = new PictureBox();
            pbHoaDon = new PictureBox();
            panelMainContainer.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbTaiKhoan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbHoaDon).BeginInit();
            SuspendLayout();
            // 
            // panelMainContainer
            // 
            panelMainContainer.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMainContainer.Controls.Add(panelMain);
            panelMainContainer.Controls.Add(panel3);
            panelMainContainer.Location = new Point(0, 0);
            panelMainContainer.Margin = new Padding(0);
            panelMainContainer.Name = "panelMainContainer";
            panelMainContainer.Size = new Size(1280, 720);
            panelMainContainer.TabIndex = 4;
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Location = new Point(12, 135);
            panelMain.Margin = new Padding(0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1256, 573);
            panelMain.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(btnDangXuat);
            panel3.Controls.Add(btnCaiDat);
            panel3.Controls.Add(btnThongBao);
            panel3.Controls.Add(pbTaiKhoan);
            panel3.Controls.Add(pbHoaDon);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1280, 140);
            panel3.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            btnDangXuat.BackgroundImage = (Image)resources.GetObject("btnDangXuat.BackgroundImage");
            btnDangXuat.BackgroundImageLayout = ImageLayout.Stretch;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.Location = new Point(1214, 31);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(40, 40);
            btnDangXuat.TabIndex = 7;
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // btnCaiDat
            // 
            btnCaiDat.BackgroundImage = (Image)resources.GetObject("btnCaiDat.BackgroundImage");
            btnCaiDat.BackgroundImageLayout = ImageLayout.Stretch;
            btnCaiDat.FlatAppearance.BorderSize = 0;
            btnCaiDat.FlatStyle = FlatStyle.Flat;
            btnCaiDat.Location = new Point(1168, 31);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(40, 40);
            btnCaiDat.TabIndex = 6;
            btnCaiDat.UseVisualStyleBackColor = true;
            // 
            // btnThongBao
            // 
            btnThongBao.BackgroundImage = (Image)resources.GetObject("btnThongBao.BackgroundImage");
            btnThongBao.BackgroundImageLayout = ImageLayout.Stretch;
            btnThongBao.FlatAppearance.BorderSize = 0;
            btnThongBao.FlatStyle = FlatStyle.Flat;
            btnThongBao.Location = new Point(1122, 31);
            btnThongBao.Name = "btnThongBao";
            btnThongBao.Size = new Size(40, 40);
            btnThongBao.TabIndex = 5;
            btnThongBao.UseVisualStyleBackColor = true;
            // 
            // pbTaiKhoan
            // 
            pbTaiKhoan.BackgroundImage = (Image)resources.GetObject("pbTaiKhoan.BackgroundImage");
            pbTaiKhoan.Cursor = Cursors.Hand;
            pbTaiKhoan.Location = new Point(508, 17);
            pbTaiKhoan.Margin = new Padding(5, 0, 5, 0);
            pbTaiKhoan.Name = "pbTaiKhoan";
            pbTaiKhoan.Size = new Size(130, 110);
            pbTaiKhoan.TabIndex = 4;
            pbTaiKhoan.TabStop = false;
            pbTaiKhoan.Click += pbTaiKhoan_Click;
            // 
            // pbHoaDon
            // 
            pbHoaDon.BackgroundImage = (Image)resources.GetObject("pbHoaDon.BackgroundImage");
            pbHoaDon.Cursor = Cursors.Hand;
            pbHoaDon.Location = new Point(648, 17);
            pbHoaDon.Margin = new Padding(5, 0, 5, 0);
            pbHoaDon.Name = "pbHoaDon";
            pbHoaDon.Size = new Size(130, 110);
            pbHoaDon.TabIndex = 1;
            pbHoaDon.TabStop = false;
            pbHoaDon.Click += pbHoaDon_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContainer);
            Name = "FormMain";
            Text = "FormMain";
            panelMainContainer.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbTaiKhoan).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbHoaDon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMainContainer;
        private Panel panelMain;
        private Panel panel3;
        private Button btnDangXuat;
        private Button btnCaiDat;
        private Button btnThongBao;
        private PictureBox pbTaiKhoan;
        private PictureBox pbHoaDon;
    }
}
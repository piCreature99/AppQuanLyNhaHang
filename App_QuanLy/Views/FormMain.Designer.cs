namespace App_QuanLy.Views
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
            panelMain = new Panel();
            panel3 = new Panel();
            btnLogout = new Button();
            settingsBtn = new Button();
            notificationBtn = new Button();
            pbTaiKhoan = new PictureBox();
            pbThucDon = new PictureBox();
            pbHoaDon = new PictureBox();
            pbKho = new PictureBox();
            panelMainContainer = new Panel();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbTaiKhoan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbThucDon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbHoaDon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbKho).BeginInit();
            panelMainContainer.SuspendLayout();
            SuspendLayout();
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
            panel3.Controls.Add(btnLogout);
            panel3.Controls.Add(settingsBtn);
            panel3.Controls.Add(notificationBtn);
            panel3.Controls.Add(pbTaiKhoan);
            panel3.Controls.Add(pbThucDon);
            panel3.Controls.Add(pbHoaDon);
            panel3.Controls.Add(pbKho);
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1280, 140);
            panel3.TabIndex = 2;
            // 
            // btnLogout
            // 
            btnLogout.BackgroundImage = (Image)resources.GetObject("btnLogout.BackgroundImage");
            btnLogout.BackgroundImageLayout = ImageLayout.Stretch;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Location = new Point(1214, 31);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(40, 40);
            btnLogout.TabIndex = 7;
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // settingsBtn
            // 
            settingsBtn.BackgroundImage = (Image)resources.GetObject("settingsBtn.BackgroundImage");
            settingsBtn.BackgroundImageLayout = ImageLayout.Stretch;
            settingsBtn.FlatAppearance.BorderSize = 0;
            settingsBtn.FlatStyle = FlatStyle.Flat;
            settingsBtn.Location = new Point(1168, 31);
            settingsBtn.Name = "settingsBtn";
            settingsBtn.Size = new Size(40, 40);
            settingsBtn.TabIndex = 6;
            settingsBtn.UseVisualStyleBackColor = true;
            // 
            // notificationBtn
            // 
            notificationBtn.BackgroundImage = (Image)resources.GetObject("notificationBtn.BackgroundImage");
            notificationBtn.BackgroundImageLayout = ImageLayout.Stretch;
            notificationBtn.FlatAppearance.BorderSize = 0;
            notificationBtn.FlatStyle = FlatStyle.Flat;
            notificationBtn.Location = new Point(1122, 31);
            notificationBtn.Name = "notificationBtn";
            notificationBtn.Size = new Size(40, 40);
            notificationBtn.TabIndex = 5;
            notificationBtn.UseVisualStyleBackColor = true;
            // 
            // pbTaiKhoan
            // 
            pbTaiKhoan.BackgroundImage = (Image)resources.GetObject("pbTaiKhoan.BackgroundImage");
            pbTaiKhoan.Cursor = Cursors.Hand;
            pbTaiKhoan.Location = new Point(394, 17);
            pbTaiKhoan.Margin = new Padding(5, 0, 5, 0);
            pbTaiKhoan.Name = "pbTaiKhoan";
            pbTaiKhoan.Size = new Size(130, 110);
            pbTaiKhoan.TabIndex = 4;
            pbTaiKhoan.TabStop = false;
            pbTaiKhoan.Click += pbTaiKhoan_Click;
            // 
            // pbThucDon
            // 
            pbThucDon.BackgroundImage = (Image)resources.GetObject("pbThucDon.BackgroundImage");
            pbThucDon.Cursor = Cursors.Hand;
            pbThucDon.Location = new Point(814, 17);
            pbThucDon.Margin = new Padding(5, 0, 5, 0);
            pbThucDon.Name = "pbThucDon";
            pbThucDon.Size = new Size(130, 110);
            pbThucDon.TabIndex = 2;
            pbThucDon.TabStop = false;
            pbThucDon.Click += pbThucDon_Click;
            // 
            // pbHoaDon
            // 
            pbHoaDon.BackgroundImage = (Image)resources.GetObject("pbHoaDon.BackgroundImage");
            pbHoaDon.Cursor = Cursors.Hand;
            pbHoaDon.Location = new Point(534, 17);
            pbHoaDon.Margin = new Padding(5, 0, 5, 0);
            pbHoaDon.Name = "pbHoaDon";
            pbHoaDon.Size = new Size(130, 110);
            pbHoaDon.TabIndex = 1;
            pbHoaDon.TabStop = false;
            pbHoaDon.Click += pbHoaDon_Click;
            // 
            // pbKho
            // 
            pbKho.BackgroundImage = (Image)resources.GetObject("pbKho.BackgroundImage");
            pbKho.Cursor = Cursors.Hand;
            pbKho.Location = new Point(674, 17);
            pbKho.Margin = new Padding(5, 0, 5, 0);
            pbKho.Name = "pbKho";
            pbKho.Size = new Size(130, 110);
            pbKho.TabIndex = 0;
            pbKho.TabStop = false;
            pbKho.Click += pbKho_Click;
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
            panelMainContainer.TabIndex = 3;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimumSize = new Size(1280, 720);
            Name = "FormMain";
            Text = "FormMain";
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbTaiKhoan).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbThucDon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbHoaDon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbKho).EndInit();
            panelMainContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void PnlAspectRatioWrapper_Resize(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Panel panelMain;
        private Panel panel3;
        private PictureBox pbKho;
        private PictureBox pbTaiKhoan;
        private PictureBox pbThucDon;
        private PictureBox pbHoaDon;
        private Button notificationBtn;
        private Button btnLogout;
        private Button settingsBtn;
        private Panel panelMainContainer;
    }
}
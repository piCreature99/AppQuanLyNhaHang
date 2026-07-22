namespace App_NguoiDung.Views
{
    partial class FormQuenMatKhau
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQuenMatKhau));
            panelMain = new Panel();
            panel1 = new Panel();
            panel5 = new Panel();
            txtSDT = new TextBox();
            panel4 = new Panel();
            txtNhapLaiMatKhau = new TextBox();
            label4 = new Label();
            panel3 = new Panel();
            txtMatKhauMoi = new TextBox();
            label3 = new Label();
            btnQuayLai = new Button();
            lbMatKhauMoi = new Label();
            btnXacNhan = new Button();
            panelMain.SuspendLayout();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel1);
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1280, 720);
            panelMain.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnQuayLai);
            panel1.Controls.Add(lbMatKhauMoi);
            panel1.Controls.Add(btnXacNhan);
            panel1.Location = new Point(279, 107);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(725, 490);
            panel1.TabIndex = 27;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel5.BackgroundImage = (Image)resources.GetObject("panel5.BackgroundImage");
            panel5.Controls.Add(txtSDT);
            panel5.Location = new Point(511, 68);
            panel5.Name = "panel5";
            panel5.Padding = new Padding(3);
            panel5.Size = new Size(189, 30);
            panel5.TabIndex = 35;
            // 
            // txtSDT
            // 
            txtSDT.BorderStyle = BorderStyle.None;
            txtSDT.Location = new Point(6, 5);
            txtSDT.Margin = new Padding(3, 2, 3, 2);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(177, 16);
            txtSDT.TabIndex = 2;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel4.BackgroundImage = (Image)resources.GetObject("panel4.BackgroundImage");
            panel4.Controls.Add(txtNhapLaiMatKhau);
            panel4.Location = new Point(511, 140);
            panel4.Name = "panel4";
            panel4.Padding = new Padding(3);
            panel4.Size = new Size(189, 30);
            panel4.TabIndex = 33;
            // 
            // txtNhapLaiMatKhau
            // 
            txtNhapLaiMatKhau.BorderStyle = BorderStyle.None;
            txtNhapLaiMatKhau.Location = new Point(6, 5);
            txtNhapLaiMatKhau.Margin = new Padding(3, 2, 3, 2);
            txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            txtNhapLaiMatKhau.Size = new Size(177, 16);
            txtNhapLaiMatKhau.TabIndex = 2;
            txtNhapLaiMatKhau.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(0, 0, 0, 0);
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label4.ForeColor = SystemColors.ControlLight;
            label4.Location = new Point(370, 68);
            label4.Name = "label4";
            label4.Size = new Size(35, 19);
            label4.TabIndex = 32;
            label4.Text = "SĐT";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(txtMatKhauMoi);
            panel3.Location = new Point(511, 104);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(3);
            panel3.Size = new Size(189, 30);
            panel3.TabIndex = 31;
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.BorderStyle = BorderStyle.None;
            txtMatKhauMoi.Location = new Point(6, 5);
            txtMatKhauMoi.Margin = new Padding(3, 2, 3, 2);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(177, 16);
            txtMatKhauMoi.TabIndex = 2;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(0, 0, 0, 0);
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label3.ForeColor = SystemColors.ControlLight;
            label3.Location = new Point(370, 140);
            label3.Name = "label3";
            label3.Size = new Size(131, 19);
            label3.TabIndex = 30;
            label3.Text = "Nhập lại mật khẩu";
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnQuayLai.BackgroundImage = (Image)resources.GetObject("btnQuayLai.BackgroundImage");
            btnQuayLai.BackgroundImageLayout = ImageLayout.Stretch;
            btnQuayLai.FlatAppearance.BorderSize = 0;
            btnQuayLai.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnQuayLai.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.ForeColor = SystemColors.ControlLight;
            btnQuayLai.Location = new Point(436, 219);
            btnQuayLai.Margin = new Padding(3, 2, 3, 2);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(189, 30);
            btnQuayLai.TabIndex = 29;
            btnQuayLai.Text = "Quay lại";
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += btnQuayLai_Click;
            // 
            // lbMatKhauMoi
            // 
            lbMatKhauMoi.AutoSize = true;
            lbMatKhauMoi.BackColor = Color.FromArgb(0, 0, 0, 0);
            lbMatKhauMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            lbMatKhauMoi.ForeColor = SystemColors.ControlLight;
            lbMatKhauMoi.Location = new Point(370, 104);
            lbMatKhauMoi.Name = "lbMatKhauMoi";
            lbMatKhauMoi.Size = new Size(101, 19);
            lbMatKhauMoi.TabIndex = 27;
            lbMatKhauMoi.Text = "Mật khẩu mới";
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnXacNhan.BackgroundImage = (Image)resources.GetObject("btnXacNhan.BackgroundImage");
            btnXacNhan.BackgroundImageLayout = ImageLayout.Stretch;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXacNhan.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.ForeColor = SystemColors.ControlLight;
            btnXacNhan.Location = new Point(436, 185);
            btnXacNhan.Margin = new Padding(3, 2, 3, 2);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(189, 30);
            btnXacNhan.TabIndex = 24;
            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // FormQuenMatKhau
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1282, 722);
            Controls.Add(panelMain);
            Name = "FormQuenMatKhau";
            Text = "FormQuenMatKhau";
            Load += FormQuenMatKhau_Load;
            panelMain.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Panel panel1;
        private Panel panel5;
        private TextBox txtSDT;
        private Panel panel4;
        private TextBox txtNhapLaiMatKhau;
        private Label label4;
        private Panel panel3;
        private TextBox txtMatKhauMoi;
        private Label label3;
        private Button btnQuayLai;
        private Label lbMatKhauMoi;
        private Button btnXacNhan;
    }
}
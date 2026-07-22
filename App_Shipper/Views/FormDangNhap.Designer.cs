namespace App_Shipper.Views
{
    partial class FormDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDangNhap));
            panelMain = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            txtMatKhau = new TextBox();
            label1 = new Label();
            btnDangNhap = new Button();
            panel6 = new Panel();
            txtTaiKhoan = new TextBox();
            label2 = new Label();
            panelMain.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMain.Controls.Add(panel1);
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1280, 720);
            panelMain.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnDangNhap);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(279, 107);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(725, 490);
            panel1.TabIndex = 27;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(txtMatKhau);
            panel2.Location = new Point(481, 121);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3);
            panel2.Size = new Size(189, 30);
            panel2.TabIndex = 28;
            // 
            // txtMatKhau
            // 
            txtMatKhau.BorderStyle = BorderStyle.None;
            txtMatKhau.Location = new Point(6, 5);
            txtMatKhau.Margin = new Padding(3, 2, 3, 2);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(177, 16);
            txtMatKhau.TabIndex = 2;
            txtMatKhau.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(0, 0, 0, 0);
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label1.ForeColor = SystemColors.ControlLight;
            label1.Location = new Point(402, 121);
            label1.Name = "label1";
            label1.Size = new Size(71, 19);
            label1.TabIndex = 27;
            label1.Text = "Mật khẩu";
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnDangNhap.BackgroundImage = (Image)resources.GetObject("btnDangNhap.BackgroundImage");
            btnDangNhap.BackgroundImageLayout = ImageLayout.Stretch;
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnDangNhap.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.ForeColor = SystemColors.ControlLight;
            btnDangNhap.Location = new Point(444, 185);
            btnDangNhap.Margin = new Padding(3, 2, 3, 2);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(189, 43);
            btnDangNhap.TabIndex = 24;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtTaiKhoan);
            panel6.Location = new Point(481, 85);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 26;
            // 
            // txtTaiKhoan
            // 
            txtTaiKhoan.BorderStyle = BorderStyle.None;
            txtTaiKhoan.Location = new Point(6, 5);
            txtTaiKhoan.Margin = new Padding(3, 2, 3, 2);
            txtTaiKhoan.Name = "txtTaiKhoan";
            txtTaiKhoan.Size = new Size(177, 16);
            txtTaiKhoan.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(0, 0, 0, 0);
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label2.ForeColor = SystemColors.ControlLight;
            label2.Location = new Point(402, 85);
            label2.Name = "label2";
            label2.Size = new Size(73, 19);
            label2.TabIndex = 25;
            label2.Text = "Tài khoản";
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 721);
            Controls.Add(panelMain);
            Name = "FormDangNhap";
            Text = "FormDangNhap";
            panelMain.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Panel panel1;
        private Panel panel2;
        private TextBox txtMatKhau;
        private Label label1;
        private Button btnDangNhap;
        private Panel panel6;
        private TextBox txtTaiKhoan;
        private Label label2;
    }
}
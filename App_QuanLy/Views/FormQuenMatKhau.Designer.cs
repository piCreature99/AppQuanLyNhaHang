namespace App_QuanLy.Views
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
            btnReturn = new Button();
            panel3 = new Panel();
            txtNhapLaiMatKhau = new TextBox();
            label6 = new Label();
            panel2 = new Panel();
            txtMatKhau = new TextBox();
            label4 = new Label();
            btnXacNhan = new Button();
            panel6 = new Panel();
            txtSDT = new TextBox();
            label5 = new Label();
            panelMain.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
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
            panelMain.TabIndex = 7;
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.manager_forgot_password;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(btnReturn);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(btnXacNhan);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(label5);
            panel1.Location = new Point(279, 107);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(725, 490);
            panel1.TabIndex = 27;
            // 
            // btnReturn
            // 
            btnReturn.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnReturn.BackgroundImage = Properties.Resources.delete_btn;
            btnReturn.BackgroundImageLayout = ImageLayout.Stretch;
            btnReturn.FlatAppearance.BorderSize = 0;
            btnReturn.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnReturn.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.ForeColor = SystemColors.ControlLight;
            btnReturn.Location = new Point(438, 239);
            btnReturn.Margin = new Padding(3, 2, 3, 2);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(189, 30);
            btnReturn.TabIndex = 31;
            btnReturn.Text = "Quay lại";
            btnReturn.UseVisualStyleBackColor = false;
            btnReturn.Click += btnReturn_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel3.BackgroundImage = (Image)resources.GetObject("panel3.BackgroundImage");
            panel3.Controls.Add(txtNhapLaiMatKhau);
            panel3.Location = new Point(508, 157);
            panel3.Name = "panel3";
            panel3.Padding = new Padding(3);
            panel3.Size = new Size(189, 30);
            panel3.TabIndex = 30;
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
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(0, 0, 0, 0);
            label6.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label6.ForeColor = SystemColors.ControlLight;
            label6.Location = new Point(366, 157);
            label6.Name = "label6";
            label6.Size = new Size(131, 19);
            label6.TabIndex = 29;
            label6.Text = "Nhập lại mật khẩu";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(txtMatKhau);
            panel2.Location = new Point(508, 121);
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
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(0, 0, 0, 0);
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label4.ForeColor = SystemColors.ControlLight;
            label4.Location = new Point(366, 121);
            label4.Name = "label4";
            label4.Size = new Size(101, 19);
            label4.TabIndex = 27;
            label4.Text = "Mật khẩu mới";
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnXacNhan.BackgroundImage = Properties.Resources.delete_btn;
            btnXacNhan.BackgroundImageLayout = ImageLayout.Stretch;
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnXacNhan.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.ForeColor = SystemColors.ControlLight;
            btnXacNhan.Location = new Point(438, 205);
            btnXacNhan.Margin = new Padding(3, 2, 3, 2);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(189, 30);
            btnXacNhan.TabIndex = 24;
            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtSDT);
            panel6.Location = new Point(508, 85);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 26;
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
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.FromArgb(0, 0, 0, 0);
            label5.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline);
            label5.ForeColor = SystemColors.ControlLight;
            label5.Location = new Point(366, 85);
            label5.Name = "label5";
            label5.Size = new Size(35, 19);
            label5.TabIndex = 25;
            label5.Text = "SĐT";
            // 
            // FormQuenMatKhau
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 721);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            MinimumSize = new Size(898, 586);
            Name = "FormQuenMatKhau";
            Text = "Lấy lại mật khẩu";
            panelMain.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtSDT;
        private TextBox txtMatKhau;
        private TextBox txtNhapLaiMatKhau;
        private Button btnXacNhan;
        private Panel panelMain;
        private Panel panel1;
        private Panel panel3;
        private Label label6;
        private Panel panel2;
        private Label label4;
        private Panel panel6;
        private Label label5;
        private Button btnReturn;
    }
}
namespace App_QuanLy.Views
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
            btnForgotPassword = new Button();
            panel2 = new Panel();
            txtPassword = new TextBox();
            label1 = new Label();
            BtnLogin = new Button();
            panel6 = new Panel();
            txtUsername = new TextBox();
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
            panelMain.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.manager_login1;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(btnForgotPassword);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(BtnLogin);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(279, 107);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(725, 490);
            panel1.TabIndex = 27;
            // 
            // btnForgotPassword
            // 
            btnForgotPassword.BackColor = Color.FromArgb(0, 0, 0, 0);
            btnForgotPassword.BackgroundImage = Properties.Resources.delete_btn;
            btnForgotPassword.BackgroundImageLayout = ImageLayout.Stretch;
            btnForgotPassword.FlatAppearance.BorderSize = 0;
            btnForgotPassword.FlatAppearance.MouseDownBackColor = Color.Crimson;
            btnForgotPassword.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            btnForgotPassword.FlatStyle = FlatStyle.Flat;
            btnForgotPassword.ForeColor = SystemColors.ControlLight;
            btnForgotPassword.Location = new Point(481, 190);
            btnForgotPassword.Margin = new Padding(3, 2, 3, 2);
            btnForgotPassword.Name = "btnForgotPassword";
            btnForgotPassword.Size = new Size(189, 30);
            btnForgotPassword.TabIndex = 29;
            btnForgotPassword.Text = "Quên mật khẩu?";
            btnForgotPassword.UseVisualStyleBackColor = false;
            btnForgotPassword.Click += btnForgotPassword_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.Controls.Add(txtPassword);
            panel2.Location = new Point(481, 121);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(3);
            panel2.Size = new Size(189, 30);
            panel2.TabIndex = 28;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.Location = new Point(6, 5);
            txtPassword.Margin = new Padding(3, 2, 3, 2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(177, 16);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
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
            // BtnLogin
            // 
            BtnLogin.BackColor = Color.FromArgb(0, 0, 0, 0);
            BtnLogin.BackgroundImage = Properties.Resources.delete_btn;
            BtnLogin.BackgroundImageLayout = ImageLayout.Stretch;
            BtnLogin.FlatAppearance.BorderSize = 0;
            BtnLogin.FlatAppearance.MouseDownBackColor = Color.Crimson;
            BtnLogin.FlatAppearance.MouseOverBackColor = Color.IndianRed;
            BtnLogin.FlatStyle = FlatStyle.Flat;
            BtnLogin.ForeColor = SystemColors.ControlLight;
            BtnLogin.Location = new Point(481, 156);
            BtnLogin.Margin = new Padding(3, 2, 3, 2);
            BtnLogin.Name = "BtnLogin";
            BtnLogin.Size = new Size(189, 30);
            BtnLogin.TabIndex = 24;
            BtnLogin.Text = "Đăng nhập";
            BtnLogin.UseVisualStyleBackColor = false;
            BtnLogin.Click += BtnLogin_Click;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(0, 0, 0, 0);
            panel6.BackgroundImage = (Image)resources.GetObject("panel6.BackgroundImage");
            panel6.Controls.Add(txtUsername);
            panel6.Location = new Point(481, 85);
            panel6.Name = "panel6";
            panel6.Padding = new Padding(3);
            panel6.Size = new Size(189, 30);
            panel6.TabIndex = 26;
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
            label2.Click += label2_Click;
            // 
            // FormDangNhap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1281, 721);
            Controls.Add(panelMain);
            Margin = new Padding(3, 2, 3, 2);
            Name = "FormDangNhap";
            Text = "Đăng nhập";
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

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button BtnLogin;
        private Label Username;
        private Label Password;
        private Panel panelMain;
        private Panel panel6;
        private TextBox TxtName;
        private Label label2;
        private Button BtnAdd;
        private Panel panel1;
        private Button btnForgotPassword;
        private Panel panel2;
        private Label label1;
    }
}
namespace App_NguoiDung.Views
{
    partial class FormMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMaster));
            panelMasterContainer = new Panel();
            SuspendLayout();
            // 
            // panelMasterContainer
            // 
            panelMasterContainer.BackColor = Color.FromArgb(0, 0, 0, 0);
            panelMasterContainer.Location = new Point(0, 0);
            panelMasterContainer.Name = "panelMasterContainer";
            panelMasterContainer.Size = new Size(1280, 720);
            panelMasterContainer.TabIndex = 1;
            // 
            // FormMaster
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMasterContainer);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMaster";
            Text = "FormMaster";
            FormClosed += FormMaster_Closed;
            Load += FormMaster_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMasterContainer;
    }
}
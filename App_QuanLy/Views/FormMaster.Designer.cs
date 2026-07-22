namespace App_QuanLy.Views
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
            panelMasterContainer = new Panel();
            SuspendLayout();
            // 
            // panelMasterContainer
            // 
            panelMasterContainer.BackgroundImage = Properties.Resources.BGWineBlur;
            panelMasterContainer.Location = new Point(0, 0);
            panelMasterContainer.Name = "panelMasterContainer";
            panelMasterContainer.Size = new Size(1280, 720);
            panelMasterContainer.TabIndex = 0;
            // 
            // FormMaster
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMasterContainer);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMaster";
            Text = "FormMaster";
            Load += FormMaster_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMasterContainer;
    }
}
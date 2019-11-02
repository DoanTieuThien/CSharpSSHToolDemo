namespace SSHTool
{
    partial class FormSSHMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSSHMain));
            this.txtSSHCommandLine = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtSSHCommandLine
            // 
            this.txtSSHCommandLine.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtSSHCommandLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSSHCommandLine.ForeColor = System.Drawing.SystemColors.Window;
            this.txtSSHCommandLine.Location = new System.Drawing.Point(0, 0);
            this.txtSSHCommandLine.Multiline = true;
            this.txtSSHCommandLine.Name = "txtSSHCommandLine";
            this.txtSSHCommandLine.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSSHCommandLine.Size = new System.Drawing.Size(876, 448);
            this.txtSSHCommandLine.TabIndex = 0;
            this.txtSSHCommandLine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSSHCommandLine_KeyDown);
            this.txtSSHCommandLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSSHCommandLine_KeyPress);
            // 
            // FormSSHMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 448);
            this.Controls.Add(this.txtSSHCommandLine);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSSHMain";
            this.Text = "SSH TOOL IS DEMOED BY TUANNX2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSSHMain_FormClosing);
            this.Load += new System.EventHandler(this.FormSSHMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSSHCommandLine;
    }
}


namespace FeedBuilder
{
    partial class ValidationForm
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
            this.mValidationBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // mValidationBrowser
            // 
            this.mValidationBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mValidationBrowser.Location = new System.Drawing.Point(0, 0);
            this.mValidationBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.mValidationBrowser.Name = "mValidationBrowser";
            this.mValidationBrowser.Size = new System.Drawing.Size(811, 561);
            this.mValidationBrowser.TabIndex = 0;
            // 
            // ValidationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 561);
            this.Controls.Add(this.mValidationBrowser);
            this.Name = "ValidationForm";
            this.Text = "Validation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser mValidationBrowser;
    }
}
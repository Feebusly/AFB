namespace FeedBuilder
{
    partial class FTPConnectForm
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
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.mUsername = new System.Windows.Forms.TextBox();
            this.mPassword = new System.Windows.Forms.TextBox();
            this.mRemotePath = new System.Windows.Forms.TextBox();
            this.mConnect = new System.Windows.Forms.Button();
            this.mCancel = new System.Windows.Forms.Button();
            this.mServer = new System.Windows.Forms.ComboBox();
            this.mSaveButton = new System.Windows.Forms.Button();
            this.mDeleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 41);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 31;
            this.label18.Text = "Username";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(19, 67);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 30;
            this.label19.Text = "Password";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(3, 93);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 13);
            this.label20.TabIndex = 29;
            this.label20.Text = "Remote Path";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(34, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 28;
            this.label24.Text = "Server";
            // 
            // mUsername
            // 
            this.mUsername.Location = new System.Drawing.Point(78, 38);
            this.mUsername.Name = "mUsername";
            this.mUsername.Size = new System.Drawing.Size(237, 20);
            this.mUsername.TabIndex = 2;
            // 
            // mPassword
            // 
            this.mPassword.Location = new System.Drawing.Point(78, 64);
            this.mPassword.Name = "mPassword";
            this.mPassword.PasswordChar = '*';
            this.mPassword.Size = new System.Drawing.Size(237, 20);
            this.mPassword.TabIndex = 3;
            // 
            // mRemotePath
            // 
            this.mRemotePath.Location = new System.Drawing.Point(78, 90);
            this.mRemotePath.Name = "mRemotePath";
            this.mRemotePath.Size = new System.Drawing.Size(237, 20);
            this.mRemotePath.TabIndex = 4;
            // 
            // mConnect
            // 
            this.mConnect.Location = new System.Drawing.Point(321, 10);
            this.mConnect.Name = "mConnect";
            this.mConnect.Size = new System.Drawing.Size(61, 23);
            this.mConnect.TabIndex = 7;
            this.mConnect.Text = "Connect";
            this.mConnect.UseVisualStyleBackColor = true;
            this.mConnect.Click += new System.EventHandler(this.mConnect_Click);
            // 
            // mCancel
            // 
            this.mCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.mCancel.Location = new System.Drawing.Point(321, 36);
            this.mCancel.Name = "mCancel";
            this.mCancel.Size = new System.Drawing.Size(61, 23);
            this.mCancel.TabIndex = 8;
            this.mCancel.Text = "Cancel";
            this.mCancel.UseVisualStyleBackColor = true;
            this.mCancel.Click += new System.EventHandler(this.mCancel_Click);
            // 
            // mServer
            // 
            this.mServer.FormattingEnabled = true;
            this.mServer.Location = new System.Drawing.Point(78, 11);
            this.mServer.Name = "mServer";
            this.mServer.Size = new System.Drawing.Size(237, 21);
            this.mServer.TabIndex = 1;
            this.mServer.SelectedIndexChanged += new System.EventHandler(this.mServer_SelectedIndexChanged);
            // 
            // mSaveButton
            // 
            this.mSaveButton.Location = new System.Drawing.Point(78, 116);
            this.mSaveButton.Name = "mSaveButton";
            this.mSaveButton.Size = new System.Drawing.Size(49, 23);
            this.mSaveButton.TabIndex = 5;
            this.mSaveButton.Text = "Save";
            this.mSaveButton.UseVisualStyleBackColor = true;
            this.mSaveButton.Click += new System.EventHandler(this.mSaveButton_Click);
            // 
            // mDeleteButton
            // 
            this.mDeleteButton.Location = new System.Drawing.Point(133, 116);
            this.mDeleteButton.Name = "mDeleteButton";
            this.mDeleteButton.Size = new System.Drawing.Size(49, 23);
            this.mDeleteButton.TabIndex = 6;
            this.mDeleteButton.Text = "Delete";
            this.mDeleteButton.UseVisualStyleBackColor = true;
            this.mDeleteButton.Click += new System.EventHandler(this.mDeleteButton_Click);
            // 
            // FTPConnectForm
            // 
            this.AcceptButton = this.mConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.mCancel;
            this.ClientSize = new System.Drawing.Size(432, 154);
            this.Controls.Add(this.mDeleteButton);
            this.Controls.Add(this.mSaveButton);
            this.Controls.Add(this.mServer);
            this.Controls.Add(this.mCancel);
            this.Controls.Add(this.mConnect);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.mUsername);
            this.Controls.Add(this.mPassword);
            this.Controls.Add(this.mRemotePath);
            this.Name = "FTPConnectForm";
            this.Text = "FTPConnectForm";
            this.Load += new System.EventHandler(this.FTPConnectForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox mUsername;
        private System.Windows.Forms.TextBox mPassword;
        private System.Windows.Forms.TextBox mRemotePath;
        private System.Windows.Forms.Button mConnect;
        private System.Windows.Forms.Button mCancel;
        private System.Windows.Forms.ComboBox mServer;
        private System.Windows.Forms.Button mSaveButton;
        private System.Windows.Forms.Button mDeleteButton;
    }
}
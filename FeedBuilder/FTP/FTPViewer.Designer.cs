namespace FeedBuilder
{
    partial class FTPViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mFTPWindow = new System.Windows.Forms.DataGridView();
            this.ImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.mNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mSizeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mServerLabel = new System.Windows.Forms.Label();
            this.mRemotePathLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.mFTPWindow)).BeginInit();
            this.mContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mFTPWindow
            // 
            this.mFTPWindow.AllowUserToAddRows = false;
            this.mFTPWindow.AllowUserToDeleteRows = false;
            this.mFTPWindow.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.mFTPWindow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mFTPWindow.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.mFTPWindow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mFTPWindow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageColumn,
            this.mNameColumn,
            this.mSizeColumn});
            this.mFTPWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFTPWindow.Location = new System.Drawing.Point(0, 0);
            this.mFTPWindow.Name = "mFTPWindow";
            this.mFTPWindow.ReadOnly = true;
            this.mFTPWindow.RowHeadersWidth = 4;
            this.mFTPWindow.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.mFTPWindow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mFTPWindow.Size = new System.Drawing.Size(348, 309);
            this.mFTPWindow.TabIndex = 29;
            this.mFTPWindow.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mFTPWindow_CellDoubleClick);
            this.mFTPWindow.SelectionChanged += new System.EventHandler(this.mFTPWindow_SelectionChanged);
            this.mFTPWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mFTPWindow_MouseClick);
            // 
            // ImageColumn
            // 
            this.ImageColumn.HeaderText = "";
            this.ImageColumn.Name = "ImageColumn";
            this.ImageColumn.ReadOnly = true;
            this.ImageColumn.Width = 20;
            // 
            // mNameColumn
            // 
            this.mNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mNameColumn.FillWeight = 70F;
            this.mNameColumn.HeaderText = "Name";
            this.mNameColumn.Name = "mNameColumn";
            this.mNameColumn.ReadOnly = true;
            // 
            // mSizeColumn
            // 
            this.mSizeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mSizeColumn.FillWeight = 30F;
            this.mSizeColumn.HeaderText = "Size";
            this.mSizeColumn.Name = "mSizeColumn";
            this.mSizeColumn.ReadOnly = true;
            this.mSizeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mSizeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // mContextMenuStrip
            // 
            this.mContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.mContextMenuStrip.Name = "mContextMenuStrip";
            this.mContextMenuStrip.Size = new System.Drawing.Size(129, 48);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // mServerLabel
            // 
            this.mServerLabel.AutoSize = true;
            this.mServerLabel.Location = new System.Drawing.Point(3, 2);
            this.mServerLabel.Name = "mServerLabel";
            this.mServerLabel.Size = new System.Drawing.Size(0, 13);
            this.mServerLabel.TabIndex = 30;
            // 
            // mRemotePathLabel
            // 
            this.mRemotePathLabel.AutoSize = true;
            this.mRemotePathLabel.Location = new System.Drawing.Point(3, 18);
            this.mRemotePathLabel.Name = "mRemotePathLabel";
            this.mRemotePathLabel.Size = new System.Drawing.Size(68, 13);
            this.mRemotePathLabel.TabIndex = 31;
            this.mRemotePathLabel.Text = "Remote Files";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mRemotePathLabel);
            this.splitContainer1.Panel1.Controls.Add(this.mServerLabel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mFTPWindow);
            this.splitContainer1.Size = new System.Drawing.Size(348, 343);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.TabIndex = 32;
            // 
            // FTPViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "FTPViewer";
            this.Size = new System.Drawing.Size(348, 343);
            ((System.ComponentModel.ISupportInitialize)(this.mFTPWindow)).EndInit();
            this.mContextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mFTPWindow;
        private System.Windows.Forms.DataGridViewImageColumn ImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mSizeColumn;
        private System.Windows.Forms.ContextMenuStrip mContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.Label mServerLabel;
        private System.Windows.Forms.Label mRemotePathLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

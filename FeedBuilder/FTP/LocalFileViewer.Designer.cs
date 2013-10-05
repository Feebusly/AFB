namespace FeedBuilder
{
    partial class LocalFileViewer
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
            Aga.Controls.Tree.TreeColumn treeColumn1 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn2 = new Aga.Controls.Tree.TreeColumn();
            this.mFeedFilesOrExplorer = new System.Windows.Forms.ComboBox();
            this.mNonItunesCheckBox = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mFeedUploadParts = new Aga.Controls.Tree.TreeViewAdv();
            this.nodeCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.nodeIcon1 = new Aga.Controls.Tree.NodeControls.NodeIcon();
            this.mContextMenuLocalFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mExplorer = new FeedBuilder.FTP.FolderBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mFeedFilesOrExplorer
            // 
            this.mFeedFilesOrExplorer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mFeedFilesOrExplorer.FormattingEnabled = true;
            this.mFeedFilesOrExplorer.Items.AddRange(new object[] {
            "Local Feed File",
            "Explorer"});
            this.mFeedFilesOrExplorer.Location = new System.Drawing.Point(3, 0);
            this.mFeedFilesOrExplorer.Name = "mFeedFilesOrExplorer";
            this.mFeedFilesOrExplorer.Size = new System.Drawing.Size(96, 21);
            this.mFeedFilesOrExplorer.TabIndex = 49;
            this.mFeedFilesOrExplorer.SelectedIndexChanged += new System.EventHandler(this.mFeedFilesOrExplorer_SelectedIndexChanged);
            // 
            // mNonItunesCheckBox
            // 
            this.mNonItunesCheckBox.AutoSize = true;
            this.mNonItunesCheckBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.mNonItunesCheckBox.Location = new System.Drawing.Point(231, 0);
            this.mNonItunesCheckBox.Name = "mNonItunesCheckBox";
            this.mNonItunesCheckBox.Size = new System.Drawing.Size(167, 25);
            this.mNonItunesCheckBox.TabIndex = 50;
            this.mNonItunesCheckBox.Text = "Create Non iTunes for Upload";
            this.mNonItunesCheckBox.UseVisualStyleBackColor = true;
            this.mNonItunesCheckBox.CheckedChanged += new System.EventHandler(this.mNonItunesCheckBox_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mFeedFilesOrExplorer);
            this.splitContainer1.Panel1.Controls.Add(this.mNonItunesCheckBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mExplorer);
            this.splitContainer1.Panel2.Controls.Add(this.mFeedUploadParts);
            this.splitContainer1.Size = new System.Drawing.Size(398, 427);
            this.splitContainer1.SplitterDistance = 25;
            this.splitContainer1.TabIndex = 52;
            // 
            // mFeedUploadParts
            // 
            this.mFeedUploadParts.BackColor = System.Drawing.SystemColors.Window;
            treeColumn1.Header = "";
            treeColumn2.Header = "";
            this.mFeedUploadParts.Columns.Add(treeColumn1);
            this.mFeedUploadParts.Columns.Add(treeColumn2);
            this.mFeedUploadParts.Cursor = System.Windows.Forms.Cursors.Default;
            this.mFeedUploadParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFeedUploadParts.DragDropMarkColor = System.Drawing.Color.Black;
            this.mFeedUploadParts.LineColor = System.Drawing.SystemColors.ControlDark;
            this.mFeedUploadParts.Location = new System.Drawing.Point(0, 0);
            this.mFeedUploadParts.Model = null;
            this.mFeedUploadParts.Name = "mFeedUploadParts";
            this.mFeedUploadParts.NodeControls.Add(this.nodeCheckBox);
            this.mFeedUploadParts.NodeControls.Add(this.nodeTextBox);
            this.mFeedUploadParts.NodeControls.Add(this.nodeIcon1);
            this.mFeedUploadParts.SelectedNode = null;
            this.mFeedUploadParts.Size = new System.Drawing.Size(398, 398);
            this.mFeedUploadParts.TabIndex = 44;
            this.mFeedUploadParts.DoubleClick += new System.EventHandler(this.mFeedUploadParts_DoubleClick);
            this.mFeedUploadParts.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mFeedUploadParts_MouseClick);
            this.mFeedUploadParts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mFeedUploadParts_MouseUp);
            // 
            // nodeCheckBox
            // 
            this.nodeCheckBox.DataPropertyName = "CheckState";
            this.nodeCheckBox.ReflectState = true;
            this.nodeCheckBox.ThreeState = true;
            // 
            // nodeTextBox
            // 
            this.nodeTextBox.DataPropertyName = "Text";
            // 
            // nodeIcon1
            // 
            this.nodeIcon1.DataPropertyName = "foo";
            // 
            // mContextMenuLocalFiles
            // 
            this.mContextMenuLocalFiles.Name = "mContextMenuLocalFiles";
            this.mContextMenuLocalFiles.Size = new System.Drawing.Size(61, 4);
            // 
            // mExplorer
            // 
            this.mExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mExplorer.Location = new System.Drawing.Point(0, 0);
            this.mExplorer.Name = "mExplorer";
            this.mExplorer.Size = new System.Drawing.Size(398, 398);
            this.mExplorer.TabIndex = 45;
            // 
            // LocalFileViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "LocalFileViewer";
            this.Size = new System.Drawing.Size(398, 427);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox mFeedFilesOrExplorer;
        private System.Windows.Forms.CheckBox mNonItunesCheckBox;
        private Aga.Controls.Tree.TreeViewAdv mFeedUploadParts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip mContextMenuLocalFiles;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox;
        private Aga.Controls.Tree.NodeControls.NodeIcon nodeIcon1;
        private FTP.FolderBrowser mExplorer;

    }
}

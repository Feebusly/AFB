namespace FeedBuilder
{
    partial class FeedForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedForm));
            this.mFTPUploadWorker = new System.ComponentModel.BackgroundWorker();
            this.mToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mRefreshImage = new System.Windows.Forms.Button();
            this.mAboutImage = new System.Windows.Forms.Button();
            this.mCloseImage = new System.Windows.Forms.Button();
            this.mOpenImage = new System.Windows.Forms.Button();
            this.mErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.nodeCheckBox = new Aga.Controls.Tree.NodeControls.NodeCheckBox();
            this.nodeTextBox = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.mItunesPodcast = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.mFileList = new System.Windows.Forms.ListBox();
            this.splitContainer9 = new System.Windows.Forms.SplitContainer();
            this.splitContainer10 = new System.Windows.Forms.SplitContainer();
            this.mActiveFile = new System.Windows.Forms.TextBox();
            this.mFormTabs = new System.Windows.Forms.TabControl();
            this.mFeedTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mTimeZoneSelector = new MyCustomControls.InheritedCombo.MultiColumnComboBox(this.components);
            this.label25 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.mFeedLastBuildDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.mFeedSummary = new System.Windows.Forms.TextBox();
            this.mFeedPubDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.mFeedSubtitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mFeedTitle = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.mFeedAuthor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mFeedDescription = new System.Windows.Forms.TextBox();
            this.mFeedLanguage = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.mFeedLink = new System.Windows.Forms.TextBox();
            this.mFeedWebmaster = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mFeedOwnerName = new System.Windows.Forms.TextBox();
            this.mFeedOwnerEmail = new System.Windows.Forms.TextBox();
            this.mFeedCategory = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.mFeedCopyright = new System.Windows.Forms.TextBox();
            this.mFeedSubCategory = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mLocalImagePath = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.mImageURL = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.mImageDescription = new System.Windows.Forms.TextBox();
            this.mImageTitle = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.mImageHeight = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.mImageLink = new System.Windows.Forms.TextBox();
            this.mImageWidth = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.mImageFileSize = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.mFeedImagePictureBox = new System.Windows.Forms.PictureBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.mItemsTab = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.mMoveUpButton = new System.Windows.Forms.Button();
            this.mMoveDownButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mItemsList = new FeedBuilder.ListboxRefresher();
            this.mItemTitle = new System.Windows.Forms.TextBox();
            this.mMP3GroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.mGetMP3 = new System.Windows.Forms.Button();
            this.mPutMP3 = new System.Windows.Forms.Button();
            this.mPathLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.mBrowseMP3Button = new System.Windows.Forms.Button();
            this.mMP3Path = new System.Windows.Forms.TextBox();
            this.mItemDuration = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.mItemPubDate = new System.Windows.Forms.DateTimePicker();
            this.label31 = new System.Windows.Forms.Label();
            this.mItemGUID = new System.Windows.Forms.TextBox();
            this.mEnclosureUrl = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.mItemDescription = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.mItemSubtitle = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.mItemLink = new System.Windows.Forms.TextBox();
            this.mItemSummary = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.mItemAuthor = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mSearchLocalFiles = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mInsertButton = new System.Windows.Forms.Button();
            this.mDeleteButton = new System.Windows.Forms.Button();
            this.mDuplicateButton = new System.Windows.Forms.Button();
            this.mXMLTab = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.mXmlText = new FeedBuilder.NumberedTextBoxUC();
            this.mFindText = new System.Windows.Forms.TextBox();
            this.mFindButton = new System.Windows.Forms.Button();
            this.mResetXmlChanges = new System.Windows.Forms.Button();
            this.mApplyXmlChanges = new System.Windows.Forms.Button();
            this.mXsltTab = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.label42 = new System.Windows.Forms.Label();
            this.mXslt = new FeedBuilder.NumberedTextBoxUC();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.mTransformXml = new System.Windows.Forms.Button();
            this.mClearXsltOut = new System.Windows.Forms.Button();
            this.mFTPXsltOut = new System.Windows.Forms.Button();
            this.mCopyXsltOut = new System.Windows.Forms.Button();
            this.mTransform = new System.Windows.Forms.WebBrowser();
            this.mFTPTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.mConsoleTextbox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mLocalFileViewer = new FeedBuilder.LocalFileViewer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mFTPImage = new System.Windows.Forms.PictureBox();
            this.mFtpStatusBar = new System.Windows.Forms.ProgressBar();
            this.mCancelUploadImage = new System.Windows.Forms.PictureBox();
            this.mUploadProgressText = new System.Windows.Forms.Label();
            this.mPutXMLFile = new System.Windows.Forms.Button();
            this.mGetXMLFile = new System.Windows.Forms.Button();
            this.mConnect = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.mFTPViewer = new FeedBuilder.FTPViewer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label43 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.mValidationURL = new System.Windows.Forms.TextBox();
            this.mValidate = new System.Windows.Forms.Button();
            this.label40 = new System.Windows.Forms.Label();
            this.mContextMenuFileCache = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeFromListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xSLTransformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mFtpDownloadWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.mErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).BeginInit();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).BeginInit();
            this.splitContainer9.Panel1.SuspendLayout();
            this.splitContainer9.Panel2.SuspendLayout();
            this.splitContainer9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).BeginInit();
            this.splitContainer10.Panel1.SuspendLayout();
            this.splitContainer10.SuspendLayout();
            this.mFormTabs.SuspendLayout();
            this.mFeedTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFeedImagePictureBox)).BeginInit();
            this.mItemsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.mMP3GroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.mXMLTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.mXsltTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.mFTPTab.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFTPImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCancelUploadImage)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.mContextMenuFileCache.SuspendLayout();
            this.SuspendLayout();
            // 
            // mFTPUploadWorker
            // 
            this.mFTPUploadWorker.WorkerReportsProgress = true;
            this.mFTPUploadWorker.WorkerSupportsCancellation = true;
            this.mFTPUploadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mFTPUploadWorker_DoWork);
            this.mFTPUploadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mFTPUploadWorker_ProgressChanged);
            this.mFTPUploadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mFTPUploadWorker_RunWorkerCompleted);
            // 
            // mRefreshImage
            // 
            this.mRefreshImage.Image = ((System.Drawing.Image)(resources.GetObject("mRefreshImage.Image")));
            this.mRefreshImage.Location = new System.Drawing.Point(142, 143);
            this.mRefreshImage.Name = "mRefreshImage";
            this.mRefreshImage.Size = new System.Drawing.Size(25, 24);
            this.mRefreshImage.TabIndex = 84;
            this.mToolTip.SetToolTip(this.mRefreshImage, "Refresh Image");
            this.mRefreshImage.UseVisualStyleBackColor = true;
            this.mRefreshImage.Click += new System.EventHandler(this.mRefreshImage_Click);
            // 
            // mAboutImage
            // 
            this.mAboutImage.Location = new System.Drawing.Point(76, 76);
            this.mAboutImage.Name = "mAboutImage";
            this.mAboutImage.Size = new System.Drawing.Size(23, 23);
            this.mAboutImage.TabIndex = 2;
            this.mAboutImage.Text = "?";
            this.mToolTip.SetToolTip(this.mAboutImage, "Help With Images");
            this.mAboutImage.UseVisualStyleBackColor = true;
            this.mAboutImage.Click += new System.EventHandler(this.mAboutImage_Click);
            // 
            // mCloseImage
            // 
            this.mCloseImage.Image = ((System.Drawing.Image)(resources.GetObject("mCloseImage.Image")));
            this.mCloseImage.Location = new System.Drawing.Point(173, 143);
            this.mCloseImage.Name = "mCloseImage";
            this.mCloseImage.Size = new System.Drawing.Size(25, 24);
            this.mCloseImage.TabIndex = 4;
            this.mToolTip.SetToolTip(this.mCloseImage, "Delete Image");
            this.mCloseImage.UseVisualStyleBackColor = true;
            this.mCloseImage.Click += new System.EventHandler(this.mCloseImage_Click);
            // 
            // mOpenImage
            // 
            this.mOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("mOpenImage.Image")));
            this.mOpenImage.Location = new System.Drawing.Point(111, 143);
            this.mOpenImage.Name = "mOpenImage";
            this.mOpenImage.Size = new System.Drawing.Size(25, 24);
            this.mOpenImage.TabIndex = 3;
            this.mToolTip.SetToolTip(this.mOpenImage, "Open Image File");
            this.mOpenImage.UseVisualStyleBackColor = true;
            this.mOpenImage.Click += new System.EventHandler(this.mOpenImage_Click);
            // 
            // mErrorProvider
            // 
            this.mErrorProvider.ContainerControl = this;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
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
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.mItunesPodcast);
            this.splitContainer4.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer4.Size = new System.Drawing.Size(996, 573);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 4;
            // 
            // mItunesPodcast
            // 
            this.mItunesPodcast.AutoSize = true;
            this.mItunesPodcast.Dock = System.Windows.Forms.DockStyle.Right;
            this.mItunesPodcast.Location = new System.Drawing.Point(911, 0);
            this.mItunesPodcast.Name = "mItunesPodcast";
            this.mItunesPodcast.Size = new System.Drawing.Size(85, 25);
            this.mItunesPodcast.TabIndex = 4;
            this.mItunesPodcast.Text = "iTunes Feed";
            this.mItunesPodcast.UseVisualStyleBackColor = true;
            this.mItunesPodcast.CheckedChanged += new System.EventHandler(this.mItunesPodcast_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(-2, 1);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(89, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem1,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.saveAsToolStripMenuItem1,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem1
            // 
            this.newToolStripMenuItem1.Name = "newToolStripMenuItem1";
            this.newToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.newToolStripMenuItem1.Text = "New         ctrl-N";
            this.newToolStripMenuItem1.Click += new System.EventHandler(this.newToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.openToolStripMenuItem.Text = "Open       ctrl-O";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.saveToolStripMenuItem1.Text = "Save         ctrl-S";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.saveAsToolStripMenuItem1.Text = "Save As";
            this.saveAsToolStripMenuItem1.Click += new System.EventHandler(this.saveAsToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(159, 22);
            this.exitToolStripMenuItem1.Text = "Exit            ctrl-Q";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.mFileList);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.splitContainer9);
            this.splitContainer8.Size = new System.Drawing.Size(996, 547);
            this.splitContainer8.SplitterDistance = 152;
            this.splitContainer8.TabIndex = 3;
            // 
            // mFileList
            // 
            this.mFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFileList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.mFileList.FormattingEnabled = true;
            this.mFileList.Location = new System.Drawing.Point(0, 0);
            this.mFileList.Name = "mFileList";
            this.mFileList.Size = new System.Drawing.Size(152, 547);
            this.mFileList.TabIndex = 0;
            this.mFileList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.mFileList_DrawItem);
            this.mFileList.DoubleClick += new System.EventHandler(this.mFileList_DoubleClick);
            this.mFileList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mFileList_MouseUp);
            // 
            // splitContainer9
            // 
            this.splitContainer9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer9.IsSplitterFixed = true;
            this.splitContainer9.Location = new System.Drawing.Point(0, 0);
            this.splitContainer9.Name = "splitContainer9";
            this.splitContainer9.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer9.Panel1
            // 
            this.splitContainer9.Panel1.Controls.Add(this.splitContainer10);
            // 
            // splitContainer9.Panel2
            // 
            this.splitContainer9.Panel2.Controls.Add(this.mFormTabs);
            this.splitContainer9.Size = new System.Drawing.Size(840, 547);
            this.splitContainer9.SplitterDistance = 25;
            this.splitContainer9.SplitterWidth = 1;
            this.splitContainer9.TabIndex = 4;
            this.splitContainer9.TabStop = false;
            // 
            // splitContainer10
            // 
            this.splitContainer10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer10.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer10.Location = new System.Drawing.Point(0, 0);
            this.splitContainer10.Name = "splitContainer10";
            // 
            // splitContainer10.Panel1
            // 
            this.splitContainer10.Panel1.Controls.Add(this.mActiveFile);
            this.splitContainer10.Panel2MinSize = 2;
            this.splitContainer10.Size = new System.Drawing.Size(840, 25);
            this.splitContainer10.SplitterDistance = 814;
            this.splitContainer10.SplitterWidth = 1;
            this.splitContainer10.TabIndex = 4;
            // 
            // mActiveFile
            // 
            this.mActiveFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mActiveFile.Location = new System.Drawing.Point(0, 0);
            this.mActiveFile.Name = "mActiveFile";
            this.mActiveFile.Size = new System.Drawing.Size(814, 20);
            this.mActiveFile.TabIndex = 3;
            // 
            // mFormTabs
            // 
            this.mFormTabs.Controls.Add(this.mFeedTab);
            this.mFormTabs.Controls.Add(this.mItemsTab);
            this.mFormTabs.Controls.Add(this.mXMLTab);
            this.mFormTabs.Controls.Add(this.mXsltTab);
            this.mFormTabs.Controls.Add(this.mFTPTab);
            this.mFormTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFormTabs.Location = new System.Drawing.Point(0, 0);
            this.mFormTabs.Name = "mFormTabs";
            this.mFormTabs.SelectedIndex = 0;
            this.mFormTabs.Size = new System.Drawing.Size(840, 521);
            this.mFormTabs.TabIndex = 2;
            // 
            // mFeedTab
            // 
            this.mFeedTab.Controls.Add(this.groupBox2);
            this.mFeedTab.Controls.Add(this.groupBox1);
            this.mFeedTab.Location = new System.Drawing.Point(4, 22);
            this.mFeedTab.Name = "mFeedTab";
            this.mFeedTab.Padding = new System.Windows.Forms.Padding(3);
            this.mFeedTab.Size = new System.Drawing.Size(832, 495);
            this.mFeedTab.TabIndex = 0;
            this.mFeedTab.Text = "Feed Info";
            this.mFeedTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.mTimeZoneSelector);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.mFeedLastBuildDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.mFeedSummary);
            this.groupBox2.Controls.Add(this.mFeedPubDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.mFeedSubtitle);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.mFeedTitle);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.mFeedAuthor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.mFeedDescription);
            this.groupBox2.Controls.Add(this.mFeedLanguage);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.mFeedLink);
            this.groupBox2.Controls.Add(this.mFeedWebmaster);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.mFeedOwnerName);
            this.groupBox2.Controls.Add(this.mFeedOwnerEmail);
            this.groupBox2.Controls.Add(this.mFeedCategory);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.mFeedCopyright);
            this.groupBox2.Controls.Add(this.mFeedSubCategory);
            this.groupBox2.Location = new System.Drawing.Point(391, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(393, 442);
            this.groupBox2.TabIndex = 85;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Feed Properties";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 337);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(102, 13);
            this.label16.TabIndex = 30;
            this.label16.Text = "iTunes:Subcategory";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(22, 233);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "iTunes:Summary";
            // 
            // mTimeZoneSelector
            // 
            this.mTimeZoneSelector.DataMember = "";
            this.mTimeZoneSelector.DataValue = "";
            this.mTimeZoneSelector.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.mTimeZoneSelector.FormattingEnabled = true;
            this.mTimeZoneSelector.Location = new System.Drawing.Point(113, 407);
            this.mTimeZoneSelector.Name = "mTimeZoneSelector";
            this.mTimeZoneSelector.Size = new System.Drawing.Size(82, 21);
            this.mTimeZoneSelector.TabIndex = 27;
            this.mTimeZoneSelector.AfterSelectEvent += new MyCustomControls.InheritedCombo.AfterSelectEventHandler(this.mTimeZoneSelector_AfterSelectEvent);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(46, 410);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(58, 13);
            this.label25.TabIndex = 63;
            this.label25.Text = "Time Zone";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(315, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Email (Name)";
            // 
            // mFeedLastBuildDate
            // 
            this.mFeedLastBuildDate.Checked = false;
            this.mFeedLastBuildDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            this.mFeedLastBuildDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mFeedLastBuildDate.Location = new System.Drawing.Point(113, 386);
            this.mFeedLastBuildDate.Name = "mFeedLastBuildDate";
            this.mFeedLastBuildDate.Size = new System.Drawing.Size(148, 20);
            this.mFeedLastBuildDate.TabIndex = 26;
            this.mFeedLastBuildDate.ValueChanged += new System.EventHandler(this.mFeedLastBuildDate_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 390);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Last Build Date";
            // 
            // mFeedSummary
            // 
            this.mFeedSummary.Location = new System.Drawing.Point(113, 230);
            this.mFeedSummary.Name = "mFeedSummary";
            this.mFeedSummary.Size = new System.Drawing.Size(271, 20);
            this.mFeedSummary.TabIndex = 20;
            this.mFeedSummary.Leave += new System.EventHandler(this.mFeedSummary_Leave);
            // 
            // mFeedPubDate
            // 
            this.mFeedPubDate.Checked = false;
            this.mFeedPubDate.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.mFeedPubDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mFeedPubDate.Location = new System.Drawing.Point(113, 360);
            this.mFeedPubDate.Name = "mFeedPubDate";
            this.mFeedPubDate.Size = new System.Drawing.Size(148, 20);
            this.mFeedPubDate.TabIndex = 25;
            this.mFeedPubDate.ValueChanged += new System.EventHandler(this.mFeedPubDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Title";
            // 
            // mFeedSubtitle
            // 
            this.mFeedSubtitle.Location = new System.Drawing.Point(113, 204);
            this.mFeedSubtitle.Name = "mFeedSubtitle";
            this.mFeedSubtitle.Size = new System.Drawing.Size(271, 20);
            this.mFeedSubtitle.TabIndex = 19;
            this.mFeedSubtitle.Leave += new System.EventHandler(this.mFeedSubtitle_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 364);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Publication Date";
            // 
            // mFeedTitle
            // 
            this.mFeedTitle.Location = new System.Drawing.Point(113, 21);
            this.mFeedTitle.Name = "mFeedTitle";
            this.mFeedTitle.Size = new System.Drawing.Size(271, 20);
            this.mFeedTitle.TabIndex = 12;
            this.mFeedTitle.Leave += new System.EventHandler(this.mFeedTitle_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 210);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "iTunes:Subtitle";
            // 
            // mFeedAuthor
            // 
            this.mFeedAuthor.Location = new System.Drawing.Point(113, 178);
            this.mFeedAuthor.Name = "mFeedAuthor";
            this.mFeedAuthor.Size = new System.Drawing.Size(271, 20);
            this.mFeedAuthor.TabIndex = 18;
            this.mFeedAuthor.Leave += new System.EventHandler(this.mFeedAuthor_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Description";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(34, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "iTunes:Author";
            // 
            // mFeedDescription
            // 
            this.mFeedDescription.Location = new System.Drawing.Point(113, 47);
            this.mFeedDescription.Name = "mFeedDescription";
            this.mFeedDescription.Size = new System.Drawing.Size(271, 20);
            this.mFeedDescription.TabIndex = 13;
            this.mFeedDescription.Leave += new System.EventHandler(this.mFeedDescription_Leave);
            // 
            // mFeedLanguage
            // 
            this.mFeedLanguage.FormattingEnabled = true;
            this.mFeedLanguage.Location = new System.Drawing.Point(113, 151);
            this.mFeedLanguage.Name = "mFeedLanguage";
            this.mFeedLanguage.Size = new System.Drawing.Size(194, 21);
            this.mFeedLanguage.TabIndex = 17;
            this.mFeedLanguage.SelectedIndexChanged += new System.EventHandler(this.mFeedLanguage_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(52, 154);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(55, 13);
            this.label24.TabIndex = 61;
            this.label24.Text = "Language";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Link";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Webmaster";
            // 
            // mFeedLink
            // 
            this.mFeedLink.Location = new System.Drawing.Point(113, 73);
            this.mFeedLink.Name = "mFeedLink";
            this.mFeedLink.Size = new System.Drawing.Size(271, 20);
            this.mFeedLink.TabIndex = 14;
            this.mFeedLink.Leave += new System.EventHandler(this.mFeedLink_Leave);
            // 
            // mFeedWebmaster
            // 
            this.mFeedWebmaster.Location = new System.Drawing.Point(113, 125);
            this.mFeedWebmaster.Name = "mFeedWebmaster";
            this.mFeedWebmaster.Size = new System.Drawing.Size(271, 20);
            this.mFeedWebmaster.TabIndex = 16;
            this.mFeedWebmaster.Leave += new System.EventHandler(this.mFeedWebmaster_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 259);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "iTunes:Owner Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Copyright";
            // 
            // mFeedOwnerName
            // 
            this.mFeedOwnerName.Location = new System.Drawing.Point(113, 256);
            this.mFeedOwnerName.Name = "mFeedOwnerName";
            this.mFeedOwnerName.Size = new System.Drawing.Size(271, 20);
            this.mFeedOwnerName.TabIndex = 21;
            this.mFeedOwnerName.Leave += new System.EventHandler(this.mFeedOwnerName_Leave);
            // 
            // mFeedOwnerEmail
            // 
            this.mFeedOwnerEmail.Location = new System.Drawing.Point(113, 282);
            this.mFeedOwnerEmail.Name = "mFeedOwnerEmail";
            this.mFeedOwnerEmail.Size = new System.Drawing.Size(196, 20);
            this.mFeedOwnerEmail.TabIndex = 22;
            this.mFeedOwnerEmail.Leave += new System.EventHandler(this.mFeedOwnerEmail_Leave);
            // 
            // mFeedCategory
            // 
            this.mFeedCategory.Location = new System.Drawing.Point(113, 308);
            this.mFeedCategory.Name = "mFeedCategory";
            this.mFeedCategory.Size = new System.Drawing.Size(271, 20);
            this.mFeedCategory.TabIndex = 23;
            this.mFeedCategory.Leave += new System.EventHandler(this.mFeedCategory_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 285);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "iTunes:Owner Email";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 311);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "iTunes:Category";
            // 
            // mFeedCopyright
            // 
            this.mFeedCopyright.Location = new System.Drawing.Point(113, 99);
            this.mFeedCopyright.Name = "mFeedCopyright";
            this.mFeedCopyright.Size = new System.Drawing.Size(271, 20);
            this.mFeedCopyright.TabIndex = 15;
            this.mFeedCopyright.Leave += new System.EventHandler(this.mFeedCopyright_Leave);
            // 
            // mFeedSubCategory
            // 
            this.mFeedSubCategory.Location = new System.Drawing.Point(113, 334);
            this.mFeedSubCategory.Name = "mFeedSubCategory";
            this.mFeedSubCategory.Size = new System.Drawing.Size(271, 20);
            this.mFeedSubCategory.TabIndex = 24;
            this.mFeedSubCategory.Leave += new System.EventHandler(this.mFeedSubCategory_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mRefreshImage);
            this.groupBox1.Controls.Add(this.mLocalImagePath);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.mAboutImage);
            this.groupBox1.Controls.Add(this.mImageURL);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.mImageDescription);
            this.groupBox1.Controls.Add(this.mImageTitle);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.mImageHeight);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.mImageLink);
            this.groupBox1.Controls.Add(this.mCloseImage);
            this.groupBox1.Controls.Add(this.mOpenImage);
            this.groupBox1.Controls.Add(this.mImageWidth);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.mImageFileSize);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.mFeedImagePictureBox);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 314);
            this.groupBox1.TabIndex = 84;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feed Image";
            // 
            // mLocalImagePath
            // 
            this.mLocalImagePath.Location = new System.Drawing.Point(104, 178);
            this.mLocalImagePath.Name = "mLocalImagePath";
            this.mLocalImagePath.Size = new System.Drawing.Size(267, 20);
            this.mLocalImagePath.TabIndex = 82;
            this.mLocalImagePath.TextChanged += new System.EventHandler(this.mLocalImagePath_TextChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(40, 181);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(58, 13);
            this.label41.TabIndex = 83;
            this.label41.Text = "Local Path";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 233);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(92, 13);
            this.label21.TabIndex = 74;
            this.label21.Text = "Image Description";
            // 
            // mImageURL
            // 
            this.mImageURL.Location = new System.Drawing.Point(104, 257);
            this.mImageURL.Name = "mImageURL";
            this.mImageURL.Size = new System.Drawing.Size(267, 20);
            this.mImageURL.TabIndex = 7;
            this.mImageURL.Leave += new System.EventHandler(this.mImageURL_Leave);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(126, 11);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(73, 26);
            this.label38.TabIndex = 1;
            this.label38.Text = "Feed Image\r\n  (Optional)";
            // 
            // mImageDescription
            // 
            this.mImageDescription.Location = new System.Drawing.Point(104, 230);
            this.mImageDescription.Name = "mImageDescription";
            this.mImageDescription.Size = new System.Drawing.Size(267, 20);
            this.mImageDescription.TabIndex = 6;
            this.mImageDescription.Leave += new System.EventHandler(this.mImageDescription_Leave);
            // 
            // mImageTitle
            // 
            this.mImageTitle.Location = new System.Drawing.Point(104, 204);
            this.mImageTitle.Name = "mImageTitle";
            this.mImageTitle.Size = new System.Drawing.Size(267, 20);
            this.mImageTitle.TabIndex = 5;
            this.mImageTitle.Leave += new System.EventHandler(this.mImageTitle_Leave);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(209, 53);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(46, 13);
            this.label35.TabIndex = 77;
            this.label35.Text = "File Size";
            // 
            // mImageHeight
            // 
            this.mImageHeight.Enabled = false;
            this.mImageHeight.Location = new System.Drawing.Point(261, 102);
            this.mImageHeight.Name = "mImageHeight";
            this.mImageHeight.Size = new System.Drawing.Size(87, 20);
            this.mImageHeight.TabIndex = 11;
            this.mImageHeight.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(38, 260);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(61, 13);
            this.label23.TabIndex = 73;
            this.label23.Text = "Image URL";
            // 
            // mImageLink
            // 
            this.mImageLink.Location = new System.Drawing.Point(104, 283);
            this.mImageLink.Name = "mImageLink";
            this.mImageLink.Size = new System.Drawing.Size(267, 20);
            this.mImageLink.TabIndex = 8;
            this.mImageLink.Leave += new System.EventHandler(this.mImageLink_Leave);
            // 
            // mImageWidth
            // 
            this.mImageWidth.Enabled = false;
            this.mImageWidth.Location = new System.Drawing.Point(261, 76);
            this.mImageWidth.Name = "mImageWidth";
            this.mImageWidth.Size = new System.Drawing.Size(87, 20);
            this.mImageWidth.TabIndex = 10;
            this.mImageWidth.TabStop = false;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(39, 287);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 13);
            this.label34.TabIndex = 75;
            this.label34.Text = "Image Link";
            // 
            // mImageFileSize
            // 
            this.mImageFileSize.Enabled = false;
            this.mImageFileSize.Location = new System.Drawing.Point(261, 50);
            this.mImageFileSize.Name = "mImageFileSize";
            this.mImageFileSize.Size = new System.Drawing.Size(87, 20);
            this.mImageFileSize.TabIndex = 9;
            this.mImageFileSize.TabStop = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(39, 207);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 13);
            this.label30.TabIndex = 72;
            this.label30.Text = "Image Title";
            // 
            // mFeedImagePictureBox
            // 
            this.mFeedImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mFeedImagePictureBox.Location = new System.Drawing.Point(104, 42);
            this.mFeedImagePictureBox.Name = "mFeedImagePictureBox";
            this.mFeedImagePictureBox.Size = new System.Drawing.Size(100, 99);
            this.mFeedImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mFeedImagePictureBox.TabIndex = 65;
            this.mFeedImagePictureBox.TabStop = false;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(217, 105);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(38, 13);
            this.label36.TabIndex = 79;
            this.label36.Text = "Height";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(220, 79);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(35, 13);
            this.label37.TabIndex = 81;
            this.label37.Text = "Width";
            // 
            // mItemsTab
            // 
            this.mItemsTab.Controls.Add(this.splitContainer2);
            this.mItemsTab.Location = new System.Drawing.Point(4, 22);
            this.mItemsTab.Name = "mItemsTab";
            this.mItemsTab.Padding = new System.Windows.Forms.Padding(3);
            this.mItemsTab.Size = new System.Drawing.Size(832, 495);
            this.mItemsTab.TabIndex = 1;
            this.mItemsTab.Text = "Feed Items";
            this.mItemsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.mMoveUpButton);
            this.splitContainer2.Panel1.Controls.Add(this.mMoveDownButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(826, 489);
            this.splitContainer2.SplitterDistance = 40;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 80;
            // 
            // mMoveUpButton
            // 
            this.mMoveUpButton.Image = ((System.Drawing.Image)(resources.GetObject("mMoveUpButton.Image")));
            this.mMoveUpButton.Location = new System.Drawing.Point(5, 122);
            this.mMoveUpButton.Name = "mMoveUpButton";
            this.mMoveUpButton.Size = new System.Drawing.Size(32, 32);
            this.mMoveUpButton.TabIndex = 0;
            this.mMoveUpButton.Click += new System.EventHandler(this.mMoveUpButton_Click);
            // 
            // mMoveDownButton
            // 
            this.mMoveDownButton.Image = ((System.Drawing.Image)(resources.GetObject("mMoveDownButton.Image")));
            this.mMoveDownButton.Location = new System.Drawing.Point(5, 160);
            this.mMoveDownButton.Name = "mMoveDownButton";
            this.mMoveDownButton.Size = new System.Drawing.Size(32, 32);
            this.mMoveDownButton.TabIndex = 1;
            this.mMoveDownButton.UseVisualStyleBackColor = true;
            this.mMoveDownButton.Click += new System.EventHandler(this.mMoveDownButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.mItemsList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mItemTitle, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.mMP3GroupBox, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label17, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label33, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mItemDescription, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label18, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label28, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.mItemSubtitle, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label27, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label29, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.mItemLink, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.mItemSummary, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label26, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.mItemAuthor, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label22, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 7);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(785, 489);
            this.tableLayoutPanel1.TabIndex = 79;
            // 
            // mItemsList
            // 
            this.mItemsList.FormattingEnabled = true;
            this.mItemsList.Location = new System.Drawing.Point(3, 29);
            this.mItemsList.Name = "mItemsList";
            this.tableLayoutPanel1.SetRowSpan(this.mItemsList, 6);
            this.mItemsList.Size = new System.Drawing.Size(164, 303);
            this.mItemsList.TabIndex = 81;
            this.mItemsList.SelectedIndexChanged += new System.EventHandler(this.mItemsList_SelectedIndexChanged);
            this.mItemsList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mItemsList_KeyPress);
            // 
            // mItemTitle
            // 
            this.mItemTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemTitle.Location = new System.Drawing.Point(256, 3);
            this.mItemTitle.Name = "mItemTitle";
            this.mItemTitle.Size = new System.Drawing.Size(526, 20);
            this.mItemTitle.TabIndex = 6;
            this.mItemTitle.Leave += new System.EventHandler(this.mItemTitle_Leave);
            // 
            // mMP3GroupBox
            // 
            this.mMP3GroupBox.Controls.Add(this.tableLayoutPanel2);
            this.mMP3GroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mMP3GroupBox.Location = new System.Drawing.Point(256, 160);
            this.mMP3GroupBox.Name = "mMP3GroupBox";
            this.mMP3GroupBox.Size = new System.Drawing.Size(526, 147);
            this.mMP3GroupBox.TabIndex = 75;
            this.mMP3GroupBox.TabStop = false;
            this.mMP3GroupBox.Text = "File Info";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 172F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label20, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.mGetMP3, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.mPutMP3, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.mPathLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.mBrowseMP3Button, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.mMP3Path, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.mItemDuration, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label32, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.mItemPubDate, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label31, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.mItemGUID, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.mEnclosureUrl, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 128);
            this.tableLayoutPanel2.TabIndex = 80;
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(57, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 13);
            this.label20.TabIndex = 81;
            this.label20.Text = "URL";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mGetMP3
            // 
            this.mGetMP3.Enabled = false;
            this.mGetMP3.Location = new System.Drawing.Point(353, 101);
            this.mGetMP3.Name = "mGetMP3";
            this.mGetMP3.Size = new System.Drawing.Size(67, 23);
            this.mGetMP3.TabIndex = 17;
            this.mGetMP3.Text = "Get MP3";
            this.mGetMP3.UseVisualStyleBackColor = true;
            this.mGetMP3.Visible = false;
            // 
            // mPutMP3
            // 
            this.mPutMP3.Enabled = false;
            this.mPutMP3.Location = new System.Drawing.Point(181, 101);
            this.mPutMP3.Name = "mPutMP3";
            this.mPutMP3.Size = new System.Drawing.Size(63, 23);
            this.mPutMP3.TabIndex = 16;
            this.mPutMP3.Text = "Put MP3";
            this.mPutMP3.UseVisualStyleBackColor = true;
            this.mPutMP3.Visible = false;
            // 
            // mPathLabel
            // 
            this.mPathLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.mPathLabel.AutoSize = true;
            this.mPathLabel.Location = new System.Drawing.Point(28, 32);
            this.mPathLabel.Name = "mPathLabel";
            this.mPathLabel.Size = new System.Drawing.Size(58, 13);
            this.mPathLabel.TabIndex = 78;
            this.mPathLabel.Text = "Local Path";
            this.mPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 74;
            this.label14.Text = "iTunes:Duration";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mBrowseMP3Button
            // 
            this.mBrowseMP3Button.Enabled = false;
            this.mBrowseMP3Button.Location = new System.Drawing.Point(92, 101);
            this.mBrowseMP3Button.Name = "mBrowseMP3Button";
            this.mBrowseMP3Button.Size = new System.Drawing.Size(83, 23);
            this.mBrowseMP3Button.TabIndex = 15;
            this.mBrowseMP3Button.Text = "Browse MP3";
            this.mBrowseMP3Button.UseVisualStyleBackColor = true;
            this.mBrowseMP3Button.Click += new System.EventHandler(this.mBrowseMP3Button_Click);
            // 
            // mMP3Path
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.mMP3Path, 3);
            this.mMP3Path.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mMP3Path.Location = new System.Drawing.Point(92, 29);
            this.mMP3Path.Name = "mMP3Path";
            this.mMP3Path.Size = new System.Drawing.Size(425, 20);
            this.mMP3Path.TabIndex = 13;
            this.mMP3Path.TabStop = false;
            // 
            // mItemDuration
            // 
            this.mItemDuration.Dock = System.Windows.Forms.DockStyle.Left;
            this.mItemDuration.Enabled = false;
            this.mItemDuration.Location = new System.Drawing.Point(92, 3);
            this.mItemDuration.Name = "mItemDuration";
            this.mItemDuration.Size = new System.Drawing.Size(83, 20);
            this.mItemDuration.TabIndex = 11;
            this.mItemDuration.TabStop = false;
            this.mItemDuration.Validating += new System.ComponentModel.CancelEventHandler(this.mItemDuration_Validating);
            this.mItemDuration.Validated += new System.EventHandler(this.mItemDuration_Validated);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(262, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(85, 26);
            this.label32.TabIndex = 38;
            this.label32.Text = "Publication Date";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemPubDate
            // 
            this.mItemPubDate.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.mItemPubDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mItemPubDate.Location = new System.Drawing.Point(353, 3);
            this.mItemPubDate.Name = "mItemPubDate";
            this.mItemPubDate.Size = new System.Drawing.Size(156, 20);
            this.mItemPubDate.TabIndex = 12;
            this.mItemPubDate.ValueChanged += new System.EventHandler(this.mItemPubDate_ValueChanged_1);
            // 
            // label31
            // 
            this.label31.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(52, 80);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(34, 13);
            this.label31.TabIndex = 79;
            this.label31.Text = "GUID";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemGUID
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.mItemGUID, 3);
            this.mItemGUID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mItemGUID.Location = new System.Drawing.Point(92, 78);
            this.mItemGUID.Name = "mItemGUID";
            this.mItemGUID.Size = new System.Drawing.Size(425, 20);
            this.mItemGUID.TabIndex = 14;
            this.mItemGUID.TabStop = false;
            // 
            // mEnclosureUrl
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.mEnclosureUrl, 3);
            this.mEnclosureUrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mEnclosureUrl.Location = new System.Drawing.Point(92, 55);
            this.mEnclosureUrl.Name = "mEnclosureUrl";
            this.mEnclosureUrl.Size = new System.Drawing.Size(425, 20);
            this.mEnclosureUrl.TabIndex = 80;
            this.mEnclosureUrl.Leave += new System.EventHandler(this.mEnclosureUrl_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Left;
            this.label17.Location = new System.Drawing.Point(3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 26);
            this.label17.TabIndex = 1;
            this.label17.Text = "Items";
            this.label17.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Right;
            this.label33.Location = new System.Drawing.Point(223, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 26);
            this.label33.TabIndex = 37;
            this.label33.Text = "Title";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemDescription
            // 
            this.mItemDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemDescription.Location = new System.Drawing.Point(256, 29);
            this.mItemDescription.Name = "mItemDescription";
            this.mItemDescription.Size = new System.Drawing.Size(526, 20);
            this.mItemDescription.TabIndex = 7;
            this.mItemDescription.Leave += new System.EventHandler(this.mItemDescription_Leave);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Right;
            this.label18.Location = new System.Drawing.Point(216, 157);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 153);
            this.label18.TabIndex = 76;
            this.label18.Text = "Audio";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Right;
            this.label28.Location = new System.Drawing.Point(190, 26);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(60, 26);
            this.label28.TabIndex = 42;
            this.label28.Text = "Description";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemSubtitle
            // 
            this.mItemSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemSubtitle.Location = new System.Drawing.Point(256, 55);
            this.mItemSubtitle.Name = "mItemSubtitle";
            this.mItemSubtitle.Size = new System.Drawing.Size(526, 20);
            this.mItemSubtitle.TabIndex = 8;
            this.mItemSubtitle.Leave += new System.EventHandler(this.mItemSubtitle_Leave);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Right;
            this.label27.Location = new System.Drawing.Point(173, 52);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(77, 26);
            this.label27.TabIndex = 44;
            this.label27.Text = "iTunes:Subtitle";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Right;
            this.label29.Location = new System.Drawing.Point(223, 310);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(27, 33);
            this.label29.TabIndex = 41;
            this.label29.Text = "Link";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemLink
            // 
            this.mItemLink.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemLink.Location = new System.Drawing.Point(256, 313);
            this.mItemLink.Name = "mItemLink";
            this.mItemLink.Size = new System.Drawing.Size(526, 20);
            this.mItemLink.TabIndex = 18;
            this.mItemLink.Leave += new System.EventHandler(this.mItemLink_Leave);
            // 
            // mItemSummary
            // 
            this.mItemSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemSummary.Location = new System.Drawing.Point(256, 81);
            this.mItemSummary.Multiline = true;
            this.mItemSummary.Name = "mItemSummary";
            this.mItemSummary.Size = new System.Drawing.Size(526, 47);
            this.mItemSummary.TabIndex = 9;
            this.mItemSummary.Leave += new System.EventHandler(this.mItemSummary_Leave);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(200, 78);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(50, 53);
            this.label26.TabIndex = 46;
            this.label26.Text = "iTunes:\r\nSummary";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemAuthor
            // 
            this.mItemAuthor.Dock = System.Windows.Forms.DockStyle.Top;
            this.mItemAuthor.Location = new System.Drawing.Point(256, 134);
            this.mItemAuthor.Name = "mItemAuthor";
            this.mItemAuthor.Size = new System.Drawing.Size(526, 20);
            this.mItemAuthor.TabIndex = 10;
            this.mItemAuthor.Leave += new System.EventHandler(this.mItemAuthor_Leave);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Right;
            this.label22.Location = new System.Drawing.Point(177, 131);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(73, 26);
            this.label22.TabIndex = 53;
            this.label22.Text = "iTunes:Author";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(256, 346);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mSearchLocalFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Size = new System.Drawing.Size(526, 33);
            this.splitContainer1.SplitterDistance = 58;
            this.splitContainer1.TabIndex = 78;
            // 
            // mSearchLocalFiles
            // 
            this.mSearchLocalFiles.Enabled = false;
            this.mSearchLocalFiles.Location = new System.Drawing.Point(3, 3);
            this.mSearchLocalFiles.Name = "mSearchLocalFiles";
            this.mSearchLocalFiles.Size = new System.Drawing.Size(53, 23);
            this.mSearchLocalFiles.TabIndex = 77;
            this.mSearchLocalFiles.Text = "Search";
            this.mSearchLocalFiles.UseVisualStyleBackColor = true;
            this.mSearchLocalFiles.Click += new System.EventHandler(this.mSearchLocalFiles_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(350, 13);
            this.label19.TabIndex = 78;
            this.label19.Text = "Examines each item link and searches for a corresponding local MP3 file.";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.mInsertButton);
            this.panel1.Controls.Add(this.mDeleteButton);
            this.panel1.Controls.Add(this.mDuplicateButton);
            this.panel1.Location = new System.Drawing.Point(3, 346);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 33);
            this.panel1.TabIndex = 79;
            // 
            // mInsertButton
            // 
            this.mInsertButton.Location = new System.Drawing.Point(3, 3);
            this.mInsertButton.Name = "mInsertButton";
            this.mInsertButton.Size = new System.Drawing.Size(38, 23);
            this.mInsertButton.TabIndex = 3;
            this.mInsertButton.Text = "Add";
            this.mInsertButton.UseVisualStyleBackColor = true;
            this.mInsertButton.Click += new System.EventHandler(this.mInsertButton_Click);
            // 
            // mDeleteButton
            // 
            this.mDeleteButton.Enabled = false;
            this.mDeleteButton.Location = new System.Drawing.Point(47, 3);
            this.mDeleteButton.Name = "mDeleteButton";
            this.mDeleteButton.Size = new System.Drawing.Size(46, 23);
            this.mDeleteButton.TabIndex = 4;
            this.mDeleteButton.Text = "Delete";
            this.mDeleteButton.UseVisualStyleBackColor = true;
            this.mDeleteButton.Click += new System.EventHandler(this.mDeleteButton_Click);
            // 
            // mDuplicateButton
            // 
            this.mDuplicateButton.Enabled = false;
            this.mDuplicateButton.Location = new System.Drawing.Point(99, 3);
            this.mDuplicateButton.Name = "mDuplicateButton";
            this.mDuplicateButton.Size = new System.Drawing.Size(64, 23);
            this.mDuplicateButton.TabIndex = 5;
            this.mDuplicateButton.Text = "Duplicate";
            this.mDuplicateButton.UseVisualStyleBackColor = true;
            this.mDuplicateButton.Click += new System.EventHandler(this.mDuplicateButton_Click);
            // 
            // mXMLTab
            // 
            this.mXMLTab.Controls.Add(this.splitContainer3);
            this.mXMLTab.Location = new System.Drawing.Point(4, 22);
            this.mXMLTab.Name = "mXMLTab";
            this.mXMLTab.Padding = new System.Windows.Forms.Padding(3);
            this.mXMLTab.Size = new System.Drawing.Size(832, 495);
            this.mXMLTab.TabIndex = 4;
            this.mXMLTab.Text = "XML";
            this.mXMLTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.mXmlText);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.mFindText);
            this.splitContainer3.Panel2.Controls.Add(this.mFindButton);
            this.splitContainer3.Panel2.Controls.Add(this.mResetXmlChanges);
            this.splitContainer3.Panel2.Controls.Add(this.mApplyXmlChanges);
            this.splitContainer3.Size = new System.Drawing.Size(826, 489);
            this.splitContainer3.SplitterDistance = 437;
            this.splitContainer3.TabIndex = 1;
            // 
            // mXmlText
            // 
            this.mXmlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mXmlText.Location = new System.Drawing.Point(0, 0);
            this.mXmlText.Name = "mXmlText";
            this.mXmlText.Size = new System.Drawing.Size(826, 437);
            this.mXmlText.TabIndex = 2;
            // 
            // mFindText
            // 
            this.mFindText.Enabled = false;
            this.mFindText.Location = new System.Drawing.Point(467, 5);
            this.mFindText.Name = "mFindText";
            this.mFindText.Size = new System.Drawing.Size(287, 20);
            this.mFindText.TabIndex = 3;
            this.mFindText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mFindText_KeyPress);
            // 
            // mFindButton
            // 
            this.mFindButton.Enabled = false;
            this.mFindButton.Location = new System.Drawing.Point(760, 3);
            this.mFindButton.Name = "mFindButton";
            this.mFindButton.Size = new System.Drawing.Size(52, 23);
            this.mFindButton.TabIndex = 2;
            this.mFindButton.Text = "Find";
            this.mFindButton.UseVisualStyleBackColor = true;
            this.mFindButton.Click += new System.EventHandler(this.mFindButton_Click);
            // 
            // mResetXmlChanges
            // 
            this.mResetXmlChanges.Enabled = false;
            this.mResetXmlChanges.Location = new System.Drawing.Point(112, 3);
            this.mResetXmlChanges.Name = "mResetXmlChanges";
            this.mResetXmlChanges.Size = new System.Drawing.Size(102, 23);
            this.mResetXmlChanges.TabIndex = 1;
            this.mResetXmlChanges.Text = "Reset Changes";
            this.mResetXmlChanges.UseVisualStyleBackColor = true;
            this.mResetXmlChanges.Click += new System.EventHandler(this.mResetXmlChanges_Click);
            // 
            // mApplyXmlChanges
            // 
            this.mApplyXmlChanges.Enabled = false;
            this.mApplyXmlChanges.Location = new System.Drawing.Point(5, 3);
            this.mApplyXmlChanges.Name = "mApplyXmlChanges";
            this.mApplyXmlChanges.Size = new System.Drawing.Size(101, 23);
            this.mApplyXmlChanges.TabIndex = 0;
            this.mApplyXmlChanges.Text = "Apply Changes";
            this.mApplyXmlChanges.UseVisualStyleBackColor = true;
            this.mApplyXmlChanges.Click += new System.EventHandler(this.mApplyXmlChanges_Click);
            // 
            // mXsltTab
            // 
            this.mXsltTab.Controls.Add(this.splitContainer5);
            this.mXsltTab.Location = new System.Drawing.Point(4, 22);
            this.mXsltTab.Name = "mXsltTab";
            this.mXsltTab.Padding = new System.Windows.Forms.Padding(3);
            this.mXsltTab.Size = new System.Drawing.Size(832, 495);
            this.mXsltTab.TabIndex = 5;
            this.mXsltTab.Text = "HTML";
            this.mXsltTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(3, 3);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.splitContainer6);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer5.Size = new System.Drawing.Size(826, 489);
            this.splitContainer5.SplitterDistance = 241;
            this.splitContainer5.TabIndex = 5;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.label42);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.mXslt);
            this.splitContainer6.Size = new System.Drawing.Size(826, 241);
            this.splitContainer6.SplitterDistance = 25;
            this.splitContainer6.TabIndex = 3;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(2, 7);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(492, 13);
            this.label42.TabIndex = 0;
            this.label42.Text = "The XSLT code below will transform your xml feed into an html document.  Just hit" +
    " the Generate button.";
            // 
            // mXslt
            // 
            this.mXslt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mXslt.Location = new System.Drawing.Point(0, 0);
            this.mXslt.Name = "mXslt";
            this.mXslt.Size = new System.Drawing.Size(826, 212);
            this.mXslt.TabIndex = 2;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.mTransformXml);
            this.splitContainer7.Panel1.Controls.Add(this.mClearXsltOut);
            this.splitContainer7.Panel1.Controls.Add(this.mFTPXsltOut);
            this.splitContainer7.Panel1.Controls.Add(this.mCopyXsltOut);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.mTransform);
            this.splitContainer7.Size = new System.Drawing.Size(826, 244);
            this.splitContainer7.SplitterDistance = 107;
            this.splitContainer7.TabIndex = 0;
            // 
            // mTransformXml
            // 
            this.mTransformXml.Location = new System.Drawing.Point(3, 3);
            this.mTransformXml.Name = "mTransformXml";
            this.mTransformXml.Size = new System.Drawing.Size(100, 23);
            this.mTransformXml.TabIndex = 4;
            this.mTransformXml.Text = "Generate";
            this.mTransformXml.UseVisualStyleBackColor = true;
            this.mTransformXml.Click += new System.EventHandler(this.mTransformXml_Click);
            // 
            // mClearXsltOut
            // 
            this.mClearXsltOut.Location = new System.Drawing.Point(3, 158);
            this.mClearXsltOut.Name = "mClearXsltOut";
            this.mClearXsltOut.Size = new System.Drawing.Size(100, 23);
            this.mClearXsltOut.TabIndex = 9;
            this.mClearXsltOut.Text = "Clear Output";
            this.mClearXsltOut.UseVisualStyleBackColor = true;
            this.mClearXsltOut.Click += new System.EventHandler(this.mClearXsltOut_Click);
            // 
            // mFTPXsltOut
            // 
            this.mFTPXsltOut.Location = new System.Drawing.Point(3, 216);
            this.mFTPXsltOut.Name = "mFTPXsltOut";
            this.mFTPXsltOut.Size = new System.Drawing.Size(100, 23);
            this.mFTPXsltOut.TabIndex = 5;
            this.mFTPXsltOut.Text = "FTP Output";
            this.mFTPXsltOut.UseVisualStyleBackColor = true;
            this.mFTPXsltOut.Click += new System.EventHandler(this.mFTPXsltOut_Click);
            // 
            // mCopyXsltOut
            // 
            this.mCopyXsltOut.Location = new System.Drawing.Point(3, 187);
            this.mCopyXsltOut.Name = "mCopyXsltOut";
            this.mCopyXsltOut.Size = new System.Drawing.Size(100, 23);
            this.mCopyXsltOut.TabIndex = 6;
            this.mCopyXsltOut.Text = "Copy to Clipboard";
            this.mCopyXsltOut.UseVisualStyleBackColor = true;
            this.mCopyXsltOut.Click += new System.EventHandler(this.mCopyXsltOut_Click);
            // 
            // mTransform
            // 
            this.mTransform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mTransform.Location = new System.Drawing.Point(0, 0);
            this.mTransform.MinimumSize = new System.Drawing.Size(20, 20);
            this.mTransform.Name = "mTransform";
            this.mTransform.Size = new System.Drawing.Size(715, 244);
            this.mTransform.TabIndex = 3;
            // 
            // mFTPTab
            // 
            this.mFTPTab.Controls.Add(this.tableLayoutPanel4);
            this.mFTPTab.Location = new System.Drawing.Point(4, 22);
            this.mFTPTab.Name = "mFTPTab";
            this.mFTPTab.Padding = new System.Windows.Forms.Padding(3);
            this.mFTPTab.Size = new System.Drawing.Size(832, 495);
            this.mFTPTab.TabIndex = 3;
            this.mFTPTab.Text = "FTP";
            this.mFTPTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel7, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel8, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.label40, 0, 3);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.20137F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.79863F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(826, 489);
            this.tableLayoutPanel4.TabIndex = 49;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.mConsoleTextbox);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 345);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(820, 83);
            this.panel6.TabIndex = 0;
            // 
            // mConsoleTextbox
            // 
            this.mConsoleTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mConsoleTextbox.Location = new System.Drawing.Point(0, 0);
            this.mConsoleTextbox.Multiline = true;
            this.mConsoleTextbox.Name = "mConsoleTextbox";
            this.mConsoleTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mConsoleTextbox.Size = new System.Drawing.Size(820, 83);
            this.mConsoleTextbox.TabIndex = 45;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 196F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(820, 316);
            this.tableLayoutPanel3.TabIndex = 48;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mLocalFileViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(306, 310);
            this.panel2.TabIndex = 0;
            // 
            // mLocalFileViewer
            // 
            this.mLocalFileViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mLocalFileViewer.ItemChecked = false;
            this.mLocalFileViewer.Location = new System.Drawing.Point(0, 0);
            this.mLocalFileViewer.Name = "mLocalFileViewer";
            this.mLocalFileViewer.Size = new System.Drawing.Size(306, 310);
            this.mLocalFileViewer.TabIndex = 27;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.mFTPImage);
            this.panel3.Controls.Add(this.mFtpStatusBar);
            this.panel3.Controls.Add(this.mCancelUploadImage);
            this.panel3.Controls.Add(this.mUploadProgressText);
            this.panel3.Controls.Add(this.mPutXMLFile);
            this.panel3.Controls.Add(this.mGetXMLFile);
            this.panel3.Controls.Add(this.mConnect);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(315, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(190, 310);
            this.panel3.TabIndex = 1;
            // 
            // mFTPImage
            // 
            this.mFTPImage.Image = ((System.Drawing.Image)(resources.GetObject("mFTPImage.Image")));
            this.mFTPImage.Location = new System.Drawing.Point(19, 50);
            this.mFTPImage.Name = "mFTPImage";
            this.mFTPImage.Size = new System.Drawing.Size(156, 65);
            this.mFTPImage.TabIndex = 35;
            this.mFTPImage.TabStop = false;
            // 
            // mFtpStatusBar
            // 
            this.mFtpStatusBar.Location = new System.Drawing.Point(19, 121);
            this.mFtpStatusBar.Name = "mFtpStatusBar";
            this.mFtpStatusBar.Size = new System.Drawing.Size(138, 18);
            this.mFtpStatusBar.TabIndex = 0;
            // 
            // mCancelUploadImage
            // 
            this.mCancelUploadImage.Image = ((System.Drawing.Image)(resources.GetObject("mCancelUploadImage.Image")));
            this.mCancelUploadImage.Location = new System.Drawing.Point(163, 120);
            this.mCancelUploadImage.Name = "mCancelUploadImage";
            this.mCancelUploadImage.Size = new System.Drawing.Size(20, 20);
            this.mCancelUploadImage.TabIndex = 39;
            this.mCancelUploadImage.TabStop = false;
            this.mCancelUploadImage.Click += new System.EventHandler(this.mCancelUploadImage_Click);
            // 
            // mUploadProgressText
            // 
            this.mUploadProgressText.AutoSize = true;
            this.mUploadProgressText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mUploadProgressText.Location = new System.Drawing.Point(53, 142);
            this.mUploadProgressText.Name = "mUploadProgressText";
            this.mUploadProgressText.Size = new System.Drawing.Size(0, 13);
            this.mUploadProgressText.TabIndex = 40;
            // 
            // mPutXMLFile
            // 
            this.mPutXMLFile.Enabled = false;
            this.mPutXMLFile.Location = new System.Drawing.Point(41, 165);
            this.mPutXMLFile.Name = "mPutXMLFile";
            this.mPutXMLFile.Size = new System.Drawing.Size(103, 23);
            this.mPutXMLFile.TabIndex = 38;
            this.mPutXMLFile.Text = ">> Upload >>";
            this.mPutXMLFile.UseVisualStyleBackColor = true;
            this.mPutXMLFile.Click += new System.EventHandler(this.mPutXMLFile_Click);
            // 
            // mGetXMLFile
            // 
            this.mGetXMLFile.Enabled = false;
            this.mGetXMLFile.Location = new System.Drawing.Point(41, 194);
            this.mGetXMLFile.Name = "mGetXMLFile";
            this.mGetXMLFile.Size = new System.Drawing.Size(103, 23);
            this.mGetXMLFile.TabIndex = 31;
            this.mGetXMLFile.Text = "<< Download <<";
            this.mGetXMLFile.UseVisualStyleBackColor = true;
            this.mGetXMLFile.Click += new System.EventHandler(this.mGetXMLFile_Click);
            // 
            // mConnect
            // 
            this.mConnect.Location = new System.Drawing.Point(56, 21);
            this.mConnect.Name = "mConnect";
            this.mConnect.Size = new System.Drawing.Size(75, 23);
            this.mConnect.TabIndex = 29;
            this.mConnect.Text = "Connect";
            this.mConnect.UseVisualStyleBackColor = true;
            this.mConnect.Click += new System.EventHandler(this.mConnect_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.mFTPViewer);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(511, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(306, 310);
            this.panel4.TabIndex = 2;
            // 
            // mFTPViewer
            // 
            this.mFTPViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mFTPViewer.Location = new System.Drawing.Point(0, 0);
            this.mFTPViewer.Name = "mFTPViewer";
            this.mFTPViewer.ServerInfo = null;
            this.mFTPViewer.Size = new System.Drawing.Size(306, 310);
            this.mFTPViewer.TabIndex = 26;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label43);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(3, 325);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(820, 14);
            this.panel7.TabIndex = 1;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(3, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(45, 13);
            this.label43.TabIndex = 46;
            this.label43.Text = "Console";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.mValidationURL);
            this.panel8.Controls.Add(this.mValidate);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(3, 446);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(820, 40);
            this.panel8.TabIndex = 49;
            // 
            // mValidationURL
            // 
            this.mValidationURL.Location = new System.Drawing.Point(3, 5);
            this.mValidationURL.Name = "mValidationURL";
            this.mValidationURL.Size = new System.Drawing.Size(504, 20);
            this.mValidationURL.TabIndex = 41;
            this.mValidationURL.Leave += new System.EventHandler(this.mValidationURL_Leave);
            // 
            // mValidate
            // 
            this.mValidate.Location = new System.Drawing.Point(513, 3);
            this.mValidate.Name = "mValidate";
            this.mValidate.Size = new System.Drawing.Size(54, 23);
            this.mValidate.TabIndex = 43;
            this.mValidate.Text = "Validate";
            this.mValidate.UseVisualStyleBackColor = true;
            this.mValidate.Click += new System.EventHandler(this.mValidate_Click);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(3, 431);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(78, 12);
            this.label40.TabIndex = 42;
            this.label40.Text = "Validation URL";
            // 
            // mContextMenuFileCache
            // 
            this.mContextMenuFileCache.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFromListToolStripMenuItem,
            this.openInExplorerToolStripMenuItem});
            this.mContextMenuFileCache.Name = "mContextMenuFileCache";
            this.mContextMenuFileCache.Size = new System.Drawing.Size(170, 48);
            // 
            // removeFromListToolStripMenuItem
            // 
            this.removeFromListToolStripMenuItem.Name = "removeFromListToolStripMenuItem";
            this.removeFromListToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.removeFromListToolStripMenuItem.Text = "Remove From List";
            this.removeFromListToolStripMenuItem.Click += new System.EventHandler(this.removeFromListToolStripMenuItem_Click);
            // 
            // openInExplorerToolStripMenuItem
            // 
            this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
            this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.openInExplorerToolStripMenuItem.Text = "Open In Explorer";
            this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
            // 
            // showXMLToolStripMenuItem
            // 
            this.showXMLToolStripMenuItem.CheckOnClick = true;
            this.showXMLToolStripMenuItem.Name = "showXMLToolStripMenuItem";
            this.showXMLToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showXMLToolStripMenuItem.Text = "Show XML";
            this.showXMLToolStripMenuItem.Click += new System.EventHandler(this.showXMLToolStripMenuItem_Click);
            // 
            // xSLTransformToolStripMenuItem
            // 
            this.xSLTransformToolStripMenuItem.CheckOnClick = true;
            this.xSLTransformToolStripMenuItem.Name = "xSLTransformToolStripMenuItem";
            this.xSLTransformToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.xSLTransformToolStripMenuItem.Text = "Transform To HTML";
            this.xSLTransformToolStripMenuItem.Click += new System.EventHandler(this.xSLTransformToolStripMenuItem_Click);
            // 
            // mFtpDownloadWorker
            // 
            this.mFtpDownloadWorker.WorkerReportsProgress = true;
            this.mFtpDownloadWorker.WorkerSupportsCancellation = true;
            this.mFtpDownloadWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.mFtpDownloadWorker_DoWork);
            this.mFtpDownloadWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.mFtpDownloadWorker_ProgressChanged);
            this.mFtpDownloadWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.mFtpDownloadWorker_RunWorkerCompleted);
            // 
            // FeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 573);
            this.Controls.Add(this.splitContainer4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FeedForm";
            this.Text = "AnotherFeedBuilder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FeedForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FeedForm_FormClosed);
            this.Load += new System.EventHandler(this.FeedForm_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FeedForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.mErrorProvider)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer8)).EndInit();
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer9.Panel1.ResumeLayout(false);
            this.splitContainer9.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer9)).EndInit();
            this.splitContainer9.ResumeLayout(false);
            this.splitContainer10.Panel1.ResumeLayout(false);
            this.splitContainer10.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer10)).EndInit();
            this.splitContainer10.ResumeLayout(false);
            this.mFormTabs.ResumeLayout(false);
            this.mFeedTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFeedImagePictureBox)).EndInit();
            this.mItemsTab.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.mMP3GroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.mXMLTab.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.mXsltTab.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.mFTPTab.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFTPImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCancelUploadImage)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.mContextMenuFileCache.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        //private System.Windows.Forms.ToolStripMenuItem mFileMenutItem;
        //private System.Windows.Forms.ToolStripMenuItem loadXMLFileToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        //private System.Windows.Forms.ToolStripMenuItem newToolstripMenuItem;
        private System.ComponentModel.BackgroundWorker mFTPUploadWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolTip mToolTip;
        private System.Windows.Forms.TabPage mFTPTab;
        private System.Windows.Forms.PictureBox mCancelUploadImage;
        private System.Windows.Forms.ProgressBar mFtpStatusBar;
        private System.Windows.Forms.Button mPutXMLFile;
        private System.Windows.Forms.PictureBox mFTPImage;
        private System.Windows.Forms.Button mGetXMLFile;
        private System.Windows.Forms.Button mConnect;
        private FTPViewer mFTPViewer;
        private System.Windows.Forms.TabPage mItemsTab;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button mSearchLocalFiles;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button mDuplicateButton;
        private System.Windows.Forms.GroupBox mMP3GroupBox;
        private System.Windows.Forms.Label mPathLabel;
        private System.Windows.Forms.Button mPutMP3;
        private System.Windows.Forms.Button mGetMP3;
        private System.Windows.Forms.TextBox mMP3Path;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox mItemDuration;
        private System.Windows.Forms.DateTimePicker mItemPubDate;
        private System.Windows.Forms.Button mBrowseMP3Button;
        private System.Windows.Forms.TextBox mItemGUID;
        private System.Windows.Forms.Button mInsertButton;
        private System.Windows.Forms.Button mMoveDownButton;
        private System.Windows.Forms.Button mMoveUpButton;
        private System.Windows.Forms.Button mDeleteButton;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox mItemAuthor;
        private System.Windows.Forms.TextBox mItemSubtitle;
        private System.Windows.Forms.TextBox mItemSummary;
        private System.Windows.Forms.TextBox mItemDescription;
        private System.Windows.Forms.TextBox mItemLink;
        private System.Windows.Forms.TextBox mItemTitle;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabPage mFeedTab;
        private System.Windows.Forms.DateTimePicker mFeedLastBuildDate;
        private System.Windows.Forms.DateTimePicker mFeedPubDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox mFeedCategory;
        private System.Windows.Forms.TextBox mFeedSubCategory;
        private System.Windows.Forms.TextBox mFeedSubtitle;
        private System.Windows.Forms.TextBox mFeedSummary;
        private System.Windows.Forms.TextBox mFeedOwnerName;
        private System.Windows.Forms.TextBox mFeedOwnerEmail;
        private System.Windows.Forms.TextBox mFeedWebmaster;
        private System.Windows.Forms.TextBox mFeedAuthor;
        private System.Windows.Forms.TextBox mFeedDescription;
        private System.Windows.Forms.TextBox mFeedLink;
        private System.Windows.Forms.TextBox mFeedCopyright;
        private System.Windows.Forms.TextBox mFeedTitle;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl mFormTabs;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private ListboxRefresher mItemsList;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox mFeedLanguage;
        private System.Windows.Forms.ErrorProvider mErrorProvider;
        private System.Windows.Forms.Label label25;
        private MyCustomControls.InheritedCombo.MultiColumnComboBox mTimeZoneSelector;
        private System.Windows.Forms.TabPage mXMLTab;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button mResetXmlChanges;
        private System.Windows.Forms.Button mApplyXmlChanges;
        private NumberedTextBoxUC mXmlText;
        private System.Windows.Forms.TextBox mFindText;
        private System.Windows.Forms.Button mFindButton;
        private System.Windows.Forms.Label mUploadProgressText;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.CheckBox mItunesPodcast;
        private System.Windows.Forms.TabPage mXsltTab;
        private System.Windows.Forms.WebBrowser mTransform;
        private System.Windows.Forms.Button mTransformXml;
        //private System.Windows.Forms.ToolStripMenuItem advancedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xSLTransformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.Button mCopyXsltOut;
        private System.Windows.Forms.Button mFTPXsltOut;
        private System.Windows.Forms.Button mClearXsltOut;
        private System.Windows.Forms.TextBox mImageHeight;
        private System.Windows.Forms.TextBox mImageWidth;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox mImageFileSize;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox mImageLink;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox mImageURL;
        private System.Windows.Forms.TextBox mImageDescription;
        private System.Windows.Forms.TextBox mImageTitle;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button mCloseImage;
        private System.Windows.Forms.PictureBox mFeedImagePictureBox;
        private System.Windows.Forms.Button mOpenImage;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button mAboutImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox mEnclosureUrl;
        private System.Windows.Forms.Button mValidate;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox mValidationURL;
        private System.Windows.Forms.TextBox mLocalImagePath;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Button mRefreshImage;
        private NumberedTextBoxUC mXslt;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.ImageList imageList1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox nodeTextBox;
        private Aga.Controls.Tree.NodeControls.NodeCheckBox nodeCheckBox;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox mConsoleTextbox;
        private System.ComponentModel.BackgroundWorker mFtpDownloadWorker;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private System.Windows.Forms.ListBox mFileList;
        private System.Windows.Forms.SplitContainer splitContainer9;
        private System.Windows.Forms.TextBox mActiveFile;
        private System.Windows.Forms.SplitContainer splitContainer10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private LocalFileViewer mLocalFileViewer;
        private System.Windows.Forms.ContextMenuStrip mContextMenuFileCache;
        private System.Windows.Forms.ToolStripMenuItem removeFromListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
       
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;
using FeedBuilder.Properties;
//using IdSharp.Tagging.ID3v2;
//using IdSharp.Tagging.ID3v1;
//using IdSharp.AudioInfo;
using System.Threading;
using System.Collections;
using FeedBuilder.FTP;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Xsl;
using Aga.Controls.Tree;
using System.Collections.ObjectModel;
using System.Net.FtpClient;
using System.Net;
using System.Text.RegularExpressions;
//using SmartSolutions.Controls;

namespace FeedBuilder
{
    public partial class FeedForm : Form
    {
        private Bitmap mFeedImage;
        private FeedData mFeedData;
        private FeedItem mSelectedFeedItem;
        private bool mFTPConnected;
        private Dictionary<string, int> mTrimLocations;
        private const string TRIM_LOC_MP3PATH = "ITEMMP3PATH";
        private const string TRIM_LOC_LINK = "ITEMLINK";
        protected const string DATE_FORMAT = "MM/dd/yyyy HH:mm:ss";
        //private TreeModel mTreeModel;
        private FeedFileCache mFeedFilesCache;
        private const string FEED_FILE_CACHE_NAME = "OpenedFiles.txt";

        private const string FTP_TAB_FEED_FILE_TEXT = "Feed XML File";
        private const string FTP_TAB_FEED_IMAGE_TEXT = "Feed Image";
        private const string FTP_TAB_SOUND_FILES_TEXT = "Sound Files";
        private const string FTP_TAB_HTML_NODE_TEXT = "HTML Transform";
        private const NodeTypes FtpNodeCountTypes =  NodeTypes.ContentNode |
                            NodeTypes.HtmlNode |
                            NodeTypes.ImageNode |
                            NodeTypes.NonItunesItemNode |
                            NodeTypes.ItunesItemNode;
        private const NodeTypes FtpUploadNodeTypes = NodeTypes.ContentNode |
                    NodeTypes.HtmlNode |
                    NodeTypes.ImageNode |
                    NodeTypes.ItunesFeedNode |
                    NodeTypes.NonItunesFeedNode;
        private bool mRefreshing;
        
        private Bitmap[] mFTPAnimationImages = new Bitmap[]
        {   Resources.FTPAnimation1,
            Resources.FTPAnimation2,
            Resources.FTPAnimation3,
            Resources.FTPAnimation4,
            Resources.FTPAnimation5,
            Resources.FTPAnimation6,
            Resources.FTPAnimation7
        };

        #region Constructors

        /// <summary>
        /// Default FeedForm constructor.
        /// </summary>
        public FeedForm()
        {
            InitializeComponent();
            InitializeNew();
            this.DoubleBuffered = true;

            FtpTrace.AddListener(new TextboxFTPTraceListener(mConsoleTextbox));
            mLocalFileViewer.ExplorerFileSelected += new LocalFileViewer.ExplorerFileSelectionHandler(mLocalFileViewer_ExplorerFileSelected);
        }

        void mLocalFileViewer_ExplorerFileSelected(string pathToFile)
        {
            mPutXMLFile.Enabled = (mFTPConnected && pathToFile != null);
        }

        #endregion

        #region Private Methods

        private void InitializeNew()
        {

            mFeedPubDate.CustomFormat = DATE_FORMAT;
            mFeedLastBuildDate.CustomFormat = DATE_FORMAT;

            mFTPViewer.SelectLine += FTPViewerDownloadEnabled;
            mFTPViewer.SelectDownloads += DownloadFiles;
            mTrimLocations = new Dictionary<string, int>();
            mLocalFileViewer.NodeChecked += new LocalFileViewer.NodeCheckedHandler(mLocalFileViewer_NodeChecked);

            mFeedData = new FeedData();
            mFeedData.InhibitDirty(true);

            foreach (Control cntrl in mFormTabs.TabPages)
            {
                ClearControl(cntrl);
            }

            mFeedTitle.Tag = FeedData.TITLE;
            mFeedDescription.Tag = FeedData.DESCRIPTION;
            mFeedLink.Tag = FeedData.LINK;
            mFeedCopyright.Tag = FeedData.COPYWRITE;
            mFeedLastBuildDate.Tag = FeedData.LAST_BUILD_DATE;
            mFeedPubDate.Tag = FeedData.PUB_DATE;
            mFeedLanguage.Tag = FeedData.LANGUAGE;
            mFeedWebmaster.Tag = FeedData.WEBMASTER;
            mFeedAuthor.Tag = FeedData.AUTHOR;
            mFeedSubtitle.Tag = FeedData.SUBTITLE;
            mFeedSummary.Tag = FeedData.SUMMARY;
            mFeedOwnerName.Tag = FeedData.OWNER_NAME;
            mFeedOwnerEmail.Tag = FeedData.OWNER_EMAIL;
            mFeedCategory.Tag = FeedData.CATEGORY;
            mFeedSubCategory.Tag = FeedData.SUBCATEGORY;
            mImageTitle.Tag = FeedData.FEED_IMAGE_TITLE;
            mImageDescription.Tag = FeedData.FEED_IMAGE_DESC;
            mImageURL.Tag = FeedData.FEED_IMAGE_URL;
            mImageLink.Tag = FeedData.FEED_IMAGE_LINK;
            
            RefreshFileList();

            mFeedData.InhibitDirty(false);

        }

        private void mLocalFileViewer_NodeChecked(Node fileNode, CheckState checkState)
        {
            try
            {
                bool checkFound = false;
                foreach (Node node in mLocalFileViewer.AllNodesOfType(FtpNodeCountTypes))
                {
                    if (node.IsChecked && node.Tag != null && mFTPConnected)
                    {
                        FtpNodeTag tag = node.Tag as FtpNodeTag;
                        if (tag != null)
                        {
                            checkFound = true;
                            break;
                        }
                    }
                }
                mPutXMLFile.Enabled = checkFound;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// TODO:  Make this a little more intelligent.  That is, only clean when you've done some 
        /// exhaustive checking to make sure that a file has not simply moved.  Also, we need a way to 
        /// allow for multiple feed files with the same name.
        /// </summary>
        private void CleanLocalDataDir()
        {
            try
            {
                string appDataFolder = FeedData.GetLocalDataPath(null);
                foreach (string additionalDataPath in Directory.EnumerateFiles(appDataFolder))
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(additionalDataPath);
                        XmlNode node = doc.SelectSingleNode(FeedData.LOCAL_FEED_PATH_XPATH);
                        if (node != null)
                        {
                            string localFeedPath = node.InnerText;
                            if (!File.Exists(localFeedPath))
                            {
                                File.Delete(localFeedPath);
                                File.Delete(additionalDataPath);
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void AddXmlNamespacesTo(XmlNamespaceManager nsmgr, string xml)
        {
            XDocument y = XDocument.Parse(xml);
            XPathNavigator nav = y.CreateNavigator();
            nav.MoveToFollowing(XPathNodeType.Element);
            IDictionary<string, string> nameURLPairs = nav.GetNamespacesInScope(XmlNamespaceScope.All);
            foreach (string name in nameURLPairs.Keys)
            {
                nsmgr.AddNamespace(name, nameURLPairs[name]);
            }
        }

        private void ClearControl(Control cntrl)
        {
            if (cntrl as TextBox != null)
                cntrl.Text = null;
            if (cntrl as DateTimePicker != null)
                cntrl.Text = null;
            if (cntrl as ListBox != null)
                (cntrl as ListBox).Items.Clear();

            if (cntrl.Controls != null && cntrl.Controls.Count > 0)
            {
                foreach (Control ctl in cntrl.Controls)
                {
                    ClearControl(ctl);
                }
            }
        }

        private void FTPViewerDownloadEnabled(bool enabled)
        {
            mGetXMLFile.Enabled = enabled;
        }

        private void RefreshForm()
        {
            mRefreshing = true;
            mFeedData.InhibitDirty(true);
            RefreshValidationState();

            mItunesPodcast.Checked = mFeedData.ItunesFeed;

            mInsertButton.Enabled = true;
            mDeleteButton.Enabled = true;
            mDuplicateButton.Enabled = true;
            mSearchLocalFiles.Enabled = true;

            bool itunesEnabled = mFeedData.ItunesFeed;
            mFeedAuthor.Enabled = itunesEnabled;
            mFeedSubtitle.Enabled = itunesEnabled;
            mFeedSummary.Enabled = itunesEnabled;
            mFeedOwnerName.Enabled = itunesEnabled;
            mFeedOwnerEmail.Enabled = itunesEnabled;
            mFeedCategory.Enabled = itunesEnabled;
            mFeedSubCategory.Enabled = itunesEnabled;
            mItemSubtitle.Enabled = itunesEnabled;
            mItemSummary.Enabled = itunesEnabled;
            mItemDuration.Enabled = itunesEnabled;
            mItemAuthor.Enabled = itunesEnabled;
            
            //File List
            RefreshFileList();

            //Path to local feed file.
            //mXMLFeedPath.Text = mFeedData.FeedPath;

            //FEED IMAGE TAB
            mImageTitle.Text = mFeedData.ImageTitle;
            mLocalImagePath.Text = mFeedData.LocalImagePath;
            mImageDescription.Text = mFeedData.ImageDescription;
            mImageURL.Text = mFeedData.ImageURL;
            mImageLink.Text = mFeedData.ImageLink;
            if (mFeedData.LocalImagePath != null && File.Exists(mFeedData.LocalImagePath))
                LoadImage(mFeedData.LocalImagePath, false);
            else
                ClearImage(false);

            //FEED INFO TAB
            mFeedTitle.Text = mFeedData.Title;
            mFeedDescription.Text = mFeedData.Description;
            mFeedLink.Text = mFeedData.Link;
            mFeedCopyright.Text = mFeedData.Copywrite;
            
            if (mFeedData.LastBuildDateString != null)
            {
                mFeedLastBuildDate.CustomFormat = DATE_FORMAT;
                mFeedLastBuildDate.Value = mFeedData.LastBuildDate;
            }
            else
            {
                mFeedLastBuildDate.CustomFormat = " ";
            }
                
            if (mFeedData.PublicationDateString != null)
            {
                mFeedPubDate.CustomFormat = DATE_FORMAT;
                mFeedPubDate.Value = mFeedData.PublicationDate;
            }
            else
            {
                mFeedPubDate.CustomFormat = " ";
            }

            
            mFeedWebmaster.Text = mFeedData.Webmaster;
            mFeedAuthor.Text = mFeedData.Author;
            mFeedSubtitle.Text = mFeedData.Subtitle;
            mFeedSummary.Text = mFeedData.Summary;
            mFeedOwnerName.Text = mFeedData.OwnerName;
            mFeedOwnerEmail.Text = mFeedData.OwnerEmail;
            mFeedCategory.Text = mFeedData.Category;
            mFeedSubCategory.Text = mFeedData.SubCategory;
            if (mFeedLanguage.Items.Count == 0)
            {
                foreach (CodeNamePair pair in Language.Pairs)
                {
                    mFeedLanguage.Items.Add(pair);
                }
            }
            
            if (mFeedData.Language != null && mFeedData.Language != string.Empty)
            {
                mFeedLanguage.Text = Language.Name(mFeedData.Language);
            }
            mTimeZoneSelector.Text = mFeedData.TimeZoneAcronym;

            //FEED ITEMS TAB
            mItemsList.Items.Clear();
            if (mFeedData.FeedItems.Count > 0)
            {
                foreach (FeedItem item in mFeedData.FeedItems)
                {
                    mItemsList.Items.Add(item);
                }
            }

            //FTP Tab
            RefreshFTPTab();

            //XML Tab
            RefreshAfterChange();

            //XSLT Tab
            mXslt.Text = mFeedData.XSLT;
            mTransform.DocumentText = "<html><body></body></html>";

            if (mItemsList.Items.Count > 0)
                mItemsList.SelectedIndex = 0;

            mFeedData.InhibitDirty(false);
            mRefreshing = false;
        }

        private void RefreshFileList()
        {
            if (mFeedFilesCache == null)
            {
                string cachPath = FeedData.GetUserDataDirectory() + "\\" + FEED_FILE_CACHE_NAME;
                mFeedFilesCache = new FeedFileCache(cachPath);
                mFeedFilesCache.FilesChanged += RefreshFileList;
            }
            mFeedFilesCache.CheckLinks();
            mFeedFilesCache.Sort();

            //This may be getting called from another thread.  Specifically, this happens
            //on file system updates from our FileSystemWatchers in the FeedFileCache class.
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    mActiveFile.Text = string.Empty;
                    mFileList.Items.Clear();
                    foreach (FileCacheEntry entry in mFeedFilesCache.Entries)
                    {
                        mFileList.Items.Add(entry);
                        if (entry.IsActive)
                        {
                            mActiveFile.Text = entry.FilePath;
                        }
                    }

                    if (mActiveFile.Text == string.Empty || mFeedData.IsNew())
                    {
                        FileCacheEntry newFile = new FileCacheEntry("New File", null);
                        mActiveFile.Text = "New File";
                        mFeedFilesCache.ClearActiveSelectedEntry();
                        mFileList.ClearSelected();
                        mFileList.Items.Insert(0, newFile);
                        mFileList.SelectedIndex = 0;
                    }
                    mFileList.Invalidate();
                });
            }
            else
            {
                mFileList.Items.Clear();
                foreach (FileCacheEntry entry in mFeedFilesCache.Entries)
                {
                    mFileList.Items.Add(entry);
                    if (entry.IsActive)
                    {
                        mActiveFile.Text = entry.FilePath;
                    }
                }
                if (mActiveFile.Text == string.Empty || mFeedData.IsNew())
                {
                    FileCacheEntry newFile = new FileCacheEntry("New File", null);
                    mActiveFile.Text = "New File";
                    mFeedFilesCache.ClearActiveSelectedEntry();
                    mFileList.ClearSelected();
                    mFileList.Items.Insert(0, newFile);
                    mFileList.SelectedIndex = 0;
                }
                mFileList.Invalidate();
            }
        }

        /// <summary>
        /// Refreshes the xml and xslt text fields, since they reflect a duplicate of
        /// information in other fields.
        /// </summary>
        private void RefreshAfterChange()
        {
            mXmlText.Text = mFeedData.GetPrettyXmlText();
           // mXsltXml.Text = mXmlText.Text;

            mResetXmlChanges.Enabled = true;
            mApplyXmlChanges.Enabled = true;
            mFindText.Enabled = true;
            mFindButton.Enabled = true;
            //Dirty?
            if (mFeedData.Dirty)
            {
                //if (!mXMLFeedPath.Text.EndsWith("*"))
                //    mXMLFeedPath.Text = mXMLFeedPath.Text + "*";
            }
            RefreshFTPTab();
        }

        private void RefreshValidationState()
        {
            IDictionary<string, ValidationError> errors = mFeedData.Validate();

            if (errors != null && errors.Count > 0)
            {
                foreach (string errorTag in errors.Keys)
                {
                    foreach (Control cntrl in mFormTabs.TabPages)
                    {
                        SetError(cntrl, errorTag, errors[errorTag]);
                    }
                }
            }
        }

        private void SetError(Control cntrl, string errorTag, ValidationError error)
        {
            if (cntrl.Tag != null && cntrl.Tag.Equals(errorTag))
            {
                mErrorProvider.SetError(cntrl, error.MESSAGE);
            }
            if (cntrl.Controls != null && cntrl.Controls.Count > 0)
            {
                foreach (Control ctl in cntrl.Controls)
                {
                    SetError(ctl, errorTag, error);
                }
            }
        }

        private void RefreshFTPTab()
        {
            mLocalFileViewer.RefreshFileViewer(mFeedData);

            mValidationURL.Text = mFeedData.ValidationURL;
        }

        private void ClearImage(bool updateDB)
        {
            mFeedImagePictureBox.Image = null;
            mImageWidth.Text = null;
            mImageHeight.Text = null;
            mImageFileSize.Text = null;
            if (updateDB)
            {
                mFeedData.LocalImagePath = null;
            }
        }

        /// <summary>
        /// Method to upload file to FTP Server
        /// </summary>
        /// <param name="fileName">local source file name</param>
        /// <param name="uploadPath">Upload FTP path including Host name</param>
        /// <param name="ftpUser">FTP login username</param>
        /// <param name="ftpPass">FTP login password</param>
        public void UploadFile(string fileName, string uploadPath, string ftpUser, string ftpPass)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);

            // Create FtpWebRequest object from the Uri provided
            System.Net.FtpWebRequest ftpWebRequest =
                (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(new Uri(uploadPath));

            // Provide the WebPermission Credintials
            ftpWebRequest.Credentials = new System.Net.NetworkCredential(ftpUser, ftpPass);

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            //ftpWebRequest.KeepAlive = false;

            // set timeout for 20 seconds
            ftpWebRequest.Timeout = 20000;

            // Specify the command to be executed.
            ftpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            ftpWebRequest.UseBinary = true;

            // Notify the server about the size of the uploaded file
            ftpWebRequest.ContentLength = fileInfo.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];

            try
            {
                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                using (System.IO.FileStream fileStream = fileInfo.OpenRead())
                {
                    // Stream to which the file to be upload is written
                    using (System.IO.Stream stream = ftpWebRequest.GetRequestStream())
                    {
                        // Read from the file stream 2kb at a time
                        int contentLen = fileStream.Read(buff, 0, buffLength);

                        // Till Stream content ends
                        while (contentLen != 0)
                        {
                            // Write Content from the file stream to the FTP Upload Stream
                            stream.Write(buff, 0, contentLen);
                            contentLen = fileStream.Read(buff, 0, buffLength);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFeedWithDialog()
        {
            OpenFileDialog fileCooser = new OpenFileDialog();
            fileCooser.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            fileCooser.FilterIndex = 1;
            fileCooser.RestoreDirectory = true;

            if (fileCooser.ShowDialog() == DialogResult.OK)
            {
                LoadFeedNoDialog(fileCooser.FileName);
            }
        }

        private void LoadFeedNoDialog(string filePath)
        {
            mFeedData = new FeedData(filePath);
            if (!mFeedFilesCache.Exists(filePath))
            {
                mFeedFilesCache.AddFile(filePath);
            }

            mFeedFilesCache.SetActiveFile(filePath);
            mFeedFilesCache.Sort();
            mFeedFilesCache.Save();

            RefreshForm();
        }

        private void SaveAs()
        {
            SaveFileDialog fileCooser = new SaveFileDialog();
            fileCooser.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            fileCooser.FilterIndex = 1;
            fileCooser.RestoreDirectory = true;
            fileCooser.FileName = mFeedData.FeedFileName;

            if (fileCooser.ShowDialog() == DialogResult.OK)
            {
                //Loose focus on the currently selected control to apply the change.
                Control currentActive = this.ActiveControl;
                this.SelectNextControl(currentActive, true, true, true, true);

                mFeedData.SaveAs(fileCooser.FileName);
                if (!mFeedFilesCache.Exists(fileCooser.FileName))
                {
                    mFeedFilesCache.AddFile(fileCooser.FileName);
                    mFeedFilesCache.Save();
                }
                this.ActiveControl = currentActive;
            }
            RefreshForm();
        }

        #endregion

        #region Feed Items Tab Events

        private void mItemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox list = sender as ListBox;
            mSelectedFeedItem = list.SelectedItem as FeedItem;

            if (mSelectedFeedItem != null)
            {
                ShowNoad(mSelectedFeedItem);                
            }
        }

        private void ShowNoad(FeedItem selectedNode)
        {
            try
            {
                mRefreshing = true;

                //Retag Nodes
                XmlNode itemNode = selectedNode.Node;
                XmlNamespaceManager nsmgr = mFeedData.NamespaceManager;
                mItemTitle.Tag = FeedValidator.BuildItemTag(
                    itemNode.SelectSingleNode(FeedItem.TITLE_XPATH, nsmgr), nsmgr);
                mItemDescription.Tag =  FeedValidator.BuildItemTag(
                    itemNode.SelectSingleNode(FeedItem.DESC_XPATH, nsmgr), nsmgr);
                if (mFeedData.ItunesFeed)
                {
                    mItemSubtitle.Tag =  FeedValidator.BuildItemTag(
                        itemNode.SelectSingleNode(FeedItem.SUBTITLE_XPATH, nsmgr), nsmgr);
                    mItemSummary.Tag =  FeedValidator.BuildItemTag(
                        itemNode.SelectSingleNode(FeedItem.SUMMARY_XPATH, nsmgr), nsmgr);
                    mItemAuthor.Tag =  FeedValidator.BuildItemTag(
                        itemNode.SelectSingleNode(FeedItem.AUTHOR_XPATH, nsmgr), nsmgr);
                }
                mItemPubDate.Tag =  FeedValidator.BuildItemTag(
                    itemNode.SelectSingleNode(FeedItem.PUB_DATE_XPATH, nsmgr), nsmgr);
                mItemLink.Tag = FeedValidator.BuildItemTag(
                    itemNode.SelectSingleNode(FeedItem.LINK_XPATH, nsmgr), nsmgr);
                
                RefreshValidationState();

                mItemTitle.Text = selectedNode.Title;
                mItemDescription.Text = selectedNode.Description;
                mItemLink.Text = selectedNode.Link;
                mEnclosureUrl.Text = selectedNode.EnclosureURL;

                if (selectedNode.HasPubDate())
                {
                    mItemPubDate.CustomFormat = DATE_FORMAT;
                    mItemPubDate.Checked = true;
                    mItemPubDate.Value = selectedNode.PubDate;
                }
                else
                {
                    mItemPubDate.CustomFormat = " ";
                }

                if (mFeedData.ItunesFeed)
                {
                    mItemSubtitle.Text = selectedNode.SubTitle;
                    mItemSummary.Text = selectedNode.Summary;
                    mItemDuration.Text = selectedNode.DurationString;
                    mItemAuthor.Text = selectedNode.Author;
                }

                mMP3Path.Text = selectedNode.EnclosurePath;
                if (mMP3Path.Text == null  || mMP3Path.Text == "")
                    mMP3Path.Text = "{unknown}";

                mItemGUID.Text = selectedNode.GUID;

                mBrowseMP3Button.Enabled = true;

                mRefreshing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mMoveUpButton_Click(object sender, EventArgs e)
        {
            int index = mItemsList.SelectedIndex;
            if (index > 0)
            {
                object selectedItem = mItemsList.SelectedItem;
                object upItem = mItemsList.Items[index - 1];
                mItemsList.ClearSelected();
                mItemsList.Items[index - 1] = selectedItem;
                mItemsList.Items[index] = upItem;
                mItemsList.SelectedIndex = index - 1;
                mItemsList.Refresh();

                //XmlNode node = (selectedItem as FeedItem).Node;
                //XmlNode channel = mXMLFeed.SelectSingleNode(CHANNEL);
                //channel.RemoveChild(node);
                //channel.InsertBefore(node, channel.SelectSingleNode(ITEMS + "[" + (index - 1) + "]"));
            }
        }

        private void mMoveDownButton_Click(object sender, EventArgs e)
        {
            int index = mItemsList.SelectedIndex;
            if (index < mItemsList.Items.Count - 1)
            {
                object selectedItem = mItemsList.SelectedItem;
                object downItem = mItemsList.Items[index + 1];
                mItemsList.ClearSelected();
                mItemsList.Items[index + 1] = selectedItem;
                mItemsList.Items[index] = downItem;
                mItemsList.SelectedIndex = index + 1;
                mItemsList.Refresh();

                //XmlNode node = (selectedItem as FeedItem).Node;
                //XmlNode channel = mXMLFeed.SelectSingleNode(CHANNEL);
                //channel.RemoveChild(node);
                //channel.InsertBefore(node, channel.SelectSingleNode(ITEMS + "[" + (index + 1) + "]"));
            }
        }

        private void mDeleteButton_Click(object sender, EventArgs e)
        {
            int currentLocation = mItemsList.SelectedIndex;
            foreach (object item in mItemsList.SelectedItems)
            {
                FeedItem deleteItem = item as FeedItem;
                mFeedData.RemoveFeedItem(deleteItem);
                mFeedData.Dirty = true;
            }
            RefreshForm();
            if (currentLocation > mItemsList.Items.Count)
            {
                currentLocation = mItemsList.Items.Count;
            }
            else if (currentLocation > 0)
            {
                --currentLocation;
            }
            mItemsList.ClearSelected();
            if (mItemsList.Items != null && mItemsList.Items.Count > 0)
                mItemsList.SetSelected(currentLocation, true);
        }

        private void mInsertButton_Click(object sender, EventArgs e)
        {
            FeedItem item = null;
            if (mItemsList.SelectedItem != null)
            {
                int insertLocation = mItemsList.SelectedIndex;
                item = mFeedData.CreateNewFeedItem();

                mFeedData.InsertFeedItem(insertLocation, item);
                RefreshForm();
                mItemsList.SetSelected(insertLocation, true);
            }
            else
            {
                item = mFeedData.CreateNewFeedItem();
                mFeedData.InsertFeedItem(0, item);
                RefreshForm();
                mItemsList.SetSelected(0, true);
            }
        }

        private void mItemEnclosure_Click(object sender, EventArgs e)
        {
            const int arrowWidth = 20;
            int mouseX = (e as MouseEventArgs).X;

            if ((sender as ComboBox).Size.Width - mouseX <= arrowWidth)
            { }
        }

        private void mBrowseMP3Button_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileChooser = new OpenFileDialog();
                fileChooser.Filter = "MP3 files (*.mp3)|*.mp3|" +
                    "M4A files (*.m4a)|*.m4a|" +
                    "M4V files (*.m4v)|*.m4v|" +
                    "MOV files (*.mov)|*.mov|" +
                    "PDF files (*.pdf)|*.pdf|" +
                    "EPUB files (*.epub)|*.epub|" +
                    "All files (*.*)|*.*";
                fileChooser.FilterIndex = 1;
                fileChooser.RestoreDirectory = true;

                if (fileChooser.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(fileChooser.FileName))
                    {
                        FeedItem currentItem = mSelectedFeedItem;

                        //Filelength
                        //IAudioFile mp3 = AudioFile.Create(fileChooser.FileName, true);
                        int fileSize = (int)(new FileInfo(fileChooser.FileName)).Length;
                        currentItem.EnclosureLength = fileSize;

                        //Duration
                        //int fileTime = (int)mp3.TotalSeconds;
                        TimeSpan ts = SoundInfo.GetMediaDuration(fileChooser.FileName);
                        currentItem.Duration = ts;
                        string duration = string.Format("{0}:{1}:{2}", ts.Hours.ToString("D2"), ts.Minutes.ToString("D2"),
                            ts.Seconds.ToString("D2"));
                        mItemDuration.Text = duration;

                        //GUID
                        string currentGUID = mItemGUID.Text;
                        string newGUID = Guid.NewGuid().ToString();
                        mItemGUID.Text = newGUID;
                        currentItem.GUID = newGUID;

                        //Filename
                        currentItem.EnclosurePath = fileChooser.FileName;
                        mMP3Path.Text = fileChooser.FileName;

                        mItemPubDate.Enabled = true;
                    }
                }

                RefreshFTPTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ":" + System.Environment.NewLine +
                    ex.StackTrace);
            }
        }

        private void mDuplicateButton_Click(object sender, EventArgs e)
        {
            FeedItem selectedItem = mItemsList.SelectedItem as FeedItem;
            if (selectedItem != null)
            {
                int insertLocation = mItemsList.SelectedIndex; 
                FeedItem clonedItem = selectedItem.Clone();
                mFeedData.InsertFeedItem(insertLocation, clonedItem);

                RefreshForm();
                mItemsList.SelectedIndex = insertLocation;
            }
        }

        private void mItemTitle_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                {
                    mSelectedFeedItem.Title = mItemTitle.Text;
                    int index = mItemsList.SelectedIndex;
                    mItemsList.RefreshItem(index);
                }
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                    mSelectedFeedItem.Description = mItemDescription.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemSubtitle_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                    mSelectedFeedItem.SubTitle = mItemSubtitle.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemSummary_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                    mSelectedFeedItem.Summary = mItemSummary.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemAuthor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                    mSelectedFeedItem.Author = mItemAuthor.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemPubDate_ValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (mRefreshing)
                    return;

                if (mSelectedFeedItem != null)
                {
                    mItemPubDate.CustomFormat = DATE_FORMAT;
                    
                    mSelectedFeedItem.PubDate = mItemPubDate.Value;
                }
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mTimeZoneSelector_AfterSelectEvent()
        {
            mFeedData.TimeZoneOffset = mTimeZoneSelector.DataValue;
            mFeedData.TimeZoneAcronym = mTimeZoneSelector.Text;
        }

        private void mItemDuration_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (mRefreshing)
                    return;

                if (mSelectedFeedItem != null && mItemDuration.Text != null && mItemDuration.Text != string.Empty)
                {
                    string[] split = mItemDuration.Text.Split(':');
                    int timeValue = 0;
                    for (int i = split.Length - 1; i >= 0; i--)
                    {
                        if (int.TryParse(split[i], out timeValue))
                        {
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Character \"{0}\" is not a numeric type.", split[i]));
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
                e.Cancel = true;
            }
        }

        private void mItemDuration_Validated(object sender, EventArgs e)
        {
            try
            {
                if (mRefreshing)
                    return;

                if (mSelectedFeedItem != null && mItemDuration.Text != null && mItemDuration.Text != string.Empty)
                {
                    string[] split = mItemDuration.Text.Split(':');
                    int[] hrsMinSec = new int[] { 0, 0, 0 };
                    int timeIndexer = 2;
                    int timeValue = 0;
                    for (int i = split.Length - 1; i >= 0; i--)
                    {
                        if (int.TryParse(split[i], out timeValue))
                        {
                            hrsMinSec[timeIndexer--] = timeValue;
                        }
                    }
                    mSelectedFeedItem.Duration = new TimeSpan(hrsMinSec[0], hrsMinSec[1], hrsMinSec[2]);
                }
                mItemDuration.Text = mSelectedFeedItem.DurationString;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mItemDuration_Validate(object sender, EventArgs e)
        {
            
        }

        private void mItemLink_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFeedItem != null)
                    mSelectedFeedItem.Link = mItemLink.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedLastBuildDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (mRefreshing)
                    return;

                if (mFeedData != null)
                    mFeedData.LastBuildDate = mFeedLastBuildDate.Value;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedPubDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (mRefreshing)
                    return;

                if (mFeedData != null)
                    mFeedData.PublicationDate = mFeedPubDate.Value;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeNamePair pair = mFeedLanguage.SelectedItem as CodeNamePair;
            mFeedLanguage.Text = pair.NAME;
            mFeedData.Language = pair.CODE;
            RefreshAfterChange();
        }

        private void mEnclosureUrl_Leave(object sender, EventArgs e)
        {
            mSelectedFeedItem.EnclosureURL = mEnclosureUrl.Text;
            RefreshAfterChange();
        }
        #endregion

        #region Tool Strip Menu Item Events

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeNew();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadFeedWithDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAs();

                if (!mFeedFilesCache.Exists(mFeedData.FeedPath))
                    mFeedFilesCache.AddFile(mFeedData.FeedPath);
                mFeedFilesCache.SetActiveFile(mFeedData.FeedPath);
                mFeedFilesCache.Sort();
                RefreshFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                if (mFeedData.FeedPath == null || mFeedData.IsNew())
                    SaveAs();
                else
                {
                    mFeedData.Save();
                }

                if (!mFeedFilesCache.Exists(mFeedData.FeedPath))
                    mFeedFilesCache.AddFile(mFeedData.FeedPath);
                mFeedFilesCache.SetActiveFile(mFeedData.FeedPath);
                mFeedFilesCache.Sort();
                RefreshFileList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ":" + System.Environment.NewLine +
                    ex.StackTrace);
            }
        }

        #endregion

        #region FTP Tab Events

        private bool CanConnect(string server, string user, string password)
        {
            bool connected = false;
            using (FtpClient conn = new FtpClient())
            {
                conn.Host = server;
                conn.Credentials = new NetworkCredential(user, password);
                conn.Connect();
                connected = true;
            }
            return connected;
        }

        private void WriteToConsole(string text, bool clear)
        {
            if (clear)
                mConsoleTextbox.Text = "";

            mConsoleTextbox.Text = mConsoleTextbox.Text + System.Environment.NewLine;
        }

        private void mConnect_Click(object sender, EventArgs e)
        {
            mFTPConnected = false;
            try
            {
                FTPConnectForm connectForm = new FTPConnectForm();
                connectForm.ShowDialog(this);

                if (connectForm.IsCancelled())
                {
                    WriteToConsole("Connection cancelled...", false);
                    return;
                }
                if (!connectForm.IsComplete())
                {
                    WriteToConsole("Connection parameters incomplete...", false);
                    return;
                }

                FTPServerInfo serverInfo = connectForm.ServerInfo;

                //Get the FTPClient with a trace that writes to the console textbox.  Test the connection now.
                if (serverInfo != null && 
                    serverInfo.Server != null && 
                    serverInfo.User != null && 
                    serverInfo.Password != null)
                {
                    try
                    {
                        mFTPViewer.ShowFTPConnection(serverInfo);

                        if (mFeedData != null && mFeedData.FeedPath != null)
                        {
                            if (mLocalFileViewer.ExplorerMode)
                            {
                                mPutXMLFile.Enabled = (mLocalFileViewer.SelectedExplorerPath != null);
                            }
                            else
                            {
                                mPutXMLFile.Enabled = (mLocalFileViewer.CheckedNodesOfType(FtpNodeCountTypes).
                                    Count > 0);
                            }
                        }
                        mFTPConnected = true;
                    }
                    catch (Exception ex)
                    {
                        mFTPImage.Image = Resources.FTPDisconnected;
                        mFTPConnected = false;
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                mFTPImage.Image = Resources.FTPDisconnected;
                mFTPConnected = false;
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                if (mFTPConnected)
                    mFTPImage.Image = Resources.FTPConnected;
            }
        }

        private List<string> GetUploadFiles()
        {
            List<string> uploadFilePaths = new List<string>();
            //Look at primary xml file.
            Node feedFileNode = mLocalFileViewer.TreeModel.Nodes[0].Nodes[0];
            if (feedFileNode.CheckState == CheckState.Checked)
            {
                uploadFilePaths.Add(feedFileNode.Tag as string);
            }
            else if (GetCollectiveChildrenState(feedFileNode) == CheckState.Indeterminate)
            {
                
            }
            return uploadFilePaths;
        }

        private void mPutXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                mFTPUploadWorker.RunWorkerAsync(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine +
                    ex.StackTrace);
            }
        }

        private void DownloadFiles(List<FtpListItem> selectedLines)
        {
            if (selectedLines != null && selectedLines.Count > 0)
            {
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                string downloadFolder = null;
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    downloadFolder = folderBrowser.SelectedPath;
                }
                else
                {
                    return;
                }
                List<object> arguments = new List<object> { downloadFolder, selectedLines };
                mFtpDownloadWorker.RunWorkerAsync(arguments);
            }
        }

        private void mGetXMLFile_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadFiles(mFTPViewer.SelectedLines);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting file." + System.Environment.NewLine +
                    ex.StackTrace);
            }
        }

        #endregion

        #region FeedInfo Tab Events

        private void mFileList_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                if (e.Index < 0)
                    return;

                ListBox lb = (ListBox)sender;
                Color bgColor = Color.White;
                Color textColor = Color.Black;

                FileCacheEntry entry = lb.Items[e.Index] as FileCacheEntry;
                if (entry != null)
                {
                    if (entry.IsUnsavedNewFile())
                    {
                        textColor = Color.White;
                        bgColor = Color.Blue;
                    }
                    else if (!entry.FileExists)
                    {
                        bgColor = Color.Red;
                        textColor = Color.White;
                    }
                    else if (entry.Selected || entry.IsActive)
                    {
                        textColor = Color.White;
                        bgColor = Color.Blue;
                    }
                }
                e.DrawBackground();

                Graphics g = e.Graphics;
                g.FillRectangle(new SolidBrush(bgColor), e.Bounds);
                g.DrawString(lb.Items[e.Index].ToString(), e.Font, new SolidBrush(textColor), new PointF(e.Bounds.X, e.Bounds.Y));

                e.DrawFocusRectangle();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private bool mDoubleClicked;
        private void mFileList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                mDoubleClicked = true;
                FileCacheEntry entry = mFileList.Items[mFileList.SelectedIndex] as FileCacheEntry;
                if (entry.FilePath == mFeedData.FeedPath)
                    return;

                if (mFeedData.Dirty)
                {
                    DialogResult yesNo = MessageBox.Show("Your file is not saved.  Save now before opening new file?",
                        "Save Now?", MessageBoxButtons.YesNo);
                    if (yesNo == DialogResult.Yes)
                    {
                        Save();
                    }
                }

                if (entry != null && !entry.IsActive)
                {
                    string filePath = entry.FilePath;
                    if (File.Exists(filePath))
                    {
                        mFeedFilesCache.Select(entry);
                        LoadFeedNoDialog(filePath);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Error, could not find file '{0}'.", filePath));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFileList_MouseHover(object sender, EventArgs e)
        {
           // mToolTip.RemoveAll();
        }

        private FileCacheEntry mSelectedFileCacheEntry;
        private void mFileList_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (mDoubleClicked)
                {
                    mDoubleClicked = false;
                    return;
                }
                Point point = new Point(e.X, e.Y);

                int index = mFileList.IndexFromPoint(point);

                if (index < 0) return;

                FileCacheEntry entry = mFileList.Items[index] as FileCacheEntry;
                mFeedFilesCache.Select(entry);
                mSelectedFileCacheEntry = entry;
                mFileList.Invalidate();

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    mContextMenuFileCache.Show(mFileList, point);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }


        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFileCacheEntry != null)
                {
                    mFeedFilesCache.Remove(mSelectedFileCacheEntry.FilePath);
                    object removeItem = null;
                    foreach (object listItem in mFileList.Items)
                    {
                        if (listItem == mSelectedFileCacheEntry)
                        {
                            removeItem = listItem;
                        }
                    }
                    if (removeItem != null)
                    {
                        mFileList.Items.Remove(removeItem);
                        mFeedFilesCache.Save();
                    }
                    mFileList.Invalidate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (mSelectedFileCacheEntry != null)
                {
                    OpenFileDialog fileChooser = new OpenFileDialog();
                    fileChooser.FilterIndex = 1;
                    fileChooser.InitialDirectory = mSelectedFileCacheEntry.Directory;
                    fileChooser.FileName = mSelectedFileCacheEntry.FileName;
                    fileChooser.Title = "Containing Folder";
                    fileChooser.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mOpenFileDir_Click(object sender, EventArgs e)
        {
            try
            {
                string file = mFeedData.FeedPath;
                if (file != null && file.Length > 0)
                {
                    string[] splits = file.Split('/', '\\');
                    string fileName = splits[splits.Length - 1];
                    string dir = string.Join("\\", splits, 0, splits.Length - 1);
                    OpenFileDialog fileChooser = new OpenFileDialog();
                    fileChooser.FilterIndex = 1;
                    fileChooser.InitialDirectory = dir;
                    fileChooser.FileName = fileName;
                    fileChooser.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedTitle_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Title = mFeedTitle.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Description = mFeedDescription.Text;

                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedLink_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Link = mFeedLink.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedCopyright_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Copywrite = mFeedCopyright.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedWebmaster_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Webmaster = mFeedWebmaster.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedAuthor_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Author = mFeedAuthor.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedSubtitle_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Subtitle = mFeedSubtitle.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedSummary_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Summary = mFeedSummary.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedOwnerName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.OwnerName = mFeedOwnerName.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedOwnerEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.OwnerEmail = mFeedOwnerEmail.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.Category = mFeedCategory.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedSubCategory_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.SubCategory = mFeedSubCategory.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mSearchLocalFiles_Click(object sender, EventArgs e)
        {
            try
            {
                //Let them choose a starting point to search for files.
                FolderBrowserDialog folderChooser = new FolderBrowserDialog();

                if (folderChooser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = folderChooser.SelectedPath;
                    FindSoundFiles(folderPath);
                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void FindSoundFiles(string folderPath)
        {
            if (mFeedData != null)
            {
                foreach (FeedItem item in mFeedData.FeedItems)
                {
                    string url = item.EnclosureURL;
                    if (url != null && url != string.Empty)
                    {
                        //trim off the filename
                        string trimmedUrl = url.Remove(url.LastIndexOf('/'));
                        string[] splits = trimmedUrl.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        string fileName = url.Substring(url.LastIndexOf('/') + 1);
                        string soundFilePath = null;
                        string dirpath = FindDirectoryPath(folderPath, splits, 0);
                        if (dirpath != null)
                        {
                            soundFilePath = dirpath + "\\" + fileName;
                            if (!File.Exists(soundFilePath))
                            {
                                soundFilePath = null;  
                            }
                        }

                        //TODO:  What to do when someone like dad created the url before establishing the 
                        //local dir?  Code below will find 20.mp3 in the first searched directory.
                        //Can't think of a way to resolve this...
                        if (soundFilePath == null)
                        {
                            soundFilePath = LocateSoundFile(folderPath, fileName);
                        }
                        item.EnclosurePath = soundFilePath;
                    }
                }
                RefreshForm();
            }
        }

        private string LocateSoundFile(string folderPath, string linkFileName)
        {
            string filePath = folderPath + "\\" + linkFileName;
            if (File.Exists(filePath))
                return filePath;
            else
            {
                foreach (string dirPath in Directory.EnumerateDirectories(folderPath))
                {
                    if ((filePath = LocateSoundFile(dirPath, linkFileName)) != null)
                        return filePath;
                }
            }
            return null;
        }

        //First run a directory search on the search path.  If found, then try to find the file in that
        //directory.
        private string FindDirectoryPath(string folderPath, string[] searchPath, int searchPathDepth)
        {
            for (; searchPathDepth < searchPath.Length; searchPathDepth++)
            {

                string thisFolder = searchPath[searchPathDepth];
                

                foreach (string dirPath in Directory.EnumerateDirectories(folderPath))
                {
                    if (dirPath.EndsWith("\\" + thisFolder))
                    {
                        if (searchPathDepth == searchPath.Length - 1)
                        {
                            return dirPath;
                        }
                        else
                        {
                            return FindDirectoryPath(dirPath, searchPath, searchPathDepth + 1);
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Simply prevents control key events from doing anything to the itemsList selection state,
        /// unless it is a control-arrowkey event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mItemsList_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Control)
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

        #region Form Events

        private void FeedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                (mFeedFilesCache as IDisposable).Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void FeedForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (mFeedData.Dirty)
                {
                    DialogResult result = MessageBox.Show("File is not saved.  Save now?", "Save Now?", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.OK)
                    {
                        mFeedData.Save();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void FeedForm_Load(object sender, EventArgs e)
        {
            try
            {
                //this.mFormTabs.Controls.Remove(this.mXsltTab);
                //this.mFormTabs.Controls.Remove(this.mXMLTab);

                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Country", typeof(string));
                dt.Columns.Add("Offset", typeof(string));
                dt.Columns.Add("OffsetValue", typeof(double));

                using (StringReader reader = new StringReader(Resources.TimeZones))
                {
                    string line;
                    string[] fields;
                    double offsetValue;
                    while ((line = reader.ReadLine()) != null)
                    {
                        fields = line.Split('|');
                        offsetValue = double.Parse(fields[4]);
                        dt.Rows.Add(fields[0], fields[1], fields[2], fields[3], offsetValue);
                    }
                }
                mTimeZoneSelector.Table = dt;
                mTimeZoneSelector.ColumnsToDisplay = new string[] { "Code", "Name", "Country", "Offset" };
                mTimeZoneSelector.DisplayMember = "Code";
                mTimeZoneSelector.DataMember = "Offset";
                mFeedData.InhibitDirty(true);
                RefreshForm();
                mFeedData.InhibitDirty(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void FeedForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int charPressed = (int)e.KeyChar;
                switch (charPressed)
                {
                    //Cntrl-n : New
                    case 14:
                        InitializeNew();
                        break;

                    //Cntrl-s : Save
                    case 19:
                        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift || 
                            mFeedData.FeedPath == null || mFeedData.IsNew())
                        {
                            SaveAs();
                        }
                        else
                        {
                            Control currentActive = this.ActiveControl;
                            this.SelectNextControl(currentActive, true, true, true, true);
                            mFeedData.Save();
                            this.ActiveControl = currentActive;
                        }
                        break;

                    //Cntrl-o : Open
                    case 15:
                        LoadFeedWithDialog();
                        break;

                    case 17:
                        this.Close();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

        #region Image Tab Events

        private void mLocalImagePath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mLocalImagePath.Text = mFeedData.LocalImagePath;
                else
                    mLocalImagePath.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mAboutImage_Click(object sender, EventArgs e)
        {
            try
            {
                AboutImageForm about = new AboutImageForm();
                about.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mImageTitle_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.ImageTitle = mImageTitle.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mImageDescription_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.ImageDescription = mImageDescription.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mImageURL_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.ImageURL = mImageURL.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mImageLink_Leave(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                    mFeedData.ImageLink = mImageLink.Text;
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFeedImagePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                LoadImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ":" + System.Environment.NewLine +
                    ex.StackTrace);
            }
        }

        private void mOpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                LoadImage();
                RefreshAfterChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mRefreshImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData.LocalFeedPath != null && mFeedData.LocalFeedPath != string.Empty)
                    LoadImage(mFeedData.LocalImagePath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mCloseImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFeedImage != null)
                {
                    if (MessageBox.Show("Are you sure you want to remove this image?", "Remove Image",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        mFeedImage = null;
                        mFeedImagePictureBox.Image = null;
                        mImageTitle.Text = null;
                        mImageDescription.Text = null;
                        mImageURL.Text = null;
                        mImageLink.Text = null;
                        mImageFileSize.Text = null;
                        mImageHeight.Text = null;
                        mImageWidth.Text = null;

                        mFeedData.LocalImagePath = null;
                        mFeedData.ImageDescription = null;
                        mFeedData.ImageLink = null;
                        mFeedData.ImageTitle = null;
                        mFeedData.ImageURL = null;
                        RefreshAfterChange();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }


        private void LoadImage()
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "png files (*.png)|*.png|jpg files(*.jpg)|*.jpg|All files (*.*)|*.*";
            fileChooser.FilterIndex = 1;
            fileChooser.RestoreDirectory = true;

            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                string filePath = null;
                if ((filePath = fileChooser.FileName) != null)
                {
                    LoadImage(filePath, true);
                }
            }
        }

        private void LoadImage(string imageLocalPath, bool updateDB)
        {
            using (FileStream fs = new FileStream(imageLocalPath, FileMode.Open))
            {
                mFeedImage = CopyImageFromStream(fs);
            }
            mFeedImagePictureBox.Image = mFeedImage;
            mImageWidth.Text = mFeedImage.Size.Width.ToString();
            mImageHeight.Text = mFeedImage.Size.Height.ToString();
            FileInfo fi = new FileInfo(imageLocalPath);
            string fileDir = fi.DirectoryName;
            mImageFileSize.Text = fi.Length.ToString();
            if (updateDB)
            {
                mFeedData.LocalImagePath = imageLocalPath;
            }
            string imgFileExt = "*" + fi.Extension;
            //CreateFileWatcher(fileDir, imgFileExt);
            mLocalImagePath.Text = imageLocalPath;
        }

        static Bitmap CopyImageFromStream(Stream stream)
        {
            Bitmap retval = null;

            using (Bitmap b = new Bitmap(stream))
            {
                retval = new Bitmap(b.Width, b.Height, b.PixelFormat);
                using (Graphics g = Graphics.FromImage(retval))
                {
                    g.DrawImage(b, Point.Empty);
                    g.Flush();
                }
            }

            return retval;
        }

        FileSystemWatcher mWatcher = null;
        public void CreateFileWatcher(string directoryPath, string fileFilter)
        {
            if (mWatcher != null)
            {
                mWatcher.Dispose();
            }

            // Create a new FileSystemWatcher and set its properties.
            mWatcher = new System.IO.FileSystemWatcher();
            mWatcher.Path = directoryPath;
            /* Watch for changes in LastAccess and LastWrite times, and 
                the renaming of files or directories. */
            mWatcher.NotifyFilter = NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            mWatcher.Filter = fileFilter;

            // Add event handlers.
            mWatcher.Changed += new FileSystemEventHandler(OnImgFileChanged);
            mWatcher.Created += new FileSystemEventHandler(OnImgFileChanged);
            mWatcher.Deleted += new FileSystemEventHandler(OnImgFileChanged);
            mWatcher.Renamed += new RenamedEventHandler(OnImgFileRenamed);

            // Begin watching.
            mWatcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private void OnImgFileChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                if (!InvokeRequired)
                {
                    LoadImage(mFeedData.LocalImagePath, false);
                    RefreshAfterChange();
                }
                else
                {
                    Invoke(new Action<object, FileSystemEventArgs>(OnImgFileChanged), source, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void OnImgFileRenamed(object source, RenamedEventArgs e)
        {
            // Specify what is done when a file is renamed.
            //MessageBox.Show(string.Format("File: {0} renamed to {1}", e.OldFullPath, e.FullPath));
            try
            {
                if (!InvokeRequired)
                {
                    LoadImage(e.FullPath, true);
                    RefreshAfterChange();
                }
                else
                {
                    Invoke(new Action<object, RenamedEventArgs>(OnImgFileRenamed), source, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

        #region XML & XSLT Tab Events

        private void mApplyXmlChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFeedData != null)
                {
                    mFeedData.LoadXmlText(mXmlText.Text);
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mResetXmlChanges_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int mLastFindPosition = 0;
        private string mLastFindText = null;
        private void mFindButton_Click(object sender, EventArgs e)
        {
            try
            {
                Find(mFindText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFindText_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13) // Return char
                {
                    Find(mFindText.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void Find(string text)
        {
            bool clearLastFind = false;
            if (mLastFindText != mFindText.Text)
            {
                clearLastFind = true;
                mLastFindPosition = 0;
            }
            mLastFindPosition = mXmlText.Find(mFindText.Text, mLastFindPosition, clearLastFind);
            mLastFindText = mFindText.Text;
        }

        private void mItunesPodcast_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!mRefreshing)
                {
                    //Itunes information is going to be cleared!
                    if (!mItunesPodcast.Checked && mFeedData.ItunesFeed)
                    {
                        DialogResult result = MessageBox.Show(
                            "Unchecking iTunes will cause you to loose your iTunes data.  Do you want to proceed?",
                            "Data Loss",
                            MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            mFeedData.ItunesFeed = false;
                        }
                    }
                    else if (mItunesPodcast.Checked && !mFeedData.ItunesFeed)
                    {
                        mFeedData.ItunesFeed = true;
                    }
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void showXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Order is XML, XSLT, FTP
                //Need to add XML.
                if (showXMLToolStripMenuItem.Checked)
                {
                    bool addXsltBack = false;
                    if (this.mFormTabs.Controls.Contains(mXsltTab))
                    {
                        this.mFormTabs.Controls.Remove(mXsltTab);
                        addXsltBack = true;
                    }
                    this.mFormTabs.Controls.Remove(mFTPTab);

                    //Now put them back
                    this.mFormTabs.Controls.Add(mXMLTab);
                    if (addXsltBack)
                        this.mFormTabs.Controls.Add(mXsltTab);
                    this.mFormTabs.Controls.Add(mFTPTab);

                    this.mFormTabs.SelectedTab = mXMLTab;
                }
                else
                    this.mFormTabs.Controls.Remove(this.mXMLTab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void xSLTransformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Order is XML, XSLT, FTP
                //Need to add XSLT.
                if (xSLTransformToolStripMenuItem.Checked)
                {

                    this.mFormTabs.Controls.Remove(mFTPTab);

                    //Now put them back
                    this.mFormTabs.Controls.Add(mXsltTab);
                    this.mFormTabs.Controls.Add(mFTPTab);
                    this.mFormTabs.SelectedTab = mXsltTab;
                }
                else
                    this.mFormTabs.Controls.Remove(this.mXsltTab);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mTransformXml_Click(object sender, EventArgs e)
        {
            try
            {
                string xml = mFeedData.GetPrettyXmlText();
                string stylesheet = mXslt.Text;
                string html = TransformXMLToHTML(xml, stylesheet);
                mFeedData.XsltOutput = html;
                mTransform.DocumentText = html;
                this.RefreshFTPTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Deprecated for now.  Use TransformXmlToHTML2 until we can figure out why
        /// XslCompiledTransform isn't working with <br/> tags.
        /// </summary>
        /// <param name="inputXml"></param>
        /// <param name="xsltString"></param>
        /// <returns></returns>
        public static string TransformXMLToHTML(string inputXml, string xsltString)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            
            using (XmlReader reader = XmlReader.Create(new StringReader(xsltString)))
            {
                transform.Load(reader);
            }
            StringWriter results = new StringWriter();
            using (XmlReader reader = XmlReader.Create(new StringReader(inputXml)))
            {
                transform.Transform(reader, null, results);
            }
            return results.ToString();
        }

        private void mXslt_Leave(object sender, EventArgs e)
        {
            try
            {
                mFeedData.XSLT = mXslt.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mClearXsltOut_Click(object sender, EventArgs e)
        {
            try
            {
                mFeedData.XsltOutput = null;
                mTransform.DocumentText = null;
                this.RefreshFTPTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mCopyXsltOut_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(mTransform.DocumentText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        private void mFTPXsltOut_Click(object sender, EventArgs e)
        {
            try
            {
                this.mFormTabs.SelectedTab = mFTPTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion

        #region Nodes Changed Recursion

        private bool mCheckStateChanging;
        void mTreeModel_NodesChanged(object sender, TreeModelEventArgs e)
        {
            try
            {
                if (!mCheckStateChanging)
                {
                    mCheckStateChanging = true;

                    foreach (Node changedNode in e.Children)
                    {
                        SetSubTreeCheckState(changedNode, changedNode.CheckState);
                        RecurseParentCheckState(changedNode.Parent);
                    }

                    mCheckStateChanging = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Looks up the node tree, setting checked states to each parent until the root
        /// is reached.
        /// </summary>
        /// <param name="node">The node to set a checked state on.</param>
        private void RecurseParentCheckState(Node node)
        {
            if (node != null)
            {
                node.CheckState = GetCollectiveChildrenState(node);

                RecurseParentCheckState(node.Parent);
            }
        }

        /// <summary>
        /// Counts the number of children and the number of children with a CheckState.Checked
        /// value.  These two numbers can then be used for comparison to determine whether the
        /// passed in node should be checked, unchecked, or indeterminate based on it descendent
        /// states.
        /// </summary>
        /// <param name="node">The node to examine descendents on.</param>
        /// <param name="childCount">Number of descendents.</param>
        /// <param name="checkedCount">Number of descendents with a checked state.</param>
        private void CountCheckedDescendents(Node node, ref int childCount, ref int checkedCount)
        {
            foreach (Node child in node.Nodes)
            {
                ++childCount;
                if (child.CheckState == CheckState.Checked)
                    ++checkedCount;

                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (Node grandChild in child.Nodes)
                    {
                        CountCheckedDescendents(grandChild, ref childCount, ref checkedCount);
                    }
                }
            }
        }

        /// <summary>
        /// Method returns checked if all children are checked, unchecked if all children
        /// are unchecked, and Indeterminate if some children are checked and some are not.
        /// </summary>
        /// <param name="node">The node to start testing at.  This node itself is not
        /// tested.  It is assumed that the caller has already tested this node.</param>
        /// <returns>Checked if all children are checked, unchecked if all children
        /// are unchecked, and Indeterminate if some children are checked and some 
        /// are not.</returns>
        private CheckState GetCollectiveChildrenState(Node node)
        {
            CheckState collectiveState = CheckState.Unchecked;
            int checkedCount = 0;
            int childrenCount = 0;
            if (node != null)
            {
                foreach (Node child in node.Nodes)
                {
                    ++childrenCount;
                    CheckState thisChildState = child.CheckState;
                    if (thisChildState == CheckState.Checked || thisChildState == CheckState.Indeterminate)
                        ++checkedCount;

                    CountCheckedDescendents(child, ref childrenCount, ref checkedCount);
                }
            }
            if (checkedCount > 0)
            {
                collectiveState = CheckState.Indeterminate;
                if (childrenCount == checkedCount)
                    collectiveState = CheckState.Checked;
            }
            return collectiveState;
        }

        /// <summary>
        /// Sets a particulare checked state to this node and all of it's descendents.
        /// </summary>
        /// <param name="node">The base of the tree to start setting checked states on.</param>
        /// <param name="state">The new CheckState value to apply to this tree.</param>
        private void SetSubTreeCheckState(Node node, CheckState state)
        {
            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (Node child in node.Nodes)
                {
                    child.CheckState = state;
                    SetSubTreeCheckState(child, state);
                }
            }
        }
        #endregion

        #region FtpDownload BackgroundWorker Methods
        private int mDownloadCount;
        private int mFileCountForDownload;
        private bool mDownloading;
        private void mFtpDownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            mFileCountForDownload = 0;
            mDownloadCount = 0;

            List<object> args = e.Argument as List<object>;
            if (args == null || args.Count < 2)
                return;
            string downloadFolder = args[0] as string;
            List<FtpListItem> selectedLines = args[1] as List<FtpListItem>;
            if (selectedLines == null)
                return;

            mFileCountForDownload = selectedLines.Count;

            foreach (FtpListItem selectedLine in selectedLines)
            {
                if (mFtpDownloadWorker.CancellationPending)
                    break;
                double fileSize = (double)selectedLine.Size;

                try
                {
                    if (selectedLine.Type == FtpFileSystemObjectType.File)
                    {
                        string localPath = string.Format("{0}/{1}", downloadFolder, selectedLine.Name);
                        if (File.Exists(localPath))
                        {
                            DialogResult remove = MessageBox.Show(
                                string.Format("Local file {0} alread exists.  Overwrite?", selectedLine.Name),
                                "Overwrite File", MessageBoxButtons.YesNo);
                            if (remove == DialogResult.Yes)
                            {
                                File.Delete(localPath);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        using (FtpClient conn = new FtpClient())
                        {
                            FTPServerInfo serverInfo = mFTPViewer.ServerInfo;
                            conn.Host = serverInfo.Server;
                            conn.Credentials = new NetworkCredential(
                                serverInfo.User, serverInfo.Password);
                           
                            string remotePath = serverInfo.RemotePath;
                            if (!remotePath.StartsWith("/"))
                                remotePath = "/" + remotePath;
                            if (!remotePath.EndsWith("/"))
                                remotePath = remotePath + "/";
                            string path = remotePath + selectedLine.Name;

                            using (Stream istream = conn.OpenRead(path, FtpDataType.Binary))
                            {
                                mDownloadCount++;
                                mDownloading = true;
                                using (FileStream localFile = File.Create(localPath))
                                {
                                    try
                                    {
                                        byte[] buf = new byte[8192]; //Read 8K at a time
                                        int percentComplete = 0;
                                        double readByteCount = 0;
                                        int bytesRead = 0;
                                        while ((bytesRead = istream.Read(buf, 0, buf.Length)) > 0 &&
                                            !mFtpDownloadWorker.CancellationPending)
                                        {
                                            readByteCount += bytesRead;
                                            localFile.Write(buf, 0, bytesRead);
                                            if (fileSize > 0)
                                            {
                                                percentComplete = (int)(((readByteCount / fileSize) * 100));
                                                mFtpDownloadWorker.ReportProgress(percentComplete);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        istream.Close();
                                        localFile.Close();
                                        mDownloading = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("{0} is not a file type.  Can only download files.",
                            selectedLine.Name);
                    }

                }
                catch (Exception ex)
                {
                    DialogResult exceptionDialogResult = MessageBox.Show(
                        string.Format("An error occured: \"{0}\".  Continue downloading files?", ex.Message),
                        "FTP Error", MessageBoxButtons.YesNo);
                    if (exceptionDialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        break;
                    }
                }
            }
        }

        private int mLastDownloadPercentComplete;
        private void mFtpDownloadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != mLastDownloadPercentComplete)
            {
                mUploadProgressText.Text = "";
                mFtpStatusBar.Value = e.ProgressPercentage;
                mCancelUploadImage.Image = Resources.RedXsmall;
                string progressText = string.Format("File {0} of {1} - {2}%",
                    mDownloadCount, mFileCountForDownload, e.ProgressPercentage);
                mUploadProgressText.Text = progressText;
                mLastDownloadPercentComplete = e.ProgressPercentage;
            }
        }

        private void mFtpDownloadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mFtpStatusBar.Value = 0;
            mCancelUploadImage.Image = Resources.GreyXsmall;
            mUploadProgressText.Text = "Downloads Complete";
            mFTPViewer.Refresh();
        }
        #endregion

        #region FTP Upload BackgroundWorker Methods
        private int mUploadCount;
        private int mFileCountForUpload;
        private bool mUploading;
        private void mFTPUploadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Node> uploadFiles = mLocalFileViewer.CheckedNodesOfType(FtpUploadNodeTypes);
            if (uploadFiles == null || uploadFiles.Count == 0 && !mLocalFileViewer.ExplorerMode)
                return;
            else if (mLocalFileViewer.ExplorerMode && mLocalFileViewer.SelectedExplorerPath != null)
            {
                UploadFile(mLocalFileViewer.SelectedExplorerPath);
                return;
            }

            mUploading = true;
            mFileCountForUpload = uploadFiles.Count;
            //uncheck the nodes last, after the uploads are complete.
            List<Node> nodesToUncheck = new List<Node>();
            foreach (Node node in uploadFiles)
            {
                //If this is the feed file and it has an indeterminate checkmark, then
                //create a temporary file that only has the items that are checked.
                FtpNodeTag tag = node.Tag as FtpNodeTag;
                string path = null;
                try
                {
                    switch (tag.NodeType)
                    {
                        case NodeTypes.ItunesFeedNode:

                            if (node.CheckState != CheckState.Unchecked)
                            {
                                //Only save the checked nodes, and do it to a temp location
                                path = mFeedData.SaveItunesForUpload(true);
                                UploadFile(path);
                                nodesToUncheck.Add(node);
                            }
                            //Delete the temporary ftp upload file
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            break;
                        case NodeTypes.NonItunesFeedNode:

                            if (node.CheckState != CheckState.Unchecked)
                            {
                                //Only save the checked nodes, and do it to a temp location
                                path = mFeedData.SaveNonItunesForUpload(true);
                                UploadFile(path);
                                nodesToUncheck.Add(node);
                            }
                            //Delete the temporary ftp upload file
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                            break;
                        case NodeTypes.ImageNode:
                            UploadFile(tag.NodeObject as string);
                            nodesToUncheck.Add(node);
                            break;
                        case NodeTypes.ContentNode:
                            UploadFile(tag.NodeObject as string);
                            nodesToUncheck.Add(node);
                            break;
                        case NodeTypes.HtmlNode:
                            UploadFile(tag.NodeObject as string);
                            nodesToUncheck.Add(node);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    DialogResult exceptionDialogResult = MessageBox.Show(
                        string.Format("An error occured: \"{0}\".  Continue downloading files?", ex.Message),
                        "FTP Error", MessageBoxButtons.YesNo);
                    if (exceptionDialogResult == System.Windows.Forms.DialogResult.No)
                    {
                        break;
                    }
                }
            }
            foreach (Node uncheckNode in nodesToUncheck)
            {
                uncheckNode.CheckState = CheckState.Unchecked;
            }
            mUploading = false;
            mUploadCount = 0;
        }

        private void UploadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] split = filePath.Split('/', '\\');
                string fileName = split[split.Length - 1];
                FileInfo fi = new FileInfo(filePath);
                double fileSize = (double)fi.Length;
                
                using (FtpClient conn = new FtpClient())
                {
                    FTPServerInfo serverInfo = mFTPViewer.ServerInfo;
                    conn.Host = serverInfo.Server;
                    conn.Credentials = new NetworkCredential(
                        serverInfo.User, serverInfo.Password);

                    string remotePath = serverInfo.RemotePath;
                    if (!remotePath.StartsWith("/"))
                        remotePath = "/" + remotePath;
                    if (!remotePath.EndsWith("/"))
                        remotePath = remotePath + "/";
                    fileName = remotePath + fileName;
                    bool mCancelled = false;

                    using (Stream istream = new FileStream(filePath, FileMode.Open, FileAccess.Read),
                        ostream = conn.OpenWrite(fileName))
                    {
                        mUploadCount++;
                        byte[] buf = new byte[8192];
                        int bytesRead = 0;
                        double readByteCount = 0;
                        int percentComplete = 0;
                        try
                        {

                            while ((bytesRead = istream.Read(buf, 0, buf.Length)) > 0 &&
                                !mFTPUploadWorker.CancellationPending)
                            {
                                readByteCount += bytesRead;
                                ostream.Write(buf, 0, bytesRead);
                                percentComplete = (int)(((readByteCount / fileSize) * 100));
                                mFTPUploadWorker.ReportProgress(percentComplete);
                            }
                            mCancelled = mFTPUploadWorker.CancellationPending;
                        }
                        finally
                        {
                            ostream.Close();
                            istream.Close();
                        }
                        if (mCancelled)
                        {
                            conn.DeleteFile(fileName);
                        }
                    }
                }
            }
        }
        

        private void mValidate_Click(object sender, EventArgs e)
        {
            ValidationForm val = new ValidationForm();
            val.Show();
            val.URL = mValidationURL.Text;
        }

        private void mValidationURL_Leave(object sender, EventArgs e)
        {
            mFeedData.ValidationURL = mValidationURL.Text;
        }

        private void mFTPUploadWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            mFtpStatusBar.Value = e.ProgressPercentage;
            mCancelUploadImage.Image = Resources.RedXsmall;
            string progressText = string.Format("File {0} of {1} - {2}%",
                mUploadCount, mFileCountForUpload, e.ProgressPercentage);
            mUploadProgressText.Text = progressText;
        }

        private void mFTPUploadWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mFtpStatusBar.Value = 0;
            mUploading = false;
            mCancelUploadImage.Image = Resources.GreyXsmall;
            mUploadProgressText.Text = "Uploads Complete";
            mFTPViewer.Refresh();
        }

        private void mCancelUploadImage_Click(object sender, EventArgs e)
        {
            if (mUploading)
            {
                mCancelUploadImage.Image = Resources.GreyXsmall;
                mFTPUploadWorker.CancelAsync();
                mUploading = false;
            }
            if (mDownloading)
            {
                mCancelUploadImage.Image = Resources.GreyXsmall;
                mFtpDownloadWorker.CancelAsync();
                mDownloading = false;
            }
        }

        #endregion

    }
}

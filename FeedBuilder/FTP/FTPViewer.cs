using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FeedBuilder.Properties;
using FeedBuilder.FTP;
using System.Net.FtpClient;
using System.Net;

namespace FeedBuilder
{

    public delegate void LineSelectionEvent(bool downloadEnabled);
    public delegate void DownloadSelectionEvent(List<FtpListItem> lines);

    public partial class FTPViewer : UserControl
    {
        private FTPServerInfo mServerInfo;
        private FtpListItem mSelectedLine;
        private bool mRefreshing;
        private int mCurrentMouseOverRow;
        private Point mMouseHit;
        private bool mRightClick;

        public LineSelectionEvent SelectLine;

        public DownloadSelectionEvent SelectDownloads;

        public FTPViewer()
        {
            InitializeComponent();
            SelectLine += ShowContextMenu;
        }

        public FTPServerInfo ServerInfo
        {
            get 
            { 
                return mServerInfo; 
            }
            set
            {
                mServerInfo = value;
            }
        }

        /// <summary>
        /// Returns the most recently selected line.
        /// </summary>
        public FtpListItem SelectedLine
        {
            get { return mSelectedLine; }
        }

        public List<FtpListItem> SelectedLines
        {
            get
            {
                List<FtpListItem> selections = new List<FtpListItem>(mFTPWindow.SelectedRows.Count);
                foreach (DataGridViewRow row in mFTPWindow.SelectedRows)
                {
                    FtpListItem lineItem = row.Tag as FtpListItem;
                    if (lineItem != null)
                    {
                        selections.Add(lineItem);
                    }
                }
                return selections;
            }
        }

        public void ShowFTPConnection()
        {
            RefreshFTPData();
        }

        public void ShowFTPConnection(FTPServerInfo serverInfo)
        {
            this.ServerInfo = serverInfo;
            ShowFTPConnection();
        }

        public override void Refresh()
        {
            base.Refresh();
            RefreshFTPData();
        }

       // <TODO> Change all FTPLineItem references to FtpListItem.</TODO>

        private void RefreshFTPData()
        {
            mRefreshing = true;
            try
            {
                using (FtpClient conn = FtpClient.Connect(new Uri(mServerInfo.UriString)))
                {
                    mServerLabel.Text = mServerInfo.UrlString;

                    mFTPWindow.Rows.Clear();
                    mFTPWindow.Rows.Add(Resources.FolderIcon, "..", "--");
                    FtpListItem upDirTag = new FtpListItem();
                    upDirTag.FullName = "..";
                    upDirTag.Name = "..";
                    upDirTag.Type = FtpFileSystemObjectType.Link;
                    mFTPWindow.Rows[0].Tag = upDirTag;

                    List<FtpListItem> files = new List<FtpListItem>();

                    Bitmap img = null;
                    string fileSize;
                    FtpListItem[] items = conn.GetListing(conn.GetWorkingDirectory(),
                        FtpListOption.Modify | FtpListOption.Size);
                    Array.Sort(items);
                    foreach (FtpListItem item in items)
                    {
                        switch (item.Type)
                        {
                            case FtpFileSystemObjectType.Directory:
                                img = Resources.FolderIcon;
                                fileSize = "--";
                                break;
                            case FtpFileSystemObjectType.File:
                                img = Resources.FileIcon;
                                fileSize = item.Size.ToString();
                                break;
                            case FtpFileSystemObjectType.Link:
                                // derefernece symbolic links
                                if (item.LinkTarget != null)
                                {
                                    // see the DereferenceLink() example
                                    // for more details about resolving links.
                                    item.LinkObject = conn.DereferenceLink(item);

                                    if (item.LinkObject != null)
                                    {
                                        switch (item.LinkObject.Type)
                                        {
                                            case FtpFileSystemObjectType.Directory:
                                                img = Resources.FolderIcon;
                                                fileSize = "--";
                                                break;
                                            case FtpFileSystemObjectType.File:
                                                img = Resources.FileIcon;
                                                fileSize = item.Size.ToString();
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                        int rowNum = mFTPWindow.Rows.Add(img, item.Name, item.Size);
                        mFTPWindow.Rows[rowNum].Tag = item;
                    }
                }
                SelectLine(false);
            }
            finally
            {
                mRefreshing = false;
            }

            //mRefreshing = true;
            //if (mConnector.Connected)
            //{
            //    mServerLabel.Text = mConnector.Hostname;
            //    mRemotePathLabel.Text = "/" + mConnector.CWD;
            //}
            //else
            //{
            //    mServerLabel.Text = null;
            //    mRemotePathLabel.Text = "Remote Files";
            //    mFTPWindow.Rows.Clear();
            //    return;
            //}
            //try
            //{
            //    mFTPWindow.Rows.Clear();
            //    mFTPWindow.Rows.Add(Resources.FolderIcon, "..", "--");
            //    mFTPWindow.Rows[0].Tag = new FTPLineItem(FTPLineItem.UP_DIR);

            //    List<FTPLineItem> files = mConnector.GetRemoteFiles("");
            //    Bitmap img = null;
            //    string fileSize;
            //    foreach (FTPLineItem file in files)
            //    {
            //        if (file.IsDirectory)
            //        {
            //            img = Resources.FolderIcon;
            //            fileSize = "--";
            //        }
            //        else
            //        {
            //            img = Resources.FileIcon;
            //            fileSize = file.Size.ToString();
            //        }
            //        int rowNum = mFTPWindow.Rows.Add(img, file.FileName, fileSize);
            //        mFTPWindow.Rows[rowNum].Tag = file;
            //    }
            //    SelectLine(false);
            //}
            //finally
            //{
            //    mRefreshing = false;
            //}
        }

        private void mFTPWindow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FtpListItem item = mFTPWindow.Rows[e.RowIndex].Tag as FtpListItem;
            if (item.Type == FtpFileSystemObjectType.Directory)
            {
                mServerInfo.RemotePath = item.FullName;
            }
            else if (item.Type == FtpFileSystemObjectType.Link && item.Name == "..")
            {
                mServerInfo.UpDir();
            }
            RefreshFTPData();
        }

        private void mFTPWindow_SelectionChanged(object sender, EventArgs e)
        {
            if (!mRefreshing)
            {
                if (mFTPWindow.SelectedRows.Count > 0)
                {
                    mSelectedLine = mFTPWindow.SelectedRows[0].Tag as FtpListItem;
                    if (mSelectedLine != null)
                    {
                        string fileName = mSelectedLine.Name;
                        if (fileName != null && (fileName.EndsWith(".jpg") ||
                                                 fileName.EndsWith(".png") ||
                                                 fileName.EndsWith(".xml") ||
                                                 fileName.EndsWith(".mp3") ||
                                                 fileName.EndsWith(".m4a") ||
                                                 fileName.EndsWith(".mp4") ||
                                                 fileName.EndsWith(".m4v") ||
                                                 fileName.EndsWith(".mov") ||
                                                 fileName.EndsWith(".pdf") ||
                                                 fileName.EndsWith(".epub")))
                        {
                            //Enable file downloads
                            if (SelectLine != null)
                            {
                                SelectLine(true);
                            }
                        }
                        else
                        {
                            //Disable file downloads
                            if (SelectLine != null)
                            {
                                SelectLine(false);
                            }
                        }
                    }
                }
            }
        }

        private void ShowContextMenu(bool show)
        {
            if (show && mRightClick)
            {
                mContextMenuStrip.Show(mFTPWindow, mMouseHit);
            }
            else
            {
                mContextMenuStrip.Hide();
            }
        }

        private void mFTPWindow_MouseClick(object sender, MouseEventArgs e)
        {
            mMouseHit = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                mRightClick = true;
                mCurrentMouseOverRow = mFTPWindow.HitTest(e.X, e.Y).RowIndex;

                if (mCurrentMouseOverRow >= 0)
                {
                    mSelectedLine = mFTPWindow.Rows[mCurrentMouseOverRow].Tag as FtpListItem;
                    //mFTPWindow.ClearSelection();
                    mFTPWindow.Rows[mCurrentMouseOverRow].Selected = true;
                    ShowContextMenu(true);
                }
            }
            mRightClick = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mFTPWindow.SelectedRows.Count >= 0)
            {
                string message = (mFTPWindow.SelectedRows.Count > 1) ?
                    "Are you sure you want to delete these files?" :
                    "Are you sure you want to delete this file?";

                DialogResult deleteAnswer = MessageBox.Show(message, 
                    "Confirm Delete", MessageBoxButtons.YesNo);
                if (deleteAnswer == DialogResult.Yes)
                {
                    if (mSelectedLine.Type != FtpFileSystemObjectType.Directory)
                    {
                        foreach (DataGridViewRow row in mFTPWindow.SelectedRows)
                        {
                            FtpListItem ftpLine = row.Tag as FtpListItem;
                            if (ftpLine.Type == FtpFileSystemObjectType.File)
                            {
                                if (ftpLine != null)
                                {
                                    using (FtpClient conn = new FtpClient())
                                    {
                                        conn.Host = mServerInfo.Server;
                                        conn.Credentials = new NetworkCredential(mServerInfo.User, mServerInfo.Password);
                                        conn.DeleteFile(ftpLine.FullName);
                                    }
                                }
                            }
                        }
                        Refresh();
                    }
                }
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDownloads(SelectedLines);
        }
    }
}

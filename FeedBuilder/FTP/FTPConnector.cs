using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.FtpClient;

namespace FeedBuilder.FTP
{
    public class FTPConnector
    {
        private string mHostname;
        private string mUser;
        private string mPassword;
        private string mCWD;

        private bool mUploadComplete;
        private int mUploadProgress;
        private List<LocalFileFTPInfo> mFtpUploadQueue;
        private string mFTPDownloadQueue;
        private long mEnqueuedByteCount;
        private string mCurrentUploadFilePath;
        private bool mUploadInProgress;
        private bool mConnected;
        private FtpException mFtpException;
        private string mLastUploadPath;

        //For Upload Operations
        FtpWebRequest mFtpUploadRequest;
        FileStream mUploadInputStream;
        Stream mUploadOutputStream;
        long mTotalReadBytesCount = 0;
        int mUploadFileIndex = 0;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public FTPConnector()
        {
            mFtpUploadQueue = new List<LocalFileFTPInfo>();
        }


        /// <summary>
        /// Creates an FTPConnector object and opens the connection.
        /// </summary>
        /// <param name="hostname">ftp host name</param>
        /// <param name="user">ftp username</param>
        /// <param name="password">ftp password</param>
        public FTPConnector(string hostname, string user, string password) 
            : this(hostname, user, password, null)
        {
        }

        /// <summary>
        /// Creates an FTPConnector object with a remote path and opens the connection.
        /// </summary>
        /// <param name="hostname">ftp host name</param>
        /// <param name="remotePath">remote directory path</param>
        /// <param name="user">ftp username</param>
        /// <param name="password">ftp password</param>
        public FTPConnector(string hostname, string user, string password, string remotePath) : this()
        {
            if (!hostname.StartsWith("ftp://"))
                mHostname = "ftp://" + hostname;
            else
                mHostname = hostname;

            mUser = user;
            mPassword = password;
            mCWD = remotePath;
        }

        /// <summary>
        /// Resets the progress parameters associated with an upload.
        /// </summary>
        public void ResetUploadProgress()
        {
            mUploadProgress = 0;
            mCurrentUploadFilePath = null;
            mUploadInProgress = false;

            mTotalReadBytesCount = 0;
            mUploadFileIndex = 0;
        }

        /// <summary>
        /// Enqueue a file to be FTP'd
        /// </summary>
        /// <param name="localFilePath">The local file to put into the ftp queue.</param>
        /// <param name="ftpDir">Absolute path of the FTP directory to send the file to, or null
        /// to use the CWD.</param>
        public void EnqueueUploadFile(string localFilePath, string ftpDir)
        {
            if (!File.Exists(localFilePath))
            {
                throw new Exception(string.Format("File '{0}' does not exist.", localFilePath));
            }
            mUploadComplete = false;
            FileInfo f = new FileInfo(localFilePath);
            if (ftpDir == null)
                ftpDir = CWD;

            mFtpUploadQueue.Add(new LocalFileFTPInfo(localFilePath, ftpDir, f.Length));
            mEnqueuedByteCount += f.Length;
        }

        /// <summary>
        /// Clears all the files from the upload queue.
        /// </summary>
        public void ClearUploadQueue()
        {
            mFtpUploadQueue.Clear();
        }

        public int CurrentFileIndex
        {
            get { return mUploadFileIndex; }
        }

        public int FileCount
        {
            get
            {
                int fileCount = 0;
                if (mFtpUploadQueue != null)
                    fileCount = mFtpUploadQueue.Count;
                return fileCount;
            }
        }

        /// <summary>
        /// Enqueues a file for download.  Only one file at a time for downloads.
        /// </summary>
        /// <param name="fileName"></param>
        public void EnqueueDownloadFile(string fileName)
        {
            mFTPDownloadQueue = fileName;
        }

        /// <summary>
        /// Downloads the queued file from the CWD and returns the byte array.
        /// </summary>
        /// <returns></returns>
        public byte[] DownloadEnqueuedFileFromCWD()
        {
            return GetRemoteFileFromCWD(mFTPDownloadQueue);
        }

        /// <summary>
        /// Gets the local file path of the file that is currently being uploaded, or null if
        /// no upload in progress.
        /// </summary>
        public string CurrentUploadFilePath
        {
            get
            {
                if (mUploadInProgress)
                    return mCurrentUploadFilePath;
                else
                    return null;
            }
        }

        /// <summary>
        /// Gets the local file name of the file that is currently being uploaded, or null if
        /// no upload in progress.
        /// </summary>
        public string CurrentUploadFileName
        {
            get
            {
                string fileName = null;
                if (!mUploadInProgress && (mCurrentUploadFilePath != null))
                {
                    string[] splits = mCurrentUploadFilePath.Split(new char[] { '/', '\\' },
                        StringSplitOptions.RemoveEmptyEntries);
                    fileName = splits[splits.Length - 1];
                }
                return fileName;
            }
        }

        public bool UploadComplete
        {
            get { return mUploadComplete; }
        }

        public void CancelUpload()
        {
            if (mUploadOutputStream != null)
            {
                mUploadOutputStream.Close();
                mUploadOutputStream.Dispose();
                mUploadOutputStream = null;
            }
            if (mUploadInputStream != null)
            {
                mUploadInputStream.Close();
                mUploadInputStream.Dispose();
                mUploadInputStream = null;
            }

            mCurrentUploadFilePath = null;
            mUploadComplete = true;
            mUploadInProgress = false;
            mUploadFileIndex = 0;
        }

        /// <summary>
        /// Uploads some bytes to the FTP server.  Returns an int representing the number of bytes
        /// uploaded.
        /// </summary>
        /// <returns>The number of bytes uploaded.</returns>
        public long UploadBytes()
        {
            //TODO: May need to set connection data mode (eg binary, text) for UPLOAD and LIST.

            if (mFtpUploadQueue.Count == 0)
            {
                mUploadComplete = true;
                return 0;
            }
            else
            {
                mUploadComplete = false;
            }

            string[] splits;
            string fileName;
            string url;
            byte[] buffer;
            bool fileUploadComplete = false;

            try
            {
                mUploadInProgress = true;
                
                if (mUploadFileIndex >= mFtpUploadQueue.Count)
                    return mTotalReadBytesCount;

                LocalFileFTPInfo pair = mFtpUploadQueue[mUploadFileIndex];

                if (pair.FTP_DIR != mCWD)
                    CWD = pair.FTP_DIR;

                //construct the remote URL.
                mCurrentUploadFilePath = pair.LOCAL_PATH;
                splits = mCurrentUploadFilePath.Split('/', '\\');
                fileName = splits[splits.Length - 1];
                url = mHostname + "/" + mCWD + "/" + fileName;
                
                //Initialize FtpWebRequest object to do the actual work.
                mFtpUploadRequest = (FtpWebRequest)WebRequest.Create(url);
                mFtpUploadRequest.Credentials = new NetworkCredential(mUser, mPassword);
                mFtpUploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                mFtpUploadRequest.KeepAlive = true;

                mFtpException = null;

                if (mCurrentUploadFilePath != mLastUploadPath)
                {
                    mUploadInputStream = File.OpenRead(mCurrentUploadFilePath);
                    mUploadOutputStream = mFtpUploadRequest.GetRequestStream();
                }

                buffer = new byte[1024 * 1024];
                int readByteCount = mUploadInputStream.Read(buffer, 0, buffer.Length);
                
                if (readByteCount > 0)
                {
                    mUploadOutputStream.Write(buffer, 0, readByteCount);
                    mTotalReadBytesCount += readByteCount;
                    mUploadProgress = (int)((mTotalReadBytesCount*1.0 / mEnqueuedByteCount*1.0) * 100.0);

                    if (mTotalReadBytesCount >= mEnqueuedByteCount)
                    {
                        fileUploadComplete = true;
                    }
                }
                else
                {
                    mUploadFileIndex += 1;
                }
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (fileUploadComplete)
                {
                    mUploadOutputStream.Close();
                    mUploadOutputStream.Dispose();
                    mUploadOutputStream = null;
                    mUploadInputStream = null;
                    mUploadComplete = true;
                    mCurrentUploadFilePath = null;
                    mLastUploadPath = null;
                    mEnqueuedByteCount = 0;
                }
            }
            mLastUploadPath = mCurrentUploadFilePath;

            return mTotalReadBytesCount;
        }

        public string Hostname
        {
            get { return mHostname; }
        }

        public string URL
        {
            get
            {
                string cwd = (mCWD == null || mCWD == "") ? "" : "/" + mCWD;
                return mHostname + cwd;
            }
        }

        public void ChangeDirectory(string newDirectory)
        {
            if (mCWD == null || mCWD == string.Empty)
            {
                mCWD = newDirectory;
            }
            else if (newDirectory != "..")
            {
                mCWD = mCWD + "/" + newDirectory;
            }
            else if (newDirectory == ".." && mCWD != null && mCWD != string.Empty)
            {
                string[] cwds = mCWD.Split('/');
                string[] newCWDs = new string[cwds.Length - 1];
                for (int i = 0; i < cwds.Length - 1; i++)
                {
                    newCWDs[i] = cwds[i];
                }
                mCWD = string.Join("/", newCWDs);
            }

            FtpWebResponse response = null;
            try
            {
                FtpWebRequest ftpRequest =
                            (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(
                            new Uri(mHostname + "/" + mCWD));
                ftpRequest.Credentials = new System.Net.NetworkCredential(mUser, mPassword);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                mConnected = false;
                mFtpException = null;
                response = (FtpWebResponse)ftpRequest.GetResponse();
                mConnected = true;
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// Returns a number between 0 and 100% indicating the progress of the upload process.
        /// </summary>
        public int UploadProgress
        {
            get { return mUploadProgress; }
        }

        public void DeleteFileFromCWD(string fileName)
        {
            FtpWebRequest request = null;
            FtpWebResponse response = null;
            try
            {
                string url = string.Format("{0}/{1}/{2}", mHostname, mCWD, fileName);
                request = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(url);
                request.Credentials = new NetworkCredential(mUser, mPassword);

                request.Method = WebRequestMethods.Ftp.DeleteFile;
                mConnected = false;
                mFtpException = null;
                response = (FtpWebResponse)request.GetResponse();
                mConnected = true;
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        public byte[] GetRemoteFileFromCWD(string fileName)
        {
            WebClient request = null;
            byte[] fileData = null;
            try
            {
                mConnected = false;
                mFtpException = null;
                request = new WebClient();
                request.Credentials = new NetworkCredential(mUser, mPassword);
                fileData = request.DownloadData(mHostname + "/" + mCWD + "/" + fileName);
                mConnected = true;
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (request != null)
                {
                    request.Dispose();
                }
            }
            return fileData;
        }

        public string CWD
        {
            get { return mCWD; }
            set { ChangeDirectory(value); }
        }

        public bool Connect()
        {
            FtpWebRequest ftpRequest = null;
            string cwd = (mCWD == null || mCWD == "") ? "" : "/" + mCWD;
            ftpRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(
                new Uri(mHostname + cwd));
            ftpRequest.Credentials = new NetworkCredential(mUser, mPassword);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = null;
            try
            {
                mConnected = false;
                mFtpException = null;
                response = (FtpWebResponse)ftpRequest.GetResponse();
                mConnected = true;
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return mConnected;
         }

        public bool Connected
        {
            get { return mConnected; }
        }

        public List<FTPLineItem> GetRemoteFiles(string remotePath)
        {
            List<FTPLineItem> remoteFiles = new List<FTPLineItem>();
            FtpWebRequest ftpRequest = null;
            string cwd = (mCWD == null || mCWD == "") ? "" : "/" + mCWD;
            ftpRequest = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(
                new Uri(mHostname + cwd));
            ftpRequest.Credentials = new NetworkCredential(mUser, mPassword);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse response = null;
            try
            {
                mConnected = false;
                mFtpException = null;
                response = (FtpWebResponse)ftpRequest.GetResponse();
                mConnected = true;
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string line = reader.ReadLine();
                        if (line != null)
                        {
                            while (line != null)
                            {
                                remoteFiles.Add(new FTPLineItem(line));
                                line = reader.ReadLine();
                            }
                        }
                    }
                }
            }
            catch (WebException webEx)
            {
                MessageBox.Show(webEx.Message);
                mFtpException = webEx.Message;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
            return remoteFiles;
        }
    }
}

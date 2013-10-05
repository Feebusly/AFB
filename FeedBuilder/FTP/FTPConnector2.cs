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
using System.Threading;

namespace FeedBuilder.FTP
{
    public class FTPConnector2
    {

        private string mHostname;
        private string mUser;
        private string mPassword;
        private string mCWD;

        private bool mUploadComplete;
        private int mUploadProgress;
        private long mEnqueuedByteCount;
        long mTotalReadBytesCount = 0;
        int mUploadFileIndex = 0;

        private string mCurrentUploadFilePath;
        private bool mUploadInProgress;

        private string mFTPDownloadQueue;
        List<LocalFileFTPInfo> mUploadQueue;



        /// <summary>
        /// Default constructor.
        /// </summary>
        public FTPConnector2()
        {
            mUploadQueue = new List<LocalFileFTPInfo>();
        }


        /// <summary>
        /// Creates an FTPConnector object and opens the connection.
        /// </summary>
        /// <param name="hostname">ftp host name</param>
        /// <param name="user">ftp username</param>
        /// <param name="password">ftp password</param>
        public FTPConnector2(string hostname, string user, string password)
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
        public FTPConnector2(string hostname, string user, string password, string remotePath)
            : this()
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

            mUploadQueue.Add(new LocalFileFTPInfo(localFilePath, ftpDir, f.Length));
            mEnqueuedByteCount += f.Length;
        }

        /// <summary>
        /// Clears all the files from the upload queue.
        /// </summary>
        public void ClearUploadQueue()
        {

            mUploadQueue.Clear();
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
                if (mUploadQueue != null)
                    fileCount = mUploadQueue.Count;
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
        public void DownloadEnqueuedFileFromCWD()
        {
            using (FtpClient conn = new FtpClient())
            {
                conn.Host = mHostname;
                conn.Credentials = new NetworkCredential(mUser, mPassword);
                conn.BeginOpenRead("/path/to/file", new AsyncCallback(BeginOpenReadCallback), conn);

                conn.Disconnect();
            }
        }

        private void BeginOpenReadCallback(IAsyncResult ar)
        {
            FtpClient conn = ar.AsyncState as FtpClient;

            try
            {
                if (conn == null)
                    throw new InvalidOperationException("The FtpControlConnection object is null!");

                using (Stream istream = conn.EndOpenRead(ar))
                {
                    byte[] buf = new byte[8192];

                    try
                    {
                        DateTime start = DateTime.Now;

                        while (istream.Read(buf, 0, buf.Length) > 0)
                        {
                            double perc = 0;

                            if (istream.Length > 0)
                                perc = (double)istream.Position / (double)istream.Length;


                            //Console.Write("\rTransferring: {0}/{1} {2}/s {3:p}         ",
                            //              istream.Position.FormatBytes(),
                            //              istream.Length.FormatBytes(),
                            //              (istream.Position / DateTime.Now.Subtract(start).TotalSeconds).FormatBytes(),
                            //              perc);
                        }
                    }
                    finally
                    {
                        Console.WriteLine();
                        istream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
                
            }
        }

        public bool UploadComplete
        {
            get 
            {
            
            }
        }

        public void CancelUpload()
        {
            
        }

        /// <summary>
        /// Uploads some bytes to the FTP server.  Returns an int representing the number of bytes
        /// uploaded.
        /// </summary>
        /// <returns>The number of bytes uploaded.</returns>
        public long UploadBytes()
        {

        }

        public string Hostname
        {
            get 
            {
            
            }
        }

        public string URL
        {
            get
            {
                
            }
        }

        public void ChangeDirectory(string newDirectory)
        {
            
        }

        /// <summary>
        /// Returns a number between 0 and 100% indicating the progress of the upload process.
        /// </summary>
        public int UploadProgress
        {
            get 
            { 
            
            }
        }

        public void DeleteFileFromCWD(string fileName)
        {

        }

        public byte[] GetRemoteFileFromCWD(string fileName)
        {
            
        }

        public string CWD
        {
            get 
            {
            }
            set
            { 
            }
        }

        public bool Connect()
        {
        }

        public bool Connected
        {
            get 
            { 
                
            }
        }

        public List<FTPLineItem> GetRemoteFiles(string remotePath)
        {

        }
    }
}

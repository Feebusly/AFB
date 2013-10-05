using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedBuilder.FTP
{
    public struct LocalFileFTPInfo
    {
        public string LOCAL_PATH, FTP_DIR;
        public long FILE_SIZE;

        public LocalFileFTPInfo(string localFile, string ftpDir, long fileSize)
        {
            this.LOCAL_PATH = localFile;
            this.FTP_DIR = ftpDir;
            this.FILE_SIZE = fileSize;
        }
    }
}

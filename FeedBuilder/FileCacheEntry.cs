using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FeedBuilder
{
    /// <summary>
    /// A helper class for the FeedFileCache.
    /// </summary>
    public class FileCacheEntry : IComparable, IDisposable
    {

        public FileCacheEntry(string filename, string directory) 
        {
            mFileName = filename;
            mDirectory = directory;
            CheckLink();
        }

        public FileCacheEntry(string filePath)
        {
            string[] splits = filePath.Split('/', '\\');
            mFileName = splits[splits.Length - 1];
            mDirectory = string.Join("\\", splits, 0, splits.Length - 1);
            CheckLink();
        }

        public bool IsUnsavedNewFile()
        {
            return mDirectory == null;
        }

        public string FilePath
        {
            get
            {
                return mDirectory + "\\" + mFileName;
            }
        }

        public void CheckLink()
        {
            if (File.Exists(FilePath))
                mFileExists = true;
            else
                mFileExists = false;
        }

        private bool mSelected;
        public bool Selected
        {
            get { return mSelected; }
            set
            {
                mSelected = value;
            }
        }
        
        private bool mFileExists;
        public bool FileExists
        {
            get { return mFileExists; }
            set
            {
                mFileExists = value;
            }
        }

        private bool mActive;
        public bool IsActive
        {
            get { return mActive; }
            set 
            {
                mActive = value;
            }
        }

        private string mFileName;
        public string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

        private string mDirectory;
        public string Directory
        {
            get { return mDirectory; }
            set { mDirectory = value; }
        }

        public override bool Equals(object obj)
        {
            FileCacheEntry testEntry = obj as FileCacheEntry;
            if (testEntry != null)
            {
                return (testEntry.FilePath == this.FilePath);
            }
            return false;
        }

        public int CompareTo(object obj)
        {
            FileCacheEntry objectIn = obj as FileCacheEntry;
            if (objectIn == null)
            {
                throw new ArgumentException("Object must be of type FileCacheEntry for FileCacheEntry.CompareTo method.");
            }
            if (this.IsActive || this.IsUnsavedNewFile())
                return -1;
            else if (objectIn.IsActive || objectIn.IsUnsavedNewFile())
                return 1;
            else
                return mFileName.CompareTo(objectIn.FileName);
        }

        public override string ToString()
        {
            return mFileName;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

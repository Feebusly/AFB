using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedBuilder
{
    public class FTPLineItem
    {
        bool mIsDirectory;

        string mLine;
        string mPermissions;
        int mPosition = 0;
        string mOwner;
        string mGroup;
        int mSize;
        string mLastModifyTime;
        string mFileName;

        public static string UP_DIR = "drwxrwxrwx 0 na na 4096 na 0 1901 ..";

        /// <summary>
        /// Parses ftp info from the line.  E.g. directory/file, owner, group, size, name, date, etc.
        /// </summary>
        /// <param name="info">The string containing info about this line item</param>
        public FTPLineItem(string info)
        {
            const int INDEX_PERMISSIONS = 0;
            const int INDEX_POS = 1;
            const int INDEX_OWNER = 2;
            const int INDEX_GROUP = 3;
            const int INDEX_SIZE = 4;
            const int INDEX_MONTH = 5;
            const int INDEX_DAY = 6;
            const int INDEX_YEAR_HOUR = 7;
            const int INDEX_FILENAME = 8;

            mLine = info;

            mIsDirectory = mLine.StartsWith("d");

            string[] data = info.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
            mPermissions = data[INDEX_PERMISSIONS];
            int.TryParse(data[INDEX_POS], out mPosition);
            mOwner = data[INDEX_OWNER];
            mGroup = data[INDEX_GROUP];
            int.TryParse(data[INDEX_SIZE], out mSize);
            mLastModifyTime = string.Format("{0} {1} {2}",
                data[INDEX_MONTH], data[INDEX_DAY], data[INDEX_YEAR_HOUR]);

            mFileName = data[INDEX_FILENAME];
            for (int i = INDEX_FILENAME + 1; i < data.Length; i++)
            {
                mFileName = mFileName + " " + data[i];
            }
        }

        public string Permissions
        {
            get { return mPermissions; }
        }

        public int Position
        {
            get { return mPosition; }
        }

        public string Owner
        {
            get { return mOwner; }
        }

        public string Group
        {
            get { return mGroup; }
        }

        public int Size
        {
            get { return mSize; }
        }

        public string LastModTime
        {
            get { return mLastModifyTime; }
        }

        public string FileName
        {
            get { return mFileName; }
        }

        public bool IsDirectory
        {
            get { return mIsDirectory; }
        }

        public override string ToString()
        {
            return mLine;
        }
    }
}

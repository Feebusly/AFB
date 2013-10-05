using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace FeedBuilder
{
    /// <summary>
    /// This class maintains the known list of open files.  It reflects a delimited list of records
    /// in the user's application folder.  When a file is opened that has not been opened before
    /// it's name and path are saved here.  When a new or existing file is saved, its name and 
    /// path are saved here.  If a cached file is attempted to be opened but cannot be found, it
    /// will be removed from the cache.
    /// </summary>
    class FeedFileCache : IDisposable
    {

        private static List<FileCacheEntry> mEntries;
        private string mCachePath;
        private Dictionary<string, FileSystemWatcher> mWatchers;
        private bool mDisposed;

        public delegate void FilesChangedHandler();
        public event FilesChangedHandler FilesChanged;

        /// <summary>
        /// Index file has a list of file records of the form {filename}|{directory}.
        /// </summary>
        /// <param name="cachePath">Path to the cache file.</param>
        public FeedFileCache(string cachePath)
        {
            mWatchers = new Dictionary<string, FileSystemWatcher>();
            mCachePath = cachePath;
            mEntries = new List<FileCacheEntry>();
            if (File.Exists(mCachePath))
            {
                using (StreamReader indexFile = new StreamReader(cachePath))
                {
                    string line = null;
                    while ((line = indexFile.ReadLine()) != null)
                    {
                        string[] split = line.Split('|');
                        string directory = split[1];
                        string file = split[0];
                        mEntries.Add(new FileCacheEntry(file, directory));
                        AddWatcher(directory);
                    }
                }
                mEntries.Sort();
            }
            else
            {
                File.Create(mCachePath);
            }

            Thread newThread = new Thread(new ThreadStart(CheckFileDeletes));
            newThread.Start();
        }

        private void CheckFileDeletes()
        {
            while (!mDisposed)
            {
                Thread.Sleep(2000);
                List<FileCacheEntry> removals = new List<FileCacheEntry>();
                foreach (FileCacheEntry entry in mEntries)
                {
                    entry.CheckLink();
                    if (!entry.FileExists)
                    {
                        removals.Add(entry);
                    }
                }
                if (removals.Count > 0)
                {
                    foreach (FileCacheEntry entry in removals)
                    {
                        mEntries.Remove(entry);
                    }
                    FilesChanged();
                }
            }
        }

        public FileCacheEntry this[string path]
        {
            get 
            {
                FileCacheEntry returnEntry = null;
                foreach (FileCacheEntry entry in mEntries)
                {
                    if (entry.FilePath == path) {returnEntry = entry;}
                }
                return returnEntry;
            }
        }

        public void Select(FileCacheEntry selected)
        {
            foreach (FileCacheEntry entry in mEntries)
            {
                entry.Selected = false;
            }
            selected.Selected = true;
        }

        public void CheckLinks()
        {
            foreach (FileCacheEntry entry in mEntries)
            {
                entry.CheckLink();
            }
        }

        private void AddWatcher(string directory)
        {
            if (!mWatchers.ContainsKey(directory))
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = directory;
                watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
                watcher.Created += new FileSystemEventHandler(watcher_Created);
                //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                //    NotifyFilters.FileName | NotifyFilters.DirectoryName;
                watcher.EnableRaisingEvents = true;
                mWatchers.Add(directory, watcher);
            }
        }

        private void RemoveWatcher(string directory)
        {
            if (!mWatchers.ContainsKey(directory))
            {
                FileSystemWatcher watcher = mWatchers[directory];
                CleanWatcher(watcher);
                mWatchers.Remove(directory);
            }
        }

        private void CleanWatcher(FileSystemWatcher watcher)
        {
            watcher.Renamed -= new RenamedEventHandler(watcher_Renamed);
            watcher.Created -= new FileSystemEventHandler(watcher_Created);
            watcher.EnableRaisingEvents = false;
        }

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (Exists(e.FullPath))
            {
                FileCacheEntry entry = this[e.FullPath];
                entry.CheckLink();
                FilesChanged();
            }
        }

        void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            this.Remove(e.FullPath);
            if (FilesChanged != null)
            {
                FilesChanged();
            }
            bool othersExist = false;
            string directory = GetDirectoryFromPath(e.FullPath);
            foreach (FileCacheEntry entry in mEntries)
            {
                if (entry.Directory == directory)
                {
                    othersExist = true;
                    break;
                }
            }
            if (!othersExist)
            {
                RemoveWatcher(directory);
            }
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            string oldPath = e.OldFullPath;
            string newPath = e.FullPath;
            if (Exists(oldPath))
            {
                this.Remove(oldPath);
                this.AddFile(newPath);
                this.Sort();
                if (FilesChanged != null)
                {
                    FilesChanged();
                }
            }
            else if (Exists(newPath))
            {
                FileCacheEntry entry = this[newPath];
                entry.CheckLink();
                
                FilesChanged();
            }
        }

        /// <summary>
        /// Serializes all the FileCacheEntry objects and saves them to a flat file.
        /// </summary>
        public void Save()
        {
            using(StreamWriter writer = new StreamWriter(mCachePath, false, Encoding.ASCII))
            {
                foreach (FileCacheEntry entry in mEntries)
                {
                    string line = entry.FileName + "|" + entry.Directory;
                    writer.WriteLine(line);
                }
            }
        }

        public List<FileCacheEntry> Entries
        {
            get { return mEntries; }
        }

        public bool Exists(string path)
        {
            foreach (FileCacheEntry entry in mEntries)
            {
                if (entry.FilePath == path)
                    return true;
            }
            return false;
        }

        public void Remove(string path)
        {
            FileCacheEntry entryToRemove = null;
            foreach (FileCacheEntry entry in mEntries)
            {
                if (entry.FilePath == path)
                {
                    entryToRemove = entry;
                    break;
                }
            }
            if (entryToRemove != null)
                mEntries.Remove(entryToRemove);
        }

        public void SetActiveFile(string filePath)
        {
            foreach (FileCacheEntry entry in mEntries)
            {
                if (entry.FilePath == filePath)
                {
                    entry.IsActive = true;
                }
                else
                {
                    entry.IsActive = false;
                }
            }
        }

        public void ClearActiveSelectedEntry()
        {
            foreach (FileCacheEntry entry in mEntries)
            {
                entry.IsActive = false;
                entry.Selected = false;
            }
        }

        public void AddFile(string filePath)
        {
            FileCacheEntry entry = new FileCacheEntry(filePath);
            mEntries.Add(entry);
            mEntries.Sort();

            //Get the directory path.
            string directory = GetDirectoryFromPath(filePath);
            AddWatcher(directory);
        }

        private string GetDirectoryFromPath(string filePath)
        {
            string[] splits = filePath.Split('/', '\\');
            string directory = string.Join("\\", splits, 0, splits.Length - 1);
            return directory;
        }

        private void AddWithoutSort(string filePath)
        {
            FileCacheEntry entry = new FileCacheEntry(filePath);
            mEntries.Add(entry);
        }

        public void Sort()
        {
            mEntries.Sort();
        }

        public void Dispose()
        {
            foreach (FileSystemWatcher watcher in mWatchers.Values)
            {
                CleanWatcher(watcher);
            }
            mWatchers.Clear();

            mDisposed = true;
        }

    }
}

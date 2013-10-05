using System;
using System.Collections.Generic;
using System.Text;

using Aga.Controls.Tree;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Threading;
using System.Security.AccessControl;

namespace FeedBuilder.FTP
{
	public class FolderBrowserModel: ITreeModel
	{
		private BackgroundWorker _worker;
		private List<BaseItem> _itemsToRead;

		public FolderBrowserModel()
		{
			_itemsToRead = new List<BaseItem>();

			_worker = new BackgroundWorker();
			_worker.WorkerReportsProgress = true;
			_worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
			_worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
		}

		void ReadFilesProperties(object sender, DoWorkEventArgs e)
		{
			while(_itemsToRead.Count > 0)
			{
				BaseItem item = _itemsToRead[0];
				_itemsToRead.RemoveAt(0);

				Thread.Sleep(100); //emulate time consuming operation
				if (item is FolderItem)
				{
					DirectoryInfo info = new DirectoryInfo(item.ItemPath);
					item.Date = info.CreationTime.ToString();
				}
				else if (item is FileItem)
				{
					FileInfo info = new FileInfo(item.ItemPath);
					item.Size = info.Length.ToString();
					item.Date = info.CreationTime.ToString();
					if (info.Extension.ToLower() == ".ico")
					{
						Icon icon = new Icon(item.ItemPath);
						item.Icon = icon.ToBitmap();
					}
					else if (info.Extension.ToLower() == ".bmp")
					{
						item.Icon = new Bitmap(item.ItemPath);
					}
				}
				_worker.ReportProgress(0, item);
			}
		}

		void ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			BaseItem item = e.UserState as BaseItem;
			if (NodesChanged != null)
			{
				TreePath path = GetPath(item.Parent);
				NodesChanged(this, new TreeModelEventArgs(path, new object[]{item}));
			}
		}

		public TreePath GetPath(BaseItem item)
		{
			Stack<object> stack = new Stack<object>();
			while (!(item is RootItem))
			{
				stack.Push(item);
				item = item.Parent;
			}
			return new TreePath(stack.ToArray());
		}

        public TreePath GetPathWithRoot(BaseItem item)
        {
            Stack<object> stack = new Stack<object>();
            while (item != null)
            {
                stack.Push(item);
                item = item.Parent;
            }
            return new TreePath(stack.ToArray());
        }

		public System.Collections.IEnumerable GetChildren(TreePath treePath)
		{
			if (treePath.IsEmpty())
				foreach (string str in Environment.GetLogicalDrives())
				{
                    if (Directory.Exists(str))
                    {
                        DirectoryInfo info = new DirectoryInfo(str);
                        if (info.Attributes != FileAttributes.Hidden)
                        {
                            RootItem item = new RootItem(str);
                            yield return item;
                        }
                    }
				}
			else
			{
				List<BaseItem> items = new List<BaseItem>();
				BaseItem parent = treePath.LastNode as BaseItem;
				if (parent != null)
				{
                    try
                    {
                        foreach (string str in Directory.EnumerateDirectories(parent.ItemPath))
                        {
                            DirectoryInfo info = new DirectoryInfo(str);
                            if (info.Attributes != FileAttributes.Hidden &&
                                info.Attributes != FileAttributes.System)
                            {
                                items.Add(new FolderItem(str, parent));
                            }
                        }
                        foreach (string str in Directory.GetFiles(parent.ItemPath))
                        {
                            DirectoryInfo info = new DirectoryInfo(str);
                            if (info.Attributes != FileAttributes.Hidden &&
                                info.Attributes != FileAttributes.System)
                            {
                                items.Add(new FileItem(str, parent));
                            }
                        }
                        _itemsToRead.AddRange(items);
                        if (!_worker.IsBusy)
                            _worker.RunWorkerAsync();
                    }
                    catch (UnauthorizedAccessException ex)
                    { /* Don't pick up any directories that have access restrictions. */ }

                    foreach (BaseItem item in items)
                        yield return item;
                    
				}
				else
					yield break;
			}
		}

		public bool IsLeaf(TreePath treePath)
		{
			return treePath.LastNode is FileItem;
		}

		public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;
	}
}

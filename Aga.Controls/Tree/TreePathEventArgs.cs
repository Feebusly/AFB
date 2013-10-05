using System;
using System.Collections.Generic;
using System.Text;

namespace Aga.Controls.Tree
{
	public class TreePathEventArgs : EventArgs
	{
		private TreePath _parentPath;
		public TreePath Path
		{
			get { return _parentPath; }
		}

		public TreePathEventArgs()
		{
			_parentPath = new TreePath();
		}

		public TreePathEventArgs(TreePath parentPath)
		{
            if (parentPath == null)
				throw new ArgumentNullException();

            _parentPath = parentPath;
		}
	}
}

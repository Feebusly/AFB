using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Aga.Controls.Tree.NodeControls;
using Aga.Controls.Tree;

namespace FeedBuilder.FTP
{
	public partial class FolderBrowser : UserControl
	{
		private class ToolTipProvider: IToolTipProvider
		{
			public string GetToolTip(TreeNodeAdv node)
			{
				if (node.Tag is RootItem)
					return null;
				else
					return "Double click to rename node";
			}
		}

		public FolderBrowser()
		{
			InitializeComponent();
			
			_name.ToolTipProvider = new ToolTipProvider();
			_name.EditorShowing += new CancelEventHandler(_name_EditorShowing);

			_treeView.Model = new FolderBrowserModel();
		}

		void _name_EditorShowing(object sender, CancelEventArgs e)
		{
			if (_treeView.CurrentNode.Tag is RootItem)
				e.Cancel = true;
		}

        public void SelectNode(TreePath path)
        {
            ExpandNodes(path);
            TreeNodeAdv bottomNode = null;
            foreach (TreeNodeAdv node in _treeView.FindNodes(path))
            {
                bottomNode = node;
            }
            _treeView.SelectedNode = bottomNode;
        }

        public void ExpandNodes(TreePath path)
        {
            if (path != null)
            {
                foreach (TreeNodeAdv node in _treeView.FindNodes(path))
                {
                    if (node != null)
                    {
                        node.IsExpanded = true;
                    }
                }
            }
        }

        public TreeViewAdv TreeView
        {
            get { return _treeView; }
        }
	}
}

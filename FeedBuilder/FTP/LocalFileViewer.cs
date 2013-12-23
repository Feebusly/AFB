using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aga.Controls.Tree;
using FeedBuilder.FTP;
using Aga.Controls.Tree.NodeControls;

namespace FeedBuilder
{
    public partial class LocalFileViewer : UserControl
    {

        private TreeModel mTreeModel;
        private bool mItemChecked = false;
        private FeedData mFeedData;
        private bool mRefreshing;

        public const string FTP_TAB_FEED_FILE_TEXT = "Feed XML File";
        public const string FTP_TAB_FEED_IMAGE_TEXT = "Feed Image";
        public const string FTP_TAB_SOUND_FILES_TEXT = "Sound Files";
        public const string FTP_TAB_HTML_NODE_TEXT = "HTML Transform";

        public delegate void NodeCheckedHandler(Node node, CheckState checkState);
        public event NodeCheckedHandler NodeChecked;

        public delegate void ExplorerFileSelectionHandler(string pathToFile);
        /// <summary>
        /// Sends the path of the newly selected exporer file, or null if selection was cleared.
        /// </summary>
        public event ExplorerFileSelectionHandler ExplorerFileSelected;

        public LocalFileViewer()
        {
            InitializeComponent();
            mTreeModel = new TreeModel();
            mFeedUploadParts.Model = mTreeModel;
            mTreeModel.NodesChanged += new EventHandler<TreeModelEventArgs>(mTreeModel_NodesChanged);
           // mEditControl_NodeEdited = new EditableControl.NodeEditedHander(editControl_NodeEdited);
            mFeedFilesOrExplorer.SelectedIndex = 0;
            mExplorer.TreeView.SelectionChanged += new EventHandler(mExplorer_SelectionChanged);
        }

        public void RefreshFileViewer(FeedData feedData)
        {
            mRefreshing = true;

            mFeedData = feedData;

            mTreeModel.Nodes.Clear();

            if (mFeedData.ItunesFeed)
            {
                mNonItunesCheckBox.Enabled = true;
                mNonItunesCheckBox.Checked = mFeedData.FtpServerPrefUploadNonItunes;
            }
            else
            {
                mNonItunesCheckBox.Checked = false;
                mNonItunesCheckBox.Enabled = false;
            }

            //Windows Explorer control.
            TreePath explorerPath = mFeedData.ExplorerTreePath;
            if (explorerPath != null)
            {
                mExplorer.SelectNode(explorerPath);
            }

            //Podcast Files Root
            Node fileNode = new Node(FTP_TAB_FEED_FILE_TEXT);
            fileNode.Tag = new FtpNodeTag(NodeTypes.RootNode, null);
            mTreeModel.Nodes.Add(fileNode);

            //Image Node Root
            Node imageNode = new Node(FTP_TAB_FEED_IMAGE_TEXT);
            imageNode.Tag = new FtpNodeTag(NodeTypes.RootNode, null); 
            mTreeModel.Nodes.Add(imageNode);

            //Content Files Node Root
            Node mp3sNode = new Node(FTP_TAB_SOUND_FILES_TEXT);
            mp3sNode.Tag = new FtpNodeTag(NodeTypes.RootNode, null); 
            mTreeModel.Nodes.Add(mp3sNode);

            //HTML File Node Root
            Node transformNode = new Node(FTP_TAB_HTML_NODE_TEXT);
            transformNode.Tag = new FtpNodeTag(NodeTypes.RootNode, null); 
            mTreeModel.Nodes.Add(transformNode);

            //First Podcast File Child With Items
            bool isItunesFeed = mFeedData.ItunesFeed;
            string fileName = (mFeedData.ItunesFeed) ? mFeedData.FtpServerPathItunesFile : mFeedData.FtpServerPathNonItunesFile;
            Node fileNodeChild = new Node(fileName);
            fileNode.Nodes.Add(fileNodeChild);
            NodeTypes fileNodeType = (isItunesFeed) ? NodeTypes.ItunesFeedNode : NodeTypes.NonItunesFeedNode;
            fileNodeChild.Tag = new FtpNodeTag(fileNodeType, mFeedData.FeedPath);
            foreach (FeedItem item in mFeedData.FeedItems)
            {
                Node itemNode = new Node(item.Title + " : " + item.PubDate.ToShortDateString());
                NodeTypes itemType = (isItunesFeed) ? NodeTypes.ItunesItemNode : NodeTypes.NonItunesItemNode;
                itemNode.Tag = new FtpNodeTag(itemType, item);
                fileNodeChild.Nodes.Add(itemNode);
            }

            //Second Podcast File Child With Items (if this is an itunes feed and the checkbox is checked).
            if (mNonItunesCheckBox.Checked && isItunesFeed)
            {
                if (mFeedData.FtpServerPathNonItunesFile == null)
                {
                    string newFileName = mFeedData.FeedFileName;
                    int extensionPosition = newFileName.LastIndexOf(".");
                    newFileName.Insert(extensionPosition-1, "(nonItunes)");
                    mFeedData.FtpServerPathNonItunesFile = newFileName;
                }
                Node nonItunesChild = new Node(mFeedData.FtpServerPathNonItunesFile);
                fileNode.Nodes.Add(nonItunesChild);
                nonItunesChild.Tag = new FtpNodeTag(NodeTypes.NonItunesFeedNode, mFeedData.FeedPath);
                foreach (FeedItem item in mFeedData.FeedItems)
                {
                    Node itemNode = new Node(item.Title + " : " + item.PubDate.ToShortDateString());
                    itemNode.Tag = new FtpNodeTag(NodeTypes.NonItunesItemNode, item);
                    nonItunesChild.Nodes.Add(itemNode);
                }
            }

            //Image Node Child
            if (mFeedData.LocalImagePath != null)
            {
                Node imgNodeChild = new Node(mFeedData.LocalImageFileName);
                imageNode.Nodes.Add(imgNodeChild);
                imgNodeChild.Tag = new FtpNodeTag(NodeTypes.ImageNode, mFeedData.LocalImagePath);
            }

            //Content Files
            if (mFeedData.FeedItems.Count > 0)
            {
                foreach (FeedItem feedItem in mFeedData.FeedItems)
                {
                    if (feedItem.EnclosurePath != null)
                    {
                        string nodeText = string.Format("{0} : {1}", feedItem.Title, feedItem.EnclosureName);
                        Node contentNode = new Node(nodeText);
                        mp3sNode.Nodes.Add(contentNode);
                        contentNode.Tag = new FtpNodeTag(NodeTypes.ContentNode, feedItem.EnclosurePath);
                    }
                }
            }

            //HTML Transform File
            if (mFeedData.XsltOutput != null)
            {
                string xslOutputPath = mFeedData.XsltOutputPath;
                if (xslOutputPath != null)
                {
                    string[] splits = xslOutputPath.Split('\\');
                    string filename = splits[splits.Length - 1];

                    Node transformNodeChild = new Node(mFeedData.FtpHtmlFileName);
                    transformNodeChild.Tag = new FtpNodeTag(NodeTypes.HtmlNode, mFeedData.XsltOutputPath);
                    transformNode.Nodes.Add(transformNodeChild);
                }
            }

            mFeedUploadParts.Refresh();
            mRefreshing = false;
        }

        public List<Node> AllNodesOfType(NodeTypes type)
        {
            List<Node> nodes = new List<Node>();
            foreach (Node node in AllNodes)
            {
                FtpNodeTag tag = node.Tag as FtpNodeTag;
                if (tag != null && (tag.NodeType & type) > 0)
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        /// <summary>
        /// Returns true when explorer is the active local file viewer.
        /// </summary>
        public bool ExplorerMode
        {
            get { return mExplorer.Enabled; }
        }

        private string mSelectedExplorerPath;
        public string SelectedExplorerPath
        {
            get
            {
                return mSelectedExplorerPath;
            }
        }

        void mExplorer_SelectionChanged(object sender, EventArgs e)
        {
            mSelectedExplorerPath = null;
            //Might be clearing the selection.
            if (mExplorer.TreeView.SelectedNode != null)
            {
                BaseItem selectedNode = mExplorer.TreeView.SelectedNode.Tag as BaseItem;
                if (selectedNode != null)
                {
                    FolderBrowserModel model = mExplorer.TreeView.Model as FolderBrowserModel;
                    TreePath selectedTreePath = model.GetPathWithRoot(selectedNode);
                    mFeedData.ExplorerTreePath = selectedTreePath;
                }

                FileItem file = mExplorer.TreeView.SelectedNode.Tag as FileItem;
                if (file != null)
                {
                    mSelectedExplorerPath = file.ItemPath;
                    if (ExplorerFileSelected != null)
                        ExplorerFileSelected(mSelectedExplorerPath);
                }
                else if (ExplorerFileSelected != null)
                    ExplorerFileSelected(null);
            }
            else if (ExplorerFileSelected != null)
                ExplorerFileSelected(null);
        }

        public List<Node> CheckedNodesOfType(NodeTypes type)
        {
            List<Node> nodes = new List<Node>();
            foreach (Node node in CheckedNodes)
            {
                FtpNodeTag tag = node.Tag as FtpNodeTag;
                if (tag != null && (tag.NodeType & type) > 0)
                {
                    nodes.Add(node);
                }
            }
            return nodes;
        }

        public System.Collections.ObjectModel.Collection<Node> AllNodes
        {
            get 
            {
                return (mFeedUploadParts.Model as TreeModel).AllNodes;
            }
        }

        public TreeModel TreeModel
        {
            get
            {
                return mFeedUploadParts.Model as TreeModel;
            }
        }

        public List<Node> CheckedNodes
        {
            get 
            {
                List<Node> nodes = new List<Node>();
                foreach (Node node in (mFeedUploadParts.Model as TreeModel).AllNodes)
                {
                    if (node.IsChecked)
                    {
                        nodes.Add(node);
                    }
                }
                return nodes;
            }
        }

        public bool ItemChecked
        {
            get { return mItemChecked; }
            set { mItemChecked = value; }
        }

        private void mNonItunesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mRefreshing)
                return;

            mFeedData.FtpServerPrefUploadNonItunes = mNonItunesCheckBox.Checked;
            RefreshFileViewer(mFeedData);
        }

        private void mFeedUploadParts_MouseUp(object sender, MouseEventArgs e)
        {
            TreeViewAdv treeView = sender as TreeViewAdv;
            TreeNodeAdv nodeAdv = treeView.GetNodeAt(new Point(e.X, e.Y));
            if (nodeAdv != null)
            {
                Node node = nodeAdv.Tag as Node;
                if (node != null)
                {
                    NodeChecked(node, node.CheckState);
                }
            }
            if (FTPSelectionExists())
                mItemChecked = true;
            else
                mItemChecked = false;
        }

        private bool FTPSelectionExists()
        {
            int checkedCount = 0;
            foreach (Node node in mTreeModel.AllNodes)
            {
                if (node.IsChecked)
                    ++checkedCount;
            }
            return checkedCount > 0;
        }

        private void mFeedUploadParts_DoubleClick(object sender, EventArgs e)
        {
            //Select the clicked node, and the next five below it.
            MouseEventArgs mouseEvent = e as MouseEventArgs;
            Point p = new Point(mouseEvent.X, mouseEvent.Y);
            TreeNodeAdv nextNode = mFeedUploadParts.GetNodeAt(p);

            if (nextNode != null)
            {
                Node node = nextNode.Tag as Node;

                //Should not do this for the top level nodes.  ...maybe.
                if (node.Parent != null)
                {
                    CheckState newState = CheckState.Checked;
                    if (node.CheckState == CheckState.Checked)
                        newState = CheckState.Unchecked;

                    for (int i = 0; i < 5; i++)
                    {
                        if (node != null)
                        {
                            //node.CheckState = newState;
                            ChangeCheckState(node, newState);
                        }

                        nextNode = nextNode.NextNode;
                        if (nextNode != null)
                            node = nextNode.Tag as Node;
                        else
                            break;
                    }
                }
            }
        }

        private void mFeedUploadParts_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                //figure out which node was clicked
                TreeViewAdv treeView = sender as TreeViewAdv;
                if (treeView != null)
                {
                    TreeNodeAdv selectedNodeAdv = treeView.SelectedNode;
                    if (selectedNodeAdv != null)
                    {
                        Node selectedNode = selectedNodeAdv.Tag as Node;
                        FtpNodeTag tag = selectedNode.Tag as FtpNodeTag;

                        if (tag != null)
                        {
                            Point mousePoint = new Point(e.X, e.Y);
                            mContextMenuLocalFiles.Items.Clear();
                            if (tag.NodeType == NodeTypes.ItunesFeedNode ||
                                tag.NodeType == NodeTypes.NonItunesFeedNode ||
                                tag.NodeType == NodeTypes.HtmlNode)
                            {
                                ToolStripItem item = mContextMenuLocalFiles.Items.Add("Rename File");
                                mContextMenuLocalFiles.Show(mFeedUploadParts, mousePoint);
                                item.Click += new EventHandler(renameFile_Click);
                            }
                        }
                    }
                }
            }
        }

        private void renameFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            switch (menuItem.Text)
            {
                case "Rename File":
                    TreeNodeAdv nodeAdv = mFeedUploadParts.SelectedNode;
                    Node node = nodeAdv.Tag as Node;
                    NodeControl control = mFeedUploadParts.GetNodeControl(nodeAdv, NodeControlInfoTypes.TextBox).Control;
                    EditableControl editControl = control as EditableControl;
                    editControl.EditEnabled = true;
                   // mTreeModel.NodesChanged += new EventHandler<TreeModelEventArgs>(mTreeModel_NodesChanged);
                    editControl.BeginEdit();
                    break;
                default:
                    break;
            }
        }

        //void editControl_NodeEdited(TreeNodeAdv nodeAdv)
        //{
        //    if (nodeAdv == null)
        //        return;

        //    //Change the appropriate FeedData object.
        //    Node node = nodeAdv.Tag as Node;
        //    FtpNodeTag tag = node.Tag as FtpNodeTag;
        //    string newName = node.Text;
            
        //    switch (tag.NodeType)
        //    {
        //        case NodeTypes.ItunesFeedNode:
        //            mFeedData.FtpServerPathItunesFile = newName;
        //            break;
        //        case NodeTypes.NonItunesFeedNode:
        //            mFeedData.FtpServerPathNonItunesFile = newName;
        //            break;
        //        case NodeTypes.HtmlNode:
        //            mFeedData.FtpServerPathHtmlFile = newName;
        //            break;
        //        default:
        //            break;
        //    }

        //    //Remove event handler so they don't start piling up.
        //    NodeControl control = mFeedUploadParts.GetNodeControl(nodeAdv, NodeControlInfoTypes.TextBox).Control;
        //    EditableControl editControl = control as EditableControl;
        //    editControl.EditEnabled = false;
        //    editControl.NodeEdited -= mEditControl_NodeEdited;
        //}

        private List<string> GetUploadFiles()
        {
            List<string> uploadFilePaths = new List<string>();
            //Look at primary xml file.
            Node feedFileNode = mTreeModel.Nodes[0].Nodes[0];
            if (feedFileNode.CheckState == CheckState.Checked)
            {
                uploadFilePaths.Add(feedFileNode.Tag as string);
            }
            else if (GetCollectiveChildrenState(feedFileNode) == CheckState.Indeterminate)
            {

            }
            return uploadFilePaths;
        }

        #region Nodes Changed Recursion

        private bool mCheckStateChanging;
        void mTreeModel_NodesChanged(object sender, TreeModelEventArgs e)
        {
            //Change the appropriate FeedData object.
            if (e.ChangeType == NodeChangeTypes.TextChanged)
            {
                foreach (Node changedNode in e.Children)
                {
                    TreeNodeAdv nodeAdv = mFeedUploadParts.SelectedNode;
                    NodeControl control = mFeedUploadParts.GetNodeControl(
                        nodeAdv, NodeControlInfoTypes.TextBox).Control;
                    EditableControl editControl = control as EditableControl;
                    editControl.EditEnabled = false;

                    FtpNodeTag tag = changedNode.Tag as FtpNodeTag;
                    string newName = changedNode.Text;

                    switch (tag.NodeType)
                    {
                        case NodeTypes.ItunesFeedNode:
                            mFeedData.FtpServerPathItunesFile = newName;
                            break;
                        case NodeTypes.NonItunesFeedNode:
                            mFeedData.FtpServerPathNonItunesFile = newName;
                            break;
                        case NodeTypes.HtmlNode:
                            mFeedData.FtpServerPathHtmlFile = newName;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (e.ChangeType == NodeChangeTypes.CheckStateChanged)
            {
                if (!mCheckStateChanging)
                {
                    mCheckStateChanging = true;

                    foreach (Node changedNode in e.Children)
                    {
                        SetSubTreeCheckState(changedNode, changedNode.CheckState);
                        RecurseParentCheckState(changedNode.Parent);
                    }

                    mCheckStateChanging = false;
                }
            }
        }

        /// <summary>
        /// Looks up the node tree, setting checked states to each parent until the root
        /// is reached.
        /// </summary>
        /// <param name="node">The node to set a checked state on.</param>
        private void RecurseParentCheckState(Node node)
        {
            if (node != null)
            {
                //node.CheckState = GetCollectiveChildrenState(node);
                ChangeCheckState(node, GetCollectiveChildrenState(node));
                RecurseParentCheckState(node.Parent);
            }
        }

        /// <summary>
        /// Counts the number of children and the number of children with a CheckState.Checked
        /// value.  These two numbers can then be used for comparison to determine whether the
        /// passed in node should be checked, unchecked, or indeterminate based on it descendent
        /// states.
        /// </summary>
        /// <param name="node">The node to examine descendents on.</param>
        /// <param name="childCount">Number of descendents.</param>
        /// <param name="checkedCount">Number of descendents with a checked state.</param>
        private void CountCheckedDescendents(Node node, ref int childCount, ref int checkedCount)
        {
            foreach (Node child in node.Nodes)
            {
                ++childCount;
                if (child.CheckState == CheckState.Checked)
                    ++checkedCount;

                if (node.Nodes != null && node.Nodes.Count > 0)
                {
                    foreach (Node grandChild in child.Nodes)
                    {
                        CountCheckedDescendents(grandChild, ref childCount, ref checkedCount);
                    }
                }
            }
        }

        /// <summary>
        /// Method returns checked if all children are checked, unchecked if all children
        /// are unchecked, and Indeterminate if some children are checked and some are not.
        /// </summary>
        /// <param name="node">The node to start testing at.  This node itself is not
        /// tested.  It is assumed that the caller has already tested this node.</param>
        /// <returns>Checked if all children are checked, unchecked if all children
        /// are unchecked, and Indeterminate if some children are checked and some 
        /// are not.</returns>
        private CheckState GetCollectiveChildrenState(Node node)
        {
            CheckState collectiveState = CheckState.Unchecked;
            int checkedCount = 0;
            int childrenCount = 0;
            if (node != null)
            {
                foreach (Node child in node.Nodes)
                {
                    ++childrenCount;
                    CheckState thisChildState = child.CheckState;
                    if (thisChildState == CheckState.Checked || thisChildState == CheckState.Indeterminate)
                        ++checkedCount;

                    CountCheckedDescendents(child, ref childrenCount, ref checkedCount);
                }
            }
            if (checkedCount > 0)
            {
                collectiveState = CheckState.Indeterminate;
                if (childrenCount == checkedCount)
                    collectiveState = CheckState.Checked;
            }
            return collectiveState;
        }

        /// <summary>
        /// Sets a particulare checked state to this node and all of it's descendents.
        /// </summary>
        /// <param name="node">The base of the tree to start setting checked states on.</param>
        /// <param name="state">The new CheckState value to apply to this tree.</param>
        private void SetSubTreeCheckState(Node node, CheckState state)
        {
            ChangeCheckState(node, state);
            if (node.Nodes != null && node.Nodes.Count > 0)
            {
                foreach (Node child in node.Nodes)
                {
                    ChangeCheckState(child, state);
                    SetSubTreeCheckState(child, state);
                }
            }
        }

        /// <summary>
        /// Change the node's checkstate and alter the FeedItem's CheckedForUpload property,
        /// if the nodes' tag contains a FeedItem.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="state"></param>
        private void ChangeCheckState(Node node, CheckState state)
        {
            node.CheckState = state;
            bool taggedForUpload = (state == CheckState.Checked);

            FtpNodeTag tag = node.Tag as FtpNodeTag;
            if (tag != null)
            {
                FeedItem item = tag.NodeObject as FeedItem;
                if (item != null)
                {
                    item.TaggedForUpload = taggedForUpload;
                }
            }
        }
        #endregion

        private void mFeedFilesOrExplorer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mFeedFilesOrExplorer.SelectedIndex == 0)
            {
                mFeedUploadParts.Visible = true;
                mFeedUploadParts.Enabled = true;
                mExplorer.Visible = false;
                mExplorer.Enabled = false;
                mNonItunesCheckBox.Enabled = true;
            }
            else if (mFeedFilesOrExplorer.SelectedIndex == 1)
            {
                mFeedUploadParts.Visible = false;
                mFeedUploadParts.Enabled = false;
                mExplorer.Visible = true;
                mExplorer.Enabled = true;
                mNonItunesCheckBox.Enabled = false;
            }
        }


    }
}

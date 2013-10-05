using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedBuilder.FTP
{
    public enum NodeTypes
    {
        RootNode = 1,
        ContainerNode = 1 << 1,
        ItunesFeedNode = 1 << 2,
        NonItunesFeedNode = 1 << 3,
        ItunesItemNode = 1 << 4,
        NonItunesItemNode = 1 << 5,
        ImageNode = 1 << 6,
        ContentNode = 1 << 7,
        HtmlNode = 1 << 8
    }

    /// <summary>
    /// A class for objects attached to local file nodes.  The LocalFileViewer class uses 
    /// this to encapsulate data in the Node.Tag elements contained in the TreeNodeAdv control.
    /// </summary>
    class FtpNodeTag
    {
        private NodeTypes mNodeType;
        private object mNodeObject;

        public FtpNodeTag(NodeTypes nodeType, object nodeObject)
        {
            mNodeType = nodeType;
            mNodeObject = nodeObject;
        }

        public NodeTypes NodeType
        {
            get { return mNodeType; }
            set { mNodeType = value; }
        }

        public object NodeObject
        {
            get { return mNodeObject; }
            set { mNodeObject = value; }
        }
    }
}

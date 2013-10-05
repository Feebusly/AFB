using System;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace Aga.Controls.Tree
{
   
	public class TreePath
	{
		public static readonly TreePath Empty = new TreePath();

		private object[] _path;
		public object[] FullPath
		{
			get { return _path; }
		}

		public object LastNode
		{
			get
			{
				if (_path.Length > 0)
					return _path[_path.Length - 1];
				else
					return null;
			}
		}

		public object FirstNode
		{
			get
			{
				if (_path.Length > 0)
					return _path[0];
				else
					return null;
			}
		}

		public TreePath()
		{
			_path = new object[0];
		}

		public TreePath(object node)
		{
			_path = new object[] { node };
		}

		public TreePath(object[] path)
		{
			_path = path;
		}

		public bool IsEmpty()
		{
			return (_path.Length == 0);
		}

        public string SerializePath()
        {
            StringBuilder serializedPathBuilder = new StringBuilder();
            serializedPathBuilder.Append("<path>");
            foreach (object pathObject in _path)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(stream, pathObject);
                    serializedPathBuilder.Append("<pathobject>");
                    serializedPathBuilder.Append(Convert.ToBase64String(stream.ToArray()));
                    serializedPathBuilder.Append("</pathobject>");
                }
            }
            serializedPathBuilder.Append("</path>");
            string serializedPath = serializedPathBuilder.ToString();
            return serializedPath;
        }

        /// <summary>
        /// Creates a tree path from the serialized string created by the SeralizePath() method.
        /// </summary>
        /// <param name="treePathBytes">The delimited set of bytes, converted to strings,
        /// that came from the SerializePath() method.</param>
        /// <returns></returns>
        public static TreePath Deserialize(string treePathBytes)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(treePathBytes);
            XmlNodeList nodes = doc.SelectNodes("//pathobject");
            object[] paths = new object[nodes.Count];
            BinaryFormatter formatter = new BinaryFormatter();
            int i = 0;
            foreach (XmlNode node in nodes)
            {
                string byteString = node.InnerText;
                byte[] bytes = Convert.FromBase64String(byteString);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    object pathObj = formatter.Deserialize(stream);
                    paths[i++] = pathObj;
                }
            }
            return new TreePath(paths);
        }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FeedBuilder
{

    /// <summary>
    /// Updates data in the XML Feed item element as well as the local xml document containing additional 
    /// feed info.
    /// </summary>
    /// <remarks>
    /// XML Feed item element schema is:
    /// <item>
    ///    <title>Finding a Life Coach</title>
    ///    <description>Life Coach Series</description>
    ///    <link>http://www.crossroads-ridgecrest.org/Websites/crossroadscommunity/images/Sermons/SermonAudio2012/11-25-2012.mp3</link>
    ///    <enclosure url="http://www.crossroads-ridgecrest.org/Websites/crossroadscommunity/images/Sermons/SermonAudio2012/11-25-2012.mp3" length="10202700" type="audio/mpeg"></enclosure>
    ///    <guid isPermaLink="false">fe655426-f6df-4b9a-8080-8271385892e9</guid>
    ///    <pubDate>Sun, 25 Nov 2012 00:00:00 PST</pubDate>
    ///    <itunes:subtitle>Finding a Life Coach</itunes:subtitle>
    ///    <itunes:summary>Life Coach Series</itunes:summary>
    ///    <itunes:duration>00:42:30</itunes:duration>
    ///    <itunes:author>Bill Corley</itunes:author>
    ///    <itunes:explicit>no</itunes:explicit>
    ///</item>
    /// 
    /// XML Additional Info schema is:
    /// <FeedInfo>
    ///     <LocalFeedPath>C:\users\me\myfeed.xml</LocalPath>
    ///     <FTPServerURL>ftp://feedserver.net</FTPServerURL>
    ///     <FTPServerPath>/podcasts</FTPServerPath> <!--optional information-->
    ///     <FTPUsername>myname</FTPUsername>
    ///     <FTPPassword>feqj890fghsivoe</FTPPassword> <!--encrypted password-->
    ///     <LocalFeedImagePath>C:\users\me\My Pictures\MyFeedImage.jpg</LocalFeedImagePath>
    ///     <FeedItem guid="fe655426-f6df-4b9a-8080-8271385892e9">
    ///         <SoundFilePath>C:\users\me\MP3Dir\soundbytes1.mp3</SoundFilePath>
    ///     </FeedItem>
    /// </FeedInfo>
    /// 
    /// FeedItemNode is linked to item node by the guid.
    /// The FeedItem is searched for by the following xpath:  
    ///    //FeedInfo/FeedItem[@guid='fe655426-f6df-4b9a-8080-8271385892e9']
    /// </remarks>
    public class FeedItem : IDisposable, IComparable
    {
        private string mSoundFilePath;
        private FeedData mParent;

        private XmlNode mFeedItemNode;
        private XmlDocument mAdditionalFeedDataXml;

        public const string TITLE_XPATH = "title";
        public const string DESC_XPATH = "description";
        public const string LINK_XPATH = "link";
        public const string ENCLOSURE_XPATH = "enclosure";
        public const string GUID_XPATH = "guid";
        public const string PUB_DATE_XPATH = "pubDate";
        public const string SUBTITLE_XPATH = "itunes:subtitle";
        public const string SUMMARY_XPATH = "itunes:summary";
        public const string DURATION_XPATH = "itunes:duration";
        public const string AUTHOR_XPATH = "itunes:author";
        public const string EXPLICIT_XPATH = "itunes:explicit";

        private const string XPATH_FEED_INFO = "//FeedInfo";
        private const string XPATH_FEED_ITEM = "FeedItem";
        private const string XPATH_ITEM_SEARCH = "//FeedInfo/FeedItem[@guid='{0}']";
        private const string XPATH_SOUND_FILE = "SoundFilePath";

        private bool mTaggedForUpload;

        private XmlNamespaceManager mNamespaceMgr;

        public FeedItem(FeedData parent, XmlNamespaceManager namespaceMgr, XmlNode itemNode,
            string guid, string soundFilePath)
        {
            mParent = parent;
            mAdditionalFeedDataXml = mParent.AdditionalFeedDataXmlDoc;
            mNamespaceMgr = namespaceMgr;
            mFeedItemNode = itemNode;
            mSoundFilePath = soundFilePath;

            //Look for FeedItem element
            string xpath_guid = string.Format(XPATH_ITEM_SEARCH, guid);
            XmlNode feedItemNode = mAdditionalFeedDataXml.SelectSingleNode(xpath_guid);
            if (feedItemNode == null)
            {
                feedItemNode = mAdditionalFeedDataXml.CreateNode(XmlNodeType.Element, XPATH_FEED_ITEM, null);
                XmlAttribute guidAttribute = mAdditionalFeedDataXml.CreateAttribute("guid");
                guidAttribute.Value = guid;
                feedItemNode.Attributes.Append(guidAttribute);
                mAdditionalFeedDataXml.SelectSingleNode(XPATH_FEED_INFO).AppendChild(feedItemNode);
            }

            //See if we have a sound file.
            XmlNode soundFileNode;
            if ((soundFileNode = feedItemNode.SelectSingleNode(XPATH_SOUND_FILE)) != null)
            {
                mSoundFilePath = soundFileNode.InnerText;
            }

            //Add a new Guid
            this.GUID = guid;
        }

        /// <summary>
        /// Removes this item data (indexed by it's guid) from the local data cache.  Call this method
        /// if you are removing a feed item.
        /// </summary>
        public void CleanFromLocalData()
        {
            string xpath_guid = string.Format(XPATH_ITEM_SEARCH, GUID);
            XmlNode feedItemNode = mAdditionalFeedDataXml.SelectSingleNode(xpath_guid);
            if (feedItemNode != null)
                feedItemNode.ParentNode.RemoveChild(feedItemNode);
        }

        /// <summary>
        /// The XML Feed item node containing the feed data for this FeedItem object.
        /// </summary>
        public XmlNode Node
        {
            get { return mFeedItemNode; }
            set { mFeedItemNode = value; }
        }

        /// <summary>
        /// If true, then this item will be saved to the temporary upload file.
        /// </summary>
        public bool TaggedForUpload
        {
            get { return mTaggedForUpload; }
            set { mTaggedForUpload = value; }
        }

        public string Title
        {
            get { return GetFeedElementValue(TITLE_XPATH); }
            set
            {
                //mFeedItemNode.SelectSingleNode(TITLE_XPATH).InnerText = value;
                SetNodeInnerText(TITLE_XPATH, value);
                mParent.Dirty = true; 
            }
        }

        public string SubTitle
        {
            get { return GetFeedElementValue(SUBTITLE_XPATH); }
            set 
            {
                //mFeedItemNode.SelectSingleNode(SUBTITLE_XPATH, mNamespaceMgr).InnerText = value; 
                SetNodeInnerText("itunes", SUBTITLE_XPATH, value, mNamespaceMgr);
                mParent.Dirty = true; 
            }
        }

        public string Summary
        {
            get { return GetFeedElementValue(SUMMARY_XPATH); }
            set
            { 
                //mFeedItemNode.SelectSingleNode(SUMMARY_XPATH, mNamespaceMgr).InnerText = value; 
                SetNodeInnerText("itunes", SUMMARY_XPATH, value, mNamespaceMgr);
                mParent.Dirty = true; 
            }
        }

        public TimeSpan Duration
        {
            get 
            {
                string duration = null;
                if (mNamespaceMgr.HasNamespace("itunes"))
                    duration = mFeedItemNode.SelectSingleNode("itunes:duration", mNamespaceMgr).InnerText;
                int hours = 0;
                int minutes = 0;
                int seconds = 0;
                if (duration != null  && duration != string.Empty)
                {
                    string[] splits = duration.Split(':');
                    int.TryParse(splits[0], out hours);
                    int.TryParse(splits[1], out minutes);
                    int.TryParse(splits[2], out seconds);
                }
                TimeSpan ts = new TimeSpan(hours, minutes, seconds);
                return ts; 
            }
            set 
            {
                TimeSpan ts = value;
                //mFeedItemNode.SelectSingleNode("itunes:duration", mNamespaceMgr).InnerText = 
                //    string.Format("{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);
                SetNodeInnerText("itunes", DURATION_XPATH,
                    string.Format("{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds), mNamespaceMgr);
                mParent.Dirty = true;
            }
        }

        public string DurationString
        {
            get { return GetFeedElementValue(DURATION_XPATH); }
            set 
            { 
                //Make sure we can parse the string first.
                string[] hrminsec = value.Split(':');
                int.Parse(hrminsec[0]);
                int.Parse(hrminsec[1]);
                int.Parse(hrminsec[2]);
                if (hrminsec.Length > 3)
                {
                    throw new Exception("Error, could not parse hr:min:sec data from string.");
                }
                //mFeedItemNode.SelectSingleNode("itunes:duration", mNamespaceMgr).InnerText = value;
                SetNodeInnerText("itunes", DURATION_XPATH, value, mNamespaceMgr);
                mParent.Dirty = true;
            }
        }

        public string Author
        {
            get  { return GetFeedElementValue(AUTHOR_XPATH); }
            set 
            {
                //mFeedItemNode.SelectSingleNode(AUTHOR_XPATH, mNamespaceMgr).InnerText = value;
                SetNodeInnerText("itunes", AUTHOR_XPATH, value, mNamespaceMgr);
                mParent.Dirty = true;
            }
        }

        public string Explicit
        {
            get  { return GetFeedElementValue(EXPLICIT_XPATH); }
            set 
            {
                //mFeedItemNode.SelectSingleNode(EXPLICIT_XPATH, mNamespaceMgr).InnerText = value.ToString();
                SetNodeInnerText("itunes", EXPLICIT_XPATH, value, mNamespaceMgr);
                mParent.Dirty = true;
            }
        }

        public string Description
        {
            get  { return GetFeedElementValue(DESC_XPATH); }
            set 
            {
                SetNodeInnerText(DESC_XPATH, value);
                mParent.Dirty = true;
            }
        }

        public bool HasPubDate()
        {
            XmlNode pubDate = mFeedItemNode.SelectSingleNode(PUB_DATE_XPATH);
            bool hasDate = false;
            if (pubDate != null)
            {
                string date = pubDate.InnerText;
                if (date != null && date != string.Empty)
                    hasDate = true;
            }
            return hasDate;
        }

        public DateTime PubDate
        {
            get 
            {
                string pubDateString = mFeedItemNode.SelectSingleNode(PUB_DATE_XPATH).InnerText;
                DateTime dt = DateTime.Now;
                FeedValidator.TryParseDateTime(pubDateString, out dt);
                return dt;
            }
            set
            {
                string dateString = null;

                if (mParent.TimeZoneOffset != null)
                {
                    Regex re = new Regex(@"[+-]\s*[0-9:]+");
                    Match match = re.Match(mParent.TimeZoneOffset);
                    if (match != null && match.Value != null && match.Value != string.Empty)
                    {
                        dateString = value.ToString("ddd, dd MMM yyyy HH:mm:ss ") + match.Value;
                    }
                }

                if (dateString == null)
                {
                    dateString = value.ToString("ddd, dd MMM yyyy HH:mm:ss zzzz");
                }

                int lastColonIndex = dateString.LastIndexOf(':');
                dateString = dateString.Remove(lastColonIndex, 1);
                SetNodeInnerText(PUB_DATE_XPATH, dateString);
                mParent.Dirty = true;
            }
        }

        public string Link
        {
            get  { return GetFeedElementValue(LINK_XPATH); }
            set 
            {
                SetNodeInnerText(LINK_XPATH, value);
                mParent.Dirty = true;
            }
        }

        /// <summary>
        /// Get or set the enclosure URL.  Setting the URL also has the side affect of setting
        /// the enclosure type per the following.
        ///  File	Type
        ///  .mp3	audio/mpeg
        ///  .m4a	audio/x-m4a
        ///  .mp4	video/mp4
        ///  .m4v	video/x-m4v
        ///  .mov	video/quicktime
        ///  .pdf	application/pdf
        ///  .epub	document/x-epub
        /// </summary>
        public string EnclosureURL
        {
            get 
            {
                XmlNode node = mFeedItemNode.SelectSingleNode(ENCLOSURE_XPATH);
                string url = null;
                if (node != null)
                {
                    XmlAttribute attr = node.Attributes["url"];
                    if (attr != null)
                        url = attr.Value;
                }
                return url; 
            }
            set 
            {
                if (value != null && value != string.Empty)
                {
                    string[] urlSplit = value.Split(new char[] { '.' }, 
                        StringSplitOptions.RemoveEmptyEntries);
                    string extenstion = urlSplit[urlSplit.Length - 1];
                    switch (extenstion)
                    {
                        case "mp3":
                            EnclosureType = "audio/mpeg";
                            break;
                        case "m4a":
                            EnclosureType = "audio/x-m4a";
                            break;
                        case "mp4":
                            EnclosureType = "video/mp4";
                            break;
                        case "m4v":
                            EnclosureType = "video/x-m4v";
                            break;
                        case "mov":
                            EnclosureType = "video/quicktime";
                            break;
                        case "pdf":
                            EnclosureType = "application/pdf";
                            break;
                        case "epub":
                            EnclosureType = "document/x-epub";
                            break;
                    }
                }
                SetNodeAttribute(ENCLOSURE_XPATH, "url", value);
                mParent.Dirty = true;
            }
        }

        public long EnclosureLength
        {
            get
            {
                long length = -1;
                
                XmlNode node = mFeedItemNode.SelectSingleNode(ENCLOSURE_XPATH);
                string lengthString = null;
                if (node != null)
                {
                    XmlAttribute attr = node.Attributes["length"];
                    if (attr != null)
                    {
                        lengthString = attr.Value;
                        long.TryParse(lengthString, out length);
                    }
                }

                return length;
            }
            set 
            {
                SetNodeAttribute(ENCLOSURE_XPATH, "length", value.ToString());
                mParent.Dirty = true;
            }
        }

        /// <summary>
        /// Sets the InnerText value of the specified node.  If the node does not exist, one
        /// is automatically created.
        /// </summary>
        /// <param name="nodeName">The name of the node to set/create.</param>
        /// <param name="nsURI">Namespace URI</param>
        /// <param name="innerText">The new InnerText value.</param>
        private void SetNodeInnerText(string nodeName, string innerText)
        {
            XmlNode node = mFeedItemNode.SelectSingleNode(nodeName);
            if (node == null)
            {
                node = mFeedItemNode.OwnerDocument.CreateNode(XmlNodeType.Element, nodeName, null);
                mFeedItemNode.AppendChild(node);
            }

            node.InnerText = innerText;
        }

        /// <summary>
        /// Sets the InnerText value of the specified node.  If the node does not exist, one
        /// is automatically created.
        /// </summary>
        /// <param name="nodeName">The name of the node to set/create.</param>
        /// <param name="nsURI">Namespace URI</param>
        /// <param name="innerText">The new InnerText value.</param>
        private void SetNodeInnerText(string prefix, string nodeName, string innerText, 
            XmlNamespaceManager nsmgr)
        {
            XmlNode node = null;
            string nsURI = null;

            if (!nsmgr.HasNamespace(prefix))
                return;

            if (nsmgr == null)
                node = mFeedItemNode.SelectSingleNode(nodeName);
            else
            {
                node = mFeedItemNode.SelectSingleNode(nodeName, nsmgr);
                nsURI = nsmgr.LookupNamespace(prefix);
            }

            if (node == null)
            {
                node = mFeedItemNode.OwnerDocument.CreateNode(XmlNodeType.Element, prefix, nodeName, nsURI);
                mFeedItemNode.AppendChild(node);
            }

            node.InnerText = innerText;
        }

        /// <summary>
        /// Gets the element value from the given xpath.
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private string GetFeedElementValue(string xpath)
        {
            XmlNode node = null;
            try
            {
                node = mFeedItemNode.SelectSingleNode(xpath, mNamespaceMgr);
            }
            catch
            { }
            return (node != null) ? node.InnerText : null;
        }

        /// <summary>
        /// Sets the given attribute to the given attribute value on the specified node.  If a node
        /// does not exist, then one is created.
        /// </summary>
        /// <param name="nodeName">Name of the node to get/create.</param>
        /// <param name="attrName">Attribute Name to set/create.</param>
        /// <param name="attrValue">New Attribute Value</param>
        private void SetNodeAttribute(string nodeName, string attrName, string attrValue)
        {
            XmlNode node = mFeedItemNode.SelectSingleNode(nodeName);
            if (node == null)
            {
                node = mFeedItemNode.OwnerDocument.CreateNode(XmlNodeType.Element, nodeName, null);
                mFeedItemNode.AppendChild(node);
            }

            XmlAttribute xmlAttr = node.Attributes[attrName];
            if (xmlAttr == null)
            {
                xmlAttr = node.OwnerDocument.CreateAttribute(attrName);
                node.Attributes.Append(xmlAttr);
            }
            xmlAttr.Value = attrValue;
        }

        /// <summary>
        /// Sets the node attribute for a node with a namespace.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="nodeName"></param>
        /// <param name="nsmgr"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        private void SetNodeAttribute(string prefix, string nodeName, XmlNamespaceManager nsmgr,
            string attrName, string attrValue)
        {
            XmlNode node = null;
            string nsURI = null;

            if (nsmgr == null)
                node = mFeedItemNode.SelectSingleNode(nodeName);
            else
            {
                node = mFeedItemNode.SelectSingleNode(nodeName, nsmgr);
                nsURI = nsmgr.LookupNamespace(prefix);
            }


            if (node == null)
            {
                node = mFeedItemNode.OwnerDocument.CreateNode(XmlNodeType.Element, prefix, nodeName, nsURI);
                mFeedItemNode.AppendChild(node);
            }

            XmlAttribute xmlAttr = node.Attributes[attrName];
            if (xmlAttr == null)
            {
                xmlAttr = node.OwnerDocument.CreateAttribute(attrName);
                node.Attributes.Append(xmlAttr);
            }
            xmlAttr.Value = attrValue;
        }

        public string EnclosureType
        {
            get 
            {
                string enctype = null;
                XmlNode node = mFeedItemNode.SelectSingleNode(ENCLOSURE_XPATH);
                XmlAttribute typeAttribute = null;
                if (node != null)
                    typeAttribute = node.Attributes["type"];
                if (typeAttribute != null)
                    enctype = typeAttribute.Value;

                return enctype;
            }
            set
            {
                //mFeedItemNode.SelectSingleNode(ENCLOSURE_XPATH).Attributes["type"].Value = value;
                SetNodeAttribute(ENCLOSURE_XPATH, "type", value);
                mParent.Dirty = true;
            }
        }

        public bool GuidIsPermaLink
        {
            get
            {
                bool permalink = false;
                XmlNode node = mFeedItemNode.SelectSingleNode(GUID_XPATH);
                XmlAttribute permalinkAttribute = null;
                if (node != null)
                    permalinkAttribute = node.Attributes["isPermaLink"];
                if (permalinkAttribute != null)
                    bool.TryParse(permalinkAttribute.Value, out permalink);
                return permalink;
            }
            set
            {
                //mFeedItemNode.SelectSingleNode(GUID_XPATH).Attributes["isPermaLink"].Value = value.ToString();
                SetNodeAttribute(GUID_XPATH, "isPermaLink", value.ToString());
                mParent.Dirty = true;
            }
        }

        public string EnclosureName
        {
            get
            {
                string soundFileName = null;
                if (mSoundFilePath != null)
                {
                    string[] splits = mSoundFilePath.Split('/', '\\');
                    soundFileName = splits[splits.Length - 1];
                }
                return soundFileName;
            }
        }

        public string EnclosurePath
        {
            get
            {
                string soundFilePath = null;
                string guid = GUID;
                string xpath_guid = string.Format(XPATH_ITEM_SEARCH, guid);
                XmlNode feedItemNode = mAdditionalFeedDataXml.SelectSingleNode(xpath_guid);
                if (feedItemNode != null)
                {
                    XmlNode soundFileNode;
                    if ((soundFileNode = feedItemNode.SelectSingleNode(XPATH_SOUND_FILE)) != null)
                        soundFilePath = soundFileNode.InnerText;
                }

                if (soundFilePath == string.Empty) soundFilePath = null;

                return soundFilePath;
            }
            set
            {
                if (GUID == null)
                    throw new ArgumentException("GUID property must be set before setting SoundFilePath.");

                mSoundFilePath = value;
                string xpath_guid = string.Format(XPATH_ITEM_SEARCH, GUID);
                XmlNode localNode = mAdditionalFeedDataXml.SelectSingleNode(xpath_guid);
                XmlNode soundFileNode;

                if (localNode != null)
                {
                    if ((soundFileNode = localNode.SelectSingleNode(XPATH_SOUND_FILE)) != null)
                    {
                        soundFileNode.InnerText = mSoundFilePath;
                    }
                    else
                    {
                        soundFileNode = localNode.OwnerDocument.CreateNode(XmlNodeType.Element, 
                            XPATH_SOUND_FILE, null);
                        soundFileNode.InnerText = mSoundFilePath;
                        localNode.AppendChild(soundFileNode);
                    }
                    mParent.Dirty = true;
                }
            }
        }

        public FeedItem Clone()
        {
            //<item>
            //  <title>For the Faithful</title>
            //  <description>Christmas</description>
            //  <link>http://www.crossroads-ridgecrest.org/</link>
            //  <enclosure url="http://www.crossroads-ridgecrest.org/Websites/crossroadscommunity/images/Sermons/SermonAudio2013/12-08-2013.mp3" length="8550089" type="audio/mpeg" />
            //  <guid isPermaLink="false">482fa05b-34a9-44fc-8c2c-3dc4ee1a10fc</guid>
            //  <pubDate>Sun, 08 Dec 2013 21:17:34 -0800</pubDate>
            //  <itunes:subtitle>Christmas</itunes:subtitle>
            //  <itunes:summary>
            //  </itunes:summary>
            //  <itunes:duration>00:35:37</itunes:duration>
            //  <itunes:author>Bill Corley</itunes:author>
            //  <itunes:explicit>No</itunes:explicit>
            //</item>

            FeedItem newItem = mParent.CreateNewFeedItem();
            newItem.Title = this.Title;
            newItem.Description = this.Description;
            newItem.Link = this.Link;
            newItem.EnclosureURL = this.EnclosureURL;
            newItem.PubDate = this.PubDate;
            newItem.SubTitle = this.SubTitle;
            newItem.Summary = this.Summary;
            newItem.Author = this.Author;
  
            return newItem;
        }

        public override string ToString()
        {
            return mFeedItemNode.SelectSingleNode(TITLE_XPATH).InnerText;
        }

        public string GUID
        {
            get 
            {
                XmlNode node = mFeedItemNode.SelectSingleNode(GUID_XPATH, mNamespaceMgr);
                return (node != null) ? node.InnerText : null; 
            }
            set
            {
                string newGuid = value;
                string currentGuid = this.GUID;
                if (newGuid != currentGuid)
                {
                    if (currentGuid == null)
                    {
                        throw new Exception("Error, GUID not initialized in item.");
                    }
                    else
                    {
                        //Find the guid in the additional feed data document and change it.
                        string xpath_guid = string.Format(XPATH_ITEM_SEARCH, currentGuid);
                        XmlNode localNode = mAdditionalFeedDataXml.SelectSingleNode(xpath_guid);
                        if (localNode != null) //might not have a node if we haven't saved yet.
                            localNode.Attributes["guid"].Value = newGuid;

                        //Find the guid node in the xml feed and change it.
                        string feed_xpath_guid = string.Format(FeedData.FEED_ITEM_SEARCH_XPATH, currentGuid);
                        XmlNode feedGuidNode = mParent.Document.SelectSingleNode(feed_xpath_guid);
                        feedGuidNode.InnerText = newGuid;
                    }
                    mParent.Dirty = true;
                }
            }
        }

        #region IDisposable Interface

        public void Dispose()
        {
            this.mParent = null;
            mNamespaceMgr = null;
        }
        #endregion

        #region IComparable Interface

        public int CompareTo(object obj)
        {
            FeedItem itemIn = obj as FeedItem;
            int compareResult = 0;
            if (itemIn != null)
            {
                compareResult = GUID.CompareTo(itemIn.GUID);
            }
            else
                throw new ArgumentException("Compare object of incorrect type.");

            return compareResult;
        }

        #endregion

    }
}

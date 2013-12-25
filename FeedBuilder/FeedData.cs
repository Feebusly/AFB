using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Cryptography;
using FeedBuilder.Properties;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml.Xsl;
using Aga.Controls.Tree;

namespace FeedBuilder
{
    /// <summary>
    /// This class will use a local xml document to load and store information
    /// about your feed locally that does not belong in the feed itself.  For
    /// example, FTP info and the location of local mp3 files. 
    /// 
    /// </summary>
    /// 
    /// <remarks>
    /// The xml structurs are as follows.
    /// 
    /// Additional Feed Data XML File:
    /// <FeedInfo>
    ///     <LocalFeedPath>C:\users\me\myfeed.xml</LocalPath>
    ///     <FTPServerURL>ftp://feedserver.net</FTPServerURL>
    ///     <FTPServerPath>/podcasts</FTPServerPath> <!--optional information-->
    ///     <FTPUsername>myname</FTPUsername>
    ///     <FTPPassword>feqj890fghsivoe</FTPPassword> <!--encrypted password-->
    ///     <LocalFeedImagePath>C:\users\me\My Pictures\MyFeedImage.jpg</LocalFeedImagePath>
    ///     <FeedItemNode>
    ///         <GUID>hfrew-grwh-wgrjfsh-43hufr-432fjdkl</GUID>
    ///         <SoundFilePath>C:\users\me\MP3Dir\soundbytes1.mp3</SoundFilePath>
    ///     </FeedItemNode>
    ///     <FeedItemNode>
    ///         <guid>few89-fewh89-fewq789654-dsuyir37g3</guid>
    ///         <SoundFilePath>C:\users\me\MP3Dir\soundbytes2.mp3</SoundFilePath>
    ///     </FeedItemNode>
    /// </FeedInfo>
    /// 
    /// XML Feed Document:
    /// <?xml version="1.0" encoding="UTF-8"?>
    /// <rss xmlns:atom="http://www.w3.org/2005/Atom"
    ///     xmlns:itunes="http://www.itunes.com/dtds/podcast-1.0.dtd" version="2.0">
    /// <channel>
    ///     <title>Crossroads Sermon Archive</title>
    ///     <description>This is the Crossroads podcast archive.</description>
    ///     <link>http://www.crossroads-ridgecrest.org</link>
    ///     <language>en-us</language>
    ///     <copyright>Copyright 2009, 2010, 2011</copyright>
     ///    <lastBuildDate>Sun, 03 Jun 2012 08:00:00 PST</lastBuildDate>
     ///    <pubDate>Sun, 03 Jun 2012 08:00:00 PST</pubDate>
     ///    <webMaster>tim.veazey@gmail.com (Tim Veazey)</webMaster>
     ///    <itunes:author>Bill Corley</itunes:author>
     ///    <itunes:subtitle>Crossroads Community Church Sermons - Ridgecrest, California</itunes:subtitle>
     ///    <itunes:summary>Sermons for the Crossroads Community Church in Ridgecrest, CA</itunes:summary>
     ///    <itunes:owner>
    ///         <itunes:name>Bill Corley</itunes:name>
    ///         <itunes:email>bill@ccc-rc.org</itunes:email>
    ///     </itunes:owner>
    ///     <itunes:explicit>No</itunes:explicit>
    /// 	<itunes:category text="Religion &amp; Spirituality">
    /// 	<itunes:category text="Christianity"/>
    /// 	</itunes:category>
    /// 	<atom:link href="http://podcast.crossroads-ridgecrest.org/podcasts.xml" rel="self" type="application/rss+xml" />
    /// <item>
    ///     <title>Finding a Life Coach</title>
    ///     <description>Life Coach Series</description>
    ///     <link>http://www.crossroads-ridgecrest.org/Websites/crossroadscommunity/images/Sermons/SermonAudio2012/11-25-2012.mp3</link>
    ///     <enclosure url="http://www.crossroads-ridgecrest.org/Websites/crossroadscommunity/images/Sermons/SermonAudio2012/11-25-2012.mp3" length="10202700" type="audio/mpeg"></enclosure>
    ///     <guid isPermaLink="false">fe655426-f6df-4b9a-8080-8271385892e9</guid>
    ///     <pubDate>Sun, 25 Nov 2012 00:00:00 PST</pubDate>
    ///     <itunes:subtitle>Finding a Life Coach</itunes:subtitle>
    ///     <itunes:summary>Life Coach Series</itunes:summary>
    ///     <itunes:duration>00:42:30</itunes:duration>
    ///     <itunes:author>Bill Corley</itunes:author>
    ///     <itunes:explicit>no</itunes:explicit>
    /// </item>
    /// </channel>
    /// </rss>
    /// </remarks>
    public class FeedData
    {
        public const string LOCAL_FEED_PATH_XPATH = "//FeedInfo/LocalFeedPath";
        public const string LOCAL_TRANSFORM_XPATH = "//FeedInfo/Transform";
        public const string LOCAL_TIMEZONE_ACRONYM_XPATH = "//FeedInfo/TimeZoneAcronym";
        public const string LOCAL_TIMEZONE_OFFSET_XPATH = "//FeedInfo/TimeZoneOffset";
        public const string LOCAL_IMAGE_PATH_XPATH = "//FeedInfo/LocalFeedImagePath";
        public const string FTP_SERVER_URL_XPATH = "//FeedInfo/FTPServerURL";
        public static string FEED_ITEM_NODE_XPATH = "//FeedInfo/FeedItemNode";
        public const string FTP_PASSWORD_XPATH = "//FeedInfo/FTPPassword";
        public const string FTP_USERNAME_XPATH = "//FeedInfo/FTPUsername";
        public const string FTP_REMOTE_SERVER_PATH_XPATH = "//FeedInfo/FTPServerPath";
        public const string LOCAL_VALIDATION_URL_XPATH = "//FeedInfo/FeedURL";
        public const string NON_ITUNES_FILE_NAME_XPATH = "//FeedInfo/NonItunesName";
        public const string XPATH_EXPLORER_TREE_PATH = "//FeedInfo/SelectedExplorerPath";

        public const string FTP_SRVR_PATH_ITUNES_FILE = "//FeedInfo/FtpPaths/iTunesFile";
        public const string FTP_SRVR_PATH_NON_ITUNES_FILE = "//FeedInfo/FtpPaths/nonItunesFile";
        public const string FTP_SRVR_PATH_FEED_IMAGE = "//FeedInfo/FtpPaths/FeedImage";
        public const string FTP_SRVR_PATH_SOUND_FILES = "//FeedInfo/FtpPaths/SoundFiles";
        public const string FTP_SRVR_PATH_HTML_FILE = "FeedInfo/FtpPaths/HTMLFile";
        public const string FTP_SRVR_PREF_UPLOAD_NON_ITUNES = "FeedInfo/FtpPaths/UploadNonItunes";

        public const string ROOT = "//rss";
        public const string CHANNEL = "//rss/channel";
        public const string TITLE = "//rss/channel/title";
        public const string DESCRIPTION = "//rss/channel/description";
        public const string LINK = "//rss/channel/link";
        public const string COPYWRITE = "//rss/channel/copyright";
        public const string LAST_BUILD_DATE = "//rss/channel/lastBuildDate";
        public const string PUB_DATE = "//rss/channel/pubDate";
        public const string LANGUAGE = "//rss/channel/language";
        public const string WEBMASTER = "//rss/channel/webMaster";
        public const string AUTHOR = "//rss/channel/itunes:author";
        public const string SUBTITLE = "//rss/channel/itunes:subtitle";
        public const string SUMMARY = "//rss/channel/itunes:summary";
        public const string OWNER_NAME = "//rss/channel/itunes:owner/itunes:name";
        public const string OWNER_EMAIL = "//rss/channel/itunes:owner/itunes:email";
        public const string CATEGORY = "//rss/channel/itunes:category";
        public const string SUBCATEGORY = "//rss/channel/itunes:category/itunes:category";
        public const string ATOM_LINK = "//rss/channel/atom:link";
        public const string ITEMS = "//rss/channel/item";
        public const string FEED_IMAGE_TITLE = "//rss/channel/image/title";
        public const string FEED_IMAGE_DESC = "//rss/channel/image/description";
        public const string FEED_IMAGE_URL = "//rss/channel/image/url";
        public const string FEED_IMAGE_LINK = "//rss/channel/image/link";
        public const string FEED_ITEM_SEARCH_XPATH = "//rss/channel/item/guid[text() = '{0}']";
        public const string FEED_ITEM_POS_SEARCH_XPATH = "//rss/channel/item[position() = {0}]";

        private const string FEED_ITEMS = "//rss/channel/item";
        private const string FEED_ITEM_GUID = "guid";

        private List<FeedItem> mFeedItems;
        /// <summary>
        /// Contains the feed data required to support this feed on this system.
        /// </summary>
        private XmlDocument mAdditionalFeedDataXml;
        private string mAdditionalFeedDataPath;
        
        /// <summary>
        /// The actual feed document.
        /// </summary>
        private XmlDocument mFeedDocument;
        private string mFeedPath;
        private XmlNamespaceManager mNamespaceMgr;

        private bool mIsNew;
        private bool mIsDirty;
        private string mXsltOutput;

        /// <summary>
        /// Default constructor that initializes this FeedData instance with a "Path.xml*" filename
        /// that is not saved.
        /// </summary>
        public FeedData()
        {
            const string DefaultFeedName = "AnotherFeed";
            const string FeedExtension = ".xml";

            //TODO Open and New do not seem to be initializing the feed path correctly or
            //populating the XSLT transform text properly.

            string myDocsDir = GetLocalDataPath(null) ;

            int fileCount = 1;
            if (Directory.Exists(myDocsDir))
                fileCount = 1 + Directory.EnumerateFiles(myDocsDir, "*.xml").Count();

            string path = myDocsDir + "\\" + DefaultFeedName + fileCount.ToString() + FeedExtension;

            mFeedPath = path;
            mAdditionalFeedDataPath = GetLocalDataPath(path);
            mFeedItems = new List<FeedItem>();

            if (File.Exists(mAdditionalFeedDataPath))
            {
                mAdditionalFeedDataXml = new XmlDocument();
                mAdditionalFeedDataXml.Load(mAdditionalFeedDataPath);
            }
            else
            {
                mAdditionalFeedDataXml = new XmlDocument();
                mAdditionalFeedDataXml.LoadXml(Resources.AdditionalFeedData);
            }

            mFeedDocument = new XmlDocument();
            mFeedDocument.LoadXml(Resources.PodcastTemplate);
            mNamespaceMgr = new XmlNamespaceManager(mFeedDocument.NameTable);
            AddXmlNamespacesTo(mNamespaceMgr, Resources.PodcastTemplate);

            //Build the feed items.
            string thisGuid;
            foreach (XmlNode node in mFeedDocument.SelectNodes(FEED_ITEMS))
            {
                XmlNode guid = node.SelectSingleNode("guid");
                thisGuid = guid.InnerText;

                if (thisGuid == null || thisGuid == string.Empty)
                    thisGuid = Guid.NewGuid().ToString();

                FeedItem feedItemInfo = new FeedItem(this, mNamespaceMgr, node, thisGuid, null);
                this[thisGuid] = feedItemInfo;
            }
            mIsNew = true;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="feedPath"></param>
        public FeedData(string feedPath)
        {
            LoadFeed(feedPath);
        }

        private void AddXmlNamespacesTo(XmlNamespaceManager nsmgr, string xml)
        {
            IDictionary<string, string> nameURLPairs = GetNamespacesIn(xml);
            foreach (string name in nameURLPairs.Keys)
            {
                nsmgr.AddNamespace(name, nameURLPairs[name]);
            }
        }

        private IDictionary<string, string> GetNamespacesIn(string xml)
        {
            XDocument y = XDocument.Parse(xml);
            XPathNavigator nav = y.CreateNavigator();
            nav.MoveToFollowing(XPathNodeType.Element);
            return nav.GetNamespacesInScope(XmlNamespaceScope.All);
        }

        /// <summary>
        /// True until a save occurs.
        /// </summary>
        /// <returns></returns>
        public bool IsNew()
        {
            return mIsNew;
        }

        public XmlNamespaceManager NamespaceManager
        {
            get { return mNamespaceMgr; }
        }

        /// <summary>
        /// Get/set this feed as an itunes feed.  This will automatically strip or add the iTunes
        /// namespace and elements to the feed and the items.
        /// </summary>
        public bool ItunesFeed
        {
            get
            {
                bool itunes = false;
                if (mFeedDocument != null)
                {
                    string xml;
                    if ((xml = mFeedDocument.OuterXml) != null)
                    {
                        IDictionary<string, string> namespaces = GetNamespacesIn(xml);
                        if (namespaces != null)
                        {
                            itunes = namespaces.ContainsKey("itunes");
                        }
                    }
                }
                return itunes;
            }
            set
            {
                if (value == ItunesFeed)
                    return;

                StringBuilder results = new StringBuilder();
                results.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
                string xml = mFeedDocument.OuterXml;

                string stylesheet = (value) ? Resources.XSLTAddItunes : Resources.XSLTRemoveItunes;
                  
                using (StringReader stream = new StringReader(xml))
                {
                    XPathDocument xpathDoc = new XPathDocument(stream);
                    XslCompiledTransform transform = new XslCompiledTransform();
                    using (XmlReader xsltReader = XmlReader.Create(new StringReader(stylesheet)))
                    {
                        transform.Load(xsltReader);
                    }

                    using (StringWriter writer = new StringWriter(results))
                    {
                        XmlTextWriter xmlWriter = new XmlTextWriter(writer);
                        transform.Transform(xpathDoc, null, xmlWriter);
                    }
                }
                LoadXmlText(results.ToString());
            }
        }

        private void SetFeedPaths(string xmlFilePath)
        {
            FeedPath = xmlFilePath;
            mAdditionalFeedDataPath = GetLocalDataPath(xmlFilePath);
        }

        /// <summary>
        /// Loads the given XML text into this FeedData object, clearing out old text if needed.
        /// </summary>
        /// <param name="xmlText"></param>
        public void LoadXmlText(string xmlText)
        {
            if (File.Exists(mAdditionalFeedDataPath))
            {
                mAdditionalFeedDataXml = new XmlDocument();
                mAdditionalFeedDataXml.Load(mAdditionalFeedDataPath);
            }
            else
            {
                mAdditionalFeedDataPath = GetLocalDataPath(mFeedPath);
                mAdditionalFeedDataXml = new XmlDocument();
                mAdditionalFeedDataXml.LoadXml(Resources.AdditionalFeedData);
                StartNewLocalDataXml(mFeedPath);
            }

            //Attempt to load new text before assigning to member variable
            XmlDocument testDoc = new XmlDocument();
            testDoc.LoadXml(xmlText); //Exception will be thrown here, preventing assignment to mFeedDocument.
            
            mFeedDocument = testDoc;
            mNamespaceMgr = new XmlNamespaceManager(mFeedDocument.NameTable);
            AddXmlNamespacesTo(mNamespaceMgr, mFeedDocument.OuterXml);

            //Build the feed items.
            string thisGuid;
            mFeedItems.Clear();
            foreach (XmlNode node in mFeedDocument.SelectNodes(FEED_ITEMS))
            {
                XmlNode guid = node.SelectSingleNode("guid");
                thisGuid = guid.InnerText;

                if (thisGuid == null || thisGuid == string.Empty)
                    thisGuid = Guid.NewGuid().ToString();

                FeedItem feedItemInfo = new FeedItem(this, mNamespaceMgr, node, thisGuid, null);
                this[thisGuid] = feedItemInfo;
            }
        }

        /// <summary>
        /// Loads the feed information from the given xml file into this FeedData object.
        /// </summary>
        /// <param name="xmlFilePath">The path to the xml feed file.</param>
        public void LoadFeed(string xmlFilePath)
        {
            this.InhibitDirty(true);
            SetFeedPaths(xmlFilePath);
            mFeedItems = new List<FeedItem>();

            using (StreamReader sr = new StreamReader(mFeedPath))
            {
                string xmlText = sr.ReadToEnd();
                LoadXmlText(xmlText);
            }
            mIsNew = false;
            this.InhibitDirty(false);
        }

        public IDictionary<string, ValidationError> Validate()
        {
            return FeedValidator.ValidateFeedData(this);
        }

        /// <summary>
        /// Here we are adding content to this file for a feed that has never been loaded into the tool before.
        /// A feed path will exist, items may exist with GUIDs, but there will not be much else info for local 
        /// feed data.  If items are found with no GUIDs, then GUIDs will be attached.
        /// </summary>
        private void StartNewLocalDataXml(string feedPath)
        {
            LocalFeedPath = feedPath;

            XmlDocument tempDoc = new XmlDocument();
            tempDoc.LoadXml(Resources.PodcastTemplate);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(tempDoc.NameTable);
            AddXmlNamespacesTo(nsmgr, Resources.PodcastTemplate);

            //Only initialize items if we actually have a feed file.
            if (File.Exists(feedPath))
            {
                XmlDocument feed = new XmlDocument();
                feed.Load(feedPath);
                string thisGuid = null;

                foreach (XmlNode node in feed.SelectNodes(FEED_ITEMS))
                {
                    XmlNode guid = node.SelectSingleNode("guid");
                    thisGuid = guid.InnerText;

                    if (thisGuid == null || thisGuid == string.Empty)
                        thisGuid = Guid.NewGuid().ToString();

                    FeedItem feedItemInfo = new FeedItem(this, nsmgr, node, thisGuid, null);
                    this[thisGuid] = feedItemInfo;
                }
            }
        }

        public static string GetLocalDataPath(string feedPath)
        {
            string userDataDir = GetUserDataDirectory();
            string fileName = null;
            if (feedPath != null)
            {
                string[] paths = feedPath.Split('/', '\\');
                fileName = paths[paths.Length - 1];
            }
            if (!Directory.Exists(userDataDir))
                Directory.CreateDirectory(userDataDir);
            return (fileName != null) ? userDataDir + "\\" + fileName : userDataDir;
        }

        public static string GetUserDataDirectory()
        {
            string appDataFolder = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            return appDataFolder + "\\" + "AnotherFeedBuilder";
        }

        private bool mDirtySet;
        private bool mInhibitDirty;
        public bool Dirty
        {
            get { return mIsDirty && mDirtySet; }
            set 
            {
                if (!mInhibitDirty)
                {
                    mDirtySet = true;
                    mIsDirty = value;
                }
            }
        }

        public void InhibitDirty(bool inhibit)
        {
            mInhibitDirty = inhibit;
        }

        public XmlDocument Document
        {
            get { return mFeedDocument; }
        }

        /// <summary>
        /// Returns the xml document this FeedData object is built arround.
        /// </summary>
        public XmlDocument AdditionalFeedDataXmlDoc
        {
            get { return mAdditionalFeedDataXml; }
        }

        /// <summary>
        /// Path to the XML file for this feed.
        /// </summary>
        public string FeedPath
        {
            get { return mFeedPath; }
            set
            {
                if (value != mFeedPath)
                {
                    //There might be an xslt transform we need to move.
                    if (XsltOutput != null)
                    {
                        string outputPath = XsltOutputPath;
                        mFeedPath = value;
                        string newOutputPath = XsltOutputPath;

                        if (outputPath != null)
                        {
                            if (File.Exists(outputPath))
                            {
                                if (File.Exists(newOutputPath))
                                    File.Delete(newOutputPath);

                                File.Move(outputPath, newOutputPath);
                            }
                        }
                    }
                }
                mFeedPath = value;

                this.Dirty = true; 
            }
        }

        public string FeedFileName
        {
            get
            {
                string fileName = null;
                if (mFeedPath != null)
                {
                    string[] splits = mFeedPath.Split('/', '\\');
                    fileName = splits[splits.Length - 1];
                }
                return fileName;
            }
        }

        public string XsltOutputPath
        {
            get
            {
                string fileName = FeedFileName;
                string rootName = fileName.Split('.')[0];

                return GetLocalDataPath(null) + "\\" + rootName + ".html";
            }
        }

        public string XsltOutput
        {
            get { return mXsltOutput; }
            set 
            { 
                mXsltOutput = value;
                string outputPath = XsltOutputPath;
                if (outputPath != null)
                {
                    if (File.Exists(outputPath))
                    {
                        File.Delete(outputPath);
                    }
                    File.WriteAllText(outputPath, mXsltOutput);
                }
            }
        }

        /// <summary>
        /// Stores the XSLT data as a CDATA section in the additional feed data file.
        /// </summary>
        public string XSLT
        {
            get
            {
                XmlNode transform = mAdditionalFeedDataXml.SelectSingleNode(LOCAL_TRANSFORM_XPATH);
                if (transform != null)
                {
                    XmlCDataSection cdata = transform.FirstChild as XmlCDataSection;
                    return (cdata != null) ? cdata.Value : null;
                }
                else
                    return null;
            }
            set
            {
                XmlCDataSection cdata = mAdditionalFeedDataXml.CreateCDataSection(value);
                XmlNode transform = mAdditionalFeedDataXml.SelectSingleNode(LOCAL_TRANSFORM_XPATH);
                if (transform != null)
                {
                    transform.RemoveAll();
                    transform.AppendChild(cdata);
                }
                else
                {
                    XmlNode root = mAdditionalFeedDataXml.SelectSingleNode("//FeedInfo");
                    XmlNode newTransform = root.AppendChild(mAdditionalFeedDataXml.CreateNode(
                        XmlNodeType.Element, "Transform", null));
                    newTransform.AppendChild(cdata);
                }
            }
        }

        /// <summary>
        /// Path to the xml file that contains additinoal information about this feed.
        /// </summary>
        public string LocalFeedInfoPath
        {
            get { return mAdditionalFeedDataPath; }
            set { mAdditionalFeedDataPath = value; this.Dirty = true; }
        }

        public string Title
        {
            get { return GetFeedElementValue(mFeedDocument, TITLE, null, null); }
            set { SetFeedElement(mFeedDocument, TITLE, value, null, null); }
        }

        public string Description
        {
            get { return GetFeedElementValue(mFeedDocument, DESCRIPTION, null, null); }
            set { SetFeedElement(mFeedDocument, DESCRIPTION, value, null, null); }
        }

        public string Link
        {
            get { return GetFeedElementValue(mFeedDocument, LINK, null, null); }
            set { SetFeedElement(mFeedDocument, LINK, value, null, null); }
        }

        public string Copywrite
        {
            get { return GetFeedElementValue(mFeedDocument, COPYWRITE, null, null); }
            set { SetFeedElement(mFeedDocument, COPYWRITE, value, null, null); }
        }

        public string LastBuildDateString
        {
            get
            {
                return GetFeedElementValue(mFeedDocument, LAST_BUILD_DATE, null, null);
            }
            set
            {
                // Will throw an exception if formatting is incorrect.
                //DateTime.ParseExact(value.Replace("PST", "-7"),
                //        "ddd, dd MMM yyyy HH:mm:ss z", CultureInfo.InvariantCulture);
                DateTime dt;
                if (value != null && value != string.Empty)
                {
                    if (FeedValidator.TryParseDateTime(value, out dt))
                        SetFeedElement(mFeedDocument, LAST_BUILD_DATE, value, null, null);
                    else
                        throw new FormatException(string.Format("Date {0} is not of the correct format.", value));
                }
            }
        }

        public DateTime LastBuildDate
        {
            get 
            {
                string buildDateString = GetFeedElementValue(mFeedDocument, LAST_BUILD_DATE, null, null);
                DateTime dt = DateTime.MinValue;
                FeedValidator.TryParseDateTime(buildDateString, out dt);
                //    dt = DateTime.ParseExact(buildDateString.Replace("PST", "-7"),
                //        "ddd, dd MMM yyyy HH:mm:ss z", CultureInfo.InvariantCulture);
                //}
                //catch
                //{
                //    DateTime.TryParse(buildDateString, out dt);
                //}
                return dt;
            }
            set 
            {
                string dateString = value.ToString("ddd, dd MMM yyyy HH:mm:ss zzzz");
                int lastColonIndex = dateString.LastIndexOf(':');
                dateString = dateString.Remove(lastColonIndex, 1);
                SetFeedElement(mFeedDocument, LAST_BUILD_DATE, dateString, null, null);
            }
        }

        public string PublicationDateString
        {
            get
            {
                return GetFeedElementValue(mFeedDocument, PUB_DATE, null, null);
            }
            set
            {
                // Will throw an exception if formatting is incorrect.
                //DateTime.ParseExact(value.Replace("PST", "-7"),
                //        "ddd, dd MMM yyyy HH:mm:ss z", CultureInfo.InvariantCulture);

                //SetFeedElement(mFeedDocument, PUB_DATE, value, null, null);

                DateTime dt = DateTime.MinValue;
                if (value != null && value != string.Empty)
                {
                    if (FeedValidator.TryParseDateTime(value, out dt))
                        SetFeedElement(mFeedDocument, PUB_DATE, value, null, null);
                    else
                        throw new FormatException(string.Format("Date {0} is not of the correct format.", value));
                }
            }
        }

        public DateTime PublicationDate
        {
            get 
            {
                string pubDateString = GetFeedElementValue(mFeedDocument, PUB_DATE, null, null);
                DateTime dt = DateTime.Now;
                FeedValidator.TryParseDateTime(pubDateString, out dt);
                //try
                //{
                //    dt = DateTime.ParseExact(pubDateString.Replace("PST", "-7"),
                //        "ddd, dd MMM yyyy HH:mm:ss z", CultureInfo.InvariantCulture);
                //}
                //catch 
                //{
                //    DateTime.TryParse(pubDateString, out dt);
                //}
                return dt;
            }
            set 
            {
                string dateString = value.ToString("ddd, dd MMM yyyy HH:mm:ss zzzz");
                int lastColonIndex = dateString.LastIndexOf(':');
                dateString = dateString.Remove(lastColonIndex, 1);
                SetFeedElement(mFeedDocument, PUB_DATE, dateString, null, null);
            }
        }

        public string Language
        {
            get { return GetFeedElementValue(mFeedDocument, LANGUAGE, null, null); }
            set { SetFeedElement(mFeedDocument, LANGUAGE, value, null, null); }
        }

        public string Webmaster
        {
            get { return GetFeedElementValue(mFeedDocument, WEBMASTER, null, null); }
            set { SetFeedElement(mFeedDocument, WEBMASTER, value, null, null); }
        }

        public string Author
        {
            get { return GetFeedElementValue(mFeedDocument, AUTHOR, null, mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, AUTHOR, value, null, mNamespaceMgr); }
        }

        public string Subtitle
        {
            get { return GetFeedElementValue(mFeedDocument, SUBTITLE, null, mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, SUBTITLE, value, null, mNamespaceMgr); }
        }

        public string Summary
        {
            get { return GetFeedElementValue(mFeedDocument, SUMMARY, null, mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, SUMMARY, value, null, mNamespaceMgr); }
        }

        public string OwnerName
        {
            get { return GetFeedElementValue(mFeedDocument, OWNER_NAME, null, mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, OWNER_NAME, value, null, mNamespaceMgr); }
        }

        public string OwnerEmail
        {
            get { return GetFeedElementValue(mFeedDocument, OWNER_EMAIL, null, mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, OWNER_EMAIL, value, null, mNamespaceMgr); }
        }

        public string Category
        {
            get { return GetFeedElementValue(mFeedDocument, CATEGORY, "text", mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, CATEGORY, value, "text", mNamespaceMgr); }
        }

        public string SubCategory
        {
            get { return GetFeedElementValue(mFeedDocument, SUBCATEGORY, "text", mNamespaceMgr); }
            set { SetFeedElement(mFeedDocument, SUBCATEGORY, value, "text", mNamespaceMgr); }
        }

        public string ImageTitle
        {
            get { return GetFeedElementValue(mFeedDocument, FEED_IMAGE_TITLE, null, null); }
            set { SetFeedElement(mFeedDocument, FEED_IMAGE_TITLE, value, null, null); }
        }

        public string ImageDescription
        {
            get { return GetFeedElementValue(mFeedDocument, FEED_IMAGE_DESC, null, null); }
            set { SetFeedElement(mFeedDocument, FEED_IMAGE_DESC, value, null, null); }
        }

        public string ImageURL
        {
            get { return GetFeedElementValue(mFeedDocument, FEED_IMAGE_URL, null, null); }
            set { SetFeedElement(mFeedDocument, FEED_IMAGE_URL, value, null, null); }
        }

        public string ImageLink
        {
            get { return GetFeedElementValue(mFeedDocument, FEED_IMAGE_LINK, null, null); }
            set { SetFeedElement(mFeedDocument, FEED_IMAGE_LINK, value, null, null); }
        }

        private string GetFeedElementValue(XmlDocument xmlDoc, string nodePath, string attributeName,
            XmlNamespaceManager nsmgr)
        {
            XmlNode testNode;
            string value = null;
            try
            {
                if ((testNode = xmlDoc.SelectSingleNode(nodePath, nsmgr)) != null)
                {
                    if (attributeName != null)
                    {
                        value = testNode.Attributes[attributeName].Value;
                    }
                    else
                    {
                        value = testNode.InnerText;
                    }
                }
            }
            catch
            {
                //No path.  Probably an itunes namespace missing.  Just ignore.
                return null;
            }

            return (value != null && value != string.Empty) ? FeedValidator.DecodeXmlText(value) : null;
        }

        /// <summary>
        /// Sets the value of the xmlnode in the given path.  If an xmlNode is not found for the
        /// given path, then one is created.
        /// </summary>
        /// <param name="nodePath">Path of the node to change.</param>
        /// <param name="value">New value to give to the node.</param>
        /// <param name="attrName">Optional attribute to apply the value to.  If null, then
        /// value will be applied to the node's InnerText property.</param>
        /// <param name="nsmgr">Optional namespace manager to use for nodes that belong to a
        /// particular namespace.</param>
        private void SetFeedElement(XmlDocument xmlDoc, string nodePath, 
            string value, string attrName, XmlNamespaceManager nsmgr)
        {
            if (xmlDoc == null)
                return;

            XmlNode node = null;
            XmlNode newNode = null;
            XmlNode lastNode = null;
            XmlNode firstItem = null;

            value = FeedValidator.EncodeXmlText(value);

            string ns = "";
            if (nsmgr != null)
                ns = nsmgr.LookupNamespace("itunes");

            string[] paths = nodePath.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            node = xmlDoc as XmlNode;
            lastNode = node;
            while (i < paths.Length)
            {
                if ((node = node.SelectSingleNode(paths[i], nsmgr)) == null)
                {
                    string prefix = "";
                    string name = paths[i];
                    if (name.Contains(':'))
                    {
                        string[] names = name.Split(':');
                        prefix = names[0];
                        name = names[1];
                    }
                    newNode = xmlDoc.CreateNode(XmlNodeType.Element, prefix, name, ns);

                    if ((firstItem = lastNode.SelectSingleNode("item")) != null)
                    {
                        lastNode.InsertBefore(newNode, firstItem);
                    }
                    else
                    {
                        lastNode.AppendChild(newNode);
                    }
                    node = newNode;
                }
                if (i == paths.Length - 1)
                {
                    if (attrName != null)
                    {
                        XmlAttribute attr = null;
                        if ((attr = node.Attributes[attrName]) != null)
                        {
                            attr.Value = value;
                        }
                        else
                        {
                            attr = xmlDoc.CreateAttribute(attrName, ns);
                            attr.Value = value;
                            node.Attributes.Append(attr);
                        }
                    }
                    else
                        node.InnerText = value;
                }
                lastNode = node;
                ++i;
            }
            this.Dirty = true;
        }

        /// <summary>
        /// Moves the given FeedItem up one position (unless current position is zero) 
        /// and returns the new position value.
        /// </summary>
        /// <param name="item">The FeedItem to move up.</param>
        /// <returns>New position of item.</returns>
        public int MoveUp(FeedItem item)
        {
            int position = PositionOf(item);
            if (position > 0)
            {
                --position;
                mFeedItems.Remove(item);
                mFeedItems.Insert(position, item);

                XmlNode itemNode = item.Node;
                XmlNode previousSibling = item.Node.PreviousSibling;

                mFeedDocument.InsertBefore(itemNode, previousSibling);
            }

            return position;
        }

        /// <summary>
        /// Moves the given FeedItem up one position (unless current position is zero) 
        /// and returns the new position value.
        /// </summary>
        /// <param name="item">The FeedItem to move up.</param>
        /// <returns>New position of item.</returns>
        public int MoveDown(FeedItem item)
        {
            int count = mFeedItems.Count;
            int position = PositionOf(item);
            if (position < count - 1)
            {
                ++position;
                mFeedItems.Remove(item);
                mFeedItems.Insert(position, item);

                XmlNode itemNode = item.Node;
                XmlNode nextSibling = item.Node.NextSibling;

                mFeedDocument.InsertAfter(itemNode, nextSibling);
            }

            return position;
        }

        /// <summary>
        /// Returns the position of this item in the item list.
        /// </summary>
        /// <param name="item">FeedItem to look for.</param>
        /// <returns>Integer value revealing the position of the given FeedItem.</returns>
        public int PositionOf(FeedItem item)
        {
            int position = 0;
            foreach (FeedItem thisItem in mFeedItems)
            {
                if (thisItem == item)
                    break;

                ++position;
            }
            return position;
        }

        /// <summary>
        /// Extract guid, and file information from node and create a new LocalFeedItemInfo.
        /// </summary>
        /// <param name="itemNode"></param>
        public void AddNewFeedItemInfo(XmlNode itemNode)
        {
            XmlNode guidNode = itemNode.SelectSingleNode("guid");
            
            if (guidNode != null)
            {
                string guid = guidNode.InnerText;
                XmlNode linkNode = itemNode.SelectSingleNode("link");
                if (linkNode != null)
                {
                    string link = linkNode.InnerText;
                    XmlNode localItemNode = mAdditionalFeedDataXml.CreateNode(
                        XmlNodeType.Element, "FeedItemNode", null);
                    XmlNode localGuidNode = guidNode.Clone();
                    XmlNode soundFilePlaceholder = mAdditionalFeedDataXml.CreateNode(
                        XmlNodeType.Element, "SoundFilePath", null);
                    localItemNode.AppendChild(localGuidNode);
                    localGuidNode.AppendChild(soundFilePlaceholder);
                    mAdditionalFeedDataXml.SelectSingleNode("//FeedInfo").AppendChild(localItemNode);
                }
            }
        }

        public FeedItem CreateNewFeedItem()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.NewItemTemplate);
            XmlNode itemNode = doc.SelectSingleNode("//rss/channel/item", mNamespaceMgr);
            string guid = Guid.NewGuid().ToString();
            itemNode.SelectSingleNode("guid").InnerText = guid;

            itemNode = mFeedDocument.ImportNode(itemNode, true);

            FeedItem item = new FeedItem(this, mNamespaceMgr, itemNode, guid, null);
            //mFeedItems.Insert(0, item);
            return item;
        }

        /// <summary>
        /// Inserts a new feed item at the given location.  Also ensures that the associated item in
        /// the xml document is positioned correctly.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="item"></param>
        public void InsertFeedItem(int position, FeedItem item)
        {
            if (mFeedItems.Contains(item))
            {
                mFeedItems.Remove(item);
            }
            mFeedItems.Insert(position, item);
            XmlNode itemNode = null;
            string findNodeXpath = string.Format(FEED_ITEM_SEARCH_XPATH, item.GUID) + "/..";
            int existingItemCount = mFeedDocument.SelectNodes(FEED_ITEMS).Count;
            XmlNode channel = mFeedDocument.SelectSingleNode(CHANNEL);

            //If we have no items, then simply add this one at the top and then we're done.
            if (existingItemCount == 0)
            {
                channel.AppendChild(item.Node);
            }
            else
            {
                string refNodeSearch = string.Format(FEED_ITEM_POS_SEARCH_XPATH, position + 1);
                XmlNode refNode = mFeedDocument.SelectSingleNode(refNodeSearch);
                if ((itemNode = mFeedDocument.SelectSingleNode(findNodeXpath)) != null)
                {
                    channel.RemoveChild(itemNode);
                }
                XmlNode insertNode = channel.OwnerDocument.ImportNode(item.Node, true);
                item.Node = insertNode;
                channel.InsertBefore(insertNode, refNode);
            }
        }

        /// <summary>
        /// Removes the given FeedItem from the FeedData collection and also removes the 
        /// assocated xml item nodes from the feed document and from the external data document.
        /// </summary>
        /// <param name="item">The FeedItem to remove.</param>
        public void RemoveFeedItem(FeedItem item)
        {
            string guid = item.GUID;
            string guidFinderXpath = string.Format(FEED_ITEM_SEARCH_XPATH, guid);
            XmlNode node = item.Node;
            mFeedItems.Remove(item);
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
            }
            item.CleanFromLocalData();
            item.Dispose();
        }

        public void MoveFeedItemUp(FeedItem item)
        {
            string guid = item.GUID;
            string guidFinderXpath = string.Format(FEED_ITEM_SEARCH_XPATH, guid);
            XmlNode node = mFeedDocument.SelectSingleNode(guidFinderXpath);
            
        }

        /// <summary>
        /// Get/set the path to the local feed file.
        /// </summary>
        public string LocalFeedPath
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, LOCAL_FEED_PATH_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, LOCAL_FEED_PATH_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set the validation URL.
        /// </summary>
        public string ValidationURL
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, LOCAL_VALIDATION_URL_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, LOCAL_VALIDATION_URL_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set the timezone offset value.
        /// </summary>
        public string TimeZoneOffset
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, LOCAL_TIMEZONE_OFFSET_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, LOCAL_TIMEZONE_OFFSET_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set the timezone acronym.
        /// </summary>
        public string TimeZoneAcronym
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, LOCAL_TIMEZONE_ACRONYM_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, LOCAL_TIMEZONE_ACRONYM_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set the path of the local image file.
        /// </summary>
        public string LocalImagePath
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, LOCAL_IMAGE_PATH_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, LOCAL_IMAGE_PATH_XPATH, value, null, null); }
        }

        public string LocalImageFileName
        {
            get
            {
                string imagePath = LocalImagePath;
                string imageFileName = null;
                if (imagePath != null)
                {
                    string[] splits = imagePath.Split('/', '\\');
                    imageFileName = splits[splits.Length - 1];
                }
                return imageFileName;
            }
        }

        /// <summary>
        /// Saved treepath to the selected Explorer node
        /// </summary>
        public TreePath ExplorerTreePath
        {
            get
            {
                string treePath = GetFeedElementValue(mAdditionalFeedDataXml, XPATH_EXPLORER_TREE_PATH, null, null);
                if (treePath == null)
                    return null;
                else
                    return TreePath.Deserialize(treePath);
            }
            set 
            {
                string path = value.SerializePath();
                SetFeedElement(mAdditionalFeedDataXml, XPATH_EXPLORER_TREE_PATH, path, null, null); 
            }
        }

        /// <summary>
        /// Get/set the ftp server URL.
        /// </summary>
        public string FTPServerURL
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_SERVER_URL_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SERVER_URL_XPATH, value, null, null); }
        }

        /// <summary>
        /// Server path to the Itunes File.
        /// </summary>
        public string FtpServerPathItunesFile
        {
            get 
            {
                string serverpath = GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PATH_ITUNES_FILE, null, null);
                if (serverpath == null)
                    serverpath = this.FeedFileName;
                return serverpath;
            }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PATH_ITUNES_FILE, value, null, null); }
        }

        /// <summary>
        /// Extracted from the FtpServerPathItunesFile property.
        /// </summary>
        public string FtpItunesFileName
        {
            get
            {
                string fileName = FtpServerPathItunesFile;
                if (fileName == null)
                {
                    fileName = FeedFileName;
                    string[] splits = fileName.Split('/', '\\');
                    fileName = splits[splits.Length - 1];
                }

                return fileName;
            }
        }

        /// <summary>
        /// Server path to the non itunes file.
        /// </summary>
        public string FtpServerPathNonItunesFile
        {
            get 
            {
                return GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PATH_NON_ITUNES_FILE, null, null);
                //if (serverpath == null)
                //    serverpath = this.FeedFileName;
                //return serverpath;
            }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PATH_NON_ITUNES_FILE, value, null, null); }
        }

        /// <summary>
        /// Get or set the preference to upload nonItunes files to the server.  Your feed needs
        /// to already be an iTunes feed and you need to have FtpServerPathNonItunesFile defined.
        /// </summary>
        public bool FtpServerPrefUploadNonItunes
        {
            get
            {
                return GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PREF_UPLOAD_NON_ITUNES, null, null) == "true" && ItunesFeed                                            ;
            }
            set
            {
                string truefalse = (value) ? "true" : "false";
                SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PREF_UPLOAD_NON_ITUNES, truefalse, null, null);
            }
        }

        /// <summary>
        /// Extracted from the FtsServerPathNonItunesFile property.
        /// </summary>
        public string FtpNonItunesFileName
        {
            get
            {
                string fileName = FtpServerPathNonItunesFile;
                if (fileName == null)
                {
                    fileName = FeedFileName;
                    string[] splits = fileName.Split('/', '\\');
                    fileName = splits[splits.Length - 1];
                }

                return fileName;
            }
        }

        /// <summary>
        /// Server path to the feed image.
        /// </summary>
        public string FtpServerPathFeedImage
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PATH_FEED_IMAGE, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PATH_FEED_IMAGE, value, null, null); }
        }

        /// <summary>
        /// Server path to the sound files.  Assumes that all sound files go in the same location.
        /// </summary>
        public string FtpServerPathSoundFiles
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PATH_SOUND_FILES, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PATH_SOUND_FILES, value, null, null); }
        }

        /// <summary>
        /// Server path to the html file.
        /// </summary>
        public string FtpServerPathHtmlFile
        {
            get 
            {
                string serverpath = GetFeedElementValue(mAdditionalFeedDataXml, FTP_SRVR_PATH_HTML_FILE, null, null);
                if (serverpath == null)
                    serverpath = this.FeedFileName;
                return serverpath;
            }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_SRVR_PATH_HTML_FILE, value, null, null); }
        }

        /// <summary>
        /// Extracted from the FtpServerPathHtmlFile property.
        /// </summary>
        public string FtpHtmlFileName
        {
            get
            {
                string fileName = FtpServerPathHtmlFile;
                if (fileName == null)
                {
                    fileName = FeedFileName;
                    string[] splits = fileName.Split('/', '\\');
                    fileName = splits[splits.Length - 1];
                }

                return fileName;
            }
        }

        /// <summary>
        /// Get/set the FTP remote server path.
        /// </summary>
        public string FTPRemoteServerPath
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_REMOTE_SERVER_PATH_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_REMOTE_SERVER_PATH_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set the FTP user name.
        /// </summary>
        public string FTPUsername
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_USERNAME_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_USERNAME_XPATH, value, null, null); }
        }

        /// <summary>
        /// Get/set an encrypted, private ftp password.
        /// </summary>
        public string FTPPassword
        {
            get { return GetFeedElementValue(mAdditionalFeedDataXml, FTP_PASSWORD_XPATH, null, null); }
            set { SetFeedElement(mAdditionalFeedDataXml, FTP_PASSWORD_XPATH, value, null, null); }
        }

        /// <summary>
        /// Indexer for the LocalFeedItemInfo objects assocated with this FeedInfo.  In setting this property,
        /// if the guid passed into the indexer does not match the guid in the LocalFeedItemInfo object, an
        /// exception will be thrown.  The caller should have code to handle this situation, if it is expected.
        /// That is, perhaps the user is wanting to change guids for some reason.  This should be allowed.
        /// </summary>
        /// <param name="guid">A unique ID for the FeedItem.</param>
        /// <returns>A LocalFeedItemInfo object matching the guid, or null if none exist.</returns>
        public FeedItem this[string guid]
        {
            get 
            { 
                FeedItem feedItem = mFeedItems.First(item => item.GUID == guid);
                return feedItem;
            }
            set
            {
                FeedItem feedItem;
                string additionalDataGuidXpath = string.Format("{0}/guid[text() = '{1}']", FEED_ITEM_NODE_XPATH, guid);
                string xmlDocItemXpath = string.Format(FEED_ITEM_SEARCH_XPATH, guid);

                //First case, we're clearing a value.
                if (value == null)
                {
                    //Remove feed item.
                    feedItem = mFeedItems.First(item => item.GUID == guid);
                    if (feedItem != null)
                        mFeedItems.Remove(feedItem);

                    //Remove node from additional data document.
                    XmlNode additionalDataGuidNode = mAdditionalFeedDataXml.SelectSingleNode(additionalDataGuidXpath);
                    if (additionalDataGuidNode != null)
                    {
                        XmlNode additionalDataFeedItem = additionalDataGuidNode.ParentNode;
                        mAdditionalFeedDataXml.RemoveChild(additionalDataFeedItem);
                    }

                    //Remove node from main feed document
                    XmlNode mainDocGuidNode = mFeedDocument.SelectSingleNode(xmlDocItemXpath);
                    if (mainDocGuidNode != null)
                    {
                        mFeedDocument.RemoveChild(mainDocGuidNode.ParentNode);
                    }

                    return;
                }

                feedItem = value;
                if (guid != feedItem.GUID)
                    throw new Exception("GUID indexer does not match FeedItem.GUID value.");

                mFeedItems.Add(feedItem);
                
                XmlNode guidNode = null;
                if ((guidNode = mAdditionalFeedDataXml.SelectSingleNode(additionalDataGuidXpath)) != null)
                {
                    XmlNode soundFilePath = guidNode.ParentNode.SelectSingleNode("SoundFilePath");
                    soundFilePath.InnerText = feedItem.EnclosurePath;
                }
            }
        }

        /// <summary>
        /// Saves this xml document and the additional feed data to the local file system.  
        /// This is the permanent path used for your full feed library.
        /// </summary>
        public void Save()
        {
            mFeedDocument.Save(mFeedPath);
            mAdditionalFeedDataXml.Save(mAdditionalFeedDataPath);
            this.Dirty = false;
            mIsNew = false;
        }

        /// <summary>
        /// Saves this document to a temporary location, to be used for FTP upload.  
        /// </summary>
        /// <param name="onlyChecked">Set to true if you want this save to only include the
        /// checked items.  Otherwise, set to false and all items will be saved for upload.</param>
        /// <returns>Path to the temporary file.</returns>
        public string SaveNonItunesForUpload(bool onlyChecked)
        {
            StringBuilder results = new StringBuilder();
            results.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            string xml = mFeedDocument.OuterXml;
            int firstItemPosition = xml.IndexOf("<item>");
            int lastItemPosition = xml.LastIndexOf("</item>") + "</item>".Length;
            int secondHalfLength = xml.Length - lastItemPosition;
            string trimmedXml = xml.Substring(0, firstItemPosition);
            trimmedXml = trimmedXml + xml.Substring(lastItemPosition, secondHalfLength);

            //Now construct the temporary document
            XmlDocument trimmedDoc = new XmlDocument();
            trimmedDoc.LoadXml(trimmedXml);
            XmlNode channel = trimmedDoc.SelectSingleNode("//rss/channel");
            foreach (FeedItem item in FeedItems)
            {
                if (!onlyChecked)
                {
                    XmlNode importNode = channel.OwnerDocument.ImportNode(item.Node, true);
                    channel.AppendChild(importNode);
                }
                else if (item.HasFtpUploadTag(FTP.FtpUploadTags.NonItunesUpload))
                {
                    XmlNode importNode = channel.OwnerDocument.ImportNode(item.Node, true);
                    channel.AppendChild(importNode);
                }
            }

            xml = GetPrettyXmlText(trimmedDoc);
            //Convert to nonItunes if necessary.
            if (ItunesFeed)
            {
                string stylesheet = Resources.XSLTRemoveItunes;

                using (StringReader stream = new StringReader(xml))
                {
                    XPathDocument xpathDoc = new XPathDocument(stream);
                    XslCompiledTransform transform = new XslCompiledTransform();
                    using (XmlReader xsltReader = XmlReader.Create(new StringReader(stylesheet)))
                    {
                        transform.Load(xsltReader);
                    }

                    using (StringWriter writer = new StringWriter(results))
                    {
                        XmlTextWriter xmlWriter = new XmlTextWriter(writer);
                        transform.Transform(xpathDoc, null, xmlWriter);
                        xml = writer.ToString();
                        XmlDocument newXmlDoc = new XmlDocument();
                        newXmlDoc.LoadXml(xml);
                        xml = GetPrettyXmlText(newXmlDoc);
                    }
                }
            }
            string directory = GetUserDataDirectory() + @"\NonItunesUploads\";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string pathToTempFile = directory + FtpServerPathNonItunesFile;

            if (File.Exists(pathToTempFile))
                File.Delete(pathToTempFile);

            //Now save the document to a temporary path
            using (StreamWriter outfile = new StreamWriter(pathToTempFile))
            {
                outfile.Write(xml);
            }

            return pathToTempFile;
        }


        /// <summary>
        /// Saves this document to a temporary location, to be used for FTP upload.  
        /// </summary>
        /// <param name="onlyChecked">Set to true if you want this save to only include the
        /// checked items.  Otherwise, set to false and all items will be saved for upload.</param>
        /// <returns>Path to the temporary file.</returns>
        public string SaveItunesForUpload(bool onlyChecked)
        {
            StringBuilder results = new StringBuilder();
            results.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            string xml = mFeedDocument.OuterXml;
            int firstItemPosition = xml.IndexOf("<item>");
            int lastItemPosition = xml.LastIndexOf("</item>") + "</item>".Length;
            int secondHalfLength = xml.Length - lastItemPosition;
            string trimmedXml = xml.Substring(0, firstItemPosition);
            trimmedXml = trimmedXml + xml.Substring(lastItemPosition, secondHalfLength);

            //Now construct the temporary document
            XmlDocument trimmedDoc = new XmlDocument();
            trimmedDoc.LoadXml(trimmedXml);
            XmlNode channel = trimmedDoc.SelectSingleNode("//rss/channel");
            foreach (FeedItem item in FeedItems)
            {
                if (!onlyChecked)
                {
                    XmlNode importNode = channel.OwnerDocument.ImportNode(item.Node, true);
                    channel.AppendChild(importNode);
                }
                else if (item.HasFtpUploadTag(FTP.FtpUploadTags.ITunesUpload))
                {
                    XmlNode importNode = channel.OwnerDocument.ImportNode(item.Node, true);
                    channel.AppendChild(importNode);
                }
            }

            xml = GetPrettyXmlText(trimmedDoc);
           
            string directory = GetUserDataDirectory() + @"\ItunesUploads\";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string pathToTempFile = directory + FtpServerPathItunesFile;

            if (File.Exists(pathToTempFile))
                File.Delete(pathToTempFile);

            //Now save the document to a temporary path
            using (StreamWriter outfile = new StreamWriter(pathToTempFile))
            {
                outfile.Write(xml);
            }

            return pathToTempFile;
        }

        public String GetPrettyXmlText()
        {
            return GetPrettyXmlText(mFeedDocument);
        }

        private string GetPrettyXmlText(XmlDocument doc)
        {
            String result = "";

            using (MemoryStream memStream = new MemoryStream())
            {
                using (XmlTextWriter writer = new XmlTextWriter(memStream, Encoding.Unicode))
                {
                    // Load the XmlDocument with the XML.

                    writer.Formatting = Formatting.Indented;

                    // Write the XML into a formatting XmlTextWriter
                    doc.WriteContentTo(writer);
                    writer.Flush();
                    memStream.Flush();

                    // Have to rewind the MemoryStream in order to read
                    // its contents.
                    memStream.Position = 0;

                    // Read MemoryStream contents into a StreamReader.
                    StreamReader reader = new StreamReader(memStream);

                    // Extract the text from the StreamReader.
                    String formattedXML = reader.ReadToEnd();

                    result = formattedXML;
                }
            }

            return result;
        }

        public void SaveAs(string newFilePath)
        {
            mAdditionalFeedDataPath = GetLocalDataPath(newFilePath);
            mAdditionalFeedDataXml.SelectSingleNode(LOCAL_FEED_PATH_XPATH).InnerText = mAdditionalFeedDataPath;
            mAdditionalFeedDataXml.Save(mAdditionalFeedDataPath);
            mFeedDocument.Save(newFilePath);
            FeedPath = newFilePath;
            this.Dirty = false;
            mIsNew = false;
        }

        /// <summary>
        /// Returns all the FeedItems contains in this object.
        /// </summary>
        public List<FeedItem> FeedItems
        {
            get
            {
                return mFeedItems;
            }
        }   
    }
}

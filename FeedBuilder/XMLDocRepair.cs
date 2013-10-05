using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FeedBuilder
{
    class XMLDocRepair
    {
        /// <summary>
        /// Uses a template XmlDocument to repair the doc object.  
        /// Primarily, this method will go through the feed document and compare
        /// it's contents to that which is laid out in the template.  Nodes that are
        /// missing from the feed will be added.  Additional information in the feed,
        /// if any, will NOT be stripped.
        /// </summary>
        /// <param name="feed">The feed to be repaired.</param>
        /// <param name="template">The template to which this feed is compared.</param>
        public static void RepairFeed(XmlDocument feed, XmlDocument template)
        {

        }
    }
}

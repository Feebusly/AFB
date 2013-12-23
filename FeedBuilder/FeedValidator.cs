using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace FeedBuilder
{
    public enum ValidationErrorCode
    {
        BAD_CHARACTER_DATA = 1,
        MAL_FORMED_DATE = 2,
        MAL_FORMED_EMAIL_ADDRESS = 4,
        MAL_FORMED_URI = 8,
        RSS_VERSION = 16,
        CHANNEL_DATA = 32,
        IMAGE_DATA = 64,
        LANGUAGE = 128
    }

    public struct ValidationError
    {
        public ValidationErrorCode CODE;
        public string MESSAGE;

        public ValidationError(ValidationErrorCode code, string errorMessage)
        {
            CODE = code;
            MESSAGE = errorMessage;
        }
    }
        
    /// <summary>
    /// A class to perform validation on the FeedData object and to provide error code.
    /// </summary>
    public static class FeedValidator
    {

        static FeedValidator()
        {

        }

        public static string EncodeXmlText(string text)
        {
            string newText = text;
            if (newText != null)
            {
                newText = newText.Replace("&", "&#x26;");
                newText = newText.Replace(">", "&#3E;");
                newText = newText.Replace("<", "&#3C;");
            }
            return newText;
        }

        public static string DecodeXmlText(string text)
        {
            string newText = text;
            if (newText != null)
            {
                newText = newText.Replace("&#x26;", "&");
                newText = newText.Replace("&#3E;", ">");
                newText = newText.Replace("&#3C;", "<");
            }
            return newText;
        }

        public static IDictionary<string, ValidationError> ValidateFeedData(FeedData data)
        {
            XmlDocument doc = data.Document;
            IDictionary<string, ValidationError> errors = new Dictionary<string, ValidationError>()
                as IDictionary<string, ValidationError>;

            foreach (XmlNode node in doc.SelectNodes(FeedData.CHANNEL))
            {
                SearchNodeForErrors(node, errors, data.NamespaceManager);
            }

            return errors;
        }

        /// <summary>
        /// date-time   =  [ day "," ] date time        ; dd mm yy
        ///                                             ; hh:mm:ss zzz
        ///
        /// day         =  "Mon"  / "Tue" /  "Wed"  / "Thu"
        ///             /  "Fri"  / "Sat" /  "Sun"
        ///
        /// date        =  1*2DIGIT month 2DIGIT        ; day month year
        ///                                             ; e.g. 20 Jun 82
        ///
        /// month       =  "Jan"  /  "Feb" /  "Mar"  /  "Apr"
        ///             /  "May"  /  "Jun" /  "Jul"  /  "Aug"
        ///             /  "Sep"  /  "Oct" /  "Nov"  /  "Dec"
        /// 
        /// time        =  hour zone                    ; ANSI and Military
        ///
        /// hour        =  2DIGIT ":" 2DIGIT [":" 2DIGIT]
        ///                                             ; 00:00:00 - 23:59:59
        ///
        /// zone        =  "UT"  / "GMT"                ; Universal Time
        ///                                             ; North American : UT
        ///             /  "EST" / "EDT"                ; Eastern:  - 5/ - 4
        ///             /  "CST" / "CDT"                ; Central:  - 6/ - 5
        ///             /  "MST" / "MDT"                ; Mountain: - 7/ - 6
        ///             /  "PST" / "PDT"                ; Pacific:  - 8/ - 7
        ///             /  1ALPHA                       ; Military: Z = UT;
        ///                                             ; A:-1; (J not used)
        ///                                             ; M:-12; N:+1; Y:+12
        ///             / ( ("+" / "-") 4DIGIT )        ; Local differential
        ///                                             ; hours+min. (HHMM)
        /// </summary>
        /// <param name="dateTimeString"></param>
        /// <remarks>
        /// E.g. Mon, 25 Jun 2012 00:00:00 -7
        ///      Wed, 15 Jun 2005 19:00:00 GMT
        ///      Sun, 6 Jan 2013 10:30:00 -0800
        /// </remarks>
        /// <returns></returns>
        public static bool TryParseDateTime(string dateTimeString, out DateTime datetime)
        {
            //First switch the zone to a numeric.
            string dt = dateTimeString;
            datetime = DateTime.MinValue;
            //bool replaced = false;
            dt = dt.TrimEnd(' ');

            if (dt.EndsWith("UT"))
            {
                dt = dt.Replace("UT", "+00:00");
                //replaced = true;
            }
            else if (dt.EndsWith("GMT"))
            { 
                dt = dt.Replace("GMT", "+00:00");
                //replaced = true;
            }
            else if (dt.EndsWith("EST"))
            {
                dt = dt.Replace("EST", "-05:00");
                //replaced = true;
            }
            else if (dt.EndsWith("EDT"))
            {
                dt = dt.Replace("EDT", "-04:00");
               // replaced = true;
            }
            else if (dt.EndsWith("CST"))
            {
                dt = dt.Replace("CST", "-06:00");
                //replaced = true;
            }
            else if (dt.EndsWith("CDT"))
            {
                dt = dt.Replace("CDT", "-05:00");
                //replaced = true;
            }
            else if (dt.EndsWith("MST"))
            {
                dt = dt.Replace("MST", "-07:00");
                //replaced = true;
            }
            else if (dt.EndsWith("MDT"))
            {
                dt = dt.Replace("MDT", "-06:00");
                //replaced = true;
            }
            else if (dt.EndsWith("PST"))
            {
                dt = dt.Replace("PST", "-08:00");
                //replaced = true;
            }
            else if (dt.EndsWith("PDT"))
            {
                dt = dt.Replace("PDT", "-07:00");
               // replaced = true;
            }
            else if (dt.EndsWith("Z"))
            {
                dt = dt.Replace("Z", "+00:00");
                //replaced = true;
            }
            else if (dt.EndsWith("A"))
            {
                dt = dt.Replace("A", "-01:00");
                //replaced = true;
            }
            else if (dt.EndsWith("M"))
            {
                dt = dt.Replace("M", "-12:00");
                //replaced = true;
            }
            else if (dt.EndsWith("N"))
            {
                dt = dt.Replace("N", "+01:00");
                //replaced = true;
            }
            else if (dt.EndsWith("Y"))
            {
                dt = dt.Replace("Y", "+12:00");
                //replaced = true;
            }

            //if (!replaced)
            //{
                //dt.TrimEnd(' ');
                //int spaceIndex = dt.LastIndexOf(' ');
                //string hoursMins = dt.Substring(spaceIndex);
                //int additionalZeroes = 6 - hoursMins.Length;
                //StringBuilder sb = new StringBuilder(hoursMins);
                //sb.Append('0', additionalZeroes);
                //string newHoursMins = sb.ToString();
                //newHoursMins = newHoursMins.Insert(4, ":");
                //dt = dt.Replace(hoursMins, newHoursMins);
            //}

            bool parsed = false;

            //Work on the z's
            string z = "zzzzzz";
            while (!parsed)
            {
                parsed = DateTime.TryParseExact(dt, "ddd, dd MMM yyyy HH:mm:ss " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            //Try the d's
           z = "zzzzzz";
           while (!parsed)
           {
                parsed = DateTime.TryParseExact(dt, "ddd, d MMM yyyy HH:mm:ss " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            //Try the y's
            z = "zzzzzz";
            while (!parsed)
            {
                parsed = DateTime.TryParseExact(dt, "ddd, d MMM yy HH:mm:ss " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            //Try the H's
            z = "zzzzzz";
            while (!parsed)
            {
                parsed = DateTime.TryParseExact(dt, "ddd, d MMM yyyy H:mm:ss " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            //Try the m's
            z = "zzzzzz";
            while (!parsed)
            {
                parsed = DateTime.TryParseExact(dt, "ddd, d MMM yyyy HH:m:ss " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            //Try the s's
            z = "zzzzzz";
            while (!parsed)
            {
                parsed = DateTime.TryParseExact(dt, "ddd, d MMM yyyy HH:mm:s " + z, CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out datetime);

                if (!parsed)
                {
                    if (z == "")
                        break;
                    z = z.Substring(0, z.Length - 1);
                }
            }

            if (!parsed)
                throw new FormatException(string.Format(
                    "Could not parse date \"{0}\" according to format \"ddd, d MMM yyyy HH:mm:s z\".", dateTimeString));

            return parsed;
        }

        /// <summary>
        /// Searches the nodes InnerText and Attributes for validation errors.
        /// Errors are recorded in the FeedErrors property with the node's name 
        /// as the key, and an error code as the value.  If an error was found in 
        /// an attribute, then the key is NodeName:AttrName.
        /// </summary>
        /// <param name="node"></param>
        private static void SearchNodeForErrors(XmlNode node, IDictionary<string, ValidationError> errors,
            XmlNamespaceManager nsmgr)
        {
            //Don't check for errors if this is an ituens node and we don't have an
            //itunes namespace.  Bad things will happen.
            if (node.Name.Contains("itunes") && !nsmgr.HasNamespace("itunes"))
                return;

            string xpath = FindXPath(node, nsmgr);
            DateTime result;
            switch (xpath)
            {
                case FeedData.LAST_BUILD_DATE:
                    if (node.InnerText != string.Empty)
                    {
                        if (!TryParseDateTime(node.InnerText, out result))
                        {
                            errors.Add(xpath, new ValidationError(ValidationErrorCode.MAL_FORMED_DATE,
                               string.Format("Error parsing feed last build date from '{0}'.", node.InnerText)));
                        }
                    }
                    break;

                case FeedData.PUB_DATE:
                    if (node.InnerText != string.Empty)
                    {
                        if (!TryParseDateTime(node.InnerText, out result))
                        {
                            errors.Add(xpath, new ValidationError(ValidationErrorCode.MAL_FORMED_DATE,
                               string.Format("Error parsing feed publication date from '{0}'.", node.InnerText)));
                        }
                    }
                    break;

                case FeedData.ITEMS + "/" + FeedItem.PUB_DATE_XPATH:
                    if (node.InnerText != string.Empty)
                    {
                        if (!TryParseDateTime(node.InnerText, out result))
                        {
                            errors.Add(xpath, new ValidationError(ValidationErrorCode.MAL_FORMED_DATE,
                                string.Format("Error parsing item publication date from '{0}'.", node.InnerText)));
                        }
                    }
                    break;

                default:
                    foreach (XmlNode child in node.SelectNodes("./*"))
                    {
                        SearchNodeForErrors(child, errors, nsmgr);
                    }
                    break;
            }
        }

        public static string BuildItemTag(XmlNode node, XmlNamespaceManager nsmgr)
        {
            string xpath = FindXPath(node, nsmgr);
            if (xpath == null)
                return null;

            int itemPosition = 0;
            foreach (XmlNode child in node.OwnerDocument.SelectNodes(FeedData.ITEMS, nsmgr))
            {
                XmlNode testNode = child.SelectSingleNode(node.Name, nsmgr);
                if (testNode != null && testNode.Equals(node))
                    break;
                ++itemPosition;
            }
            return string.Format("{0}[{1}]", xpath, itemPosition);
        }

        static string FindXPath(XmlNode node, XmlNamespaceManager nsmgr)
        {
            StringBuilder builder = new StringBuilder();
            while (node != null)
            {
                switch (node.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, "/@" + node.Name);
                        node = ((XmlAttribute)node).OwnerElement;
                        break;
                    case XmlNodeType.Element:
                        //Put an index number into element if it has identical siblings.
                        XmlNodeList nodes = null;
                        if (node.ParentNode == null)
                        { }
                        if ((nodes = node.ParentNode.SelectNodes(node.Name, nsmgr)) != null && nodes.Count > 1)
                        {
                            int position = 0;
                            foreach (XmlNode testNode in nodes)
                            {
                                if (testNode != null && testNode.Equals(node))
                                    break;
                                ++position;
                            }
                            builder.Insert(0, string.Format("/{0}[{1}]", node.Name, position));
                        }
                        else
                            builder.Insert(0, "/" + node.Name);
                        
                        node = node.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        builder.Insert(0, "/");
                        return builder.ToString();
                    default:
                        return null;
                }
            }
            return null;
        }

        static int FindElementIndex(XmlElement element)
        {
            XmlNode parentNode = element.ParentNode;
            if (parentNode is XmlDocument)
            {
                return 1;
            }
            XmlElement parent = (XmlElement)parentNode;
            int index = 1;
            foreach (XmlNode candidate in parent.ChildNodes)
            {
                if (candidate is XmlElement && candidate.Name == element.Name)
                {
                    if (candidate == element)
                    {
                        return index;
                    }
                    index++;
                }
            }
            throw new ArgumentException("Couldn't find element within parent");
        }
    }
}

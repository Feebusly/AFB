using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using FeedBuilder.Properties;
using System.Collections.Specialized;

namespace FeedBuilder
{
    public static class Language
    {
        private static OrderedDictionary mLanguages;
        private static IList<string> mCodes;
        private static IList<CodeNamePair> mPairs;

        static Language()
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Resources.Languages);
            XmlNodeList langNodes = doc.SelectNodes("//languages/language");

            mLanguages = new OrderedDictionary(langNodes.Count);
            List<string> codes = new List<string>(langNodes.Count);
            List<CodeNamePair> pairs = new List<CodeNamePair>(langNodes.Count);

            foreach (XmlNode node in langNodes)
            {
                string name = node.Attributes["name"].Value;
                string code = node.Attributes["code"].Value;
                mLanguages.Add(code, name);
                codes.Add(code);
                pairs.Add(new CodeNamePair(code, name));
            }
            mCodes = codes.AsReadOnly();
            mPairs = pairs.AsReadOnly();
        }

        public static IList<CodeNamePair> Pairs
        {
            get { return mPairs; }
        }

        public static IList<string> Codes
        {
            get { return mCodes; }
        }

        public static string Name(string code)
        {
            string name = null;
            if (mLanguages.Contains(code))
            {
                name = mLanguages[code] as string;
            }
            return name;
        }
    }

    public class CodeNamePair
    {
        public string CODE;
        public string NAME;

        public CodeNamePair(string code, string name)
        {
            CODE = code;
            NAME = name;
        }

        public override string ToString()
        {
            return NAME;
        }
    }
}

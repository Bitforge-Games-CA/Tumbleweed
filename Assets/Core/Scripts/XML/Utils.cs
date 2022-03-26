using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Tumbleweed.Core.XML
{

    public class Utils
    {
        public static TextAsset CreateTextAssetFromXML(string path)
        {
            string text = File.ReadAllText(path);
            TextAsset textAsset = new TextAsset(text);
            return textAsset;
        }

        public static XmlDocument ParseXMLFile(string XMLData)
        {
            XmlDocument XMLDoc = new XmlDocument();
            XMLDoc.Load(new StringReader(XMLData));
            return XMLDoc;
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class LoadXML : MonoBehaviour
{
    public TextAsset XMLMayorData;
    public TextAsset XMLSettlerData;

    // Start is called before the first frame update
    void Start()
    {
        // characterData
        string characterDataMayor = XMLMayorData.text;
        XmlDocument CDXML = ParseXMLFile(characterDataMayor);

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Mayor"))
        {
            CharacterData CD = character.AddComponent<CharacterData>();

            CD.Health = CDXML.GetElementsByTagName("Health").Item(0).InnerText;

            CD.Level = CDXML.GetElementsByTagName("Level").Item(0).InnerText;

            CD.MeleeDamage = CDXML.GetElementsByTagName("MeleeDamage").Item(0).InnerText;

        }

        string characterDataSettler = XMLSettlerData.text;
        CDXML = ParseXMLFile(characterDataSettler);

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Settler"))
        {
            CharacterData CD = character.AddComponent<CharacterData>();

            CD.Health = CDXML.GetElementsByTagName("Health").Item(0).InnerText;

            CD.Level = CDXML.GetElementsByTagName("Level").Item(0).InnerText;

            CD.MeleeDamage = CDXML.GetElementsByTagName("MeleeDamage").Item(0).InnerText;

        }



        // more Data




    }

    XmlDocument ParseXMLFile(string XMLData)
    {
        XmlDocument XMLDoc = new XmlDocument();
        XMLDoc.Load(new StringReader(XMLData));
        return XMLDoc;
    }
}

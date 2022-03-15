using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("CharacterData")]
public class CharacterData : MonoBehaviour
{

    //[XmlElement("")]
    //public string;

    // Info
    [XmlElement("Name")]
    public string Name;

    [XmlElement("Biography")]
    public string Biography;

    [XmlElement("Background")]
    public string Background;

    [XmlElement("Type")]
    public string Type;



    // Stats
    [XmlElement("Health")]
    public string Health;

    [XmlElement("Level")]
    public string Level;

    [XmlElement("MeleeDamage")]
    public string MeleeDamage;

}

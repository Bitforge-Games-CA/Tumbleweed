using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("CharacterData")]
public class CharacterData : MonoBehaviour
{
    [XmlElement("Health")]
    public string Health;

    [XmlElement("Level")]
    public string Level;

    [XmlElement("MeleeDamage")]
    public string MeleeDamage;

}

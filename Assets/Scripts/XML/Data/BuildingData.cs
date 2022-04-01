using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("BuildingData")]
public class BuildingData : MonoBehaviour
{
    // Info
    [XmlElement("Name")]
    public string BuildingName;

    [XmlElement("Description")]
    public string BuildingDescription;



    // Stats
    [XmlElement("HealthScore")]
    public string BuildingHealthScore;




    // Properties
    [XmlElement("IsRotateable")]
    public string BuildingIsRotatable;




}

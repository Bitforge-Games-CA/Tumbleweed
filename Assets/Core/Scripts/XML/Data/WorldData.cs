using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Tumbleweed.Core.XML.Data
{

    [XmlRoot("WorldData")]
    public class WorldData : MonoBehaviour
    {
        // Info
        [XmlElement("WorldPositionX")]
        public int WorldPositionX;

        [XmlElement("WorldPositionY")]
        public int WorldPositionY;

        [XmlElement("WorldPositionZ")]
        public int WorldPositionZ;


        [XmlElement("CurrentLayer")]
        public int CurrentLayer;


        [XmlElement("SleepingSpot")]
        public Vector3Int SleepingSpot;

    }

}
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Tumbleweed.Core.XML.Data
{

    [XmlRoot("CharacterWorldSaveData")]
    public class CharacterWorldSaveData
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


        public CharacterWorldSaveData ToSaveData(CharacterWorldData CWD)
        {
            CharacterWorldSaveData CWSD = new CharacterWorldSaveData
            {
                WorldPositionX = CWD.WorldPositionX,
                WorldPositionY = CWD.WorldPositionY,
                WorldPositionZ = CWD.WorldPositionZ,

                CurrentLayer = CWD.CurrentLayer,
                SleepingSpot = CWD.SleepingSpot

               

            };

            return CWSD;
        }

    }

}
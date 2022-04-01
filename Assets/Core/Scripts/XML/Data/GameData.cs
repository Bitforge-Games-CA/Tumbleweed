using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System;

namespace Tumbleweed.Core.XML.Data
{

    [XmlRoot("GameData")]
    public class GameData
    {

        // Buildings Save Data


        // Characters Save Data

        [XmlArray("Characters"), XmlArrayItem("CharacterData")]
        public List<CharacterSaveData> Characters = new List<CharacterSaveData>();

        // Character World Save Data

        [XmlArray("CharactersWorld"), XmlArrayItem("CharactersWorldData")]
        public List<CharacterWorldSaveData> CharactersWorldData = new List<CharacterWorldSaveData>();

        // World Save Data



    }

}

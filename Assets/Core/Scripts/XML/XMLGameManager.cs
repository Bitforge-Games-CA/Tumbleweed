using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Tumbleweed.Core.XML.Data;


namespace Tumbleweed.Core.XML
{

    public class XMLGameManager : MonoBehaviour
    {
        public static string DataPath;

        public static GameData XMLGameData = new GameData();

        public static int SaveGameIndex;

        private void Start()
        {
            DataPath = Application.dataPath + "/Core/GameData";
            SaveGameIndex = Directory.GetFiles(DataPath).Length + 1;
        }

        public static void SaveGameData(GameData data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));

            FileStream stream = new FileStream(DataPath + "/File_" + SaveGameIndex +  ".xml", FileMode.Create);

            serializer.Serialize(stream, data);

            stream.Close();

            SaveGameIndex++;

            Debug.Log("Save Finished");
        }

        public static GameData LoadGameData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));

            FileStream stream = new FileStream(DataPath, FileMode.Open);

            GameData data = serializer.Deserialize(stream) as GameData;

            stream.Close();

            Debug.Log("Load Finished");

            return data;
        }
    }
}
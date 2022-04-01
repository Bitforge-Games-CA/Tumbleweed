using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;

public class LoadXML : MonoBehaviour
{
    public string XMLDataPath;

    public TextAsset XMLTextsData;

    public TextAsset XMLMayorMaleCharacterData;
    public TextAsset XMLSettlerMaleCharacterData;
    public TextAsset XMLSettlerFemaleCharacterData;

    public TextAsset XMLTentBuildingData;

    // Start is called before the first frame update
    void Start() 
    { 
    
        if (Application.isEditor == true){

            XMLDataPath = "Assets/XML";

            // TextsData
            XMLTextsData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/ENTextsData.xml", typeof(TextAsset)) as TextAsset;

            // CharacterData
            XMLMayorMaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/MayorMaleData.xml", typeof(TextAsset)) as TextAsset;
            XMLSettlerMaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/SettlerMaleData.xml", typeof(TextAsset)) as TextAsset;
            XMLSettlerFemaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/SettlerFemaleData.xml", typeof(TextAsset)) as TextAsset;

            // BuildingData
            XMLTentBuildingData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/TentData.xml", typeof(TextAsset)) as TextAsset;

        } else
        {
            XMLDataPath = Application.dataPath + "/XML";

            // TextsData
            XMLTextsData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/ENTextsData.xml", typeof(TextAsset)) as TextAsset;

            // CharacterData
            XMLMayorMaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/MayorMaleData.xml", typeof(TextAsset)) as TextAsset;
            XMLSettlerMaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/SettlerMaleData.xml", typeof(TextAsset)) as TextAsset;
            XMLSettlerFemaleCharacterData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/SettlerFemaleData.xml", typeof(TextAsset)) as TextAsset;

            // BuildingData
            XMLTentBuildingData = AssetDatabase.LoadAssetAtPath(XMLDataPath + "/TentData.xml", typeof(TextAsset)) as TextAsset;
        }

        // TextsData
        string textsData = XMLTextsData.text;
        XmlDocument TD = ParseXMLFile(textsData);

        // CharacterData
        string characterDataMayor = XMLMayorMaleCharacterData.text;
        XmlDocument CDXML = ParseXMLFile(characterDataMayor);


        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Mayor Male"))
        {
            CharacterData CD = character.AddComponent<CharacterData>();

            // Info
            CD.Gender = CDXML.GetElementsByTagName("Gender").Item(0).InnerText;
            CD.Role = CDXML.GetElementsByTagName("Role").Item(0).InnerText;

            // Stats
            CD.HealthScore = CDXML.GetElementsByTagName("HealthScore").Item(0).InnerText;
            CD.Level = CDXML.GetElementsByTagName("Level").Item(0).InnerText;
            CD.XP = CDXML.GetElementsByTagName("XP").Item(0).InnerText;
            CD.MeleeDamage = CDXML.GetElementsByTagName("MeleeDamage").Item(0).InnerText;

            // Abilites
            CD.Ability_Persuade_Name = TD.GetElementsByTagName("Ability_01_Name").Item(0).InnerText;
            CD.Ability_Persuade_Desc = TD.GetElementsByTagName("Ability_01_Desc").Item(0).InnerText;
            CD.Ability_Persuade_Level = CDXML.GetElementsByTagName("Level").Item(1).InnerText;
            CD.Ability_Persuade_XP = CDXML.GetElementsByTagName("XP").Item(1).InnerText;

            CD.Ability_Rally_Name = TD.GetElementsByTagName("Ability_02_Name").Item(0).InnerText;
            CD.Ability_Rally_Desc = TD.GetElementsByTagName("Ability_02_Desc").Item(0).InnerText;
            CD.Ability_Rally_Level = CDXML.GetElementsByTagName("Level").Item(2).InnerText;
            CD.Ability_Rally_XP = CDXML.GetElementsByTagName("XP").Item(2).InnerText;

            // StatModifiers
            CD.Strength = CDXML.GetElementsByTagName("Strength").Item(0).InnerText;
            CD.Endurance = CDXML.GetElementsByTagName("Endurance").Item(0).InnerText;
            CD.Resilience = CDXML.GetElementsByTagName("Resilience").Item(0).InnerText;
            CD.Dexterity = CDXML.GetElementsByTagName("Dexterity").Item(0).InnerText;
            CD.Intellect = CDXML.GetElementsByTagName("Intellect").Item(0).InnerText;
            CD.Perception = CDXML.GetElementsByTagName("Perception").Item(0).InnerText;
            CD.Willpower = CDXML.GetElementsByTagName("Willpower").Item(0).InnerText;
            CD.Wisdom = CDXML.GetElementsByTagName("Wisdom").Item(0).InnerText;
            CD.Charisma = CDXML.GetElementsByTagName("Charisma").Item(0).InnerText;
            CD.Luck = CDXML.GetElementsByTagName("Luck").Item(0).InnerText;

            // Traits


        }

        string characterDataSettlerMale = XMLSettlerMaleCharacterData.text;
        CDXML = ParseXMLFile(characterDataSettlerMale);

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Settler Male"))
        {
            CharacterData CD = character.AddComponent<CharacterData>();

            // Info
            CD.Name = NameGenerator.GenerateName("Settler", "Male");
            CD.Gender = CDXML.GetElementsByTagName("Gender").Item(0).InnerText;
            CD.Role = CDXML.GetElementsByTagName("Role").Item(0).InnerText;

            // Skills
            CD.Skill_Flatten_Name = TD.GetElementsByTagName("Skill_00_Name").Item(0).InnerText;
            CD.Skill_Flatten_Desc = TD.GetElementsByTagName("Skill_00_Desc").Item(0).InnerText;
            CD.Skill_Flatten_Level = CDXML.GetElementsByTagName("Level").Item(1).InnerText;
            CD.Skill_Flatten_XP = CDXML.GetElementsByTagName("XP").Item(1).InnerText;

            CD.Skill_Mine_Name = TD.GetElementsByTagName("Skill_01_Name").Item(0).InnerText;
            CD.Skill_Mine_Desc = TD.GetElementsByTagName("Skill_01_Desc").Item(0).InnerText;
            CD.Skill_Mine_Level = CDXML.GetElementsByTagName("Level").Item(2).InnerText;
            CD.Skill_Mine_XP = CDXML.GetElementsByTagName("XP").Item(2).InnerText;

            CD.Skill_Forage_Name = TD.GetElementsByTagName("Skill_02_Name").Item(0).InnerText;
            CD.Skill_Forage_Desc = TD.GetElementsByTagName("Skill_02_Desc").Item(0).InnerText;
            CD.Skill_Forage_Level = CDXML.GetElementsByTagName("Level").Item(3).InnerText;
            CD.Skill_Forage_XP = CDXML.GetElementsByTagName("XP").Item(3).InnerText;







            // Stats
            CD.HealthScore = CDXML.GetElementsByTagName("HealthScore").Item(0).InnerText;
            CD.Level = CDXML.GetElementsByTagName("Level").Item(0).InnerText;
            CD.XP = CDXML.GetElementsByTagName("XP").Item(0).InnerText;
            CD.MeleeDamage = CDXML.GetElementsByTagName("MeleeDamage").Item(0).InnerText;

            // StatModifiers
            CD.Strength = CDXML.GetElementsByTagName("Strength").Item(0).InnerText;
            CD.Endurance = CDXML.GetElementsByTagName("Endurance").Item(0).InnerText;
            CD.Resilience = CDXML.GetElementsByTagName("Resilience").Item(0).InnerText;
            CD.Dexterity = CDXML.GetElementsByTagName("Dexterity").Item(0).InnerText;
            CD.Intellect = CDXML.GetElementsByTagName("Intellect").Item(0).InnerText;
            CD.Perception = CDXML.GetElementsByTagName("Perception").Item(0).InnerText;
            CD.Willpower = CDXML.GetElementsByTagName("Willpower").Item(0).InnerText;
            CD.Wisdom = CDXML.GetElementsByTagName("Wisdom").Item(0).InnerText;
            CD.Charisma = CDXML.GetElementsByTagName("Charisma").Item(0).InnerText;
            CD.Luck = CDXML.GetElementsByTagName("Luck").Item(0).InnerText;

            // Traits





        }

        string characterDataSettlerFemale = XMLSettlerFemaleCharacterData.text;
        CDXML = ParseXMLFile(characterDataSettlerFemale);

        foreach (GameObject character in GameObject.FindGameObjectsWithTag("Settler Female"))
        {
            CharacterData CD = character.AddComponent<CharacterData>();

            // Info
            CD.Name = NameGenerator.GenerateName("Settler", "Female");
            CD.Gender = CDXML.GetElementsByTagName("Gender").Item(0).InnerText;
            CD.Role = CDXML.GetElementsByTagName("Role").Item(0).InnerText;


            // Skills
            CD.Skill_Flatten_Name = TD.GetElementsByTagName("Skill_00_Name").Item(0).InnerText;
            CD.Skill_Flatten_Desc = TD.GetElementsByTagName("Skill_00_Desc").Item(0).InnerText;
            CD.Skill_Flatten_Level = CDXML.GetElementsByTagName("Level").Item(1).InnerText;
            CD.Skill_Flatten_XP = CDXML.GetElementsByTagName("XP").Item(1).InnerText;

            CD.Skill_Mine_Name = TD.GetElementsByTagName("Skill_01_Name").Item(0).InnerText;
            CD.Skill_Mine_Desc = TD.GetElementsByTagName("Skill_01_Desc").Item(0).InnerText;
            CD.Skill_Mine_Level = CDXML.GetElementsByTagName("Level").Item(2).InnerText;
            CD.Skill_Mine_XP = CDXML.GetElementsByTagName("XP").Item(2).InnerText;

            CD.Skill_Forage_Name = TD.GetElementsByTagName("Skill_02_Name").Item(0).InnerText;
            CD.Skill_Forage_Desc = TD.GetElementsByTagName("Skill_02_Desc").Item(0).InnerText;
            CD.Skill_Forage_Level = CDXML.GetElementsByTagName("Level").Item(3).InnerText;
            CD.Skill_Forage_XP = CDXML.GetElementsByTagName("XP").Item(3).InnerText;







            // Stats
            CD.HealthScore = CDXML.GetElementsByTagName("HealthScore").Item(0).InnerText;
            CD.Level = CDXML.GetElementsByTagName("Level").Item(0).InnerText;
            CD.XP = CDXML.GetElementsByTagName("XP").Item(0).InnerText;
            CD.MeleeDamage = CDXML.GetElementsByTagName("MeleeDamage").Item(0).InnerText;

            // StatModifiers
            CD.Strength = CDXML.GetElementsByTagName("Strength").Item(0).InnerText;
            CD.Endurance = CDXML.GetElementsByTagName("Endurance").Item(0).InnerText;
            CD.Resilience = CDXML.GetElementsByTagName("Resilience").Item(0).InnerText;
            CD.Dexterity = CDXML.GetElementsByTagName("Dexterity").Item(0).InnerText;
            CD.Intellect = CDXML.GetElementsByTagName("Intellect").Item(0).InnerText;
            CD.Perception = CDXML.GetElementsByTagName("Perception").Item(0).InnerText;
            CD.Willpower = CDXML.GetElementsByTagName("Willpower").Item(0).InnerText;
            CD.Wisdom = CDXML.GetElementsByTagName("Wisdom").Item(0).InnerText;
            CD.Charisma = CDXML.GetElementsByTagName("Charisma").Item(0).InnerText;
            CD.Luck = CDXML.GetElementsByTagName("Luck").Item(0).InnerText;

            // Traits



        }

        foreach (GameObject building in GameObject.FindGameObjectsWithTag("Building"))
        {
            if (building.name.Contains("Tent"))
            {
                string buildingDataTent = XMLTentBuildingData.text;
                XmlDocument BDTXML = ParseXMLFile(buildingDataTent);

                BuildingData BD = building.AddComponent<BuildingData>();

                // Info
                BD.BuildingName = TD.GetElementsByTagName("Building_00_Name").Item(0).InnerText;
                BD.BuildingDescription = TD.GetElementsByTagName("Building_00_Desc").Item(0).InnerText;

                // Stats
                BD.BuildingHealthScore = BDTXML.GetElementsByTagName("HealthScore").Item(0).InnerText;

                // Properties
                BD.BuildingIsRotatable = BDTXML.GetElementsByTagName("IsRotateable").Item(0).InnerText;
            }
        }

    }

    XmlDocument ParseXMLFile(string XMLData)
    {
        XmlDocument XMLDoc = new XmlDocument();
        XMLDoc.Load(new StringReader(XMLData));
        return XMLDoc;
    }
}

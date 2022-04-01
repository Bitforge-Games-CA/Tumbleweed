using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace Tumbleweed.Core.XML.Data
{

    [XmlRoot("CharacterSaveData")]
    public class CharacterSaveData
    {

        [XmlAttribute("id")]
        public int id;



        // Info
        [XmlElement("Name", Order = 1)]
        public string Name;

        [XmlElement("Gender", Order = 2)]
        public string Gender;

        [XmlElement("Biography", Order = 3)]
        public string Biography;

        [XmlElement("Background", Order = 4)]
        public string Background;

        [XmlElement("Role", Order = 5)]
        public string Role;



        // Stats
        [XmlElement("HealthScore", Order = 6)]
        public int HealthScore;

        [XmlElement("HungerScore", Order = 7)]
        public int HungerScore;

        [XmlElement("RestScore", Order = 8)]
        public int RestScore;

        [XmlElement("WorkEthicScore", Order = 9)]
        public int WorkEthicScore;

        [XmlElement("Level", Order = 10)]
        public int Level;

        [XmlElement("XP", Order = 11)]
        public int XP;

        [XmlElement("MeleeDamage", Order = 12)]
        public int MeleeDamage;



        // Stats/Abilities
        [XmlElement("Name", Order = 13)]
        public string Ability_Persuade_Name;

        [XmlElement("Level", Order = 14)]
        public int Ability_Persuade_Level;

        [XmlElement("XP", Order = 15)]
        public int Ability_Persuade_XP;

        [XmlElement("Description", Order = 16)]
        public string Ability_Persuade_Desc;


        [XmlElement("Name", Order = 17)]
        public string Ability_Rally_Name;

        [XmlElement("Level", Order = 18)]
        public int Ability_Rally_Level;

        [XmlElement("XP", Order = 19)]
        public int Ability_Rally_XP;

        [XmlElement("Description", Order = 20)]
        public string Ability_Rally_Desc;



        // Stats/Skills
        [XmlElement("Name", Order = 21)]
        public string Skill_Flatten_Name;

        [XmlElement("Level", Order = 22)]
        public int Skill_Flatten_Level;

        [XmlElement("XP", Order = 23)]
        public int Skill_Flatten_XP;

        [XmlElement("Description", Order = 24)]
        public string Skill_Flatten_Desc;



        [XmlElement("Name", Order = 25)]
        public string Skill_Mine_Name;

        [XmlElement("Level", Order = 26)]
        public int Skill_Mine_Level;

        [XmlElement("XP", Order = 27)]
        public int Skill_Mine_XP;

        [XmlElement("Description", Order = 28)]
        public string Skill_Mine_Desc;



        [XmlElement("Name", Order = 29)]
        public string Skill_Forage_Name;

        [XmlElement("Level", Order = 30)]
        public int Skill_Forage_Level;

        [XmlElement("XP", Order = 31)]
        public int Skill_Forage_XP;

        [XmlElement("Description", Order = 32)]
        public string Skill_Forage_Desc;



        // Stats/StatModifiers
        [XmlElement("Strength", Order = 33)]
        public string Strength;

        [XmlElement("Endurance", Order = 34)]
        public string Endurance;

        [XmlElement("Resilience", Order = 35)]
        public string Resilience;

        [XmlElement("Dexterity", Order = 36)]
        public string Dexterity;

        [XmlElement("Intellect", Order = 37)]
        public string Intellect;

        [XmlElement("Perception", Order = 38)]
        public string Perception;

        [XmlElement("Willpower", Order = 39)]
        public string Willpower;

        [XmlElement("Wisdom", Order = 40)]
        public string Wisdom;

        [XmlElement("Charisma", Order = 41)]
        public string Charisma;

        [XmlElement("Luck", Order = 42)]
        public string Luck;



        // Traits



        public CharacterSaveData ToSaveData(CharacterData CD)
        {
            CharacterSaveData CSD = new CharacterSaveData
            {
                id = CD.id,

                Name = CD.Name,
                Gender = CD.Gender,
                Role = CD.Role,

                HealthScore = CD.HealthScore,
                HungerScore = CD.HungerScore,
                RestScore = CD.RestScore,
                WorkEthicScore = CD.WorkEthicScore,
                Level = CD.Level,
                XP = CD.XP,
                MeleeDamage = CD.MeleeDamage,

                Ability_Persuade_Name = CD.Ability_Persuade_Name,
                Ability_Persuade_Desc = CD.Ability_Persuade_Desc,
                Ability_Persuade_Level = CD.Ability_Persuade_Level,
                Ability_Persuade_XP = CD.Ability_Persuade_XP,

                Ability_Rally_Name = CD.Ability_Rally_Name,
                Ability_Rally_Desc = CD.Ability_Rally_Desc,
                Ability_Rally_Level = CD.Ability_Rally_Level,
                Ability_Rally_XP = CD.Ability_Rally_XP,

                Skill_Flatten_Name = CD.Skill_Flatten_Name,
                Skill_Flatten_Desc = CD.Skill_Flatten_Desc,
                Skill_Flatten_Level = CD.Skill_Flatten_Level,
                Skill_Flatten_XP = CD.Skill_Flatten_XP,

                Skill_Mine_Name = CD.Skill_Mine_Name,
                Skill_Mine_Desc = CD.Skill_Mine_Desc,
                Skill_Mine_Level = CD.Skill_Mine_Level,
                Skill_Mine_XP = CD.Skill_Mine_XP,

                Skill_Forage_Name = CD.Skill_Forage_Name,
                Skill_Forage_Desc = CD.Skill_Forage_Desc,
                Skill_Forage_Level = CD.Skill_Forage_Level,
                Skill_Forage_XP = CD.Skill_Forage_XP,

                Strength = CD.Strength,
                Endurance = CD.Endurance,
                Resilience = CD.Resilience,
                Dexterity = CD.Dexterity,
                Intellect = CD.Intellect,
                Perception = CD.Perception,
                Willpower = CD.Willpower,
                Wisdom = CD.Wisdom,
                Charisma = CD.Charisma,
                Luck = CD.Luck
                
            };

            return CSD;
        }

    }

}

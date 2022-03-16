using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("CharacterData")]
public class CharacterData : MonoBehaviour
{
    // Info
    [XmlElement("Name")]
    public string Name;

    [XmlElement("Gender")]
    public string Gender;

    [XmlElement("Biography")]
    public string Biography;

    [XmlElement("Background")]
    public string Background;

    [XmlElement("Role")]
    public string Role;


    // Stats
    [XmlElement("HealthScore")]
    public string HealthScore;

    [XmlElement("Level")]
    public string Level;

    [XmlElement("XP")]
    public string XP;

    [XmlElement("MeleeDamage")]
    public string MeleeDamage;


    // Stats/Abilities
    [XmlElement("Name")]
    public string Ability_Persuade_Name;

    [XmlElement("Level")]
    public string Ability_Persuade_Level;

    [XmlElement("XP")]
    public string Ability_Persuade_XP;

    [XmlElement("Description")]
    public string Ability_Persuade_Desc;


    [XmlElement("Name")]
    public string Ability_Rally_Name;

    [XmlElement("Level")]
    public string Ability_Rally_Level;

    [XmlElement("XP")]
    public string Ability_Rally_XP;

    [XmlElement("Description")]
    public string Ability_Rally_Desc;

    // Stats/Skills
    [XmlElement("Name")]
    public string Skill_Flatten_Name;

    [XmlElement("Level")]
    public string Skill_Flatten_Level;

    [XmlElement("XP")]
    public string Skill_Flatten_XP;

    [XmlElement("Description")]
    public string Skill_Flatten_Desc;


    [XmlElement("Name")]
    public string Skill_Mine_Name;

    [XmlElement("Level")]
    public string Skill_Mine_Level;

    [XmlElement("XP")]
    public string Skill_Mine_XP;

    [XmlElement("Description")]
    public string Skill_Mine_Desc;


    [XmlElement("Name")]
    public string Skill_Forage_Name;

    [XmlElement("Level")]
    public string Skill_Forage_Level;
    
    [XmlElement("XP")]
    public string Skill_Forage_XP;

    [XmlElement("Description")]
    public string Skill_Forage_Desc;






    // Stats/StatModifiers
    [XmlElement("Strength")]
    public string Strength;

    [XmlElement("Endurance")]
    public string Endurance;

    [XmlElement("Resilience")]
    public string Resilience;

    [XmlElement("Dexterity")]
    public string Dexterity;

    [XmlElement("Intellect")]
    public string Intellect;

    [XmlElement("Perception")]
    public string Perception;

    [XmlElement("Willpower")]
    public string Willpower;

    [XmlElement("Wisdom")]
    public string Wisdom;

    [XmlElement("Charisma")]
    public string Charisma;

    [XmlElement("Luck")]
    public string Luck;

    // Traits








}

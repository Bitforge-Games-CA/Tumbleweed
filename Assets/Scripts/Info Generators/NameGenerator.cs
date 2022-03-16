using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameGenerator : MonoBehaviour
{
    public static string[] settlerMaleNameList = { "John", "Willy", "Pete", "Matthew", "Smiley", "Doug", "Mark", "William", "Thomas", "George", "Charles", "Henry", "Samuel", "Alfred", "Frederick", "David", "Stephen", "Edmund", "Tom", "Michael", "Herbert", "Philip", "Eli", "Reuben", "Evan" };

    public static string[] settlerFemaleNameList = { "Mary", "Elizabeth", "Sarah", "Ann", "Jane", "Emma", "Eliza", "Ellen", "Margaret", "Hannah", "Emily", "Harriet", "Alice", "Louise", "Catherine", "Caroline", "Susanna", "Elenaor", "Ruby", "Julia", "Ruth", "Rose", "Beth", "Julia", "Margot" };

    public static string[] prospectorMaleNameList = { "1", "", "", "", "", "2", "", "", "", "", "3", "", "", "", "", "4", "", "", "", "", "5", "", "", "", "" };

    public static string[] prospectorFemaleNameList = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

    public static string[] cowboyMaleNameList = { "1", "", "", "", "", "2", "", "", "", "", "3", "", "", "", "", "4", "", "", "", "", "5", "", "", "", "" };

    public static string[] cowboyFemaleNameList = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

    public static string[] banditMaleNameList = { "Johnathan", "Ulysses", "Jenkins", "Arnold", "Oswald", "Eric", "Daniels", "Leeroy", "Reginald", "Curly", "Watson", "Godfrey", "Dick", "Alan", "Stanley", "Titus", "Abraham", "Percival", "Oscar", "Howell", "Jasper", "Marshell", "Lenny", "Barret", "Ben" };

    public static string[] banditFemaleNameList = { "Thomasin", "Joan", "Myra", "Maud", "Penelope", "Philipa", "Adele", "Cecila", "Dora", "Eveline", "Lizzy", "Nelly", "Drusella", "Helen", "Jenny", "Sheila", "Shelly", "Margo", "Jan", "Kara", "Lucell", "Mina", "Olivia", "Sarah", "" };



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static string GenerateName(string type, string gender)
    {
        string name = "";

        int nameIndex = Random.Range(0, 24);

        if (type == "Settler" && gender == "Male")
        {
            name = settlerMaleNameList[nameIndex];
            return name;
        }

        if (type == "Settler" && gender == "Female")
        {
            name = settlerMaleNameList[nameIndex];
            return name;
        }

        if (type == "Prospector" && gender == "Male")
        {
            name = prospectorMaleNameList[nameIndex];
            return name;
        }

        if (type == "Prospector" && gender == "Female")
        {
            name = prospectorFemaleNameList[nameIndex];
            return name;
        }

        if (type == "Cowboy" && gender == "Male")
        {
            name = cowboyMaleNameList[nameIndex];
            return name;
        }

        if (type == "Cowboy" && gender == "Female")
        {
            name = cowboyFemaleNameList[nameIndex];
            return name;
        }

        if (type == "Bandit" && gender == "Male")
        {
            name = banditMaleNameList[nameIndex];
            return name;
        }

        if (type == "Bandit" && gender == "Female")
        {
            name = banditFemaleNameList[nameIndex];
            return name;
        }


        return "No type or gender found for name";
    }








}

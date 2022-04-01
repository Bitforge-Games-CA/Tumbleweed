using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.CharacterGen
{

    public class NameGenerator : MonoBehaviour
    {
        public static string[] SettlerMaleNameList = { "John", "Willy", "Pete", "Matthew", "Smiley", "Doug", "Mark", "William", "Thomas", "George", "Charles", "Henry", "Samuel", "Alfred", "Frederick", "David", "Stephen", "Edmund", "Tom", "Michael", "Herbert", "Philip", "Eli", "Reuben", "Evan" };

        public static string[] SettlerFemaleNameList = { "Mary", "Elizabeth", "Sarah", "Ann", "Jane", "Emma", "Eliza", "Ellen", "Margaret", "Hannah", "Emily", "Harriet", "Alice", "Louise", "Catherine", "Caroline", "Susanna", "Elenaor", "Ruby", "Julia", "Ruth", "Rose", "Beth", "Julia", "Margot" };

        public static string[] ProspectorMaleNameList = { "1", "", "", "", "", "2", "", "", "", "", "3", "", "", "", "", "4", "", "", "", "", "5", "", "", "", "" };

        public static string[] ProspectorFemaleNameList = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        public static string[] CowboyMaleNameList = { "1", "", "", "", "", "2", "", "", "", "", "3", "", "", "", "", "4", "", "", "", "", "5", "", "", "", "" };

        public static string[] CowboyFemaleNameList = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

        public static string[] BanditMaleNameList = { "Johnathan", "Ulysses", "Jenkins", "Arnold", "Oswald", "Eric", "Daniels", "Leeroy", "Reginald", "Curly", "Watson", "Godfrey", "Dick", "Alan", "Stanley", "Titus", "Abraham", "Percival", "Oscar", "Howell", "Jasper", "Marshell", "Lenny", "Barret", "Ben" };

        public static string[] BanditFemaleNameList = { "Thomasin", "Joan", "Myra", "Maud", "Penelope", "Philipa", "Adele", "Cecila", "Dora", "Eveline", "Lizzy", "Nelly", "Drusella", "Helen", "Jenny", "Sheila", "Shelly", "Margo", "Jan", "Kara", "Lucell", "Mina", "Olivia", "Sarah", "" };



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
                name = SettlerMaleNameList[nameIndex];
                return name;
            }

            if (type == "Settler" && gender == "Female")
            {
                name = SettlerMaleNameList[nameIndex];
                return name;
            }

            if (type == "Prospector" && gender == "Male")
            {
                name = ProspectorMaleNameList[nameIndex];
                return name;
            }

            if (type == "Prospector" && gender == "Female")
            {
                name = ProspectorFemaleNameList[nameIndex];
                return name;
            }

            if (type == "Cowboy" && gender == "Male")
            {
                name = CowboyMaleNameList[nameIndex];
                return name;
            }

            if (type == "Cowboy" && gender == "Female")
            {
                name = CowboyFemaleNameList[nameIndex];
                return name;
            }

            if (type == "Bandit" && gender == "Male")
            {
                name = BanditMaleNameList[nameIndex];
                return name;
            }

            if (type == "Bandit" && gender == "Female")
            {
                name = BanditFemaleNameList[nameIndex];
                return name;
            }


            return "No type or gender found for name";
        }








    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameMenu : MonoBehaviour
{
    public static InputField TownName;
    public static InputField Seed;
    public Text Dialog;

    public void Generate()
    {
        TownName = transform.Find("NGMTownNameInputField").GetComponent<InputField>();
        Seed = transform.Find("NGMSeedInputField").GetComponent<InputField>();
        Dialog = transform.Find("NGMDialog").GetComponent<Text>();

        if (TownName.text.ToString().Length > 0 && Seed.text.ToString().Length > 0)
        {
            Dialog.text = "Generating...";
            SceneManager.LoadScene(4);

        } else
        {
            Dialog.text = "Nothing selected";
        }
    }



       public void Back()
    {
        SceneManager.LoadScene(0);
    }
}

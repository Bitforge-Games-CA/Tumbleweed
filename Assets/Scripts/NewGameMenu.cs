using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameMenu : MonoBehaviour
{
    public static InputField townName;
    public static InputField seed;
    public Text dialog;

    public void Generate()
    {
        townName = transform.Find("NGMTownNameInputField").GetComponent<InputField>();
        seed = transform.Find("NGMSeedInputField").GetComponent<InputField>();
        dialog = transform.Find("NGMDialog").GetComponent<Text>();

        if (townName.text.ToString().Length > 0 && seed.text.ToString().Length > 0)
        {
            dialog.text = "Generating...";
            SceneManager.LoadScene(4);

        } else
        {
            dialog.text = "Nothing selected";
        }
    }



       public void Back()
    {
        SceneManager.LoadScene(0);
    }
}

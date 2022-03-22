using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text TownNameUI;

    // Start is called before the first frame update
    void Start()
    {
        // set the town name
        TownNameUI = transform.Find("MGTownName").GetComponent<Text>();
        TownNameUI.text = NewGameMenu.TownName.text;

        // Default frame rate
        Application.targetFrameRate = -1;
    }
}

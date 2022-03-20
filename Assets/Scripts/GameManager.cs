using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text townNameUI;

    // Start is called before the first frame update
    void Start()
    {
        // set the town name
        townNameUI = transform.Find("MGTownName").GetComponent<Text>();
        townNameUI.text = NewGameMenu.townName.text;

        Application.targetFrameRate = -1;





    }

    // Update is called once per frame
    void Update()
    { 

    }
    
}

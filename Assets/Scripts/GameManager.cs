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
        townNameUI = transform.Find("MGTownName").GetComponent<Text>();
        townNameUI.text = NewGameMenu.townName.text;

        // Update is called once per frame
        void Update()
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tumbleweed.Core.UI;

namespace Tumbleweed.Core.Managers
{

    public class GameManager : MonoBehaviour
    {
        public Text TownNameUI;

        // Start is called before the first frame update
        void Start()
        {
            // set the town name
            TownNameUI = GameObject.Find("MGTownName").GetComponent<Text>();
            TownNameUI.text = NewGameMenu.TownName.text;

            // Default frame rate
            Application.targetFrameRate = -1;
        }

    }

}
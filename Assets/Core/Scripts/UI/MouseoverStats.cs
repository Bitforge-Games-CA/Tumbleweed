using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tumbleweed.Core.XML.Data;
using Tumbleweed.Core.UtilityAI;

namespace Tumbleweed.Core.UI
{

    public class MouseoverStats : MonoBehaviour
    {

        public static MouseoverStats current;

        public GameObject CharacterStatsPanel;
        public Text CSPNameText;
        public Text CSPXPText;
        public Text CSPBestAvailableActionText;
        public Text CSPHungerText;
        public Text CSPRestText;


        public CharacterData CharacterData;
        public AIBrain AIBrain;


        void Awake()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }
        }

        public void Show()
        {
            // setup the canvas
            CharacterStatsPanel.SetActive(true);
            Canvas canvas = CharacterStatsPanel.GetComponentInChildren<Canvas>();
            canvas.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1);

            // setup the stat readout
            CSPNameText.text = CharacterData.Name;
            CSPXPText.text = Convert.ToString(CharacterData.XP);

            CSPHungerText.text = Convert.ToString(CharacterData.HungerScore);
            CSPRestText.text = Convert.ToString(CharacterData.RestScore);

            if (AIBrain != null && AIBrain.BestAction != null)
            {
                CSPBestAvailableActionText.text = AIBrain.BestAction.ToString();
            } 
            else
            {
                CSPBestAvailableActionText.text = "No task";
            }

        }

        public void Hide()
        {
            CharacterStatsPanel.SetActive(false);
            CharacterData = null;
        }



    }

}
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

        public GameObject CharacterStatsPanel;
        public Text CSPNameLabel;
        public Text CSPXPLabel;
        public Text CSPBestAvailableAction;


        public CharacterData CharacterData;
        public AIBrain AIBrain;

        // Start is called before the first frame update
        void Start()
        {
            CharacterStatsPanel.SetActive(false);

        }

        private void OnMouseOver()
        {
            // setup the canvas
            CharacterStatsPanel.SetActive(true);
            Canvas canvas = CharacterStatsPanel.GetComponentInChildren<Canvas>();
            canvas.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 5);

            // setup the stat readouts
            CharacterData = gameObject.GetComponent<CharacterData>();
            CSPNameLabel.text = CharacterData.Name;
            CSPXPLabel.text = Convert.ToString(CharacterData.XP);

            AIBrain = gameObject.GetComponent<AIBrain>();
            CSPBestAvailableAction.text = AIBrain.BestAction.ToString();


        }

        private void OnMouseExit()
        {
            CharacterStatsPanel.SetActive(false);
            CharacterData = null;
        }



    }

}
using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.XML.Data;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI
{
    public class NPCController : MonoBehaviour
    {

        //public MoveController Mover { get; set; }
        public AIBrain AIBrain { get; set; }
        public Action[] ActionsAvailable;
        public CharacterData CharacterData;

        // Start is called before the first frame update
        void Start()
        {
            //Mover = GetComponent<MoveController>();
            AIBrain = gameObject.GetComponent<AIBrain>();
            CharacterData = gameObject.GetComponent<CharacterData>();
        }

    }

}
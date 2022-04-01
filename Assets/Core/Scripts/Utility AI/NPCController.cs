using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.XML.Data;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI
{
    public class NPCController : MonoBehaviour
    {
        public int Characterid;
        public AIBrain AIBrain { get; set; }
        public Action[] ActionsAvailable;
        public CharacterData CharacterData;
        // public MoveController MoveController;

        void Awake()
        {
            //Mover = GetComponent<MoveController>();
            AIBrain = gameObject.GetComponent<AIBrain>();
            CharacterData = gameObject.GetComponent<CharacterData>();
        }

    }

}
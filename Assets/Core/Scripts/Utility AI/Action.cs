using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.Managers;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI
{

    public abstract class Action : ScriptableObject
    {

        public string Name;
        public float Timer;
        private float _score;

        public float Score
        {
            get { return _score; }
            set { _score = Mathf.Clamp01(value); }
        }

        public Consideration[] Considerations;

        public Task RequiredDestination;

        public virtual void Awake()
        {
            Score = 0;
        }

        public abstract void Execute(AIBrain aiBrain, NPCController npc);

        public abstract void SetRequiredDestination(NPCController npc, MoveController mc);
    }

}
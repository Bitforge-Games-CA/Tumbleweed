using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;
using System;

namespace Tumbleweed.Core.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Eat", menuName ="UtilityAI/Actions/Eat")]
    public class Eat : Action
    {
        public bool IsEating;
        private NPCController NPC;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (!TimeManager.current.PausedTime)
                {
                    Timer = 0;
                    IsEating = true;
                    NPC = npc;
                    TimeManager.current.OnTickShort += Eat_OnTickShort;
                }

            }

        }

        private void Eat_OnTickShort(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsEating)
                {
                    Timer += Time.deltaTime;
                    //Debug.Log(Timer / TimeManager.current.TimeScale);
                    if (Timer >= TimeManager.current.TimeScale * 0.5)
                    {
                        IsEating = false;
                        // stat gain
                        
                        Debug.Log("Finished eating");
                    }

                }

            }

        }



    }

}
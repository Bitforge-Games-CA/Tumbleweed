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
        private AIBrain AIBrain;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                Debug.Log("Starting to eat");

                Timer = 0;
                IsEating = true;
                AIBrain = aiBrain;
                NPC = npc;
                TimeManager.current.OnTickShort += Eat_OnTickShort;

            }

        }

        public override void SetRequiredDestination(NPCController npc, MoveController mc)
        {
            // TODO implement logic
        }

        private void Eat_OnTickShort(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsEating)
                {
                    Timer += Time.deltaTime;

                    if (Timer >= TimeManager.current.TimeScale * 0.5)
                    {
                        IsEating = false;

                        // stat gain
                        NPC.CharacterData.HungerScore -= (int)33.3f;
                        
                        // finish eating
                        Debug.Log("Finished eating");
                        AIBrain.FinishedExecutingBestAction = true;
                    }

                }

            }

        }



    }

}
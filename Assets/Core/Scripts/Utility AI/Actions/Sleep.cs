using System;
using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.Managers;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Sleep", menuName = "UtilityAI/Actions/Sleep")]
    public class Sleep : Action
    {
        public bool IsSleeping;
        private NPCController NPC;
        private AIBrain AIBrain;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                Debug.Log("Starting to sleep");

                Timer = 0;
                IsSleeping = true;
                AIBrain = aiBrain;
                NPC = npc;
                TimeManager.current.OnTickHour += Sleep_OnTickHour;

            }

        }

        public override void SetRequiredDestination(NPCController npc, MoveController mc)
        {
            // TODO implement logic

            //RequiredDestination = npc.CharacterData.SleepingSpot
            // TO ADD: movement logic
            //Mover.MoveTo()
        }

        private void Sleep_OnTickHour(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsSleeping)
                {
                    Timer += 1;

                    if (Timer >= 8) // TO ADD: Add RestRate variable in CharacterData
                    {
                        IsSleeping = false;

                        // stat gain
                        NPC.CharacterData.RestScore = 100;

                        // finish sleeping
                        Debug.Log("Finished sleeping");
                        AIBrain.FinishedExecutingBestAction = true;
                    }

                }

            }

        }
    }


}
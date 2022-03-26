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

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (!TimeManager.current.PausedTime)
                {
                    Timer = 0;
                    IsSleeping = true;
                    NPC = npc;
                    TimeManager.current.OnTickHour += Sleep_OnTickHour;
                }

            }

        }


        private void Sleep_OnTickHour(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsSleeping)
                {
                    Timer += 1;
                    //Debug.Log(Timer / TimeManager.current.TimeScale);
                    if (Timer >= 8)
                    {
                        IsSleeping = false;
                        // stat gain
                        NPC.CharacterData.RestScore = 100;
                        Debug.Log("Finished sleeping");
                    }

                }

            }

        }
    }


}
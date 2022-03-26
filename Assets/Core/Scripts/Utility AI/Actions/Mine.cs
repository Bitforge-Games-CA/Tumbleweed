using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.UtilityAI;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Mine", menuName = "UtilityAI/Actions/Mine")]
    public class Mine : Action
    {
        public bool IsWorking;
        private NPCController NPC;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {

            if (!TimeManager.current.PausedTime)
            {
                Timer = 0;
                IsWorking = true;
                NPC = npc;
                TimeManager.current.OnTickShort += Mine_OnTickShort;
                
                //aiBrain.StartCoroutine(ActionCoroutines.WorkCoroutine(aiBrain, this));
            }

        }

        private void Mine_OnTickShort(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsWorking)
                {
                    Timer += Time.deltaTime;
                    //Debug.Log(Timer / TimeManager.current.TimeScale);
                    if (Timer >= TimeManager.current.TimeScale * 1)
                    {
                        IsWorking = false;
                        NPC.CharacterData.XP += 10 * NPC.CharacterData.Level;
                        NPC.CharacterData.Skill_Mine_XP += 10 * NPC.CharacterData.Level;
                        Debug.Log("Finished working");
                    }

                }

            }

        }

    }

}


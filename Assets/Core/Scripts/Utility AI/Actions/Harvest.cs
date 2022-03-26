using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;
using System;

namespace Tumbleweed.Core.UtilityAI.Actions
{

    [CreateAssetMenu(fileName = "Harvest", menuName = "UtilityAI/Actions/Harvest")]
    public class Harvest : Action
    {
        public bool IsWorking;
        private NPCController NPC;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (!TimeManager.current.PausedTime)
                {
                    Timer = 0;
                    IsWorking = true;
                    NPC = npc;
                    TimeManager.current.OnTickShort += Harvest_OnTickShort;
                }

            }

        }

        private void Harvest_OnTickShort(object sender, EventArgs e)
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
                        // stat gain
                        NPC.CharacterData.Skill_Forage_Level += 10 * NPC.CharacterData.Level;
                        NPC.CharacterData.XP += 10 * NPC.CharacterData.Level;
                        Debug.Log("Finished working");
                    }

                }

            }

        }

    }

}


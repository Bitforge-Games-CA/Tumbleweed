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
        private AIBrain AIBrain;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {

            if (!TimeManager.current.PausedTime)
            {
                Debug.Log("Starting to mine");

                Timer = 0;
                IsWorking = true;
                AIBrain = aiBrain;
                NPC = npc;
                TimeManager.current.OnTickShort += Mine_OnTickShort;
                
                //aiBrain.StartCoroutine(ActionCoroutines.WorkCoroutine(aiBrain, this));
            }

        }

        public override void SetRequiredDestination(NPCController npc, MoveController mc)
        {
            float distance = Mathf.Infinity;
            Job nearestJob = null;

            List<Job> jobs = JobManager.current.JobsListMine;

            foreach (Job job in jobs)
            {
                float distanceFromJob = Vector3.Distance(job.Positions[0], NPC.transform.position);
                if (distanceFromJob < distance)
                {
                    nearestJob = job;
                    distance = distanceFromJob;
                }
            }

            RequiredDestination = nearestJob;
        }

        private void Mine_OnTickShort(object sender, EventArgs e)
        {
            if (!TimeManager.current.PausedTime)
            {
                if (IsWorking)
                {
                    Timer += Time.deltaTime;

                    if (Timer >= TimeManager.current.TimeScale * 1)
                    {
                        IsWorking = false;

                        // stat gain
                        NPC.CharacterData.XP += 10 * NPC.CharacterData.Level;
                        NPC.CharacterData.Skill_Mine_XP += 10 * NPC.CharacterData.Level;

                        // finish working
                        Debug.Log("Finished working (mine)");
                        AIBrain.FinishedExecutingBestAction = true;
                        
                    }

                }

            }

        }

    }

}


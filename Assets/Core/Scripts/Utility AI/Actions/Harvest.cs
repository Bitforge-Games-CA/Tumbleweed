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
        private AIBrain AIBrain;

        public override void Execute(AIBrain aiBrain, NPCController npc)
        {
            if (!TimeManager.current.PausedTime)
            {
                Debug.Log("Starting to harvest");

                Timer = 0;
                IsWorking = true;
                AIBrain = aiBrain;
                NPC = npc;
                TimeManager.current.OnTickShort += Harvest_OnTickShort;

            }

        }

        public override void SetRequiredDestination(NPCController npc, MoveController mc)
        {
            float distance = Mathf.Infinity;
            Job nearestJob = null;

            List<Job> jobs = JobManager.current.JobsListHarvest;

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

        private void Harvest_OnTickShort(object sender, EventArgs e)
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
                        NPC.CharacterData.Skill_Forage_Level += 10 * NPC.CharacterData.Level;
                        NPC.CharacterData.XP += 10 * NPC.CharacterData.Level;

                        // finished working
                        Debug.Log("Finished working (harvest)");
                        AIBrain.FinishedExecutingBestAction = true;
                    }

                }

            }

        }

    }

}


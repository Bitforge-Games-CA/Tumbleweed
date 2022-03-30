using System;
using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.UtilityAI;
using UnityEngine;

[CreateAssetMenu(fileName = "Haul", menuName = "UtilityAI/Actions/Haul")]
public class Haul : Tumbleweed.Core.UtilityAI.Action
{
    public bool IsWorking;
    private NPCController NPC;
    private AIBrain AIBrain;

    public override void Execute(AIBrain aiBrain, NPCController npc)
    {

        if (!TimeManager.current.PausedTime)
        {
            Debug.Log("Starting to haul");

            Timer = 0;
            IsWorking = true;
            AIBrain = aiBrain;
            NPC = npc;
            TimeManager.current.OnTickShort += Haul_OnTickShort;

            //aiBrain.StartCoroutine(ActionCoroutines.WorkCoroutine(aiBrain, this));
        }
    }

    public override void SetRequiredDestination(NPCController npc, MoveController mc)
    {
        float distance = Mathf.Infinity;
        Job nearestJob = null;

        List<Job> jobs = JobManager.current.JobsListHaul;

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

    private void Haul_OnTickShort(object sender, EventArgs e)
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
                    // TO ADD: remove resources from inventory
                    
                    // finish hauling
                    Debug.Log("Finished working (haul)");
                    AIBrain.FinishedExecutingBestAction = true;

                }

            }

        }

    }
}

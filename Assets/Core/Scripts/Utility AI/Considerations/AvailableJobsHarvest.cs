using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AvailableJobsHarvest", menuName = "UtilityAI/Considerations/Available Jobs Harvest")]
    public class AvailableJobsHarvest : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            if (JobManager.current.JobsListHarvest.Count > 0)
            {
                Score = ResponseCurve.Evaluate(Mathf.Clamp01(JobManager.current.JobsListHarvest.Count / 50));
                return Score;
            }
            return 0;
        }
    }

}
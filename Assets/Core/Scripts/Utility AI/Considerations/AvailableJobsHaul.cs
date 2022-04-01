using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AvailableJobsHaul", menuName = "UtilityAI/Considerations/Available Jobs Haul")]
    public class AvailableJobsHaul : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            if (JobManager.current.JobsListHaul.Count > 0)
            {
                Score = ResponseCurve.Evaluate(Mathf.Clamp01(JobManager.current.JobsListHaul.Count / 50));
                return Score;
            }
            return 0;
        }
    }

}
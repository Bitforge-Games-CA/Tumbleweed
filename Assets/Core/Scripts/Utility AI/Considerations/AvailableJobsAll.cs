using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AvailableJobsAll", menuName = "UtilityAI/Considerations/Available Jobs All")]
    public class AvailableJobsAll : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            if (JobManager.current.JobsListAll.Count > 0)
            {
                Score = ResponseCurve.Evaluate(Mathf.Clamp01(JobManager.current.JobsListAll.Count / 50));
                return Score;
            }
            return 0;
        }

    }
}
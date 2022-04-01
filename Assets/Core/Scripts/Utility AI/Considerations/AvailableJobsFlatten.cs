using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AvailableJobsFlatten", menuName = "UtilityAI/Considerations/Available Jobs Flatten")]
    public class AvailableJobsFlatten : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            if (JobManager.current.JobsListFlatten.Count > 0)
            {
                Score = ResponseCurve.Evaluate(Mathf.Clamp01(JobManager.current.JobsListFlatten.Count / 50));
                return Score;
            }
            return 0;
        }
    }

}
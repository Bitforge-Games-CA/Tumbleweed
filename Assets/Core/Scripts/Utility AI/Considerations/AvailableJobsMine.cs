using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.Managers;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "AvailableJobsMine", menuName = "UtilityAI/Considerations/Available Jobs Mine")]
    public class AvailableJobsMine : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            if (JobManager.current.JobsListMine.Count > 0)
            {
                Score = ResponseCurve.Evaluate(Mathf.Clamp01(JobManager.current.JobsListMine.Count / 50));
                return Score;
            }
            return 0;
        } 
    }

}
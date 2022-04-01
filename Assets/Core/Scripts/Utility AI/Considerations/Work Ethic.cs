using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.UtilityAI;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Work Ethic", menuName = "UtilityAI/Considerations/Work Ethic")]
    public class WorkEthic : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            Score = ResponseCurve.Evaluate(Mathf.Clamp01(npc.CharacterData.WorkEthicScore / 100f));
            return Score;
        }
    }

}

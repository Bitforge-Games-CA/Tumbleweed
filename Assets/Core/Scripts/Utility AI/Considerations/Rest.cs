using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.UtilityAI;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Rest", menuName = "UtilityAI/Considerations/Rest")]
    public class Rest : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            Score = ResponseCurve.Evaluate(Mathf.Clamp01(npc.CharacterData.RestScore / 100f));
            return Score;
        }
    }

}


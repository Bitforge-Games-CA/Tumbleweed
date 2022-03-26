using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Money", menuName = "UtilityAI/Considerations/Money")]
    public class Money : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            Score = ResponseCurve.Evaluate(Mathf.Clamp01(npc.CharacterData.XP / 100f));
            return Score;
        }
    }

}

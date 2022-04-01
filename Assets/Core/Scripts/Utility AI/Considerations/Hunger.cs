using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI.Considerations
{
    [CreateAssetMenu(fileName = "Hunger", menuName ="UtilityAI/Considerations/Hunger")]
    public class Hunger : Consideration
    {
        [SerializeField]
        private AnimationCurve ResponseCurve;

        public override float ScoreConsideration(NPCController npc)
        {
            Score = ResponseCurve.Evaluate(Mathf.Clamp01(npc.CharacterData.HungerScore / 100f));
            return Score;
        }
    }

}

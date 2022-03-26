using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tumbleweed.Core.XML.Data;
using Tumbleweed.Core.UtilityAI;

namespace Tumbleweed.Core.UtilityAI
{

    public enum State
    {
        Move,
        Decide,
        Execute
    }

    public class AIBrain : MonoBehaviour
    {
        public Action BestAction { get; set; }
        public NPCController NPC;
        public bool FinishedDeciding { get; set; }

        public State currentState { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            NPC = gameObject.GetComponent<NPCController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (BestAction == null)
            {
                DecideBestAction(NPC.ActionsAvailable);
                BestAction.Execute(this, NPC);
            }

        }


        // Loop through all the considerations of the action
        // Score all the considerations
        // "Average" the consideraton scores => overall action score
        public float ScoreAction(Action action)
        {

            float overallScore = 1f;

            for (int i = 0; i < action.Considerations.Length; i++)
            {

                float considerationScore = action.Considerations[i].ScoreConsideration(NPC);
                overallScore *= considerationScore;

                if (overallScore == 0)
                {
                    action.Score = 0;
                    return action.Score; // no point computing further
                }

            }

            // Averaging scheme of overall score
            // to rescale values to avoid downward
            // decimal multiplication shift
            float originalScore = overallScore;
            float modFactor = 1 - (1 / action.Considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.Score = originalScore + (makeupValue * originalScore);

            return action.Score;

        }

        // Loop through all the available actions and
        // give me the highest scoring action
        public void DecideBestAction(Action[] actionsAvailable)
        {

            float score = 0f;
            int nextBestActionIndex = 0;

            for (int i = 0; i < actionsAvailable.Length; i++)
            {

                if (ScoreAction(actionsAvailable[i]) > score)
                {
                    nextBestActionIndex = i;
                    score = actionsAvailable[i].Score;
                }

            }

            BestAction = actionsAvailable[nextBestActionIndex];
            FinishedDeciding = true;

        }
        
       public void OnFinishedAction()
        {
            FinishedDeciding = false;
            DecideBestAction(NPC.ActionsAvailable);
            BestAction.Execute(this, NPC);
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.Managers;
using UnityEngine;

namespace Tumbleweed.Core.UtilityAI
{

    public class ActionCoroutines : MonoBehaviour
    {
        public static IEnumerator WorkCoroutine(AIBrain aiBrain, Action action)
        {

            // implement more logic here

            yield return null;
        }

        public static IEnumerator SleepCoroutine(int time, AIBrain aiBrain)
        {

            yield return new WaitForSeconds(time);

            Debug.Log("I slept and gained 1 energy!");
            // Logic to update rest score

            //aiBrain.OnFinishedAction();
        }

        public static IEnumerator EatCoroutine(int time, AIBrain aiBrain)
        {

            yield return new WaitForSeconds(time);

            Debug.Log("I ate and lost 1 hunger!");
            // Logic to update hunger score

           //aiBrain.OnFinishedAction();
        }

        public static IEnumerator TickCoroutine()
        {
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using Tumbleweed.Core.UtilityAI;
using Tumbleweed.Core.XML;
using Tumbleweed.Core.XML.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tumbleweed.Core.UI

{

    public class MouseoverStatsTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {

            MouseoverStats.current.AIBrain = GetComponent<AIBrain>();
            MouseoverStats.current.CharacterData = GetComponent<CharacterData>();
            MouseoverStats.current.Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseoverStats.current.Hide();
            MouseoverStats.current.AIBrain = null;
            MouseoverStats.current.CharacterData = null;
        }

    }

}

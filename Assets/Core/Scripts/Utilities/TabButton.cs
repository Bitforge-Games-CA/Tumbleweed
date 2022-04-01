using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Tumbleweed.Core.Utilities
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public TabGroup TabGroup;

        public Image Background;

        public void OnPointerClick(PointerEventData eventData)
        {
            TabGroup.OnTabSelected(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TabGroup.OnTabExit(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TabGroup.OnTabEnter(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            Background = GetComponent<Image>();
            TabGroup.Subscribe(this);
        }

    }

}

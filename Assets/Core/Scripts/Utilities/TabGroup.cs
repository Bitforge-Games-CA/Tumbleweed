using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tumbleweed.Core.Utilities
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> TabButtons;

        public Sprite TabIdle;
        public Sprite TabHover;
        public Sprite TabActive;

        public TabButton SelectedTab;
        
        public void Subscribe(TabButton button)
        {
            if (TabButtons == null)
            {
                TabButtons = new List<TabButton>();
            }

            TabButtons.Add(button);
        }    

        public void OnTabEnter(TabButton button)
        {
            ResetTabs();
            button.Background.sprite = TabHover;
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }    

        public void OnTabSelected(TabButton button)
        {
            SelectedTab = button;
            ResetTabs();
            button.Background.sprite = TabActive;
        }

        public void ResetTabs()
        {
            foreach(TabButton button in TabButtons)
            {
                if(SelectedTab != null && button == SelectedTab) { continue; }
                button.Background.sprite = TabIdle;
            }
        }
    }

}

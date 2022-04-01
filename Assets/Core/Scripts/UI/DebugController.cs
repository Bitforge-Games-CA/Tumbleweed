using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumbleweed.Core.UI
{

    public class DebugController : MonoBehaviour
    {
        public static DebugController current;

        public bool ShowDebugConsole;
        public string DebugInput;

        private void Start()
        {
            // instatiate the singleton
            if (current == null)
            {
                current = this;
            }
            else if (current != this)
            {
                Destroy(current);
            }
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.BackQuote))
            {
                ShowDebugConsole = !ShowDebugConsole;
            }
        }

        private void OnGUI()
        {
            if (!ShowDebugConsole) { return; }

            float y = 0f;

            GUI.Box(new Rect(0, y, Screen.width, 60), "");
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            DebugInput = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), DebugInput);

        }

        public void Log(string message)
        {
            DebugInput = message;
        }

    }

}


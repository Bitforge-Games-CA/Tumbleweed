using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tumbleweed.Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        // Start is called before the first frame update

        public void NewGame()
        {
            SceneManager.LoadScene(1);
        }

        public void LoadGame()
        {

        }


        public void Quit()
        {
            Application.Quit();
        }

    }
}

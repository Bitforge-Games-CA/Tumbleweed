using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tumbleweed.Core.Managers;
using Tumbleweed.Core.XML;
using Tumbleweed.Core.XML.Data;

namespace Tumbleweed.Core.UI
{

    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject PauseMenuGO;
        [SerializeField] GameObject SettingsMenuGO;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true && PauseMenuGO.activeSelf == false && SettingsMenuGO.activeSelf == false)
            {
                Pause();

            }
            else if (Input.GetKeyDown(KeyCode.Escape) == true && PauseMenuGO.activeSelf == true && SettingsMenuGO.activeSelf == false)
            {
                Resume();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) == true && PauseMenuGO.activeSelf == false && SettingsMenuGO.activeSelf == true)
            {
                BackToPause();
            }
        }

        public void Pause()
        {
            TimeManager.current.PausedTime = true;
            PauseMenuGO.SetActive(true);
            Time.timeScale = 0.0f;
        }

        public void Resume()
        {
            TimeManager.current.PausedTime = false;
            PauseMenuGO.SetActive(false);
            Time.timeScale = 1.0f;
        }

        public void SaveGame()
        {
            XMLGameManager.SaveGameData(XMLGameManager.XMLGameData);
        }

        public void LoadGame()
        {
            XMLGameManager.XMLGameData = XMLGameManager.LoadGameData();
        }

        public void Settings()
        {
            SettingsMenuGO.SetActive(true);
            PauseMenuGO.SetActive(false);
        }

        public void BackToPause()
        {
            SettingsMenuGO.SetActive(false);
            PauseMenuGO.SetActive(true);
        }

        public void MainMenu(int sceneID)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneID);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}
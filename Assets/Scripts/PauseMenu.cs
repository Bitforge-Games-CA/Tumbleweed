using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuGO;
    [SerializeField] GameObject SettingsMenuGO;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true  && PauseMenuGO.activeSelf == false && SettingsMenuGO.activeSelf == false)
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
        PauseMenuGO.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        PauseMenuGO.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

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

    public void quitGame()
    {
        Application.Quit();
    }

}

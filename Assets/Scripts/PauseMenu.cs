using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true  && pauseMenu.activeSelf == false && settingsMenu.activeSelf == false)
        {
            Pause();

        } 
        else if (Input.GetKeyDown(KeyCode.Escape) == true && pauseMenu.activeSelf == true && settingsMenu.activeSelf == false)
        {
            Resume();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) == true && pauseMenu.activeSelf == false && settingsMenu.activeSelf == true)
        {
            BackToPause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
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
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
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

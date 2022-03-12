using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true  && pauseMenu.activeSelf == false)
        {
            Pause();

        } else if (Input.GetKeyDown(KeyCode.Escape) == true && pauseMenu.activeSelf == true)
        {
            Resume();
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

    public void MainMenu(int sceneID)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneID);
    }

}

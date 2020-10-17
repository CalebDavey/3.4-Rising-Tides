using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingScenes : MonoBehaviour
{
    public GameObject pauseMenu;

    public void startGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void quit()
    {
        Application.Quit();   
    }

    public void fail() 
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Fail Screen");
    }

    public void win()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Win Screen");
    }

    public void pause()
    {

        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}

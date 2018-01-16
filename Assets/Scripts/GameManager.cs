using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	GameObject[] sas = new GameObject[4000];

    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject optionsMenu, pauseMenu;
    private bool isPaused;
    private int Marcoculo;

    private void Start()
    {
        isPaused = false;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }
    public void EndGame()
    {
        gameOverScreen.SetActive(true);
        SaveData.instance.levelreached = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene("level1");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Pause()
    {
        if (isPaused)
        {
            pauseScreen.SetActive(false);
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);  
    }
}

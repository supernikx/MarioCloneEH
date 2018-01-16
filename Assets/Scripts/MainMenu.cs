using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject options, mainmenu, addtext, disabletext;
    private void Start()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
    }
    public void Play()
    {
        if (SaveData.instance.mario_datastored)
        {
            SceneManager.LoadScene(SaveData.instance.levelreached);
        }
        else
        {
            SceneManager.LoadScene("level1");
        }
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        mainmenu.SetActive(false);
        options.SetActive(true);
        if (SaveData.instance.player2spawned)
        {
            addtext.SetActive(false);
            disabletext.SetActive(true);
        }
        if (!SaveData.instance.player2spawned)
        {
            disabletext.SetActive(false);
            addtext.SetActive(true);
        }
    }

    public void AddDisablePlayer2()
    {
        if (disabletext.active)
        {
            disabletext.SetActive(false);
            addtext.SetActive(true);
            SaveData.instance.player2spawned = false;
        }
        else if (addtext.active)
        {
            addtext.SetActive(false);
            disabletext.SetActive(true);
            SaveData.instance.player2spawned = true;
        }
    }

    public void Back()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
    }
}

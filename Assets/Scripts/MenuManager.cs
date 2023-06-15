using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public SceneChanging changeScene;


    private void Awake()
    {
        instance = this;
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(changeScene.nextSceneLoad);
            if (changeScene.nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", changeScene.nextSceneLoad);
            }
        }

    }

    public void PositiveBtn()
    {
        AudioManager.Instance.Play("Button");
    }

    public void NegativeBtn()
    {
        AudioManager.Instance.Play("Back");
    }
    public void StartBtn()
    {
        SceneManager.LoadScene(10);
    }

    public void PauseBtn()
    {
        Time.timeScale = 0f;
        GameManager.Instance.PauseGame();
    }

    public void MateriBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ResumeBtn()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResumeGame();
    }

    public void RetryBtn()
    {
        GameManager.Instance.RetryGame();
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void ForestBtn()
    {
        SceneManager.LoadScene(11);
    }

    public void MountainBtn()
    {
        SceneManager.LoadScene(12);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void FullscreenResolution()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleMute()
    {
        AudioManager.Instance.ToggleMute();
    }
}
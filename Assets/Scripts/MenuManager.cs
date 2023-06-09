using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    [SerializeField] private int nextSceneLoad;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Debug.Log("TAMAT!");
        }
        else
        {
            SceneManager.LoadScene(nextSceneLoad);
            if (nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", nextSceneLoad);
            }
        }

    }
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseBtn()
    {
        Time.timeScale = 0f;
        GameManager.Instance.PauseGame();
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
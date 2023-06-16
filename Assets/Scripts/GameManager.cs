using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] TextMeshProUGUI texTutorial;

    [Header("Tutorial")]
    public GameObject tutorialGo;
    [SerializeField] private bool isTutorial = true;
    int indexTut = -3;

    [Header("References")]
    public static GameManager Instance;

    private void Awake()
    {
        isTutorial = PlayerPrefs.GetInt("isTutorial", 1) == 1;
        indexTut = PlayerPrefs.GetInt("indexTut", indexTut);
        Instance = this;
        if (isTutorial && indexTut !=6)
        {
            Time.timeScale = 0f;
            tutorialGo.SetActive(true);
            OkTutorKlik();
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }

    public void OkTutorKlik()
    {
        switch (indexTut)
        {
            case -3:
                texTutorial.text = "Gako, seorang kesatria, merasa prihatin akan kepunahan kebudayaan Indonesia akibat modernisasi..";
                break;
            case -2:
                texTutorial.text = "Untuk menyelamatkannya, ia menjelajahi daerah-daerah untuk mempelajari kebudayaan setempat";
                break;
            case -1:
                texTutorial.text = "Dengan tekad yang kuat untuk melestarikan kebudayaan Indonesia, Gako berpetualang dan belajar dari berbagai daerah.";
                break;
            case 0:
                texTutorial.text = "Selama perjalanannya, ia mengumpulkan kunci untuk membuka peta daerah yang belum terkenal, dan menghadapi hantu mistis yang menjaga kebudayaan misterius.";
                break;
            case 1:
                texTutorial.text = "Tutorial:" + "\n" + "Tekan 'A' atau panah kiri untuk berjalan ke kiri " + "\n" + "dan 'D' atau panah kanan untuk berjalan ke kanan";

                break;
            case 2:
                texTutorial.text = "Tekan 'Spasi' untuk melompat";

                break;
            case 3:
                texTutorial.text = "Tekan 'E' untuk interaksi dengan orang";

                break;
            case 4:
                texTutorial.text = "Tekan 'Z' untuk menyerang musuh";

                break;
            case 5:
                texTutorial.text = "Selamat Bermain!";

                break;
            case 6:
                isTutorial = false;
                tutorialGo.SetActive(false);
                Time.timeScale = 1f;
                PlayerPrefs.SetInt("isTutorial", isTutorial ? 1 : 0);
                PlayerPrefs.SetInt("indexTut", indexTut);
                break;
        }
        indexTut++;
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        AudioManager.Instance.Pause();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        AudioManager.Instance.Resume();
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int jumlahNyawa = 3;
    public int jumlahSoal = 0;
    public GameObject[] nyawaObject;
    public GameObject[] soalObject;
    public GameObject GameOverUI;
    public GameObject PauseGame;
    public UnityEvent Die;

    void Awake() 
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            MinHealth();
        }
    }

    // GAME PAUSE SCRIPT
    public void GamePause()
    {
        // Game Pause
        PauseGame.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameContinue() 
    {
        // Game Continue
        PauseGame.SetActive(false);
        Time.timeScale = 1;    
    }
    // GAME PAUSE SCRIPT
    
    public void MinHealth () {
        jumlahNyawa -= 1;
        HealthUpdate();

        if (jumlahNyawa <= 0)
        {
            GameOver();
        }
    }

    public void GetQuestion () {
        jumlahSoal += 1;
        SoalUpdateCount();
    }

    void GameOver() 
    {
        GameOverUI.SetActive(true);
    }
    
    void HealthUpdate()
    {
        foreach (GameObject Horizontal in nyawaObject)
        {
            Horizontal.SetActive(false);
        }

        for (int i = 0; i < jumlahNyawa; i++)
        {
            nyawaObject[i].SetActive(true);
        }
    }

    
    void SoalUpdateCount()
    {
        foreach (GameObject Horizontal in soalObject)
        {
            Horizontal.SetActive(false);
        }

        for (int i = 0; i < jumlahSoal; i++)
        {
            soalObject[i].SetActive(true);
        }
    }

    private void PlayerDie() 
    {
        Die.Invoke();
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] private int jumlahNyawa = 3;
    [SerializeField] private int jumlahSoal = 0;
    [SerializeField] private int jumlahCoin = 0;

    [Header("GameObject")]
    [SerializeField] private GameObject[] nyawaObject;
    [SerializeField] private GameObject[] soalObject;
    [SerializeField] private GameObject GameOverUI;

    [Header("TextObject")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI[] labelKalah;

    public UnityEvent Die;
    public static PlayerManager Instance;

    void Awake() 
    {
        Instance = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                MinHealth();
                break;
            case "Heart":
                IncreaseHearts(1);
                Destroy(other.gameObject);
                break;
            case "Coin":
                IncreaseCoins(1);
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }
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
        Time.timeScale = 0f;
        GameOverUI.SetActive(true);
        int randomIndex = Random.Range(0, labelKalah.Length);
        for (int i = 0; i < labelKalah.Length; i++)
        {
            if (i == randomIndex)
                labelKalah[i].gameObject.SetActive(true);
            else
                labelKalah[i].gameObject.SetActive(false);
        }
    }
    
    void HealthUpdate()
    {

        for (int i = 0; i < nyawaObject.Length; i++)
        {
            if (i < jumlahNyawa)
                nyawaObject[i].SetActive(true);
            else
                nyawaObject[i].SetActive(false);
        }
    }

    public void IncreaseHearts(int amount)
    {
        jumlahNyawa += amount;
        HealthUpdate();
    }
    public void IncreaseCoins(int counter)
    {
        jumlahCoin += counter;
        coinText.text = jumlahCoin.ToString();
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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishState : MonoBehaviour
{
    [Header("Panel")]
    public GameObject finishPanel;
    [SerializeField] private TMP_Text scoreHasil;

    [Header("Stars")]
    [SerializeField] private GameObject[] starsObject;

    [Header("References")]
    public QuestionManager questionManager;
    public PlayerManager playerManager;


    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.Play("Finish");
            finishPanel.SetActive(true);
            scoreHasil.text = questionManager.scorePlayer.ToString();
            UpdateStars();
            SavePlayerStatus();
        }
    }

    private void UpdateStars()
    {
        int score = questionManager.scorePlayer;

        switch (score)
        {
            case > 2000:
                ActivateStars(3);
                break;
            case >= 1000:
                ActivateStars(2);
                break;
            case > 500:
                ActivateStars(1);
                break;
            default:
                break;
        }

    }

    private void ActivateStars(int count)
    {
        for (int i = 0; i < starsObject.Length; i++)
        {
            starsObject[i].SetActive(i < count);
        }
    }

    public void SavePlayerStatus()
    {
        PlayerPrefs.SetInt("JumlahCoin", playerManager.jumlahCoin);
        PlayerPrefs.SetInt("ScorePlayer", questionManager.scorePlayer);
    }
}
